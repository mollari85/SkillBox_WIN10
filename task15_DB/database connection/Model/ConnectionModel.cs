using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Collections.ObjectModel;

namespace database_connection.Model
{
    internal class ConnectionModel : IConnectionModel
    {
        private string _ConnectionString= "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
        //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
        private string _connectionLogin;
        private string _connectionPassword;
        private string ConnectionString{ get; set;}
        public string ConnectionLogin { get { return (_connectionLogin); } set { if (_connectionLogin != value) { _connectionLogin = value; OnPropertyChanged(ConnectionLogin); } } }
        public string ConnectionPassword {  get { return (_connectionPassword); } set { if (_connectionPassword != value) { _connectionPassword = value; OnPropertyChanged(ConnectionPassword); } } }

        private DataSet ds;
        public DataSet DS { get { return (ds); } set { ds = value; OnPropertyChanged("DS"); } }

        private DataTable dt;
        public DataTable DT { get { return (dt); } set { dt = value; OnPropertyChanged("DT"); } }
        //Action<>
        private ObservableCollection<Client> _Collection;
        public ObservableCollection<Client> Collection { get { return (_Collection); } }
        public ConnectionModel()
        {
            _Collection = new ObservableCollection<Client>();
           
        }
        async public Task<IEnumerable<Client>> GetClientsSQL()
        {
        List<Client> Collection=new List<Client>();
        //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
       //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(ConnectionString))
                {
                    await _connection.OpenAsync();
                    if (_connection.State == System.Data.ConnectionState.Open)
                        MessageBox.Show("Connection good");
                    else
                        MessageBox.Show("Connection BAD");
                   
                    string query = "Select * from dbo.Client";
                    SqlDataAdapter da = new SqlDataAdapter(query,_connection);
                   //DataSet ds = new DataSet();
                   DT=new DataTable("Client");
                    DataTable dd = new DataTable("cl");
                    da.Fill(DT);
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    for (int i=0; i<DT.Rows.Count; i++)
                    {

                        Collection.Add(new Client(int.Parse(Convert.ToString(DT.Rows[i]["ID"])), 
                            Convert.ToString(DT.Rows[i][1]), Convert.ToString(DT.Rows[i][2]), 
                            Convert.ToString(DT.Rows[i][3]), Convert.ToString(DT.Rows[i][4]),
                            Convert.ToString(DT.Rows[i][5])));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
                return Collection;

        }
        async public Task<bool> DeleteInSQL()
        {
            
           // ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    await _connection.OpenAsync();
                    if (_connection.State == System.Data.ConnectionState.Open)
                        MessageBox.Show("Connection good");
                    else
                        MessageBox.Show("Connection BAD");

                    string query = "Delete  from dbo.Client where ID=1";
                    SqlDataAdapter da = new SqlDataAdapter(query, _connection);
                    //DataSet ds = new DataSet();
                    DT = new DataTable("Client");
                    DataTable dd = new DataTable("cl");
                    da.Fill(DT);
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {

                        Collection.Add(new Client(int.Parse(Convert.ToString(DT.Rows[i]["ID"])),
                            Convert.ToString(DT.Rows[i][1]), Convert.ToString(DT.Rows[i][2]),
                            Convert.ToString(DT.Rows[i][3]), Convert.ToString(DT.Rows[i][4]),
                            Convert.ToString(DT.Rows[i][5])));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            return true;

        }


        #region tools
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
