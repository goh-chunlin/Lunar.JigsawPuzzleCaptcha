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
            string storageEndpoint = Environment.GetEnvironmentVariable("StorageEndpoint");
            string tableName = Environment.GetEnvironmentVariable("TableName");
            string accountName = Environment.GetEnvironmentVariable("AccountName");
            string accessKey = Environment.GetEnvironmentVariable("AccessKey");

            builder.Services.AddSingleton<IPieceService, PieceService>();
            builder.Services.AddSingleton<IStorageService>(new StorageService(storageEndpoint, tableName, accountName, accessKey));
        }
    }
}