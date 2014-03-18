using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Domain.Interface;

namespace WindowsFormsApplication1
{
    internal class SearchConfig : ObservableCollection<string>
    {
        private ICsprojFinder _csprojFinder;
        private ICsprojParseur _csprojParseur;
        private IConfigTransform _configTransform;
        private IConfigParseur _configParseur;

        public SearchConfig(
            ICsprojFinder csprojFinder,
            ICsprojParseur csprojParseur,
            IConfigTransform configTransform,
            IConfigParseur configParseur)
            
        {
            _csprojFinder = csprojFinder;
            _csprojParseur = csprojParseur;
            _configTransform = configTransform;
            _configParseur = configParseur;

        }

        public async void Launch(string directoriesSearch)
        {
            //var csprojFinder = ServiceLocator.Current.GetInstance<ICsprojFinder>();
            await Task.Run(() => _csprojFinder.DirSearch(directoriesSearch, CsProjFind));
        }

        private void CsProjFind(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                return;

            _csprojParseur.ParseAsync(fileInfo.FullName, CsProjFind);
        }

        private void CsProjFind(IConfigFile configFile)
        {
            if (!configFile.SourceFile.Exists)
                return;

            string destFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var file = Path.Combine(destFile, configFile.SourceFile.Name);

            if (configFile.TransformFile == null)
            {
                Directory.CreateDirectory(destFile);
                configFile.SourceFile.CopyTo(file);
            }
            else if (!_configTransform.Transform(
                configFile.SourceFile.FullName,
                configFile.TransformFile.FullName,
                destFile))
                return;

            _configParseur.Parse(new FileInfo(file), configFile.SourceFile, ConectionStringFind);
        }

        private void ConectionStringFind(IConnectionStringItem conectionStringItem)
        {
            Add(conectionStringItem.ConnectionString);
        }
    }
}