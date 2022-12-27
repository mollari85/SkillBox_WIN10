using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bankAccount.Model.Types
{
    internal class DepositAccount//:Account
    {
        /// <summary>
        /// i think we can reUse closed accounts
        /// </summary>
        private const int yearsDealyForReUse= 3;
        //let aasume that Deposit accouts have number 4500 0000 xxxx xxxx xxxx
        public bool IsAvailableNumber(List<Account> Accounts)
        {
            return Accounts.FirstOrDefault(x => x.DateEndAccount < DateOnly.FromDateTime(DateTime.Today.AddDays(-yearsDealyForReUse)))==null?false:true;           
        }
        public string GetAvailableNumber(List<Account> Accounts)
        {
            return Accounts.FirstOrDefault(x => x.DateEndAccount < DateOnly.FromDateTime(DateTime.Today.AddDays(-yearsDealyForReUse))).AccountNumber;
        }

        public void CreateAccount()
        {
            //base();
        }
    }
}
