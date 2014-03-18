using System;
using System.IO;

namespace Domain.Interface
{
    /// <summary>
    /// Search Visual Studio project on the file system.
    /// </summary>
    public interface ICsprojFinder
    {
        /// <summary>
        /// Search Visual studio file on the file system.
        /// </summary>
        /// <param name="sDir">Where search Visual Studio files.</param>
        /// <param name="fileInfoFind">Callback</param>
        void DirSearch(string sDir, Action<FileInfo> fileInfoFind);
    }
}