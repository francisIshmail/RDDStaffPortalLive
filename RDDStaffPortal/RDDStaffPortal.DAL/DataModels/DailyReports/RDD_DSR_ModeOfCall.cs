using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.DailyReports
{
   public partial class RDD_DSR_ModeOfCall
    {
        public int ID { get; set; }
        public string ModeOfCall { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool DeleteFlag { get; set; }

        public bool Saveflag { get; set; }

        public bool Editflag { get; set; }

        public string ActionType { get; set; }

        public string ErrorMsg { get; set; }
    }
}
