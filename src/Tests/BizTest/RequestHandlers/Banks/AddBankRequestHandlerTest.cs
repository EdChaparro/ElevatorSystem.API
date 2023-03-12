using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Banks
{
    [TestClass]
    public class AddBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldAddBank()
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
    }
}