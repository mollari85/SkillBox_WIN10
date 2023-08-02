using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bank.Models.Types
{
    public enum LogAction
    {
        
        Create,
        Delete,
        Withdraw,
        Refill,
        TransferIn,
        TransferOut,
        Transfer,
        ClientCreate,
        ClientEdit,
        ClientDelete
        
    }
}
