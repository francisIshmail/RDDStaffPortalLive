using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public class RDD_LeaveRequest
    {
        public int LeaveRequestId { get; set; }        public int EmployeeId { get; set; }        public int LeaveTypeId { get; set; }
        public string Reason { get; set; }        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }        public int NoOfDays { get; set; }        public string LeaveStatus { get; set; }        public string ApproverRemarks { get; set; }
        public bool IsPrivateLeave { get; set; }        public bool IsDeleted { get; set; }        public string AttachmentUrl { get; set; }        public string CreatedBy { get; set; }        public DateTime? CreatedOn { get; set; }        public string LastUpdatedBy { get; set; }        public DateTime? LastUpdatedOn { get; set; }        public bool DeleteFlag { get; set; }        public bool Saveflag { get; set; }        public bool Editflag { get; set; }        public string ActionType { get; set; }        public string ErrorMsg { get; set; }
    }
}
