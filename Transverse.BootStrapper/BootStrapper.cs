using System.Collections.Generic;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Modules;

namespace Transverse.BootStrapper
{
    public class BootStrapper
    {
        public static IEnumerable<INinjectModule> GetModules()
        {
            yield return new Log4NetModule();
            yield return new TransverseModule();
        }
    }
}
