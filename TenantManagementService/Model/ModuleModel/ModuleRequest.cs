using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.ModuleModel
{
    public class ModuleRequest
    {
        public string module_id { get; set; }
        public string access_level_id { get; set; }
        public string created_by { get; set; }


        public string instance_name { get; set; }
        public string user_name { get; set; }
        public string user_hash { get; set; }
    }
    public class ReportAccessRequest
    {
        public string report_id { get; set; }
        public string access_level_id { get; set; }
        public string created_by { get; set; }


        public string instance_name { get; set; }
        public string user_name { get; set; }
        public string user_hash { get; set; }
    }

    public class DataUploadAccessRequest
    {
        public string data_upload_id { get; set; }
        public string access_level_id { get; set; }
        public string created_by { get; set; }


        public string instance_name { get; set; }
        public string user_name { get; set; }
        public string user_hash { get; set; }
    }
}
