﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Entities;

namespace IdentityService.Model
{
    public class RegistrationResponse
    {

        public string email_address { get; set; }
        public string id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool active { get; set; }

        public RegistrationResponse(User user)
        {
            Username = user.username;
            email_address = user.email_address;
            id = user.id;
            active = user.active;
        }

    }
}
