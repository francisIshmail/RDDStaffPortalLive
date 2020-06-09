using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_QuickLinks
    {

    public int  QuickLinkId { get; set; }
        public string UserName{ get; set; }
    public string URL { get; set; }
    public string FormName{ get; set; }
    public DateTime CreatedOn{ get; set; }
    public string CreatedBy{ get; set; }
    public DateTime LastUpdatedOn{ get; set; }
    public string LastUpdatedBy{ get; set; }
        public bool Saveflag { get; set; }

        public string ErrorMsg { get; set; }
    }
}
