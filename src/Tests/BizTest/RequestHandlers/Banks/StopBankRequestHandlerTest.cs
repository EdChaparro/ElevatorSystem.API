using System;
using System.Collections.Generic;
using IntrepidProducts.ElevatorService;
using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Banks;

[TestClass]
public class StopBankRequestHandlerTest
{
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