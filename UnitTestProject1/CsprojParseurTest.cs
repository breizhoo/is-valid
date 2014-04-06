using System.Text;
using Domain.Implementation;
using Domain.Interface;
using Domain.Interface.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using NFluent;
using Transverse.Api.Fakes;

namespace UnitTestProject1
{
    [TestClass]
    public class CsprojParseurTest
    {
        private ICsprojParseur _csprojParseur;
        private ICsprojExecutor _csprojExecutor;
        private bool _csprojExecutorCalled;

        [TestInitialize]
        public void TestInitializer()
        {
            _csprojExecutor = new StubICsprojExecutor
            {
                ExecuteProject = project => _csprojExecutorCalled = true
            };
            var iLogger = new StubILogger();
            _csprojParseur = new CsprojParseur(new[] { _csprojExecutor }, iLogger);
        }

        [TestMethod]
        [DeploymentItem("MvcApplication1.csproj")]
        public void CsprojParseur_Parse_FileExist_Success()
        {
            _csprojParseur.Parse("MvcApplication1.csproj");
            Check.That(_csprojExecutorCalled).IsTrue();
        }

        [TestMethod]
        public void CsprojParseur_Parse_FileNotFound_Faild()
        {
            _csprojParseur.Parse("MvcApplication1Fake.csproj");
            Check.That(_csprojExecutorCalled).IsFalse();
        }
    }
}
