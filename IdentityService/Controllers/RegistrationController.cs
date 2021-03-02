using IdentityService.Entities;
using IdentityService.Model.IdentityRegistration;
using IdentityService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> userManager;

        protected RegistrationController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var user = new User
            {
                Username = model.Username,
                email_address = model.email_address,
                active = model.active

            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
