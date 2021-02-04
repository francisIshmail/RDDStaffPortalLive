using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public class RDD_LeaveApproval
    {
        public int LeaveRequestApprovalId { get; set; }
        public Int32 EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int LeaveRequestId { get; set; }  
        public int BackUp1Id { get; set; }
        public int BackUp2Id { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal NoOfDays { get; set; }        
        public int ManagerId { get; set; }
        public string ManagerType { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApproverRemarks { get; set; }
        public string Reason { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public List<SelectListItem> EmployeeLists { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }
        public List<GetWeeklyOff> WeeklyOffDays { get; set; }
        public bool DeleteFlag { get; set; }

        public bool Saveflag { get; set; }

        public bool Editflag { get; set; }

        public string ActionType { get; set; }

        public string ErrorMsg { get; set; }
    }
    public class Rdd_comonDrops
    {
        public string Code { get; set; }
        public string CodeName{ get; set; }


    }



}
