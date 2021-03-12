using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.DropdownModel
{
    public class DropdownIURequest
    {
        public int dropdown_id { get; set; }

        public int dropdown_type_id { get; set; }

        public string dropdown_description { get; set; }

        public string created_by { get; set; }

        public bool active { get; set; }

        public string instance_name { get; set; }

        public string user_name { get; set; }

        public string user_hash { get; set; }


    }
    public class DropdownRequest
    {

        public int dropdown_type_id { get; set; }

        public string dropdown_type { get; set; }

    }
}
