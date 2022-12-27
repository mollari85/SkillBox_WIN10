using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using task_12_bank.Models.Repository;
using task_12_bank.Models.Support;
using task_12_bank.Models.Types;
using task_12_bank.ViewModels;
using task_12_bank.Models.Types.Bank_;

namespace task_12_bank.View
{
    /// <summary>
    /// Interaction logic for ClientWorkingArea.xaml
    /// </summary>
    public partial class ClientWorkingArea : Window
    {
        VM_ClientWorkingArea Area;
        public ClientWorkingArea()
        {
            InitializeComponent();

            

            Area = new VM_ClientWorkingArea(TestStatic.MyClient,BankSystem.BankSber);
            this.DataContext = Area;
          
        }
    }
}
