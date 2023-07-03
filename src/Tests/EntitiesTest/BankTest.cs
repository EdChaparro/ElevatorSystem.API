using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.Shared.EntitiesTest
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void ShouldConvertToEntityObject()
        {
            var originalBankBizObj = new Bank(2, 1..10);
            var originalBuildingBizObj = new Building(originalBankBizObj);

            var entityBankObj = new BuildingElevatorBank
                (originalBuildingBizObj.Id, originalBankBizObj);

            Assert.AreEqual(originalBuildingBizObj.Id, entityBankObj.BuildingId);
            Assert.AreEqual(originalBankBizObj.NumberOfElevators,
                entityBankObj.NumberOfElevators);

            foreach (var elevator in entityBankObj.Elevators)
            {
                var originalElevatorObj = originalBankBizObj.Elevators
                    .Where(x => x.Id == elevator.Id)
                    .Select(x => x)
                    .FirstOrDefault();

                Assert.IsNotNull(originalElevatorObj);
            }
        }

        [TestMethod]
        public void ShouldConvertToBizObject()
        {
            var originalBankBizObj = new Bank(2, 1..10)
            {
                Name = "Elevator A"
            };

            var buildingId = Guid.NewGuid();

            var entityBankObj = new BuildingElevatorBank(buildingId, originalBankBizObj);

            var rehydratedBankBizObj = entityBankObj.ToBusinessObject();

            Assert.IsNotNull(rehydratedBankBizObj);

            Assert.AreEqual(originalBankBizObj.Name, rehydratedBankBizObj.Name);

            Assert.AreEqual(originalBankBizObj.NumberOfElevators,
                rehydratedBankBizObj.NumberOfElevators);

            foreach (var elevator in rehydratedBankBizObj.Elevators)
            {
                var originalElevatorObj = originalBankBizObj.Elevators
                    .Where(x => x.Id == elevator.Id)
                    .Select(x => x)
                    .FirstOrDefault();

                Assert.IsNotNull(originalElevatorObj);
            }
        }
    }
}