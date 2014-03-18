namespace Domain.Interface
{
    public interface IConfigParseurConnectionString
    {
        void FindConnectionString(string name, string providerName, string connectionString);
    }
}