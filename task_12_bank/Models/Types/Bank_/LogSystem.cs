using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_12_bank.Models.Types.AccountDescr;
using task_12_bank.Models.Support;
using task_12_bank.ViewModels;
using System.Collections.ObjectModel;

namespace task_12_bank.Models.Types.Bank_
{
    internal static class LogSystem
    {

        //public static List<LogItem> LogList;
        public static ObservableCollection<LogItem> LogList;
        static LogSystem()
        {
            //LogList = new List<LogItem>();
            LogList=new ObservableCollection<LogItem>();
            LogList.Add(new LogItem(LogAction.Create, 100, "11111111", null, null));
            VM_Client.eventIsLogChanged += IsLog;

            
            
        }
        static bool _log=false;
        public static void IsLog(object o, bool isLog)
        { 
            Log= isLog;
        }
        public static bool Log { get { return (_log); } 
            set 
            {
                if (_log != value)
                {
                    _log = value;
                    if (_log)
                    {
                        BankSystem.eventNotifyRefillAccount += LogAddItem;
                        BankSystem.eventNotifyWithdrawAccount += LogAddItem;
                        BankSystem.eventNotifyCreateAccount += LogAddItem;
                        BankSystem.eventNotifyDeleteAccount += LogAddItem;
                        BankSystem.eventNotifyTransferBetweenAccounts += LogAddItem;
                        VM_Client.eventClientCreated += LogAddItem;
                        VM_Client.eventClientDeleted += LogAddItem;
                        VM_Client.eventClientEdit += LogAddItem;
                    }
                    else
                    {
                        BankSystem.eventNotifyRefillAccount -= LogAddItem;
                        BankSystem.eventNotifyWithdrawAccount -= LogAddItem;
                        BankSystem.eventNotifyCreateAccount -= LogAddItem;
                        BankSystem.eventNotifyDeleteAccount -= LogAddItem;
                        BankSystem.eventNotifyTransferBetweenAccounts -= LogAddItem;
                        VM_Client.eventClientCreated -= LogAddItem;
                        VM_Client.eventClientDeleted -= LogAddItem;
                        VM_Client.eventClientEdit -= LogAddItem;
                    }
                }
            } 
         }
        /// <summary>
        /// if operation is withdraw or transfer or fill
        /// </summary>
        /// <param name="PersonID"></param>
        /// <param name="AccountNumber"></param>
        /// <param name="Amount"></param>
        /// <param name="action"></param>
        public static void LogAddItem(Guid PersonID, string AccountNumber, decimal Amount, LogAction action)
        {
            Debug.WriteLine(" dd "+action.ToString());
            LogList.Add(new LogItem(action, Amount, AccountNumber));
           
        }
        public static void LogAddItem(object o, EventArgs e)
        {

            if (e is LogEventArgs LogArg)
            {
                Debug.WriteLine(" dd " + LogArg.LogAction.ToString());
                //LogList.Add(new LogItem(LogArg.LogAction, LogArg.Value, LogArg.AccountNumber));
                LogList.Add(new LogItem(LogArg));
            }
            else
            {
                Debug.WriteLine($" can not convert {e} into LogEventArgs");
            }

        }
        /// <summary>
        /// in case of create or delete an account
        /// </summary>
        /// <param name="account"></param>
        /// <param name="action"></param>
        public static void LogAddItem(Account account, LogAction action)
        {
            Debug.WriteLine(" Log 2 parameters "+account.PersonID + action.ToString());
            LogList.Add(new LogItem( action, null,account.AccountNumber ));

        }
        public static void LogAddItem(Client clientOldInfo, Client clientNewInfo, LogAction action)
        {
            Debug.WriteLine("log 3 parameter info " + clientOldInfo + action.ToString());
            LogList.Add(new LogItem( action, null, null,clientOldInfo,clientNewInfo));

        }
    }
}
