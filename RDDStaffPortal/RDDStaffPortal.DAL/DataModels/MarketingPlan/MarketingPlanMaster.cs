using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.MarketingPlan
{
   public  class MarketingPlanMaster
    {
        public string SourceOfFund { get; set; }
public string RefNo { get; set; }
public string Country { get; set; }
        public string CountryName { get; set; }
        public string Vendor { get; set; }
        public int VendorApprovedAmt { get; set; }
        public int RDDApprovedAmt { get; set; }
        public int BalanceAmount { get; set; }
        public int BalanceFromApp { get; set; }
        public int UsedAmount { get; set; }
        public string Description { get; set; }
        public string planStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string ApproverRemark { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string IsDraft { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedOn { get; set; }
        public string LastUpdateBy { get; set; }
    }
}
