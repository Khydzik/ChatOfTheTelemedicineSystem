using System;
using System.Globalization;
using System.Windows.Data;

namespace GraduateWork.Client.Wpf.Converter
{
    public class ArithmeticValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double currentWidth = (double) value;
            double val = double.Parse(parameter.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture);
            return currentWidth * val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
