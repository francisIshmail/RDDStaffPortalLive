using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.Admin
{
    public partial class RDD_Approval_Templates
    {
        public int Template_Id { get; set; }
        public string ObjType { get; set; }
        public string DocumentName { get; set; }

        public List<SelectListItem> DocumentNameList { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int no_of_approvals { get; set; }
        public bool Condition { get; set; }
        public string Condition_Text { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Erormsg { get; set; }
        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
        public int pagesize { get; set; }
        public int pageno { get; set; }
        public string psearch { get; set; }
        public int TotalCount { get; set; }       
        public int RowNum { get; set; }
        public int id { get; set; }
        public string RType { get; set; }
        public string Ptype { get; set; }

        public RDD_Approval_Originators RDD_Approval_Originators { get; set; }

        public List<RDD_Approval_Originators> RDD_Approval_OriginatorsList { get; set; }

        public RDD_Approval_Approvers RDD_Approval_Approvers { get; set; }

        public List<RDD_Approval_Approvers> RDD_Approval_ApproversList { get; set; }

    }
        public partial class RDD_Approval_Originators
        {
            public int Originator_Id { get; set; }
            public int Template_Id { get; set; }
            public string Originator { get; set; }
            public string OriginatorName { get; set; }
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
            public DateTime LastUpdatedOn { get; set; }
            public string LastUpdatedBy { get; set; }
        }
        public partial class RDD_Approval_Approvers
        {
        public int Approver_Id { get; set; }
        public int Template_Id { get; set; }
        public string Approver { get; set; }
        public int Approval_Sequence { get; set; }
        public bool IsApproval_Mandatory { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
        
}
