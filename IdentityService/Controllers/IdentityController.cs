using IdentityService.Helper;
using IdentityService.Model;
using IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityService.Entities;
namespace IdentityService.Controllers
{
    public class IdentityController : APIController
    {
        private IUserService _userService;
        
        public IdentityController(IUserService userService)
        {
            _userService = userService;

        }



        [HttpPost("authenticateLogin")]
        public IActionResult AuthenticateLogin(AuthenticateRequest model)
        {
            var response = _userService.AuthenticateLogin(model);

            if (response.Id == 0)
                return BadRequest(new { message = response.type });

            return Ok(response);
        }



        [HttpPost("Registration")]
        public IActionResult  Register(Registration model)
        {
            var user = new User
            {
                email_address = model.email_address,
                Username = model.Username,
                active = model.active
            };
              _userService.Create(model);

            return Ok();
        }

        //public IdentityController(IRegistrationService registrationService, IOptions<AppSettings> appsetting)
        //{
        //    //_userService = userService;
        //    _RegistrationService = registrationService;

        //}

        //[HttpPost("Registration")]
        //public IActionResult Register(Registration model)
        //{
        //    var user = new User
        //    {
        //        Username = model.Username,
        //        email_address = model.email_address,
        //        active = model.active

        //    };
        //    var result = _RegistrationService.Registration(user);

        //    return ((IActionResult)result);
        //}




    }
}
