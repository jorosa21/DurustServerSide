using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthService.Entities
{

    public class User
    {
        public int id { get; set; }
        public string guid { get; set; }
        public string username { get; set; }
        public string email_address { get; set; }
        public string routing { get; set; }
        public string type { get; set; }
        public bool email_verified { get; set; }
        public bool lock_account { get; set; }
        public bool active { get; set; }

        public int company_id { get; set; }
        public string company_code { get; set; }
        public string instance_name { get; set; }
        public string company_user_name { get; set; }
        public string company_user_hash { get; set; }

        public string json { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
