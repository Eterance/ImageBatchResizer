using ImageBatchResizer.ViewModels;
using ImageBatchResizer.Views;
using ImageBatchResizer.Views.Controls;
using ImageBatchResizer.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Contracts;
using Wpf.Ui.Services;

namespace ImageBatchResizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // https://laurentkempe.com/2019/09/03/WPF-and-dotnet-Generic-Host-with-dotnet-Core-3-0/
        private IHost _host;

        public App()
        {
            _host = new HostBuilder().ConfigureServices((context, services) =>
            {

                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();

                services.AddSingleton<MainWindow>();
                //services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<CoreViewModel>();
                //services.AddSingleton<FilePage>();
                //services.AddSingleton<TotalParametersPage>();
            }).Build();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync(); 
            var mainWindow = _host.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
    }
}
