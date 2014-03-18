using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Interface;
using Ninject.Extensions.Logging;

namespace Domain.Implementation
{
    /// <summary>
    /// Search Visual Studio project on the file system.
    /// </summary>
    internal class CsprojFinder : ICsprojFinder
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        public CsprojFinder(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Search Visual studio file on the file system.
        /// </summary>
        /// <param name="sDir">Where search Visual Studio files.</param>
        /// <param name="fileInfoFind">Callback</param>
        public void DirSearch(string sDir, Action<FileInfo> fileInfoFind)
        {
            Parallel.ForEach(new DirectoryInfo(sDir)
                .GetFiles("*.csproj", SearchOption.AllDirectories),
                (fileinfo) =>
                {
                    _logger.Info("Find file : {0}", fileinfo.Name);
                    try
                    {
                        fileInfoFind(fileinfo);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Exception on callback of DirSearch.");
                    }
                }
                );
        }
    }
}
