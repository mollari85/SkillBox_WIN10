using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_11_bank.Models.Types;

namespace task_11_bank.Models.Repository
{
    internal class EmployeeRepository:IEmployeeRepository
    {
        public bool IsEmployee(string Employee)
        {
            if (String.IsNullOrEmpty(Employee))
                return false;
            string tmpEmployee=Employee.ToUpper().Trim();
            if (tmpEmployee == "MANAGER" | tmpEmployee == "CONSULTANT")
                return (true);
            return (false);
        }
        public Employee GetEmployee(string EmployeeLogin)
        {
            if (String.IsNullOrEmpty(EmployeeLogin))
                return null;
            string tmpEmployee=EmployeeLogin.ToUpper().Trim();
            return tmpEmployee == "MANAGER" ? new Manager() : new Consultant();
        }
    }
}
