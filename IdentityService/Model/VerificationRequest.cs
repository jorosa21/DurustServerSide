using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Model
{
    public class VerificationRequest
    {
        [Required]
        public string guid { get; set; }

    }
}
