using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Elevators;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Elevators
{
    [TestClass]
    public class FindAllElevatorsRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBankElevators()
        {
            var buildingId = Guid.NewGuid();

            //Setup
            var bank = new BuildingElevatorBank(buildingId, new Bank(2, 1..5));

            var mockBankRepo = new Mock<IBuildingElevatorBankRepository>();

            mockBankRepo.Setup(x =>
                    x.FindById(bank.Id))
                .Returns(bank);

            var findAllElevatorRequestHandler = new FindAllElevatorsRequestHandler(mockBankRepo.Object);

            var request = new FindAllElevatorsRequest { BankId = bank.Id };
            var findResponse = findAllElevatorRequestHandler.Handle(request);

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var elevatorsReturned = findResponse.Entities;
            Assert.AreEqual(2, elevatorsReturned.Count);
            Assert.AreEqual(bank.Elevators.First().Id, elevatorsReturned[0].Id);
            Assert.AreEqual(bank.Elevators.Last().Id, elevatorsReturned[1].Id);
        }
    }
}