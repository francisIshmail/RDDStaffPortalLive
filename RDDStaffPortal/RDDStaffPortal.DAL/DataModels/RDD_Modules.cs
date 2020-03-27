using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
    public class RDD_Modules
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string cssClass { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
