using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for Height.xaml
    /// </summary>
    public partial class Height : Page
    {
        private HeightViewModel model;

        public Height(DataProvider data)
        {
            InitializeComponent();

            model = new HeightViewModel(data);

            this.DataContext = model;
        }

        private void slValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.slValueChanged((int)slValue.Value);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}