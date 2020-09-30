using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
  public  class RDD_SalesPersonTarget
    {
        public string year { get; set; }
        public string month { get; set; }
        public string country { get; set; }
        public string quarter { get; set; }
        public string Qyear { get; set; }
        public string type { get; set; }
        public string salesperson { get; set; }
        public string BU { get; set; }
        public string revenue { get; set; }
        public string gp { get; set; }
        public string sfullname { get; set; }
    }
}
