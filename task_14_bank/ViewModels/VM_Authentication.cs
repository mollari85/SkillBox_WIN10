using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using task_14_bank.Models.Repository;
using task_14_bank.Models.Support;
using task_14_bank.Tools;
using task_14_bank.View;
using task_14_bank.Models.Support;
using task_14_bank.Models.Types;
using System.Collections.ObjectModel;

namespace task_14_bank.ViewModels
{
    internal class VM_Authentication
    {
        public string Login { get; set; }
        IEmployeeRepository _employeeRepository;
        IClientRepository _clientRepository;
        ObservableCollection<Client> Clients;
        

        public VM_Authentication(IEmployeeRepository EmployeeRepository,IClientRepository rep)
        {
            this._employeeRepository = EmployeeRepository;
            this._clientRepository = rep;
            Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            CommandLogin = new RelayCommand(Authenticate);
        }
        private RelayCommand _commandLogin;     
        public RelayCommand CommandLogin 
        { 
            get 
            {
                return _commandLogin ?? (_commandLogin = new RelayCommand(obj => MessageBox.Show("Button Login is not working")));
             } 
            set 
            {
                _commandLogin = value;  
            } 
        }
        public void Authenticate(object ob)
        {
            if (_employeeRepository.IsEmployee(Login))
            {
                AuthenticatedAccount.AuthenticatedEmployee = _employeeRepository.GetEmployee(Login);
                OpenClientView();
                CloseThisView();
                return;
            }
            if (_clientRepository.IsClient(Login))
            {
                AuthenticatedAccount.AuthenticatedClient = _clientRepository.GetClient(Login);
                OpenClientWorkingArea();
                CloseThisView();
                return;
            }
        }

        public void OpenClientView()
        {
            ClientView window=new ClientView();
            window.Show();           
        }
        public void OpenClientWorkingArea()
        {
            ClientWorkingArea window = new ClientWorkingArea();
            window.Show();
        }
        public void CloseThisView()
        {
            foreach (Window window in Application.Current.Windows)
                if (window is AuthenticationView)
                {
                    window.Close();
                    break;
                }
        }
            
    
    }
}
