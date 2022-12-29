using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bankAccount.Model.Types
{
    internal class Client:Person
    {
        Guid IDguid;

        public Client()
        {
            IDguid = Guid.NewGuid();
        }

    }
}
