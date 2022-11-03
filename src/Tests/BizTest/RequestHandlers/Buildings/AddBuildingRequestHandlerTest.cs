using IntrepidProducts.Biz.RequestHandlers.Buildings;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IntrepidProducts.BizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class AddBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldAddBuilding()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            mockRepo.Setup(x =>
                    x.Create(It.IsAny<Building>()))
                .Returns(1);

            var rh = new AddBuildingRequestHandler(mockRepo.Object);

            var dto = new BuildingDTO { Name = "Foo" };
            var request = new AddBuildingRequest { Building = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);

            mockRepo.Verify(x =>
                x.Create(It.IsAny<Building>()), Times.Once);
        }

        [TestMethod]
        public void ShouldValidateBuildingDTO()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            mockRepo.Setup(x =>
                    x.Create(It.IsAny<Building>()))
                .Returns(1);

            var rh = new AddBuildingRequestHandler(mockRepo.Object);

            var dto = new BuildingDTO { Name = null };  //Name is required
            var request = new AddBuildingRequest { Building = dto };
            var response = rh.Handle(request);

            Assert.IsFalse(response.IsSuccessful);

            var errorInfo = response.ErrorInfo;
            Assert.IsNotNull(errorInfo);

            Assert.AreEqual("ArgumentException", errorInfo.ErrorId);
            Assert.AreEqual("The Name field is required.", errorInfo.Message);
        }
    }
}