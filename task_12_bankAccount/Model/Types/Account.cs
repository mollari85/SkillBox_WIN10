using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_12_bankAccount.Model.Types
{
    abstract class Account
    {
        /// <summary>
        /// Low limit let which the account can't reach.
        /// </summary>
        decimal _lowLimit;

        /// <summary>
        /// Max amount that can be withdrawn for one time
        /// </summary>
        decimal _maxOneShotWithdraw;
        /// <summary>
        ///  Max amount that can be withdrawn for a day
        /// </summary>
        decimal _maxDailyWithdraw;
        /// <summary>
        /// amount which has been withdrawn
        /// </summary>
        decimal _currentDailyWithdraw;
        /// <summary>
        /// the date when last withdraw happens
        /// </summary>
        DateOnly _dateLastWithdraw;
        /// <summary>
        /// Max amount that can be added since opening the account, Null is not working option
        /// </summary>
        decimal? _maxLimitPeriodRefill;
        /// <summary>
        /// Max amount that can be added since opening the account, Null is not working option
        /// </summary>
        public decimal? MaxLimitPeriodRefill { get { return _maxLimitPeriodRefill; } private set { if (_maxLimitPeriodRefill != value && value > 0) { _maxLimitPeriodRefill = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Sum of all refilling for the period
        /// </summary>
        decimal _currentSumPeriodRefill;
        private  DateOnly _dateStartAccount;
        public DateOnly DateStartAccount { get;}
        /// <summary>
        /// for a test reason i allow set any value here
        /// </summary>
        public DateOnly DateEndAccount { get; set; }
        int? _periodInDays;
        public int? PeriodInDays { get { return _periodInDays; } private set { if (_periodInDays != value && value>0) { _periodInDays = value; OnPropertyChanged(); } } }
        /// <summary>
        /// Account General
        /// </summary>
        decimal _accountGeneral;
        public decimal AccountGeneral  { get { return _accountGeneral; } private set { if (_accountGeneral != value && _accountGeneral <= _lowLimit) { _accountGeneral = value; OnPropertyChanged(); } } }
        private readonly string _accountNumber;
        public string AccountNumber { get; }
 
        public Account(Guid AccountGuid,String AccountNumber,decimal LowLimit = 0, decimal MaxOneShotWithdraw = 0, decimal MaxDailyWithdraw = 0, decimal? MaxLimitPeriodRefill = null, int PeriodInDays = 30 )
        {
            
            _lowLimit = LowLimit;
            _maxOneShotWithdraw= MaxOneShotWithdraw;
            _maxDailyWithdraw= MaxDailyWithdraw;
            _maxLimitPeriodRefill= MaxLimitPeriodRefill;
            _periodInDays= PeriodInDays;
            _dateLastWithdraw = DateOnly.FromDateTime(DateTime.Today);
            DateStartAccount = DateOnly.FromDateTime(DateTime.Today);
            _accountGeneral = 0;

            this.AccountNumber = AccountNumber;

        }
        public bool WithDraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Withdraw amount can't be less or equal than zero");
            if (amount>_maxOneShotWithdraw)
                throw new ArgumentOutOfRangeException($"You can't one time withdraw more then limit.The limit is {_maxOneShotWithdraw}");
            if (AccountGeneral-amount<0-_lowLimit)
                throw new ArgumentOutOfRangeException($"You can't withdraw more then limit.Low limit is {_lowLimit}");
            if (_currentDailyWithdraw+amount>_maxDailyWithdraw)
                throw new ArgumentOutOfRangeException($"You can't withdraw more then daily limit. Daily limit is {_maxDailyWithdraw}");
            AccountGeneral -= amount;
            DailyWithdraw(amount);
            return true;
        }
        /// <summary>
        /// culculate the sum of daily withdrawing
        /// </summary>
        /// <param name="amount">amount of money to withdraw</param>
        /// <returns></returns>
        private decimal DailyWithdraw(Decimal amount)
        {
            _currentDailyWithdraw = (_dateLastWithdraw == DateOnly.FromDateTime(DateTime.Today)) ? _currentDailyWithdraw + amount : amount;
            return _currentDailyWithdraw;
        }

        public virtual bool Refill(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException("Refill amount can't be less or equal than zero");
            if (_currentSumPeriodRefill + amount < MaxLimitPeriodRefill)
                throw new ArgumentOutOfRangeException($"The account will overjump the limit if add the amount {amount}");
            AccountGeneral += amount;
            PeriodSumRefill(amount);
            return (true);
            

        }
        private decimal? PeriodSumRefill(decimal amount)
        {
            if (MaxLimitPeriodRefill == null)
                return null;
            if (_dateStartAccount.AddDays(-_periodInDays ?? 0) < DateOnly.FromDateTime(DateTime.Today)&& _currentSumPeriodRefill+amount<MaxLimitPeriodRefill)
                _currentSumPeriodRefill += amount;
            return (_currentSumPeriodRefill);
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
        public void SetMaxOneShotRefill(decimal maxDailyRefill)=>this._maxOneShotWithdraw = maxDailyRefill;
        public void SetMaxDailyWithdraw(decimal maxDailyWithdraw) => this._maxDailyWithdraw = maxDailyWithdraw;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
