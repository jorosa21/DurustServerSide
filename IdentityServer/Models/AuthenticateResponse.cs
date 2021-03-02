using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Entities;

namespace IdentityServer.Models
{
    public class AuthenticateResponse
    {
        private Users users;
        private object token;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Users user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }

        public AuthenticateResponse(Users users, object token)
        {
            this.users = users;
            this.token = token;
        }
    }
}
