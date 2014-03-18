using Microsoft.Build.Evaluation;

namespace Domain.Interface
{
    public interface ICsprojExecutor
    {
        void Execute(Project project);
    }
}