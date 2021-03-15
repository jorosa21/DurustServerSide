using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.ModuleModel
{
    public class ModuleResponse
    {
            public string module_id { get; set; }
            public string module_name { get; set; }
            public string access_level_id { get; set; }
            public string created_by { get; set; }
            public bool active { get; set; }


    }


    public class ReportAccessResponse
    {
        public string report_id { get; set; }
        public string report_name { get; set; }
        public string access_level_id { get; set; }
        public string created_by { get; set; }
        public bool active { get; set; }


    }

    public class DataUploadAccessResponse
    {
        public string data_upload_id { get; set; }
        public string data_upload_name { get; set; }
        public string access_level_id { get; set; }
        public string created_by { get; set; }
        public bool active { get; set; }


    }
}
