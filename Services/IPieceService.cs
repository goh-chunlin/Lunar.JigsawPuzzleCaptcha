using Services.Models;

namespace Services
{
    public interface IPieceService
    {
        JigsawPuzzle CreateJigsawPuzzle(string imageUrl);

        bool IsPuzzleSolved(JigsawPuzzleInfo submission, JigsawPuzzleInfo record);
    }
}
