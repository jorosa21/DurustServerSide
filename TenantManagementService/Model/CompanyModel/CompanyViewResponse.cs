using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.CompanyModel
{
    public class CompanyViewResponse
    {
        public string company_id { get; set; }

        public string company_code { get; set; }

        public string company_name { get; set; }

        public string street { get; set; }

        public string barangay { get; set; }

        public string municipality { get; set; }

        public string city_id { get; set; }

        public string city { get; set; }

        public string region_id { get; set; }

        public string region { get; set; }

        public string country { get; set; }

        public string country_id { get; set; }

        public string zip_code { get; set; }

        public string company_logo { get; set; }

        public string instance_name { get; set; }

        public string user_name { get; set; }

        public string user_hash { get; set; }

        public string created_by { get; set; }

        public string username { get; set; }

        public string active { get; set; }

        public string guid { get; set; }

    }
}
