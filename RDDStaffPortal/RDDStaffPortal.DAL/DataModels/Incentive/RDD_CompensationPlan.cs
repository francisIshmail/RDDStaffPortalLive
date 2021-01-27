using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace RDDStaffPortal.DAL.DataModels.Incentive
{
    public class RDD_CompensationPlan
    {
        public int CompPlanId { get; set; }        
        public int EmployeeId { get; set; }
        public string Period { get; set; }
        public string Years { get; set; }
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public string Designation { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string TotalCompensation { get; set; }
        public string Currency { get; set; }
        public string CurrCode { get; set; }
        public string AcceptedBySalesperson { get; set; }
        public string TotalSplitGpPercent { get; set; }
        public string TotalSplitRevPercent { get; set; }
        public string TargetNumber { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool EditFlag { get; set; }
        public bool MailFlag { get; set; }
        public bool ClickFlag { get; set; }
        public bool SaveFlag { get; set; }
        public bool IsDraft { get; set; }
        public string ErrorMsg { get; set; }
        public int pagesize { get; set; }
        public int pageno { get; set; }
        public string psearch { get; set; }
        public int TotalCount { get; set; }
        public int RowNum { get; set; }
        public string RType { get; set; }
        public string Ptype { get; set; }
        public int id { get; set; }
        public List<SelectListItem> DesignationNameList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> CurrencyList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<RDD_BU_CompensationPlan> RDD_BU_CompensationPlanList { get; set; }
        public List<RDD_KPI_CompensationPlan> RDD_KPI_CompensationPlanList { get; set; }
    }
    public partial class RDD_BU_CompensationPlan
    {
        public int BUCompId { get; set; }
        public int CompPlanId { get; set; }
        public string BU { get; set; }
        public string GPTarget { get; set; }
        public string RevenueTarget { get; set; }
        public string Rev_Split_Percentage { get; set; }
        public string GP_Split_Percentage { get; set; }
    }
    public partial class RDD_KPI_CompensationPlan
    {
        public int KPICompId { get; set; }
        public int CompPlanId { get; set; }
        public string KPI_Parameter { get; set; }
        public string KPI_Target { get; set; }
        public string KPI_Split_Percentage { get; set; }
    }
    public partial class DesignationLists
    {
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public string Email { get; set; }
    }
    
}
