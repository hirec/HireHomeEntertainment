using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using HireHomeEntertainment.Singletons;

namespace HireHomeEntertainment.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary> 
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();            
        }

        private void onPageLoaded(object sender, RoutedEventArgs e)
        {
            PluginCoverFlow.SelectedIndex = 1;
        }    
    }
}
