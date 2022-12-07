using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_11_bank.Models.Types
{
    internal class Manager:Employee
    {
        public Manager():base(IsPassportHidden: false, IsReadOnlyFIO: false)
        { 
        }
    }
}
