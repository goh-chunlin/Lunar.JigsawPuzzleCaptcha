using Azure.Data.Tables;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StorageService : ICaptchaStorageService
    {
        private string _storageEndpoint;
        private string _tableName;
        private string _accountName;
        private string _accessKey;

        public StorageService(string storageEndpoint, string tableName, string accountName, string accessKey) 
        {
            _storageEndpoint = storageEndpoint;
            _tableName = tableName;
            _accountName = accountName;
            _accessKey = accessKey;
        }

        public bool Save(JigsawPuzzle jigsawPuzzle) 
        {
            try
            {
                var tableClient = new TableClient(new Uri(_storageEndpoint), _tableName, new TableSharedKeyCredential(_accountName, _accessKey));

                string id = jigsawPuzzle.Id;
                int x = jigsawPuzzle.X;
                int y = jigsawPuzzle.Y;
                DateTimeOffset createdAt = DateTimeOffset.Now;
                DateTimeOffset expiredAt = DateTimeOffset.Now.AddSeconds(30);

                var entity = new JigsawPuzzleEntity
                {
                    PartitionKey = "captcha",
                    RowKey = id,
                    Id = id,
                    X = x,
                    Y = y,
                    CreatedAt = createdAt,
                    ExpiredAt = expiredAt
                };

                tableClient.AddEntity(entity);
            }
            catch (Exception) 
            {
                return false;
            }            

            return true;
        }

        public async Task<JigsawPuzzleEntity> LoadAsync(string id) 
        {
            var tableClient = new TableClient(new Uri(_storageEndpoint), _tableName, new TableSharedKeyCredential(_accountName, _accessKey));
            
            var queryResultsFilter = tableClient.QueryAsync<JigsawPuzzleEntity>(r => r.Id == id);

            await foreach (JigsawPuzzleEntity qEntity in queryResultsFilter)
            {
                return qEntity;
            }

            return null;
        }
    }
}
