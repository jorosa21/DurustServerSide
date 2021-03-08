using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public class AuthenticateResponse
    {
        public int id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string email_address { get; set; }

        public string type { get; set; }

        public string guid { get; set; }

        public string routing { get; set; }

        public bool email_verified { get; set; }

        public bool lock_account { get; set; }

        public string Token { get; set; }

        public string json { get; set; }

        public bool active { get; set; }

        public int company_id { get; set; }

        public string company_code { get; set; }

        public string instance_name { get; set; }

        public string company_user_name { get; set; }

        public string company_user_hash { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            id = user.id;
            username = user.username;
            routing = user.routing;
            guid = user.guid;
            email_verified = user.email_verified;
            lock_account = user.lock_account;
            type = user.type;
            email_address = user.email_address;
            active = user.active;

            company_id = user.company_id;
            company_code = user.company_code;
            instance_name = user.instance_name;
            company_user_name = user.company_user_name;
            company_user_hash = user.company_user_hash;



            Token = token;
            json = user.json;
        }

    }
}
