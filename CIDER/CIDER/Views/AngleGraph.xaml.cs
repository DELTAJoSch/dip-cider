using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for AngleGraph.xaml
    /// </summary>
    public partial class AngleGraph : Page
    {
        private AngleGraphViewModel model;

        public AngleGraph(DataProvider _data)
        {
            InitializeComponent();
            model = new AngleGraphViewModel(_data);
            this.DataContext = model;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}