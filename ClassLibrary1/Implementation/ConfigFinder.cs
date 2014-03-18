using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Interface;
using Microsoft.Build.Evaluation;

namespace Domain.Implementation
{
    internal class ConfigFinder : ICsprojExecutor
    {
        private readonly IEnumerable<IConfigFindExecutor> _configExecutors;

        public ConfigFinder(IEnumerable<IConfigFindExecutor> configExecutors)
        {
            _configExecutors = configExecutors;
        }

        public void Execute(Project project)
        {
            try
            {
                var projectDirectoryName = Path.GetDirectoryName(project.FullPath);

                var result = from projectItem in project.Items
                    where projectItem.EvaluatedInclude.Contains(".config")
                    select MapProjectItemToConfigFile(
                        projectDirectoryName,
                        projectItem);

                var list = _configExecutors.ToList();
                foreach (var configExecutor in _configExecutors)
                {
                    configExecutor.Execute(project, result);
                }
            }
            catch (Exception ex)
            {
                //todo : log ?
            }
        }

        private IConfigFile MapProjectItemToConfigFile(string projectDirectoryName, ProjectItem projectItem)
        {
            var evaluatedValueMeta = projectItem.DirectMetadata.FirstOrDefault(x => x.EvaluatedValue.Contains(".config"));

            var sourceFile = evaluatedValueMeta != null ?
                evaluatedValueMeta.EvaluatedValue :
                projectItem.EvaluatedInclude;

            var transformFile = projectItem.DirectMetadataCount > 0 ?
                projectItem.EvaluatedInclude :
                null;

            var fullTransformFile = transformFile == null
                ? null
                : Path.Combine(projectDirectoryName, transformFile);

            var realDirectory = fullTransformFile == null ? projectDirectoryName : Path.GetDirectoryName(fullTransformFile);

            return new ConfigFile
            {
                SourceFile = new FileInfo(Path.Combine(realDirectory, sourceFile)),
                TransformFile = fullTransformFile == null ?
                    null :
                    new FileInfo(fullTransformFile)
            };

        }

    }
}
