using System;
using Xamarin.Forms;
using static System.Convert;

namespace Maok.App.Utils.Converters
{
    public class IsValueGreaterZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ToDouble(value) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string);
        }
    }
}