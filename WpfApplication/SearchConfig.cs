using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
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

    internal class SearchConfig : ObservableCollection<SearchResult>
    {
        private readonly ICsprojFinder _csprojFinder;
        private readonly ICsprojParseur _csprojParseur;
        //private readonly IConfigTransform _configTransform;
        //private readonly IConfigParseur _configParseur;
        private readonly IMessagingReceiver _message;

        public SearchConfig(
            ICsprojFinder csprojFinder,
            ICsprojParseur csprojParseur,
            IMessagingReceiver message)
            //IConfigTransform configTransform,
            //IConfigParseur configParseur)
        {
            _message = message;
            _csprojFinder = csprojFinder;
            _csprojParseur = csprojParseur;
            //_configTransform = configTransform;
            //_configParseur = configParseur;

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
                //_searchConfig._csprojParseur.ParseAsync(fileInfo.FullName, new ForCsprojParseur(this, fileInfo).CsprojParseur);


            }

            private void ReceiveMessage(IMessage message)
            {
                var searchResult = new SearchResult
                {
                    //Chemin = _fileInfo.DirectoryName.Replace(file.FullName, ""),
                    //Project = _fileInfo.Name,
                    //DataSource = conectionStringItem.ConnectionString,
                    //Name = conectionStringItem.Name,
                    //ProviderName = conectionStringItem.ProviderName
                    Name = message.Message
                };



                Application.Current.Dispatcher.Invoke(() => 
                    _searchConfig.Add(searchResult));
            }


            //private class ForCsprojParseur
            //{
            //    private readonly ForCsProjFind _forCsProjFind;
            //    private readonly FileInfo _fileInfo;

            //    public ForCsprojParseur(ForCsProjFind forCsProjFind, FileInfo fileInfo)
            //    {
            //        _forCsProjFind = forCsProjFind;
            //        _fileInfo = fileInfo;
            //    }

            //    public void CsprojParseur(IConfigFile configFile)
            //    {
            //        if (!configFile.SourceFile.Exists)
            //            return;

            //        string destFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            //        var file = Path.Combine(destFile, configFile.SourceFile.Name);

            //        if (configFile.TransformFile == null)
            //        {
            //            Directory.CreateDirectory(destFile);
            //            configFile.SourceFile.CopyTo(file);
            //        }
            //        else if (!_forCsProjFind.
            //            _searchConfig.
            //            _configTransform.Transform(
            //            configFile.SourceFile.FullName,
            //            configFile.TransformFile.FullName,
            //            destFile))
            //            return;
            //        _forCsProjFind.
            //            _searchConfig.
            //            _configParseur.
            //            Parse(new FileInfo(file), configFile.SourceFile, ConectionStringFind);
            //    }

            //    private void ConectionStringFind(IConnectionStringItem conectionStringItem)
            //    {
            //        var file = new DirectoryInfo(_forCsProjFind._directoriesSearch);

            //        var searchResult = new SearchResult
            //        {
            //            Chemin = _fileInfo.DirectoryName.Replace(file.FullName, ""),
            //            Project = _fileInfo.Name,
            //            DataSource = conectionStringItem.ConnectionString,
            //            Name = conectionStringItem.Name,
            //            ProviderName = conectionStringItem.ProviderName
            //        };

            //        Application.Current.Dispatcher.Invoke(() => _forCsProjFind.
            //            _searchConfig.Add(searchResult));
            //    }


            //}
        }





    }
}