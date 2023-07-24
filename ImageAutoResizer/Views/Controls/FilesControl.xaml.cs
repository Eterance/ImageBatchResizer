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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageBatchResizer.Views.Controls
{
    /// <summary>
    /// FilesControl.xaml 的交互逻辑
    /// </summary>
    public partial class FilesControl : UserControl
    {
        public FilesControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BottomRowHeightProperty =
            DependencyProperty.Register("BottomRowHeight", typeof(double), typeof(FilesControl), new FrameworkPropertyMetadata(40.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double BottomRowHeight
        {
            get { return (double)GetValue(BottomRowHeightProperty); }
            set { SetValue(BottomRowHeightProperty, value); }
        }

        public static readonly DependencyProperty IsEnableEditProperty =
            DependencyProperty.Register("IsEnableEdit", typeof(bool), typeof(FilesControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsEnableEdit
        {
            get { return (bool)GetValue(IsEnableEditProperty); }
            set 
            { 
                SetValue(IsEnableEditProperty, value); 
            }
        }
    }
}
