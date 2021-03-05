using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Controllers
{
    public class TenantManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
