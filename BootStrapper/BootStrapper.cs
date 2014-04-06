using System;
using System.Collections.Generic;
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

            var ninjectModules = new List<INinjectModule> ();
            ninjectModules.AddRange(Domain.BootStrapper.BootStrapper.GetModules());
            ninjectModules.AddRange(Transverse.BootStrapper.BootStrapper.GetModules());

            IKernel kernel = new StandardKernel(settings, ninjectModules.ToArray());

            if (_configuration != null)
                _configuration(kernel);
            return kernel;
        }
    }
}
