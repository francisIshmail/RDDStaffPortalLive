using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public partial class RDD_LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeIde { get; set; }
        public int backupid { get; set; }
        public int backup2id { get; set; }
        public string Fullname { get; set; }
        public int LeaveTypeId { get; set; }
        public string Reason { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string NoOfDays { get; set; }
        public string LeaveStatus { get; set; }
        public string ApproverRemarks { get; set; }
        public bool IsPrivateLeave { get; set; }
        public bool IsDeleted { get; set; }
        public string AttachmentUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public List<SelectListItem> EmployeeLists { get; set; }
        public List<SelectListItem> EmployeeListsModal { get; set; }
        public List<LeaveRequestDetails> LeaveRequestDetailsList { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }
        public List<GetWeeklyOff> WeeklyOffDays { get; set; }
        public List<LeaveRequestApprover> LeaveRequestApproverList { get; set; }
        public bool HR { get; set; }
        public bool DeleteFlag { get; set; }

        public bool Saveflag { get; set; }

        public bool Editflag { get; set; }

        public string ActionType { get; set; }

        public string ErrorMsg { get; set; }
    }
    public partial class EmployeeList
    {
        public int EmployeeId { get; set; }
        public string EmployeeName{ get; set; }
    }
    public partial class EmployeeListsModal
    {
        public int EmployeeIde { get; set; }
        public string EmployeeName { get; set; }
    }
    public class Rdd_comonDrop
    {
        public string Code { get; set; }
        public string CodeName { get; set; }

        
    }

    public partial class LeaveTypeList
    {
        public int LeaveTypeId { get; set; }
        public string LeaveName { get; set; }
    }
    public partial class LeaveRequestApprover
    {
        public int LeaveRequestId { get; set; }
        public string CreatedBy { get; set; }
    }
    public partial class GetWeeklyOff
    {
        public string WeeklyOff { get; set; }
        public string LeaveRules { get; set; }
    }
    public partial class LeaveRequestDetails
    {
        public int LeaveRequestDetailId { get; set; }
        public int LeaveRequestId { get; set; }
        public string LeaveDate { get; set; }
        public string LeaveDayType { get; set; }
        public string LeaveDay { get; set; }
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
}
