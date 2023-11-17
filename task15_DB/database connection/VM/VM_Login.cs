using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using database_connection.Tools;
using System.Windows;
using database_connection.View;
using database_connection.Model;

namespace database_connection.VM
{
    internal class VM_Login
    {
        /*
        public event EventHandler<DataLoginEventArgs> DataLoginTransfer;
        protected virtual void OnDataLoginTransfer(string login, string password)
        {
            DataLoginTransfer?.Invoke(this, new DataLoginEventArgs(login, password));
        }
        */
        IConnectionModel _IConnectionModel;
        public string Login { get; set; }
        public string Password { get; set; }
        public VM_Login(IConnectionModel IModel)
        {

            _IConnectionModel = IModel;
            CommandOk = new RelayCommand(OK);
            CommandCancel = new RelayCommand(Cancel);
        }
        public void OpenConnectionView()
        {
            //ConnectionView window = new ConnectionView();
           // window.Show();
        }
        public void CloseThisView()
        {
            MessageBox.Show($"Login is {Login} password is {Password}");
            foreach (Window window in Application.Current.Windows)
                if (window is LoginView)
                {
                    window.Close();
                    break;
                }
        }
        #region Command Function
        public void OK(object obj) 
        {
            //here is starting connection to SQL and if success closing the window
            
            if (String.IsNullOrEmpty(Login))
                throw new ArgumentException("Login can't be null or empty");
            if (String.IsNullOrEmpty(Password))
                throw new ArgumentException("Password can't be null or empty");
            _IConnectionModel.ConnectionLogin = Login;
            _IConnectionModel.ConnectionPassword = Password;
            CloseThisView();
        }
        public void Cancel(object obj)
        {
            MessageBox.Show("cancel button");
            ///here is closing the window and open old one
             CloseThisView();
            

        }
        #endregion 
        #region Commands
        RelayCommand _commandOk;
        public RelayCommand CommandOk
        {
            get
            {
                return _commandOk ?? new RelayCommand(obj => MessageBox.Show("Button ConnectionOk is not working"));
            }
            set { _commandOk = value; }
        }
        RelayCommand _commandCancel;
        public RelayCommand CommandCancel
        {
            get
            {
                return _commandCancel ?? new RelayCommand(obj => MessageBox.Show("Button ConnectionCancel is not working"));
            }
            set { _commandCancel = value; }
        }
        #endregion
    }
    public class DataLoginEventArgs : EventArgs
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public DataLoginEventArgs(string login, string password)
        {
            Login = login;
            Password=password;
        }
    }
}
