using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types;
using task_12_bank.Models.Types.AccountDescr;
using task_12_bankAccount.Model.Types;

namespace task_12_bank.Models.Support
{
    static class TestStatic
    {
        /// test
        /// 
        public static Client MyClient = new Client(name: "Denis", surname: "Mel", thirdName: "Nick", sID: "1100", nID: "123457", phone: "891234567890");
        public static Account MyDepositAccount = new DepositAccount(MyClient.PersonID, 0, 3000, 3000, 0, 30);
       // public static BankSystem MyBankSystem= new BankSystem(MyDepositAccount);

    }
}
