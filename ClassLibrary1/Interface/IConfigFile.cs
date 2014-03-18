using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Interface
{
    public interface IConfigFile
    {
        FileInfo SourceFile { get; }

        FileInfo TransformFile { get; }
    }

    public enum TypeError
    {
        Info,
        Warning,
        Error
    }

    public interface IMessage
    {
        int ErrorCode { get; }

        string Message { get; }

        TypeError TypeError { get; }

        string Id { get; }

    }
}