using System;
using MVVM;
using HireHomeEntertainment.View;

namespace HireHomeEntertainment.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
        }

        public String Heading
        {
            get { return _Heading; }
            set
            {
                _Heading = value;
                NotifyPropertyChanged("Heading");
            }
        } String _Heading = "My Company Logo";
    }
}
