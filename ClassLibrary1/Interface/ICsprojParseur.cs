using System;
using System.Threading.Tasks;

namespace Domain.Interface
{
    /// <summary>
    /// Open a csproj and send it to ICsprojExecutor for being parsed.
    /// </summary>
    public interface ICsprojParseur
    {
        /// <summary>
        /// Parse the csproj file Async
        /// </summary>
        /// <param name="fullPathName">path of the file</param>
        /// <returns></returns>
        Task ParseAsync(string fullPathName);

        /// <summary>
        /// Parse the csproj file
        /// </summary>
        /// <param name="fullPathName">path of the file</param>
        void Parse(string fullPathName);
    }
}