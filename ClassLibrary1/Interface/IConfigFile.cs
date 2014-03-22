using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Interface
{
    /// <summary>
    /// Representation of a config file.
    /// </summary>
    public interface IConfigFile
    {
        /// <summary>
        /// Path of the config file
        /// </summary>
        FileInfo SourceFile { get; }

        /// <summary>
        /// Path of le config file of transformation.
        /// </summary>
        FileInfo TransformFile { get; }
    }
}