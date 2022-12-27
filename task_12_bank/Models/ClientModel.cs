using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types;

namespace task_12_bank.Models
{
    internal class ClientModel
    {
        static ObservableCollection<Client> ClientDB;

        public ClientModel()
        {
            ClientDB = new ObservableCollection<Client>();
            ClientDB.Add(new Client());
        }
    }
}
