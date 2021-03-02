﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace IdentityService.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string email_address { get; set; }
        public string type { get; set; }
        public bool active { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}