﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantManagementService.Model.DropdownModel
{
    public class DropdownIUResponse
    {

        public int dropdown_id { get; set; }

        public int dropdown_type_id { get; set; }

        public string dropdown_description { get; set; }

        public Guid created_by { get; set; }

        public bool active { get; set; }
    }

    public class DropdownResponse
    {
        public int id { get; set; }

        public string description { get; set; }

        public int type_id { get; set; }


        public int to_id { get; set; }

        public string to_description { get; set; }

        public int to_type_id { get; set; }


        public int id_to { get; set; }

        public string description_to { get; set; }

        public int type_id_to { get; set; }


        public int to_id_to { get; set; }

        public string to_description_to { get; set; }

        public int to_type_id_to { get; set; }
    }


}
