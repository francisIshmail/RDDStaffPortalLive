using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.PerformanceEvaluation
{
    public class RDD_Settings
    {
        public int RatingId { get; set; }
        public int FrequencyId { get; set; }
        public int RatingNo { get; set; }
        public string AppraisalFrequency { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool SaveFlag { get; set; }
        public bool EditFlag { get; set; }
        public string ActionType { get; set; }
        public string ErrorMsg { get; set; }
        public int id { get; set; }
    }
}
