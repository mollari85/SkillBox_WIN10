using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using task_14_bank.Models.Types.AccountDescr;
using Logging;
using Accountlib;
using task_14_bank.Models.Types.Enum;

namespace task_14_bank.Models.Types
{
    internal class LogEventArgs: AccountEventArgs
    {
        public Client? OldClientLog { get; set; }
        public Client? NewClientLog { get; set; }
       /* public LogAction LogAction { get; set; }


        public string? AccountNumber { get; set; }
        public decimal? Value { get; set; }
       */

        public LogEventArgs(LogAction logAction, Client? oldClientLog, Client? newClientLog, string? accountNumber, decimal? value)
            :base (logAction,accountNumber,value)
        {
            OldClientLog = oldClientLog;
            NewClientLog = newClientLog;

        }
        public LogEventArgs(LogEventArgs e)
            : base(e.LogAction, e.AccountNumber,e.Value)
        {
            this.OldClientLog = e.OldClientLog;
            NewClientLog = e.NewClientLog;

        }
    }
}
