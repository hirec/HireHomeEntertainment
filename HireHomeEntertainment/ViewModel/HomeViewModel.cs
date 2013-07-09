using GalaSoft.MvvmLight.Messaging;
using MVVM;
using System.Windows;
using System.Windows.Input;
using HireHomeEntertainment.Singletons;

namespace HireHomeEntertainment.ViewModel
{
    class HomeViewModel : ViewModelBase
    {
        #region Private Variables
        #endregion

        #region Public Variables

        public int selectedPluginIndex
        {
            get { return _selectedPluginIndex; }
            set
            {
                _selectedPluginIndex = value;
                SetBorderStyler();
                NotifyPropertyChanged("selectedPluginIndex");
            }
        } int _selectedPluginIndex;

        public string selectedPluginName
        {
            get { return _selectedPluginName; }
            set
            {
                _selectedPluginName = value;
                _selectedPluginName = _selectedPluginName.Remove(0, 31); //Removes everything but the name
                NotifyPropertyChanged("selectedPluginName");
            }
        } string _selectedPluginName;

         public int CurrentSelection
        {
            get { return _CurrentSelection; }
            set
            {
                _CurrentSelection = value;
                NotifyPropertyChanged("CurrentSelection");
            }
        } int _CurrentSelection;

        #endregion

        #region Constructor

         public HomeViewModel()
        {
            Messenger.Default.Register<KeyEventArgs>(this, MainWindow_KeyDown);
            selectedPluginIndex = 1;
            CurrentSelection = 150;
        }

        #endregion

        private void MainWindow_KeyDown(KeyEventArgs e)
        {
            if (PageNavigation.CurrentPage == "Home")
            {
                if (e.Key == Key.Left)
                {
                    if (selectedPluginIndex >= 1)
                    {
                        selectedPluginIndex--;
                    }

                }
                if (e.Key == Key.Right)
                {
                    selectedPluginIndex++;
                }

                if (e.Key == Key.Enter)
                {
                    PageNavigation.Instance.NavigatePage(selectedPluginName);
                }
            }
        }

        private void SetBorderStyler()
        {
            switch (selectedPluginIndex)
            {
                case 0:
                    CurrentSelection = 150;
                    break;
                case 1:
                    CurrentSelection = 150;
                    break;
                case 2:
                    CurrentSelection = 200;
                    break;
                case 3:
                    CurrentSelection = 225;
                    break;
            }
        }                            
    }
}
