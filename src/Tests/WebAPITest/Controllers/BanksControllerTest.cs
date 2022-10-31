using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
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
using IntrepidProducts.WebAPI.Results;

namespace IntrepidProducts.WebApiTest.Controllers
{
    [TestClass]
    public class BanksControllerTest
    {
        #region Get
        [TestMethod]
        public void ShouldGetAllBanks()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllBanksRequest { BuildingId = Guid.NewGuid() };
            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(new FindEntityResponse<BankDTO>(request)
            {
                Entities = new List<BankDTO>
                {
                    new BankDTO
                    {
                        Id = new Guid(),
                        BuildingId = request.BuildingId,
                        Name = "Foo",
                        FloorNbrs = Enumerable.Range(1, 5).ToList(),
                        HighestFloorNbr = 5,
                        LowestFloorNbr = 1,
                        NumberOfElevators = 2

                    },
                    new BankDTO
                    {
                        Id = new Guid(),
                        BuildingId = request.BuildingId,
                        Name = "Bar",
                        FloorNbrs = Enumerable.Range(1, 5).ToList(),
                        HighestFloorNbr = 5,
                        LowestFloorNbr = 1,
                        NumberOfElevators = 2
                    }
                }
            });

            var expectedResponseBlock = responseBlock;

            mockRequestHandlerProcessor.Setup
                (x =>
                    x.Process(It.IsAny<RequestBlock>()))
                .Returns(expectedResponseBlock);

            var mockLinkGenerator = new Mock<LinkGenerator>();

            var controller = new BanksController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                }
            };

            var actionResult = controller.Get(request.BuildingId);

            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var result = okObjectResult.Value as BankCollection;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Banks.Count);
        }

        [TestMethod]
        public void ShouldReturn500WhenGetAllBuildingsFails()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllBanksRequest { BuildingId = Guid.NewGuid() };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new FindEntityResponse<BankDTO>(request)
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

            var controller = new BanksController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                },

                ProblemDetailsFactory = new MockProblemDetailsFactory()

            };

            var actionResult = controller.Get(request.BuildingId);

            var actionResultValue = actionResult.Result as ObjectResult;
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