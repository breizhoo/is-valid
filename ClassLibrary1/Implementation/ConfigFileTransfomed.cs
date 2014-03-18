using System.IO;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConfigFileTransfomed : ConfigFile, IConfigFileTransfomed
    {
        internal ConfigFileTransfomed(IConfigFile configFile)
        {
            SourceFile = configFile.SourceFile;
            TransformFile = configFile.TransformFile;
        }

        public FileInfo LocationFile { get; set; }
    }
}