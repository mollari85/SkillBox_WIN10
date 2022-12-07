using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using task_11_bank.Models.Support;
using task_11_bank.Tools;
using task_11_bank.Models.Types;
using System.Windows.Automation.Peers;
using System.Collections.ObjectModel;
using task_11_bank.Models.Repository;
using task_11_bank.View;

namespace task_11_bank.ViewModels
{
    internal class VM_NewClient
    {

        /*  private Client _newClient;
          public Client NewClient {
              get { return (_newClient); }
              set { _newClient = value; } 
          }
        */
        ObservableCollection<Client> _clients;
        IClientRepository _clientRepository;
      public Client NewClient { get; set; }
       public VM_NewClient(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
            _clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            _commandSave = new RelayCommand(Save,CanSave);
            NewClient = new Client(name:"Denis",surname:"Melnikov",thirdName:"Ivan",phone:"79162172122",sID:"1234",nID:"123456");
        }
        private void Save(object obj)
        {

            _clients.Add(NewClient);
            _clientRepository.SaveToRepository(_clients);
            foreach (Window win in Application.Current.Windows)
                if (win is NewClientView)
                {
                    win.DialogResult = true;
                    win.Close();
                    break;
                }
            
            
        }
        private bool CanSave(object obj)
        {
            var user = _clientRepository.GetAll().FirstOrDefault(x => x.SID == NewClient.SID & x.NID == NewClient.NID);
            if (NewClient.IsPhone() & NewClient.IsPassportSerial() & NewClient.IsPassportNumber() & (user==null))
                return true;
            return false;
        }
        private RelayCommand _commandSave;
        public RelayCommand CommandSave
        {
            get { return _commandSave ?? new RelayCommand(obj => MessageBox.Show("Button Save is not working")); }
            set { _commandSave = value; }
        }
     
    }
}
