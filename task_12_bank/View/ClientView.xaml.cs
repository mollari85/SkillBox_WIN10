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
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        VM_Client vM_Client;
        public ClientView()
        {
            InitializeComponent();
            ClientRepository rep = new ClientRepository();
            vM_Client = new VM_Client(rep);
            this.DataContext = vM_Client;
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vM_Client.CloseView();  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // Window window=new AuthenticationView();
           // vM_Client.CloseView();
           // window.ShowDialog();
            //Application.Current.MainWindow = window;
        }
    }
}
