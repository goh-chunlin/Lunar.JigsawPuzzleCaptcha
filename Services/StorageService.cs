using Azure.Data.Tables;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StorageService : IStorageService
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

                var entity = new TableEntity("captcha", id)
                {
                    { "Id", id },
                    { "X", x },
                    { "Y", y },
                    { "CreatedAt", createdAt },
                    { "ExpiredAt", expiredAt }
                };

                tableClient.AddEntity(entity);
            }
            catch (Exception) 
            {
                return false;
            }            

            return true;
        }
    }
}
