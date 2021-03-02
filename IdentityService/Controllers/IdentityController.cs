using IdentityService.Helper;
using IdentityService.Model;
using IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Model.IdentityRegistration;
using Microsoft.AspNetCore.Identity;
using IdentityService.Entities;
using System.Net;

namespace IdentityService.Controllers
{
    public class IdentityController : APIController
    {
        private IUserService _userService;
        //private IRegistrationService _RegistrationService;
        //private readonly UserManager<User> userManager;

        //protected IdentityController(UserManager<User> userManager)
        //{

        //}


        public IdentityController(IUserService userService)
        {
            _userService = userService;
            //_RegistrationService = registrationService;

        }


        //public IActionResult Register(AuthenticateRequest model)
        //{
        //    var user = new User
        //    {
        //        Username = model.Username,
        //        email_address = model.email_address,
        //        active = model.active

        //    };
        //    var result = _userService.Register(user);

        //    return ((IActionResult)result);
        //}



        [HttpPost("authenticateLogin")]
        public IActionResult AuthenticateLogin(AuthenticateRequest model)
        {
            var response = _userService.AuthenticateLogin(model);

            if (response.Id == 0)
                return BadRequest(new { message = response.type});

            return Ok(response);
        }

   


    }
}
