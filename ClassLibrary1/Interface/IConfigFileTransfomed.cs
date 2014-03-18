using System.IO;

namespace Domain.Interface
{
    public interface IConfigFileTransfomed : IConfigFile
    {
        FileInfo LocationFile { get; }
    }
}