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
        public MainWindow()
        {
            InitializeComponent();           
        }      
        private void NavigationFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //Sets the framwork element to inject pages into this area with pagenavigation class
            FrameworkElement element = NavigationFrame.NavigationService.Content as FrameworkElement;
        }       
        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {         
            //Sends Keypress message to any viewmodel that registers to receive messsage
            //EX. to REGISTER  Messenger.Default.Register<KeyEventArgs>(this, MainWindow_KeyDown);   
            //EX. Funtion   private void MainWindow_KeyDown(KeyEventArgs e)
            Messenger.Default.Send<KeyEventArgs>(e);          
        }        
    }
}
