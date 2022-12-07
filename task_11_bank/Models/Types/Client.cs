using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task_11_bank.Models.Types
{
    [Serializable]
    public class Client : Person, IDataErrorInfo
    {
        private int _phoneLength=11;
        private int _sIDLength = 4;
        private int _nIDLength = 6;
        string _phone;
        string _sID;
        string _nID;

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
        }
        // public Client() : this("EMptyClient", "EMptyClient", "EMptyClient", "81111111111", "1111", "111111")
        public Client() 
        {
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
      
       
    }
}
