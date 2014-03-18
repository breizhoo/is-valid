using Domain.Implementation;
using Domain.Interface;
using Domain.Interface.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class CsprojParseurTest
    {
        [TestMethod]
        [DeploymentItem("MvcApplication1.csproj")]
        public void CsprojParseur_Parse()
        {
            var iCsprojParseurOutput = new StubICsprojParseurOutput();
            var csprojParseur = new CsprojParseur(iCsprojParseurOutput);
            Task t = csprojParseur.ParseAsync("MvcApplication1.csproj");
            t.Wait();
        }

        public void Files(IConfigFile configFile)
        {
            Thread.Sleep(1000);
        }

    }
}
