using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
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
using System.Linq;
using Bank = IntrepidProducts.WebAPI.Models.Bank;

namespace IntrepidProducts.WebApiTest.Controllers
{
    [TestClass]
    public class BanksControllerTest
    {
        #region Get
        [TestMethod]
        public void ShouldGetAllBanksForBuilding()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllBanksRequest { BuildingId = Guid.NewGuid() };
            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var responseBlock = new ResponseBlock(requestBlock);
            responseBlock.Add(new FindEntitiesResponse<BankDTO>(request)
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

            var okObjectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okObjectResult);

            var result = okObjectResult.Value;
            Assert.IsNotNull(result);
            //TODO: Assert anonymous properties are correct, perhaps with JSON Serialization?
        }

        [TestMethod]
        public void ShouldReturn500WhenGetAllBanksFails()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new FindAllBanksRequest { BuildingId = Guid.NewGuid() };

            var requestBlock = new RequestBlock();
            requestBlock.Add(request);

            var response = new FindEntitiesResponse<BankDTO>(request)
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

            var request = new AddBankRequest();
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

            var controller = new BanksController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                }
            };

            var buildingId = Guid.NewGuid();
            var bankModel = new Bank
            {
                Name = "Foo"
            };

            var actionResult = controller.Post(buildingId, bankModel);

            var createdAtActionResult = actionResult as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);

            Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

            var actualResults = createdAtActionResult.Value as Bank;
            Assert.IsNotNull(actualResults);

            var routeValues = createdAtActionResult
                .RouteValues?.Values.ToList();

            Assert.IsNotNull(routeValues);

            Assert.AreEqual("Get", createdAtActionResult.ActionName);
            Assert.AreEqual(2, routeValues.Count);
            Assert.AreEqual(buildingId, routeValues[0]);
            Assert.AreEqual(response.EntityId, routeValues[1]);
        }

        [TestMethod]
        public void ShouldReturn500WhenPostFails()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var request = new AddBankRequest();
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

            var controller = new BanksController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                },

                ProblemDetailsFactory = new MockProblemDetailsFactory()
            };

            var actionResult = controller.Post
                (Guid.NewGuid(), new Bank { Name = "Foo" });

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
            var request = new UpdateBankRequest
            {
                Bank = new BankDTO
                {
                    Id = id,
                    Name = "Foo",
                    BuildingId = Guid.NewGuid(),
                    LowestFloorNbr = 1,
                    HighestFloorNbr = 10,
                    NumberOfElevators = 3
                }
            };

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

            var controller = new BanksController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                }
            };

            var actionResult = controller.Put(id, new Bank { Name = "Foo" });

            var objectResult = actionResult as NoContentResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [TestMethod]
        public void ShouldReportNotFoundOnPut()
        {
            var mockRequestHandlerProcessor = new Mock<IRequestHandlerProcessor>();

            var id = Guid.NewGuid();
            var request = new UpdateBankRequest
            { Bank = new BankDTO { Id = id } };

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

            var controller = new BanksController
                (mockRequestHandlerProcessor.Object, mockLinkGenerator.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() //Needed for HATEOAS URI generation
                }
            };

            var actionResult = controller.Put(id, new Bank() { Name = "Foo" });

            var objectResult = actionResult as NotFoundObjectResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
        #endregion
    }
}