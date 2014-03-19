using Microsoft.Build.Evaluation;

namespace Domain.Interface
{
    /// <summary>
    /// Execute item on en csproj
    /// </summary>
    public interface ICsprojExecutor
    {
        /// <summary>
        /// Launch process on Project
        /// </summary>
        /// <param name="project"></param>
        void Execute(Project project);
    }
}