using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public class AuthenticateRequest
    {

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string company_code { get; set; }
    }
}
