using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
  public class RDD_CustomerMerging
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string DBName { get; set; }
        public bool IsAlreadyMapped { get; set; }
        public string  bgcolor { get; set; }
        public string CustTyp { get; set; }
    }
}
