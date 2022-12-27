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
using task_12_bank.ViewModels;

namespace task_12_bank.View
{
    /// <summary>
    /// Interaction logic for AuthenticationView.xaml
    /// </summary>
    public partial class AuthenticationView : Window
    {
        public AuthenticationView()
        {
            InitializeComponent();
            EmployeeRepository Employees=new EmployeeRepository();
            ClientRepository rep = new ClientRepository();
            VM_Authentication vM_Authentication = new VM_Authentication(Employees,rep);
            this.DataContext = vM_Authentication;
        }
    }
}
