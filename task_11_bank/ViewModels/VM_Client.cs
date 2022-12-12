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
using System.Diagnostics.Metrics;

namespace task_11_bank.ViewModels
{
    internal class VM_Client:INotifyPropertyChanged
    {
        private bool _editing; 
        public ObservableCollection<LogData> ActionsLog { get; set; }

        private bool _isLog;
        /// <summary>
        /// Is Employee's actions logging
        /// </summary>
        public bool IsLog
        {
            get { return _isLog; }
            set { if (_isLog != value) { _isLog = value; OnPropertyChanged(); } }
        }
 
        /// <summary>
        /// view mode when you can only view another mode is creating user   all feild are grey
        /// </summary>
        private bool _isViewMode;
        public bool IsViewMode
        { get { return _isViewMode; } 
            set { if (_isViewMode != value) { _isViewMode = value; OnPropertyChanged(); } } }

        private Client _oldValueClientView;
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
            
            Clients =new ObservableCollection<Client>(_clientRepository.GetAll());
            ClientView = Clients.FirstOrDefault();
            CommandOpenAuthenticationView = new RelayCommand(OpenAuthenticationView);
            CommandNewClient = new RelayCommand(AddClient);
            CommandRemoveClient = new RelayCommand(RemoveClient);
            CommandSaveClient= new RelayCommand(SaveClient,CanSaveClient);
            CommandEdit = new RelayCommand(Edit);
            CommandCancel = new RelayCommand(Cancel);
            CommandSort = new RelayCommand(Sort);
            CommandSortDesc = new RelayCommand(SortDesc);

            Employee = AuthenticatedAccount.AuthenticatedEmployee; ;
            IsViewMode = true;
            IsLog = false;

           
            
            ActionsLog = new ObservableCollection<LogData>();
            ActionsLog.Add(new LogData(Employee, LogAction.Edit, ClientView, ClientView));
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
            if (IsLog)
                if (_editing)
                    ActionsLog.Add(new LogData(Employee, LogAction.Edit, _oldValueClientView, ClientView));
                else
                    ActionsLog.Add(new LogData(Employee, LogAction.Create, null, ClientView));
            if (_editing)
            {
                Clients[Clients.IndexOf(_oldValueClientView)] = ClientView;
            }
            else
                Clients.Add(ClientView);

            IsViewMode = true;             
                    
        }
        public bool CanSaveClient(object obj)
        {
            if (ClientView == null)
                return false;
            return (ClientView.IsValidClient() ? true : false);
             
               
        }
        public void AddClient(object obj)
        {
            IsViewMode = false;
            if (IsLog)_oldValueClientView = null;
            ClientView = new Client();
            


        }
        public void RemoveClient(object obj)
        {
            if(IsLog) ActionsLog.Add(new LogData(Employee, LogAction.Create, ClientView, null));
            Clients.Remove(ClientView);
           // ClientView = Clients.FirstOrDefault();
        }
        public void Cancel(object obj)
        {
            IsViewMode = true;
            _editing = false;
        }
        public void Edit(object obj)
        {
            if (ClientView is null)
            {
                MessageBox.Show("Select an item in the list");
                return;
            }
            _editing = true;
            IsViewMode = false;
            _oldValueClientView = ClientView;
            ClientView = new Client(ClientView);


        }
        public void Sort(object obj)
        {
            List<Client> tmpClients = new List<Client>(Clients);
            tmpClients.Sort();
            for (int i = 0; i < tmpClients.Count; i++)
                Clients.Move(Clients.IndexOf(tmpClients[i]),i);
        }
        public void SortDesc(object obj)
        {
            List<Client> tmpClients = new List<Client>(Clients);
            tmpClients.Sort();
            tmpClients.Reverse();
            for (int i = 0; i < tmpClients.Count; i++)
                Clients.Move(Clients.IndexOf(tmpClients[i]), i);
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
        private RelayCommand _commandCancel;
        public RelayCommand CommandCancel
        {
            get
            {
                return _commandCancel ?? new RelayCommand(obj => MessageBox.Show("Button Cancel is not working"));
            }
            set { _commandCancel = value; }
        }
        private RelayCommand _commandEdit;
        public RelayCommand CommandEdit
        {
            get
            {
                return _commandEdit ?? new RelayCommand(obj => MessageBox.Show("Button Edit is not working"));
            }
            set { _commandEdit = value; }
        }
        private RelayCommand _commandSort;
        public RelayCommand CommandSort
        {
            get
            {
                return _commandSort ?? new RelayCommand(obj => MessageBox.Show("Button Sort is not working"));
            }
            set { _commandSort = value; }
        }
        private RelayCommand _commandSortDesc;
        public RelayCommand CommandSortDesc
        {
            get
            {
                return _commandSortDesc ?? new RelayCommand(obj => MessageBox.Show("Button SortDesc is not working"));
            }
            set { _commandSortDesc = value; }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
          
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
