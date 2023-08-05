using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace task_14_bank.Tools
{
    internal class ConverterStarV:IValueConverter
    {
        private string RawText;


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RawText = (string)value;
            try
            {
                if (parameter is bool ConvertToStar1)
                 // var  ConvertToStar1 = (int)parameter;
                if (parameter is bool ConvertToStar)
                    if (ConvertToStar == true)
                        if (value is string Text)
                        {
                            if (!String.IsNullOrEmpty(Text))
                            {
                                return (new string('*', Text.Length));
                            }
                        }
                return (value);
            }
            catch (Exception e) { MessageBox.Show("ConverterTOStar" + e.Message); }
            return (value);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {

                if (parameter is bool ConvertToStar)
                    if (ConvertToStar == true)
                        return (new object[] { RawText });
            }
            catch (Exception e) { MessageBox.Show("ConverterTOStar" + e.Message); }
            return (value);
        }
    }
}
