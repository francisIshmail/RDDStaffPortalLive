using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class Departments
    {

        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}