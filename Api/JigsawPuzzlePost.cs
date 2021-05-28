using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services;
using Api.ViewModels;
using System.Text.Json;
using Services.Models;

namespace Api
{
    public class JigsawPuzzlePost
    {
        private readonly IPuzzleService _pieceService;
        private readonly ICaptchaStorageService _storageService;

        public JigsawPuzzlePost(IPuzzleService pieceService, ICaptchaStorageService storageService)
        {
            _pieceService = pieceService;
            _storageService = storageService;
        }

        [FunctionName("JigsawPuzzlePost")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "jigsaw-puzzle")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var puzzleSubmission = JsonSerializer.Deserialize<PuzzleSubmissionViewModel>(body, 
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var correspondingRecord = await _storageService.LoadAsync(puzzleSubmission.Id);

            var submission = new JigsawPuzzleInfo 
            {
                Id = puzzleSubmission.Id,
                X = puzzleSubmission.X,
                SubmittedAt = DateTimeOffset.Now
            };

            var record = new JigsawPuzzleInfo 
            {
                Id = correspondingRecord.Id,
                X = correspondingRecord.X,
                ExpiredAt = correspondingRecord.ExpiredAt
            };

            bool isPuzzleSolved = _pieceService.IsPuzzleSolved(submission, record);

            var response = new Response 
            {
                IsSuccessful = isPuzzleSolved,
                Message = isPuzzleSolved ? "The puzzle is solved" : "Sorry, time runs out or you didn't solve the puzzle"
            };

            return new OkObjectResult(response);
        }
    }
}
