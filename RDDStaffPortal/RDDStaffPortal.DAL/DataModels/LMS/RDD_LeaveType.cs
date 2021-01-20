using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public partial class RDD_LeaveType
    {
        public List<SelectListItem> CountryList { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveCode { get; set; }
        public string LeaveName { get; set; }
        public string AccruedRule { get; set; }
        public decimal AccruedDays { get; set; }
        public string MinimumLeaveBalanceCondition { get; set; }
        public bool IsLeaveLapseAtEndOfYear { get; set; }
        public string MaximumLeavecarryForwardToNextYear { get; set; }
        public decimal AllowMaximumNegativeLeaveDays { get; set; }
        public bool IsDeleted { get; set; }
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
    public partial class GetCountryDetail
    {
        public string CountryCode { get; set; }
        public string Country { get; set; }
    }
    
}
