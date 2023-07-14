using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Responses;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Controllers.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace IntrepidProducts.WebApiTest.Controllers.Operations
{
    [TestClass]
    public class OperationsControllerTest
    {
        #region Put
        [TestMethod]
        public void ShouldStartElevatorBank()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var buildingId = Guid.NewGuid();
            var bankId = Guid.NewGuid();
            const string ENGINE_PARAMETER = "Start";

            var request = new StartBankRequest
            {
                BuildingId = buildingId,
                BankId = bankId
            };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new BankOperationsResponse(request);

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(response);

            var expectedResponseBlock = responseBlock;

            mockRequestHandlerProcessor.Setup
                (x =>
                    x.Process(It.IsAny<RequestBlock>()))
                .Returns(expectedResponseBlock);

            var mockLinkGenerator = new Mock<LinkGenerator>();

            var controller = new OperationsController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                }
            };

            var actionResult = controller.Put
                (buildingId, bankId, ENGINE_PARAMETER);

            var objectResult = actionResult as OkResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [TestMethod]
        public void ShouldStopElevatorBank()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var buildingId = Guid.NewGuid();
            var bankId = Guid.NewGuid();
            const string ENGINE_PARAMETER = "Stop";

            var request = new StopBankRequest
            {
                BuildingId = buildingId,
                BankId = bankId
            };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new BankOperationsResponse(request);

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(response);

            var expectedResponseBlock = responseBlock;

            mockRequestHandlerProcessor.Setup
                (x =>
                    x.Process(It.IsAny<RequestBlock>()))
                .Returns(expectedResponseBlock);

            var mockLinkGenerator = new Mock<LinkGenerator>();

            var controller = new OperationsController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                    }
                };

            var actionResult = controller.Put
                (buildingId, bankId, ENGINE_PARAMETER);

            var objectResult = actionResult as OkResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        #endregion
    }
}