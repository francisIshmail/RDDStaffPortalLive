using RDDStaffPortal.DAL.DataModels.SORCodeGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.SORCodeGenerator
{
    public class RDD_SORCodeGenerator_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        public List<SelectListItem> GetDatabaseList()
        {

            List<SelectListItem> GetDatabaseListData = new List<SelectListItem>();          

            try
            {
                SqlParameter[] parm ={
                        
                         new SqlParameter("@Type","GetDatabaselist")  };
                DataSet ds = Com.ExecuteDataSet("RDD_Get_GenerateSORCodeData", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetDatabaseListData.Add(new SelectListItem()
                        {
                            Text = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return GetDatabaseListData;
        }
        public DataSet GetRequestedByList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetRequestedByList")
                };
                ds = Com.ExecuteDataSet("RDD_Get_GenerateSORCodeData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet DraftSORNumVallidation(string DraftNo, string DBName)
        {
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {                
                 new SqlParameter("@p_DraftSORNum",DraftNo),
                 new SqlParameter("@p_DBName",DBName),
            };
            ds = Com.ExecuteDataSet("RDD_Validate_DraftSORNum", CommandType.StoredProcedure, prm);
            return ds;
        }
        public DataSet GetSORApprovaLCodeList()
        {
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
               
                 new SqlParameter("@Type","read"),
            };
            ds = Com.ExecuteDataSet("RDD_Get_SORApprovalCodeList", CommandType.StoredProcedure, prm);
            return ds;
        }
        public DataSet GetCACM(string DraftNo, string DBName)
        {
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                 new SqlParameter("@p_DraftDocEntry",DraftNo),
                 new SqlParameter("@p_DBName",DBName),
            };
            ds = Com.ExecuteDataSet("RDD_GetDraftSORApproverList", CommandType.StoredProcedure, prm);
            return ds;
        }
        public RDD_SORCodeGenerator SaveSOR(RDD_SORCodeGenerator rDD_SORCodeGenerator)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;                    
                    SqlParameter[] parm = {
                        new SqlParameter("@DBName",rDD_SORCodeGenerator.DBName),
                        new SqlParameter("@ProjectCode",rDD_SORCodeGenerator.ProjectCode),
                        new SqlParameter("@BUCode",rDD_SORCodeGenerator.BUCode),
                        new SqlParameter("@BU",rDD_SORCodeGenerator.BU),
                        new SqlParameter("@DraftSORKey",rDD_SORCodeGenerator.DraftSORKey),
                        new SqlParameter("@DraftSORNum",rDD_SORCodeGenerator.DraftSORNum),
                        new SqlParameter("@SORApprovalCode",rDD_SORCodeGenerator.SORApprovalCode),
                        new SqlParameter("@Remarks",rDD_SORCodeGenerator.Remarks),
                        new SqlParameter("@SalesPerson",rDD_SORCodeGenerator.SalesPerson),
                        new SqlParameter("@RequestedBy",rDD_SORCodeGenerator.RequestedBy),
                        new SqlParameter("@RequestedByEmail",rDD_SORCodeGenerator.RequestedByEmail),
                        new SqlParameter("@CACM",rDD_SORCodeGenerator.CACM),
                        new SqlParameter("@CACMEmail",rDD_SORCodeGenerator.CACMEmail),
                        new SqlParameter("@LoggedInUser",rDD_SORCodeGenerator.CreatedBy),
                       // new SqlParameter("@Type",rDD_SORCodeGenerator.ActionType),  
                       new SqlParameter("@p_ide",rDD_SORCodeGenerator.Id),
                       new SqlParameter("@p_response",rDD_SORCodeGenerator.ErrorMsg)
                    };

                    List<Outcls1> outcls = new List<Outcls1>();
                    outcls = Com.ExecuteNonQueryListID("RDD_Insert_SORApprovalCode", parm);
                    rDD_SORCodeGenerator.Saveflag = outcls[0].Outtf;
                    rDD_SORCodeGenerator.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_SORCodeGenerator.ErrorMsg = ex.Message;
                rDD_SORCodeGenerator.Saveflag = false;
            }
            return rDD_SORCodeGenerator;
        }
    }
}
