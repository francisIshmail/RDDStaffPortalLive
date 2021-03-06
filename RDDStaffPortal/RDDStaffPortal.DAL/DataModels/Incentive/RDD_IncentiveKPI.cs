using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels.Incentive
{
    public class RDD_IncentiveKPI
    {
        public int KPI_Id { get; set; }
        public string KPIname { get; set; }
        public string DesignationId { get; set; }
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public List<SelectListItem> DesignationNameList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public string Period { get; set; }
        public string Retain_Percentage { get; set; }
        public string Years { get; set; }
        public string Total_Percentage { get; set; }
        public string TermsAndCondition { get; set; }             
        public string IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
        public string ErrorMsg { get; set; }
        public string KPI_ParameterName { get; set; }
        public string KPIparamId { get; set; }
        public int id { get; set; }
        public int pagesize { get; set; }
        public int pageno { get; set; }
        public string psearch { get; set; }
        public int TotalCount { get; set; }
        public int RowNum { get; set; }
        public List<RDD_IncentiveKPI_Parameter> RDD_IncentiveKPI_ParameterList { get; set; }
        public string RType { get; set; }
        public string Ptype { get; set; }
    }
    public partial class RDD_IncentiveKPI_Parameter
    {
        public int KPI_Parameter_Id { get; set; }
        public int KPI_Id { get; set; }
        public string KPI_Parameter { get; set; }
        public int Split_Percentage { get; set; }       
        public DateTime CreatedOn { get; set; }
        public string KPIType { get; set; }       
        public string IsDeleted { get; set; }
    }   
}
