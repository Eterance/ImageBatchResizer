using System;
using System.Collections;
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
    /// ComboBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class ComboBoxControl : UserControl
    {
        public ComboBoxControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ComboBoxControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ToolTipContentProperty =
            DependencyProperty.Register("ToolTipContent", typeof(string), typeof(ComboBoxControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ToolTipContent
        {
            get { return (string)GetValue(ToolTipContentProperty); }
            set { SetValue(ToolTipContentProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ComboBoxControl), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(ComboBoxControl), new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisibilityProperty =
            DependencyProperty.Register("ToolTipVisibility", typeof(Visibility), typeof(ComboBoxControl), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Visibility ToolTipVisibility
        {
            get { return (Visibility)GetValue(ToolTipVisibilityProperty); }
            set { SetValue(ToolTipVisibilityProperty, value); }
        }

    }
}
