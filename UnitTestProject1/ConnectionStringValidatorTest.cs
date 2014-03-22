using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Implementation;
using Domain.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Ninject.Extensions.Logging.Fakes;

namespace UnitTestProject1
{
    [TestClass]
    public class ConnectionStringValidatorTest
    {
        private IConnectionStringValidator _connectionStringRulesValidator;
        private IConnectionStringItemForValidator _connectionString;

        [TestInitialize]
        public void TestInitializer()
        {
            var iLogger = new StubILogger();
            _connectionStringRulesValidator = new ConnectionStringValidator();
            _connectionString = new ConnectionStringItemForValidator()
                                {
                                    Name = "NameOfConnectionString",
                                    ConnectionString =
                                        "Data Source=(LocalDb)\v11.0;Initial Catalog=aspnet-MvcApplication1-20140304213218;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\aspnet-MvcApplication1-20140304213218.mdf",
                                    File = "C:\\Executable\\App.config",
                                    Project = "",
                                    Provider = "System.Data.SqlClient"
                                };
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_RulesEmpty_EmptyResult()
        {
            var rule = new ConnectionStringRulesValidator();
            Check.That(
                _connectionStringRulesValidator.IsValid(_connectionString, new[] { rule })
                ).IsEmpty();
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_Empty_EmptyResult()
        {
            Check.That(
                _connectionStringRulesValidator.IsValid(_connectionString, new ConnectionStringRulesValidator[0])
                ).IsEmpty();
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_MatchName_Success()
        {
            var rule1 = new ConnectionStringRulesValidator()
            {
                Name = new ConnectionStringItemValidator()
                       {
                           Active = true,
                           Criteria = true,
                           Regex = _connectionString.Name + "__",
                           Match = true
                       },
                ConnectionString = new ConnectionStringItemValidator()
                                   {
                                       Active = true,
                                       Criteria = false,
                                       Regex = ".*",
                                       Match = true
                                   }

            };

            var rule2 = new ConnectionStringRulesValidator()
            {
                Name = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = true,
                    Regex = _connectionString.Name,
                    Match = true
                },
                ConnectionString = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = false,
                    Regex = ".*",
                    Match = true
                }

            };
            var rulesMatch = _connectionStringRulesValidator.IsValid(_connectionString, new[] { rule1, rule2 }).ToList();
            Check.That(rulesMatch).HasSize(1);
            Check.That(rulesMatch.First().Id).IsEqualTo(rule2.Id);
        }
    }
}
