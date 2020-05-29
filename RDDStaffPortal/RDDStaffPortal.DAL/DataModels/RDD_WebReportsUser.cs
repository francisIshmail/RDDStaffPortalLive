using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  class RDD_WebReportsUser
    {
        public string userName { get; set; }
        public int fk_repTypeId { get; set; }
        public int fk_BUId { get; set; }
        public DateTime lastUpdated { get; set; }

        public List<WebRepList> WebRepLists { get; set; }
        public WebRepList WebRepList { get; set; }
        public bool saveflag { get; set; }
        public string errormsg { get; set; }

    }
    public class WebRepList
    {        
    public int fk_repTypeId { get; set; }
}
}
