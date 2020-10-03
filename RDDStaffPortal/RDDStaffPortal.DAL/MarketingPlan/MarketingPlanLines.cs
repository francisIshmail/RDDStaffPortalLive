using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.MarketingPlan
{
   public class MarketingPlanLines
    {
        public string PlanId { get; set; }
public string LineRefNo { get; set; }
        public string VenderPONo { get; set; }
        public string SAPPONo { get; set; }
        public string ActivityDate { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Amount { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string Status { get; set; }
        public string Status1 { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public string ApproverRemark { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedOn { get; set; }
        public string Lastupdatedby { get; set; }
        public string Flag { get; set; }
    }
}
