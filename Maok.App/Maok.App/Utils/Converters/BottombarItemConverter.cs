using Maok.App.Modules.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Maok.App.Utils.Converters
{
    public class BottombarItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var itemType = (ContentItemType)value;
            var itemParameter = (ContentItemType)parameter;

            return itemType == itemParameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}