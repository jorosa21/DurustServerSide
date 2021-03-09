﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterSettingService.Model.CompanyModel
{
    public class CompanyIURequest
    {
        public int company_id { get; set; }

        public string company_code { get; set; }

        public string company_name { get; set; }

        public string unit_floor { get; set; }

        public string building { get; set; }

        public string street { get; set; }

        public string barangay { get; set; }

        public string municipality { get; set; }

        public string city { get; set; }

        public string region { get; set; }

        public int country { get; set; }

        public string zip_code { get; set; }

        public string company_logo { get; set; }

        public int created_by { get; set; }

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

        public string city { get; set; }

        public string region { get; set; }

        public int country { get; set; }

        public string zip_code { get; set; }

        public string company_logo { get; set; }

        public int created_by { get; set; }

        public bool active { get; set; }


    }

}
