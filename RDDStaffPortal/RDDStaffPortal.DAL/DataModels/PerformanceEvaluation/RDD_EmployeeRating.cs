using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels.PerformanceEvaluation
{
    public class RDD_EmployeeRating
    {
        public int AppraisalId { get; set; }
        public int CategoryId { get; set; }
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public bool SaveFlag { get; set; }
        public bool EditFlag { get; set; }
        public string ActionType { get; set; }
        public string ActionTypeTrans { get; set; }
        public string ErrorMsg { get; set; }
        public int id { get; set; }
        public string Emp_SubmittedBy { get; set; }
        public DateTime? Emp_LastUpdatedOn { get; set; }
        public DateTime? Emp_SubmittedOn { get; set; }
        public string Mng_SubmittedBy { get; set; }
        public DateTime? Mng_SubmittedOn { get; set; }
        public DateTime? Mng_LastUpdatedOn { get; set; }
        public List<RDD_EmployeeRatingTrans> rDD_EmpAppraisalList { get; set; }
    }
    public partial class RDD_EmployeeRatingTrans
    {
        public int AppraisalTransId { get; set; }
        public int AppraisalId { get; set; }
        public int QuestionId { get; set; }
        public int EmployeeRating { get; set; }
        public string EmployeeComment { get; set; }
        public int ManagerRating { get; set; }
        public string ManagerComment { get; set; }
    }
}
