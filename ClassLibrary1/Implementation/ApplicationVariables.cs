using System;
using System.IO;

namespace Domain.Interface
{
    public class ApplicationVariables : IApplicationVariables
    {

        public string GetApplicationDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public string GetTempDirectory()
        {
            return Path.GetTempPath();
        }

        public string GetApplicationName()
        {
            return "CsprojParseur";
        }
    }
}