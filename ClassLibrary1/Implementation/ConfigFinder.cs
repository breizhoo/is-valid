using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Interface;
using Microsoft.Build.Evaluation;
using Ninject.Extensions.Logging;

namespace Domain.Implementation
{
    /// <summary>
    /// Parse a Csproj for finding .config.
    /// </summary>
    internal class ConfigFinder : ICsprojExecutor
    {
        private readonly IEnumerable<IConfigFindExecutor> _configExecutors;
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configExecutors">item to execute on config file finded</param>
        /// <param name="logger">logguer</param>
        public ConfigFinder(IEnumerable<IConfigFindExecutor> configExecutors, ILogger logger)
        {
            _logger = logger;
            _configExecutors = configExecutors;
        }

        /// <summary>
        /// Launch process
        /// </summary>
        /// <param name="project"></param>
        public void Execute(Project project)
        {
            try
            {
                //Origin path of the file is le path of the project
                var projectDirectoryName = Path.GetDirectoryName(project.FullPath);

                // All config files
                var result = from projectItem in project.Items
                             where projectItem.EvaluatedInclude.Contains(".config")
                             let configFile = MapProjectItemToConfigFile(
                                 projectDirectoryName,
                                 projectItem)
                             where configFile != null
                             select configFile;


                foreach (var configExecutor in _configExecutors)
                {
                    try
                    {
                        configExecutor.Execute(project, result);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error during parsing some config files.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during search config file in the csproj.");
            }
        }

        /// <summary>
        /// Transforme a ProjectItem to IConfigFile
        /// </summary>
        /// <param name="projectDirectoryName">Path of the csproj</param>
        /// <param name="projectItem">Item to transform</param>
        /// <returns></returns>
        private IConfigFile MapProjectItemToConfigFile(string projectDirectoryName, ProjectItem projectItem)
        {
            try
            {


                var evaluatedValueMeta =
                    projectItem.DirectMetadata.FirstOrDefault(x => x.EvaluatedValue.Contains(".config"));

                var sourceFile = evaluatedValueMeta != null
                    ? evaluatedValueMeta.EvaluatedValue
                    : projectItem.EvaluatedInclude;

                var transformFile = projectItem.DirectMetadataCount > 0
                    ? projectItem.EvaluatedInclude
                    : null;

                var fullTransformFile = transformFile == null
                    ? null
                    : Path.Combine(projectDirectoryName, transformFile);

                var realDirectory = fullTransformFile == null
                    ? projectDirectoryName
                    : Path.GetDirectoryName(fullTransformFile);

                return new ConfigFile
                {
                    SourceFile = new FileInfo(Path.Combine(realDirectory, sourceFile)),
                    TransformFile = fullTransformFile == null
                        ? null
                        : new FileInfo(fullTransformFile)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error durring transfomation to IConfigFile.");
            }
            return null;
        }

    }
}
