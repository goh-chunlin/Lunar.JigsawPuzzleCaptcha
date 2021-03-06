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
    public class CaptchaStorageUnitTest
    {
        private string _storageEndpoint;
        private string _tableName;
        private string _accountName;
        private string _accessKey;

        public CaptchaStorageUnitTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

            _storageEndpoint = configuration.GetValue<string>("CaptchaStorageEndpoint");
            _tableName = configuration.GetValue<string>("CaptchaStorageTableName");
            _accountName = configuration.GetValue<string>("CaptchaStorageAccountName");
            _accessKey = configuration.GetValue<string>("CaptchaStorageAccessKey");
        }

        [TestMethod]
        public void CreateMissingPieceAndBackground()
        {
            // Arrange
            var captchaStorageService = new CaptchaStorageService(_storageEndpoint, _tableName, _accountName, _accessKey);

            // Act
            var testJigsawPuzzle = new JigsawPuzzle { 
                X = 20, 
                Y = 400 
            };
            bool result = captchaStorageService.Save(testJigsawPuzzle);

            // Assert    
            Assert.IsTrue(result, "The entity is successfully saved.");
        }
    }
}
