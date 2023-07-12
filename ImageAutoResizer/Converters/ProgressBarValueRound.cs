using System;
using System.Windows;
using System.Globalization;
using System.Windows.Data;

namespace ImageBatchResizer.Converters
{
    //将导航栏的选中状态化为可视状态。
    [ValueConversion(typeof(double), typeof(string))]
    class ProgressBarValueRound : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double pValue = (double)value;
            return @$"{Math.Round(pValue, 2)}%";
        }

        /// <summary>
        /// 不会被调用
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
