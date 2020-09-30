using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.DailyReports
{
    public class RDD_DSR_ModeOfCall_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        public RDD_DSR_ModeOfCall Save(RDD_DSR_ModeOfCall rDD_DSR_Next)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (!rDD_DSR_Next.Editflag)
                    {
                        rDD_DSR_Next.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_DSR_Next.ActionType = "Update";
                    }
                    SqlParameter[] parm ={ new SqlParameter("@p_ID",rDD_DSR_Next.ID),
                        new SqlParameter("@p_ModeOfCall",rDD_DSR_Next.ModeOfCall),
                        new SqlParameter("@p_CreatedBy",rDD_DSR_Next.CreatedBy),
                        new SqlParameter("@p_CreatedOn",rDD_DSR_Next.CreatedOn),
                        new SqlParameter("@p_LastUpdatedBy",rDD_DSR_Next.LastUpdatedBy),
                        new SqlParameter("@p_LastUpdatedOn",rDD_DSR_Next.LastUpdatedOn),
                        new SqlParameter("@p_Action",rDD_DSR_Next.ActionType),
                         new SqlParameter("@p_response",response)   };

                    List<Outcls> outcls = new List<Outcls>();
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_ModeOfCall_Insert_Update_delete", parm);
                    rDD_DSR_Next.Saveflag = outcls[0].Outtf;
                    rDD_DSR_Next.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }

            }
            catch (Exception ex)
            {
                rDD_DSR_Next.ErrorMsg = ex.Message;
                rDD_DSR_Next.Saveflag = false;
            }

            return rDD_DSR_Next;
        }

        public List<Outcls> DeleteFlag(string Id)
        {
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    SqlParameter[] parm ={ new SqlParameter("@p_ID",Id),
                         new SqlParameter("@p_Action","Delete"),
                         new SqlParameter("@p_response",response)   };
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_ModeOfCall_Insert_Update_delete", parm);
                    scope.Complete();
                }

            }
            catch (Exception ex)
            {

                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }

        public List<RDD_DSR_ModeOfCall> GetList()
        {
            List<RDD_DSR_ModeOfCall> GetListData = new List<RDD_DSR_ModeOfCall>();
            try
            {
                SqlParameter[] parm ={
                         new SqlParameter("@p_Action","All")  };
                DataSet ds = Com.ExecuteDataSet("RDD_DSR_ModeOfCall_GET", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_DSR_ModeOfCall()
                        {
                            ModeOfCall = !string.IsNullOrWhiteSpace(dr["ModeOfCall"].ToString()) ? dr["ModeOfCall"].ToString() : "",
                            ID = !string.IsNullOrWhiteSpace(dr["ID"].ToString()) ? Convert.ToInt32(dr["ID"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception)
            {
                GetListData.Add(new RDD_DSR_ModeOfCall()
                {
                    ModeOfCall = "",
                    ID = 0
                });
            }

            return GetListData;

        }


        public RDD_DSR_ModeOfCall GetData(string Id)
        {
            RDD_DSR_ModeOfCall RDD_DSR_ModeOfCall = new RDD_DSR_ModeOfCall();
            try
            {
                SqlParameter[] parm ={
                new SqlParameter("@p_ID",Id)  ,
                 new SqlParameter("@p_Action","Single")  };
                DataSet ds = Com.ExecuteDataSet("RDD_DSR_ModeOfCall_GET", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_DSR_ModeOfCall.ModeOfCall = !string.IsNullOrWhiteSpace(dr["ModeOfCall"].ToString()) ? dr["ModeOfCall"].ToString() : "";
                        RDD_DSR_ModeOfCall.Editflag = true;
                    }
                }
            }
            catch (Exception)
            {
                RDD_DSR_ModeOfCall.ModeOfCall = "";
                RDD_DSR_ModeOfCall.ID = 0;
                RDD_DSR_ModeOfCall.Editflag = false;
            }
            return RDD_DSR_ModeOfCall;
        }

    }
}
