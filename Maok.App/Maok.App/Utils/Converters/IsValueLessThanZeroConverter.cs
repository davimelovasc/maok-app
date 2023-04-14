using System;
using Xamarin.Forms;
using static System.Convert;

namespace Maok.App.Utils.Converters
{
    public class IsValueLessThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ToDouble(value) < 0 ? 0 : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string);
        }
    }
}