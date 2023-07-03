using IntrepidProducts.ElevatorService;
using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Banks
{
    [TestClass]
    public class StartBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldRegisterBankServiceWhenUnregistered()
        {
            //Setup
            var mockRepo = new Mock<IBuildingElevatorBankRepository>();
            var mockBankRegistry = new Mock<IBankServiceRegistry>();
            var mockBackgroundService = new Mock<IBackgroundService>();

            var businessId = Guid.NewGuid();

            var bank = new Bank(2, 1..10) { Name = "Bank A" };

            var elevatorBank = new BuildingElevatorBank(Guid.NewGuid(), bank);

            mockRepo.Setup(x =>
                    x.FindByBusinessId(businessId))
                        .Returns(new List<BuildingElevatorBank> { elevatorBank });

            mockBankRegistry.Setup(x =>
                    x.IsRegistered(It.IsAny<Bank>()))
                        .Returns(false);

            mockBankRegistry.Setup(x =>
                    x.Get(It.IsAny<Bank>()))
                        .Returns(mockBackgroundService.Object);

            var startBankRequestHandler = new StartBankRequestHandler
                (mockRepo.Object, mockBankRegistry.Object);

            var operationResponse = startBankRequestHandler
                .Handle(new StartBankRequest { BankId = bank.Id, BusinessId = businessId });

            //Assert
            Assert.IsTrue(operationResponse.IsSuccessful);

            mockBankRegistry.Verify
            (x
                => x.Register(It.IsAny<Bank>()), Times.Once());
        }

        [TestMethod]
        public void ShouldNotRegisterBankServiceWhenAlreadyRegistered()
        {
            //Setup
            var mockRepo = new Mock<IBuildingElevatorBankRepository>();
            var mockBankRegistry = new Mock<IBankServiceRegistry>();
            var mockBackgroundService = new Mock<IBackgroundService>();

            var businessId = Guid.NewGuid();

            var bank = new Bank(2, 1..10) { Name = "Bank A" };

            var elevatorBank = new BuildingElevatorBank(Guid.NewGuid(), bank);

            mockRepo.Setup(x =>
                    x.FindByBusinessId(businessId))
                        .Returns(new List<BuildingElevatorBank> { elevatorBank });

            mockBankRegistry.Setup(x =>
                    x.IsRegistered(It.IsAny<Bank>()))
                        .Returns(true);

            mockBankRegistry.Setup(x =>
                    x.Get(It.IsAny<Bank>()))
                        .Returns(mockBackgroundService.Object);

            var startBankRequestHandler = new StartBankRequestHandler
                (mockRepo.Object, mockBankRegistry.Object);

            var operationResponse = startBankRequestHandler
                .Handle(new StartBankRequest { BankId = bank.Id, BusinessId = businessId });

            //Assert
            Assert.IsTrue(operationResponse.IsSuccessful);

            mockBankRegistry.Verify
            (x
                => x.Register(It.IsAny<Bank>()), Times.Never);
        }

        [TestMethod]
        public void ShouldFailWhenBankNotFound()
        {
            //Setup
            var mockRepo = new Mock<IBuildingElevatorBankRepository>();
            var mockBankRegistry = new Mock<IBankServiceRegistry>();

            var businessId = Guid.NewGuid();

            var bank = new Bank(2, 1..10) { Name = "Bank A" };

            mockRepo.Setup(x =>
                    x.FindByBusinessId(businessId))
                .Returns(new List<BuildingElevatorBank>());

            var startBankRequestHandler = new StartBankRequestHandler
                (mockRepo.Object, mockBankRegistry.Object);

            var operationResponse = startBankRequestHandler
                .Handle(new StartBankRequest { BankId = bank.Id });

            //Assert
            Assert.IsFalse(operationResponse.IsSuccessful);
        }
    }
}