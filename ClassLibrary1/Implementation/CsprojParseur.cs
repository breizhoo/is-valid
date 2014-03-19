using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.Build.Evaluation;
using Ninject.Extensions.Logging;

namespace Domain.Implementation
{
    /// <summary>
    /// Open a csproj and send it to ICsprojExecutor for being parsed.
    /// </summary>
    internal class CsprojParseur : ICsprojParseur
    {
        private readonly IEnumerable<ICsprojExecutor> _csprojExecutors;
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="csprojExecutors">Item to execute for parsing csproj</param>
        /// <param name="logger">logguer</param>
        public CsprojParseur(IEnumerable<ICsprojExecutor> csprojExecutors, ILogger logger)
        {
            _logger = logger;
            _csprojExecutors = csprojExecutors;
        }

        /// <summary>
        /// Parse the csproj file Async
        /// </summary>
        /// <param name="fullPathName">path of the file</param>
        /// <returns></returns>
        public Task ParseAsync(string fullPathName)
        {
            return Task.Run(() => Parse(fullPathName));
        }

        /// <summary>
        /// Parse the csproj file
        /// </summary>
        /// <param name="fullPathName">path of the file</param>
        public void Parse(string fullPathName)
        {
            try
            {
                if (_csprojExecutors == null)
                    return;

                var projectCollection = new ProjectCollection();
                var project = projectCollection.LoadProject(fullPathName);

                foreach (var csprojExecutor in _csprojExecutors)
                {
                    try
                    {
                        csprojExecutor.Execute(project);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Exception during executing item durring the parsing of the csproj.");
                    }

                }

                projectCollection.UnloadAllProjects();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Exception during parsing the csproj.");
            }
        }
    }
}
