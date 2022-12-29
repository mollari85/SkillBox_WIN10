using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bank.Models.Types.AccountDescr
{
    public abstract class Account:INotifyPropertyChanged, IComparable
    {
        // public static List<Account> AllAccounts;
        public AccountLimits Limit { get; set; }
        
        /// <summary>
        /// amount which has been withdrawn
        /// </summary>
        decimal _currentDailyWithdraw;
        /// <summary>
        /// amount which has been withdrawn
        /// </summary>
        public decimal CurrentDailyWithdraw
        { get { return _currentDailyWithdraw; } set { if (value > 0) _currentDailyWithdraw = value; OnPropertyChanged(); } }
        /// <summary>
        /// the date when last withdraw happens
        /// </summary>
        DateOnly _dateLastWithdraw;
     /// <summary>
        /// Sum of all refilling for the period
        /// </summary>
        decimal _currentSumPeriodRefill;
        private DateOnly _dateStartAccount;
        public DateOnly DateStartAccount { get { return _dateStartAccount; } private set { _dateStartAccount = value; } }
        /// <summary>
        /// for a test reason i allow set any value here
        /// </summary>
        public DateOnly DateEndAccount { get; set;}
        int? _periodInDays;
        public int? PeriodInDays { get { return _periodInDays; } private set { if (_periodInDays != value && value > 0) { _periodInDays = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Account General
        /// </summary>
         decimal _accountGeneral;
         public decimal AccountGeneral { get { return _accountGeneral; } private  set { if (_accountGeneral != value && value >= Limit.LowLimit) { _accountGeneral = value; OnPropertyChanged(); } } }


        private readonly string _accountNumber;
        public string AccountNumber { get; }

        public Guid PersonID {get;}
        public String Description { get; set; }
        public Account(Guid PersonID, string AccountNumber, decimal LowLimit = 0, decimal MaxOneShotWithdraw = 0, decimal MaxDailyWithdraw = 0, decimal? MaxLimitPeriodRefill = null, int? PeriodInDays = 30)
        {
            Limit = new AccountLimits();
            this.DateStartAccount = DateOnly.FromDateTime(DateTime.Today);
            _dateLastWithdraw = DateOnly.FromDateTime(DateTime.Today);
            this.Limit.LowLimit = LowLimit;
            this.Limit.MaxOneShotWithdraw = MaxOneShotWithdraw;
           this.Limit.MaxDailyWithdraw = MaxDailyWithdraw;
            this.Limit.MaxLimitPeriodRefill = MaxLimitPeriodRefill;
            this.PeriodInDays = PeriodInDays;
            _dateLastWithdraw = DateOnly.FromDateTime(DateTime.Today);
            this.PersonID = PersonID;
            this.AccountNumber = AccountNumber;
            Description = "Common Account";
        }
        public bool WithDraw(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Withdraw amount can't be less or equal than zero");
            if (amount > Limit.MaxOneShotWithdraw)
                throw new ArgumentOutOfRangeException($"You can't one time withdraw more then limit.The limit is {Limit.MaxOneShotWithdraw}");
            if (AccountGeneral - amount < Limit.LowLimit)
                throw new ArgumentOutOfRangeException($"You can't withdraw more than limit.Low limit is {Limit.LowLimit}");
            if (_currentDailyWithdraw + amount > Limit.MaxDailyWithdraw)
                throw new ArgumentOutOfRangeException($"You can't withdraw more then daily limit. Daily limit is {Limit.MaxDailyWithdraw}");
            AccountGeneral -= amount;
            DailyWithdraw(amount);
            return true;
        }
        /// <summary>
        /// culculate the sum of daily withdrawing
        /// </summary>
        /// <param name="amount">amount of money to withdraw</param>
        /// <returns></returns>
        private decimal DailyWithdraw(decimal amount)
        {
            _currentDailyWithdraw = _dateLastWithdraw == DateOnly.FromDateTime(DateTime.Today) ? _currentDailyWithdraw + amount : amount;
            return _currentDailyWithdraw;
        }

        public bool Refill(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Refill amount can't be less or equal than zero");
            if (_currentSumPeriodRefill + amount > Limit.MaxLimitPeriodRefill)
                throw new ArgumentOutOfRangeException($"The account will overjump the limit if add the amount {amount}");
             AccountGeneral +=  (decimal)amount;
            PeriodSumRefill(amount);
            return true;
        }
        private decimal? PeriodSumRefill(decimal amount)
        {
            if (Limit.MaxLimitPeriodRefill == null)
                return null;
            if (_dateStartAccount.AddDays(-_periodInDays ?? 0) <= DateOnly.FromDateTime(DateTime.Today) && _currentSumPeriodRefill + amount < Limit.MaxLimitPeriodRefill)
                _currentSumPeriodRefill += amount;
            return _currentSumPeriodRefill;
        }
        public void SetPeriodInDays(int days)
        {
            if (days > 0)
                _periodInDays = days;
        }
        public void SetDateStartAccount(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.Now))
                _dateStartAccount = date;
        }
        public void SetMaxOneShotRefill(decimal maxDailyRefill) => this.Limit.MaxOneShotWithdraw = maxDailyRefill;
        public void SetMaxDailyWithdraw(decimal maxDailyWithdraw) => Limit.MaxDailyWithdraw = maxDailyWithdraw;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        #region override
        public override string ToString()
        {
            return new string($"MaxDaily Withdraw={Limit.MaxDailyWithdraw} \n\r" +
                $" Account Number={AccountNumber}\n\r" +
                $"Accont MaxOneshotWithdraw={Limit.MaxOneShotWithdraw}"
                );
        }

        public int CompareTo(object? obj)
        {
            if ((obj != null) && (obj is Account account))
            return (string.Compare(this.AccountNumber, account.AccountNumber));
            throw new ArgumentException($"Account compare requires type {typeof(Account)}");
           
        }
        public int CompareTo(Account account)
        {
            return (string.Compare(this.AccountNumber, account.AccountNumber));

        }
        public override bool Equals(object? obj)
        {
            if (obj is Account TmpAccount)
                return (this.AccountNumber == TmpAccount.AccountNumber && this.PersonID == TmpAccount.PersonID) ? true : false;
            return false;
        }

        public override int GetHashCode()
        {
            return (this.AccountNumber).GetHashCode();
        }
        #endregion

    }

}
