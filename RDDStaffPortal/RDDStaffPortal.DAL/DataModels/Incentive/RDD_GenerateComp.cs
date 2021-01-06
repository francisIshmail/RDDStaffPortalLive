using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.Incentive
{
    public class RDD_GenerateComp
    {
        public int Comp_Calc_Id { get; set; }
        public string Period { get; set; }
        public string Years { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public string Currency { get; set; }
        public string TotalCompensation { get; set; }
        public string Total_Comp_Earned { get; set; }
        public string Total_Deduction { get; set; }
        public string Final_Comp_Earned { get; set; }
        public string TermsAndCondition { get; set; }
        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
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
        public List<RDD_BU_CompensationCalculation> RDD_BU_CompensationCalculationList { get; set; }
        public List<RDD_KPI_CompensationCalculation> RDD_KPI_CompensationCalculationList { get; set; }
    }

    public partial class RDD_BU_CompensationCalculation
    {
        public int BUComp_Calc_Id { get; set; }
        public int Comp_Calc_Id { get; set; }
        public string BU { get; set; }
        public string Earned { get; set; }
        public string Achieved_Percentage { get; set; }
        public string TotalAcheived { get; set; }
        public string TotalTarget { get; set; }
        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string M4 { get; set; }
        public string M5 { get; set; }
        public string M6 { get; set; }
    }

    public partial class RDD_KPI_CompensationCalculation
    {
        public int KPIComp_Calc_Id { get; set; }
        public int Comp_Calc_Id { get; set; }
        public string KPI_Parameter { get; set; }
        public string Earned { get; set; }
        public string Achieved_Percentage { get; set; }
        public string TotalAcheived { get; set; }
        public string TotalTarget { get; set; }
        
        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string M4 { get; set; }
        public string M5 { get; set; }
        public string M6 { get; set; }
    }
        
}