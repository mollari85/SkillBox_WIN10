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
    internal class ConverterStar : IMultiValueConverter
    {
        private string Phone;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            
            try
            {
                
                if (values[1] is bool IsPassportHidden)
                  if(IsPassportHidden == true )
                    if ( values[0] is string Text)
                    {  
                            if (!String.IsNullOrEmpty(Text))
                        {
                                Phone= Text;    
                            return (new string('*', Phone.Length));
                        }
                    }
                return (values[0]);
            }
            catch (Exception e) { MessageBox.Show("ConverterTOStar"  +e.Message); }
            return (values[0]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            try
            {

                if (value is string PhoneOrStar)
                    if (PhoneOrStar.Contains('*') | string.IsNullOrEmpty(PhoneOrStar))
                    return (new object[] {(object)Phone });
            }
            catch (Exception e) { MessageBox.Show("ConverterBackTOStar" + e.Message); }
            return (new object[] { value });
            
        }
    }
}
