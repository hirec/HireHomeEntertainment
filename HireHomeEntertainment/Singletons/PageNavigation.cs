using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using MVVM;
using HireHomeEntertainment.Singletons;
using HireHomeEntertainment.View;
using HireHomeEntertainment.ViewModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace HireHomeEntertainment.Singletons
{
    public sealed class PageNavigation : ViewModelBase
    {
        private static volatile PageNavigation instance;
        private static object syncRoot = new Object();
        public static string CurrentPage;
        public static string PreviousPage;

        private Home Home;
        private Media MOVIES;
        private Media TV;
        private MainWindow Main;
        private MediaPlayer MediaPlayer;
        private HomeViewModel HomeVM;
        private MediaViewModel MediaVM;
        private MediaViewModel TVVM;
        private MainWindowViewModel MainVM;
        private MediaPlayerViewModel MediaPlayerVM;

        private Frame _frame;
        private bool _stopSearch;
        private string _previousPage;
        private string _currentPage;
        private List<string> _navigationStack;
        
        public static PageNavigation Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new PageNavigation();
                    }
                }
                return instance;
            }
        }

        private PageNavigation()
        {
            Window mainWindow = Application.Current.MainWindow;
            _frame = (Frame)mainWindow.FindName("NavigationFrame");   
            _navigationStack = new List<string>();
        }       

        public void NavigateBack(string callingPage)
        {
            _navigationStack.Remove(callingPage);
            NavigatePage(_navigationStack.Last());

            //_stopSearch = false;

            //foreach (JournalEntry j in _frame.BackStack)
            //{
            //    if (_stopSearch == false)
            //    {
            //        _previousPage = j.Name;
            //    }
            //    _stopSearch = true;
            //}

            //if (_previousPage == "Media")
            //{
            //    _previousPage = callingPage;
            //}
           // NavigatePage(_previousPage);

        }
        

        public void NavigatePage(string _pageName)
        {
            _navigationStack.Add(_pageName);
            MessageBox.Show("added " + _pageName + " to stack");

            CurrentPage = _pageName;

            switch (_pageName)
            {
                case "Main":
                    if (Main == null)
                    {
                        MainVM = new MainWindowViewModel();
                        Main = new MainWindow();                        
                    }
                    Main.DataContext = MainVM;                   
                    Navigator.NavigationService.Navigate(Main);
                    break;
                case "Home":
                    if (Home == null)
                    {
                        HomeVM = new HomeViewModel();
                        Home = new Home();                       
                    }
                    Home.DataContext = HomeVM;
                    Navigator.NavigationService.Navigate(Home);
                    break;
                case "MOVIES":
                    if (MOVIES == null)
                    {
                        MediaVM = new MediaViewModel(_pageName);
                        MOVIES = new Media();                        
                    }
                    MOVIES.DataContext = MediaVM;
                    Navigator.NavigationService.Navigate(MOVIES);
                    break;            
                
                case "TV SHOWS":
                    if (TV == null)
                    {
                        TVVM = new MediaViewModel(_pageName);
                        TV = new Media();                        
                    }
                    TV.DataContext = TVVM;
                    Navigator.NavigationService.Navigate(TV);
                    break;
            }
        }
        public void NavigatePage(string _pageName, string parameters, string callingPage)
        {
            _navigationStack.Add(_pageName);
            MessageBox.Show("added " + _pageName + " to stack");
            CurrentPage = _pageName;

            switch (_pageName)
            {
                case "MediaPlayer":
                    if (MediaPlayer == null)
                    {
                        MediaPlayerVM = new MediaPlayerViewModel(parameters);
                        MediaPlayer = new MediaPlayer(parameters, callingPage);                        
                    }
                    MediaPlayer.DataContext = MediaPlayerVM;
                    Navigator.NavigationService.Navigate(MediaPlayer);
                    break;            
            }
        }

    }
}
