using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace database_connection.Model
{
    internal class SQLClientRep : IRepository<Client>
    {
        private IEnumerable<Client> db;
      //  private string _ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
           private string _ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
        public SQLClientRep()
        {
            this.db = new List<Client>();
            GetItemList();
        }

        public void Create(Client item)
        {
            CreateAsync(item);
        }

        public void Delete(int id)
        {
            DeleteAsync(id);
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
                if (disposing)
                    Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Client> GetItemList()
        {
             GetClientsSQL();
           


            return  db;
        }


        public Client GetItem(int id)
        {
            return db.FirstOrDefault(x => x.ID == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Client item)
        {
            UpdateAsync(item);
        }
        async private Task<int?> DeleteAsync(int id)
        {
            
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    await _connection.OpenAsync();
                    string query = "Delete  from dbo.Client where ID=@id";
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    SqlCommand commnad = new SqlCommand(query, _connection);
                    SqlParameter idParam = new SqlParameter("@id", id);
                    commnad.Parameters.Add(idParam);
                    return (await commnad.ExecuteNonQueryAsync());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return (null);
         
        }
        async private Task<int?> CreateAsync(Client client)
        {

            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    await _connection.OpenAsync();
                    string query = "insert  INTO dbo.Client ([Surname],[Name],[SecondName],[PhoneNumber],[Mail]) " +
                        "VALUES (@Surname,@Name,@SecondName,@PhoneNumber,@Mail)";
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    SqlCommand commnad = new SqlCommand(query, _connection);
                   // SqlParameter idParam = new SqlParameter("@id", client.ID);
                   // commnad.Parameters.Add(idParam);
                    commnad.Parameters.AddWithValue("@Surname", client.Surname);
                    commnad.Parameters.AddWithValue("@Name", client.Name);
                    commnad.Parameters.AddWithValue("@SecondName", client.SecondName);
                    commnad.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    commnad.Parameters.AddWithValue("@Mail", client.Mail);
                    return (await commnad.ExecuteNonQueryAsync());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return (null);

        }

        async private Task<int?> UpdateAsync(Client client)
        {

            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    await _connection.OpenAsync();
                    string query = "Update dbo.Client Set [Surname]=@Surname,[Name]=@Name," +
                        "[SecondName]=@SecondName,[PhoneNumber]=@PhoneNumber,[Mail]=@Mail  Where [ID]=@id ";
                        
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    SqlCommand commnad = new SqlCommand(query, _connection);
                    SqlParameter idParam = new SqlParameter("@id", client.ID.ToString());
                    commnad.Parameters.Add(idParam);
                    commnad.Parameters.AddWithValue("@Surname", client.Surname);
                    commnad.Parameters.AddWithValue("@Name", client.Name);
                    commnad.Parameters.AddWithValue("@SecondName", client.SecondName);
                    commnad.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    commnad.Parameters.AddWithValue("@Mail", client.Mail);
                    return (await commnad.ExecuteNonQueryAsync());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return (null);

        }
        async private Task GetClientsSQL()
        {
            List<Client> Collection = new List<Client>();
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
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

                    string query = "Select * from dbo.Client";
                    SqlDataAdapter da = new SqlDataAdapter(query, _connection);
                    DataTable DT = new DataTable("Client");
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

            db = Collection;
           // return Collection;
        }
    }
}
