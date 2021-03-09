using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterSettingService.Entities;

namespace MasterSettingService.Model.DropdownModel
{
    public class DropdownResponse
    {
        public int id { get; set; }

        public string description { get; set; }

        public string type_id { get; set; }


        //public DropdownResponse(DropdownSetting dropdown)
        //{
        //        id = dropdown.id;
        //        description = dropdown.description;

        //}

    }

    public class DropdownTypeResponse
    {
        public int id { get; set; }

        public string description { get; set; }



    }
}
