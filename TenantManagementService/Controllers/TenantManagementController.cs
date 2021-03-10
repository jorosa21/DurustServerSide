using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantManagementService.Model.CompanyModel;
using TenantManagementService.Services;

namespace TenantManagementService.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class TenantManagementController : Controller
    {



        private ITenantManagementServices _tenantmanagementServices;

        public TenantManagementController(ITenantManagementServices tenantmanagementServices)
        {

            _tenantmanagementServices = tenantmanagementServices;

        }



        [HttpPost("CompanyIU")]
        public CompanyIUResponse CompanyIU(CompanyIURequest model)
        {

            var result = _tenantmanagementServices.CompanyIU(model);
            return result;
        }


        [HttpPost("BranchIU")]
        public BranchIUResponse BranchIU(BranchIURequest model)
        {

            var result = _tenantmanagementServices.BranchIU(model);
            return result;
        }



        [HttpPost("CompanyBranchIU")]
        public IActionResult CompanyBranchIU(CompanyBranchIU model)
        {

            var result = _tenantmanagementServices.CompanyIU(model.company_IU);

            if (model.Branch_IU[0].instance_name is null)
            {
                model.Branch_IU[0].instance_name = result.instance_name;
                model.Branch_IU[0].username = result.user_name;
                model.Branch_IU[0].password = result.user_hash;
            }

            var branch_result = _tenantmanagementServices.MultipleBranchIU(model.Branch_IU);

            return Ok();
        }



    }
}
