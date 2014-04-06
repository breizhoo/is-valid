using System;
using System.ComponentModel;
using Domain.Interface;

namespace WpfApplication
{
    public class ConnectionRules
    {
        public ConnectionRules(IConnectionStringRulesValidatorSimple connectionStringRulesValidatorSimple)
        {
            Map(connectionStringRulesValidatorSimple, this);
        }

        public ConnectionRules()
        {
            
        }

        public static ConnectionRules Map(
            IConnectionStringRulesValidatorSimple @from,
            ConnectionRules to = null)
        {
            to = to ?? new ConnectionRules();
            to.Id = @from.Id;
            to.Name = @from.RuleName;

            if (@from.ConnectionString != null)
            {
                to.ConnectionStringActive = @from.ConnectionString.Active;
                to.ConnectionStringCriteria = @from.ConnectionString.Criteria;
                to.ConnectionStringMatch = @from.ConnectionString.Match;
                to.ConnectionStringRegex = @from.ConnectionString.Regex;
            }

            if (@from.Name != null)
            {
                to.NameActive = @from.Name.Active;
                to.NameCriteria = @from.Name.Criteria;
                to.NameMatch = @from.Name.Match;
                to.NameRegex = @from.Name.Regex;
            }
            return to;
        }

        //private static IConnectionStringRulesValidatorSimple MapTo(ConnectionRules from, IConnectionStringRulesValidatorSimple to )
        //{

        //}
        

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
    }
}