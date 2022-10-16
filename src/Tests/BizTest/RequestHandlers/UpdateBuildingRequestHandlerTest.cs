﻿using IntrepidProducts.Biz.RequestHandlers;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.RequestResponse.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

        [TestMethod]
        public void ShouldValidateBuildingDTO()
        {
            var buildings = new Buildings();

            var rh = new UpdateBuildingRequestHandler(buildings);

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