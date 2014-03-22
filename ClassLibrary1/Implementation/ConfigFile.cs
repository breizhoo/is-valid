using System.IO;
using Domain.Interface;

namespace Domain.Implementation
{
    /// <summary>
    /// Representation of a config file.
    /// </summary>
    internal class ConfigFile : IConfigFile
    {
        /// <summary>
        /// Path of the config file
        /// </summary>
        public FileInfo SourceFile { get; set; }

        /// <summary>
        /// Path of le config file of transformation.
        /// </summary>
        public FileInfo TransformFile { get; set; }
    }
}