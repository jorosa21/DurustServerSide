using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Entities;

namespace IdentityService.Model
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

        public bool active { get; set; }


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
            Token = token;
        }



    }
}
