using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_EmploymentStatus
    {

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
