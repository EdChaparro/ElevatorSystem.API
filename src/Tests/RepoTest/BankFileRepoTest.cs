using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RepoTest
{
    [TestClass]
    public class BankFileRepoTest
    {
        private BankFileRepo _repo;

        [TestInitialize]
        public void Initialize()
        {
            var configManager = new RepoConfigurationManager("TestDb");
            _repo = new BankFileRepo(configManager);
            _repo.Clear(); //Clear all data
        }

        [TestMethod]
        public void ShouldPersistEntity()
        {
            var building = new Building { Name = "Foo" };

            var entity = new BuildingElevatorBank(building.Id, new Bank(2, 1..10));

            Assert.AreEqual(1, _repo.Create(entity));

            var deserializedEntity = _repo.FindById(entity.Id);

            Assert.IsNotNull(deserializedEntity);
            Assert.AreEqual(entity.Id, deserializedEntity.Id);
            Assert.AreEqual(entity.BuildingId, deserializedEntity.BuildingId);
        }

        #region Find
        [TestMethod]
        public void ShouldFindById()
        {
            var building = new Building { Name = "Foo" };

            var entity = new BuildingElevatorBank(building.Id, new Bank(2, 1..10));

            Assert.AreEqual(1, _repo.Create(entity));

            var persistedEntity = _repo.FindById(entity.Id);

            Assert.IsNotNull(persistedEntity);
            Assert.AreEqual(entity.BuildingId, persistedEntity.BuildingId);
            Assert.AreEqual(entity.Id, persistedEntity.Id);
        }

        [TestMethod]
        public void ShouldFindByBusinessId()
        {
            var building = new Building { Name = "Foo" };

            var entity = new BuildingElevatorBank(building.Id, new Bank(2, 1..10));

            Assert.AreEqual(1, _repo.Create(entity));

            var persistedEntity = _repo.FindByBusinessId(entity.BuildingId)
                .FirstOrDefault();

            Assert.IsNotNull(persistedEntity);
            Assert.AreEqual(entity.Id, persistedEntity.Id);
        }
        #endregion
    }
}