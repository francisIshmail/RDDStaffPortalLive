using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels;

namespace RDDStaffPortal.DAL.HR
{
    public class EmployeeRegistrationDbOperation
    {
        public string Save(RDD_EmployeeRegistration Emp)
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
                            Int32 Emp_ID = 0;

                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Convert.ToInt16(Emp.EmployeeId);
                           
                            cmd.Parameters.Add("@p_FName", SqlDbType.VarChar, 150).Value = Emp.FName;

                            cmd.Parameters.Add("@p_LName", SqlDbType.VarChar, 150).Value = Emp.LName;
                            cmd.Parameters.Add("@p_Email", SqlDbType.VarChar, 100).Value = Emp.Email;
                            cmd.Parameters.Add("@p_Gender", SqlDbType.VarChar, 25).Value = Emp.Gender;
                            cmd.Parameters.Add("@p_Current_Address", SqlDbType.VarChar, 200).Value = Emp.Current_Address;
                            cmd.Parameters.Add("@p_Permanent_Address", SqlDbType.VarChar, 200).Value = Emp.Permanent_Address;
                            cmd.Parameters.Add("@p_Contact_No", SqlDbType.VarChar, 25).Value = Emp.Contact_No;
                            cmd.Parameters.Add("@p_Ext_no", SqlDbType.VarChar, 25).Value = Emp.Ext_no;
                            cmd.Parameters.Add("@p_IM_Id", SqlDbType.VarChar, 150).Value = Emp.IM_Id;
                            cmd.Parameters.Add("@p_Marital_Status", SqlDbType.VarChar, 25).Value = Emp.Marital_Status;

                            cmd.Parameters.Add("@p_DOB", SqlDbType.DateTime).Value = Emp.DOB;


                            cmd.Parameters.Add("@p_Citizenship", SqlDbType.VarChar, 25).Value = Emp.Citizenship;
                            cmd.Parameters.Add("@p_DesigId", SqlDbType.Int).Value = Convert.ToInt16(Emp.DesigId);
                            cmd.Parameters.Add("@p_DeptId", SqlDbType.Int).Value = Convert.ToInt16(Emp.DeptId);
                            cmd.Parameters.Add("@p_Emergency_Contact", SqlDbType.VarChar, 25).Value = Emp.Emergency_Contact;
                            cmd.Parameters.Add("@p_passport_no", SqlDbType.VarChar, 25).Value = Emp.passport_no;

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = Emp.CreatedBy;
                            cmd.Parameters.Add("@p_ManagerName", SqlDbType.VarChar, 10).Value = Emp.ManagerName;
                            cmd.Parameters.Add("@p_CountryCode", SqlDbType.VarChar, 10).Value = Emp.CountryCodeName;
                            cmd.Parameters.Add("@p_EmployeeNo", SqlDbType.VarChar, 10).Value = Emp.EmployeeNo;

                            cmd.Parameters.Add("@p_Empstatus", SqlDbType.Int).Value = Convert.ToInt16(Emp.StatusId);
                            cmd.Parameters.Add("@p_EmpType", SqlDbType.VarChar, 150).Value = Emp.type_of_employement;
                            cmd.Parameters.Add("@p_Joining_Date", SqlDbType.DateTime).Value = Emp.Joining_Date;
                            cmd.Parameters.Add("@p_no_of_child", SqlDbType.Int).Value = Convert.ToInt16(Emp.No_child);
                            cmd.Parameters.Add("@p_NationalId", SqlDbType.VarChar, 25).Value = Emp.National_id;
                            cmd.Parameters.Add("@p_Contract_Start_date", SqlDbType.DateTime).Value = Emp.Contract_Start_date;
                            cmd.Parameters.Add("@p_Note", SqlDbType.VarChar, 100).Value = Emp.Note;
                            



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
                            cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(Emp.FId);

                            cmd.Parameters.Add("@p_Currency", SqlDbType.VarChar, 50).Value = Emp.Currency;
                            cmd.Parameters.Add("@p_Salary", SqlDbType.Int).Value = Emp.Salary;
                            cmd.Parameters.Add("@p_Salary_Start_Date", SqlDbType.Date).Value = Emp.Salary_Start_Date;
                            cmd.Parameters.Add("@p_Remark", SqlDbType.VarChar, 50).Value = Emp.Remark;
                            cmd.Parameters.Add("@p_Account_No", SqlDbType.VarChar, 50).Value = Emp.Account_No;
                            cmd.Parameters.Add("@p_Bank_Name", SqlDbType.VarChar, 50).Value = Emp.Bank_Name;
                            cmd.Parameters.Add("@p_Branch_Name", SqlDbType.VarChar, 50).Value = Emp.Branch_Name;
                            cmd.Parameters.Add("@p_Bank_Code", SqlDbType.VarChar, 50).Value = Emp.Bank_Code;
                            cmd.Parameters.Add("@p_Tax_no", SqlDbType.VarChar, 25).Value = Emp.Tax_no;
                            cmd.Parameters.Add("@p_Insurance_no", SqlDbType.VarChar, 50).Value = Emp.Insurance_no;
                            cmd.Parameters.Add("@p_other_ref_no", SqlDbType.VarChar, 50).Value = Emp.other_ref_no;
                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = Emp.CreatedBy;


                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;


                            cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();

                            cmd.Dispose();

                           // cmd = new SqlCommand();

                           // cmd.CommandType = CommandType.StoredProcedure;
                           // cmd.CommandText = "RDD_Employees_Cou_InsertUpdate";
                           // cmd.Connection = connection;
                           // cmd.Transaction = transaction;

                           // cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Emp_ID;
                           // cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(Emp.CId);
                           // cmd.Parameters.Add("@p_Country", SqlDbType.VarChar, 800).Value = Emp.CountryCode;
                           // // cmd.Parameters.Add("@p_BU", SqlDbType.VarChar, 800).Value = Emp.ItmsGrpNam;
                           // cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = Emp.CreatedBy;
                           
                           // cmd.ExecuteNonQuery();
                           

                           //// response = cmd.Parameters["@p_Response"].Value.ToString();
                           // cmd.Dispose();
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
                        if (DS.Tables[0].Rows[0]["EmployeeId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["EmployeeId"]))
                        {
                            emp.EmployeeId = Convert.ToInt32(DS.Tables[0].Rows[0]["EmployeeId"]);
                        }

                        if (DS.Tables[0].Rows[0]["FName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["FName"]))
                        {
                            emp.FName = DS.Tables[0].Rows[0]["FName"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["LName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["LName"]))
                        {
                            emp.LName = DS.Tables[0].Rows[0]["LName"].ToString();
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
                    }



                }
            }
            catch (Exception ex)
            {
                emp = null;
            }
            return emp;
        }
    }
}

