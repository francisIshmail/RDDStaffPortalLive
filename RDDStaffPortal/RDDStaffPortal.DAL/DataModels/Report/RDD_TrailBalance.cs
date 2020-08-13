using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.Report
{
  public partial  class RDD_TrailBalance
    {

        public string AcctName { get; set; }
        public string AcctCode { get; set; }
        public string Postable { get; set; }
        public int AcctLevel { get; set; }
        public string FatherAcct { get; set; }
        public decimal LocCreditTRI { get; set; }
        public decimal LocDebitTRI { get; set; }
        public decimal SysCreditTRI { get; set; }
        public decimal SysDebitTRI { get; set; }
        public decimal LocCreditTZ { get; set; }
        public decimal LocDebitTZ { get; set; }
        public decimal SysCreditTZ { get; set; }
        public decimal SysDebitTZ { get; set; }
        public decimal LocCreditKE { get; set; }
        public decimal LocDebitKE { get; set; }
        public decimal SysCreditKE { get; set; }
        public decimal SysDebitKE { get; set; }
        public decimal LocCreditUG { get; set; }
        public decimal LocDebitUG { get; set; }
        public decimal SysCreditUG { get; set; }
        public decimal SysDebitUG { get; set; }
        public decimal LocCreditZM { get; set; }
        public decimal LocDebitZM { get; set; }
        public decimal SysCreditZM { get; set; }
        public decimal SysDebitZM { get; set; }
        public decimal LocCreditMA { get; set; }
        public decimal LocDebitMA { get; set; }
        public decimal SysCreditMA { get; set; }
        public decimal SysDebitMA { get; set; }
        public decimal LocCreditTRNGL { get; set; }
        public decimal LocDebitTRNGL { get; set; }
        public decimal SysCreditTRNGL { get; set; }
        public decimal SysDebitTRNGL { get; set; }
        public decimal LocCreditTotal { get; set; }
        public decimal SysCreditTotal { get; set; }
        public decimal LocDebitTotal { get; set; }
        public decimal SysDebitTotal { get; set; }

    }
}
