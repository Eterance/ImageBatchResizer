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
    /// SwitchableTwoParametersControl.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchableTwoParametersControl : UserControl
    {
        public SwitchableTwoParametersControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ToolTipContentProperty =
            DependencyProperty.Register("ToolTipContent", typeof(string), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ToolTipContent
        {
            get { return (string)GetValue(ToolTipContentProperty); }
            set { SetValue(ToolTipContentProperty, value); }
        }

        public static readonly DependencyProperty LeftLableTextProperty =
            DependencyProperty.Register("LeftLableText", typeof(string), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string LeftLableText
        {
            get { return (string)GetValue(LeftLableTextProperty); }
            set { SetValue(LeftLableTextProperty, value); }
        }

        public static readonly DependencyProperty RightLableTextProperty =
            DependencyProperty.Register("RightLableText", typeof(string), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string RightLableText
        {
            get { return (string)GetValue(RightLableTextProperty); }
            set { SetValue(RightLableTextProperty, value); }
        }

        public static readonly DependencyProperty LeftPlaceHolderProperty =
            DependencyProperty.Register("LeftPlaceHolder", typeof(string), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string LeftPlaceHolder
        {
            get { return (string)GetValue(LeftPlaceHolderProperty); }
            set { SetValue(LeftPlaceHolderProperty, value); }
        }

        public static readonly DependencyProperty RightPlaceHolderProperty =
            DependencyProperty.Register("RightPlaceHolder", typeof(string), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string RightPlaceHolder
        {
            get { return (string)GetValue(RightPlaceHolderProperty); }
            set { SetValue(RightPlaceHolderProperty, value); }
        }

        public static readonly DependencyProperty LeftValueProperty =
            DependencyProperty.Register("LeftValue", typeof(double), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double LeftValue
        {
            get { return (double)GetValue(LeftValueProperty); }
            set { SetValue(LeftValueProperty, value); }
        }

        public static readonly DependencyProperty RightValueProperty =
            DependencyProperty.Register("RightValue", typeof(double), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double RightValue
        {
            get { return (double)GetValue(RightValueProperty); }
            set { SetValue(RightValueProperty, value); }
        }

        public static readonly DependencyProperty LeftDefaultValueProperty =
            DependencyProperty.Register("LeftDefaultValue", typeof(double), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double LeftDefaultValue
        {
            get { return (double)GetValue(LeftDefaultValueProperty); }
            set { SetValue(LeftDefaultValueProperty, value); }
        }

        public static readonly DependencyProperty RightDefaultValueProperty =
            DependencyProperty.Register("RightDefaultValue", typeof(double), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double RightDefaultValue
        {
            get { return (double)GetValue(RightDefaultValueProperty); }
            set { SetValue(RightDefaultValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set 
            { 
                SetValue(IsCheckedProperty, value); 
            }
        }

        public static readonly DependencyProperty MaxDecimalPlacesProperty =
            DependencyProperty.Register("MaxDecimalPlaces", typeof(int), typeof(SwitchableTwoParametersControl), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int MaxDecimalPlaces
        {
            get { return (int)GetValue(MaxDecimalPlacesProperty); }
            set { SetValue(MaxDecimalPlacesProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LeftValue = LeftDefaultValue;
            RightValue = RightDefaultValue;
        }
    }
}
