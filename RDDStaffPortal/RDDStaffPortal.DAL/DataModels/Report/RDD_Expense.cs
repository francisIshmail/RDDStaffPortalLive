using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.Report
{
   public class RDD_Expense
    {
      public int  TotalCount { get; set; }
    public int RowNum { get; set; }
    public string SourceDb { get; set; }  
        public string SourceDbCode { get; set; }  
        public string AcctCode { get; set; }  
        public string Acct { get; set; }  
        public string TransId { get; set; } 
        public string TransType { get; set; }  
        public string Refrence { get; set; }  
        public DateTime TxDate { get; set; }  
        public string Year { get; set; }  
        public string  Qtr { get; set; }  
        public string month { get; set; }  
        public string  weekNo { get; set; }  
        public string Project { get; set; }  
        public string Country { get; set; }  
        public decimal Rate { get; set; }  
        public decimal DebitLC { get; set; }  
        public decimal DebitFC { get; set; } 
        public decimal DebitSC { get; set; } 
         public decimal CreditLC { get; set; }  
        public decimal CreditFC { get; set; }  
        public decimal TotalLC { get; set; }  
        public decimal TotalFC { get; set; } 
        
        public decimal TotalSC { get; set; } 
        public string Remarks { get; set; }
    }
}
