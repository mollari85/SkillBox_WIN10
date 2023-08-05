
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using task_14_bank.Models.Types;
using task_14_bank.Models.Support;
using task_14_bank.Tools;
using System.Windows;
using Microsoft.VisualBasic;
using task_14_bank.Models.Types.Bank_;
using System.ComponentModel;
//using task_14_bank.Models.Types.AccountDescr;
using task_14_bank.Models.Types.Enum;
using System.Runtime.CompilerServices;
//using task_14_bankAccount.Model.Types;
using task_14_bank.View;
using Accountlib;
using Logging;

namespace task_14_bank.ViewModels
{
    internal class VM_ClientWorkingArea : INotifyPropertyChanged
    {
        IBankClient BankSystem { get; set; }
        private Account _account;
        /// <summary>
        /// current account to display
        /// </summary>
        public Account Account
        { get { return _account; }
            set { _account = value; OnPropertyChanged(); }
        }
        /// <summary>
        /// current client to display
        /// </summary>
        private Client _currentclient;
        public Client CurrentClient
        { get { return _currentclient; }
            set { _currentclient = value; OnPropertyChanged(); } }

        private TypeAccounts _typeaccounts;
        public TypeAccounts AccountType
        {
            get { return _typeaccounts; }
            set {
                _typeaccounts = value;
                switch (value)
                {
                    case TypeAccounts.Depoist:
                        Account = new DepositAccount(CurrentClient.PersonID);

                        break;
                    case TypeAccounts.NonDeposit:
                        Account = new NonDepositAccount(CurrentClient.PersonID);
                        break;
                }

                OnPropertyChanged();
            }
        }

        public DateOnly DateToday { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public PanelTransaction PanelTransactionView { get; set; }


        #region Support property for the View
        private Visibility _containerNewAccount = Visibility.Collapsed;
        public Visibility ContainerNewAccount { get { return _containerNewAccount; } set { _containerNewAccount = value; OnPropertyChanged(); } }

        private Visibility _containerRefill = Visibility.Collapsed;
        public Visibility ContainerRefill { get { return _containerRefill; } set { _containerRefill = value; OnPropertyChanged(); } }

        private Visibility _containerWithdraw = Visibility.Collapsed;
        public Visibility ContainerWithdraw { get { return _containerWithdraw; } set { _containerWithdraw = value; OnPropertyChanged(); } }


        #endregion

        #region test area
        public class Test { string Name; string Surname; public Test() { Name = "Den";Surname = "Mel"; } }
        public Test TestTest {get;set;}
        private ObservableCollection<Test> _testCol = new ObservableCollection<Test>();
        public ObservableCollection<Test> TestCol { get ; set; }
        #endregion
        public VM_ClientWorkingArea(Client Client, IBankClient BankSystem)
        {
            this.BankSystem = BankSystem;
            this.CurrentClient = Client;
            Client.GetMyAccounts();

            PanelTransactionView = new PanelTransaction(Account, BankSystem);


           
            #region TEst area

            CurrentClient.Name = "ChangedDenisName";
            /*
            TestCol.Add(new Test());
            TestCol.Add(new Test());
            TestCol.Add(new Test());

*/
            #endregion

            #region Interfaces set link


            #endregion
            #region Set Commnads link to a Method
            CommandOKCreateAccount = new RelayCommand(OKCreateAccount);
            CommandCancelCreateAccount = new RelayCommand(CancelCreateAccount);
            CommandOpenNewAccount = new RelayCommand(OpenNewAccount);
            CommandDeleteAccount = new RelayCommand(DeleteAccount);
            CommandOKAdd = new RelayCommand(OKAdd);
            CommandWithDraw = new RelayCommand(Withdraw);
            CommandRefill = new RelayCommand(Refill);
            CommandButtonWithdraw = new RelayCommand(ButtonWithdraw);
            CommandTranfer = new RelayCommand(Transfer);
            CommandExitClient = new RelayCommand(Exit);

            #endregion
        }

        #region Methods for Commands
        private void OKAdd(object obj)
        {
            if (obj is string sTmp)
            {
                int.TryParse(sTmp, out int RefillAmount);
                Account.Refill(RefillAmount);
            }
            ContainerRefill = Visibility.Collapsed;
        }
        private void Withdraw(object obj)
        {
            try
            {
                if (obj is string sTmp)
                {
                    int.TryParse(sTmp, out int WithdrawAmount);
                    Account.WithDraw(WithdrawAmount);
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            ContainerWithdraw = Visibility.Collapsed;
        }
        private void Refill(object obj)
        {
            ContainerWithdraw=Visibility.Collapsed;
            ContainerRefill = Visibility.Visible;
        }
        private void ButtonWithdraw(object obj)
        {
            ContainerWithdraw = Visibility.Visible;
            ContainerRefill = Visibility.Collapsed;
        }
        private void Transfer(object obj)
        {
            PanelTransactionView.ContainerTransferVisibility=Visibility.Visible;
        }
        private void OpenNewAccount(object obj)
        {
            ContainerNewAccount = Visibility.Visible;

            switch (AccountType)
            {
                case TypeAccounts.Depoist:
                    Account = new DepositAccount(CurrentClient.PersonID);

                    break;
                case TypeAccounts.NonDeposit:
                    Account = new NonDepositAccount(CurrentClient.PersonID);
                    break;
            }
        }
        private void DeleteAccount(object obj)
        {
            
          
            BankSystem.DeleteAccount(Account);
            CurrentClient.GetMyAccounts();

        }
        private void OKCreateAccount(object obj)
        {

            BankSystem.CreateNewAccount(Account);
            CurrentClient.GetMyAccounts();
            ContainerNewAccount = Visibility.Collapsed;
            
        }

        private void CancelCreateAccount(object obj)
        {
            ContainerNewAccount = Visibility.Collapsed;
            //Account
        }
        private void Exit(object obj)
        {
            
            Window window = new AuthenticationView();
            window.Show();
           
            foreach (Window win in Application.Current.Windows)
            {
                if (win is ClientWorkingArea)
                    win.Close();
            }
        }

        #endregion
        #region Commands
        RelayCommand _commandOpenNewAccount;
        public RelayCommand CommandOpenNewAccount
        {
            get
            {
                return _commandOpenNewAccount ?? new RelayCommand(obj => MessageBox.Show("Button Create Account is not working"));
            }
            set { _commandOpenNewAccount = value; }
        }
        RelayCommand _commandDeleteAccount;
        public RelayCommand CommandDeleteAccount
        {
            get
            {
                return _commandDeleteAccount ?? new RelayCommand(obj => MessageBox.Show("Button Delete Account is not working"));
            }
            set { _commandDeleteAccount = value; }
        }
        RelayCommand _commandOKCreate;
        public RelayCommand CommandOKCreateAccount
        {
            get
            {
                return _commandOKCreate ?? new RelayCommand(obj => MessageBox.Show("Button OK Create Account is not working"));
            }
            set { _commandOKCreate = value; }
        }
        RelayCommand _commandCancelCreateAccount;
        public RelayCommand CommandCancelCreateAccount { 
            get
            {
                return _commandCancelCreateAccount ?? new RelayCommand(obj => MessageBox.Show("Button Cancel Create Account is not working"));
            }
            set { _commandCancelCreateAccount = value; }
        }

        RelayCommand _commandOKAdd;
        public RelayCommand CommandOKAdd
        {
            get
            {
                return _commandOKAdd ?? new RelayCommand(obj => MessageBox.Show("Button OK ADD is not working"));
            }
            set { _commandOKAdd = value; }
        }
        
        RelayCommand _commandWithDraw;
        public RelayCommand CommandWithDraw
        {
            get
            {
                return _commandWithDraw ?? new RelayCommand(obj => MessageBox.Show("Button Withdraw is not working"));
            }
            set { _commandWithDraw = value; }
        }
        
        RelayCommand _commandRefill;
        public RelayCommand CommandRefill
        {
            get
            {
                return _commandRefill ?? new RelayCommand(obj => MessageBox.Show("Button Refill is not working"));
            }
            set { _commandRefill = value; }
        }
        RelayCommand _commandButtonWithdraw;
        public RelayCommand CommandButtonWithdraw
        {
            get
            {
                return _commandButtonWithdraw ?? new RelayCommand(obj => MessageBox.Show("Button Withdraw is not working"));
            }
            set { _commandButtonWithdraw = value; }
        }
        
        RelayCommand _commandTranfer;
        public RelayCommand CommandTranfer
        {
            get
            {
                return _commandTranfer ?? new RelayCommand(obj => MessageBox.Show("Button Transfer is not working"));
            }
            set { _commandTranfer = value; }
        }
        RelayCommand _commandExitClient;
        public RelayCommand CommandExitClient
        {
            get
            {
                return _commandExitClient ?? new RelayCommand(obj => MessageBox.Show("Button ExitClient is not working"));
            }
            set { _commandExitClient = value; }
        }

        #endregion
        #region tools
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }

    class PanelTransaction:INotifyPropertyChanged
    {
        IBankClient _CommunicationWithBank;
        public Account AccountFrom { get; set; }
        public Account AccountTo { get; set; }

        private int _amountToTransfer;
        public int AmountToTransfer
        {
            get { return _amountToTransfer; }
            set { if (AccountFrom != null && AccountTo != null && value > 0 && value < AccountFrom.AccountGeneral) ; _amountToTransfer = value; OnPropertyChanged(); }
        }

        public PanelTransaction(Account account, IBankClient Bank)
        {
            CommandTransferOK = new RelayCommand(Transfer);
            _CommunicationWithBank = Bank;
            ContainerTransferVisibility = Visibility.Collapsed;

        }
        private void Transfer(object obj)
        {
           bool result= _CommunicationWithBank.TransferBetween(AccountFrom, AccountTo, AmountToTransfer);
            if (result)
            {
                ContainerTransferVisibility = Visibility.Collapsed;
            }
            else
                MessageBox.Show("There is a mistake");


        }

        private Visibility _ContainerTransferVisibility = Visibility.Collapsed;
        public Visibility ContainerTransferVisibility { get { return _ContainerTransferVisibility; } set { _ContainerTransferVisibility = value; OnPropertyChanged(); } }

        RelayCommand _commandTransferOk;
        public RelayCommand CommandTransferOK
        {
            get
            {
                return _commandTransferOk ?? new RelayCommand(obj => MessageBox.Show("Button Transfer is not working"));
            }
            set { _commandTransferOk = value; }
        }




        #region tools
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}


