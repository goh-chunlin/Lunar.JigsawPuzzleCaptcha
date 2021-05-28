using Services.Models;

namespace Services
{
    public interface IPuzzleService
    {
        JigsawPuzzle CreateJigsawPuzzle(string imageUrl);

        bool IsPuzzleSolved(JigsawPuzzleInfo submission, JigsawPuzzleInfo record);
    }
}
