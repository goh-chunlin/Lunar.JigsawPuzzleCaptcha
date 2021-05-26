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
        private readonly IPieceService _pieceService;
        private readonly IStorageService _storageService;

        public JigsawPuzzleGet(IPieceService pieceService, IStorageService storageService) 
        {
            _pieceService = pieceService;
            _storageService = storageService;
        }

        [FunctionName("JigsawPuzzleGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "jigsaw-puzzle")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var jigsawPuzzle = _pieceService.CreateJigsawPuzzle("https://gclstorage.blob.core.windows.net/images/genshin-impact-01.png");
            _storageService.Save(jigsawPuzzle);

            return new OkObjectResult(jigsawPuzzle);
        }
    }
}
