using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Domain.Interface;
using Microsoft.Build.Evaluation;

namespace Domain.Implementation
{
    internal class ConfigParseur : IConfigParseur, IConfigParseurExecutor
    {
        private readonly IMessagingExecutor _messagingExecutor;

        public ConfigParseur(IMessagingExecutor messagingExecutor)
        {
            _messagingExecutor = messagingExecutor;
        }

        public void Parse(FileInfo configFile, FileInfo originPath, Action<IConnectionStringItem> connectionStringsFind)
        {
            try
            {
                if (!configFile.Exists)
                    return;

                //todo : clean code. manage no case sesitive
                var eet = XElement.Load(configFile.FullName);
                var result = from connectionStrings in eet.Elements("connectionStrings")
                             from connectionString in connectionStrings.Descendants()
                             select (IConnectionStringItem)new ConnectionStringItemItem
                             {
                                 Name = connectionString.Attribute("name").Value,
                                 ProviderName = connectionString.Attribute("providerName").Value,
                                 ConnectionString = connectionString.Attribute("connectionString").Value,
                             };

                foreach (var item in result)
                    connectionStringsFind(item);

            }
            catch (Exception ex)
            {
                //todo : log ?
            }
        }

        public void Execute(Project project, IEnumerable<IConfigFileTransfomed> configFile)
        {
            foreach (var configFileTransfomed in configFile)
                Parse(configFileTransfomed.TransformFile, null, (connectionStringItem) => _messagingExecutor.SendMessage(new Messaging
                {
                    TypeError = TypeError.Info,
                    Message = this.GetType().ToString() + "Find connection string with name : " + connectionStringItem.Name,
                }));
        }
    }
}
