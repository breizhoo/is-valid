using System.IO;
using Domain.Interface;

namespace Domain.Implementation
{
    /// <summary>
    /// Config File with the transformed file.
    /// </summary>
    internal class ConfigFileTransfomed : ConfigFile, IConfigFileTransfomed
    {
        /// <summary>
        /// Ctor of copy
        /// </summary>
        /// <param name="configFile"></param>
        internal ConfigFileTransfomed(IConfigFile configFile)
        {
            SourceFile = configFile.SourceFile;
            TransformFile = configFile.TransformFile;
        }

        /// <summary>
        /// Location of file transformed.
        /// </summary>
        public FileInfo LocationFile { get; set; }
    }
}