using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_14_bank.Models.Types;

namespace task_14_bank.Models.Support
{
    static class AuthenticatedAccount
    {
        public static Employee AuthenticatedEmployee { get;set; }
        public static Client AuthenticatedClient { get; set; }



    }

}