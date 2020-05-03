using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public partial class RDD_Test
    {
        [DisplayName("CODE")]
        public string CODE { get; set; }

        [DisplayName("NAME")]
        public string DESCRIPTION { get; set; }


        public bool IsDefault { get; set; }
        public bool SaveFlag { get; set; }

        public bool EditFlag { get; set; }

        public string ErrorMessage { get; set; }

        public List<RDD_TestDetailnew> RDD_TestDetailnew { get; set; }
        public RDD_TestDetailnew RDD_TestDetail { get; set; }

        public List<string> Drecord { get; set; }

        public string UserName { get; set; }

        public int CellNo { get; set; }
        public string SingleVal { get; set; }

    }

    public class RDD_TestDetailnew
    {       
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public bool IsDefault { get; set; }

    }
}
