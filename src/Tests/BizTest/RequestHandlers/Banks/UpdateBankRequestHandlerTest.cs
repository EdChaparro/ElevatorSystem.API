using System;
using IntrepidProducts.Biz.RequestHandlers.Banks;
using IntrepidProducts.ElevatorSystem.Shared.DTOs.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IntrepidProducts.BizTest.RequestHandlers.Banks
{
    [TestClass]
    public class UpdateBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldUpdate()
        {
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            var bank = new BuildingElevatorBank();

            mockRepo.Setup(x =>
                    x.FindById(bank.Id))
                .Returns(bank);

            mockRepo.Setup(x =>
                    x.Update(bank))
                .Returns(1);

            var dto = new BankDTO
            {
                Id = Guid.NewGuid(),
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,
                HighestFloorNbr = 10
            };

            var rh = new UpdateBankRequestHandler(mockRepo.Object);
            dto.Id = bank.Id;
            dto.Name = "bar";

            var updateRequest = new UpdateBankRequest { Bank = dto };
            var updateResponse = rh.Handle(updateRequest);

            Assert.IsTrue(updateResponse.IsSuccessful);
            Assert.IsTrue(updateResponse.Result == OperationResult.Successful);

            Assert.AreEqual(dto.Name, bank.Name);
            Assert.AreEqual(dto.NumberOfElevators, bank.NumberOfElevators);
            Assert.AreEqual(dto.LowestFloorNbr, bank.LowestFloorNbr);
            Assert.AreEqual(dto.HighestFloorNbr, bank.HighestFloorNbr);
        }

        [TestMethod]
        public void ShouldResponseWithNotFound()
        {
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            var dto = new BankDTO
            {
                Id = Guid.NewGuid(),
                Name = "Foo",
                NumberOfElevators = 2,
                LowestFloorNbr = 1,
                HighestFloorNbr = 10
            };

            var updateRh = new UpdateBankRequestHandler(mockRepo.Object);
            var updateRequest = new UpdateBankRequest { Bank = dto };
            var updateResponse = updateRh.Handle(updateRequest);
            Assert.IsTrue(updateResponse.Result == OperationResult.NotFound);
        }

        [TestMethod]
        public void ShouldValidateDTO()
        {
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            var rh = new UpdateBankRequestHandler(mockRepo.Object);

            var dto = new BankDTO();    //No properties set, will fail validation
            var request = new UpdateBankRequest { Bank = dto };
            var response = rh.Handle(request);

            Assert.IsFalse(response.IsSuccessful);

            var errorInfo = response.ErrorInfo;
            Assert.IsNotNull(errorInfo);

            Assert.AreEqual("ArgumentException", errorInfo.ErrorId);
            Assert.AreEqual("The field NumberOfElevators must be between 2 and 999.", errorInfo.Message);
        }
    }
}