using Ninject.Modules;

namespace Transverse.BootStrapper
{
    public class TransverseModule : NinjectModule
    {

        public override void Load()
        {
            Bind<Api.ILogger>().To<Implementation.Logger>();
        }
    }
}