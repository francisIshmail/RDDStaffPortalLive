using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.SAP
{
    class RDD_SOR1
    {

        public Int64 SO_LineId { get; set; }
        public Int32 SO_ID { get; set; }
        public Int32 Base_Obj { get; set; }
        public Int32 Base_Id { get; set; }
        public Int32 Base_LinId { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscPer { get; set; }
        public decimal LineTotal { get; set; }
        public string TaxCode { get; set; }
        public string WhsCode { get; set; }
        public decimal QtyInWhs { get; set; }
        public decimal QtyAval { get; set; }
        public string OpgRefAlpha { get; set; }
        public decimal GP { get; set; }
        public decimal GPPer { get; set; }
        public decimal TaxRate { get; set; }
    }
}
