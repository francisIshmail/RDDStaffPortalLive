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
   public class RDD_DSR_NextAction_Db_Operation
    {

        CommonFunction Com = new CommonFunction();
        public RDD_DSR_NextAction Save(RDD_DSR_NextAction rDD_DSR_Next)
        {
            try
            {
                using (TransactionScope scope=new TransactionScope())
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
                        new SqlParameter("@p_NextAction",rDD_DSR_Next.NextAction),
                        new SqlParameter("@p_CreatedBy",rDD_DSR_Next.CreatedBy),
                        new SqlParameter("@p_CreatedOn",rDD_DSR_Next.CreatedOn),
                        new SqlParameter("@p_LastUpdatedBy",rDD_DSR_Next.LastUpdatedBy),
                        new SqlParameter("@p_LastUpdatedOn",rDD_DSR_Next.LastUpdatedOn),
                        new SqlParameter("@p_Action",rDD_DSR_Next.ActionType),
                         new SqlParameter("@p_response",response)   };

                    List<Outcls>  outcls= new List<Outcls>();
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_NextAction_Insert_Update_delete", parm);
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
                using (TransactionScope scope=new TransactionScope())
                {
                    string response = string.Empty;
                    SqlParameter[] parm ={ new SqlParameter("@p_ID",Id),
                         new SqlParameter("@p_Action","Delete"),
                         new SqlParameter("@p_response",response)   };
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_NextAction_Insert_Update_delete", parm);
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

        public List<RDD_DSR_NextAction> GetList()
        {
            List<RDD_DSR_NextAction> GetListData= new List<RDD_DSR_NextAction>();
            try
            {
                SqlParameter[] parm ={ 
                         new SqlParameter("@p_Action","All")  };
            DataSet ds = Com.ExecuteDataSet("RDD_DSR_NextAction_GET",CommandType.StoredProcedure,parm);
          
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_DSR_NextAction()
                        {
                            NextAction = !string.IsNullOrWhiteSpace(dr["NextAction"].ToString()) ? dr["NextAction"].ToString() : "",
                            ID=!string.IsNullOrWhiteSpace(dr["ID"].ToString())?Convert.ToInt32(dr["ID"].ToString()):0
                        });
                    }
                }
            }
            catch (Exception)
            {
                GetListData.Add(new RDD_DSR_NextAction()
                {
                    NextAction =  "",
                    ID=0
                });
            }
           
            return GetListData;
            
        }


        public RDD_DSR_NextAction GetData(string Id)
        {
            RDD_DSR_NextAction rDD_DSR_NextAction = new RDD_DSR_NextAction();
            try
            {
                SqlParameter[] parm ={
                new SqlParameter("@p_ID",Id)  ,
                 new SqlParameter("@p_Action","Single")  };
                DataSet ds = Com.ExecuteDataSet("RDD_DSR_NextAction_GET", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_DSR_NextAction.NextAction = !string.IsNullOrWhiteSpace(dr["NextAction"].ToString()) ? dr["NextAction"].ToString() : "";
                        rDD_DSR_NextAction.Editflag = true;
                    }
                }
            }
            catch (Exception)
            {
                rDD_DSR_NextAction.NextAction = "";
                rDD_DSR_NextAction.Editflag = false;
            }           
             return rDD_DSR_NextAction;
        }

    }
}
