using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  class RDD_WebReportsList
    {
        //	RowNum	repTypeId	reportTitle	bgcolor	IsAlreadyMapped

        public int RowNum { get; set; }
        public int TotalCount { get; set; }
        public int repTypeId { get; set; }
        public string reportTitle { get; set; }
        public string bgcolor { get; set; }

        public bool IsAlreadyMapped { get; set; }
    }
}
