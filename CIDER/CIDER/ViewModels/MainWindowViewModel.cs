using CIDER.MVVMBase;
using CIDER.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CIDER.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            frame.Navigate(about);
        }

        private Frame _frame;
        private Uri about = new Uri("Views/About.xaml", UriKind.Relative);
        private Uri angleGraph = new Uri("Views/AngleGraph.xaml", UriKind.Relative);
        private Uri velocityGraph = new Uri("Views/VelocityGraph.xaml", UriKind.Relative);
        private Uri load = new Uri("Views/Load.xaml", UriKind.Relative);
        private Uri mapTimed = new Uri("Views/MapTimed.xaml", UriKind.Relative);
        private Uri mapRoute = new Uri("Views/MapRoute.xaml", UriKind.Relative);
        private Uri angleTimed = new Uri("Views/AngleTimed.xaml", UriKind.Relative);
        private Uri velocityTimed = new Uri("Views/VelocityTimed.xaml", UriKind.Relative);
    }
}
