using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks.Floors;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IntrepidProducts.WebApiTest.Controllers
{
    [TestClass]
    public class FloorsControllerTest
    {
        #region Get
        [TestMethod]
        public void ShouldGetAllFloorsForBuildingBank()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllFloorsRequest
            {
                BankId = Guid.NewGuid(),
                BuildingId = Guid.NewGuid()
            };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(new FindEntitiesResponse<FloorDTO>(request)
            {
                Entities = new List<FloorDTO>
                {
                    new FloorDTO
                    {
                        Number = 1,
                        Name = null,
                        IsDownCallBackRequested = true,
                        IsUpCallBackRequested = false,
                        IsLocked = false
                    },
                    new FloorDTO
                    {
                        Number = 2,
                        Name = "Sky Lobby",
                        IsDownCallBackRequested = false,
                        IsUpCallBackRequested = true,
                        IsLocked = false
                    }
                }
            });

            var expectedResponseBlock = responseBlock;

            mockRequestHandlerProcessor.Setup
                (x =>
                    x.Process(It.IsAny<RequestBlock>()))
                .Returns(expectedResponseBlock);

            var mockLinkGenerator = new Mock<LinkGenerator>();

            var controller = new FloorsController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                }
            };

            var actionResult = controller.Get(request.BuildingId, request.BankId);

            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var result = okObjectResult.Value;
            Assert.IsNotNull(result);
            //TODO: Assert anonymous properties are correct, perhaps with JSON Serialization?
        }

        [TestMethod]
        public void ShouldReturn500WhenGetAllFloorsFails()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllFloorsRequest
            {
                BankId = Guid.NewGuid(),
                BuildingId = Guid.NewGuid()
            };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new FindEntitiesResponse<FloorDTO>(request)
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

            var controller = new FloorsController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                },

                ProblemDetailsFactory = new MockProblemDetailsFactory()

            };

            var actionResult = controller.Get(request.BuildingId, request.BankId);

            var actionResultValue = actionResult as ObjectResult;
            Assert.IsNotNull(actionResultValue);

            var problemDetails = actionResultValue.Value as ProblemDetails;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, problemDetails?.Status);

            var expectedErrorMsg = $"Error encountered in Request: {request.GetType().Name}, " +
                                   $"Error: {response.ErrorInfo.ErrorId} - {response.ErrorInfo.Message}";
            Assert.AreEqual(expectedErrorMsg, problemDetails?.Detail);
        }

        #endregion
    }
}