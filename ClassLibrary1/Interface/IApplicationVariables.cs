namespace Domain.Interface
{
    public interface IApplicationVariables
    {
        string GetApplicationName();

        string GetApplicationDataDirectory();

        string GetTempDirectory();
    }
}
