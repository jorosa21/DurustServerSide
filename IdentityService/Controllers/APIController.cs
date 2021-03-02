using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Model.IdentityRegistration;

namespace IdentityService.Controllers
{


    [ApiController]
    [Route("[controller]")]

    public abstract class APIController: Controller
    {
    }
}
