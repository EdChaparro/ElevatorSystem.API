using IntrepidProducts.ElevatorSystem.Shared.DTOs;
using IntrepidProducts.ElevatorSystem.Shared.Requests;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using IntrepidProducts.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IntrepidProducts.WebApiTest.Controllers
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

            var mockLinkGenerator = new Mock<LinkGenerator>();

            var controller = new BuildingsController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                    }
                };

            var actionResult = controller.Get();

            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var model = okObjectResult.Value as BuildingsModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Buildings.Count);
        }
    }
}