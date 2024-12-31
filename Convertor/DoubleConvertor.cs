using System.Globalization;
using System.Windows.Data;

namespace MinimalisticWPF.Controls.Convertor
{
    internal class DoubleConvertor : IValueConverter
    {
        public double Rate { get; set; } = 1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * Rate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / Rate;
        }
    }
}
