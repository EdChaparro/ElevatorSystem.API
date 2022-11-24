using IntrepidProducts.Biz.RequestHandlers.Banks;
using IntrepidProducts.ElevatorSystem.Banks;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Banks;
using IntrepidProducts.Repo;
using IntrepidProducts.Shared.ElevatorSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IntrepidProducts.BizTest.RequestHandlers.Banks
{
    [TestClass]
    public class FindAllBanksRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildingElevatorBanks()
        {
            var buildingId = Guid.NewGuid();
            var request = new FindAllBanksRequest { BuildingId = buildingId };

            //Setup
            var bank1 = new BuildingElevatorBank(buildingId, new Bank(2, 1..5));
            var bank2 = new BuildingElevatorBank(buildingId, new Bank(2, 6..4));

            var mockBankRepo = new Mock<IRepository<BuildingElevatorBank>>();
            var mockFindAllBankRepo = mockBankRepo.As<IFindByBusinessId>();

            mockFindAllBankRepo.Setup(x =>
                    x.FindByBusinessId(request.BuildingId))
                .Returns(new List<BuildingElevatorBank> { bank1, bank2 });

            var findAllBankRequestHandler = new FindAllBanksRequestHandler(mockBankRepo.Object);

            var findResponse = findAllBankRequestHandler.Handle(request);

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var banksReturned = findResponse.Entities;
            Assert.AreEqual(2, banksReturned.Count);
            Assert.AreEqual(bank1.Id, banksReturned[0].Id);
            Assert.AreEqual(bank2.Id, banksReturned[1].Id);
        }
    }
}