using System.Linq;
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
        private ConnectionStringRulesValidator _ruleDontMatch;

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
            _ruleDontMatch = new ConnectionStringRulesValidator()
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
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_RulesEmpty_EmptyResult()
        {
            var rule = new ConnectionStringRulesValidator();
            Check.That(
                _connectionStringRulesValidator.IsValid(_connectionString, new[] {rule}).ToList()
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
            var rule2 = new ConnectionStringRulesValidator()
            {
                Name = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = true,
                    Regex = ".*"+_connectionString.Name + ".*",
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
            var rulesMatch = _connectionStringRulesValidator.IsValid(_connectionString, new[] { _ruleDontMatch, rule2 }).ToList();
            Check.That(rulesMatch).HasSize(1);
            Check.That(rulesMatch.First().Id).IsEqualTo(rule2.Id);
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_MatchConnectionString_Success()
        {
            var rule2 = new ConnectionStringRulesValidator()
            {
                Name = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = true,
                    Regex = ".*",
                    Match = true
                },
                ConnectionString = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = false,
                    Regex = "Data Source=\\(LocalDb\\)\\v11\\.0.*",
                    Match = true
                }

            };
            var rulesMatch = _connectionStringRulesValidator.IsValid(_connectionString, new[] { _ruleDontMatch, rule2 }).ToList();
            Check.That(rulesMatch).HasSize(1);
            Check.That(rulesMatch.First().Id).IsEqualTo(rule2.Id);
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_MatchConnectionStringWithoutCriteria_Success()
        {
            var rule2 = new ConnectionStringRulesValidator()
            {
                ConnectionString = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = false,
                    Regex = "Data Source=\\(LocalDb\\)\\v11\\.0.*",
                    Match = true
                }

            };
            var rulesMatch = _connectionStringRulesValidator.IsValid(_connectionString, new[] { _ruleDontMatch, rule2 }).ToList();
            Check.That(rulesMatch).HasSize(1);
            Check.That(rulesMatch.First().Id).IsEqualTo(rule2.Id);
        }

        [TestMethod]
        public void ConnectionStringValidator_Parse_MatchConnectionStringWithoutCondition_EmptyResult()
        {
            var rule2 = new ConnectionStringRulesValidator()
            {
                Name = new ConnectionStringItemValidator()
                {
                    Active = true,
                    Criteria = true,
                    Regex = ".*",
                    Match = true
                }
            };

            var rulesMatch = _connectionStringRulesValidator.IsValid(_connectionString, new[] { _ruleDontMatch, rule2 }).ToList();
            Check.That(rulesMatch).IsEmpty();
        }
    }
}
