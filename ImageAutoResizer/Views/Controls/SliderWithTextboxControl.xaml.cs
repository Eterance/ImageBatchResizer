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
    /// SliderWithTextboxControl.xaml 的交互逻辑
    /// </summary>
    public partial class SliderWithTextboxControl : UserControl
    {
        public SliderWithTextboxControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", 
            typeof(string), 
            typeof(SliderWithTextboxControl),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ToolTipContentProperty = DependencyProperty.Register(
            "ToolTipContent", 
            typeof(string), 
            typeof(SliderWithTextboxControl), 
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public string ToolTipContent
        {
            get { return (string)GetValue(ToolTipContentProperty); }
            set { SetValue(ToolTipContentProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", 
            typeof(double), 
            typeof(SliderWithTextboxControl), 
            new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }


        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(SliderWithTextboxControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(SliderWithTextboxControl), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register("SmallChange", typeof(double), typeof(SliderWithTextboxControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double SmallChange
        {
            get { return (double)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }

        public static readonly DependencyProperty LargeChangeProperty =
            DependencyProperty.Register("LargeChange", typeof(double), typeof(SliderWithTextboxControl), new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double LargeChange
        {
            get { return (double)GetValue(LargeChangeProperty); }
            set { SetValue(LargeChangeProperty, value); }
        }

        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register("TextBoxWidth", typeof(double), typeof(SliderWithTextboxControl), new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double TextBoxWidth
        {
            get { return (double)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }
    }
}
