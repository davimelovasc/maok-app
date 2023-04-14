using System;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class NegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double valDouble)
                return -valDouble;
            if (value is int valInt)
                return -valInt;
            if (value is decimal valDecimal)
                return -valDecimal;
            if (value is long valLong)
                return -valLong;
            if (value is float valFloat)
                return -valFloat;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}