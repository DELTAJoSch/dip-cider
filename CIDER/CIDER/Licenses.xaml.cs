using CIDER.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace CIDER
{
    /// <summary>
    /// This is the License Window.
    /// The purpose of this Window is to provide a way to show the user all the licenses and handle all the license-agreement related issues.
    /// </summary>
    public partial class Licenses : MetroWindow
    {
        private LicensesViewModel model;

        /// <summary>
        /// This is the constructor for the License Window
        /// The DataContext is set here
        /// </summary>
        public Licenses()
        {
            InitializeComponent();

            model = new LicensesViewModel(new LicenseWriter(new FileReader()));
            this.DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.SaveAcceptAgreement();
            this.Close();
        }
    }
}
