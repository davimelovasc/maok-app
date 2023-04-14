using System;
using System.Linq;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class ClearMaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new String(value?.ToString().Where(c => char.IsDigit(c)).ToArray());
        }
    }
}