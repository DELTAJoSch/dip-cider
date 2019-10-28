using System.Windows;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for LoadBoxWindow.xaml
    /// </summary>
    public partial class LoadBoxWindow : Window
    {
        public LoadBoxWindow()
        {
            InitializeComponent();
        }

        public void SetProgress(double progress)
        {
            LoadBar.Value = progress;
        }

        public void SetMax(double max)
        {
            LoadBar.Maximum = max;
        }
    }
}