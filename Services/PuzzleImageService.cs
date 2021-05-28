using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PuzzleImageService : IPuzzleImageService
    {
        private string _storageConnectionString;
        private string _containerName;

        public PuzzleImageService(string storageConnectionString, string containerName)
        {
            _storageConnectionString = storageConnectionString;
            _containerName = containerName;
        }

        public async Task<List<string>> GetAllImageUrlsAsync() 
        {
            var output = new List<string>();

            var container = new BlobContainerClient(_storageConnectionString, _containerName);

            var blobItems = container.GetBlobsAsync();

            await foreach (var blob in blobItems) 
            {
                var blobClient = container.GetBlobClient(blob.Name);
                output.Add(blobClient.Uri.ToString());
            }

            return output;
        }
    }
}
