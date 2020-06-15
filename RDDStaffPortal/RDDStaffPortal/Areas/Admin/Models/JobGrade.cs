using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class JobGrade
    {
        public int JobGradeId { get; set; }
        public string JobGradeName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}