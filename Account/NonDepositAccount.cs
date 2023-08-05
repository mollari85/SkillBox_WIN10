using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Accountlib
{
    public class NonDepositAccount:Account,ICloneable
    {
        //let assume that Deposit accouts have number 4500 5000 xxxx xxxx xxxx
        public static int NextAvailableNumber;
      //  public static List<DepositAccount> AllNonDepositAccount;
        public NonDepositAccount
        (Guid PersonID, decimal LowLimit = 0, decimal MaxOneShotWithdraw = 0,
            decimal MaxDailyWithdraw = 0, decimal? MaxLimitPeriodRefill = null, int? PeriodInDays = 30)
            :base(PersonID, "45005000"+NextAvailableNumber.ToString(), LowLimit,MaxOneShotWithdraw,MaxDailyWithdraw,MaxLimitPeriodRefill,PeriodInDays)
        {
            
            Description = "NonDeposit Account";
        }
        public object Clone()
        {
            return new NonDepositAccount(this.PersonID, this.Limit.LowLimit, this.Limit.MaxOneShotWithdraw,
                this.Limit.MaxDailyWithdraw, this.Limit.MaxLimitPeriodRefill, this.PeriodInDays);
        }
    }
}
