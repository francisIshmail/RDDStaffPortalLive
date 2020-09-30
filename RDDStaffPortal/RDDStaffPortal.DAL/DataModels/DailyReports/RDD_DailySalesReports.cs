using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.DailyReports
{
    public partial class RDD_DailySalesReports
    {

        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
        public string ActionType { get; set; }
        public string ErrorMsg { get; set; }
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime? FromDate { get; set; }
        public string VisitType { get; set; }
        public bool IsNewPartner { get; set; }
        public string Country { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public string CardCode { get; set; }
        [StringLength(510, ErrorMessage = "Comany cannot be longer than 510 characters.")]
        public string Company { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        [StringLength(300, ErrorMessage = "Contact Person cannot be longer than 300 characters.")]
        public string PersonMet { get; set; }
        [StringLength(200, ErrorMessage = "Email cannot be longer than 200 characters.")]
        public string Email { get; set; }
        [StringLength(200, ErrorMessage = "Designation cannot be longer than 200 characters.")]
        public string Designation { get; set; }
        [StringLength(20, ErrorMessage = "Contact No cannot be longer than 20 characters.")]
        public string ContactNo { get; set; }
        [StringLength(100, ErrorMessage = "BU cannot be longer than 100 characters.")]
        public string BU { get; set; }
        [StringLength(2000, ErrorMessage = "Discussion cannot be longer than 2000 characters.")]
        public string Discussion { get; set; }
        [Range(0,999999999.99, ErrorMessage = "Amount cannot be longer than 9 characters.")]
        public decimal ExpectedBusinessAmt { get; set; }
        public string CallStatus { get; set; }
        public List<SelectListItem> CallStatusList { get; set; }

        public string ModeOfCall { get; set; }
        public List<SelectListItem> ModeOfCallList { get; set; }
        public string NextAction { get; set; }
        public List<SelectListItem> NextActionList { get; set; }
        [StringLength(2000, ErrorMessage = "Feedback cannot be longer than 2000 characters.")]
        public string Feedback { get; set; }
        [StringLength(200, ErrorMessage = "Forward Email cannot be longer than 200 characters.")]
        public string ForwardCallToEmail { get; set; }
        [StringLength(2000, ErrorMessage = "Forward Remark cannot be longer than 2000 characters.")]
        public string ForwardRemark { get; set; }
        public string ForwardCallCCEmail { get; set; }
        public string Priority { get; set; }
        public bool IsDraft { get; set; }
        public DateTime? NextReminderDate { get; set; }
        public DateTime? DocDraftDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime ReminderDate { get; set; }
        [StringLength(2000, ErrorMessage = "Reminder Description cannot be longer than 2000 characters.")]
        public string ReminderDesc { get; set; }
        public bool IsRead { get; set; }
        public string ReportReadBy { get; set; }
        public DateTime? ReportReadOn { get; set; }
        public string Comments { get; set; }
        public DateTime ActualVisitDate { get; set; }
        public bool IsRptSentToManager { get; set; }
    }


    public partial class RDD_DSR_NewResellerEntry
    {
        public int ResellerId { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string CardCode { get; set; }
        public string NewResellerName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }


    public partial class RDD_DailySalesReportComment {
        public int visitid { get; set; }
        public string Flag { get; set; }
        public string ReportReadBy { get; set; }
        public DateTime ReportReadOn { get; set; }
        public string Comments { get; set; }
        public string PM_Comments { get; set; }
        public string PM_ReportReadBy { get; set; }
        public DateTime PM_ReportReadOn { get; set; }
         
    }

}
