using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace database_connection.Model
{
    internal class Good
    {
        public int ID { get; set; }
        public string Mail { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

        public Good()
        {
            Code = 0;
            Mail = "testMail";
            ID= 0;
            Name = "Test";
        }
        public Good(int Id, String Mail, int Code, string Name)
        {
            this.Code =Code ;
            this.Mail = Mail;
            this.ID = Id;
            this.Name = Name;
        }
    }
}
