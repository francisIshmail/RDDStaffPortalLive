using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Documents;

namespace RDDStaffPortal.DAL.DataModels.LMS
{
    public partial class RDD_Holidays
    {
        public List<SelectListItem> CountryList { get; set; }
        public int HolidayId { get; set; }
        public string CountryCode { get; set; }
        public string HolidayName { get; set; }
        public DateTime? HolidayDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool HR { get; set; }

        public bool DeleteFlag { get; set; }

        public bool Saveflag { get; set; }

        public bool Editflag { get; set; }

        public bool UpdateFlag { get; set; }

        public string ActionType { get; set; }
        public int id { get; set; }

        public string ErrorMsg { get; set; }
    }
    public partial class GetCountryDetails
    {
        public string CountryCode { get; set; }
        public string Country { get; set; }
    }
}
