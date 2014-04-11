using System;
using System.ComponentModel;
using Domain.Interface;

namespace WpfApplication
{
    public class ConnectionStringItemValidator : IConnectionStringItemValidator
    {

        public string Regex { get; set; }

        public bool Match { get; set; }

        public bool Criteria { get; set; }

        public bool Active { get; set; }
    }

    public class ConnectionStringRulesValidatorSimple : IConnectionStringRulesValidatorSimple
    {

        public ConnectionStringRulesValidatorSimple()
        {
            Project = new ConnectionStringItemValidator();
            File = new ConnectionStringItemValidator();
            Provider = new ConnectionStringItemValidator();
            Name = new ConnectionStringItemValidator();
            ConnectionString = new ConnectionStringItemValidator();
        }

        public Guid Id { get; set; }

        public string RuleName { get; set; }

        public IConnectionStringItemValidator Project { get; set; }

        public IConnectionStringItemValidator File { get; set; }

        public IConnectionStringItemValidator Provider { get; set; }

        public IConnectionStringItemValidator Name { get; set; }

        public IConnectionStringItemValidator ConnectionString { get; set; }
    }

    public class ConnectionRules
    {
        public ConnectionRules(IConnectionStringRulesValidatorSimple connectionStringRulesValidatorSimple)
        {
            Map(connectionStringRulesValidatorSimple, this);
        }

        public ConnectionRules()
        {

        }

        public IConnectionStringRulesValidatorSimple Convert()
        {
            return Map(this);
        }


        public static ConnectionRules Map(
            IConnectionStringRulesValidatorSimple ffrom,
            ConnectionRules to = null)
        {
            to = to ?? new ConnectionRules();
            to.Id = ffrom.Id;
            to.Name = ffrom.RuleName;

            if (ffrom.ConnectionString != null)
            {
                to.ConnectionStringActive = ffrom.ConnectionString.Active;
                to.ConnectionStringCriteria = ffrom.ConnectionString.Criteria;
                to.ConnectionStringMatch = ffrom.ConnectionString.Match;
                to.ConnectionStringRegex = ffrom.ConnectionString.Regex;
            }

            if (ffrom.Name != null)
            {
                to.NameActive = ffrom.Name.Active;
                to.NameCriteria = ffrom.Name.Criteria;
                to.NameMatch = ffrom.Name.Match;
                to.NameRegex = ffrom.Name.Regex;
            }

            if (ffrom.File != null)
            {
                to.FileActive = ffrom.File.Active;
                to.FileCriteria = ffrom.File.Criteria;
                to.FileMatch = ffrom.File.Match;
                to.FileRegex = ffrom.File.Regex;
            }

            if (ffrom.Project != null)
            {
                to.ProjectActive = ffrom.Project.Active;
                to.ProjectCriteria = ffrom.Project.Criteria;
                to.ProjectMatch = ffrom.Project.Match;
                to.ProjectRegex = ffrom.Project.Regex;
            }

            if (ffrom.Provider != null)
            {
                to.ProviderActive = ffrom.Provider.Active;
                to.ProviderCriteria = ffrom.Provider.Criteria;
                to.ProviderMatch = ffrom.Provider.Match;
                to.ProviderRegex = ffrom.Provider.Regex;
            }

            return to;
        }

        public static IConnectionStringRulesValidatorSimple Map(
            ConnectionRules ffrom,
            IConnectionStringRulesValidatorSimple to = null
            )
        {
            to = to ?? new ConnectionStringRulesValidatorSimple();
            to.Id = ffrom.Id;
            to.RuleName = ffrom.Name;

            to.ConnectionString.Active = ffrom.ConnectionStringActive;
            to.ConnectionString.Criteria = ffrom.ConnectionStringCriteria;
            to.ConnectionString.Match = ffrom.ConnectionStringMatch;
            to.ConnectionString.Regex = ffrom.ConnectionStringRegex;
            
            to.Name.Active = ffrom.NameActive;
            to.Name.Criteria = ffrom.NameCriteria;
            to.Name.Match = ffrom.NameMatch;
            to.Name.Regex = ffrom.NameRegex;

            to.File.Active = ffrom.FileActive;
            to.File.Criteria = ffrom.FileCriteria;
            to.File.Match = ffrom.FileMatch;
            to.File.Regex = ffrom.FileRegex;

            to.Project.Active = ffrom.ProjectActive;
            to.Project.Criteria = ffrom.ProjectCriteria;
            to.Project.Match = ffrom.ProjectMatch;
            to.Project.Regex = ffrom.ProjectRegex;

            to.Provider.Active = ffrom.ProviderActive;
            to.Provider.Criteria = ffrom.ProviderCriteria;
            to.Provider.Match = ffrom.ProviderMatch;
            to.Provider.Regex = ffrom.ProviderRegex;

            return to;
        }

        [Category("Inforamtion")]
        public Guid Id { get; private set; }

        [Category("Inforamtion")]
        public String Name { get; set; }


        [Category("ConnectionString")]
        [DisplayName("Active")]
        [Description("This property uses a ...")]
        public bool ConnectionStringActive { get; set; }

        [Category("ConnectionString")]
        [DisplayName("Criteria")]
        [Description("This property uses a ...")]
        public bool ConnectionStringCriteria { get; set; }

        [Category("ConnectionString")]
        [DisplayName("Match")]
        [Description("This property uses a ...")]
        public bool ConnectionStringMatch { get; set; }

        [Category("ConnectionString")]
        [DisplayName("Regex")]
        [Description("This property uses a ...")]
        public string ConnectionStringRegex { get; set; }




        [Category("Name")]
        [DisplayName("Active")]
        [Description("This property uses a ...")]
        public bool NameActive { get; set; }

        [Category("Name")]
        [DisplayName("Criteria")]
        [Description("This property uses a ...")]
        public bool NameCriteria { get; set; }

        [Category("Name")]
        [DisplayName("Match")]
        [Description("This property uses a ...")]
        public bool NameMatch { get; set; }

        [Category("Name")]
        [DisplayName("Regex")]
        [Description("This property uses a ...")]
        public string NameRegex { get; set; }







        [Category("File")]
        [DisplayName("Active")]
        [Description("This property uses a ...")]
        public bool FileActive { get; set; }

        [Category("File")]
        [DisplayName("Criteria")]
        [Description("This property uses a ...")]
        public bool FileCriteria { get; set; }

        [Category("File")]
        [DisplayName("Match")]
        [Description("This property uses a ...")]
        public bool FileMatch { get; set; }

        [Category("File")]
        [DisplayName("Regex")]
        [Description("This property uses a ...")]
        public string FileRegex { get; set; }







        [Category("Project")]
        [DisplayName("Active")]
        [Description("This property uses a ...")]
        public bool ProjectActive { get; set; }

        [Category("Project")]
        [DisplayName("Criteria")]
        [Description("This property uses a ...")]
        public bool ProjectCriteria { get; set; }

        [Category("Project")]
        [DisplayName("Match")]
        [Description("This property uses a ...")]
        public bool ProjectMatch { get; set; }

        [Category("Project")]
        [DisplayName("Regex")]
        [Description("This property uses a ...")]
        public string ProjectRegex { get; set; }



        [Category("Provider")]
        [DisplayName("Active")]
        [Description("This property uses a ...")]
        public bool ProviderActive { get; set; }

        [Category("Provider")]
        [DisplayName("Criteria")]
        [Description("This property uses a ...")]
        public bool ProviderCriteria { get; set; }

        [Category("Provider")]
        [DisplayName("Match")]
        [Description("This property uses a ...")]
        public bool ProviderMatch { get; set; }

        [Category("Provider")]
        [DisplayName("Regex")]
        [Description("This property uses a ...")]
        public string ProviderRegex { get; set; }
    }
}