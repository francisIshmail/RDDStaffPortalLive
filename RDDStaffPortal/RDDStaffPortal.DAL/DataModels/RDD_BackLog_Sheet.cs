using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_BackLog_Sheet
    {
        public int TotalCount { get; set; }
        public string   db     { get; set; }
        public string ProjectCode     { get; set; }
        public string mapped_Project { get; set; }
        public string ItemGroup   { get; set; }
        public string mapped_BU { get; set; }
        public string  BUGroup { get; set; }
        public string OrderNum { get; set; }
        public string Code { get; set; }
        public string UnitPriceExcl { get; set; }
        public string  OrderQty { get; set; }
        public string  LineTotalExcl { get; set; }
        public string  QtyOutStanding { get; set; }
        public string  QtyConfirmed { get; set; }
        public string LineTotalExclConfirmed { get; set; }
        public string OrderType { get; set; }
        public string  supplierId { get; set; }
        public string supplierAcct { get; set; }
        public string supplier { get; set; }
        public string ShipDate { get; set; }
        public string DeliveryDate { get; set; }
        public string ActualShipDate { get; set; }
        public string ActualDeliveryDate { get; set; }
        public string ShipTo { get; set; }
        public string CustomerName { get; set; }
        public string OrderStatus { get; set; }
    }
}
