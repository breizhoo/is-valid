using System.IO;

namespace Domain.Interface
{
    public interface IConfigFile
    {
        FileInfo SourceFile { get; }

        FileInfo TransformFile { get; }
    }
}