using System;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepoTest
{
    [TestClass]
    public class RepoConfigurationManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailValidationWhenNoDatabaseNameProvided()
        {
            var configManager = new RepoConfigurationManager(string.Empty);
            configManager.ValidateConfiguration();
        }

        [TestMethod]
        public void ShouldDefaultDatabaseDirectory()
        {
            var configManager = new RepoConfigurationManager(string.Empty);

            Assert.IsNotNull(configManager.DatabaseBaseDirectory);
        }

        [TestMethod]
        public void ShouldConstructFullFilePath()
        {
            const string DATABASE_NAME = "MyDb";
            var configManager = new RepoConfigurationManager(DATABASE_NAME);

            Assert.IsNotNull(configManager.FilePath);
            Assert.AreEqual(DATABASE_NAME,
                configManager.FilePath
                    .Substring(configManager.FilePath.Length - DATABASE_NAME.Length));
        }
    }
}
