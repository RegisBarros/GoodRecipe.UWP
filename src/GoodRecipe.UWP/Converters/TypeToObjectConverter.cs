using System;
using Windows.UI.Xaml.Data;

namespace GoodRecipe.UWP.Converters
{
    public class TypeToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ChangeType(value, targetType);
        }
    }
}
