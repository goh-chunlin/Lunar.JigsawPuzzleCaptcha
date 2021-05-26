﻿using Services.Models;
using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace Services
{
    public class PieceService : IPieceService
    {
        const int PIECE_WIDTH = 88;
        const int PIECE_HEIGHT = 80;
        const int TAB_RADIUS = 8;

        public JigsawPuzzle CreateJigsawPuzzle(string imageUrl)
        {
            var jigsawPuzzle = new JigsawPuzzle();

            try
            {
                using (WebClient wc = new WebClient())
                {
                    using (Stream s = wc.OpenRead(imageUrl))
                    {
                        using (Bitmap originalImage = new Bitmap(s))
                        {
                            Random random = new Random();

                            int xRandom = random.Next(originalImage.Width - PIECE_WIDTH);
                            int yRandom = random.Next(originalImage.Height - PIECE_HEIGHT);

                            var puzzle = GenerateMissingPieceAndPuzzle(originalImage, xRandom, yRandom);

                            jigsawPuzzle.BackgroundImage = GetImageBase64(puzzle.JigsawPuzzle);
                            jigsawPuzzle.MissingPieceImage = GetImageBase64(puzzle.MissingPiece);
                            jigsawPuzzle.X = xRandom;
                            jigsawPuzzle.Y = yRandom;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return jigsawPuzzle;
        }

        public bool IsPuzzleSolved(JigsawPuzzleInfo submission, JigsawPuzzleInfo record) 
        {
            if (submission.Id != record.Id) return false;
            if (Math.Abs(submission.X - record.X) > 5) return false;
            if (submission.SubmittedAt > record.ExpiredAt) return false;

            return true;
        }

        private int[,] GetMissingPieceData()
        {
            int[,] data = new int[PIECE_WIDTH, PIECE_HEIGHT];

            double c1 = (PIECE_WIDTH - TAB_RADIUS) / 2;
            double c2 = PIECE_HEIGHT / 2;
            double squareOfTabRadius = Math.Pow(TAB_RADIUS, 2);

            double xBegin = PIECE_WIDTH - TAB_RADIUS;
            double yBegin = TAB_RADIUS;

            for (int i = 0; i < PIECE_WIDTH; i++)
            {
                for (int j = 0; j < PIECE_HEIGHT; j++)
                {
                    double d1 = Math.Pow(j, 2) + Math.Pow(i - c1, 2);
                    double d2 = Math.Pow(i - xBegin, 2) + Math.Pow(j - c2, 2);
                    if ((j <= yBegin && d1 < squareOfTabRadius) || (i >= xBegin && d2 > squareOfTabRadius))
                    {
                        data[i, j] = 0;
                    }
                    else
                    {
                        data[i, j] = 1;
                    }
                }
            }

            return data;
        }

        private (Bitmap MissingPiece, Bitmap JigsawPuzzle) GenerateMissingPieceAndPuzzle(Bitmap originalImage, int x, int y)
        {
            Bitmap missingPiece = new Bitmap(PIECE_WIDTH, PIECE_HEIGHT, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap jigsawPuzzle = new Bitmap(originalImage.Width, originalImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int i = 0; i < jigsawPuzzle.Width; i++)
            {
                for (int j = 0; j < jigsawPuzzle.Height; j++)
                {
                    jigsawPuzzle.SetPixel(i, j, originalImage.GetPixel(i, j));
                }
            }

            int[,] missingPiecePattern = GetMissingPieceData();

            for (int i = 0; i < PIECE_WIDTH; i++)
            {
                for (int j = 0; j < PIECE_HEIGHT; j++)
                {
                    int argb = missingPiecePattern[i, j];
                    int originalArgb = originalImage.GetPixel(x + i, y + j).ToArgb();

                    if (argb == 1)
                    {
                        missingPiece.SetPixel(i, j, Color.FromArgb(originalArgb));

                        jigsawPuzzle.SetPixel(x + i, y + j, FilterPixel(originalImage, x + i, y + j));
                    }
                    else
                    {
                        missingPiece.SetPixel(i, j, Color.Transparent);
                    }
                }
            }

            return (missingPiece, jigsawPuzzle);
        }

        private Color FilterPixel(Bitmap img, int x, int y)
        {
            const int KERNEL_SIZE = 3;
            int[,] kernel = new int[KERNEL_SIZE, KERNEL_SIZE];

            int xStart = x - 1;
            int yStart = y - 1;
            for (int i = xStart; i < KERNEL_SIZE + xStart; i++)
            {
                for (int j = yStart; j < KERNEL_SIZE + yStart; j++)
                {
                    int tx = i;
                    if (tx < 0)
                    {
                        tx = -tx;
                    }
                    else if (tx >= img.Width)
                    {
                        tx = x - 1;
                    }

                    int ty = j;
                    if (ty < 0)
                    {
                        ty = -ty;
                    }
                    else if (ty >= img.Height)
                    {
                        ty = y - 1;
                    }

                    kernel[i - xStart, j - yStart] = img.GetPixel(tx, ty).ToArgb();

                }
            }

            int r = 0;
            int g = 0;
            int b = 0;
            int count = KERNEL_SIZE * KERNEL_SIZE;
            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < kernel.GetLength(1); j++)
                {
                    Color c = (i == j) ? Color.Black : Color.FromArgb(kernel[i, j]);
                    r += c.R;
                    g += c.G;
                    b += c.B;
                }
            }

            return Color.FromArgb(r / count, g / count, b / count);
        }

        private string GetImageBase64(Bitmap image)
        {
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = stream.ToArray();

            string base64ImageContent = Convert.ToBase64String(imageBytes);

            return base64ImageContent;
        }
    }
}
