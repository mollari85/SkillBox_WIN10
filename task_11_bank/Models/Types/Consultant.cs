using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_11_bank.Models.Types
{
    internal class Consultant:Employee
    {
        public Consultant() : base(IsPassportHidden:true, IsReadOnlyFIO: true) 
        {
            

        }
    }
}
