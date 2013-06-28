using System;
using System.Windows;
using HireHomeEntertainment.View;
using HireHomeEntertainment.ViewModel;
using HireHomeEntertainment.Singletons;
using System.Reflection;
using System.IO;

namespace HireHomeEntertainment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);            
           
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
