using Domain.Implementation;
using Domain.Interface;
using Ninject.Modules;

namespace Domain.BootStrapper
{
    public class DomainModule : NinjectModule
    {

        public override void Load()
        {
            Bind<IConfigTransform>().To<ConfigTransform>();
            Bind<ICsprojFinder>().To<CsprojFinder>();
            Bind<ICsprojParseur>().To<CsprojParseur>();
            Bind<ICsprojExecutor>().To<ConfigFinder>();
            Bind<IConfigFindExecutor>().To<ConfigFindExecutor>();
            Bind<IConfigParseurExecutor>().To<ConfigParseur>();
            Bind<IMessagingSender, IMessagingReceiver>().To<MessagingExecutor>().InSingletonScope();
            Bind<IConnectionStringRulesValidatorService>().To<ConnectionStringRulesValidatorService>();
            Bind<IConnectionStringValidator>().To<ConnectionStringValidator>();
            Bind<IApplicationVariables>().To<ApplicationVariables>();
        }
    }
}