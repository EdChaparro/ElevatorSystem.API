using IntrepidProducts.ElevatorService;
using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Elevators;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks.Floors;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks.Floors;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Banks.Floors
{
    [TestClass]
    public class FindAllFloorsRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBankFloors()
        {
            //Setup
            var buildingId = Guid.NewGuid();
            var bank1 = new BuildingElevatorBank(buildingId, new Bank(2, 1..5));
            var bank2 = new BuildingElevatorBank(buildingId, new Bank(2, 6..4));

            var request = new FindAllFloorsRequest
            {
                BuildingId = buildingId,
                BankId = bank2.Id
            };

            var mockBankRepo = new Mock<IBuildingElevatorBankRepository>();
            mockBankRepo.Setup(x =>
                    x.FindByBuildingId(request.BuildingId))
                .Returns(new List<BuildingElevatorBank> { bank1, bank2 });

            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();

            var findAllFloorsRequestHandler = new FindAllFloorsRequestHandler
                (mockBankRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findAllFloorsRequestHandler.Handle(request);

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var floorsReturned = findResponse.Entities;
            Assert.AreEqual(4, floorsReturned.Count);

            var bankFloorNbr = bank2.LowestFloorNbr;

            for (var i = 0; i < bank2.FloorNbrs.Count; i++)
            {
                var floorDTO = floorsReturned[i];

                Assert.AreEqual(bankFloorNbr, floorDTO.Number);
                Assert.IsFalse(floorDTO.IsDownCallBackRequested);
                Assert.IsFalse(floorDTO.IsUpCallBackRequested);

                bankFloorNbr++;
            }
        }

        [TestMethod]
        public void ShouldReturnCorrectElevatorCallBackStatus()
        {
            //Setup
            var buildingId = Guid.NewGuid();

            var bankEntity2 = new Bank(2, 6..4);

            var bank1 = new BuildingElevatorBank(buildingId, new Bank(2, 1..5));
            var bank2 = new BuildingElevatorBank(buildingId, bankEntity2);

            Assert.IsTrue(bankEntity2.PressButtonForFloorNumber(7, Direction.Up));
            Assert.IsTrue(bankEntity2.PressButtonForFloorNumber(9, Direction.Down));

            var request = new FindAllFloorsRequest
            {
                BuildingId = buildingId,
                BankId = bank2.Id
            };

            var mockBankService = new Mock<IEntityBackgroundService<Bank>>();
            mockBankService.Setup(x =>
                    x.Entity)
                .Returns(bankEntity2);

            var mockBankRepo = new Mock<IBuildingElevatorBankRepository>();
            mockBankRepo.Setup(x =>
                    x.FindByBuildingId(request.BuildingId))
                .Returns(new List<BuildingElevatorBank> { bank1, bank2 });

            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();
            mockBankServiceRegistry.Setup(x =>
                    x.Get(bank2.Id))
                .Returns(mockBankService.Object);

            var findAllFloorsRequestHandler = new FindAllFloorsRequestHandler
                (mockBankRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findAllFloorsRequestHandler.Handle(request);

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var floorsReturned = findResponse.Entities;
            Assert.AreEqual(4, floorsReturned.Count);

            var bankFloorNbr = bank2.LowestFloorNbr;

            for (var i = 0; i < bank2.FloorNbrs.Count; i++)
            {
                var floorDTO = floorsReturned[i];

                Assert.AreEqual(bankFloorNbr, floorDTO.Number);

                switch (bankFloorNbr)
                {
                    case 7:
                        Assert.IsFalse(floorDTO.IsDownCallBackRequested);
                        Assert.IsTrue(floorDTO.IsUpCallBackRequested);
                        bankFloorNbr++;
                        continue;
                    case 9:
                        Assert.IsTrue(floorDTO.IsDownCallBackRequested);
                        Assert.IsFalse(floorDTO.IsUpCallBackRequested);
                        bankFloorNbr++;
                        continue;
                    default:
                        Assert.IsFalse(floorDTO.IsDownCallBackRequested);
                        Assert.IsFalse(floorDTO.IsUpCallBackRequested);
                        bankFloorNbr++;
                        break;
                }
            }
        }

        [TestMethod]
        public void ShouldHandleNotFound()
        {
            //Setup
            var buildingId = Guid.NewGuid();
            var bank = new BuildingElevatorBank(buildingId, new Bank(2, 6..4));

            var request = new FindAllFloorsRequest
            {
                BuildingId = buildingId,
                BankId = bank.Id
            };

            var mockBankRepo = new Mock<IBuildingElevatorBankRepository>();
            mockBankRepo.Setup(x =>
                    x.FindByBuildingId(request.BuildingId))
                .Returns(new List<BuildingElevatorBank>());

            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();

            var findAllFloorsRequestHandler = new FindAllFloorsRequestHandler
                (mockBankRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findAllFloorsRequestHandler.Handle(request);

            //Assert
            Assert.IsFalse(findResponse.Entities.Any());
        }
    }
}