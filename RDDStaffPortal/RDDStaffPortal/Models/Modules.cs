using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Models
{
    public class Modules
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}