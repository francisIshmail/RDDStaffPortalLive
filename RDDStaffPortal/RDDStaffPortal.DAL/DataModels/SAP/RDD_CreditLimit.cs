using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.SAP
{
    public class RDD_CreditLimit
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string CAUserName { get; set; }
        public string CAtempCLLimit { get; set; }
        public string CMUserName { get; set; }
        public string CMtempCLLimit { get; set; }
        public string CRUserName { get; set; }
        public string CRtempCLLimit { get; set; }
        public string HOFUserName { get; set; }
        public string HOFtempCLLimit { get; set; }
        public string HOISUserName { get; set; }
        public string COOUserName { get; set; }
        public string CEOUserName { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public int IDe{ get; set;}
      
        public bool Saveflag { get; set; }

        public bool Editflag { get; set; }

        public string ActionType { get; set; }

        public string ErrorMsg { get; set; }
    }
}
