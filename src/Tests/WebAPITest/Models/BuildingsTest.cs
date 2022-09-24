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

            var model = Buildings.MapFrom(dto);

            Assert.AreEqual(2, model.BuildingsInfo.Count);

            for (int i = 0; i < model.BuildingsInfo.Count; i++)
            {
                Assert.AreEqual
                    (dto.Buildings[i].Id, model.BuildingsInfo[i].Id);

                Assert.AreEqual
                    (dto.Buildings[i].Name, model.BuildingsInfo[i].Name);
            }
        }
    }
}