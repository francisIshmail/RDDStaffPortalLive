using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public class RDD_HRReport
    {

        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> EmployeesList { get; set; }
        public List<SelectListItem> LeaveTypeList { get; set; }
        public string TotalTakenLeave { get; set; }
        public string TotakPendingLeave { get; set; }        
        public string Remarks { get; set; }
        public string BalanceLeave { get; set; }       
        public int LeaveRequestId { get; set; }
        public int LeaveTypeId { get; set; }
        public int EmployeeId { get; set; }
        public string LeaveName { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
    }
}
