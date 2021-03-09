using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterSettingService.Model.DropdownModel;
using MasterSettingService.Model.CompanyModel;
using MasterSettingService.Services;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Data;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.ServiceModel;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterSettingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterSettingController : ControllerBase
    {

        private IMasterSettingServices _masterServices;

        public MasterSettingController(IMasterSettingServices masterService)
        {

            _masterServices = masterService;

        }


        [HttpGet("Dropdown_List")]
        public List<DropdownResponse> Dropdown_List(string dropdowntype_id, string dropdown_type)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _masterServices.Dropdown_List(dropdowntype_id, dropdown_type);
            return result;
        }


        [HttpGet("Dropdowntype_List")]
        public List<DropdownTypeResponse> Dropdowntype_List()
        {

            var result = _masterServices.Dropdowntype_view();
            return result;
        }


        [HttpPost("CompanyIU")]
        public CompanyIUResponse CompanyIU(CompanyIURequest model)
        {

            var result = _masterServices.CompanyIU(model);
            return result;
        }


        [HttpPost("BranchIU")]
        public BranchIUResponse BranchIU(BranchIURequest model)
        {

            var result = _masterServices.BranchIU(model);
            return result;
        }



        [HttpPost("DropdownIU")]
        public DropdownIUResponse DropdownIU(DropdownIURequest model)
        {

            var result = _masterServices.DropdownIU(model);
            return result;
        }


        //[HttpPost("CompanyBranchIU")]
        //public IActionResult CompanyBranchIU(CompanyIURequest model,BranchIURequest branchmodel)
        //{

        //    var result = _masterServices.CompanyIU(model);

        //    branchmodel.instance_name = result.instance_name;
        //    branchmodel.username = result.user_name;
        //    branchmodel.password = result.user_hash;

        //    var branch_result = _masterServices.BranchIU(branchmodel);
            
        //    return Ok();
        //}

    }
}
