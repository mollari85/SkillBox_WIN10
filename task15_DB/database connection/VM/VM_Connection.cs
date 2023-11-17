using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using database_connection.Model;
using database_connection.Tools;
using database_connection.View;
using database_connection;
using System.Data;
using System.Collections.ObjectModel;

namespace database_connection.VM
{

    internal class VM_Connection: INotifyPropertyChanged
    {

        internal IRepository<Client> iReposSQL;
        internal SQLClientRep ModelSQL;
        public ObservableCollection<Client> dbClient { get; set; }
        IConnectionModel _IConnectionModel;
        public Client SelectedClient { get; set; }


        internal SQLGoodRep ModelSQLGood;
        internal IRepository<Good> iReposSQLGood;
        public ObservableCollection<Good> dbGood { get; set; }
        public Good SelectedGood { get; set; }


        public ObservableCollection<SelectedClientGood> dbClientGood{get;set;}

        bool tabSelectedView;
        public bool TabSelectedView { get { return (tabSelectedView); } set { tabSelectedView = value; OnPropertyChanged("TabSelectedView"); } }
        bool tabSelectedGood;
        public bool TabSelectedGood { get { return (tabSelectedGood); } set { tabSelectedGood = value; OnPropertyChanged("TabSelectedGood"); } }
        bool tabSelectedClient;
        public bool TabSelectedClient { get { return (tabSelectedClient); } set { tabSelectedClient = value; OnPropertyChanged("TabSelectedClient"); } }


        public VM_Connection()
        {

            tabSelectedGood = true;
            dbClient = new ObservableCollection<Client>();
            dbGood = new ObservableCollection<Good>();
            dbClientGood = new ObservableCollection<SelectedClientGood>();

            CommandConnectionAccess =new RelayCommand(ConnectionAccess);
            CommandConnectionSQL = new RelayCommand(ConnectionSQL);
            CommandDelete = new RelayCommand(Delete,CanDelete);
            CommandUpdate = new RelayCommand(Update,CanUpdate);
            CommandCreate = new RelayCommand(Create);
            CommandDeleteGood = new RelayCommand(DeleteGood, CanDeleteGood);
            CommandUpdateGood = new RelayCommand(UpdateGood, CanUpdateGood);
            CommandCreateGood = new RelayCommand(CreateGood);
            CommandView = new RelayCommand(View);

            ModelSQL = new SQLClientRep();
            iReposSQL = ModelSQL;
            ModelSQLGood=new SQLGoodRep();
            iReposSQLGood = ModelSQLGood;


        }

        public void OpenLoginView()
        {
            LoginView window = new LoginView();
           VM_Login VMLogin = new VM_Login(_IConnectionModel);
            foreach (Window tmpWindow in Application.Current.Windows)
            {
                if (tmpWindow is MainWindow)
                {
                    window.Owner = tmpWindow;
                    window.DataContext = VMLogin;
                }
            }

            // window.Show();
            window.ShowDialog();
          //  MessageBox.Show($"Login is {_IConnectionModel.ConnectionLogin} password is {_IConnectionModel.ConnectionPassword} ");
        }
        public void CloseThisView()
        {
            foreach (Window window in Application.Current.Windows)
                if (window is LoginView)
                {
                    window.Close();
                    break;
                }
        }
        #region tools
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region methods for command
        public void ConnectionAccess(object obj) 
        {
            /*  // MessageBox.Show("Access Connection");
              IEnumerable<Client> dbClientTemp;
              dbClientTemp = (IEnumerable<Client>)(_IConnectionModel.GetClientsSQL());
              dbClient = new ObservableCollection<Client>(dbClientTemp);
             // Collection= new ObservableCollection<Client>(_IConnectionModel.GetClientsSQL());



            //  OpenLoginView();
  */
            updateDBClient();
            TabSelectedClient = true;




        }
        public void ConnectionSQL(object obj)
        {
           // OpenLoginView();
            UpdateDBGood();
            TabSelectedGood = true;
        }
        private void updateDBClient()
        {
            dbClient.Clear();
            // foreach (var i in (ModelSQL.GetItemList()))
            foreach (var i in (iReposSQL.GetItemList()))
                dbClient.Add(i);
        }
   
        
        public void Delete(object obj)
        {
            if (SelectedClient != null)
            {
                iReposSQL.Delete(SelectedClient.ID);
                updateDBClient();
            }
        }
        public bool CanDelete(object obj)
        {
            return SelectedClient != null ? true :false;

        }
        public void Update(object obj)
        {
              ClientView WindowClient = new ClientView();
               VM_CLient VM_WindowClient=new VM_CLient(SelectedClient);
            WindowClient.DataContext = VM_WindowClient;
            WindowClient.ShowDialog();
            if (VM_WindowClient.Result == true)
                iReposSQL.Update(SelectedClient);
                updateDBClient();


        }
        public bool CanUpdate(object obj)
        {
            return SelectedClient != null ? true : false;

        }
        public void Create(object obj)
        {
            Client NewClient = new Client();
            ClientView WindowClient = new ClientView();       
            VM_CLient VM_WindowClient = new VM_CLient(NewClient);
            WindowClient.DataContext = VM_WindowClient;
            WindowClient.ShowDialog();
            if (VM_WindowClient.Result == true)
            {
                iReposSQL.Create(NewClient);
                updateDBClient();
            }


        }
        #region GoodMethods
        private void UpdateDBGood()
        {
            dbGood.Clear();
            foreach (var i in (iReposSQLGood.GetItemList()))
                dbGood.Add(i);
        }
        public void DeleteGood(object obj)
        {
            if (SelectedGood != null)
            {
                iReposSQLGood.Delete(SelectedGood.ID);
                UpdateDBGood();
            }
        }
        public bool CanDeleteGood(object obj)
        {
            return SelectedGood != null ? true : false;

        }
        public void UpdateGood(object obj)
        {
            GoodView WindowGood = new GoodView();
            VM_Good VM_WindowGood = new VM_Good(SelectedGood);
            WindowGood.DataContext = VM_WindowGood;
            WindowGood.ShowDialog();
            if (VM_WindowGood.Result == true)
                iReposSQLGood.Update(SelectedGood);
            UpdateDBGood();


        }
        public bool CanUpdateGood(object obj)
        {
            return SelectedGood != null ? true : false;

        }
        public void CreateGood(object obj)
        {
            Good NewGood = new Good();
            GoodView WindowGood = new GoodView();
            VM_Good VM_WindowGood = new VM_Good(NewGood);
            WindowGood.DataContext = VM_WindowGood;
            WindowGood.ShowDialog();
            if (VM_WindowGood.Result == true)
            {
                iReposSQLGood.Create(NewGood);
                UpdateDBGood();
            }


        }
        #endregion

        #endregion
        #region Jeneral Methods
        public void View(object obj)
        {
            TabSelectedView = true;
          //  updateDBClient();
            UpdateDBGood();
            //SelectedDBGood.Clear();
            //SelectedDBClient.Clear();
            if (SelectedClient != null)
            {
               // var ddd = dbGood.Where(x => x.Mail.TrimEnd() == SelectedClient.Mail.TrimEnd());
                var ddd2 = dbGood.Join(dbClient.Where(w => w.Mail == SelectedClient.Mail),
                    x => x.Mail,
                    y => y.Mail,
                    (x, y) => new { Name = x.Name, Mail = x.Mail, Code = x.Code, 
                        GoodName = y.Name, Surname = y.Surname, SecondName=y.SecondName, PhoneNumber=y.PhoneNumber });
                dbClientGood.Clear();
                foreach (var x in ddd2)
              
                    dbClientGood.Add(new SelectedClientGood(x.Name,x.Surname,x.SecondName,x.PhoneNumber,x.Mail,x.Code,x.GoodName));
                /*
                foreach (var good in dbGood.Where(x => x.Mail.TrimEnd() == SelectedClient.Mail.TrimEnd()))
                {
                    SelectedDBClient.Add(dbClient.FirstOrDefault(x=>x.Mail==SelectedClient.Mail));
                    SelectedDBGood.Add(good);
                }
                */
            }


        }
        #endregion
        #region Commands
        RelayCommand _commandConnectionAccess;
        public RelayCommand CommandConnectionAccess
        {
            get
            {
                return _commandConnectionAccess ?? new RelayCommand(obj => MessageBox.Show("Button ConnectionAccess is not working"));
            }
            set { _commandConnectionAccess = value; }
        }

        RelayCommand _commandConnectionSQL;
        public RelayCommand CommandConnectionSQL
        {
            get
            {
                return _commandConnectionSQL ?? new RelayCommand(obj => MessageBox.Show("Button ConnectionSQL is not working"));
            }
            set { _commandConnectionSQL = value; }
        }
        RelayCommand _commandUpdate;
        public RelayCommand CommandUpdate
        {
            get
            {
                return _commandUpdate ?? new RelayCommand(obj => MessageBox.Show("Button Update is not working"));
            }
            set { _commandUpdate = value; }
        }
        RelayCommand _commandDelete;
        public RelayCommand CommandDelete
        {
            get
            {
                return _commandDelete ?? new RelayCommand(obj => MessageBox.Show("Button Delete is not working"));
            }
            set { _commandDelete = value; }
        }
        RelayCommand _commandCreate;
        public RelayCommand CommandCreate
        {
            get
            {
                return _commandCreate ?? new RelayCommand(obj => MessageBox.Show("Button Create is not working"));
            }
            set { _commandCreate = value; }
        }
        RelayCommand _commandUpdateGood;
        public RelayCommand CommandUpdateGood
        {
            get
            {
                return _commandUpdateGood ?? new RelayCommand(obj => MessageBox.Show("Button Update is not working"));
            }
            set { _commandUpdateGood = value; }
        }
        RelayCommand _commandDeleteGood;
        public RelayCommand CommandDeleteGood
        {
            get
            {
                return _commandDeleteGood ?? new RelayCommand(obj => MessageBox.Show("Button Delete is not working"));
            }
            set { _commandDeleteGood = value; }
        }
        RelayCommand _commandCreateGood;
        public RelayCommand CommandCreateGood
        {
            get
            {
                return _commandCreateGood ?? new RelayCommand(obj => MessageBox.Show("Button Create is not working"));
            }
            set { _commandCreateGood = value; }
        }
        RelayCommand _commandView;
        public RelayCommand CommandView
        {
            get
            {
                return _commandView ?? new RelayCommand(obj => MessageBox.Show("Button View is not working"));
            }
            set { _commandView = value; }
        }
        #endregion
    }
    public class SelectedClientGood
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String SecondName { get; set; }
        public String PhoneNumber { get; set; }
        public String Mail { get; set; }
        public int Code { get; set; }
        public string GoodName { get; set; }

        public SelectedClientGood(string name, string surname, string secondName, string phoneNumber, string mail, int code, string goodName)
        {
            Name = name;
            Surname = surname;
            SecondName = secondName;
            PhoneNumber = phoneNumber;
            Mail = mail;
            Code = code;
            GoodName = goodName;
        }
    }
}
