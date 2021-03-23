using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL.DataModels;
using System.Transactions;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_AddAppraisalQuestion_DbOperation
    {
        CommonFunction Com = new CommonFunction();
        public DataSet GetPeriodCategoryList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetCategoryAndPreviousPeriod")
                };
                ds = Com.ExecuteDataSet("RDD_SetAppraisalQuestion_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public RDD_AddAppraisalQuestion SaveAssignCategoryDetails(RDD_AddAppraisalQuestion rDD_Question)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (rDD_Question.EditFlag == false)
                    {
                        rDD_Question.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_Question.ActionType = "Update";
                    }
                    int m = 0;
                    while (rDD_Question.rDD_QuestionList.Count > m)
                    {
                        SqlParameter[] parm = {
                            new SqlParameter("@Period",rDD_Question.Period),
                            new SqlParameter("@CategoryId",rDD_Question.CategoryId),
                            new SqlParameter("@Question",rDD_Question.rDD_QuestionList[m].Question),
                            new SqlParameter("@CreatedBy",rDD_Question.CreatedBy),
                            new SqlParameter("@LastUpdatedBy",rDD_Question.LastUpdatedBy),
                            new SqlParameter("@Type",rDD_Question.ActionType),
                            //new SqlParameter("@p_ide",rDD_Question.id),
                            //new SqlParameter("@Response",rDD_Question.ErrorMsg)
                        };
                        
                        var det1 = Com.ExecuteNonQuery("RDD_Insert_Update_AppraisalQuestions", parm);
                        if (det1 == false)
                        {
                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Outtf = false,
                                Id = -1,
                                Responsemsg = "Error occured : KPI Parameter Details "
                            });
                            return str;
                        }
                        m++;
                    }
                    

                    
                    rDD_Question.SaveFlag = outcls[0].Outtf;
                    rDD_Question.ErrorMsg = outcls[0].Responsemsg;
                    if (outcls[0].Id == -1)
                    {
                        rDD_Question.SaveFlag = false;
                    }
                    else
                    {
                        rDD_Question.SaveFlag = true;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_Question.ErrorMsg = ex.Message;
                rDD_Question.SaveFlag = false;
            }
            return rDD_Question;
        }
    }
}
