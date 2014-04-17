using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.Build.Evaluation;
using Transverse.Api;

namespace Domain.Implementation
{
    /// <summary>
    /// On config finded transforme file if need and parsed it.
    /// </summary>
    internal class ConfigFindExecutor : IConfigFindExecutor
    {
        private readonly IConfigTransform _configTransform;
        private readonly IEnumerable<IConfigParseurExecutor> _configParseurExecutors;
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configTransform">To transforme the configFile</param>
        /// <param name="configParseurExecutors">parseurs of config file</param>
        /// <param name="logger">loggeur</param>
        public ConfigFindExecutor(
            IConfigTransform configTransform,
            IEnumerable<IConfigParseurExecutor> configParseurExecutors,
            ILogger logger)
        {
            _logger = logger;
            _configParseurExecutors = configParseurExecutors;
            _configTransform = configTransform;
        }

        /// <summary>
        /// Execute on ConfigFile and Project associed
        /// </summary>
        /// <param name="project">Project</param>
        /// <param name="configFiles">Config file</param>
        public void Execute(Project project, IEnumerable<IConfigFile> configFiles)
        {
            try
            {
                var configFileTransfomeds = new List<IConfigFileTransfomed>();


                Parallel.ForEach(configFiles, (configFile) =>
                {
                    if (!configFile.SourceFile.Exists)
                    {
                        _logger.Warn("SourceFile desn't exist : " + configFile.SourceFile.FullName);
                        return;
                    }

                    var destFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                    Directory.CreateDirectory(destFile);
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
                    {
                        _logger.Warn("unable to transform config file : " + configFile.TransformFile.FullName);
                        return;
                    }

                    configFileTransfomeds.Add(
                        new ConfigFileTransfomed(configFile)
                        {
                            TransformFile = new FileInfo(file)
                        }
                        );
                });

                foreach (var configParseurExecutor in _configParseurExecutors)
                {
                    try
                    {
                        configParseurExecutor.Execute(project, configFileTransfomeds);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error on executing the parseur of config file.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on config file executor.");
            }

        }
    }
}
