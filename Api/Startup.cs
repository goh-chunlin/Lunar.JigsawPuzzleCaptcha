using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using System;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string storageEndpoint = Environment.GetEnvironmentVariable("CaptchaStorageEndpoint");
            string tableName = Environment.GetEnvironmentVariable("CaptchaStorageTableName");
            string accountName = Environment.GetEnvironmentVariable("CaptchaStorageAccountName");
            string accessKey = Environment.GetEnvironmentVariable("CaptchaStorageAccessKey");
            string imageBlobStorageConnectionString = Environment.GetEnvironmentVariable("ImageBlobStorageConnectionString");
            string imageBlobContainerName = Environment.GetEnvironmentVariable("ImageBlobContainerName");

            builder.Services.AddSingleton<IPuzzleService, PuzzleService>();
            builder.Services.AddSingleton<ICaptchaStorageService>(new CaptchaStorageService(storageEndpoint, tableName, accountName, accessKey));
            builder.Services.AddSingleton<IPuzzleImageService>(new PuzzleImageService(imageBlobStorageConnectionString, imageBlobContainerName));
        }
    }
}