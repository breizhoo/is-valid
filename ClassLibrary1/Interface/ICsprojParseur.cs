using System;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ICsprojParseur
    {
        Task ParseAsync(string fullPathName, Action<IConfigFile> configFileFind);
        void Parse(string fullPathName, Action<IConfigFile> configFileFind);

        Task ParseAsync(string fullPathName);
        void Parse(string fullPathName);
    }
}