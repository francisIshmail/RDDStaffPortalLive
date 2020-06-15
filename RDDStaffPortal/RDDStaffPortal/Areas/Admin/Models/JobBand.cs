using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class JobBand
    {

        public int JobBandId { get; set; }
        public string JobBandName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}