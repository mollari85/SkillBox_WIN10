using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.Enum;

namespace task_12_bank.Models.Types
{
    internal class Manager:Employee
    {
        public Manager():base(IsPassportHidden: false, IsReadOnlyFIO: false, Position:Positions.Manager)
        { 
        }
    }
}
