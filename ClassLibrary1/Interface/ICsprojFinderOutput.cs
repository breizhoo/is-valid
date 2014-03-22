using System.IO;

namespace Domain.Interface
{
    public interface ICsprojFinderOutput
    {
        void FindCsproj(FileInfo fileInfo);
    }
}