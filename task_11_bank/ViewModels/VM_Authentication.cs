using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using task_11_bank.Models.Repository;
using task_11_bank.Models.Support;
using task_11_bank.Tools;
using task_11_bank.View;
using task_11_bank.Models.Support;

namespace task_11_bank.ViewModels
{
    internal class VM_Authentication
    {
        public string Login { get; set; }
        IEmployeeRepository _employeeRepository;
        

        public VM_Authentication(IEmployeeRepository EmployeeRepository)
        {
            this._employeeRepository = EmployeeRepository;
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
            }
        }

        public void OpenClientView()
        {
            ClientView window=new ClientView();
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
