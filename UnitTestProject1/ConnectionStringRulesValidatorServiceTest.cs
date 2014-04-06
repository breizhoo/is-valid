using System.Linq;
using Domain.Implementation;
using Domain.Interface;
using Domain.Interface.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Transverse.Api.Fakes;

namespace UnitTestProject1
{
    [TestClass]
    public class ConnectionStringRulesValidatorServiceTest
    {
        private IConnectionStringRulesValidatorService _connectionStringRulesValidatorService;
        private IApplicationVariables _applicationVariables;

        [TestInitialize]
        public void TestInitializer()
        {
            _applicationVariables = new StubIApplicationVariables()
                                         {
                                             GetApplicationDataDirectory = () => "",
                                             GetApplicationName = () => "ApplicationName"
                                         };
            var iLogger = new StubILogger();
            _connectionStringRulesValidatorService = new ConnectionStringRulesValidatorService(_applicationVariables, iLogger);
        }

        [TestMethod]
        public void ConnectionStringRulesValidatorService_Add_RulesEmpty_EmptyResult()
        {
            var item = _connectionStringRulesValidatorService.GetNew();
            _connectionStringRulesValidatorService.Save(item);
            var allItems = _connectionStringRulesValidatorService.Get().ToList();
            Check.That(allItems.Count(x => x.Id == item.Id)).IsEqualTo(1);

            _connectionStringRulesValidatorService.Delete(allItems.First());
            allItems = _connectionStringRulesValidatorService.Get().ToList();
            Check.That(allItems.Where(x => x.Id == item.Id)).IsEmpty();
        }

        [TestMethod]
        public void test()
        {
            var item = _connectionStringRulesValidatorService.GetNew();
            item.RuleName = "Test_Rules";
            item.Name = new ConnectionStringItemValidator
            {
                Active = true,
                Regex = ".*",
                Criteria = false,
                Match = true
            };

            _connectionStringRulesValidatorService.Save(item);
            var allItems = _connectionStringRulesValidatorService.Get().ToList();
            Check.That(allItems.Count(x => x.Id == item.Id)).IsEqualTo(1);

            _connectionStringRulesValidatorService.Delete(allItems.First());
            allItems = _connectionStringRulesValidatorService.Get().ToList();
            Check.That(allItems.Where(x => x.Id == item.Id)).IsEmpty();
        }
    }
}
