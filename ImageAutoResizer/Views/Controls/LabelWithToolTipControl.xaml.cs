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
    /// LabelWithToolTipControl.xaml 的交互逻辑
    /// </summary>
    public partial class LabelWithToolTipControl : UserControl
    {
        public LabelWithToolTipControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LabelWithToolTipControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ToolTipContentProperty =
            DependencyProperty.Register("ToolTipContent", typeof(string), typeof(LabelWithToolTipControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ToolTipContent
        {
            get { return (string)GetValue(ToolTipContentProperty); }
            set { SetValue(ToolTipContentProperty, value); }
        }

        public static readonly DependencyProperty ToolTipVisibilityProperty =
            DependencyProperty.Register("ToolTipVisibility", typeof(Visibility), typeof(LabelWithToolTipControl), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Visibility ToolTipVisibility
        {
            get { return (Visibility)GetValue(ToolTipVisibilityProperty); }
            set { SetValue(ToolTipVisibilityProperty, value); }
        }
    }
}
