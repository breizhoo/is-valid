using System.IO;

namespace Domain.Interface
{
    /// <summary>
    /// Config File with the transformed file.
    /// </summary>
    public interface IConfigFileTransfomed : IConfigFile
    {
        /// <summary>
        /// Location of file transformed.
        /// </summary>
        FileInfo LocationFile { get; }
    }
}