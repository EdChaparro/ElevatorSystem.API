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
using System.Linq;
using IntrepidProducts.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IntrepidProducts.WebApiTest.Controllers
{
    [TestClass]
    public class BuildingsControllerTest
    {
        #region Get
        [TestMethod]
        public void ShouldGetAllBuildings()
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
        #endregion

        [TestMethod]
        public void ShouldReportSuccessfulPost()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new AddBuildingRequest();
            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new EntityAddedResponse(request)
            {
                EntityId = new Guid()
            };

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(response);

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

            var actionResult = controller.Post(new BuildingName { Name = "Foo" });

            var createdAtActionResult = actionResult as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);

            Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

            var actualResults = createdAtActionResult.Value as BuildingName;
            Assert.IsNotNull(actualResults);

            var routeValues = createdAtActionResult.RouteValues;

            Assert.AreEqual("Get", createdAtActionResult.ActionName);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual("id", routeValues.Keys.First());
            Assert.AreEqual(response.EntityId, routeValues.Values.First());
        }
    }
}