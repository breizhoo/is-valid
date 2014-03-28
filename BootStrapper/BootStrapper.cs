using System;
using Domain.Implementation;
using Domain.Interface;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NinjectAdapter;
using Ninject.Extensions.Logging.Log4net;

namespace BootStrapper
{
    public class BootStrapper
    {
        private static Action<IKernel> _configuration;

        public static void Initialize(Action<IKernel> configuration = null)
        {
            _configuration = configuration;
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(CreateKernel()));
        }

        private static IKernel CreateKernel()
        {
            log4net.Config.XmlConfigurator.Configure();
            var settings = new NinjectSettings { LoadExtensions = false };
            IKernel kernel = new StandardKernel(settings, new INinjectModule[] { new Log4NetModule() });

            // Domain configuration
            kernel.Bind<IConfigTransform>().To<ConfigTransform>();
            kernel.Bind<ICsprojFinder>().To<CsprojFinder>();
            kernel.Bind<ICsprojParseur>().To<CsprojParseur>();
            kernel.Bind<ICsprojExecutor>().To<ConfigFinder>();
            kernel.Bind<IConfigFindExecutor>().To<ConfigFindExecutor>();
            kernel.Bind<IConfigParseurExecutor>().To<ConfigParseur>();
            kernel.Bind<IMessagingSender, IMessagingReceiver>().To<MessagingExecutor>().InSingletonScope();
            kernel.Bind<IConnectionStringRulesValidatorService>().To<ConnectionStringRulesValidatorService>();
            kernel.Bind<IConnectionStringValidator>().To<ConnectionStringValidator>();
            kernel.Bind<IApplicationVariables>().To<ApplicationVariables>();

            if (_configuration != null)
                _configuration(kernel);
            return kernel;
        }
    }
}
