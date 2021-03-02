using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Entities;

namespace IdentityService.Model
{
    public class AuthenticateResponse
    {

        public int Id { get; set; }
        public string email_address { get; set; }
        public string type { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool active { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            type = user.type;
            email_address = user.email_address;
            active = user.active;
            Token = token;
        }



    }
}
