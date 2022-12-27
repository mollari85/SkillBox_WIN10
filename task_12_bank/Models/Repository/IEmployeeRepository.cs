using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types;

namespace task_12_bank.Models.Repository
{
    internal interface IEmployeeRepository
    {
        public bool IsEmployee(string Employee);
        public Employee GetEmployee(string EmployeeLogin);
    }
}
