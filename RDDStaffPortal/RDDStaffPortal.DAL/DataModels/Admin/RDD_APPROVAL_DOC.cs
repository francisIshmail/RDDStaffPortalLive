using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.Admin
{
    public partial class RDD_APPROVAL_DOC
    {
        public string Refno { get; set; }
        public string Country { get; set; }
        public int SRNO { get; set; }
        public int ID { get; set; }
        public int TEMPLATE_ID { get; set; }
        public int OBJTYPE { get; set; }
        public string Currency { get; set; }
        public string DocumentName { get; set; }
        public int DOC_ID { get; set; }
        public DateTime? DOC_DATE { get; set; }
        public string CARDCODE { get; set; }
        public string CARDNAME { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal DocTotal { get; set; }
        public int SEQ_NO { get; set; }
        public string ORIGINATOR { get; set; }

        public string ORIGINATORCode { get; set; }
        public List<OriginatorList> OriginatorList { get; set; }
        public OriginatorList OriginList { get; set; }
        public string ORG_Remark { get; set; }
        public string APPROVER { get; set; }
        public string APPROVAL_DECISION { get; set; }
        public DateTime? APPROVAL_DATE { get; set; }
        public string APPROVAL_Remark { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class OriginatorList
    {
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
