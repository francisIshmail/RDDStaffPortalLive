using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.Report
{
   public class RDD_Stock_Sheet
    {
        public decimal QtyBal { get; set; }
        public string OrderType { get; set; }
        public int TotalCount{ get; set; }
        public int RowNum { get; set; }
        public string Country { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc{ get; set; }
        public string WhseName{ get; set; }
        public string WhseStatus{ get; set; }
        public string BU{ get; set; }
        public string BUGroup{ get; set; }
        public decimal UnitCost { get; set; }
        public decimal Qty { get; set; }
        public decimal QtyOrdered { get; set; }
        public decimal QtyCommitted { get; set; }
        public decimal Value { get; set; }
        public string WhseOwner{ get; set; }
        public string ProdCategory{ get; set; }
        public string ProdLine{ get; set; }
        public string ProdGroup { get; set; }
        public string CustTyp { get; internal set; }
    }
}
