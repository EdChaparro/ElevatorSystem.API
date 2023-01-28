using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RepoTest
{
    [TestClass]
    public class ElevatorFileRepoTest
    {
        private ElevatorFileRepo _repo;

        [TestInitialize]
        public void Initialize()
        {
            var configManager = new RepoConfigurationManager("TestDb");
            _repo = new ElevatorFileRepo(configManager);
            _repo.Clear(); //Clear all data
        }

        [TestMethod]
        public void ShouldPersistEntity()
        {
            var bank = new Bank { Name = "Foo" };

            var entity = new Elevator(bank.Id,
                new IntrepidProducts.ElevatorSystem.Elevators.Elevator(1..10));

            Assert.AreEqual(1, _repo.Create(entity));

            var deserializedEntity = _repo.FindById(entity.Id);

            Assert.IsNotNull(deserializedEntity);
            Assert.AreEqual(entity.Id, deserializedEntity.Id);
            Assert.AreEqual(entity.BankId, deserializedEntity.BankId);
        }

        #region Find
        [TestMethod]
        public void ShouldFindById()
        {
            var bank = new Bank { Name = "Foo" };

            var entity = new Elevator(bank.Id,
                new IntrepidProducts.ElevatorSystem.Elevators.Elevator(1..10));

            Assert.AreEqual(1, _repo.Create(entity));

            var persistedEntity = _repo.FindById(entity.Id);

            Assert.IsNotNull(persistedEntity);
            Assert.AreEqual(entity.BankId, persistedEntity.BankId);
            Assert.AreEqual(entity.Id, persistedEntity.Id);
        }

        [TestMethod]
        public void ShouldFindByBankId()
        {
            var bank = new Bank { Name = "Foo" };

            var entity = new Elevator(bank.Id,
                new IntrepidProducts.ElevatorSystem.Elevators.Elevator(1..10));

            Assert.AreEqual(1, _repo.Create(entity));

            var persistedEntity = _repo.FindByBankId(entity.BankId)
                .FirstOrDefault();

            Assert.IsNotNull(persistedEntity);
            Assert.AreEqual(entity.Id, persistedEntity.Id);
        }
        #endregion
    }
}