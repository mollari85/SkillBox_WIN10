using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;

namespace Accountlib
{
   public  class AccountEventArgs : EventArgs
    {
        public LogAction LogAction { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? Value { get; set; }

        public AccountEventArgs(LogAction logAction, string? accountNumber, decimal? value)
        {
   
            LogAction = logAction;
            AccountNumber = accountNumber;
            Value = value;
        }
        public AccountEventArgs(AccountEventArgs e)
        {

            LogAction = e.LogAction;
            AccountNumber = e.AccountNumber;
            Value = e.Value;
        }
    }
}
