using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;

namespace Domain.BootStrapper
{
    public class BootStrapper
    {
        public static IEnumerable<INinjectModule> GetModules()
        {
            yield return new DomainModule();

        }
    }
}
