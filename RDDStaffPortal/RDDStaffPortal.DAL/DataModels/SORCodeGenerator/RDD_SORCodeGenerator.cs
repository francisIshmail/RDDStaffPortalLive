using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.SORCodeGenerator
{
    public class RDD_SORCodeGenerator
    {
        public int Id { get; set; }
        public string DBName { get; set; }
        public string DraftSORNum { get; set; }
        public string BU { get; set; }
        public string BUCode { get; set; }
        public string DraftSORKey { get; set; }
        public string ProjectCode { get; set; }
        public string SORApprovalCode { get; set; }
        public string Remarks { get; set; }
        public string SalesPerson { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedByEmail { get; set; }
        public string CACM { get; set; }
        public string CACMEmail { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public List<SelectListItem> DatabaseList { get; set; }
        public List<SelectListItem> RequestedByList { get; set; }
        public bool Saveflag { get; set; }

        public bool Editflag { get; set; }

        public string ActionType { get; set; }

        public string ErrorMsg { get; set; }
    }

}
