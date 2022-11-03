using IntrepidProducts.IoC.MicrosoftStrategy;
using IntrepidProducts.IocContainer;
using IntrepidProducts.Repo;
using IntrepidProducts.RequestResponseHandler;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            RegisterRepositories();

            iocContainer.RegisterInstance(_requestHandlerRegistry);
            iocContainer.RegisterSingleton
                (typeof(IRequestHandlerProcessor), typeof(RequestHandlerProcessor));

            iocContainer.RegisterInstance(typeof(IIocContainer), IocContainer);

            var repoConfigManager = new RepoConfigurationManager("ElevatorSystemDb");
            iocContainer.RegisterInstance(repoConfigManager);
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

        private void RegisterRepositories()
        {
            var repoTypes = FindRepositories(typeof(BuildingFileRepo).Assembly);

            foreach (var repoType in repoTypes)
            {
                var interfaceType = repoType.GetInterfaces()
                    .FirstOrDefault(x => x.GetGenericTypeDefinition() == typeof(IRepository<>));

                if (interfaceType == null)
                {
                    throw new InvalidOperationException
                        ($"Repo Type {repoType.FullName} does not implement IRepository<>");
                }

                IocContainer.RegisterTransient(interfaceType, repoType);
            }
        }

        private static IEnumerable<Type> FindRepositories(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => x.GetInterfaces()
                    .Any(x => x.GetGenericTypeDefinition() == typeof(IRepository<>)));
        }
    }
}
