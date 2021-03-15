using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.Series
{
    public class SeriesResponse
    {
        public string module_id { get; set; }
        public string module_name { get; set; }
        public string prefix { get; set; }
        public int year { get; set; }
        public int series { get; set; }
        public int length { get; set; }
        public string series_code { get; set; }
        public bool active { get; set; }
        public string created_by { get; set; }

    }
}
