using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types;

namespace task_12_bank.Models.Repository
{
    internal interface IClientRepository
    {
        public IEnumerable<Client> GetAll();
        public void SaveToRepository(IEnumerable<Client> rep);
        public bool IsClient(string Surname);
        public Client GetClient(string Surname);
    }
}
