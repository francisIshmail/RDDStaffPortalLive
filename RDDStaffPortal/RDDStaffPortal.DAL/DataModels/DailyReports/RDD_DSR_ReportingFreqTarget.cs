using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.DailyReports
{
  public partial  class RDD_DSR_ReportingFreqTarget
    {
        
        public bool Saveflag { get; set; }
        public bool Editflag { get; set; }
        public string ActionType { get; set; }
        public string Country { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> freqOfRptList { get; set; }
        public List<SelectListItem> SendReportList { get; set; }
        public List<SelectListItem> ReportMustReadList { get; set; }
        public string ErrorMsg { get; set; }
        public List<RDD_DSR_ReportingFreqTargetDetailnew> RDD_DSR_ReportingFreqTargetDetailnew { get; set; }
        public RDD_DSR_ReportingFreqTargetDetailnew RDD_DSR_ReportingFreqTargetDetail { get; set; }
    }

    public class RDD_DSR_ReportingFreqTargetDetailnew
    {
        public int TargetId { get; set; }
        public int EmpId { get; set; }
        public int DesigId { get; set; }
        public int VisitPerMonth { get; set; }
        public string freqOfRpt { get; set; }
        public string SendReportTo { get; set; }
        public string ReportMustReadBy { get; set; }
        public string countrycode { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
