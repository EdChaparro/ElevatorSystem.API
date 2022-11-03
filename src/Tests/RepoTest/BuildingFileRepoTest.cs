using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepoTest
{
    [TestClass]
    public class BuildingFileRepoTest
    {
        private BuildingFileRepo _repo;

        [TestInitialize]
        public void Initialize()
        {
            var configManager = new RepoConfigurationManager("TestDb");
            _repo = new BuildingFileRepo(configManager);
            _repo.Clear(); //Clear all data
        }

        [TestMethod]
        public void ShouldPersistEntity()
        {
            var entity = new Building { Name = "Foo" };

            Assert.AreEqual(1, _repo.Create(entity));

            var deserializedEntity = _repo.FindById(entity.Id);

            Assert.IsNotNull(deserializedEntity);
            Assert.AreEqual(entity.Id, deserializedEntity.Id);
            Assert.AreEqual(entity.Name, deserializedEntity.Name);
        }
    }
}