using System.Collections.Generic;
using Microsoft.Build.Evaluation;

namespace Domain.Interface
{
    /// <summary>
    /// Invoked when configFile findings
    /// </summary>
    public interface IConfigFindExecutor
    {
        /// <summary>
        /// ConfigFile and Project associed
        /// </summary>
        /// <param name="project">Project</param>
        /// <param name="configFile">Config file</param>
        void Execute(Project project, IEnumerable<IConfigFile> configFile);
    }
}