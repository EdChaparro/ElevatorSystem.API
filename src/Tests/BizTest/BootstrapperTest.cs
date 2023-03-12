using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.ElevatorSystemBiz;
using IntrepidProducts.ElevatorSystemBiz.RequestHandlers.Buildings;
using IntrepidProducts.IocContainer;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.ElevatorSystemBizTest
{
    [TestClass]
    public class BootstrapperTest
    {
        [TestMethod]
        public void ShouldRegisterDependencies()
        {
            var bootstrapper = new Bootstrapper(new ServiceCollection());
            bootstrapper.Bootstrap();

            var iocContainer = bootstrapper.IocContainer;

            //Note: Comparing Type to RuntimeTypes
            Assert.AreEqual
            (typeof(AddBuildingRequestHandler).FullName,
                iocContainer.Resolve<IRequestHandler>
                    (typeof(AddBuildingRequestHandler).FullName).GetType().FullName);

            Assert.IsNotNull(iocContainer.Resolve<IRequestHandlerRegistry>());
            Assert.IsNotNull(iocContainer.Resolve<IRequestHandlerProcessor>());
            Assert.IsNotNull(iocContainer.Resolve<IIocContainer>());
            Assert.IsNotNull(iocContainer.Resolve<RepoConfigurationManager>());

            Assert.IsNotNull(iocContainer.Resolve<IRepository<Building>>());
        }
    }
}