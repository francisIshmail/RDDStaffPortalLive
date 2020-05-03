using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class EmploymentStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}