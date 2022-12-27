using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace task_12_bank.Tools
{
    internal class ConverterBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is bool _bool)
                    return (!_bool);
            }
            catch (Exception e) { MessageBox.Show($"Bool converter is not working {e.Message}"); }
            return (value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is bool _bool)
                    return (!_bool);
            }
            catch (Exception e) { MessageBox.Show($"Bool converterBAckis not working {e.Message}"); }
            return (value);
        }
    }
}
