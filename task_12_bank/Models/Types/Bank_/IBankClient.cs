using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.AccountDescr;

namespace task_12_bank.Models.Types.Bank_
{
    public interface IBankClient
    {
        public void CreateNewAccount(Account account);
        public void DeleteAccount(Account account);
        public IEnumerable<Account> GetClientAccounts(Client client);
        public bool IsClient(Client client);

        public enum TypeOfAccounts {Deposit,NonDeposit};

    }
}
