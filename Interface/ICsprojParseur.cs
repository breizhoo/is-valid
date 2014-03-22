using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ICsprojParseur
    {
        Task ParseAsync(string fullPathName);
        void Parse(string fullPathName);
    }
}