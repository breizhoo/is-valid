using System.Text;
using Domain.Implementation;
using Domain.Interface;
using Domain.Interface.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using Ninject.Extensions.Logging.Fakes;

namespace UnitTestProject1
{
    [TestClass]
    public class CsprojParseurTest
    {
        [TestMethod]
        [DeploymentItem("MvcApplication1.csproj")]
        public void CsprojParseur_Parse()
        {
            var iCsprojExecutor = new StubICsprojExecutor();
            var iLogger = new StubILogger();
            var csprojParseur = new CsprojParseur(new[] { iCsprojExecutor }, iLogger);
            Task t = csprojParseur.ParseAsync("MvcApplication1.csproj");
            t.Wait();
        }
    }
}
