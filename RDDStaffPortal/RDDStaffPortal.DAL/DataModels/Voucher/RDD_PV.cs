using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.Voucher
{
    public partial class RDD_PV
    {
        public string ApprovalStatus { get; set; }

        public string Erormsg { get; set; }
        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
        public int PVId { get; set; }
        public string Country { get; set; }
        public string RType { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public string RefNo { get; set; }
        public string DocStatus { get; set; }
        public string VType { get; set; }
        public List<SelectListItem> VTypeList { get; set; }
        public string DBName { get; set; }

        public List<SelectListItem> DBNameList { get; set; }
        public string Currency { get; set; }

        public List<SelectListItem> CurrencyList { get; set; }
        public string VendorCode { get; set; }
        public string VendorEmployee { get; set; }
        public string Benificiary { get; set; }
        public decimal RequestedAmt { get; set; }
        public decimal ApprovedAmt { get; set; }
        public string BeingPayOf { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? PayRequestDate { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string PayMethod { get; set; }
        public List<SelectListItem> PayMethList { get; set; }
        public string PayRefNo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? PayDate { get; set; }
        public string FilePath { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ClosedDate { get; set; }
        public string CAappStatus { get; set; }
        public string CAappRemarks { get; set; }
        public string CAapprovedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CAapprovedOn { get; set; }
        public string CMappStatus { get; set; }
        public string CMappRemarks { get; set; }
        public string CMapprovedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CMapprovedOn { get; set; }
        public string CFOappStatus { get; set; }
        public string CFOappRemarks { get; set; }
        public string CFOapprovedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CFOapprovedOn { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }

        public List<RDD_PVLines> RDD_PVLinesDetails { get; set; }

        public RDD_PVLines RDD_PVLinesDetail { get; set; }

        public int pagesize { get; set; }
        public int pageno { get; set; }
        public string psearch { get; set; }

        public int TotalCount { get; set; }
       

        public int RowNum { get; set; }
        public int id { get; set; }

    }
        public class RDD_PVLines
        {
            public int PVLineId { get; set; }
            public int PVId { get; set; }
            public string LineRefNo { get; set; }
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
             public DateTime? Date { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }
            public string FilePath { get; set; }
            public bool IsDeleted { get; set; }
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
             public DateTime? CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public DateTime? LastUpdatedOn { get; set; }
            public string LastUpdatedBy { get; set; }
             public string Ptype { get; set; }
    }

    public partial class City
    {
        public string CityName { get; set; }
        public int Id { get; set; }
    }
}
