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
using MasterSettingService.Model.MenuViewModel;



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


        [HttpGet("Dropdown_entitlement")]
        public List<DropdownResponse> Dropdown_entitlement(string dropdowntype_id, string dropdown_type)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _masterServices.Dropdown_entitlement(dropdowntype_id, dropdown_type);
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


        [HttpPost("CompanyBranchIU")]
        public IActionResult CompanyBranchIU(CompanyBranchIU model)
        {

            var result = _masterServices.CompanyIU(model.company_IU);

            if (model.Branch_IU[0].instance_name is null)
            {
                model.Branch_IU[0].instance_name = result.instance_name;
                model.Branch_IU[0].username = result.user_name;
                model.Branch_IU[0].password = result.user_hash;
            }

            var branch_result = _masterServices.MultipleBranchIU(model.Branch_IU);

            return Ok();
        }



        [HttpGet("Menu_view")]
        public MenuViewResponse Menu_view()
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _masterServices.Menu_view();
            return result;
        }



    }
}
