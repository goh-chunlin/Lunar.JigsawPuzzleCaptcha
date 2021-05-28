using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class PuzzleImageServiceUnitTest
    {
        private string _storageConnectionString;
        private string _containerName;

        public PuzzleImageServiceUnitTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

            _storageConnectionString = configuration.GetValue<string>("ImageBlobStorageConnectionString");
            _containerName = configuration.GetValue<string>("ImageBlobContainerName");
        }

        [TestMethod]
        public async Task GetImageUrls()
        {
            // Arrange
            var puzzleImageService = new PuzzleImageService(_storageConnectionString, _containerName);

            // Act
            var imageUrls = await puzzleImageService.GetAllImageUrlsAsync();

            // Assert    
            Assert.IsTrue(imageUrls.Count > 0, "There is at least one image!");
        }
    }
}
