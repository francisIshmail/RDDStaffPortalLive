using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.SAP
{
    public class RDD_PDCConfigSetUp
    {
        public int PDCConfigId { get; set; }
        public int PDCMaxAllowableDays { get; set; }
        public string AllowSORforOSAmount { get; set; }
        public int DaysToWaitForOriginalPDC { get; set; }
        public string AuthUsersForBankGuarantee { get; set; }
        public string AuthUsersForSecurityCheque { get; set; }
        public List<ChequeBounceReason> ChequeBounceReasonList { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public int ID { get; set; }
        public bool Saveflag { get; set; }
        public bool Editflag { get; set; }
        public string ActionType { get; set; }
        public string ErrorMsg { get; set; }
    }
    public partial class ChequeBounceReason
    {
        public int ReasonId { get; set; }
        public string Reason { get; set; }
        public int BlockCustomerAccount { get; set; }
        
    }
}
