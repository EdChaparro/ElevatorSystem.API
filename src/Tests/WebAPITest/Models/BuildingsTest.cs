using System;
using System.Collections.Generic;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.WebAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.WebApiTest.Models
{
    [TestClass]
    public class BuildingsTest
    {
        [TestMethod]
        public void ShouldMapFromDTO()
        {
            var dto = new BuildingsDTO
            {
                Buildings = new List<BuildingDTO>
                {
                    new BuildingDTO { Id = Guid.NewGuid(), Name = "Foo" },
                    new BuildingDTO { Id = Guid.NewGuid(), Name = "Bar" }
                }
            };

            var model = BuildingsModel.MapFrom(dto);

            Assert.AreEqual(2, model.Buildings.Count);

            for (int i = 0; i < model.Buildings.Count; i++)
            {
                Assert.AreEqual
                    (dto.Buildings[i].Id, model.Buildings[i].Id);

                Assert.AreEqual
                    (dto.Buildings[i].Name, model.Buildings[i].Name);
            }
        }
    }
}