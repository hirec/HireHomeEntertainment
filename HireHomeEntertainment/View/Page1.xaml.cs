using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using HireHomeEntertainment.Singletons;

namespace HireHomeEntertainment.View
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary> 
    public partial class Page1 : Page
    {

        private bool AllowContentModification;
 
        public Page1()
        {
            InitializeComponent();            
            AllowContentModification = false;
            Messenger.Default.Register<KeyEventArgs>(this, MainWindow_KeyDown);          
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AllowContentModification = true;           
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AllowContentModification = false;
        }   

        private void MainWindow_KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Left && AllowContentModification) 
            {               
                if (SampleCoverflow.SelectedIndex >= 1)
                {
                    SampleCoverflow.SelectedIndex--;
                    SampleCoverflowText.SelectedIndex--;
                }

            }
            if (e.Key == Key.Right && AllowContentModification)
            {
                SampleCoverflow.SelectedIndex++;
                SampleCoverflowText.SelectedIndex++;
            }
        }

        public void SampleCoverflow_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        public void SampleCoverflowText_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (AllowContentModification == true)
            {
                SetBorderStyler();
            }

        }

        private void SetBorderStyler()
        {
            switch (SampleCoverflow.SelectedIndex)
            {
                case 0:
                    CurrentSelection.Width = 150;
                    break;
                case 1:
                    CurrentSelection.Width = 150;
                    break;
                case 2:
                    CurrentSelection.Width = 200;
                    break;
                case 3:
                    CurrentSelection.Width = 225;
                    break;
            }
        }           
    }
}
