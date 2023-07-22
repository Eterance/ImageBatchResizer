using ImageBatchResizer.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;

namespace ImageBatchResizer.Views
{
    /// <summary>
    /// MainWIndow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    { 
        public MainWindow(
            CoreViewModel vm, 
            INavigationService navigationService,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
            )
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);

            DataContext = vm;
            InitializeComponent();

            //snackbarService.SetSnackbarPresenter(RootSnackbarPresenter);
            //navigationService.SetNavigationControl(RootNavigation);
            contentDialogService.SetContentPresenter(RootContentDialog);
            //var coreVM = new CoreViewModel();
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
