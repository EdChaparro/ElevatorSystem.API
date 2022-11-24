using System;
using IntrepidProducts.Biz.RequestHandlers.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IntrepidProducts.BizTest.RequestHandlers.Banks
{
    [TestClass]
    public class FindBankRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnById()
        {
            //Setup
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            var bank = new Bank(2, 1..10) { Name = "Bank A" };

            var elevatorBank = new BuildingElevatorBank(Guid.NewGuid(), bank);

            mockRepo.Setup(x =>
                    x.FindById(bank.Id))
                .Returns(elevatorBank);

            var findBankRequestHandler = new FindBankRequestHandler(mockRepo.Object);

            var findResponse = findBankRequestHandler
                .Handle(new FindBankRequest { BankId = bank.Id });

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);
            Assert.IsNotNull(findResponse.Bank);
            Assert.AreEqual(bank.Id, findResponse.Bank.Id);
        }

        [TestMethod]
        public void ShouldReturnNullWhenNotFound()
        {
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            var findBankRequestHandler = new FindBankRequestHandler(mockRepo.Object);

            var findResponse = findBankRequestHandler
                .Handle(new FindBankRequest { BankId = Guid.NewGuid() });

            Assert.IsTrue(findResponse.IsSuccessful);

            //TODO: Also test for NotFound enum
            Assert.IsNull(findResponse.Bank);
        }
    }
}