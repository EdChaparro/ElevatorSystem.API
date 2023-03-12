using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Buildings;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class DeleteBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldDeleteBuilding()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            var building = new Building();

            mockRepo.Setup(x =>
                    x.FindById(building.Id))
                .Returns(building);

            mockRepo.Setup(x =>
                    x.Delete(building))
                .Returns(1);

            var deleteBuildingRequestHandler = new DeleteBuildingRequestHandler(mockRepo.Object);

            var deleteResponse = deleteBuildingRequestHandler
                .Handle(new DeleteBuildingRequest() { BuildingId = building.Id });

            //Assert
            Assert.IsTrue(deleteResponse.IsSuccessful);
            Assert.AreEqual(OperationResult.Successful, deleteResponse.Result);

            mockRepo.Verify(x => x.Delete(building), Times.Once);
        }

        [TestMethod]
        public void ShouldReturnNotFound()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            var deleteBuildingRequestHandler = new DeleteBuildingRequestHandler(mockRepo.Object);

            var deleteResponse = deleteBuildingRequestHandler
                .Handle(new DeleteBuildingRequest() { BuildingId = Guid.NewGuid() });

            Assert.IsTrue(deleteResponse.IsSuccessful);
            Assert.AreEqual(OperationResult.NotFound, deleteResponse.Result);
        }
    }
}