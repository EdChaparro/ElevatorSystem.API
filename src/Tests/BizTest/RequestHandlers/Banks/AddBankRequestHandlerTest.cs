using IntrepidProducts.Biz.RequestHandlers.Banks;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using IntrepidProducts.RequestResponse.Responses;
using System;
using IntrepidProducts.Shared.ElevatorSystem.Entities;

namespace IntrepidProducts.BizTest.RequestHandlers.Banks
{
    [TestClass]
    public class AddBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldAddBankWithFloorRange()
        {
            var building = new Building();

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,
                HighestFloorNbr = 10
            };

            var mockBuildingRepo = new Mock<IRepository<Building>>();

            mockBuildingRepo.Setup(x =>
                    x.FindById(dto.BuildingId))
                .Returns(building);

            var mockBankRepo = new Mock<IRepository<BuildingElevatorBank>>();

            mockBankRepo.Setup(x =>
                    x.Create(It.IsAny<BuildingElevatorBank>()))
                .Returns(1);

            var rh = new AddBankRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object);

            var request = new AddBankRequest { Bank = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(1, building.NumberOfBanks);

            Assert.AreEqual(1, building.LowestFloorNbr);
            Assert.AreEqual(10, building.HighestFloorNbr);

            var bank = building.GetBank("Foo");
            Assert.IsNotNull(bank);
        }

        [TestMethod]
        public void ShouldAddBankWithItemizedFloorNumbers()
        {
            var building = new Building();

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,     //This will be
                HighestFloorNbr = 10,   //  ignored due to explicit FloorNbrs list
                FloorNbrs = new List<int> { 12, 15, 19 }
            };

            var mockBuildingRepo = new Mock<IRepository<Building>>();

            mockBuildingRepo.Setup(x =>
                    x.FindById(dto.BuildingId))
                .Returns(building);

            var mockBankRepo = new Mock<IRepository<BuildingElevatorBank>>();

            mockBankRepo.Setup(x =>
                    x.Create(It.IsAny<BuildingElevatorBank>()))
                .Returns(1);

            var rh = new AddBankRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object);

            var request = new AddBankRequest { Bank = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(1, building.NumberOfBanks);

            Assert.AreEqual(12, building.LowestFloorNbr);
            Assert.AreEqual(19, building.HighestFloorNbr);

            var bank = building.GetBank("Foo");
            Assert.IsNotNull(bank);
        }

        [TestMethod]
        public void ShouldRespondWithNotFound()
        {
            var dto = new BankDTO
            {
                BuildingId = Guid.NewGuid(),
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,
                HighestFloorNbr = 10
            };

            var mockBuildingRepo = new Mock<IRepository<Building>>();

            mockBuildingRepo.Setup(x =>
                    x.FindById(dto.BuildingId))
                .Returns<Building>(null);

            var mockBankRepo = new Mock<IRepository<BuildingElevatorBank>>();

            mockBankRepo.Setup(x =>
                    x.Create(It.IsAny<BuildingElevatorBank>()))
                .Returns(1);

            var rh = new AddBankRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object);

            var request = new AddBankRequest { Bank = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.Result == OperationResult.OperationalError);

            var errorInfo = response.ErrorInfo;
            Assert.IsNotNull(errorInfo);
            Assert.AreEqual("Building Id not found", errorInfo.Message);
        }

        [TestMethod]
        public void ShouldIgnoreHighAndLowFloorsWhenFloorsNumbersAreItemized()
        {
            var building = new Building();

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,     //Will be
                HighestFloorNbr = 10,   //  ignored
                FloorNbrs = new List<int> { 1, 2, 3 }   //Itemized list provided
            };

            var mockBuildingRepo = new Mock<IRepository<Building>>();

            mockBuildingRepo.Setup(x =>
                    x.FindById(dto.BuildingId))
                .Returns(building);

            var mockBankRepo = new Mock<IRepository<BuildingElevatorBank>>();

            mockBankRepo.Setup(x =>
                    x.Create(It.IsAny<BuildingElevatorBank>()))
                .Returns(1);

            var rh = new AddBankRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object);

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
            var building = new Building();

            var dto = new BankDTO
            {
                BuildingId = building.Id,
                Name = "Foo",
                NumberOfElevators = 2,
                FloorNbrs = new List<int> { 1, 3, 7 }
            };

            var mockBuildingRepo = new Mock<IRepository<Building>>();

            mockBuildingRepo.Setup(x =>
                    x.FindById(dto.BuildingId))
                .Returns(building);

            var mockBankRepo = new Mock<IRepository<BuildingElevatorBank>>();

            mockBankRepo.Setup(x =>
                    x.Create(It.IsAny<BuildingElevatorBank>()))
                .Returns(1);

            var rh = new AddBankRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object);

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