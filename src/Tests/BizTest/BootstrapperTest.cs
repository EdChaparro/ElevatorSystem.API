using IntrepidProducts.Biz;
using IntrepidProducts.Biz.RequestHandlers;
using IntrepidProducts.IocContainer;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.BizTest
{
    [TestClass]
    public class BootstrapperTest
    {
        [TestMethod]
        public void ShouldRegisterDependencies()
        {
            var bootstrapper = new Bootstrapper();
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
