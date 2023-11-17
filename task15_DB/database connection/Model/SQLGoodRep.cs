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
    internal class SQLGoodRep : IRepository<Good>
    {
        private IEnumerable<Good> db;
       // private string _ConnectionString = "Server=ES-RW-20-32; Database=Good;Trusted_Connection=true;";
        private string   _ConnectionString = "Server=ES-RW-20-32; Database=Good;User=abc; Password=123";
        public SQLGoodRep()
        {
            this.db = new List<Good>();
            GetItemList();
        }

        public void Create(Good item)
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

        public IEnumerable<Good> GetItemList()
        {
            GetGoodsSQL();



            return db;
        }


        public Good GetItem(int id)
        {
            return db.FirstOrDefault(x => x.ID == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Good item)
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
                    string query = "Delete  from dbo.Good where ID=@id";
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
        async private Task<int?> CreateAsync(Good item)
        {

            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    await _connection.OpenAsync();
                    string query = "insert  INTO dbo.Good ([Code],[Name],[Mail]) " +
                        "VALUES (@Code,@Name,@Mail)";
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    SqlCommand commnad = new SqlCommand(query, _connection);
                    // SqlParameter idParam = new SqlParameter("@id", client.ID);
                    // commnad.Parameters.Add(idParam);

                    commnad.Parameters.AddWithValue("@Name", item.Name);
                    commnad.Parameters.AddWithValue("@Code", item.Code);
                    commnad.Parameters.AddWithValue("@Mail", item.Mail);
                    return (await commnad.ExecuteNonQueryAsync());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return (null);

        }

        async private Task<int?> UpdateAsync(Good item)
        {

            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;Trusted_Connection=true;";
            //   ConnectionString = "Server=ES-RW-20-32; Database=Clients;User=abc; Password=123";
            try
            {
                using (SqlConnection _connection = new SqlConnection(_ConnectionString))
                {
                    await _connection.OpenAsync();
                    string query = "Update dbo.Good Set [Code]=@Code,[Name]=@Name," +
                        "[Mail]=@Mail  Where [ID]=@id ";

                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    SqlCommand commnad = new SqlCommand(query, _connection);
                    commnad.Parameters.AddWithValue("@id", item.ID);
                    commnad.Parameters.AddWithValue("@Code", item.Code);
                    commnad.Parameters.AddWithValue("@Name", item.Name);
                    commnad.Parameters.AddWithValue("@Mail", item.Mail);
                    return (await commnad.ExecuteNonQueryAsync());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return (null);

        }
        async private Task GetGoodsSQL()
        {
            List<Good> Collection = new List<Good>();
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

                    string query = "Select * from dbo.Good";
                    SqlDataAdapter da = new SqlDataAdapter(query, _connection);
                    DataTable DT = new DataTable("Good");
                    da.Fill(DT);
                    // using (SqlCommand _cmnd=new SqlCommand(query,_connection))
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {

                        Collection.Add(new Good(Convert.ToInt32(DT.Rows[i]["ID"]),
                            Convert.ToString(DT.Rows[i]["Mail"]), Convert.ToInt32(DT.Rows[i]["Code"]),
                            Convert.ToString(DT.Rows[i]["Name"])));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            db = Collection;
            // return Collection;
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
