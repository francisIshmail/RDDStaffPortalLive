using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.HR.Models
{
    public class BUCountries
    {
        public string CountryCodeName { get; set; }
        public string ItmsGrpNam { get; set; }
        public int ItmsGrpCod { get; set; }
        public int EmployeeId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmpName { get; set; }
        public List<BuCountries> BUCountriesnew { get; set; }
        public BuCountries BUCountrie { get; set; }

        public string CreatedBy { get; set; }
    }
    public class BuCountries
    {
        public int EmpId { get; set; }
        public int BUId { get; set; }
       // public int BU { get; set; }
        public string CountryCode { get; set; }
    }
}