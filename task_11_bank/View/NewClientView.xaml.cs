using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using task_11_bank.Models.Repository;
using task_11_bank.Models.Types;
using task_11_bank.ViewModels;

namespace task_11_bank.View
{
    /// <summary>
    /// Interaction logic for NewClientView.xaml
    /// </summary>
    public partial class NewClientView : Window
    {
        public NewClientView()
        {
            InitializeComponent();
            ClientRepository rep = new ClientRepository();
            this.DataContext = new VM_NewClient(rep);
        }
       
    }
}
