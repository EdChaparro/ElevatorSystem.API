using IntrepidProducts.Biz.RequestHandlers.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class FindAllBuildingsRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildings()
        {
            //Setup
            var buildings = new ElevatorSystem.Buildings();
            var addBuildingRequestHandler = new AddBuildingRequestHandler(buildings);

            var request1 = new AddBuildingRequest { Building = new BuildingDTO { Name = "Foo" } };
            var request2 = new AddBuildingRequest { Building = new BuildingDTO { Name = "Bar" } };

            var addResponse = addBuildingRequestHandler.Handle(request1);
            Assert.IsTrue(addResponse.IsSuccessful);

            addResponse = addBuildingRequestHandler.Handle(request2);
            Assert.IsTrue(addResponse.IsSuccessful);

            var findBuildingsRequestHandler = new FindAllBuildingsRequestHandler(buildings);

            var findResponse = findBuildingsRequestHandler
                .Handle(new FindAllBuildingsRequest());

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var buildingsReturned = findResponse.Buildings;
            Assert.AreEqual(2, buildingsReturned.Count);
            Assert.AreEqual(buildings[0].Id, buildingsReturned[0].Id);
            Assert.AreEqual(buildings[1].Id, buildingsReturned[1].Id);
        }
    }
}