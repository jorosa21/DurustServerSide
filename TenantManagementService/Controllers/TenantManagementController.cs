﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantManagementService.Model.CompanyModel;
using TenantManagementService.Model.DropdownModel;
using TenantManagementService.Services;

namespace TenantManagementService.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class TenantManagementController : Controller
    {



        private ITenantManagementServices _tenantmanagementServices;

        public TenantManagementController(ITenantManagementServices tenantmanagementServices)
        {

            _tenantmanagementServices = tenantmanagementServices;

        }



        [HttpPost("CompanyIU")]
        public CompanyResponse CompanyIU(CompanyIURequest model)
        {
            

            var result = _tenantmanagementServices.CompanyIU(model);
            return result;
        }


        [HttpPost("BranchIU")]
        public BranchResponse BranchIU(BranchIURequest model)
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
                model.Branch_IU[0].username = result.username;
                model.Branch_IU[0].password = result.password;
            }
            model.Branch_IU[0].company_id = result.companyID;


            var branch_result = _tenantmanagementServices.MultipleBranchIU(model.Branch_IU);

            return Ok();
        }




        [HttpGet("Dropdown_List")]
        public List<DropdownResponse> Dropdown_List(string dropdowntype_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _tenantmanagementServices.Dropdown_List(dropdowntype_id);
            return result;
        }



        [HttpPost("DropdownIU")]
        public DropdownIUResponse DropdownIU(DropdownIURequest model)
        {

            var result = _tenantmanagementServices.DropdownIU(model);
            return result;
        }


    }
}
