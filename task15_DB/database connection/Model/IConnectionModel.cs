using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_connection.Model
{
    internal interface IConnectionModel
    {
        public string ConnectionLogin { get; set; }
        public string ConnectionPassword { get; set; }

        public DataTable DT { get; set; }
        public  Task<IEnumerable<Client>> GetClientsSQL();
      //  public ObservableCollection<Client> Collection { get; }


    }
}
