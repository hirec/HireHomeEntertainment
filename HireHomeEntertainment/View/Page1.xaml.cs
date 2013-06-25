using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HireHomeEntertainment.View
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Page_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (SampleCoverflow.SelectedIndex >= 1)
                {
                    SampleCoverflow.SelectedIndex--;
                    SampleCoverflowText.SelectedIndex--;
                }

            }
            if (e.Key == Key.Right)
            {
                SampleCoverflow.SelectedIndex++;
                SampleCoverflowText.SelectedIndex++;
            }
        }
    }
}
