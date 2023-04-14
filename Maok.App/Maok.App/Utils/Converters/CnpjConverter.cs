using System;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class CnpjConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.ToString()?.CnpjFormatted();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}