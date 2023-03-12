using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystem.Shared.Requests.Buildings;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Buildings;
using IntrepidProducts.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace IntrepidProducts.ElevatorSystemBizTest.RequestHandlers.Buildings
{
    [TestClass]
    public class FindAllBuildingsRequestHandlerTest
    {
        [TestMethod]
        public void ShouldReturnAllBuildings()
        {
            //Setup
            var mockRepo = new Mock<IRepository<Building>>();

            var buildings = new List<Building>
            {
                new Building(),
                new Building()
            };

            mockRepo.Setup(x =>
                    x.FindAll())
                .Returns(buildings);

            var findBuildingsRequestHandler = new FindAllBuildingsRequestHandler(mockRepo.Object);

            var findResponse = findBuildingsRequestHandler
                .Handle(new FindAllBuildingsRequest());

            //Assert
            Assert.IsTrue(findResponse.IsSuccessful);

            var buildingsReturned = findResponse.Entities;
            Assert.AreEqual(2, buildingsReturned.Count);
            Assert.AreEqual(buildings[0].Id, buildingsReturned[0].Id);
            Assert.AreEqual(buildings[1].Id, buildingsReturned[1].Id);
        }
    }
}