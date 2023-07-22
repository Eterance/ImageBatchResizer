using CommunityToolkit.Mvvm.ComponentModel;
using ImageBatchResizer.Views;
using ImageBatchResizer.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Media;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace ImageBatchResizer.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ICollection<object> _navMenuItems;
        [ObservableProperty]
        private ICollection<object> _footerMenuItems;
        public CoreViewModel CoreViewModel { get; }
        public MainWindowViewModel(CoreViewModel coreViewModel)
        {
            CoreViewModel = coreViewModel;
            FontFamily font = new FontFamily(new Uri("pack://application:,,,/"), "./Resources/Fonts/#Segoe Fluent Icons");
            _navMenuItems = new ObservableCollection<object>
            {
                new NavigationViewItem("打开文件", typeof(FilePage))
                {
                    Icon = new FontIcon() 
                    { 
                        Glyph = "&#xe8e5;",
                        FontFamily = font,
                    },
                },
                new NavigationViewItem("全部参数", typeof(TotalParametersPage))
                {
                    Icon = new FontIcon()
                    {
                        Glyph = "&#xf246;",
                        FontFamily = font,
                    },
                },
            };

            _footerMenuItems = new ObservableCollection<object>()
            {
                new NavigationViewItem
                {
                    Content = "Settings",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    //TargetPageType = typeof(SettingsPage)
                }
            };
        }
    }
}
