using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.Enum;

namespace task_12_bank.Models.Types
{
    internal class Consultant:Employee
    {
        public Consultant() : base(IsPassportHidden:true, IsReadOnlyFIO: true, Position: Positions.Consultant) 
        {
            

        }
    }
}
