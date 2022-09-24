using System;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.WebAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.WebApiTest.Models
{
    [TestClass]
    public class BuildingTest
    {
        [TestMethod]
        public void ShouldMapFromDTO()
        {
            var dto = new BuildingDTO { Id = Guid.NewGuid(), Name = "Foo" };

            var model = Building.MapFrom(dto);

            Assert.AreEqual(dto.Id, model.Id);
            Assert.AreEqual(dto.Name, model.Name);
        }
    }
}