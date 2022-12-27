using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace task_12_bank.Tools.NotUseUserControlsArchive_
{
    public class Converter4Char : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is string sTmp)
                    if (sTmp.Length >= 4)
                        return (sTmp.Substring(sTmp.Length - 4));
                    else return (sTmp);
            }
            catch (Exception) { MessageBox.Show("Converter Converter4Char is not working"); }
            return (value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // throw new NotImplementedException();
            return (value);
        }
    }
}
