using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.WebAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

        [TestMethod]
        public void ShouldRequireName()
        {
            var dto = new BuildingDTO { Id = Guid.NewGuid(), Name = null };

            var validationResults = ModelValidator.Validate(dto);
            Assert.IsTrue(validationResults.Any());

            var validationError = validationResults.First();
            Assert.AreEqual
                ("The Name field is required.", validationError.ErrorMessage);
        }
    }
}