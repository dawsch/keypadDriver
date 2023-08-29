using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace expandMenuA
{
    [ValueConversion(typeof(bool), typeof(double))]
    internal class expandBoolToHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isExpand = (bool)values[0];

            if (isExpand)
            {
                return (double)values[1] + (double)values[2];
            }
            else
            {
                return (double)values[2];
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(double), typeof(double))]
    internal class combineTwoValues : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v1 = (double)value;
            double v2 = (double)parameter;

            return v1 + v2 / Math.Pow(10, v2.ToString().Length);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
