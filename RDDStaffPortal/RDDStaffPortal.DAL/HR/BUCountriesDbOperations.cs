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
    public class BUCountriesDbOperations
    {
        public string Save(RDD_BUCountries BUCountrie)

        {
          //string username = User.Identity.Name;
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
                            if (BUCountrie != null)
                            {
                                for (int i = 0; i < BUCountrie.BUCountriesnew.Count; i++)
                                {

                                    SqlCommand cmd = new SqlCommand();

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "RDD_BUMapping_InsertUpdate";
                                    cmd.Connection = connection;
                                    cmd.Transaction = transaction;



                                    cmd.Parameters.Add("@p_CreatedBy", SqlDbType.VarChar, 50).Value = BUCountrie.CreatedBy;

                                   cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Value = BUCountrie.BUCountriesnew[i].EmpId;
                                    cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = BUCountrie.BUCountriesnew[i].CId;
                                    // cmd.Parameters.Add("@p_BU", SqlDbType.Int).Value = BUCountrie.BUCountriesnew[i].CId;

                                    cmd.Parameters.Add("@p_BU", SqlDbType.Int).Value = BUCountrie.BUCountriesnew[i].BUId;
                                    cmd.Parameters.Add("@p_CountryCode", SqlDbType.VarChar, 50).Value = BUCountrie.BUCountriesnew[i].CountryCode;                                                                     
                                    cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                                    cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;
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




        public string Delete(int DId,string name)
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
                            cmd.CommandText = "RDD_BUMapping_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_Id", SqlDbType.Int).Value = Convert.ToInt16(DId);
                            cmd.Parameters.Add("@p_UpdatedBy", SqlDbType.VarChar, 50).Value = name;
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