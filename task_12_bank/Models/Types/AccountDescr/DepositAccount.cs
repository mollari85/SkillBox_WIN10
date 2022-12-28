using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.AccountDescr;

namespace task_12_bankAccount.Model.Types
{
    public class DepositAccount:Account,ICloneable
    {

        //let assume that Deposit accouts have number 4500 0000 xxxx xxxx xxxx
        private const int _firstNumber = 33;//for test
        public static int NextAvailableNumber= _firstNumber;
       // public static List<DepositAccount> AllDepositAccount;
        public DepositAccount(Guid PersonID, decimal LowLimit = 0, decimal MaxOneShotWithdraw = 50000, 
            decimal MaxDailyWithdraw = 100000, decimal? MaxLimitPeriodRefill = 500000, int? PeriodInDays = 30)
            :base(PersonID, "45000000"+NextAvailableNumber.ToString(), LowLimit,MaxOneShotWithdraw,MaxDailyWithdraw,MaxLimitPeriodRefill,PeriodInDays)
        {
            
            Description = "Deposit Account";
        }

        public object Clone()
        {
            return new DepositAccount(this.PersonID, this.Limit.LowLimit, this.Limit.MaxOneShotWithdraw,
                this.Limit.MaxDailyWithdraw, this.Limit.MaxLimitPeriodRefill, this.PeriodInDays);
        }
    }
}
