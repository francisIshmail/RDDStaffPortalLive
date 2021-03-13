using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.HR.Models
{
    public class Translations
    {
        public string de { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string ja { get; set; }
        public string it { get; set; }
    }

    public class CountryModel
    {
        public string name { get; set; }
        public string capital { get; set; }
        public List<string> altSpellings { get; set; }
        public string relevance { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public Translations translations { get; set; }
        public int population { get; set; }
        public List<object> latlng { get; set; }
        public string demonym { get; set; }
        public double? area { get; set; }
        public double? gini { get; set; }
        public List<string> timezones { get; set; }
        public List<object> borders { get; set; }
        public string nativeName { get; set; }
        public List<string> callingCodes { get; set; }
        public List<string> topLevelDomain { get; set; }
        public string alpha2Code { get; set; }
        public string alpha3Code { get; set; }
        public List<string> currencies { get; set; }
        public List<object> languages { get; set; }
    }
    public class Employees
    {
        public int Local_HR { get; set; }
        public int HOD_HR { get; set; }
        public int ManagerIdL2 { get; set; }
        public int JobBandId { get; set; }
        public string JobBandName { get; set; }
        public string Emergency_Contact_Name { get; set; }
        public string Emergency_Contact_Relation { get; set; }
        public int JobGradeId { get; set; }
        public string JobGradeName { get; set; }
        public string About { get; set; }
        public string CountryName { get; set; }
        public bool IsActive { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public int DesigId { get; set; }
        public string DesigName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public bool Editflag { get; set; }

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

      //  public List<SelectListItem> Citizenshiplist { get; set; }

        public string Emergency_Contact { get; set; }
        public string passport_no { get; set; }
        public string CreatedBy { get; set; }
        public string EmployeeNo { get; set; }

        public int ProfileCompletedPercentage { get; set; }
        //finance
        public int FId { get; set; }
        public string Currency { get; set; }
        public int Salary { get; set; }
        public DateTime? Salary_Start_Date { get; set; }
        public string Remark { get; set; }
        public string Account_No { get; set; }
        public string Bank_Name { get; set; }
        public string Branch_Name { get; set; }
        public string Bank_Code { get; set; }
        public string Tax_no { get; set; }
        public string Insurance_no { get; set; }

        public string other_ref_no { get; set; }

        // public string EmployeeNo { get; set; }



        //BU

        public string u_bugroup { get; set; }
        //Country
        public int CId { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeName { get; set; }
        public string Country { get; set; }
        //Items of BU
        public string ItmsGrpNam { get; set; }

        //To Get Manager
        public int ManagerL2 { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }

        //Additional fields

        public int Empstatus { get; set; }
        public string type_of_employement { get; set; }

        public DateTime Joining_Date { get; set; }

        public int No_child { get; set; }

        public string National_id { get; set; }

        public DateTime Contract_Start_date { get; set; }
        public string Note { get; set; }


        // public HttpPostedFileBase my_file { get; set; }

        public string ImagePath1 { get; set; }

        public byte[] ImagePath { get; set; }
        public string LogoType { get; set; }

        //Educational and professional
        public int EId { get; set; }
        public string Type { get; set; }
        public string Institute { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }



        public List<EmpInfoProEdunew> EmpInfoProEdunews { get; set; }

        public EmpInfoProEdunew EmpInfoProEdu { get; set; }

       

    }
   
    public class EmpInfoProEdunew
    {
        public int EId { get; set; }
        public string Type { get; set; }
        public string Institute { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }

    }
    
   
}