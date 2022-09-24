using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System;
using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Requests;
using Microsoft.AspNetCore.Mvc;

namespace IntrepidProducts.WebApiTest
{
    [TestClass]
    public class BuildingsControllerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildings()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllBuildingsRequest();
            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(new FindAllBuildingsResponse(request)
            {
                Buildings = new List<BuildingDTO>
                {
                    new BuildingDTO
                    {
                        Id = new Guid(),
                        Name = "Foo"
                    },
                    new BuildingDTO
                    {
                        Id = new Guid(),
                        Name = "Bar"
                    }
                }
            });

            var expectedResponseBlock = responseBlock;

            mockRequestHandlerProcessor.Setup
                    (x =>
                        x.Process(It.IsAny<RequestBlock>()))
                .Returns(expectedResponseBlock);

            var controller = new BuildingsController(mockRequestHandlerProcessor.Object);

            var actionResult = controller.Get();

            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var buildingsDTO = okObjectResult.Value as BuildingsDTO;
            Assert.IsNotNull(buildingsDTO);
            Assert.AreEqual(2, buildingsDTO.Buildings.Count);
        }
    }
}