using IntrepidProducts.Biz.RequestHandlers.Buildings;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IntrepidProducts.BizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class FindBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnById()
        {
            //Setup
            var mockRepo = new Mock<IRepository<Building>>();

            var buildingId = Guid.NewGuid();

            mockRepo.Setup(x =>
                    x.FindById(buildingId))
                .Returns(new Building { Id = buildingId });

            var findBuildingRequestHandler = new FindBuildingRequestHandler(mockRepo.Object);

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
            var mockRepo = new Mock<IRepository<Building>>();

            var findBuildingRequestHandler = new FindBuildingRequestHandler(mockRepo.Object);

            var findResponse = findBuildingRequestHandler
                .Handle(new FindBuildingRequest { BuildingId = Guid.NewGuid() });

            Assert.IsTrue(findResponse.IsSuccessful);

            //TODO: Also test for NotFound enum
            Assert.IsNull(findResponse.Building);
        }
    }
}