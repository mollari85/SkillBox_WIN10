﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using task_12_bank.Models.Types;
using task_12_bank.Models.Support;
using task_12_bank.Tools;
using System.Windows;
using Microsoft.VisualBasic;
using task_12_bank.Models.Types.Bank_;
using System.ComponentModel;
using task_12_bank.Models.Types.AccountDescr;
using task_12_bank.Models.Types.Enum;
using System.Runtime.CompilerServices;
using task_12_bankAccount.Model.Types;

namespace task_12_bank.ViewModels
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

        #region Support property for the View
        private Visibility _containerNewAccount = Visibility.Collapsed;
        public Visibility ContainerNewAccount { get { return _containerNewAccount; } set { _containerNewAccount = value; OnPropertyChanged(); } }

        private Visibility _containerRefill = Visibility.Collapsed;
        public Visibility ContainerRefill { get { return _containerRefill; } set { _containerRefill = value; OnPropertyChanged(); } }

        private Visibility _containerWithdraw = Visibility.Collapsed;
        public Visibility ContainerWithdraw { get { return _containerWithdraw; } set { _containerWithdraw = value; OnPropertyChanged(); } }
        #endregion

        #region test area
        public ObservableCollection<TestClass> ListTest = new ObservableCollection<TestClass>();
        public ObservableCollection<TestClass> ListTestProperty { get { return (ListTest); } set { ListTest = value; } }
        public class TestClass
        {
            public string Name { get; set; }
            public TestClass() => Name = "Vasya";
            public TestClass(string Name) => this.Name = Name;

            
            }
        #endregion
        public VM_ClientWorkingArea(Client Client, IBankClient BankSystem)
        {
            this.BankSystem = BankSystem;
            this.CurrentClient = Client;
            Client.GetMyAccounts();


            #region TEst area

            CurrentClient.Name = "ChangedDenisName";
            
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
        private void OpenNewAccount(object obj)
        {
            ContainerNewAccount = Visibility.Visible;

            
           
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
                return _commandWithDraw ?? new RelayCommand(obj => MessageBox.Show("Button OK ADD is not working"));
            }
            set { _commandWithDraw = value; }
        }
        
        RelayCommand _commandRefill;
        public RelayCommand CommandRefill
        {
            get
            {
                return _commandRefill ?? new RelayCommand(obj => MessageBox.Show("Button OK ADD is not working"));
            }
            set { _commandRefill = value; }
        }
        RelayCommand _commandButtonWithdraw;
        public RelayCommand CommandButtonWithdraw
        {
            get
            {
                return _commandButtonWithdraw ?? new RelayCommand(obj => MessageBox.Show("Button OK ADD is not working"));
            }
            set { _commandButtonWithdraw = value; }
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

}


