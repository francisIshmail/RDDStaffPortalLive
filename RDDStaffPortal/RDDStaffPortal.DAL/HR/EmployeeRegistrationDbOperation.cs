using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels;
using static RDDStaffPortal.DAL.CommonFunction;


namespace RDDStaffPortal.DAL.HR
{
    public class EmployeeRegistrationDbOperation
    {
       // public string Save(RDD_EmployeeRegistration EmpData)
         public string Save(RDD_EmployeeRegistration EmpData, List<RDD_EmployeeRegistration> EmpInfoProEdu)
      
        {
            string response = string.Empty;
            string result = string.Empty;
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

                            byte[] file;
                            using (var stream = new FileStream(EmpData.ImagePath1, FileMode.Open, FileAccess.Read))
                            {
                                using (var reader = new BinaryReader(stream))
                                {
                                    file = reader.ReadBytes((int)stream.Length);
                                }
                            }
                            Int32 Emp_ID = 0;

                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Convert.ToInt16(EmpData.EmployeeId);


                            //.Parameters.Add("@p_ImagePath", SqlDbType.VarBinary,1000).Value = file;
                            cmd.Parameters.Add("@p_ImagePath", SqlDbType.VarBinary, file.Length).Value = file;
                            cmd.Parameters.Add("@p_LogoType", SqlDbType.VarChar, 20).Value = EmpData.LogoType;

                            //cmd.Parameters.Add("@p_ePath", SqlDbType.VarBinary, 1000).Value = EmpData.ImagePath;
                            cmd.Parameters.Add("@p_FName", SqlDbType.VarChar, 150).Value = EmpData.FName;

                            cmd.Parameters.Add("@p_LName", SqlDbType.VarChar, 150).Value = EmpData.LName;
                            cmd.Parameters.Add("@p_Email", SqlDbType.VarChar, 100).Value = EmpData.Email;
                            cmd.Parameters.Add("@p_Gender", SqlDbType.VarChar, 25).Value = EmpData.Gender;
                            cmd.Parameters.Add("@p_Current_Address", SqlDbType.VarChar, 200).Value = EmpData.Current_Address;
                            cmd.Parameters.Add("@p_Permanent_Address", SqlDbType.VarChar, 200).Value = EmpData.Permanent_Address;
                            cmd.Parameters.Add("@p_Contact_No", SqlDbType.VarChar, 25).Value = EmpData.Contact_No;
                            // cmd.Parameters.Add("@p_Ext_no", SqlDbType.VarChar, 25).Value = Emp.Ext_no;

                            if (EmpData.Ext_no == null)
                            {
                                cmd.Parameters.Add("@p_Ext_no", SqlDbType.VarChar, 252).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Ext_no", SqlDbType.VarChar, 25).Value = EmpData.Ext_no;
                            }

                            if (EmpData.IM_Id == null)
                            {
                                cmd.Parameters.Add("@p_IM_Id", SqlDbType.VarChar, 150).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_IM_Id", SqlDbType.VarChar, 150).Value = EmpData.IM_Id;
                            }
                            cmd.Parameters.Add("@p_ManagerId", SqlDbType.Int).Value = Convert.ToInt16(EmpData.ManagerId);

                            if (EmpData.About == null)
                            {
                                cmd.Parameters.Add("@p_About", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_About", SqlDbType.VarChar, 1000).Value = EmpData.About;
                            }
                            // cmd.Parameters.Add("@p_IM_Id", SqlDbType.VarChar, 150).Value = Emp.IM_Id;
                            cmd.Parameters.Add("@p_Marital_Status", SqlDbType.VarChar, 25).Value = EmpData.Marital_Status;

                            cmd.Parameters.Add("@p_DOB", SqlDbType.DateTime).Value = EmpData.DOB;


                            cmd.Parameters.Add("@p_Citizenship", SqlDbType.VarChar, 25).Value = EmpData.Citizenship;
                            cmd.Parameters.Add("@p_DesigId", SqlDbType.Int).Value = Convert.ToInt16(EmpData.DesigId);
                            cmd.Parameters.Add("@p_DeptId", SqlDbType.Int).Value = Convert.ToInt16(EmpData.DeptId);
                            ///cmd.Parameters.Add("@p_Emergency_Contact", SqlDbType.VarChar, 25).Value = Emp.Emergency_Contact;
                            if (EmpData.Emergency_Contact == null)
                            {
                                cmd.Parameters.Add("@p_Emergency_Contact", SqlDbType.VarChar, 25).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Emergency_Contact", SqlDbType.VarChar, 25).Value = EmpData.Emergency_Contact;
                            }
                            //cmd.Parameters.Add("@p_passport_no", SqlDbType.VarChar, 25).Value = Emp.passport_no;

                            if (EmpData.passport_no == null)
                            {
                                cmd.Parameters.Add("@p_passport_no", SqlDbType.VarChar, 25).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_passport_no", SqlDbType.VarChar, 25).Value = EmpData.passport_no;
                            }
                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = EmpData.CreatedBy;
                            // cmd.Parameters.Add("@p_ManagerName", SqlDbType.VarChar, 10).Value = Emp.ManagerName;
                           
                            //if (EmpData.ManagerName == null)
                            //{
                            //    cmd.Parameters.Add("@p_ManagerName", SqlDbType.VarChar, 100).Value = DBNull.Value;
                            //}
                            //else
                            //{
                            //    cmd.Parameters.Add("@p_ManagerName", SqlDbType.VarChar, 100).Value = EmpData.ManagerName;
                            //}
                            cmd.Parameters.Add("@p_CountryCode", SqlDbType.VarChar, 10).Value = EmpData.CountryCodeName;
                            cmd.Parameters.Add("@p_EmployeeNo", SqlDbType.VarChar, 10).Value = EmpData.EmployeeNo;

                            cmd.Parameters.Add("@p_Empstatus", SqlDbType.Int).Value = Convert.ToInt16(EmpData.StatusId);
                            cmd.Parameters.Add("@p_EmpType", SqlDbType.VarChar, 150).Value = EmpData.type_of_employement;
                            cmd.Parameters.Add("@p_Joining_Date", SqlDbType.DateTime).Value = EmpData.Joining_Date;
                            cmd.Parameters.Add("@p_no_of_child", SqlDbType.Int).Value = Convert.ToInt16(EmpData.No_child);
                            cmd.Parameters.Add("@p_NationalId", SqlDbType.VarChar, 25).Value = EmpData.National_id;
                            cmd.Parameters.Add("@p_Contract_Start_date", SqlDbType.DateTime).Value = EmpData.Contract_Start_date;
                            // cmd.Parameters.Add("@p_Note", SqlDbType.VarChar, 100).Value = Emp.Note;

                            if (EmpData.Note == null)
                            {
                                cmd.Parameters.Add("@p_Note", SqlDbType.VarChar, 100).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Note", SqlDbType.VarChar, 100).Value = EmpData.Note;
                            }

                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value = EmpData.IsActive;
                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;


                            cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                            //result = cmd.Parameters["@p_Response"].Value.ToString();


                            cmd.ExecuteNonQuery();
                            Emp_ID = Convert.ToInt32(cmd.Parameters["@p_EmployeeIdOUT"].Value.ToString());

                            response = cmd.Parameters["@p_Response"].Value.ToString();

                            cmd.Dispose();

                            cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_Fin_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Emp_ID;
                            cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(EmpData.FId);

                            //cmd.Parameters.Add("@p_Currency", SqlDbType.VarChar, 50).Value = Emp.Currency;
                            if (EmpData.Currency == null)
                            {
                                cmd.Parameters.Add("@p_Currency", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Currency", SqlDbType.VarChar, 50).Value = EmpData.Currency;
                            }

                            //if (Emp.Salary == null)
                            //{
                            //    cmd.Parameters.Add("@p_Salary", SqlDbType.Int).Value = DBNull.Value;
                            //}
                            //else
                            //{
                            //    cmd.Parameters.Add("@p_Salary", SqlDbType.Int).Value = Emp.Salary;
                            //}


                            cmd.Parameters.Add("@p_Salary", SqlDbType.Int).Value = EmpData.Salary;

                            
                            if (EmpData.Salary_Start_Date.Year < (DateTime.Now.Year - 1))
                            {
                                cmd.Parameters.Add("@p_Salary_Start_Date", SqlDbType.Date).Value = DBNull.Value; ;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Salary_Start_Date", SqlDbType.Date).Value = EmpData.Salary_Start_Date;
                            }
                           

                            if (EmpData.Remark == null)
                            {
                                cmd.Parameters.Add("@p_Remark", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Remark", SqlDbType.VarChar, 50).Value = EmpData.Remark;
                            }

                            // cmd.Parameters.Add("@p_Remark", SqlDbType.VarChar, 50).Value = Emp.Remark;
                            // cmd.Parameters.Add("@p_Account_No", SqlDbType.VarChar, 50).Value = Emp.Account_No;
                            if (EmpData.Account_No == null)
                            {
                                cmd.Parameters.Add("@p_Account_No", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Account_No", SqlDbType.VarChar, 50).Value = EmpData.Account_No;
                            }

                            //cmd.Parameters.Add("@p_Bank_Name", SqlDbType.VarChar, 50).Value = Emp.Bank_Name;
                            if (EmpData.Bank_Name == null)
                            {
                                cmd.Parameters.Add("@p_Bank_Name", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Bank_Name", SqlDbType.VarChar, 50).Value = EmpData.Bank_Name;
                            }
                            //cmd.Parameters.Add("@p_Branch_Name", SqlDbType.VarChar, 50).Value = Emp.Branch_Name;

                            if (EmpData.Branch_Name == null)
                            {
                                cmd.Parameters.Add("@p_Branch_Name", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Branch_Name", SqlDbType.VarChar, 50).Value = EmpData.Branch_Name;
                            }

                            //cmd.Parameters.Add("@p_Bank_Code", SqlDbType.VarChar, 50).Value = Emp.Bank_Code;
                            if (EmpData.Bank_Code == null)
                            {
                                cmd.Parameters.Add("@p_Bank_Code", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Bank_Code", SqlDbType.VarChar, 50).Value = EmpData.Bank_Code;
                            }
                            //cmd.Parameters.Add("@p_Tax_no", SqlDbType.VarChar, 25).Value = Emp.Tax_no;
                            if (EmpData.Tax_no == null)
                            {
                                cmd.Parameters.Add("@p_Tax_no", SqlDbType.VarChar, 25).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Tax_no", SqlDbType.VarChar, 25).Value = EmpData.Tax_no;
                            }
                            // cmd.Parameters.Add("@p_Insurance_no", SqlDbType.VarChar, 50).Value = Emp.Insurance_no;
                            if (EmpData.Insurance_no == null)
                            {
                                cmd.Parameters.Add("@p_Insurance_no", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Insurance_no", SqlDbType.VarChar, 50).Value = EmpData.Insurance_no;
                            }
                            // cmd.Parameters.Add("@p_other_ref_no", SqlDbType.VarChar, 50).Value = Emp.other_ref_no;
                            if (EmpData.other_ref_no == null)
                            {
                                cmd.Parameters.Add("@p_other_ref_no", SqlDbType.VarChar, 50).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_other_ref_no", SqlDbType.VarChar, 50).Value = EmpData.other_ref_no;
                            }

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = EmpData.CreatedBy;


                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;


                            cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();

                            cmd.Dispose();


                            if (EmpInfoProEdu != null)
                            {
                                for (int i = 0; i < EmpInfoProEdu.Count; i++)
                                {

                                    cmd = new SqlCommand();

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "RDD_Employees_EduProf_InsertUpdate";
                                    cmd.Connection = connection;
                                    cmd.Transaction = transaction;

                                    cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Emp_ID;


                                    cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = EmpInfoProEdu[i].EId;

                                    if (EmpInfoProEdu[i].Type == null)
                                    {
                                        cmd.Parameters.Add("@p_Type", SqlDbType.VarChar, 150).Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@p_Type", SqlDbType.VarChar, 50).Value = EmpInfoProEdu[i].Type;
                                    }
                                    if (EmpInfoProEdu[i].Institute == null)
                                    {
                                        cmd.Parameters.Add("@p_Institute", SqlDbType.VarChar, 100).Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@p_Institute", SqlDbType.VarChar, 100).Value = EmpInfoProEdu[i].Institute;
                                    }
                                    // cmd.Parameters.Add("@p_Institute", SqlDbType.VarChar, 100).Value = EmpInfoProEdu[i].Institute;

                                    cmd.Parameters.Add("@p_Start_date", SqlDbType.DateTime).Value = EmpInfoProEdu[i].StartDate;



                                    cmd.Parameters.Add("@p_End_date", SqlDbType.DateTime).Value = EmpInfoProEdu[i].EndDate;


                                    if (EmpInfoProEdu[i].Description == null)
                                    {
                                        cmd.Parameters.Add("@p_Description", SqlDbType.VarChar, 500).Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@p_Description", SqlDbType.VarChar, 500).Value = EmpInfoProEdu[i].Description;

                                    }
                                    if (EmpInfoProEdu[i].Score == null)
                                    {
                                        cmd.Parameters.Add("@p_Score", SqlDbType.Int).Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add("@p_Score", SqlDbType.Int).Value = EmpInfoProEdu[i].Score;

                                    }



                                    // cmd.Parameters.Add("@p_Description", SqlDbType.VarChar, 500).Value = EmpInfoProEdu[i].Description;
                                    // cmd.Parameters.Add("@p_Score", SqlDbType.Int).Value = EmpInfoProEdu[i].Score;
                                    cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;

                                    cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;

                                    //cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = Emp.CreatedBy;

                                    cmd.ExecuteNonQuery();


                                    response = cmd.Parameters["@p_Response"].Value.ToString();
                                    cmd.Dispose();
                                }
                            }
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
                        if (DS.Tables[0].Rows[0]["CountryName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["CountryName"]))
                        {
                            emp.CountryName = DS.Tables[0].Rows[0]["CountryName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["EmployeeId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["EmployeeId"]))
                        {
                            emp.EmployeeId = Convert.ToInt32(DS.Tables[0].Rows[0]["EmployeeId"]);
                        }

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
                        if (DS.Tables[0].Rows[0]["manager"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["manager"]))
                        {
                            emp.ManagerName = DS.Tables[0].Rows[0]["manager"].ToString();
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

                            cmd.Parameters.Add("@p_LoginName", SqlDbType.NVarChar,50).Value = LoginName;

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

    }
}

