using System.Collections.Generic;
using Microsoft.Build.Evaluation;

namespace Domain.Interface
{
    public interface IConfigParseurExecutor
    {
        void Execute(Project project, IEnumerable<IConfigFileTransfomed> configFile);
    }
}