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

        /// <summary>
        /// This is the constructor for the Load page
        /// </summary>
        /// <param name="data">A DataProvider object to store the data in</param>
        /// <param name="main">An instance of the MainWindowViewModel</param>
        public Load(DataProvider data, MainWindowViewModel main)
        {
            InitializeComponent();
            _dataProvider = data;
            LoadViewModel loadView = new LoadViewModel(_dataProvider, new CIDER.LoadIO.FolderChecker(), new CIDER.LoadIO.FolderSelector(), new CIDER.LoadIO.FileIO(), main);
            this.DataContext = loadView;
        }
    }
}