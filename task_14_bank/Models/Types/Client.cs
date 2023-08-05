using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using task_14_bank.Models.Support;
//using task_14_bank.Models.Types.AccountDescr;
using Accountlib;
using task_14_bank.Models.Types;
using task_14_bank.Models.Types.Bank_;

namespace task_14_bank.Models.Types
{
    [Serializable]
    public class Client : Person, IDataErrorInfo,ICloneable,IComparable
    {
        private int _phoneLength=11;
        private int _sIDLength = 4;
        private int _nIDLength = 6;
        string _phone;
        string _sID;
        string _nID;

        #region working with bank
        private IBankClient _CommunicationBank;
        public void GetMyAccounts()
        {
         
            _CommunicationBank = BankSystem.BankSber;
            Accounts.Clear();
            foreach(Account i in _CommunicationBank.GetClientAccounts(this))
                    Accounts.Add(i);
        }
        public ObservableCollection<Account> Accounts { get; set; }
        #endregion
        /// <summary>
        /// Person's Mobile Phone number
        /// </summary>
        public string Phone
        {
            get
            { return _phone; }
            set
            {
                if (_phone != value & IsPhone(value))
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Serial number of Person's ID
        /// </summary>
        public string SID { get { return _sID; } set { if (_sID != value & IsPassportSerial(value)) { _sID = value; OnPropertyChanged();  } } }


        /// <summary>
        /// Number of Person's ID
        /// </summary>
        public string NID { get { return _nID; } set { if (_nID != value & IsPassportNumber(value)) { _nID = value; OnPropertyChanged();  } } }

        public bool IsValidClient()
        {
            return (IsPassportNumber() && IsPassportSerial() && IsPhone() && (!String.IsNullOrEmpty(Name)) && (!String.IsNullOrEmpty(Surname)));
        }
        
        public bool IsPhone()
        {
            return (IsPhone(this.Phone));
        }
        public bool IsPhone(string Phone)
        {
            if (string.IsNullOrEmpty(Phone))
                return (false);
            Regex reg = new Regex(@"^[78]\d{10}");
            if (reg.IsMatch(Phone) & Phone.Length == _phoneLength)
            {               
                return true;
            }
            return false;
        }
        public bool IsPassportSerial()
        {
            return (IsPassportSerial(this.SID));
        }
        public bool IsPassportSerial(string SID)
        {
            if (string.IsNullOrEmpty(SID))
                return false;
            Regex reg = new Regex(@"^\d{" + _sIDLength.ToString() + "}");
            if (reg.IsMatch(SID) & SID.Length == _sIDLength)
            {
                return true;
            }
            return false;
        }
        public bool IsPassportNumber()
        {
            return (IsPassportNumber(this.NID));
        }
        public bool IsPassportNumber(string NID)
        {
            if (string.IsNullOrEmpty(NID))
                return false;
            Regex reg = new Regex(@"^\d{"+_nIDLength.ToString()+"}") ;
            if (reg.IsMatch(NID) & NID.Length == _nIDLength)
            {
                return true;
            }
            return false;
        }
        public Client(string surname, string name, string? thirdName, string phone, string sID, string nID) : base(surname, name, thirdName)
        {
            if (!string.IsNullOrEmpty(phone) & !string.IsNullOrEmpty(sID) & !string.IsNullOrEmpty(nID))
            {
                Phone = phone;
                SID = sID;
                NID = nID;
            }
            Accounts = new ObservableCollection<Account>();
        }
        // public Client() : this("EMptyClient", "EMptyClient", "EMptyClient", "81111111111", "1111", "111111")
        public Client() 
        {
            Accounts = new ObservableCollection<Account>();
        }
        public Client(Client tmpClient)
        {
            
                this.Name = tmpClient.Name;
                this.Surname = tmpClient.Surname;
                this.NID = tmpClient.NID;
                this.SID = tmpClient.SID;
                this.Phone = tmpClient.Phone;
                this.ThirdName = tmpClient.ThirdName;
            
        }
        public string this[string PropertyName]
        {
            get 
            {
                string error = String.Empty;
                switch (PropertyName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name)| Name.Contains("Den"))
                            error = "Name can not be empty";
                        break;
                    case "Surname":
                        if (string.IsNullOrEmpty(Surname))
                            error = "Surname can no be empty";
                        break;
                    case "Phone":
                        if (!IsPhone(Phone))
                            error = $"It doesn't look like a phone number";
                        break;
                    case "SID":
                        if (!IsPassportSerial(SID))
                            error = $"It doesn't look like a passport serial";
                        break;
                    case "NID":
                        if (!IsPassportNumber(NID))
                            error = $"It doesn't look like a passport number";
                        break;
                    default:
                        {
                            throw new ArgumentException($"Unrecognize Property {PropertyName}");
                         }

                }
                return error;
            }
        }

        public  string Error
        {
            get { throw new NotImplementedException(); }
        }
        public override bool Equals(object? obj)
        {
            if (obj is Client SeconClient)
                if ((this.SID == SeconClient.SID) && (this.NID == SeconClient.NID) &&
                    (this.ThirdName == SeconClient.ThirdName) && (this.Name == SeconClient.Name) && (this.Surname == SeconClient.Surname) && (this.Phone == SeconClient.Phone))
                    return (true);
            return false;
        }
        public override int GetHashCode()
        {
            return (this.SID+this.NID).GetHashCode();   
        }
        public override string ToString()
        {
            return new string($"Name is {Name} Surname is {Surname} ThirdName is {ThirdName} Phone is {Phone} ");
        }

        public object Clone()
        {
            return new Client(Name, Surname, ThirdName, Phone, SID, NID);
        }

        public int CompareTo(object? obj)
        {
            if ((obj != null) && (obj is Client tmpClient))
                return (string.Compare(this.Surname + this.Name + this.ThirdName, tmpClient.Surname + tmpClient.Name + tmpClient.ThirdName));            
            throw new NotImplementedException();
        }
    }
}
