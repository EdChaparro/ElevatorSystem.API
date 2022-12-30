using IntrepidProducts.Biz.RequestHandlers.Buildings;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IntrepidProducts.BizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class UpdateBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldUpdate()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            var building = new Building();

            mockRepo.Setup(x =>
                    x.FindById(building.Id))
                .Returns(building);

            mockRepo.Setup(x =>
                    x.Update(building))
                .Returns(1);

            var dto = new BuildingDTO { Name = "Foo" };

            var rh = new UpdateBuildingRequestHandler(mockRepo.Object);
            dto.Id = building.Id;
            dto.Name = "bar";

            var updateRequest = new UpdateBuildingRequest { Building = dto };
            var updateResponse = rh.Handle(updateRequest);

            Assert.IsTrue(updateResponse.IsSuccessful);
            Assert.IsTrue(updateResponse.Result == OperationResult.Successful);

            Assert.AreEqual(dto.Name, building.Name);
        }

        [TestMethod]
        public void ShouldResponseWithNotFound()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            var dto = new BuildingDTO { Id = Guid.NewGuid(), Name = "Foo" };

            var updateRh = new UpdateBuildingRequestHandler(mockRepo.Object);
            var updateRequest = new UpdateBuildingRequest { Building = dto };
            var updateResponse = updateRh.Handle(updateRequest);
            Assert.IsTrue(updateResponse.Result == OperationResult.NotFound);
        }

        [TestMethod]
        public void ShouldValidateDTO()
        {
            var mockRepo = new Mock<IRepository<Building>>();

            var rh = new UpdateBuildingRequestHandler(mockRepo.Object);

            var dto = new BuildingDTO { Name = null };  //Name is required
            var request = new UpdateBuildingRequest { Building = dto };
            var response = rh.Handle(request);

            Assert.IsFalse(response.IsSuccessful);

            var errorInfo = response.ErrorInfo;
            Assert.IsNotNull(errorInfo);

            Assert.AreEqual("ArgumentException", errorInfo.ErrorId);
            Assert.AreEqual("The Name field is required.", errorInfo.Message);
        }
    }
}