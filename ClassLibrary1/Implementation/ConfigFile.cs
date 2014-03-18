using System.IO;
using Domain.Interface;

namespace Domain.Implementation
{
    internal class ConfigFile : IConfigFile
    {
        public FileInfo SourceFile { get; set; }

        public FileInfo TransformFile { get; set; }
    }
}