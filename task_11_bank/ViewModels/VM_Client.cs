using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using task_11_bank.Models.Types;
using task_11_bank.Models;
using System.ComponentModel.DataAnnotations;
using task_11_bank.Models.Repository;
using task_11_bank.Tools;
using System.Windows;
using task_11_bank.View;
using task_11_bank.Models.Support;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task_11_bank.ViewModels
{
    internal class VM_Client:INotifyPropertyChanged
    {

        public ObservableCollection<string> ActionsLog { get; set; }

       
        /// <summary>
        /// view mode when you can only view another mode is creating user   all feild are grey
        /// </summary>
        private bool _isViewMode;
        public bool IsViewMode
        { get { return _isViewMode; } 
            set { if (_isViewMode != value) { _isViewMode = value; OnPropertyChanged(); } } }
        public string TextLog {get;}
        private Client _clinetView;
        public Client ClientView { get { return _clinetView; } set {  _clinetView = value; OnPropertyChanged(); } }
        public Employee Employee { get; set; }
        
        public ObservableCollection<Client> Clients { get; set; }
        IClientRepository _clientRepository;
        public VM_Client(IClientRepository clientRepository)
        {
            //ClientModel Model = new ClientModel();
            //ClientView = new Client();

            _clientRepository = clientRepository;         

            Clients=new ObservableCollection<Client>(_clientRepository.GetAll());
            ClientView = Clients.FirstOrDefault();
            CommandOpenAuthenticationView = new RelayCommand(OpenAuthenticationView);
            CommandNewClient = new RelayCommand(AddClient);
            CommandRemoveClient = new RelayCommand(RemoveClient);
            CommandSaveClient= new RelayCommand(SaveClient,CanSaveClient);
            
            Employee = AuthenticatedAccount.AuthenticatedEmployee; ;
            IsViewMode = true;

            ActionsLog=new ObservableCollection<string> {"HZ","HAHA"};
            CommandTest = new RelayCommand(Test);
        }
        public void CloseView()
        {
            _clientRepository.SaveToRepository(Clients);
                         
        }
        public void OpenAuthenticationView(object o)
        {
            Window window = new AuthenticationView();
            window.Show();
            CloseView();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is ClientView)
                    win.Close();
            }
        }
        public void SaveClient(object obj)
        {

            if (!ClientView.IsValidClient())
            {
                MessageBox.Show($"Client: {ClientView.Surname} {ClientView.Name} {ClientView.ThirdName} has no fulfiled items");
                return;
            }
            if (Clients.Contains(ClientView))
            {
                MessageBox.Show($"Client: {ClientView.Surname} {ClientView.Name} {ClientView.ThirdName} is already exists");
                return;
            }                       
                Clients.Add(ClientView);
                IsViewMode = true;             
                    
        }
        public bool CanSaveClient(object obj)
        {
            return (ClientView.IsValidClient() ? true : false);
             
               
        }
        public void AddClient(object obj)
        {
            IsViewMode = false;      
             ClientView = new Client();

            /*
             Client TmpClient = new Client();
             ClientView.Name = TmpClient.Name;
             ClientView.Surname = TmpClient.Surname;
             ClientView.ThirdName = TmpClient.ThirdName;
             ClientView.Phone=TmpClient.Phone;
             ClientView.SID = TmpClient.SID;
             ClientView.NID = TmpClient.NID;
            */

            /*
            _clientRepository.SaveToRepository(Clients);

            NewClientView window = new NewClientView();                   
           if (window.ShowDialog() == true)
                Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            
                Clients = new ObservableCollection<Client>(_clientRepository.GetAll());
            */



        }
        public void RemoveClient(object obj)
        {
            Clients.Remove(ClientView);
           // ClientView = Clients.FirstOrDefault();
        }
        public void Test(object obj)
        {
            MessageBox.Show($"check phone ={ClientView.IsPhone()} NID={ClientView.IsPassportNumber()}  SID={ClientView.IsPassportSerial()}  {ClientView.Name}   {ClientView.Surname}  =={ClientView.IsValidClient()}");
        }
        private RelayCommand _commandOpenAuthenticationView;
        public RelayCommand CommandOpenAuthenticationView 
        {
            get 
            {
                return _commandOpenAuthenticationView ?? (_commandOpenAuthenticationView = new RelayCommand(obj => MessageBox.Show("THere is no method in Open this view")));
            }
            set 
            { 
                _commandOpenAuthenticationView = value;
            }
        }

        private RelayCommand _commandNewClient;
        public RelayCommand CommandNewClient 
        {
            get {
                return _commandNewClient ?? new RelayCommand(obj => MessageBox.Show("Button New is not working"));
            }
            set { _commandNewClient = value; }
        }

        private RelayCommand _commandRemoveClient;
        public RelayCommand CommandRemoveClient
        {
            get { return _commandRemoveClient ?? new RelayCommand(obj => MessageBox.Show("Button Delete is not working")); }
            set { _commandRemoveClient = value; }
        }
        private RelayCommand _commandSaveClient;
        public RelayCommand CommandSaveClient
        {
            get { return _commandSaveClient ?? new RelayCommand(obj => MessageBox.Show("Button Save is not working")); }
            set { _commandSaveClient = value; }
        }
        private RelayCommand _commandTest;
        public RelayCommand CommandTest
        {
            get
            {
                return _commandTest ?? new RelayCommand(obj => MessageBox.Show("Button New is not working"));
            }
            set { _commandTest = value; }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
          
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
