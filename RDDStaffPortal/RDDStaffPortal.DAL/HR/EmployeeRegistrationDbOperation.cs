using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.HR
{
    public class EmployeeRegistrationDbOperation
    {
        CommonFunction Com = new CommonFunction();
        // public string Save(RDD_EmployeeRegistration EmpData)
        public List<Outcls1> Save(RDD_EmployeeRegistration EmpData, List<RDD_EmployeeRegistration> EmpInfoProEdu, List<DocumentList> DocumentList)
        {
            string response = string.Empty;
            string result = string.Empty;
            List<Outcls1> str = new List<Outcls1>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    try
                    {
                        byte[] file;


                        using (var stream = new FileStream(EmpData.ImagePath1, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = new BinaryReader(stream))
                            {
                                file = reader.ReadBytes((int)stream.Length);
                            }
                        }

                        

                        Int32 Emp_ID = 0;
                        SqlParameter[] Para = {
                                                   new SqlParameter("@p_EmployeeId",EmpData.EmployeeId),
                          new SqlParameter("@p_ImagePath",file),
                           new SqlParameter("@p_LogoType",EmpData.LogoType),
                            new SqlParameter("@p_FName",EmpData.FName),
                            new SqlParameter("@p_LName",EmpData.LName),
                             new SqlParameter("@p_Email",EmpData.Email),
                             new SqlParameter("@p_Gender",EmpData.Gender),
                             new SqlParameter("@p_Current_Address",EmpData.Current_Address),
                            new SqlParameter("@p_Permanent_Address",EmpData.Permanent_Address),
                             new SqlParameter("@p_Contact_No",EmpData.Contact_No),
                              new SqlParameter("@p_Ext_no",EmpData.Ext_no),
                           new SqlParameter("@p_imgbool",EmpData.imgbool),
                           new SqlParameter("@p_IM_Id",EmpData.IM_Id),
                            new SqlParameter("@p_ManagerId",EmpData.ManagerId),
                            new SqlParameter("@p_HOD_HR",EmpData.HOD_HR),
                              new SqlParameter("@p_Local_HR",EmpData.Local_HR),
                                new SqlParameter("@p_ManagerL2Id",EmpData.ManagerIdL2),
                                 new SqlParameter("@p_JobBandId",EmpData.JobBandId),
                                  new SqlParameter("@p_JobGradeId",EmpData.JobGradeId),
                                  new SqlParameter("@p_About",EmpData.About),
                                   new SqlParameter("@p_Marital_Status",EmpData.Marital_Status),
                                     new SqlParameter("@p_DOB",EmpData.DOB),
                                     new SqlParameter("@p_Citizenship",EmpData.Citizenship),
                                        new SqlParameter("@p_DesigId",EmpData.DesigId),
                            new SqlParameter("@p_DeptId",EmpData.DeptId),
                             new SqlParameter("@p_Emergency_Contact",EmpData.Emergency_Contact),
                              new SqlParameter("@p_Emergency_Contact_Name",EmpData.Emergency_Contact_Name),
                            new SqlParameter("@p_Emergency_Contact_Relation",EmpData.Emergency_Contact_Relation),

                              new SqlParameter("@p_passport_no",EmpData.passport_no),
                               new SqlParameter("@p_CreatedBy",EmpData.CreatedBy),
                                new SqlParameter("@p_EmployeeNo",EmpData.EmployeeNo),
                                new SqlParameter("@p_CountryCode",EmpData.CountryCodeName),
                                  new SqlParameter("@p_Empstatus",EmpData.StatusId),
                                   new SqlParameter("@p_EmpType",EmpData.type_of_employement),
                                    new SqlParameter("@p_Joining_Date",EmpData.Joining_Date),
                                      new SqlParameter("@p_no_of_child",EmpData.No_child),
                                      new SqlParameter("@p_Nationalid",EmpData.National_id),
                            new SqlParameter("@p_Contract_Start_date",EmpData.Contract_Start_date),
                             new SqlParameter("@p_NOTE",EmpData.Note),
                              new SqlParameter("@p_IsActive",EmpData.IsActive),
                            new SqlParameter("@p_EmployeeIdOUT",Emp_ID),
                            new SqlParameter("@p_Response",response),
                            };
                       
                        str = Com.ExecuteNonQueryListID("RDD_Employees_InsertUpdate", Para);
                        Emp_ID = str[0].Id;
                        response = str[0].Responsemsg;
                        


                        List<Outcls1> str1 = new List<Outcls1>();
                        if (str[0].Outtf == true)
                        {
                            SqlParameter[] Para2 = {
                                        new SqlParameter("@p_EmployeeId", Emp_ID),
                                         new SqlParameter("@p_Id", EmpData.FId),
                                         new SqlParameter("@p_Currency", EmpData.Currency),
                                         new SqlParameter("@p_Salary", EmpData.Salary),
                                          new SqlParameter("@p_Salary_Start_Date", EmpData.Salary_Start_Date),
                                           new SqlParameter("@p_Remark", EmpData.Remark),
                                           new SqlParameter("@p_Account_No", EmpData.Account_No),
                                             new SqlParameter("@p_Bank_Code", EmpData.Bank_Code),
                                              new SqlParameter("@p_Bank_Name", EmpData.Bank_Name),
                                                new SqlParameter("@p_Branch_Name", EmpData.Branch_Name),
                                                 new SqlParameter("@p_Tax_no", EmpData.Tax_no),
                                                 new SqlParameter("@p_Insurance_no",EmpData.Insurance_no),
                                                 new SqlParameter("@p_other_ref_no",EmpData.other_ref_no),
                                                 new SqlParameter("@p_CreatedBy",EmpData.CreatedBy),
                                                  new SqlParameter("@p_EmployeeIdOUT",Emp_ID),
                                                 new SqlParameter("@p_Response",response),



                                    };
                            str1 = Com.ExecuteNonQueryListID("RDD_Employees_Fin_InsertUpdate", Para2);
                            response = str1[0].Responsemsg;                       
                            if (DocumentList != null)
                            {
                                for (int i = 0; i < DocumentList.Count; i++)
                                {
                                    SqlParameter[] Para3 = {
                                        new SqlParameter("@p_EmployeeId", Emp_ID),
                                        new SqlParameter("@p_Id", DocumentList[i].DId),
                                        new SqlParameter("@p_Description", DocumentList[i].DcumenName),
                                         new SqlParameter("@p_Link", DocumentList[i].DocPath),
                                          new SqlParameter("@p_CreatedBy", EmpData.CreatedBy),
                                           new SqlParameter("@p_EmployeeIdOUT",Emp_ID),
                                          new SqlParameter("@p_Response",response),
                                        };
                                    str1 = Com.ExecuteNonQueryListID("RDD_Employees_Doc_InsertUpdate", Para3);
                                    response = str1[0].Responsemsg;

                                }
                            }                        
                            if (EmpInfoProEdu != null)
                            {
                                for (int i = 0; i < EmpInfoProEdu.Count; i++)
                                { 
                                    SqlParameter[] Para4 = {
                                        new SqlParameter("@p_EmployeeId", Emp_ID),
                                        new SqlParameter("@p_Id", EmpInfoProEdu[i].EId),
                                         new SqlParameter("@p_Type", EmpInfoProEdu[i].Type),
                                         new SqlParameter("@p_Institute",EmpInfoProEdu[i].Institute),
                                         new SqlParameter("@p_Start_date",EmpInfoProEdu[i].StartDate),
                                         new SqlParameter("@p_End_date",EmpInfoProEdu[i].EndDate),
                                         new SqlParameter("@p_Description",EmpInfoProEdu[i].Description),
                                         new SqlParameter("@p_Score",EmpInfoProEdu[i].Score),
                                         new SqlParameter("@p_CreatedBy", EmpData.CreatedBy),
                                          new SqlParameter("@p_EmployeeIdOUT",Emp_ID),
                                          new SqlParameter("@p_Response",response)
                                        };
                                    str1 = Com.ExecuteNonQueryListID("RDD_Employees_EduProf_InsertUpdate", Para4);
                                    response = str1[0].Responsemsg;
                                }
                            }
                        }
                        SqlParameter[] Para1 = {
                                        new SqlParameter("@EmployeeId", Emp_ID)};
                        Com.ExecuteNonQuery("RDD_SetEmployeeProfileCompletedPercentage", Para1);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Outtf = false,
                            Id = -1,
                            Responsemsg = "Error occured : " + ex.Message
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                str.Clear();
                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return str;

        }

        public RDD_EmployeeRegistration Edit(int? EmployeeId)
        {

            RDD_EmployeeRegistration emp = new RDD_EmployeeRegistration();


            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;





            DataSet DS = Db.myGetDS("EXEC  dbo.RDD_EmpInfo_GetData  '" + EmployeeId + "'");

            try
            {
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows.Count > 0)
                    {

                        if (DS.Tables[0].Rows[0]["LoginName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["LoginName"]))
                        {
                            emp.LoginName = DS.Tables[0].Rows[0]["LoginName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["CountryName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["CountryName"]))
                        {
                            emp.CountryName = DS.Tables[0].Rows[0]["CountryName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["EmployeeId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["EmployeeId"]))
                        {
                            emp.EmployeeId = Convert.ToInt32(DS.Tables[0].Rows[0]["EmployeeId"]);
                        }
                        emp.Emergency_Contact_Name = (DS.Tables[0].Rows[0]["Emergency_Contact_Name"].ToString()==null && DBNull.Value.Equals(DS.Tables[0].Rows[0]["Emergency_Contact_Name"].ToString())) ? "": DS.Tables[0].Rows[0]["Emergency_Contact_Name"].ToString();
                        emp.Emergency_Contact_Relation = (DS.Tables[0].Rows[0]["Emergency_Contact_Relation"].ToString()==null && DBNull.Value.Equals(DS.Tables[0].Rows[0]["Emergency_Contact_Relation"].ToString())) ? "": DS.Tables[0].Rows[0]["Emergency_Contact_Relation"].ToString();

                        if (DS.Tables[0].Rows[0]["Id"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Id"]))
                        {
                            emp.FId = Convert.ToInt32(DS.Tables[0].Rows[0]["Id"]);
                        }

                        if (DS.Tables[0].Rows[0]["FName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["FName"]))
                        {
                            emp.FName = DS.Tables[0].Rows[0]["FName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["LName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["LName"]))
                        {
                            emp.LName = DS.Tables[0].Rows[0]["LName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["AboutUs"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["AboutUs"]))
                        {
                            emp.About = DS.Tables[0].Rows[0]["AboutUs"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["EmployeeNo"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["EmployeeNo"]))
                        {
                            emp.EmployeeNo = DS.Tables[0].Rows[0]["EmployeeNo"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Email"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["LName"]))
                        {
                            emp.Email = DS.Tables[0].Rows[0]["Email"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Current_Address"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Current_Address"]))
                        {
                            emp.Current_Address = DS.Tables[0].Rows[0]["Current_Address"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Permanent_Address"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Permanent_Address"]))
                        {
                            emp.Permanent_Address = DS.Tables[0].Rows[0]["Permanent_Address"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Contact_No"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Contact_No"]))
                        {
                            emp.Contact_No = DS.Tables[0].Rows[0]["Contact_No"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Ext_no"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Ext_no"]))
                        {
                            emp.Ext_no = DS.Tables[0].Rows[0]["Ext_no"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["IM_Id"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["IM_Id"]))
                        {
                            emp.IM_Id = DS.Tables[0].Rows[0]["IM_Id"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Marital_Status"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Marital_Status"]))
                        {
                            emp.Marital_Status = DS.Tables[0].Rows[0]["Marital_Status"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["DOB"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DOB"]))
                        {
                            emp.DOB = Convert.ToDateTime(DS.Tables[0].Rows[0]["DOB"].ToString());
                        }
                        if (DS.Tables[0].Rows[0]["Citizenship"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Citizenship"]))
                        {
                            emp.Citizenship = DS.Tables[0].Rows[0]["Citizenship"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["manager"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["manager"]))
                        {
                            emp.ManagerId = Convert.ToInt32(DS.Tables[0].Rows[0]["manager"]);
                        }
                        if (DS.Tables[0].Rows[0]["ManagerName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["ManagerName"]))
                        {
                            emp.ManagerName = DS.Tables[0].Rows[0]["ManagerName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["DesigId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DesigId"]))
                        {
                            emp.DesigId = Convert.ToInt32(DS.Tables[0].Rows[0]["DesigId"]);
                        }
                        if (DS.Tables[0].Rows[0]["DesigName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DesigName"]))
                        {
                            emp.DesigName = DS.Tables[0].Rows[0]["DesigName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["DeptId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DeptId"]))
                        {
                            emp.DeptId = Convert.ToInt32(DS.Tables[0].Rows[0]["DeptId"]);
                        }
                        if (DS.Tables[0].Rows[0]["DeptName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DeptName"]))
                        {
                            emp.DeptName = DS.Tables[0].Rows[0]["DeptName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["StatusId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["StatusId"]))
                        {
                            emp.StatusId = Convert.ToInt32(DS.Tables[0].Rows[0]["StatusId"]);
                        }
                        if (DS.Tables[0].Rows[0]["StatusName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["StatusName"]))
                        {
                            emp.StatusName = DS.Tables[0].Rows[0]["StatusName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["type_of_employment"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["type_of_employment"]))
                        {
                            emp.type_of_employement = DS.Tables[0].Rows[0]["type_of_employment"].ToString();
                        }


                        if (DS.Tables[0].Rows[0]["Emergency_Contact"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Emergency_Contact"]))
                        {
                            emp.Emergency_Contact = DS.Tables[0].Rows[0]["Emergency_Contact"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["passport_no"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["passport_no"]))
                        {
                            emp.passport_no = DS.Tables[0].Rows[0]["passport_no"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["managerL2"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["managerL2"]))
                        {
                            emp.ManagerIdL2 = Convert.ToInt32(DS.Tables[0].Rows[0]["managerL2"].ToString());
                        }
                        if (DS.Tables[0].Rows[0]["ManagerNameL2"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["ManagerNameL2"]))
                        {
                            emp.ManagerName = DS.Tables[0].Rows[0]["ManagerNameL2"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["JobBand"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["JobBand"]))
                        {
                            emp.JobBandId = Convert.ToInt32(DS.Tables[0].Rows[0]["JobBand"]);
                        }
                        if (DS.Tables[0].Rows[0]["JobBandName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["JobBandName"]))
                        {
                            emp.JobBandName = DS.Tables[0].Rows[0]["JobBandName"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["JobGrade"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["JobGrade"]))
                        {
                            emp.JobGradeId = Convert.ToInt32(DS.Tables[0].Rows[0]["JobGrade"]);
                        }
                        if (DS.Tables[0].Rows[0]["JobGradeName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["JobGradeName"]))
                        {
                            emp.JobGradeName = DS.Tables[0].Rows[0]["JobGradeName"].ToString();
                        }









                        if (DS.Tables[0].Rows[0]["Country"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Country"]))
                        {
                            emp.CountryCodeName = DS.Tables[0].Rows[0]["Country"].ToString();
                        }




                        if (DS.Tables[0].Rows[0]["EmployeeNo"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["EmployeeNo"]))
                        {
                            emp.EmployeeNo = DS.Tables[0].Rows[0]["EmployeeNo"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["joining_date"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["joining_date"]))
                        {
                            emp.Joining_Date = Convert.ToDateTime(DS.Tables[0].Rows[0]["joining_date"]);
                        }
                        if (DS.Tables[0].Rows[0]["IsActive"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["IsActive"]))
                        {
                            emp.IsActive = Convert.ToBoolean(DS.Tables[0].Rows[0]["IsActive"]);
                        }


                        if (DS.Tables[0].Rows[0]["Gender"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Gender"]))
                        {
                            emp.Gender = DS.Tables[0].Rows[0]["Gender"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["No_Child"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["No_Child"]))
                        {
                            emp.No_child = Convert.ToInt32(DS.Tables[0].Rows[0]["No_Child"].ToString());
                        }
                        if (DS.Tables[0].Rows[0]["National_Id"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["National_Id"]))
                        {
                            emp.National_id = DS.Tables[0].Rows[0]["National_Id"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Contract_Start_Date"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Contract_Start_Date"]))
                        {
                            emp.Contract_Start_date = Convert.ToDateTime(DS.Tables[0].Rows[0]["Contract_Start_Date"]);
                        }


                        if (DS.Tables[0].Rows[0]["NOTE"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["NOTE"]))
                        {
                            emp.Note = DS.Tables[0].Rows[0]["NOTE"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Currency"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Currency"]))
                        {
                            emp.Currency = DS.Tables[0].Rows[0]["Currency"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Account_No"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Account_No"]))
                        {
                            emp.Account_No = DS.Tables[0].Rows[0]["Account_No"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Tax_no"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Tax_no"]))
                        {
                            emp.Tax_no = DS.Tables[0].Rows[0]["Tax_no"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Salary"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Salary"]))
                        {
                            emp.Salary = Convert.ToInt32(DS.Tables[0].Rows[0]["Salary"].ToString());
                        }
                        if (DS.Tables[0].Rows[0]["Branch_Name"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Branch_Name"]))
                        {
                            emp.Branch_Name = DS.Tables[0].Rows[0]["Branch_Name"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["Insurance_no"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Insurance_no"]))
                        {
                            emp.Insurance_no = DS.Tables[0].Rows[0]["Insurance_no"].ToString();
                        }


                        if (DS.Tables[0].Rows[0]["Salary_Start_Date"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Salary_Start_Date"]))
                        {
                            emp.Salary_Start_Date = Convert.ToDateTime(DS.Tables[0].Rows[0]["Salary_Start_Date"]);
                        }


                        if (DS.Tables[0].Rows[0]["Bank_Name"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Bank_Name"]))
                        {
                            emp.Bank_Name = DS.Tables[0].Rows[0]["Bank_Name"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["other_ref_no"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["other_ref_no"]))
                        {
                            emp.other_ref_no = DS.Tables[0].Rows[0]["other_ref_no"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["Remark"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Remark"]))
                        {
                            emp.Remark = DS.Tables[0].Rows[0]["Remark"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["Bank_Code"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Bank_Code"]))
                        {
                            emp.Bank_Code = DS.Tables[0].Rows[0]["Bank_Code"].ToString();
                        }

                        if (DS.Tables[0].Rows[0]["IsActive"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["IsActive"]))
                        {
                            emp.IsActive = Convert.ToBoolean(DS.Tables[0].Rows[0]["IsActive"]);
                        }


                        if (DS.Tables[0].Rows[0]["ImagePath"] != null && DS.Tables[0].Rows[0]["ImagePath"].ToString().Length > 0)

                        {
                            emp.ImagePath = (byte[])DS.Tables[0].Rows[0]["ImagePath"];
                        }
                        else
                        {

                            //emp.ImagePath = file;
                        }


                        emp.HOD_HR = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[0]["HOD_HR"].ToString()) ? Convert.ToInt32(DS.Tables[0].Rows[0]["HOD_HR"].ToString()) : 0;
                        emp.Local_HR = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[0]["Local_HR"].ToString()) ? Convert.ToInt32(DS.Tables[0].Rows[0]["Local_HR"].ToString()) : 0;
                        //emp.ImagePath = (byte[])DS.Tables[0].Rows[0]["ImagePath"];
                        string base64String = Convert.ToBase64String(emp.ImagePath);
                        emp.ImagePath1 = base64String;


                        //emp.ImagePath = Convert.ToByte(DS.Tables[0].Rows[0]["ImagePath"]);

                        //if (DS.Tables[0].Rows[0]["ImagePath"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["ImagePath"]))
                        //{
                        //    emp.ImagePath = Convert.ToByte(DS.Tables[0].Rows[0]["ImagePath"]);
                        //}
                        if (DS.Tables[0].Rows[0]["LogoType"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["LogoType"]))
                        {
                            emp.LogoType = DS.Tables[0].Rows[0]["LogoType"].ToString();
                        }
                    }


                    List<DocumentList> DocumentList = new List<DocumentList>();
                    if (DocumentList != null)
                    {
                        for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
                        {
                            DocumentList Doc = new DocumentList();

                            DataTable Ds = DS.Tables[2];
                            if (DS.Tables[2].Rows[i]["Id"] != null && !DBNull.Value.Equals(DS.Tables[2].Rows[i]["Id"]))
                            {
                                Doc.DId = Convert.ToInt32(DS.Tables[2].Rows[i]["Id"]);
                            }
                            if (DS.Tables[2].Rows[i]["Description"] != null && !DBNull.Value.Equals(DS.Tables[2].Rows[i]["Description"]))
                            {
                                Doc.DcumenName = DS.Tables[2].Rows[i]["Description"].ToString();
                            }
                            if (DS.Tables[2].Rows[i]["link"] != null && !DBNull.Value.Equals(DS.Tables[2].Rows[i]["link"]))
                            {
                                Doc.DocPath = DS.Tables[2].Rows[i]["link"].ToString();
                            }

                            DocumentList.Add(Doc);
                        }
                    }
                    emp.DocumentList = DocumentList;

                    ////Find Log History//
                    //List<LogList> Log = new List<LogList>();
                    //if (Log != null)
                    //{
                    //    for (int i = 0; i < DS.Tables[3].Rows.Count; i++)

                    //    {
                    //        LogList loginfo = new LogList();
                    //        DataTable Ds = DS.Tables[3];
                    //        if (DS.Tables[3].Rows[i]["ColDescription"] != null && !DBNull.Value.Equals(DS.Tables[3].Rows[i]["ColDescription"]))
                    //        {
                    //            loginfo.ColDescription = DS.Tables[3].Rows[i]["ColDescription"].ToString();
                    //        }
                    //        if (DS.Tables[3].Rows[i]["OldValue"] != null && !DBNull.Value.Equals(DS.Tables[3].Rows[i]["OldValue"]))
                    //        {
                    //            loginfo.OldValue = DS.Tables[3].Rows[i]["OldValue"].ToString();
                    //        }
                    //        if (DS.Tables[3].Rows[i]["NewValue"] != null && !DBNull.Value.Equals(DS.Tables[3].Rows[i]["NewValue"]))
                    //        {
                    //            loginfo.NewValue = DS.Tables[3].Rows[i]["NewValue"].ToString();
                    //        }
                    //        if (DS.Tables[3].Rows[i]["ChangedBy"] != null && !DBNull.Value.Equals(DS.Tables[3].Rows[i]["ChangedBy"]))
                    //        {
                    //            loginfo.ChangedBy = DS.Tables[3].Rows[i]["ChangedBy"].ToString();
                    //        }
                    //        if (DS.Tables[3].Rows[i]["ChangedOn"] != null && !DBNull.Value.Equals(DS.Tables[3].Rows[i]["ChangedOn"]))
                    //        {
                    //            loginfo.ChangedOn = Convert.ToDateTime(DS.Tables[3].Rows[i]["ChangedOn"].ToString());
                    //        }
                    //        Log.Add(loginfo);
                    //    }
                    //}
                    //emp.LogList = Log;


                    //Edit for Edu Table Data

                    List<EmpInfoProEdunew> EmpInfoProEdu = new List<EmpInfoProEdunew>();
                    if (EmpInfoProEdu != null)
                    {
                        for (int i = 0; i < DS.Tables[1].Rows.Count; i++)

                        {
                            EmpInfoProEdunew Eduinfo = new EmpInfoProEdunew();

                            DataTable Ds = DS.Tables[1];

                            //if (DS.Tables[1].Rows[i]["EId"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["EId"]))
                            //{
                            //    Eduinfo.EId = Convert.ToInt32(DS.Tables[1].Rows[i]["EId"]);
                            //}

                            if (DS.Tables[1].Rows[i]["Id"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["Id"]))
                            {
                                Eduinfo.EId = Convert.ToInt32(DS.Tables[1].Rows[i]["Id"]);
                            }

                            if (DS.Tables[1].Rows[i]["Description"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["Description"]))
                            {
                                Eduinfo.Description = DS.Tables[1].Rows[i]["Description"].ToString();
                            }
                            if (DS.Tables[1].Rows[i]["Institute"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["Institute"]))
                            {
                                Eduinfo.Institute = DS.Tables[1].Rows[i]["Institute"].ToString();
                            }
                            if (DS.Tables[1].Rows[i]["Start_date"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["Start_date"]))
                            {
                                Eduinfo.StartDate = Convert.ToDateTime(DS.Tables[1].Rows[i]["Start_date"]);
                            }

                            if (DS.Tables[1].Rows[i]["End_date"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["End_date"]))
                            {
                                Eduinfo.EndDate = Convert.ToDateTime(DS.Tables[1].Rows[i]["End_date"]);
                            }

                            if (DS.Tables[1].Rows[i]["Type"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["Type"]))
                            {
                                Eduinfo.Type = DS.Tables[1].Rows[i]["Type"].ToString();
                            }

                            if (DS.Tables[1].Rows[i]["Score"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["Score"]))
                            {
                                Eduinfo.Score = Convert.ToInt32(DS.Tables[1].Rows[i]["Score"]);
                            }

                            EmpInfoProEdu.Add(Eduinfo);
                        }
                    }

                    emp.EmpInfoProEdus = EmpInfoProEdu;


                }
            }
            catch (Exception ex)
            {
                emp = null;
            }
            return emp;
        }

        public int GetEmployeeIdByLoginName(string LoginName)
        {
            int EmployeeId = 0;
            using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlTransaction transaction;
                using (transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "RDD_GetEmployeeIdByLoginName";
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        cmd.Parameters.Add("@p_LoginName", SqlDbType.NVarChar, 50).Value = LoginName;

                        cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        EmployeeId = (int)cmd.Parameters["@p_EmployeeId"].Value;
                        cmd.Dispose();
                        transaction.Commit();

                    }

                    catch (Exception ex)
                    {
                        EmployeeId = 0;
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return EmployeeId;

        }
        public string Delete(int EId)
        {
            string response = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlTransaction transaction;
                    using (transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_EduProf_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(EId);

                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
                            cmd.Dispose();
                            transaction.Commit();

                        }

                        catch (Exception ex)
                        {
                            response = "Error occured : " + ex.Message;
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;

        }
        public string DeleteAttc(int DId)
        {
            string response = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlTransaction transaction;
                    using (transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_DocAttachment_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(DId);

                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
                            cmd.Dispose();
                            transaction.Commit();

                        }

                        catch (Exception ex)
                        {
                            response = "Error occured : " + ex.Message;
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;

        }

        public DataSet GetDrop1(string username)
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                      new SqlParameter("@p_username",username),
                };
                dsModules = Com.ExecuteDataSet("Emp_Drop_Fill", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                dsModules = null;
                throw;
            }

            return dsModules;
        }
        public DataSet GetDrop2(string username, int? EMPID)
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@p_EmployeeId",EMPID),
                      new SqlParameter("@p_username",username),
                };
                dsModules = Com.ExecuteDataSet("Emp_Drop_Fill", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                dsModules = null;
                throw;
            }

            return dsModules;
        }

        public DataSet GetDropRole(string username)
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {

                      new SqlParameter("@p_username",username),
                };
                dsModules = Com.ExecuteDataSet("RDD_UserRole", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                dsModules = null;
                throw;
            }

            return dsModules;
        }

        public DataSet GetEmployeeConfigure(string UserRole, string type)
        
        {

            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                     new SqlParameter("@p_flag",type),
                      new SqlParameter("@p_username",UserRole),
                };
                dsModules = Com.ExecuteDataSet("RDD_EMPLOYEES_DISBALE", CommandType.StoredProcedure, parm);

            }
            catch (Exception)
            {
                dsModules = null;
                throw;
            }

            return dsModules;
        }

        public bool EmployeeConfigure(Employee_Configure _Configure)
        {
            bool t = false;
            var k = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] ParaDet2 = {
                                                 new SqlParameter("@p_flag","II"),
                                            new SqlParameter("@p_userrole",_Configure.UserRole),
                            };
                    t = Com.ExecuteNonQuery("RDD_Employess_Insert_Delete", ParaDet2);
                    if (_Configure.Employee_Configs != null)
                    {
                        t = true;
                        while (_Configure.Employee_Configs.Count > k)
                        {
                            SqlParameter[] ParaDet1 = {
                                                 new SqlParameter("@p_flag","I"),
                                            new SqlParameter("@p_userrole",_Configure.UserRole),
                                             new SqlParameter("@p_status",_Configure.Employee_Configs[k].status),

                                            new SqlParameter("@p_columname",_Configure.Employee_Configs[k].ColumnName),


                            };

                            var det1 = Com.ExecuteNonQuery("RDD_Employess_Insert_Delete", ParaDet1);
                            if (det1 == false)
                            {
                                t = false;
                            }
                            k++;
                        }
                    }

                    scope.Complete();




                }


            }
            catch (Exception ex)
            {

                t = false;
                if (_Configure.Employee_Configs == null)
                {
                    t = true;
                }
            }


            return t;
        }
        public bool Update(string useremail, string fname, string lname)
        {

            bool t = true;
            try
            {
                SqlParameter[] Para = {
                     new SqlParameter("@p_Email",useremail),
                    new SqlParameter("@p_FName",fname),
                    new SqlParameter("@p_LName",lname)


                };
                t = Com.ExecuteNonQuery("RDD_UpdateEmployeeLogin", Para);
                //using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                //{
                //    if (connection.State == ConnectionState.Closed)
                //    {
                //        connection.Open();
                //    }
                //    SqlTransaction transaction;
                //    using (transaction = connection.BeginTransaction())
                //    {
                //        try
                //        {
                //            SqlCommand cmd = new SqlCommand();
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            cmd.CommandText = "RDD_UpdateEmployeeLogin"; 
                //            cmd.Parameters.Add("@p_Email", SqlDbType.VarChar, 100).Value = useremail;
                //            cmd.Parameters.Add("@p_FName", SqlDbType.VarChar, 150).Value = fname;

                //            cmd.Parameters.Add("@p_LName", SqlDbType.VarChar, 150).Value = lname;

                //            cmd.ExecuteNonQuery();
                //            cmd.Dispose();
                //            transaction.Commit();

                //        }

                //        catch (Exception ex)
                //        {
                //          //  t = "Error occured : " + ex.Message;
                //            transaction.Rollback();
                //        }
                //        finally
                //        {
                //            if (connection.State == ConnectionState.Open)
                //            {
                //                connection.Close();
                //            }
                //        }
                //    }
                //}
            }

            catch (Exception ex)
            {
                // response = "Error occured : " + ex.Message;
            }
            return t;
            //bool t = true;
            //SqlCommand cmd = new SqlCommand();
            //try
            //{

            //    cmd = new SqlCommand();

            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandText = "RDD_UpdateEmployeeLogin";

            //    //cmd.Connection = connection;
            //    // cmd.Transaction = transaction;

            //    // cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Emp_ID;

            //    cmd.Parameters.Add("@p_FName", SqlDbType.VarChar, 150).Value = fname;

            //    cmd.Parameters.Add("@p_LName", SqlDbType.VarChar, 150).Value = lname;
            //    cmd.Parameters.Add("@p_Email", SqlDbType.VarChar, 100).Value = useremail;

            //   cmd.ExecuteNonQuery();
            //    cmd.Dispose();
            //   //  transaction.Commit();




            //}

            //catch (Exception ex)
            //{

            //}
            //finally
            //{

            //}
            //return t;

        }


    }
}

