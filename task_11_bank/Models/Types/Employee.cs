using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_11_bank.Models.Types.Enum;

namespace task_11_bank.Models.Types
{
    abstract class Employee : Person
    {

        public bool IsReadOnlyFIO{ get; }
        public bool IsPassportHidden { get; }
        public Positions Position { get; set; }
       
        public Employee(bool IsReadOnlyFIO, bool IsPassportHidden, Positions Position) : base()
        {
            this.IsReadOnlyFIO = IsReadOnlyFIO;
            this.IsPassportHidden = IsPassportHidden;
            this.Position = Position;
        }

    }
}
