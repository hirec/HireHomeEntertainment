using System.Windows;
using System.Windows.Navigation;

namespace HireHomeEntertainment.Singletons
{
    public sealed class Navigator
    {
        private static readonly Navigator instance = new Navigator();
        private Navigator() { }
        public static NavigationService NavigationService { get; set; }
        public static void Cancel()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                App.Current.Shutdown(1);
        }
    }
}
