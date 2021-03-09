using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterSettingService.Model.CompanyModel
{
    public class CompanyIUResponse
    {

        public int company_id { get; set; }

        public string company_code { get; set; }

        public string company_name { get; set; }

        public string unit_floor { get; set; }

        public string building { get; set; }

        public string street { get; set; }

        public string barangay { get; set; }

        public string municipality { get; set; }

        public int city { get; set; }

        public int region { get; set; }

        public int country { get; set; }

        public string zip_code { get; set; }

        public string company_logo { get; set; }

        public int created_by { get; set; }

        public bool active { get; set; }

        public string guid { get; set; }

        public string instance_name { get; set; }

        public string user_name { get; set; }

        public string user_hash { get; set; }


    }

    public class BranchIUResponse
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


    }
}
