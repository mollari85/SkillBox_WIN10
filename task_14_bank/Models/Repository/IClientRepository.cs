using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_14_bank.Models.Types;

namespace task_14_bank.Models.Repository
{
    internal interface IClientRepository
    {
        public IEnumerable<Client> GetAll();
        public void SaveToRepository(IEnumerable<Client> rep);
        public bool IsClient(string Surname);
        public Client GetClient(string Surname);
    }
}
