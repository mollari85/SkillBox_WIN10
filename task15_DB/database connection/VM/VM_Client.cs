using database_connection.Model;
using database_connection.Tools;
using database_connection.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace database_connection.VM
{
    internal class VM_CLient
    {
        public Client Client { get; set; }
        public bool Result { get; set; }
        public VM_CLient(Client client)
        {
            Client = client;
            Result = false;
            CommandCancel = new RelayCommand(Cancel);
            CommandOk = new RelayCommand(Ok);
        }
        public void Ok(object obj)
        {
            Result = true;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is ClientView)
                {
                    win.Close();
                    break;

                }
            }
        }
        public void Cancel(object obj)
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win is ClientView)
                { 
                    win.Close();
                    break;

                }
            }
        }
        #region command
        RelayCommand _commandOk;
        public RelayCommand CommandOk
        {
            get
            {
                return _commandOk ?? new RelayCommand(obj => MessageBox.Show("Button OK is not working"));
            }
            set { _commandOk = value; }
        }
        RelayCommand _commandCancel;
        public RelayCommand CommandCancel
        {
            get
            {
                return _commandCancel ?? new RelayCommand(obj => MessageBox.Show("Button Cancel is not working"));
            }
            set { _commandCancel = value; }
        }
#endregion
    }
}
