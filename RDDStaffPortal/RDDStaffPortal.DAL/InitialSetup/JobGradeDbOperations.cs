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
   public class JobGradeDbOperations
    {

        
        public string Save(RDD_JobGrade jobGrade)
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
                            cmd.CommandText = "RDDJobGrade_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_JobGradeId", SqlDbType.Int).Value = Convert.ToInt16(jobGrade.JobGradeId);
                            cmd.Parameters.Add("@p_JobGrade", SqlDbType.VarChar, 50).Value = jobGrade.JobGradeName;
                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value = jobGrade.IsActive;
                          
                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 50).Value = jobGrade.CreatedBy;

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

        
        public List<RDD_JobGrade> GetJobGradeList()
        {
            List<RDD_JobGrade> _GradeList = new List<RDD_JobGrade>();
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
                DataSet dsjobgrade = Db.myGetDS("EXEC dbo.RDDJobGradeName_GetData");
                if (dsjobgrade.Tables.Count > 0)
                {
                    DataTable dtModule = dsjobgrade.Tables[0];
                    for (int i = 0; i < dsjobgrade.Tables[0].Rows.Count; i++)
                    {
                        RDD_JobGrade itm = new RDD_JobGrade();
                        if (dsjobgrade.Tables[0].Rows[i]["JobGradeId"] != null && !DBNull.Value.Equals(dsjobgrade.Tables[0].Rows[i]["JobGradeId"]))
                        {
                            itm.JobGradeId = Convert.ToInt32(dsjobgrade.Tables[0].Rows[i]["JobGradeId"]);
                        }
                        if (dsjobgrade.Tables[0].Rows[i]["JobGradeName"] != null && !DBNull.Value.Equals(dsjobgrade.Tables[0].Rows[i]["JobGradeName"]))
                        {
                            itm.JobGradeName = dsjobgrade.Tables[0].Rows[i]["JobGradeName"].ToString();
                        }
                        if (dsjobgrade.Tables[0].Rows[i]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["IsActive"]))
                        {
                            itm.IsActive = Convert.ToBoolean(dsjobgrade.Tables[0].Rows[i]["IsActive"]);
                        }
                        _GradeList.Add(itm);
                    }

                }
            }

            catch (Exception ex)
            {
                _GradeList = null;
            }
            return _GradeList;
        }



        public RDD_JobGrade GetJobGradeID(int DeptId)
        {
            RDD_JobGrade _JobGrade = new RDD_JobGrade();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet djobGrade = Db.myGetDS("EXEC DeptName_GetData " + DeptId.ToString());
                if (djobGrade.Tables.Count > 0)
                {
                    DataTable dtjobgrade = djobGrade.Tables[0];
                    if (dtjobgrade.Rows[0]["DeptId"] != null && !DBNull.Value.Equals(dtjobgrade.Rows[0]["DeptId"]))
                    {
                        _JobGrade.JobGradeId = Convert.ToInt32(dtjobgrade.Rows[0]["DeptId"]);
                    }
                    if (dtjobgrade.Rows[0]["DeptName"] != null && !DBNull.Value.Equals(dtjobgrade.Rows[0]["DeptName"]))
                    {
                        _JobGrade.JobGradeName = dtjobgrade.Rows[0]["DeptName"].ToString();
                    }

                    if (dtjobgrade.Rows[0]["IsActive"] != null && !DBNull.Value.Equals(dtjobgrade.Rows[0]["IsActive"]))
                    {
                        _JobGrade.IsActive = Convert.ToBoolean(dtjobgrade.Rows[0]["IsActive"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                _JobGrade = null;
            }
            return _JobGrade;



        }

        public string Delete(int JobGradeId)
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
                            cmd.CommandText = "RDDJobGrade_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_JobGradeId", SqlDbType.Int).Value = Convert.ToInt16(JobGradeId);

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

