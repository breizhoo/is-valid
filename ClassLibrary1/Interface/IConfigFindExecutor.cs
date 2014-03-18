using System.Collections.Generic;
using Microsoft.Build.Evaluation;

namespace Domain.Interface
{
    public interface IConfigFindExecutor
    {
        void Execute(Project project, IEnumerable<IConfigFile> configFile);
    }
}