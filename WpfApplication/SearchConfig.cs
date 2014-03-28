using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Domain.Implementation;
using Domain.Interface;

namespace WpfApplication
{
    public class SearchResult
    {
        public string Project { get; set; }
        public string Chemin { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public string DataSource { get; set; }
    }

    public class ConnectionRules
    {
        public ConnectionRules(IConnectionStringRulesValidatorSimple connectionStringRulesValidatorSimple)
        {
            Id = connectionStringRulesValidatorSimple.Id;
            Name = connectionStringRulesValidatorSimple.RuleName;

            ConnectionStringActive = connectionStringRulesValidatorSimple.ConnectionString.Active;
            ConnectionStringCriteria = connectionStringRulesValidatorSimple.ConnectionString.Criteria;
            ConnectionStringMatch = connectionStringRulesValidatorSimple.ConnectionString.Match;
            ConnectionStringRegex = connectionStringRulesValidatorSimple.ConnectionString.Regex;

            NameActive = connectionStringRulesValidatorSimple.Name.Active;
            NameCriteria = connectionStringRulesValidatorSimple.Name.Criteria;
            NameMatch = connectionStringRulesValidatorSimple.Name.Match;
            NameRegex = connectionStringRulesValidatorSimple.Name.Regex;
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
    }

    public class SearchConfig : ObservableCollection<SearchResult>
    {
        private readonly ICsprojFinder _csprojFinder;
        private readonly ICsprojParseur _csprojParseur;
        private readonly IMessagingReceiver _message;
        public ObservableCollection<SearchResult> _searchValues = new ObservableCollection<SearchResult>();
        public ObservableCollection<ConnectionRules> _rules = new ObservableCollection<ConnectionRules>();

        public ObservableCollection<SearchResult> SearchValues
        {
            get { return _searchValues; }
            set { _searchValues = value; }
        }

        public ObservableCollection<ConnectionRules> Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }


        public SearchConfig(
            ICsprojFinder csprojFinder,
            ICsprojParseur csprojParseur,
            IMessagingReceiver message,
            IConnectionStringRulesValidatorService connectionStringRulesValidatorService
            )
        {
            _message = message;
            _csprojFinder = csprojFinder;
            _csprojParseur = csprojParseur;
            var items = connectionStringRulesValidatorService.Get();
            foreach (var connectionStringRulesValidatorSimple in items)
                Rules.Add(new ConnectionRules(connectionStringRulesValidatorSimple));
        }

        public async void Launch(string directoriesSearch)
        {
            Clear();
            await Task.Run(() => _csprojFinder.DirSearch(directoriesSearch, new ForCsProjFind(this).CsProjFind));
        }

        private class ForCsProjFind
        {
            private readonly SearchConfig _searchConfig;

            public ForCsProjFind(SearchConfig searchConfig)
            {
                _searchConfig = searchConfig;
            }

            public void CsProjFind(FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    return;

                _searchConfig._message.ReceiveMessage += ReceiveMessage;
                _searchConfig._csprojParseur.ParseAsync(fileInfo.FullName);
            }

            private void ReceiveMessage(IMessage message)
            {
                var searchResult = new SearchResult
                {
                    Name = message.Message
                };



                Application.Current.Dispatcher.Invoke(() =>
                    _searchConfig.SearchValues.Add(searchResult));
            }
        }
    }
}