using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "saideep_chhetri@yahoo.com";
        public string MailFromAddress = "saideep_chhetri@yahoo.com";
        public bool UseSsl = true;
        public string Username = "saideep@saideeprajchhetri.com";
        public string Password = "smarterasp.net4153"; // Create own google app password
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
    }
}
