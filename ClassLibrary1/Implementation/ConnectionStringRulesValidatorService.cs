using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Domain.Interface;
using Ninject.Extensions.Logging;

namespace Domain.Implementation
{

    internal class ConnectionStringRulesValidatorService : IConnectionStringRulesValidatorService
    {
        private static object locker = new Object();
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
                lock (locker)
                {
                    var file = GetFile();
                    var dcs = new DataContractSerializer(typeof(ConnectionStringRulesValidatorSimple[]));

                    if (file.Exists)
                        using (var stream = file.Open(FileMode.OpenOrCreate))
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
                lock (locker)
                {
                    var file = GetFile();
                    var dcs = new DataContractSerializer(typeof(ConnectionStringRulesValidatorSimple[]));

                    using (var stream = file.Open(FileMode.OpenOrCreate))
                    {
                        using (XmlDictionaryWriter writer =
                            XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                        {
                            writer.WriteStartDocument();
                            dcs.WriteObject(writer,
                                connectionStringRulesValidators.Cast<ConnectionStringRulesValidatorSimple>().ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on saving data.");
            }
        }
    }

    public interface IConnectionStringRulesValidatorService
    {
        IConnectionStringRulesValidatorSimple GetNew();

        IEnumerable<IConnectionStringRulesValidatorSimple> Get();

        void Delete(IConnectionStringRulesValidatorSimple connectionStringRulesValidator);

        void Save(IConnectionStringRulesValidatorSimple connectionStringRulesValidator);

        void Save(IEnumerable<IConnectionStringRulesValidatorSimple> connectionStringRulesValidators);


    }
}
