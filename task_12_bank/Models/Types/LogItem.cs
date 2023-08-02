using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Support;
using task_12_bank.Models.Types.Enum;

namespace task_12_bank.Models.Types
{
    internal class LogItem:LogEventArgs
    {
        /*
        public Client? OldClientLog { get; set; }
        public Client? NewClientLog { get; set; }
        public Positions WorkingEmployee { get; set; }
        public LogAction LogAction { get; set; }
        public DateTime DT { get; }


        public string? AccountNumber { get; set; }
        public decimal? Value { get; set; }
        */
        public DateTime DT { get; }
        public Positions Position { get; set; }
        public LogItem(LogAction Log, decimal? Value = null, string? AccountNumber = null, Client? OldCData = null, Client? NewData = null) 
            :base(Log, OldCData ,NewData , AccountNumber ,  Value )
        {
            if (Log == LogAction.Withdraw |
                Log == LogAction.Refill |
                Log == LogAction.Create |
                Log == LogAction.Delete)
                Position = Positions.Client;
            else
                Position = AuthenticatedAccount.AuthenticatedEmployee.Position;
            DT = DateTime.Now;

        }
        public LogItem( LogEventArgs e):base(e)
        {
            if (e.LogAction==LogAction.Withdraw | 
                e.LogAction == LogAction.Refill |
                e.LogAction == LogAction.Create |
                e.LogAction == LogAction.Delete)
                    Position = Positions.Client;
            else
            Position = AuthenticatedAccount.AuthenticatedEmployee.Position;
            DT = DateTime.Now;

        }
    }
}
