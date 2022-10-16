using IntrepidProducts.Biz.RequestHandlers.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest.RequestHandlers.Banks
{
    [TestClass]
    public class FindAllBanksRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildingElevatorBanks()
        {
            //Setup
            var buildings = new ElevatorSystem.Buildings();
            var bank1 = new Bank(2, 1..5);
            var bank2 = new Bank(2, 6..4);

            var building = new ElevatorSystem.Building(bank1, bank2);
            buildings.Add(building);



            var findAllBankRequestHandler = new FindAllBanksRequestHandler(buildings);

            var findResponse = findAllBankRequestHandler
                .Handle(new FindAllBanksRequest { BuildingId = building.Id });

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var banksReturned = findResponse.Banks;
            Assert.AreEqual(2, banksReturned.Count);
            Assert.AreEqual(bank1.Id, banksReturned[0].Id);
            Assert.AreEqual(bank2.Id, banksReturned[1].Id);
        }
    }
}