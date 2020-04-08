using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public class RDD_Employee_Fin
    {
        public string Currency { get; set; }
        public int Salary { get; set; }
        public DateTime Salary_Start_Date { get; set; }
        public string Remark { get; set; }
        public string Account_No { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Name { get; set; }
        public string Bank_Code { get; set; }
        public string Tax_no { get; set; }
        public string Insurance_no { get; set; }
    }
}
