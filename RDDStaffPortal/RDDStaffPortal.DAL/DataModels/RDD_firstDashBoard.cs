using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_firstDashBoard
    {
        public string FirstText { get; set; }
        public string SecondText { get; set; }
        public decimal FirstValue { get; set; }
        public decimal SecondValue { get; set; }
        public string cssclass  { get; set; }
        public Int32 perValue { get; set; }
        public string Userid { get; set; }
        public int DisplaySeq { get; set; }

        public string colcss { get; set; }
    }
}
