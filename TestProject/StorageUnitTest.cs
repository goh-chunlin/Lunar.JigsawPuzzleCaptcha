using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class StorageUnitTest
    {
        private string _storageEndpoint;
        private string _tableName;
        private string _accountName;
        private string _accessKey;

        public StorageUnitTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

            _storageEndpoint = configuration.GetValue<string>("StorageEndpoint");
            _tableName = configuration.GetValue<string>("TableName");
            _accountName = configuration.GetValue<string>("AccountName");
            _accessKey = configuration.GetValue<string>("AccessKey");
        }

        [TestMethod]
        public void CreateMissingPieceAndBackground()
        {
            // Arrange
            var storageService = new StorageService(_storageEndpoint, _tableName, _accountName, _accessKey);

            // Act
            var testJigsawPuzzle = new JigsawPuzzle { 
                X = 20, 
                Y = 400 
            };
            bool result = storageService.Save(testJigsawPuzzle);

            // Assert    
            Assert.IsTrue(result, "The entity is successfully saved.");
        }
    }
}
