using System;
using System.Collections.Generic;
using System.IO;
using Domain.Interface;
using Microsoft.Build.Evaluation;

namespace Domain.Implementation
{
    internal class ConfigFindExecutor : IConfigFindExecutor
    {
        private readonly IConfigTransform _configTransform;
        private readonly IEnumerable<IConfigParseurExecutor> _configParseurExecutors;

        public ConfigFindExecutor(
            IConfigTransform configTransform,
            IEnumerable<IConfigParseurExecutor> configParseurExecutors)
        {
            _configParseurExecutors = configParseurExecutors;
            _configTransform = configTransform;
        }

        public void Execute(Project project, IEnumerable<IConfigFile> configFiles)
        {
            var configFileTransfomeds = new List<IConfigFileTransfomed>();

            foreach (var configFile in configFiles)
            {
                if (!configFile.SourceFile.Exists)
                    continue;//todo : put warning.

                string destFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                var file = Path.Combine(destFile, configFile.SourceFile.Name);

                if (configFile.TransformFile == null)
                {
                    Directory.CreateDirectory(destFile);
                    configFile.SourceFile.CopyTo(file);
                }
                else if (!_configTransform.Transform(
                    configFile.SourceFile.FullName,
                    configFile.TransformFile.FullName,
                    destFile))
                    continue; //todo: put error log.

                configFileTransfomeds.Add(
                        new ConfigFileTransfomed(configFile)
                        {
                            TransformFile = new FileInfo(file)
                        }
                    );
            }

            foreach (var configParseurExecutor in _configParseurExecutors)
                configParseurExecutor.Execute(project, configFileTransfomeds);


        }
    }
}
