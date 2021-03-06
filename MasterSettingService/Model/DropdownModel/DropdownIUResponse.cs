﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterSettingService.Model.DropdownModel
{
    public class DropdownIUResponse
    {

        public int dropdown_id { get; set; }

        public int dropdown_type_id { get; set; }

        public string dropdown_description { get; set; }

        public Guid created_by { get; set; }

        public bool active { get; set; }
    }
}
