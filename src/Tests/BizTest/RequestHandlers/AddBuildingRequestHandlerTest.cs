using IntrepidProducts.Biz.RequestHandlers;
using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest.RequestHandlers
{
    [TestClass]
    public class AddBuildingRequestHandlerTest
    {
        [TestMethod]
        public void ShouldAddBuilding()
        {
            var buildings = new Buildings();
            Assert.AreEqual(0, buildings.Count);

            var rh = new AddBuildingRequestHandler(buildings);

            var dto = new BuildingDTO { Name = "Foo" };
            var request = new AddBuildingRequest { Building = dto };
            var response = rh.Handle(request);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(1, buildings.Count);
            Assert.AreEqual(dto.Name, buildings[0].Name);
        }
    }
}
