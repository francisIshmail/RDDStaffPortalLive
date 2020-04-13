using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  class RDD_DashBoard
    {
        public int DashboardId { get; set; }
        public string DashboardName { get; set; }
        public int Levels { get; set; }
        public int ModuleId { get; set; }
        public string cssClass { get; set; }
        public string URL { get; set; }
        public int DisplaySeq { get; set; }
        public bool IsDefault {get; set; }
         public bool IsDeleted {get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; } 
    }
}
