using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.Build.Evaluation;

namespace Domain.Implementation
{
    internal class CsprojParseur : ICsprojParseur
    {
        private readonly IEnumerable<ICsprojExecutor> _csprojExecutors;

        public CsprojParseur(IEnumerable<ICsprojExecutor> csprojExecutors)
        {
            _csprojExecutors = csprojExecutors;
        }

        public Task ParseAsync(string fullPathName, Action<IConfigFile> configFileFind)
        {
            return Task.Run(() => Parse(fullPathName, configFileFind));
        }

        public void Parse(string fullPathName, Action<IConfigFile> configFileFind)
        {

            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(fullPathName);

            var projectDirectoryName = Path.GetDirectoryName(project.FullPath);

            Parallel.ForEach(project.Items, projectItem =>
            {
                if (!projectItem.EvaluatedInclude.Contains(".config"))
                    return;

                configFileFind(MapProjectItemToConfigFile(
                    projectDirectoryName,
                    projectItem));

            });

            projectCollection.UnloadAllProjects();


        }

        public Task ParseAsync(string fullPathName)
        {
            return Task.Run(() => Parse(fullPathName));
        }

        public void Parse(string fullPathName)
        {
            if (_csprojExecutors == null)
                return;

            var projectCollection = new ProjectCollection();
            var project = projectCollection.LoadProject(fullPathName);

            foreach (var csprojExecutor in _csprojExecutors)
                csprojExecutor.Execute(project);

            projectCollection.UnloadAllProjects();
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
