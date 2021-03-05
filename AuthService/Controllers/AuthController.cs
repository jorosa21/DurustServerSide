using AuthService.Helper;
using AuthService.Model;
using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        private EmailSender email;
        private Default_Url url;

        public AuthController(IAuthService userService, IOptions<EmailSender> appSettings, IOptions<Default_Url> settings)
        {

            //this._settings = _audience;
            _authService = userService;

            email = appSettings.Value;
            url = settings.Value;
        }




        [HttpPost("authenticateLogin")]
        public IActionResult authenticateLogin(AuthenticateRequest model)
        {
            var response = _authService.AuthenticateLogin(model);
            return Ok(response);
        }

    }
}
