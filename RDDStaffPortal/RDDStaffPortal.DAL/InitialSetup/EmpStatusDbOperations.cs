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
  public  class EmpStatusDbOperations
    {
        public string Save(RDD_EmploymentStatus Status)
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
                            cmd.CommandText = "RDD_StatusName_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_StatusId", SqlDbType.Int).Value = Convert.ToInt16(Status.StatusId);
                            cmd.Parameters.Add("@p_StatusName", SqlDbType.VarChar, 50).Value = Status.StatusName;
                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value = Status.IsActive;
                            // cmd.Parameters.Add("@p_Status",SqlDbType.Bit).Value = Dept.Status;
                            //cmd.Parameters.Add("@p_LoggedInUser", SqlDbType.NVarChar, 50).Value = User.Identity.Name;

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 50).Value = Status.CreatedBy;

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


        public List<RDD_EmploymentStatus> GetStatusList()
        {
            List<RDD_EmploymentStatus> _StatusList = new List<RDD_EmploymentStatus>();
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
                DataSet dsStatus = Db.myGetDS("EXEC dbo.RDD_StatusName_GetData");
                if (dsStatus.Tables.Count > 0)
                {
                    DataTable dtModule = dsStatus.Tables[0];
                    for (int i = 0; i < dsStatus.Tables[0].Rows.Count; i++)
                    {
                        RDD_EmploymentStatus itm = new RDD_EmploymentStatus();
                        if (dsStatus.Tables[0].Rows[i]["StatusId"] != null && !DBNull.Value.Equals(dsStatus.Tables[0].Rows[i]["StatusId"]))
                        {
                            itm.StatusId = Convert.ToInt32(dsStatus.Tables[0].Rows[i]["StatusId"]);
                        }
                        if (dsStatus.Tables[0].Rows[i]["StatusName"] != null && !DBNull.Value.Equals(dsStatus.Tables[0].Rows[i]["StatusName"]))
                        {
                            itm.StatusName = dsStatus.Tables[0].Rows[i]["StatusName"].ToString();
                        }
                        if (dsStatus.Tables[0].Rows[i]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["IsActive"]))
                        {
                            itm.IsActive = Convert.ToBoolean(dsStatus.Tables[0].Rows[i]["IsActive"]);
                        }
                        _StatusList.Add(itm);
                    }

                }
            }

            catch (Exception ex)
            {
                _StatusList = null;
            }
            return _StatusList;
        }



        public RDD_EmploymentStatus GetStatusID(int StatusId)
        {
            RDD_EmploymentStatus _Status = new RDD_EmploymentStatus();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsDept = Db.myGetDS("EXEC RDD_StatusName_GetData " + StatusId.ToString());
                if (dsDept.Tables.Count > 0)
                {
                    DataTable dtDept = dsDept.Tables[0];
                    if (dtDept.Rows[0]["StatusId"] != null && !DBNull.Value.Equals(dtDept.Rows[0]["StatusId"]))
                    {
                        _Status.StatusId = Convert.ToInt32(dtDept.Rows[0]["StatusId"]);
                    }
                    if (dtDept.Rows[0]["StatusName"] != null && !DBNull.Value.Equals(dtDept.Rows[0]["StatusName"]))
                    {
                        _Status.StatusName = dtDept.Rows[0]["StatusName"].ToString();
                    }

                    if (dtDept.Rows[0]["IsActive"] != null && !DBNull.Value.Equals(dtDept.Rows[0]["IsActive"]))
                    {
                        _Status.IsActive = Convert.ToBoolean(dtDept.Rows[0]["IsActive"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                _Status = null;
            }
            return _Status;



        }

        public string Delete(int StatusId)
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
                            cmd.CommandText = "RDD_StatusName_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_StatusId", SqlDbType.Int).Value = Convert.ToInt16(StatusId);

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
