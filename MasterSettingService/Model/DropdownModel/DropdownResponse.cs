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
        
        public string type_description { get; set; }

        public int type_id { get; set; }

        public bool active { get; set; }


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

    public class DropdownTypeResponse
    {
        public int id { get; set; }

        public string description { get; set; }



    }
}
