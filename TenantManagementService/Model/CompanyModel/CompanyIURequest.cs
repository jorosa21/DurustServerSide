using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.CompanyModel
{
    public class CompanyIURequest
    {
        public string companyID { get; set; }

        public string companyName { get; set; }

        public string companyCode { get; set; }

        public string unit { get; set; }

        public string building { get; set; }

        public string street { get; set; }

        public string barangay { get; set; }

        public string municipality { get; set; }

        public int SelectedCity { get; set; }

        public int SelectedRegion { get; set; }

        public int selectedCompanyCountry { get; set; }

        public string zipCode { get; set; }

        public string img { get; set; }

        public string createdBy { get; set; }

        public bool active { get; set; }


    }

    public class BranchIURequest
    {


        public string branchID { get; set; }

        public string bankAccount { get; set; }

        public string barangay { get; set; }

        public string branchName { get; set; }

        public string building { get; set; }

        public string municipality { get; set; }

        public string pagibig { get; set; }

        public string philhealth { get; set; }

        public int SelectedBank { get; set; }

        public int SelectedBranchCountry { get; set; }

        public int SelectedCity { get; set; }

        public int SelectedIndustry { get; set; }

        public int SelectedPCity { get; set; }

        public int SelectedPCode { get; set; }

        public int SelectedPRegion { get; set; }

        public int SelectedRdoBranch { get; set; }

        public int SelectedRdoOffice { get; set; }

        public int SelectedRegion { get; set; }

        public string sss { get; set; }

        public string street { get; set; }

        public string tin { get; set; }

        public string unit { get; set; }

        public string zipCode { get; set; }

        public string company_id { get; set; }

        public string guid { get; set; }

        public string CreatedBy { get; set; }

        public bool active { get; set; }

        public string instance_name { get; set; }
        public string user_name { get; set; }
        public string user_hash { get; set; }


        public IPIU[] iP_IU { get; set; }
        public ContactIU[] Contact_IU { get; set; }
        public EmailIU[] Email_IU { get; set; }

    }

    public class IPIU
    {
        public string branch_id { get; set; }

        public string description { get; set; }

        public string createdBy { get; set; }
    }


    public class ContactIU
    {
        public string branch_id { get; set; }

        public int id { get; set; }

        public string number { get; set; }

        public string createdBy { get; set; }
    }


    public class EmailIU
    {
        public string branch_id { get; set; }

        public int id { get; set; }

        public string email_address { get; set; }

        public string createdBy { get; set; }
    }

    public class CompanyBranchIU
    {

        public CompanyIURequest company_IU { get; set; }

        public BranchIURequest[] Branch_IU { get; set; }
    }





}
