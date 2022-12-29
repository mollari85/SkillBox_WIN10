using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.Enum;

namespace task_12_bank.Models.Types
{
    internal class LogData
    {
       public Client? OldClientLog { get; set; }
       public  Client? NewClientLog { get; set; }
       public   Positions WorkingEmployee { get; set; }
       public LogAction LogAction { get; set; }
        public DateTime DT { get; }

        public LogData(Employee emp,LogAction log,Client? OldCData=null, Client? NewData=null)
        {
            WorkingEmployee = emp.Position;
            LogAction = log;
            OldClientLog = OldCData;
            NewClientLog = NewData;
            DT= DateTime.Now;

        }
    }
}
