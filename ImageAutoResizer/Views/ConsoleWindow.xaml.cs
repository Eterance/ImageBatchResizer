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
    /// ConsoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public static ConsoleWindow? SingleConsoleWindow { get; set; }
        public ConsoleWindow(CoreViewModel coreVM)
        {
            InitializeComponent();
            DataContext = coreVM;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).ScrollToEnd();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SingleConsoleWindow = null;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (Owner != null && Owner.IsVisible)
            {
                Owner.Topmost = true;
                Owner.Focus();
                Owner.Topmost = false;
            }
        }
    }
}
