using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Interface;
using Microsoft.Build.Evaluation;
using Transverse.Api;

namespace Domain.Implementation
{
    /// <summary>
    /// Parseur of config file
    /// </summary>
    internal class ConfigParseur : IConfigParseurExecutor
    {
        private readonly IMessagingSender _messagingSender;
        private readonly ILogger _logger;
        private readonly IConnectionStringRulesValidatorService _connectionStringRulesValidatorService;
        private readonly IConnectionStringValidator _connectionStringValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="messagingSender"></param>
        /// <param name="logger"></param>
        /// <param name="connectionStringRulesValidatorService"></param>
        /// <param name="connectionStringValidator"></param>
        public ConfigParseur(IMessagingSender messagingSender,
            ILogger logger,
            IConnectionStringRulesValidatorService connectionStringRulesValidatorService,
            IConnectionStringValidator connectionStringValidator)
        {
            _connectionStringValidator = connectionStringValidator;
            _connectionStringRulesValidatorService = connectionStringRulesValidatorService;
            _logger = logger;
            _messagingSender = messagingSender;
        }

        private string GetFromXElement(XElement connectionString, string key)
        {
            if (connectionString.Attribute(key) != null)
                return connectionString.Attribute(key).Value;
            return "";
        }

        /// <summary>
        /// Parse the file.
        /// </summary>
        /// <param name="project"></param>
        private IEnumerable<IConnectionStringItemForValidator> Parse(Project project, IConfigFileTransfomed configFileTransfomed)
        {
            var configFile = configFileTransfomed.TransformFile;
            if (!configFile.Exists)
            {
                _logger.Warn("configFile don't exist.");
                return Enumerable.Empty<IConnectionStringItemForValidator>();
            }

            // Todo : clean code, manage no case sensitive
            var eet = XElement.Load(configFile.FullName);
            return from connectionStrings in eet.Elements("connectionStrings")
                   from connectionString in connectionStrings.Descendants()
                   select new ConnectionStringItemForValidator
                   {
                       Name = GetFromXElement(connectionString, "name"),
                       Provider = GetFromXElement(connectionString, "providerName"),
                       ConnectionString = GetFromXElement(connectionString, "connectionString"),
                       Project = project.FullPath,
                       File = (configFileTransfomed.SourceFile)
                       .FullName
                   };
        }

        /// <summary>
        /// Launch the parsong of the files.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="configFile"></param>
        public void Execute(Project project, IEnumerable<IConfigFileTransfomed> configFile)
        {
            try
            {
                Parallel.ForEach(configFile, configFileTransfomed =>
                {
                    var connectionStrings = Parse(project, configFileTransfomed);
                    Parallel.ForEach(connectionStrings, connectionString =>
                    {
                        var rulesService = _connectionStringRulesValidatorService.Get()
                            .Select(x => new ConnectionStringRulesValidator(x));
                        var rules = _connectionStringValidator.IsValid(connectionString, rulesService);

                        foreach (var rule in rules)
                        {
                            _messagingSender.SendMessage(new Messaging
                            {
                                TypeError = TypeError.Error,
                                Message = string.Format(
                                "The rules with the name {0} has bean declanched on the connection string named {1} on project {2}",
                                rule.RuleName, connectionString.Name, project.FullPath)
                            });
                        }
                    });
                });

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on parsing configFile");
            }
        }
    }
}
