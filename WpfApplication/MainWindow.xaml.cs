using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain.Interface;
using Microsoft.Practices.ServiceLocation;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SearchConfig SearchingConfig { get; set; }

        private FormSettings formSettings;

        public ObservableCollection<SearchResult> Collection
        {
            get { return SearchingConfig; }
        }

        public MainWindow()
        {
            formSettings = new FormSettings();
            SearchingConfig = ServiceLocator.Current.GetInstance<SearchConfig>();
            InitializeComponent();

            txtDirectorySearch.Text = formSettings.DirectorySearch;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SearchingConfig.Launch(txtDirectorySearch.Text);

        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            txtDirectorySearch.Text = dialog.SelectedPath;

            formSettings.DirectorySearch = txtDirectorySearch.Text;
            formSettings.Save();

        }

        private void addNewRules(object sender, RoutedEventArgs e)
        {
            SearchingConfig.CreateNewRules();
        }
    }

    //Application settings wrapper class 
    sealed class FormSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        public String DirectorySearch
        {
            get { return (String)this["DirectorySearch"]; }
            set { this["DirectorySearch"] = value; }
        }
    }
}
