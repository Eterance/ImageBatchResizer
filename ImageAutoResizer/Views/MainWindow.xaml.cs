using ImageBatchResizer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ImageBatchResizer.Views
{
    /// <summary>
    /// MainWIndow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var coreVM = new CoreViewModel();
            DataContext = coreVM;
        }

        private void ListBox_Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenConsole_Click(object sender, RoutedEventArgs e)
        {
            if (ConsoleWindow.SingleConsoleWindow == null)
            {
                ConsoleWindow.SingleConsoleWindow = new(DataContext as CoreViewModel)
                {
                    Owner = this,
                };
            }
            ConsoleWindow.SingleConsoleWindow.Show();
            ConsoleWindow.SingleConsoleWindow.Topmost = true;
            ConsoleWindow.SingleConsoleWindow.Focus();
            ConsoleWindow.SingleConsoleWindow.Topmost = false;
        }

        private void NavigationItem_Click(object sender, RoutedEventArgs e)
        {
            var page = new FilesPage();
            page.DataContext = DataContext;
            mainFrame.Navigate(page);
        }
    }
}
