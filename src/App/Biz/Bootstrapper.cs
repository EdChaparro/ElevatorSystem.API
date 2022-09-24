﻿using IntrepidProducts.ElevatorSystem;
using IntrepidProducts.IoC.MicrosoftStrategy;
using IntrepidProducts.IocContainer;
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