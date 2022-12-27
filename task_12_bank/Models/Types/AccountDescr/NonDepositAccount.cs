﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.AccountDescr;

namespace task_12_bankAccount.Model.Types
{
    internal class NonDepositAccount:Account
    {
        //let assume that Deposit accouts have number 4500 5000 xxxx xxxx xxxx
        public static int NextAvailableNumber;
      //  public static List<DepositAccount> AllNonDepositAccount;
        public NonDepositAccount
        (Guid PersonID, decimal LowLimit = 0, decimal MaxOneShotWithdraw = 0,
            decimal MaxDailyWithdraw = 0, decimal? MaxLimitPeriodRefill = null, int PeriodInDays = 30)
            :base(PersonID, "45005000"+NextAvailableNumber.ToString(), LowLimit,MaxOneShotWithdraw,MaxDailyWithdraw,MaxLimitPeriodRefill,PeriodInDays)
        {
            
            Description = "NonDeposit Account";
        }

}
}
