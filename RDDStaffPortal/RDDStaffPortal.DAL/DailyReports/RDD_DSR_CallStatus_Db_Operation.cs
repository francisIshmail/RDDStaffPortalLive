using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.DailyReports
{
  public  class RDD_DSR_CallStatus_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        public RDD_DSR_CallStatus Save(RDD_DSR_CallStatus rDD_DSR_CallStatus)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (!rDD_DSR_CallStatus.Editflag)
                    {
                        rDD_DSR_CallStatus.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_DSR_CallStatus.ActionType = "Update";
                    }
                    SqlParameter[] parm ={ new SqlParameter("@p_ID",rDD_DSR_CallStatus.ID),
                        new SqlParameter("@p_CallStatus",rDD_DSR_CallStatus.CallStatus),
                        new SqlParameter("@p_CreatedBy",rDD_DSR_CallStatus.CreatedBy),
                        new SqlParameter("@p_CreatedOn",rDD_DSR_CallStatus.CreatedOn),
                        new SqlParameter("@p_LastUpdatedBy",rDD_DSR_CallStatus.LastUpdatedBy),
                        new SqlParameter("@p_LastUpdatedOn",rDD_DSR_CallStatus.LastUpdatedOn),
                        new SqlParameter("@p_Action",rDD_DSR_CallStatus.ActionType),
                         new SqlParameter("@p_response",response)   };

                    List<Outcls> outcls = new List<Outcls>();
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_CallStatus_Insert_Update_delete", parm);
                    rDD_DSR_CallStatus.Saveflag = outcls[0].Outtf;
                    rDD_DSR_CallStatus.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }

            }
            catch (Exception ex)
            {
                rDD_DSR_CallStatus.ErrorMsg = ex.Message;
                rDD_DSR_CallStatus.Saveflag = false;
            }

            return rDD_DSR_CallStatus;
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
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_CallStatus_Insert_Update_delete", parm);
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

        public List<RDD_DSR_CallStatus> GetList()
        {
            List<RDD_DSR_CallStatus> GetListData = new List<RDD_DSR_CallStatus>();
            try
            {
                SqlParameter[] parm ={
                         new SqlParameter("@p_Action","All")  };
                DataSet ds = Com.ExecuteDataSet("RDD_DSR_CallStatus_GET", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_DSR_CallStatus()
                        {
                            CallStatus = !string.IsNullOrWhiteSpace(dr["CallStatus"].ToString()) ? dr["CallStatus"].ToString() : "",
                            ID = !string.IsNullOrWhiteSpace(dr["ID"].ToString()) ? Convert.ToInt32(dr["ID"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception)
            {
                GetListData.Add(new RDD_DSR_CallStatus()
                {
                    CallStatus = "",
                    ID = 0
                });
            }

            return GetListData;

        }


        public RDD_DSR_CallStatus GetData(string Id)
        {
            RDD_DSR_CallStatus RDD_DSR_CallStatus = new RDD_DSR_CallStatus();
            try
            {
                SqlParameter[] parm ={
                new SqlParameter("@p_ID",Id)  ,
                 new SqlParameter("@p_Action","Single")  };
                DataSet ds = Com.ExecuteDataSet("RDD_DSR_CallStatus_GET", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_DSR_CallStatus.CallStatus = !string.IsNullOrWhiteSpace(dr["CallStatus"].ToString()) ? dr["CallStatus"].ToString() : "";
                        RDD_DSR_CallStatus.Editflag = true;
                    }
                }
            }
            catch (Exception)
            {
                RDD_DSR_CallStatus.CallStatus = "";
                RDD_DSR_CallStatus.ID = 0;
                RDD_DSR_CallStatus.Editflag = false;
            }
            return RDD_DSR_CallStatus;
        }

    }
}
