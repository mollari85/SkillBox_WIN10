using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task_11_bank.Models.Types
{
    public class Client : Person
    {
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
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Serial number of Person's ID
        /// </summary>
        public string SID { get { return _sID; } set { if (_sID != value) { _sID = value; OnPropertyChanged(); } } }


        /// <summary>
        /// Number of Person's ID
        /// </summary>
        public string NID { get { return _nID; } set { if (_nID != value) { _nID = value; OnPropertyChanged(); } } }

        public bool SetPhone(string Phone)
        {
            Regex reg = new Regex(@"+7\d{10}");
            if (reg.IsMatch(Phone))
            {
                this.Phone = Phone;
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
        public Client() : this("EMptyClient", "EMptyClient", "EMptyClient", "EMptyClient", "EMptyClient", "EMptyClient")
        {
        }
    }
}
