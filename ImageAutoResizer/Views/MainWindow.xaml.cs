using ImageBatchResizer.ViewModels;
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

namespace ImageBatchResizer.Views
{
    /// <summary>
    /// MainWIndow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
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
    }
}
