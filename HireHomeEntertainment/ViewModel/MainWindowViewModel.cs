using System;
using System.Windows;
using System.Windows.Input;
using MVVM;
using GalaSoft.MvvmLight.Messaging;
using HireHomeEntertainment.View;
using HireHomeEntertainment.Singletons;

namespace HireHomeEntertainment.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {        
        public MainWindowViewModel()
        {
            PageNavigation.Instance.NavigatePage("Home");          
        }       
       
    }
}
