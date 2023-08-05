using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task_14_bank.Models.Types.AccountDescr___
{
    public class AccountLimits
    {
        /// Low limit let which the account can't reach.
        /// </summary>
        decimal _lowLimit;
        /// <summary>
        /// Low limit let which the account can't reach.
        /// </summary>
        public decimal LowLimit
        { get { return _lowLimit; } set { if (value > 0) _lowLimit = value; OnPropertyChanged(); } }
        /// <summary>
        /// Max amount that can be withdrawn for one time
        /// </summary>
        decimal _maxOneShotWithdraw;
        /// <summary>
        /// Max amount that can be withdrawn for one time
        /// </summary>
        public decimal MaxOneShotWithdraw
        { get { return _maxOneShotWithdraw; } set { if (value > 0) _maxOneShotWithdraw = value; OnPropertyChanged(); } }
        /// <summary>
        ///  Max amount that can be withdrawn for a day
        /// </summary>
        decimal _maxDailyWithdraw;
        /// <summary>
        ///  Max amount that can be withdrawn for a day
        /// </summary>
        public decimal MaxDailyWithdraw
        { get { return _maxDailyWithdraw; } set { if (value > 0) _maxDailyWithdraw = value; OnPropertyChanged(); } }
        /// <summary>
        /// Max amount that can be added since opening the account, Null is not working option
        /// </summary>
        decimal? _maxLimitPeriodRefill;
        /// <summary>
        /// Max amount that can be added since opening the account, Null is not working option
        /// </summary>
        public decimal? MaxLimitPeriodRefill { get { return _maxLimitPeriodRefill; } set { if (_maxLimitPeriodRefill != value && value > 0) { _maxLimitPeriodRefill = value; OnPropertyChanged(); } } }

        #region Support
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
