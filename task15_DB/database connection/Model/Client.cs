using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_connection.Model
{
    internal class Client
    {
        public int ID { get; set; }

        public String Name { get; set; }
        public String Surname { get; set; }
        public String SecondName { get; set; }
        public String PhoneNumber { get; set; }
        public String Mail { get; set; }
        public Client()
        {
            ID =1;
            Name = "VVV";
            Surname = "DD";
            SecondName = "ss";
            PhoneNumber = "WWW";
            Mail = "SSSS";
        }
        public Client(int ID,String Name,String Surname, String SecondName,string PhoneNumber, string Mail)
        {
            this.ID = ID;
            this.Name = Name;
            this.Surname = Surname;
            this.SecondName = SecondName;
            this.PhoneNumber = PhoneNumber;
            this.Mail = Mail;
        }
    }
}
