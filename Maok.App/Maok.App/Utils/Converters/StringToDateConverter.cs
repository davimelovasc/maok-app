using System;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class StringToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as string)?.ToDate();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime date)
                return date.ToShortDateString();

            return string.Empty;
        }
    }
}