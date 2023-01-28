using IntrepidProducts.Biz.RequestHandlers.Elevators;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Elevators;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace IntrepidProducts.BizTest.RequestHandlers.Elevators
{
    [TestClass]
    public class FindElevatorRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnById()
        {
            //Setup
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            const int NUMBER_OF_ELEVATORS = 2;

            var bank = new Bank(NUMBER_OF_ELEVATORS, 1..10) { Name = "Bank A" };

            var elevatorBank = new BuildingElevatorBank(Guid.NewGuid(), bank);

            mockRepo.Setup(x =>
                    x.FindById(bank.Id))
                .Returns(elevatorBank);

            var requestHandler = new FindElevatorRequestHandler(mockRepo.Object);

            var findResponse = requestHandler
                .Handle(new FindElevatorRequest
                {
                    BankId = bank.Id,
                    ElevatorId = bank.Elevators.First().Id
                });

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);
            Assert.IsNotNull(findResponse.Entity);
            Assert.AreEqual(bank.Elevators.First().Id, findResponse.Entity.Id);
            Assert.AreEqual(NUMBER_OF_ELEVATORS, bank.Elevators.Count());
        }

        [TestMethod]
        public void ShouldReturnNullWhenNotFound()
        {
            var mockRepo = new Mock<IRepository<BuildingElevatorBank>>();

            var requestHandler = new FindElevatorRequestHandler(mockRepo.Object);

            var findResponse = requestHandler
                .Handle(new FindElevatorRequest
                {
                    BankId = Guid.NewGuid(),
                    ElevatorId = Guid.NewGuid()
                });

            Assert.IsTrue(findResponse.IsSuccessful);

            Assert.IsNull(findResponse.Entity);
        }
    }
}