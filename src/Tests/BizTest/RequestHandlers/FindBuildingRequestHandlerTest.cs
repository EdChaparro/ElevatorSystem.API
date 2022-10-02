using IntrepidProducts.Biz.RequestHandlers;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IntrepidProducts.BizTest.RequestHandlers
{
    [TestClass]
    public class FindBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildings()
        {
            //Setup
            var buildings = new Buildings();
            var addBuildingRequestHandler = new AddBuildingRequestHandler(buildings);

            var addRequest = new AddBuildingRequest { Building = new BuildingDTO { Name = "Foo" } };

            var addResponse = addBuildingRequestHandler.Handle(addRequest);
            Assert.IsTrue(addResponse.IsSuccessful);
            var buildingId = addResponse.EntityId;

            var findBuildingRequestHandler = new FindBuildingRequestHandler(buildings);

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
            var buildings = new Buildings();

            var findBuildingRequestHandler = new FindBuildingRequestHandler(buildings);

            var findResponse = findBuildingRequestHandler
                .Handle(new FindBuildingRequest { BuildingId = Guid.NewGuid() });

            Assert.IsTrue(findResponse.IsSuccessful);

            //TODO: Also test for NotFound enum
            Assert.IsNull(findResponse.Building);
        }
    }
}