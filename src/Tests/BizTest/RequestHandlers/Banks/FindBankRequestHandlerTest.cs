﻿using IntrepidProducts.ElevatorService.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Banks
{
    [TestClass]
    public class FindBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnById()
        {
            //Setup
            var mockRepo = new Mock<IBuildingElevatorBankRepository>();

            var buildingId = Guid.NewGuid();

            const int NUMBER_OF_ELEVATORS = 2;

            var bank = new Bank(NUMBER_OF_ELEVATORS, 1..10) { Name = "Bank A" };

            var elevatorBank = new BuildingElevatorBank(buildingId, bank);

            mockRepo.Setup(x =>
                    x.FindByBuildingId(buildingId))
                .Returns(new List<BuildingElevatorBank> { elevatorBank });

            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();

            var findBankRequestHandler = new FindBankRequestHandler
                (mockRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findBankRequestHandler
                .Handle(new FindBankRequest { BuildingId = buildingId, BankId = bank.Id });

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);
            Assert.IsNotNull(findResponse.Entity);
            Assert.AreEqual(bank.Id, findResponse.Entity.Id);
            Assert.AreEqual(NUMBER_OF_ELEVATORS, bank.Elevators.Count());
        }

        [TestMethod]
        public void ShouldReturnNullWhenNotFound()
        {
            var mockRepo = new Mock<IBuildingElevatorBankRepository>();
            var mockBankServiceRegistry = new Mock<IBankServiceRegistry>();

            var findBankRequestHandler = new FindBankRequestHandler
                (mockRepo.Object, mockBankServiceRegistry.Object);

            var findResponse = findBankRequestHandler
                .Handle(new FindBankRequest { BankId = Guid.NewGuid() });

            Assert.IsTrue(findResponse.IsSuccessful);

            //TODO: Also test for NotFound enum
            Assert.IsNull(findResponse.Entity);
        }
    }
}