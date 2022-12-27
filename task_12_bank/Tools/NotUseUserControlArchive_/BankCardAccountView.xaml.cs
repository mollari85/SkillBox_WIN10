using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using task_12_bank.Tools.NotUseUserControlsArchive_;
namespace task_12_bank.Tools.NotUseUserControlsArchive_
{
    /// <summary>
    /// Interaction logic for BankCardAccountView.xaml
    /// </summary>
    public partial class BankCardAccountView : UserControl
    {
        public Converter4Char MyConverter4Char;
        public string MyLabel;
        public BankCardAccountView()
        {
            //uncomment this
           // InitializeComponent();

            Test = "Test";
            TestString = "StaticText";
        }
        const int viewLength = 4;
        
        //public Decimal Amount { get; set; }
        /*string _Last4CardNumbers;
        public string Last4CardNumbers { get { return (_Last4CardNumbers); }
            set { if (!string.IsNullOrEmpty(value) && value.Length >= viewLength) _Last4CardNumbers = value.Substring(value.Length - viewLength); } }
        */

        //public string AccountName { get; set; }


        /*
        public Decimal Amount
        {
            get { return (Decimal)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(Decimal), typeof(BankCardAccountView), new PropertyMetadata(0.0));
        */
        public static string TestString { get; set; }
        public string Test { get; set; }
        public class TestClass { string Test; }
        public TestClass TestClassProperty { get; set; }
        public Converter4Char Converter4 { get; set; }

        public String Amount
        {
            get { return (String)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(String), typeof(BankCardAccountView), new PropertyMetadata(""));


        public string Last4CardNumbers
        {
            get { return (string)GetValue(Last4CardNumbersProperty); }
            set { SetValue(Last4CardNumbersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Last4CardNumbers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Last4CardNumbersProperty =
            DependencyProperty.Register("Last4CardNumbers", typeof(string), typeof(BankCardAccountView), new PropertyMetadata(""));


        public string LastTestNumbers
        {
            get {   return (string)GetValue(LastTestNumbersProperty); }
            set {  SetValue(LastTestNumbersProperty,value);
            }
        }
        static FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("", 
            new PropertyChangedCallback(OnDPValueChanged),
            new CoerceValueCallback(CoerceValue));
        private static void OnDPValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           TestString+=($"ValueChanged new value is {e.NewValue.ToString()}");
        }
        private static object CoerceValue(DependencyObject d, object value)
        {
            TestString +=($"CoerceValue value is {value.ToString()}");
            if (value is string sTmp)
                if (sTmp.Length >= viewLength)
                {
                    sTmp = sTmp.Substring(sTmp.Length - viewLength);
                    return sTmp;
                }
            
            return (value);
        }

        // Using a DependencyProperty as the backing store for Last4CardNumbers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastTestNumbersProperty =
            DependencyProperty.Register("LastTestNumbers", typeof(string), typeof(BankCardAccountView), metadata);






       /* static BankCardAccountView()
        {

            Last4CardNumbersProperty.OverrideMetadata(
                typeof(BankCardAccountView),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender)
            );
        }
       */

        //propdp tab tab


        public string AccountName
        {
            get { return (string)GetValue(AccountNameProperty); }
            set { SetValue(AccountNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AccountNameProperty =
            DependencyProperty.Register("AccountName", typeof(string), typeof(BankCardAccountView), new PropertyMetadata(""));


        public class Converter4LastChar : MarkupExtension, IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    if (value is string sTmp)
                        return (sTmp.Substring(sTmp.Length - 4));
                }
                catch (Exception) { MessageBox.Show("Converter Converter4Char is not working"); }
                return (value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                // throw new NotImplementedException();
                return (value);
            }
            public override object ProvideValue(IServiceProvider serviceProvider)
            {
                return this;
            }
        }
    }
}
