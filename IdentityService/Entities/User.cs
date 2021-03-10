using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace IdentityService.Entities
{
    public class User
    {
        public string id { get; set; }
        public string guid { get; set; }
        public string username { get; set; }
        public string email_address { get; set; }
        public string routing { get; set; }
        public string type { get; set; }
        public bool email_verified { get; set; }
        public bool lock_account { get; set; }
        public bool active { get; set; }

        public string json { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
