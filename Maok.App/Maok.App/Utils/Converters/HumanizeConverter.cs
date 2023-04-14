using Humanizer;
using System;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class HumanizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Enum)value).Humanize();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}