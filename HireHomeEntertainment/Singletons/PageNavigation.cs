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

namespace HireHomeEntertainment.Singletons
{
    public sealed class PageNavigation : ViewModelBase
    {
        private static volatile PageNavigation instance;
        private static object syncRoot = new Object();

        private Page1 p1;
        private Page2 p2;
        private MainWindow pM;
        private MediaPlayer MP;
        private Page1ViewModel vm1;
        private Page2ViewModel vm2;
        private MainWindowViewModel vmM;
        private MediaPlayerViewModel vmMP;
        
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
        }

        public void NavigatePage(string _pageName)
        {
            switch (_pageName)
            {
                case "pM":
                    if (pM == null)
                    {
                        pM = new MainWindow();
                        vmM = new MainWindowViewModel();
                    }
                    pM.DataContext = vmM;
                    Navigator.NavigationService.Navigate(pM);
                    break;
                case "p1":
                    if (p1 == null)
                    {
                        p1 = new Page1();
                        vm1 = new Page1ViewModel();
                    }
                    p1.DataContext = vm1;
                    Navigator.NavigationService.Navigate(p1);
                    break;
                case "p2":
                    if (p2 == null)
                    {
                        p2 = new Page2();
                        vm2 = new Page2ViewModel();
                    }
                    p2.DataContext = vm2;
                    Navigator.NavigationService.Navigate(p2);
                    break;
                //FUTURE PAGES
                
                //case "p4":
                //    if (p4 == null)
                //    {

                //    }
                //    break;
            }
        }
        public void NavigatePage(string _pageName, string parameters)
        {
            switch (_pageName)
            {
                case "MP":
                    if (MP == null)
                    {
                        MP = new MediaPlayer(parameters);
                        vmMP = new MediaPlayerViewModel(parameters);
                    }
                    MP.DataContext = vmMP;
                    Navigator.NavigationService.Navigate(MP);
                    break;            
            }
        }
    }
}
