using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class Designations
    {
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}