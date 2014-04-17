using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Domain.Interface;
using Transverse.Api;

namespace Domain.Implementation
{

    internal class ConnectionStringRulesValidatorService : IConnectionStringRulesValidatorService
    {
        private static readonly object Locker = new Object();
        private readonly IApplicationVariables _applicationVariables;
        private readonly ILogger _logger;

        public ConnectionStringRulesValidatorService(
            IApplicationVariables applicationVariables,
            ILogger logger)
        {
            _logger = logger;
            _applicationVariables = applicationVariables;
        }

        public IConnectionStringRulesValidatorSimple GetNew()
        {
            return new ConnectionStringRulesValidatorSimple();
        }

        public IEnumerable<IConnectionStringRulesValidatorSimple> Get()
        {
            try
            {
                lock (Locker)
                {
                    var file = GetFile();
                    var dcs = new DataContractSerializer(typeof(ConnectionStringRulesValidatorSimple[]));

                    if (file.Exists)
                        using (var stream = file.Open(FileMode.Open))
                        {
                            using (XmlDictionaryReader reader =
                                XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas()))
                            {
                                return (ConnectionStringRulesValidatorSimple[])dcs.ReadObject(reader, true);
                            }
                        }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on getting data.");
            }
            return Enumerable.Empty<IConnectionStringRulesValidatorSimple>();
        }

        public void Delete(IConnectionStringRulesValidatorSimple connectionStringRulesValidator)
        {
            var allDatas = Get().ToList();
            allDatas.RemoveAll(x => x.Id == connectionStringRulesValidator.Id);
            Save(allDatas);
        }

        public void Save(IConnectionStringRulesValidatorSimple connectionStringRulesValidator)
        {
            var allDatas = Get().ToList();
            allDatas.RemoveAll(x => x.Id == connectionStringRulesValidator.Id);
            allDatas.Add(connectionStringRulesValidator);

            Save(allDatas);
        }

        private DirectoryInfo GetDirectory()
        {
            var appData = _applicationVariables.GetApplicationDataDirectory();
            return new DirectoryInfo(Path.Combine(appData, "OlivierAppli"));
        }

        private FileInfo GetFile()
        {
            var path = GetDirectory();
            if (!path.Exists)
                path.Create();
            return new FileInfo(Path.Combine(path.FullName, "ConnectionStringRulesValidatorService.xml"));
        }

        public void Save(IEnumerable<IConnectionStringRulesValidatorSimple> connectionStringRulesValidators)
        {
            try
            {
                lock (Locker)
                {
                    var file = GetFile();
                    var dcs = new DataContractSerializer(typeof(ConnectionStringRulesValidatorSimple[]));

                    if(file.Exists)
                        file.Delete();
                    using (var stream = file.Open(FileMode.OpenOrCreate))
                    {
                        stream.SetLength(0);
                        stream.Flush();
                        using (XmlDictionaryWriter writer =
                            XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                        {
                            writer.WriteStartDocument();
                            dcs.WriteObject(writer,
                                connectionStringRulesValidators
                                .Select(x => new ConnectionStringRulesValidatorSimple(x))
                                .ToArray());
                        }
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on saving data.");
            }
        }
    }
}
