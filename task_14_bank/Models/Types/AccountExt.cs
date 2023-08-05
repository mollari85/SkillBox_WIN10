using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Accountlib;

namespace task_14_bank.Models.Types
{
    static class AccountExt
    {
        public static void PrintAccount(this Account account)
        {
            MessageBox.Show($"account has value={account.AccountGeneral}");
        }
    }
}
