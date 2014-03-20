using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Domain.Interface;
using Microsoft.Build.Evaluation;
using Ninject.Extensions.Logging;

namespace Domain.Implementation
{
    /// <summary>
    /// Parseur of config file
    /// </summary>
    internal class ConfigParseur : IConfigParseurExecutor
    {
        private readonly IMessagingSender _messagingSender;
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="messagingSender"></param>
        /// <param name="logger"></param>
        public ConfigParseur(IMessagingSender messagingSender,
            ILogger logger)
        {
            _logger = logger;
            _messagingSender = messagingSender;
        }

        /// <summary>
        /// Parse the file.
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="connectionStringsFind"></param>
        private void Parse(FileInfo configFile, Action<IConnectionStringItem> connectionStringsFind)
        {
            if (!configFile.Exists)
            {
                _logger.Warn("configFile don't exist.");
                return;
            }

            // Todo : clean code. manage no case sesitive
            var eet = XElement.Load(configFile.FullName);
            var result = from connectionStrings in eet.Elements("connectionStrings")
                         from connectionString in connectionStrings.Descendants()
                         select (IConnectionStringItem)new ConnectionStringItem
                         {
                             Name = connectionString.Attribute("name").Value,
                             ProviderName = connectionString.Attribute("providerName").Value,
                             ConnectionString = connectionString.Attribute("connectionString").Value,
                         };

            foreach (var item in result)
            {
                try
                {
                    connectionStringsFind(item);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error on messaging connectionStringsFind");
                }
            }
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
                foreach (var configFileTransfomed in configFile)
                    Parse(configFileTransfomed.TransformFile,
                        connectionStringItem => _messagingSender.SendMessage(new Messaging
                    {
                        TypeError = TypeError.Info,
                        Message = GetType().ToString() + "Find connection string with name : " + connectionStringItem.Name,
                    }));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on parsing configFile");
            }
        }
    }
}
