using GalaSoft.MvvmLight.Messaging;
using HireHomeEntertainment.Singletons;
using HireHomeEntertainment.ViewModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;


namespace HireHomeEntertainment.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page1 p1;
        private Page2 p2;
        private Page1ViewModel vm1;
        private Page2ViewModel vm2;
       
        public MainWindow()
        {
            InitializeComponent();
            InitializePages();
        }

        public void InitializePages()
        {
            vm1 = new Page1ViewModel();
            vm2 = new Page2ViewModel();
            p1 = new Page1();
            p2 = new Page2(); 
        }

        private void NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            FrameworkElement element = NavigationFrame.NavigationService.Content as FrameworkElement;
        }       
        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Messenger.Default.Send<KeyEventArgs>(e);
           
            if (e.Key == Key.D1 || e.Key == Key.NumPad1)
            {
                // Go to page1                
                p1.DataContext = vm1;
                Navigator.NavigationService.Navigate(p1);               
            }
            if (e.Key == Key.D2 || e.Key == Key.NumPad2)
            {
                // Go to page2
                p2.DataContext = vm2;
                Navigator.NavigationService.Navigate(p2);               
            }
        }        
    }
}
