using IntrepidProducts.ElevatorSystem.Shared.DTOs.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Controllers;
using IntrepidProducts.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

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
            responseBlock.Add(new FindEntityResponse<BuildingDTO>(request)
            {
                Entities = new List<BuildingDTO>
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

            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var model = okObjectResult.Value;
            Assert.IsNotNull(model);
            //TODO: Assert anonymous properties are correct, perhaps with JSON Serialization?
        }

        [TestMethod]
        public void ShouldReturn500WhenGetAllBuildingsFails()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllBuildingsRequest();

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new FindEntityResponse<BuildingDTO>(request)
            {
                ErrorInfo = new ErrorInfo("error", "something went wrong")
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
                    HttpContext = new DefaultHttpContext()
                },

                ProblemDetailsFactory = new MockProblemDetailsFactory()

            };

            var actionResult = controller.Get();

            var actionResultValue = actionResult as ObjectResult;
            Assert.IsNotNull(actionResultValue);

            var problemDetails = actionResultValue.Value as ProblemDetails;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, problemDetails?.Status);

            var expectedErrorMsg = $"Error encountered in Request: {request.GetType().Name}, " +
                                     $"Error: {response.ErrorInfo.ErrorId} - {response.ErrorInfo.Message}";
            Assert.AreEqual(expectedErrorMsg, problemDetails?.Detail);
        }

        #endregion

        #region Post
        [TestMethod]
        public void ShouldReportSuccessfulPost()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new AddBuildingRequest();
            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new EntityOperationResponse(request)
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

            var actionResult = controller.Post(new Building { Name = "Foo" });

            var createdAtActionResult = actionResult as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);

            Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

            var actualResults = createdAtActionResult.Value as Building;
            Assert.IsNotNull(actualResults);

            var routeValues = createdAtActionResult.RouteValues;

            Assert.AreEqual("Get", createdAtActionResult.ActionName);
            Assert.AreEqual(1, routeValues.Count);
            Assert.AreEqual("id", routeValues.Keys.First());
            Assert.AreEqual(response.EntityId, routeValues.Values.First());
        }

        [TestMethod]
        public void ShouldReturn500WhenPostFails()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new AddBuildingRequest();
            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new EntityOperationResponse(request)
            {
                ErrorInfo = new ErrorInfo("error", "something went wrong")
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
                },

                ProblemDetailsFactory = new MockProblemDetailsFactory()
            };

            var actionResult = controller.Post(new Building { Name = "Foo" });

            var objectResult = actionResult as ObjectResult;
            Assert.IsNotNull(objectResult);

            var problemDetails = objectResult.Value as ProblemDetails;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, problemDetails?.Status);
        }
        #endregion

        #region Put
        [TestMethod]
        public void ShouldReportSuccessfulPut()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var id = Guid.NewGuid();
            var request = new UpdateBuildingRequest
            { Building = new BuildingDTO { Id = id } };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new OperationResponse(request) { Result = OperationResult.Successful };

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

            var actionResult = controller.Put(id, new Building() { Name = "Foo" });

            var objectResult = actionResult as NoContentResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [TestMethod]
        public void ShouldReportNotFoundOnPut()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var id = Guid.NewGuid();
            var request = new UpdateBuildingRequest
            { Building = new BuildingDTO { Id = id } };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new OperationResponse(request) { Result = OperationResult.NotFound };

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

            var actionResult = controller.Put(id, new Building() { Name = "Foo" });

            var objectResult = actionResult as NotFoundObjectResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        #endregion

        #region Delete
        [TestMethod]
        public void ShouldReportSuccessfulDelete()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var id = Guid.NewGuid();
            var request = new DeleteBuildingRequest { BuildingId = id };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new OperationResponse(request) { Result = OperationResult.Successful };

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

            var actionResult = controller.Delete(id);

            var objectResult = actionResult as NoContentResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [TestMethod]
        public void ShouldReportNotFoundOnDelete()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var id = Guid.NewGuid();
            var request = new DeleteBuildingRequest { BuildingId = id };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new OperationResponse(request) { Result = OperationResult.NotFound };

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

            var actionResult = controller.Delete(id);

            var objectResult = actionResult as NotFoundObjectResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
        #endregion
    }
}