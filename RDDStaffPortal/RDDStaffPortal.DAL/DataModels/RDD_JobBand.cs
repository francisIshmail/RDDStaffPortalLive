using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  class RDD_JobBand
    {
        public int JobBandId { get; set; }
        public string JobBandName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
