using System.Windows;
using System.Windows.Navigation;

namespace HireHomeEntertainment.Singletons
{
    public sealed class Navigator
    {
        private static readonly Navigator instance = new Navigator();
        private Navigator() { }
        public static NavigationService NavigationService { get; set; }        
    }
}
