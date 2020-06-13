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
   public class JobBandDbOperations
    {

        public string Save(RDD_JobBand jobband)
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
                            cmd.CommandText = "RDDJobBand_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_JobBandId", SqlDbType.Int).Value = Convert.ToInt16(jobband.JobBandId);
                            cmd.Parameters.Add("@p_JobBand", SqlDbType.VarChar, 50).Value = jobband.JobBandName;
                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value = jobband.IsActive;

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 50).Value = jobband.CreatedBy;

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

        
        public List<RDD_JobBand> GetJobBandList()
        {
            List<RDD_JobBand> _BandList = new List<RDD_JobBand>();
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
                DataSet dsjobband = Db.myGetDS("EXEC dbo.RDDJobBandName_GetData");
                if (dsjobband.Tables.Count > 0)
                {
                    DataTable dtModule = dsjobband.Tables[0];
                    for (int i = 0; i < dsjobband.Tables[0].Rows.Count; i++)
                    {
                        RDD_JobBand itm = new RDD_JobBand();
                        if (dsjobband.Tables[0].Rows[i]["JobBandId"] != null && !DBNull.Value.Equals(dsjobband.Tables[0].Rows[i]["JobBandId"]))
                        {
                            itm.JobBandId = Convert.ToInt32(dsjobband.Tables[0].Rows[i]["JobBandId"]);
                        }
                        if (dsjobband.Tables[0].Rows[i]["JobBandName"] != null && !DBNull.Value.Equals(dsjobband.Tables[0].Rows[i]["JobBandName"]))
                        {
                            itm.JobBandName = dsjobband.Tables[0].Rows[i]["JobBandName"].ToString();
                        }
                        if (dsjobband.Tables[0].Rows[i]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["IsActive"]))
                        {
                            itm.IsActive = Convert.ToBoolean(dsjobband.Tables[0].Rows[i]["IsActive"]);
                        }
                        _BandList.Add(itm);
                    }

                }
            }

            catch (Exception ex)
            {
                _BandList = null;
            }
            return _BandList;
        }



        //public RDD_JobGrade GetJobGradeID(int DeptId)
        //{
        //    RDD_JobGrade _JobGrade = new RDD_JobGrade();
        //    try
        //    {
        //        Db.constr = Global.getConnectionStringByName("tejSAP");
        //        DataSet djobGrade = Db.myGetDS("EXEC DeptName_GetData " + DeptId.ToString());
        //        if (djobGrade.Tables.Count > 0)
        //        {
        //            DataTable dtjobgrade = djobGrade.Tables[0];
        //            if (dtjobgrade.Rows[0]["DeptId"] != null && !DBNull.Value.Equals(dtjobgrade.Rows[0]["DeptId"]))
        //            {
        //                _JobGrade.JobGradeId = Convert.ToInt32(dtjobgrade.Rows[0]["DeptId"]);
        //            }
        //            if (dtjobgrade.Rows[0]["DeptName"] != null && !DBNull.Value.Equals(dtjobgrade.Rows[0]["DeptName"]))
        //            {
        //                _JobGrade.JobGradeName = dtjobgrade.Rows[0]["DeptName"].ToString();
        //            }

        //            if (dtjobgrade.Rows[0]["IsActive"] != null && !DBNull.Value.Equals(dtjobgrade.Rows[0]["IsActive"]))
        //            {
        //                _JobGrade.IsActive = Convert.ToBoolean(dtjobgrade.Rows[0]["IsActive"].ToString());
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        _JobGrade = null;
        //    }
        //    return _JobGrade;



        //}

        public string Delete(int JobBandId)
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
                            cmd.CommandText = "RDDJobBand_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_JobBandId", SqlDbType.Int).Value = Convert.ToInt16(JobBandId);

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
