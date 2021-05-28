using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace TestProject
{
    [TestClass]
    public class JigsawPuzzleServiceUnitTest
    {
        [TestMethod]
        public void CreateMissingPieceAndBackground()
        {
            // Arrange
            var pieceService = new PuzzleService();

            // Act
            var resultMap = pieceService.CreateJigsawPuzzle("https://gclstorage.blob.core.windows.net/images/genshin-impact-01.png");

            // Assert    
            Assert.IsTrue(!string.IsNullOrWhiteSpace(resultMap.BackgroundImage), "There is a background image!");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(resultMap.MissingPieceImage), "There is a missing piece image!");
        }
    }
}
