using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_11_bank.Models.Types
{
    abstract class Employee : Person
    {

        public bool IsReadOnlyFIO{ get; }
        public bool IsPassportHidden { get; }
       
        public Employee(bool IsReadOnlyFIO, bool IsPassportHidden) : base()
        {
            this.IsReadOnlyFIO = IsReadOnlyFIO;
            this.IsPassportHidden = IsPassportHidden;
        }

    }
}
