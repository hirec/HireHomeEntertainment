using System;
using System.Windows;
using HireHomeEntertainment.View;
using HireHomeEntertainment.ViewModel;
using HireHomeEntertainment.Singletons;
using System.Reflection;
using System.IO;
using MediaBrowser.ApiInteraction;
using System.Threading.Tasks;
using System.Threading;
using MediaBrowser.Model.System;
using MediaBrowser.Model.Net;

namespace HireHomeEntertainment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ApiClient privApiClient;
      
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            privApiClient = BaseMediaBrowserAPI.Instance.publicAPIClient;
            startApplication();            
        }

        private void startApplication()
        {
            // Load and show the MainWindow           
            MainWindow main = new MainWindow();
            Navigator.NavigationService = main.NavigationFrame.NavigationService;
            //MainWindowViewModel has to be initialized after navigationService is set otherwise the 
            //MainWindowViewModel tries to use an instance of NavigationService per the PageNavigation class
            //before it is initialized and returns a null reference to navigationservice
            MainWindowViewModel vm = new MainWindowViewModel();
            main.DataContext = vm;
            main.Show();
        }
    }
}
