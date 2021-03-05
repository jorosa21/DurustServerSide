using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Helper
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public class connectionString
    {
        public string _DB_Master { get; set; }

    }

    public class EmailSender
    {
        public string email_name { get; set; }
        public string email_username { get; set; }

        public string email_password { get; set; }

        public string host { get; set; }

        public int port { get; set; }

    }

    public class Default_Url
    {
        public string name { get; set; }
    }
}
