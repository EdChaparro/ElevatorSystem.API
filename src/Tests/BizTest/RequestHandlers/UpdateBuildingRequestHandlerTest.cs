using System;
using System.Linq;
using IntrepidProducts.Biz.RequestHandlers;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest.RequestHandlers
{
    [TestClass]
    public class UpdateBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldUpdateBuilding()
        {
            var buildings = new Buildings();

            var addRh = new AddBuildingRequestHandler(buildings);
            var dto = new BuildingDTO { Name = "Foo" };
            var addRequest = new AddBuildingRequest { Building = dto };
            var addResponse = addRh.Handle(addRequest);
            Assert.IsTrue(addResponse.IsSuccessful);

            var updateRh = new UpdateBuildingRequestHandler(buildings);
            dto.Id = addResponse.EntityId;
            dto.Name = "bar";
            var updateRequest = new UpdateBuildingRequest { Building = dto };
            var updateResponse = updateRh.Handle(updateRequest);
            Assert.IsTrue(updateResponse.IsSuccessful);
            Assert.IsTrue(updateResponse.Result == OperationResult.Successful);

            var updatedBuilding = buildings.FirstOrDefault(x => x.Id == dto.Id);
            Assert.IsNotNull(updatedBuilding);
            Assert.AreEqual(dto.Name, updatedBuilding.Name);
        }

        [TestMethod]
        public void ShouldResponseWithNotFound()
        {
            var buildings = new Buildings();

            var dto = new BuildingDTO { Id = Guid.NewGuid(), Name = "Foo" };

            var updateRh = new UpdateBuildingRequestHandler(buildings);
            var updateRequest = new UpdateBuildingRequest { Building = dto };
            var updateResponse = updateRh.Handle(updateRequest);
            Assert.IsTrue(updateResponse.Result == OperationResult.NotFound);
        }
    }
}