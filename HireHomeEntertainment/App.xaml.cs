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
            MainWindowViewModel vm = new MainWindowViewModel();
            MainWindow main = new MainWindow();
            Navigator.NavigationService = main.NavigationFrame.NavigationService;
            main.DataContext = vm;
            main.Show();           
           
            // Load and navigate to the first page
            Page1ViewModel pagevm = new Page1ViewModel();
            Page1 p1 = new Page1();
            p1.DataContext = pagevm;
            Navigator.NavigationService.Navigate(p1);
            
        }
    }
}
