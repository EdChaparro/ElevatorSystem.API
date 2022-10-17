using IntrepidProducts.Biz;
using IntrepidProducts.Biz.RequestHandlers.Buildings;
using IntrepidProducts.IocContainer;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest
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
        }
    }
}
