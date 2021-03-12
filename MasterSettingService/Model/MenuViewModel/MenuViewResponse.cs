using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterSettingService.Model.MenuViewModel
{
    public class MenuViewResponse
    {
        public string ordey_by { get; set; }
        public int module_id { get; set; }
        public int parent_module_id { get; set; }
        public string module_name { get; set; }
        public string classes { get; set; }
        public string module_type { get; set; }
        public string link { get; set; }
    }
}
