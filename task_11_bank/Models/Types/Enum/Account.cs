using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace task_11_bank.Models.Types.Enum
{
    abstract class Account : INotifyPropertyChanged
    {
        string _nameAccount;
        public string NameAccount { get { return _nameAccount; } private set { if (!string.IsNullOrEmpty(value) && (!string.IsNullOrWhiteSpace(value))) _nameAccount = value; } }

        /// <summary>
        /// Amount of money at the Account must be >=0
        /// </summary>
        Decimal _amount;
         public Decimal Amount { get { return _amount;}
           private set 
            { 
                if (value < 0) throw new ArgumentOutOfRangeException($"Account can't take value less than zero as {value}");
                _amount = value;
                OnPropertyChanged();
            }
        }


        public void SetAccountName(string Name) => NameAccount = Name;

        public void Add(decimal refill)
        {
            if (refill<0)
                throw new ArgumentOutOfRangeException($"The amount must be more than 0- but value is {refill}" );
            this.Amount += refill;  
        }
        public void Withdraw(decimal withdraw)
        {
            if (withdraw < 0) throw new ArgumentOutOfRangeException($"The amount must be more than 0- but value is {withdraw}");
            if (Amount-withdraw < 0) throw new ArgumentOutOfRangeException($"it is not possible to withdraw({withdraw}) more that the account has ({Amount}).");
            this.Amount -= withdraw;    

        }
    

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
