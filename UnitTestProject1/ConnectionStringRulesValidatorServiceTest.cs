using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Implementation;
using Domain.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace UnitTestProject1
{
    [TestClass]
    public class ConnectionStringRulesValidatorServiceTest
    {
        private IConnectionStringRulesValidatorService _connectionStringRulesValidatorService;

        [TestInitialize]
        public void TestInitializer()
        {
            _connectionStringRulesValidatorService = new ConnectionStringRulesValidatorService();
        }

        [TestMethod]
        public void ConnectionStringRulesValidatorService_Add_RulesEmpty_EmptyResult()
        {
            var item = _connectionStringRulesValidatorService.GetNew();
            _connectionStringRulesValidatorService.Save(item);
            var allItems = _connectionStringRulesValidatorService.Get().ToList();
            Check.That(
                allItems.Count(x => x.Id == item.Id)
                ).IsEqualTo(1);
            _connectionStringRulesValidatorService.Delete(allItems.First());

            allItems = _connectionStringRulesValidatorService.Get().ToList();
            Check.That(
                allItems.Where(x => x.Id == item.Id)
                ).IsEmpty();

        }
    }
}
