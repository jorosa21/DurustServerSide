using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantManagementService.Model.CompanyModel;
using TenantManagementService.Model.DropdownModel;
using TenantManagementService.Model.ModuleModel;
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


            if (result.companyID == null)
            {
                result.companyID = "0";
            }
            return result;
        }


        [HttpPost("BranchIU")]
        public BranchResponse BranchIU(BranchIURequest model)
        {

            var result = _tenantmanagementServices.BranchIU(model);

            if (result.branch_id == null)
            {
                result.branch_id = "0";
            }

            return result;
        }



        [HttpPost("CompanyBranchIU")]
        public CompanyBranchOutput CompanyBranchIU(CompanyBranchIU model)
        {
            CompanyBranchOutput res = new CompanyBranchOutput();
            var result = _tenantmanagementServices.CompanyIU(model.company_IU);




            if (result.companyID == null )
            {
               res.description = "Company data creation have a problem!";
                res.id = 0;
            }
            else
            {

                if (model.Branch_IU[0].instance_name is null)
                {
                    model.Branch_IU[0].instance_name = result.instance_name;
                    model.Branch_IU[0].user_name = result.username;
                    model.Branch_IU[0].user_hash = result.password;
                }
                model.Branch_IU[0].company_id = result.companyID;


                var branch_result = _tenantmanagementServices.MultipleBranchIU(model.Branch_IU);

                if (branch_result.branch_id == null)
                {

                    res.description = " Branch data creation have a problem!";
                    res.id = 0;
                }
                else
                {

                    res.description = "Saving Successful! ";
                    res.id = 1;
                }
            }
         

            return res;
        }



        [HttpGet("company_view_sel")]
        public List<CompanyResponse> company_view_sel(string company_id, string created_by)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _tenantmanagementServices.company_view_sel(company_id,created_by);
            return result;
        }


        [HttpGet("branch_view")]
        public List<BranchResponse> branch_view(string instance_name, string user_name, string user_hash, string company_id, string branch_id, string created_by)
        {

            var result = _tenantmanagementServices.branch_view(instance_name, user_name, user_hash, company_id, branch_id, created_by);
            return result;
        }

        [HttpGet("branch_ip_view")]
        public List<IPResponse> branch_ip_view(string instance_name, string user_name, string user_hash, string branch_id)
         {

            var result = _tenantmanagementServices.branch_ip_view(instance_name, user_name, user_hash,  branch_id);
            return result;
        }

    [HttpGet("branch_contact_view")]
        public List<ContactResponse> branch_contact_view(string instance_name, string user_name, string user_hash, string branch_id)
        {

            var result = _tenantmanagementServices.branch_contact_view(instance_name, user_name, user_hash, branch_id);
            return result;
        }

        [HttpGet("branch_email_view")]
        public List<EmailResponse> branch_email_view(string instance_name, string user_name, string user_hash, string branch_id)
        {

            var result = _tenantmanagementServices.branch_email_view(instance_name, user_name, user_hash, branch_id);
            return result;
        }



        [HttpGet("Dropdown_List")]
        public List<DropdownResponse> Dropdown_List(string instance_name, string user_name, string user_hash, string dropdowntype_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _tenantmanagementServices.Dropdown_List(instance_name, user_name, user_hash, dropdowntype_id);
            return result;
        }



        [HttpGet("Dropdown_List_all")]
        public List<DropdownResponse> Dropdown_List_all(string instance_name, string user_name, string user_hash, string dropdowntype_id)
        {
            //dropdowntype_id = dropdowntype_id == "null" ? "0" : dropdowntype_id;
            //dropdown_type = dropdown_type == "null" ? "" : dropdown_type;

            var result = _tenantmanagementServices.Dropdown_List_all(instance_name, user_name, user_hash, dropdowntype_id);
            return result;
        }



        [HttpPost("DropdownIU")]
        public DropdownIUResponse DropdownIU(DropdownIURequest model)
        {

            var result = _tenantmanagementServices.DropdownIU(model);
            return result;
        }


        [HttpPost("module_access_in")]
        public ModuleResponse module_access_in(ModuleRequest model)
        {

            var result = _tenantmanagementServices.module_access_in(model);
            return result;
        }



        [HttpPost("data_upload_access_in")]
        public DataUploadAccessResponse data_upload_access_in(DataUploadAccessRequest model)
        {

            var result = _tenantmanagementServices.data_upload_access_in(model);
            return result;
        }


        [HttpPost("report_access_in")]
        public ReportAccessResponse report_access_in(ReportAccessRequest model)
        {

            var result = _tenantmanagementServices.report_access_in(model);
            return result;
        }


    }   
}
