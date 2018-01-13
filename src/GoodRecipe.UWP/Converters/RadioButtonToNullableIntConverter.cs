using System;
using Windows.UI.Xaml.Data;

namespace GoodRecipe.UWP.Converters
{
    public class RadioButtonToNullableIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null && value != null)
            {
                return parameter.ToString() == value.ToString();
            }

            return object.Equals(parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var radioButtonValue = parameter as string;
            var isChecked = System.Convert.ToBoolean(value);
            return isChecked ? System.Convert.ToInt32(radioButtonValue) : default(int?);
        }
    }
}
