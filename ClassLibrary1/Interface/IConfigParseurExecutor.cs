using System.Collections.Generic;
using Microsoft.Build.Evaluation;

namespace Domain.Interface
{
    /// <summary>
    /// Execute when config file finded.
    /// </summary>
    public interface IConfigParseurExecutor
    {
        /// <summary>
        /// Launch the parsing of the files.
        /// </summary>
        /// <param name="project">the Csproj</param>
        /// <param name="configFile">all the config files.</param>
        void Execute(Project project, IEnumerable<IConfigFileTransfomed> configFile);
    }
}