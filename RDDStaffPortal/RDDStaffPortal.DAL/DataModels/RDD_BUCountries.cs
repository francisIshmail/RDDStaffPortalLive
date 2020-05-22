using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_BUCountries
    {
        public string CountryCodeName { get; set; }
        public string ItmsGrpNam { get; set; }
        public int ItmsGrpCod { get; set; }
        public int EmployeeId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmpName { get; set; }
        public int BU { get; set; }
        public int CId { get; set; }
        public string CreatedBy { get; set; }
        public List<BuCountries> BUCountriesnew { get; set; }
        public BuCountries BUCountrie { get; set; }
    }
    public class BuCountries
    {
        public int CId { get; set; }
        public int EmpId { get; set; }
        public int BUId { get; set; }
        public int BU { get; set; }
        public string CountryCode { get; set; }
    }
}
