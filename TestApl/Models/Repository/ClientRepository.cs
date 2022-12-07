using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_11_bank.Models.Types;

namespace task_11_bank.Models.Repository
{
    internal class ClientRepository:IClientRepository
    {
        private List<Client> _clients;
        public ClientRepository()
        {
            GenerateClients();
        }
        public IEnumerable<Client> GetAll() => _clients;

        private void GenerateClients()
        {
            List<Client> _listClients = new List<Client>();
            _listClients.Add(new Client(
                name: "Denis",
                surname:"Smolov",
                thirdName:"Petrovich",
                phone:"89161192123",
                sID:"4500",
                nID:"324567"
                ));
            _listClients.Add(new Client(
                name: "Vasya",
                surname: "Petrov",
                thirdName: "Nickolaevich",
                phone: "89161135421",
                sID: "4501",
                nID: "765432"
                ));
              _listClients.Add(new Client(
                name: "Oleg",
                surname: "Pukin",
                thirdName: "Olegovich",
                phone: "89431138765",
                sID: "4701",
                nID: "763412"
                ));
            _clients=_listClients;
        }
    }
}
