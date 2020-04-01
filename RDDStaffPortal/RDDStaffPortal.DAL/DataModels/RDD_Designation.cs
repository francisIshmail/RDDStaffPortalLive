using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_Designation
    {
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
