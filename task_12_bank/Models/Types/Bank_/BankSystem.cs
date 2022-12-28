using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
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
        public bool TransferBetween(Account accountFrom, Account accountTo, decimal amount)
        {
            if (  (accountFrom == null || accountTo == null) || (accountFrom == accountTo) || (accountFrom.PersonID != accountTo.PersonID) || (amount <= 0)  )
                return false;
            if (!IsClient(accountFrom.PersonID))
                return false;

            Account BankAccountFrom = BankAccounts.FirstOrDefault(x => x == accountFrom);
            Account BankAccountTo = BankAccounts.FirstOrDefault(x => x == accountTo);
            if (BankAccountFrom == null || BankAccountTo == null)
                return false;
            BankAccountTo.Refill(amount);
            BankAccountFrom.WithDraw(amount);
            return true;

        }
        public bool TransferTo(Client ClientFrom, Account AccountFrom, Client ClientTo, Account AccountTo, decimal amount)
        {
            if (ClientFrom == null || ClientFrom == null || AccountTo == null || AccountFrom==null || ClientFrom.PersonID != ClientTo.PersonID || amount <= 0)
                return false;
            if (!IsClient(ClientTo.PersonID) || !IsClient(ClientFrom.PersonID))
                return false;

            Account BankAccountFrom = BankAccounts.FirstOrDefault(x => x == AccountFrom);
            Account BankAccountTo = BankAccounts.FirstOrDefault(x => x == AccountTo);
            if (BankAccountFrom == null || BankAccountTo == null)
                return false;
            BankAccountTo.Refill(amount);
            BankAccountFrom.WithDraw(amount);
            return true;
        }
        public void DeleteAccount(Account account)
        {
            if (account == null)
                throw new ArgumentException("account can't be null");
            if (!IsClient(account.PersonID))
                throw new ArgumentException("account is not in the base");
            Account AccountTmp = BankAccounts.FirstOrDefault(x => x == account);
            if (AccountTmp != null)
                if (AccountTmp.AccountGeneral == 0)
                    BankAccounts.Remove(AccountTmp);
                else
                {
                    MessageBox.Show($"Accoun has moneyt. it can't be deleted");
                    throw new Exception($"Account {AccountTmp.AccountNumber} can't be deleted because it has not zero account ");
                }
        }
        public bool IsClient(Client client)
        {
            return BankAccounts.FirstOrDefault(x => x.PersonID == client.PersonID) == null ? false : true;
        }
        public bool IsClient(Guid ID)
        {
            return BankAccounts.FirstOrDefault(x => x.PersonID == ID) == null ? false : true;
        }
        public IEnumerable<Account> GetClientAccounts(Client client)
        {
            
            return BankAccounts.FindAll(x => x.PersonID == client.PersonID);
        }

    }
}
