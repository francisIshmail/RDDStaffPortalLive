using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.PerformanceEvaluation
{
    public class RDD_AddAppraisalQuestion
    {        
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public string Period { get; set; }        
        public string IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool SaveFlag { get; set; }
        public bool EditFlag { get; set; }
        public string ActionType { get; set; }
        public string ErrorMsg { get; set; }
        public int id { get; set; }
        public List<RDD_AddAppraisalQuestionList> rDD_QuestionList { get; set; }
    }
    public partial class RDD_AddAppraisalQuestionList
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}
