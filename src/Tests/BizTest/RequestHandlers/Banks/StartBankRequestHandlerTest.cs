using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Banks;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Banks
{
    [TestClass]
    public class StartBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldStartBank()
        {
            //Setup
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();
            var mockBankRegistry = new Mock<IBankServiceRegistry>();

            var bank = new Bank(2, 1..10) { Name = "Bank A" };

            var elevatorBank = new BuildingElevatorBank(Guid.NewGuid(), bank);

            mockRepo.Setup(x =>
                    x.FindById(bank.Id))
                .Returns(elevatorBank);

            var startBankRequestHandler = new StartBankRequestHandler
                (mockRepo.Object, mockBankRegistry.Object);

            var operationResponse = startBankRequestHandler
                .Handle(new StartBankRequest { BankId = bank.Id });

            //Assert
            Assert.IsTrue(operationResponse.IsSuccessful);
        }
    }
}