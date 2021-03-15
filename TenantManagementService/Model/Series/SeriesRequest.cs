using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.Series
{
    public class SeriesRequest
    {
        public string created_by { get; set; }
    }
    public class SeriesIURequest
    {
        public string module_id { get; set; }

        public string series { get; set; }

        public string created_by { get; set; }

        public string prefix { get; set; }

        public int year { get; set; }

        public int length { get; set; }

        public bool active { get; set; }
    }
}
