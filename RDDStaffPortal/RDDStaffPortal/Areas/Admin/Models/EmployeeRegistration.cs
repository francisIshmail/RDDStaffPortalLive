using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class EmployeeRegistration
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int DesigId { get; set; }
        public string DesigName { get; set; }


        public int EmployeeId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Current_Address { get; set; }
        public string Permanent_Address { get; set; }
        public string Contact_No { get; set; }
        public string Ext_no { get; set; }
        public string IM_Id { get; set; }
        public string Marital_Status { get; set; }
        public DateTime DOB { get; set; }
        public string Citizenship { get; set; }

        public string Emergency_Contact { get; set; }
        public string passport_no { get; set; }
        public string CreatedBy { get; set; }


        public int FId { get; set; }
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

        public string other_ref_no { get; set; }

        public string EmployeeNo { get; set; }



        //BU

        public string u_bugroup { get; set; }
        //Country
        
        public string CountryCode { get; set; }

    }
}