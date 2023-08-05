using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace task_14_bank.Tools
{
    internal class ConverterToDateOnly : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly TempDate)
                return TempDate.ToDateTime(new TimeOnly());
            else
                throw new ArgumentException($"Wrong argumnet in Converter");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return (null);
            if (value is DateTime TempDate)
                return DateOnly.FromDateTime(TempDate);
            else
                throw new ArgumentException($"Wrong argumnet in Converter");

        }
    }
}
