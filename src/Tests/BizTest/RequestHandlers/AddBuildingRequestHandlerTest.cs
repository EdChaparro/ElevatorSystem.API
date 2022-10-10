using System;
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

        [TestMethod]
        public void ShouldValidateBuildingDTO()
        {
            var buildings = new Buildings();

            var rh = new AddBuildingRequestHandler(buildings);

            var dto = new BuildingDTO { Name = null };  //Name is required
            var request = new AddBuildingRequest { Building = dto };
            var response = rh.Handle(request);

            Assert.IsFalse(response.IsSuccessful);

            var errorInfo = response.ErrorInfo;
            Assert.IsNotNull(errorInfo);

            Assert.AreEqual("ArgumentException", errorInfo.ErrorId);
            Assert.AreEqual("Building name is required", errorInfo.Message);
        }
    }
}