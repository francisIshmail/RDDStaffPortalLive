using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Data.SqlClient;
using System.Data;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_Settings_DbOperation
    {
        CommonFunction Com = new CommonFunction();

        public DataSet GetSettingsDetails()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetData")
                };
                ds = Com.ExecuteDataSet("RDD_AppraisalSettings_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public RDD_Settings SaveAppraisalSettings(RDD_Settings rDD_Setting)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (rDD_Setting.EditFlag == false)
                    {
                        rDD_Setting.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_Setting.ActionType = "Update";
                    }
                    SqlParameter[] parm = {
                        new SqlParameter("@RatingId",rDD_Setting.RatingId),
                        new SqlParameter("@FrequencyId",rDD_Setting.FrequencyId),
                        new SqlParameter("@RatingNo",rDD_Setting.RatingNo),
                        new SqlParameter("@AppraisalFrequency",rDD_Setting.AppraisalFrequency),
                        new SqlParameter("@CreatedBy",rDD_Setting.CreatedBy),
                        new SqlParameter("@Type",rDD_Setting.ActionType),
                        new SqlParameter("@p_ide",rDD_Setting.id),
                        new SqlParameter("@Response",rDD_Setting.ErrorMsg)
                    };

                    List<Outcls1> outcls = new List<Outcls1>();
                    outcls = Com.ExecuteNonQueryListID("RDD_Insert_Update_AppraisalSettings", parm);
                    rDD_Setting.SaveFlag = outcls[0].Outtf;
                    rDD_Setting.ErrorMsg = outcls[0].Responsemsg;
                    if (outcls[0].Id == -1)
                    {
                        rDD_Setting.SaveFlag = false;
                    }
                    else
                    {
                        rDD_Setting.SaveFlag = true;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_Setting.ErrorMsg = ex.Message;
                rDD_Setting.SaveFlag = false;
            }
            return rDD_Setting;
        }
    }
}
