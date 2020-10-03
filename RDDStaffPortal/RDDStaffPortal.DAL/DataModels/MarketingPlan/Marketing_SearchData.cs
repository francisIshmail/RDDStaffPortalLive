using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.MarketingPlan
{
   public  class Marketing_SearchData
    {
        public int Planid { get; set; }
       public string SourceOfFund { get; set; }
         public string CountryName{ get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Vendor { get; set; }
        public string VendorApprovedAmt { get; set; }
        public string RDDApprovedAmt { get; set; }
        public string UsedAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string BalanceFromApp { get; set; }
        public string Description { get; set; }
        public string  ApprovedBy { get; set; }
        public string planStatus { get; set; }
        public string ApprovalStatus { get; set; }
        public string CreatedOn { get; set; }
    }
}
