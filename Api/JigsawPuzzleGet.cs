using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Services;

namespace Api
{
    public class JigsawPuzzleGet
    {
        private readonly IPuzzleImageService _puzzleImageService;
        private readonly IPuzzleService _pieceService;
        private readonly ICaptchaStorageService _storageService;

        public JigsawPuzzleGet(IPuzzleImageService puzzleImageService, IPuzzleService pieceService, ICaptchaStorageService storageService) 
        {
            _puzzleImageService = puzzleImageService;
            _pieceService = pieceService;
            _storageService = storageService;
        }

        [FunctionName("JigsawPuzzleGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jigsaw-puzzle")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var availablePuzzleImageUrls = await _puzzleImageService.GetAllImageUrlsAsync();

            var random = new Random();
            string selectedPuzzleImageUrl = availablePuzzleImageUrls[random.Next(availablePuzzleImageUrls.Count)];

            var jigsawPuzzle = _pieceService.CreateJigsawPuzzle(selectedPuzzleImageUrl);
            _storageService.Save(jigsawPuzzle);

            return new OkObjectResult(jigsawPuzzle);
        }
    }
}
