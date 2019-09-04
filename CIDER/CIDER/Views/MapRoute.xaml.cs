using CIDER.ViewModels;
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

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for MapRoute.xaml
    /// </summary>
    public partial class MapRoute : Page
    {
        public MapRoute(DataProvider data)
        {
            InitializeComponent();

            MapRouteViewModel model = new MapRouteViewModel(data);
            this.DataContext = model;
        }
    }
}
