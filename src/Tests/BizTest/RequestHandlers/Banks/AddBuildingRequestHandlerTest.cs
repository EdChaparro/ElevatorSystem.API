using System.Collections.Generic;
using IntrepidProducts.Biz.RequestHandlers.Banks;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest.RequestHandlers.Banks
{
    [TestClass]
    public class AddBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldAddBank()
        {
            var buildings = new ElevatorSystem.Buildings();
            var building = new Building();
            buildings.Add(building);

            Assert.AreEqual(0, building.NumberOfBanks);

            var rh = new AddBankRequestHandler(buildings);

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,
                HighestFloorNbr = 10
            };

            var request = new AddBankRequest{ Bank = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(1, building.NumberOfBanks);

            Assert.AreEqual(1, building.LowestFloorNbr);
            Assert.AreEqual(10, building.HighestFloorNbr);

            var bank = building.GetBank("Foo");
            Assert.IsNotNull(bank);
        }

        [TestMethod]
        public void ShouldIgnoreHighAndLowFloorsWhenFloorsNumbersAreItemized()
        {
            var buildings = new ElevatorSystem.Buildings();
            var building = new Building();
            buildings.Add(building);

            Assert.AreEqual(0, building.NumberOfBanks);

            var rh = new AddBankRequestHandler(buildings);

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,     //Will be
                HighestFloorNbr = 10,   //  ignored
                FloorNbrs = new List<int> {1, 2, 3}
            };

            var request = new AddBankRequest { Bank = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(1, building.NumberOfBanks);

            Assert.AreEqual(1, building.LowestFloorNbr);
            Assert.AreEqual(3, building.HighestFloorNbr);

            var bank = building.GetBank("Foo");
            Assert.IsNotNull(bank);
            Assert.AreEqual(3, bank.NumberOfFloors);
        }

        [TestMethod]
        public void ShouldSupportBankWithNonSequentialFloors()
        {
            var buildings = new ElevatorSystem.Buildings();
            var building = new Building();
            buildings.Add(building);

            Assert.AreEqual(0, building.NumberOfBanks);

            var rh = new AddBankRequestHandler(buildings);

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                FloorNbrs = new List<int> { 1, 3, 7 }
            };

            var request = new AddBankRequest { Bank = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(1, building.NumberOfBanks);

            Assert.AreEqual(1, building.LowestFloorNbr);
            Assert.AreEqual(7, building.HighestFloorNbr);

            var bank = building.GetBank("Foo");
            Assert.IsNotNull(bank);
            Assert.AreEqual(3, bank.NumberOfFloors);
        }
    }
}