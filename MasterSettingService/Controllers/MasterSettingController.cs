﻿using System;
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
        public List<DropdownResponse> Dropdown_List(string dropdowntype_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _masterServices.Dropdown_List(dropdowntype_id);
            return result;
        }



        [HttpGet("Dropdown_List_all")]
        public List<DropdownResponse> Dropdown_List_all(string dropdowntype_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _masterServices.Dropdown_List_all(dropdowntype_id);
            return result;
        }


        [HttpGet("Dropdown_entitlement")]
        public List<DropdownResponse> Dropdown_entitlement(string dropdowntype_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _masterServices.Dropdown_entitlement(dropdowntype_id);
            return result;
        }


        [HttpGet("Dropdowntype_List")]
        public List<DropdownTypeResponse> Dropdowntype_List()
        {

            var result = _masterServices.Dropdowntype_view();
            return result;
        }




        [HttpGet("Dropdown_fix_view")]
        public List<DropdownTypeResponse> Dropdown_fix_view(string active)
        {

            var result = _masterServices.Dropdown_fix_view(active);
            return result;
        }


        [HttpPost("DropdownIU")]
        public DropdownIUResponse DropdownIU(DropdownIURequest model)
        {

            var result = _masterServices.DropdownIU(model);
            return result;
        }


        [HttpGet("Menu_view")]
        public List<MenuViewResponse> Menu_view(string instance_name, string user_name, string user_hash, string access_level_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;
            
            var result = _masterServices.Menu_view(instance_name, user_name, user_hash,access_level_id);

            //result = JsonConvert.SerializeObject(result);
            return result;
        }



    }
}
