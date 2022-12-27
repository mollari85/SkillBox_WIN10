using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Support;
using task_12_bank.Models.Types;
using task_12_bank.Models.Types.AccountDescr;
using task_12_bankAccount.Model.Types;

namespace task_12_bank.Models.Types.Bank_
{
    internal class BankSystem : IBankClient
    {
        public static BankSystem BankSber= new BankSystem(); 
        List<Account> BankAccounts;

        public BankSystem()
        {
            
            BankAccounts = new List<Account>();
        }
        public void CreateNewAccount(Account account)
        {

            BankAccounts.Add(account);
            if (account is DepositAccount )
                DepositAccount.NextAvailableNumber += 1;
            if (account is NonDepositAccount)
               NonDepositAccount.NextAvailableNumber += 1;


        }
        public void DeleteAccount(Account account)
        {
            BankAccounts.Remove(account);
        }
        public bool IsClient(Client client)
        {
            return BankAccounts.FirstOrDefault(x => x.PersonID == client.PersonID) == null ? false : true;
        }
        public IEnumerable<Account> GetClientAccounts(Client client)
        {
            
            return BankAccounts.FindAll(x => x.PersonID == client.PersonID);
        }

    }
}
