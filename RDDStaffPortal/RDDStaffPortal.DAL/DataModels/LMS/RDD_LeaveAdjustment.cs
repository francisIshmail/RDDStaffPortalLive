using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Web.Mvc;


namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public partial class RDD_LeaveAdjustment
    {
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }
        public string LeaveName { get; set; }
        public string Country { get; set; }
        public string DeptId { get; set; }
        //public string CountryCode { get; set; }
        public string FullName { get; set; }
        public int LeaveLedgerId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        //public string Types_ { get; set; }
        public bool CreditDebit { get; set; }
        public decimal NoOfDays { get; set; }
        public string Remarks { get; set; }
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
    public partial class GetEmployeeDetails
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
    }
    public partial class GetCountryDetails
    {
        public string CountryCode { get; set; }
        public string Country { get; set; }
    }
    public partial class GetLeaveTypeDetails
    {
        public int LeaveTypeId { get; set; }
        public string LeaveName { get; set; }
    }
}
