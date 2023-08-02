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
        /// <summary>
        /// Transfer between Clients accounts
        /// </summary>
        /// <param name="account1"></param>
        /// <param name="account2"></param>
        /// <returns></returns>
        public bool TransferBetween(Account account1, Account account2, decimal amount);
        public bool TransferTo(Client Client1, Account account1,Client Client2, Account account2, decimal amount);
        
        public static event Action<Guid, string, decimal, LogAction> eventNotify;

        public enum TypeOfAccounts {Deposit,NonDeposit};

    }
}
