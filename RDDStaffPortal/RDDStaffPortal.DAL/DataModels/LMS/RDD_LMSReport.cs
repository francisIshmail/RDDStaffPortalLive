using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Web.Mvc;


namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public class RDD_LMSReport
    {
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> EmployeesList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal NoOfDays { get; set; }
        public string Remarks { get; set; }
        public int EmployeeId { get; set; }
        public string LeaveName { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
    }
    //public partial class GetCountryDetails
    //{
    //    public string CountryCode { get; set; }
    //    public string Country { get; set; }
    //}
}
