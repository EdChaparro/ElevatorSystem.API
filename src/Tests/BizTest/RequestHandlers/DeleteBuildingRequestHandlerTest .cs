using IntrepidProducts.Biz.RequestHandlers;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IntrepidProducts.BizTest.RequestHandlers
{
    [TestClass]
    public class DeleteBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldDeleteBuilding()
        {
            var buildings = new Buildings();
            var addBuildingRequestHandler = new AddBuildingRequestHandler(buildings);

            var addRequest = new AddBuildingRequest { Building = new BuildingDTO { Name = "Foo" } };

            var addResponse = addBuildingRequestHandler.Handle(addRequest);
            Assert.IsTrue(addResponse.IsSuccessful);
            var buildingId = addResponse.EntityId;
            Assert.AreEqual(1, buildings.Count);

            var deleteBuildingRequestHandler = new DeleteBuildingRequestHandler(buildings);

            var deleteResponse = deleteBuildingRequestHandler
                .Handle(new DeleteBuildingRequest() { BuildingId = buildingId });

            //Assert
            Assert.IsTrue(deleteResponse.IsSuccessful);
            Assert.AreEqual(OperationResult.Successful, deleteResponse.Result);
            Assert.AreEqual(0, buildings.Count);
        }

        [TestMethod]
        public void ShouldReturnNotFound()
        {
            var buildings = new Buildings();

            var deleteBuildingRequestHandler = new DeleteBuildingRequestHandler(buildings);

            var deleteResponse = deleteBuildingRequestHandler
                .Handle(new DeleteBuildingRequest() { BuildingId = Guid.NewGuid() });

            Assert.IsTrue(deleteResponse.IsSuccessful);
            Assert.AreEqual(OperationResult.NotFound, deleteResponse.Result);
            Assert.AreEqual(0, buildings.Count);
        }
    }
}