
namespace IdentityServer.Controllers
{

    using IdentityServer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]

    public class HomeController : ControllerBase
    {
        [Authorize]
      public IActionResult Get()
        {
            return Ok("Works");
        }
    }
}
