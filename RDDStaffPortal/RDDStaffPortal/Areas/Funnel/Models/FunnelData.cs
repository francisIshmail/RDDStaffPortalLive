using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Funnel.Models
{
    public class FunnelData
    {
       
        public string resellername { get; set; }
        public int TotalCost { get; set; }
        public int ChangeCount { get; set; }
        public int TotalCount { get; set; }
        public  int RowNum { get; set; }
        public int fid { get; set; }
        public string quoteID { get; set; }
        public string CreatedBy { get; set; }
        public string Country { get; set; }
        public string BUName { get; set; }

        public string bdm { get; set; }
        public string goodsDescr { get; set; }
        public string enduser { get; set; }
        public string remarks { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public int Cost { get; set; }
        public int Landed { get; set; }

        public decimal MarginUSD { get; set; }

        public string CardName { get; set; }

        public decimal value { get; set; }

        public string CardCode { get; set; }
       

        public string ItmsGrpNam { get; set; }

        public DateTime Createddte { get; set; }
        public DateTime Updateddte { get; set; }

        public DateTime NextReminderDt { get; set; }
        public DateTime quoteDate { get; set; }
        public string DealStatus { get; set; }

        public DateTime expClosingDt { get; set; }
        public DateTime orderBookedDate { get; set; }
        public  DateTime InvoiceDt { get; set; }

        public int marginper { get; set; }

    }
}