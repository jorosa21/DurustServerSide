using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Entities;

namespace IdentityService.Model
{

    public class VerificationResponse
    {

        public string guid { get; set; }

        public VerificationResponse(User user)
            {

                guid = user.guid;

            }
        }
}
