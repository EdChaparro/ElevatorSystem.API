using IntrepidProducts.Biz.RequestHandlers.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IntrepidProducts.BizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class FindBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildings()
        {
            //Setup
            var buildings = new ElevatorSystem.Buildings();
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
            var buildings = new ElevatorSystem.Buildings();

            var findBuildingRequestHandler = new FindBuildingRequestHandler(buildings);

            var findResponse = findBuildingRequestHandler
                .Handle(new FindBuildingRequest { BuildingId = Guid.NewGuid() });

            Assert.IsTrue(findResponse.IsSuccessful);

            //TODO: Also test for NotFound enum
            Assert.IsNull(findResponse.Building);
        }
    }
}