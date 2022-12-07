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

namespace task_11_bank.ViewModels
{
    internal class VM_Client
    {   
        public string Phone { get; set; }
        public Client ClientView { get; set; }
        public ObservableCollection<Client> Clients { get; }
        IClientRepository _clientRepository;
        public VM_Client(IClientRepository clientRepository)
        {
            //ClientModel Model = new ClientModel();
            //ClientView = new Client();

            _clientRepository = clientRepository;         

            Clients=new ObservableCollection<Client>(_clientRepository.GetAll());

            ClientView = Clients.FirstOrDefault();

            Phone = "HI";
        }

    }
}
