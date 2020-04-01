using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels;

namespace RDDStaffPortal.DAL.InitialSetup
{
    public class EmployeeRegistrationDbOperation
    {
        public string Save(RDD_EmployeeRegistration Emp)
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
                            Int32 Emp_ID = 0;

                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Convert.ToInt16(Emp.EmployeeId);
                            cmd.Parameters.Add("@p_EmployeeNo", SqlDbType.VarChar, 10).Value = Emp.EmployeeNo;
                            cmd.Parameters.Add("@p_FName", SqlDbType.VarChar, 150).Value = Emp.FName;
                            cmd.Parameters.Add("@p_LName", SqlDbType.VarChar, 150).Value = Emp.LName;
                            cmd.Parameters.Add("@p_Email", SqlDbType.VarChar, 100).Value = Emp.Email;
                            cmd.Parameters.Add("@p_Gender", SqlDbType.VarChar, 25).Value = Emp.Gender;
                             cmd.Parameters.Add("@p_Current_Address" , SqlDbType.VarChar, 200).Value = Emp.Current_Address;
                            cmd.Parameters.Add("@p_Permanent_Address" ,  SqlDbType.VarChar, 200).Value = Emp.Permanent_Address;
                            cmd.Parameters.Add("@p_Contact_No", SqlDbType.VarChar, 25).Value = Emp.Contact_No;
                            cmd.Parameters.Add("@p_Ext_no", SqlDbType.VarChar, 25).Value = Emp.Ext_no;
                            cmd.Parameters.Add("@p_IM_Id", SqlDbType.VarChar, 150).Value = Emp.IM_Id;
                            cmd.Parameters.Add("@p_Marital_Status", SqlDbType.VarChar, 25).Value = Emp.Marital_Status;

                            cmd.Parameters.Add("@p_DOB", SqlDbType.DateTime).Value = Emp.DOB;
                            

                            cmd.Parameters.Add("@p_Citizenship", SqlDbType.VarChar, 25).Value = Emp.Citizenship;
                            cmd.Parameters.Add("@p_DesigId", SqlDbType.Int).Value = Convert.ToInt16(Emp.DesigId);
                            cmd.Parameters.Add("@p_DeptId", SqlDbType.Int).Value = Convert.ToInt16(Emp.DeptId);
                            cmd.Parameters.Add("@p_Emergency_Contact",SqlDbType.VarChar, 25).Value = Emp.Emergency_Contact;
                            cmd.Parameters.Add("@p_passport_no",SqlDbType.VarChar, 25).Value = Emp.passport_no;                 

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 20).Value = Emp.CreatedBy;

                            //cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                           // cmd.ExecuteNonQuery();
                           // response = cmd.Parameters["@p_Response"].Value.ToString();

                            cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            Emp_ID = Convert.ToInt32(cmd.Parameters["@p_EmployeeIdOUT"].Value.ToString());


                            cmd.Dispose();

                            cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Employees_Fin_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = Emp_ID;
                            cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(Emp.FId);
                           
                            cmd.Parameters.Add("@p_Currency", SqlDbType.VarChar,50).Value = Emp.Currency;
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

                            cmd.ExecuteNonQuery();

                            cmd.Dispose();
                            transaction.Commit();
                            response = "Record Saved Successfully";


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

