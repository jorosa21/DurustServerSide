using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterSettingService.Model.CompanyModel
{
    public class CompanyIURequest
    {
        public int companyID { get; set; }

        public string companyName { get; set; }

        public string companyCode { get; set; }

        public string unit { get; set; }

        public string building { get; set; }

        public string street { get; set; }

        public string barangay { get; set; }

        public string municipality { get; set; }

        public int city { get; set; }

        public int region { get; set; }

        public int selectedCompanyCountry { get; set; }

        public string zipCode { get; set; }

        public string img { get; set; }

        public int createdBy { get; set; }

        public bool active { get; set; }


    }

    public class BranchIURequest
    {
        public int branch_id { get; set; }

        public string branch_code { get; set; }

        public string branch_name { get; set; }

        public string unit_floor { get; set; }

        public string building { get; set; }

        public string street { get; set; }

        public string barangay { get; set; }

        public string municipality { get; set; }

        public int city { get; set; }

        public int region { get; set; }

        public int country { get; set; }

        public string zip_code { get; set; }

        public string email_address { get; set; }

        public string sss { get; set; }

        public string philhealth { get; set; }

        public string tin { get; set; }

        public string rdo { get; set; }

        public string pagibig { get; set; }

        public string pagibig_branch { get; set; }

        public string ip_address { get; set; }

        public int industry { get; set; }

        public int bank_id { get; set; }

        public string bank_account { get; set; }

        public int company_id { get; set; }

        public string guid { get; set; }

        public int created_by { get; set; }

        public bool active { get; set; }

        public string instance_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }

    public class CompanyBranchIU
    {

        public CompanyIURequest company_IU { get; set; }

        public BranchIURequest[] Branch_IU { get; set; }
    }
}


