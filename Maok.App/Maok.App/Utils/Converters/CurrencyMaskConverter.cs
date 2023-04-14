using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class CurrencyMaskConverter : IValueConverter
    {
        private NumberFormatInfo _nfi = new CultureInfo("pt-BR").NumberFormat;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return value;

            Decimal.TryParse(value?.ToString(), out var result);

            return result.ToString("N", _nfi);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return null;

            var valueFromString = Regex.Replace(value.ToString(), @"\D", "");

            if (valueFromString.Length <= 0)
                return 0m;

            if (!long.TryParse(valueFromString, out var valueLong) || valueLong <= 0)
                return null;

            return valueLong / 100D;
        }
    }
}