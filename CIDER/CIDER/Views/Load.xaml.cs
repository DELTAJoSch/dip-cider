using CIDER.ViewModels;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for Load.xaml
    /// </summary>
    public partial class Load : Page
    {
        private DataProvider _dataProvider;

        public Load(DataProvider data, MainWindowViewModel main)
        {
            InitializeComponent();
            _dataProvider = data;
            LoadViewModel loadView = new LoadViewModel(_dataProvider, new CIDER.LoadIO.FolderChecker(), new CIDER.LoadIO.FolderSelector(), new CIDER.LoadIO.FileIO(), main);
            this.DataContext = loadView;
        }
    }
}