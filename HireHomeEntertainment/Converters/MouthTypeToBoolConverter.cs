using System;
using System.Windows.Data;
using HireHomeEntertainment.Model;

namespace HireHomeEntertainment.Converters
{
    class MouthTypeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value == System.Convert.ToInt32(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return (Mouth.Type)System.Convert.ToInt32(parameter);
            else
                return (System.Convert.ToInt32(parameter) == 0) ? Mouth.Type.Frown : Mouth.Type.Smile;
        }
    }
}
