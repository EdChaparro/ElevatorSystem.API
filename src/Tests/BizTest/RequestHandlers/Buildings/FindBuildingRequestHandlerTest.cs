using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Buildings;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class FindBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnById()
        {
            //Setup
            var mockBuildingRepo = new Mock<IRepository<Building>>();
            var mockBankRepo = new Mock<IBuildingElevatorBankRepository>();
            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();

            var buildingId = Guid.NewGuid();

            mockBuildingRepo.Setup(x =>
                    x.FindById(buildingId))
                .Returns(new Building { Id = buildingId });

            var findBuildingRequestHandler = new FindBuildingRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findBuildingRequestHandler
                .Handle(new FindBuildingRequest { BuildingId = buildingId });

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);
            Assert.IsNotNull(findResponse.Building);
            Assert.AreEqual(buildingId, findResponse.Building.Id);
        }

        [TestMethod]
        public void ShouldReturnNullWhenNotFound()
        {
            var mockBuildingRepo = new Mock<IRepository<Building>>();
            var mockBankRepo = new Mock<IBuildingElevatorBankRepository>();
            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();

            var findBuildingRequestHandler = new FindBuildingRequestHandler
                (mockBuildingRepo.Object, mockBankRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findBuildingRequestHandler
                .Handle(new FindBuildingRequest { BuildingId = Guid.NewGuid() });

            Assert.IsTrue(findResponse.IsSuccessful);

            //TODO: Also test for NotFound enum
            Assert.IsNull(findResponse.Building);
        }
    }
}