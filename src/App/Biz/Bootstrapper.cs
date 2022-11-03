using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.IoC.MicrosoftStrategy;
using IntrepidProducts.IocContainer;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace IntrepidProducts.Biz
{
    public class Bootstrapper : BootstrapperAbstract
    {
        public Bootstrapper(IServiceCollection? serviceCollection = null)
            : base(new MicrosoftStrategy(serviceCollection))
        {
            _requestHandlerRegistry = new RequestHandlerRegistry();
        }

        private readonly IRequestHandlerRegistry _requestHandlerRegistry;

        protected override void ConfigIoC(IIocContainer iocContainer)
        {
            RegisterRequestHandlers();
            iocContainer.RegisterInstance(_requestHandlerRegistry);
            iocContainer.RegisterSingleton
                (typeof(IRequestHandlerProcessor), typeof(RequestHandlerProcessor));

            iocContainer.RegisterInstance(typeof(IIocContainer), IocContainer);

            var repoConfigManager = new RepoConfigurationManager("ElevatorSystemDb");
            iocContainer.RegisterInstance(repoConfigManager);

            //TODO: Remove this once all Request Handlers are switched to use Repos
            iocContainer.RegisterInstance(new Buildings());
        }

        private void RegisterRequestHandlers()
        {
            var requestHandlerInterfaceType = typeof(IRequestHandler);

            _requestHandlerRegistry.RequestHandlerFoundEvent += (sender, e) =>
            {
                IocContainer.RegisterTransient
                    (e.RequestHandlerType.FullName, requestHandlerInterfaceType, e.RequestHandlerType);
            };

            _requestHandlerRegistry.Register(GetType().Assembly);   //Register all Request Handlers in this Assembly
        }
    }
}
