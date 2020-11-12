using SAPbobsCOM;
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
        public int Doc_Object { get; set; }
        public string AprovedBy { get; set; }

        public bool IsDraft { get; set; }
        public string ApprovalStatus { get; set; }
        public string Approvalby { get; set; }

        public string Erormsg { get; set; }
        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
        public int PVId { get; set; }
        [StringLength(30, ErrorMessage = "Country cannot be longer than 30 characters.")]
        public string Country { get; set; }
        public string RType { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public string RefNo { get; set; }
        public string DocStatus { get; set; }
        public string VType { get; set; }
        public List<SelectListItem> VTypeList { get; set; }
        [StringLength(30, ErrorMessage = "DBName cannot be longer than 30 characters.")]
        public string DBName { get; set; }

        public List<SelectListItem> DBNameList { get; set; }
        [StringLength(30, ErrorMessage = "Currency cannot be longer than 30 characters.")]
        public string Currency { get; set; }

        public List<SelectListItem> CurrencyList { get; set; }
        public string VendorCode { get; set; }
        [StringLength(500, ErrorMessage = "Employee cannot be longer than 500 characters.")]
        public string VendorEmployee { get; set; }
        [StringLength(500, ErrorMessage = "Benificiary cannot be longer than 500 characters.")]
        public string Benificiary { get; set; }
        [Range(0, 999999999.99, ErrorMessage = "Request Amt cannot be longer than 9 characters.")]
        public decimal RequestedAmt { get; set; }
        [Range(0, 999999999.99, ErrorMessage = "Approve Amt  cannot be longer than 9 characters.")]
        public decimal ApprovedAmt { get; set; }
        [StringLength(500, ErrorMessage = "Being Pay cannot be longer than 500 characters.")]
        public string BeingPayOf { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? PayRequestDate { get; set; }
        public string BankCode { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot be longer than 500 characters.")]
        public string BankName { get; set; }
        [StringLength(30, ErrorMessage = "Pay Method cannot be longer than 500 characters.")]
        public string PayMethod { get; set; }
        public List<SelectListItem> PayMethList { get; set; }
        [StringLength(500, ErrorMessage = "Being Pay cannot be longer than 500 characters.")]
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

        public string Ptype { get; set; }
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
            
    }

  
}
