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

        public DataSet GetPreviousPeriodQuestion(string PrevPeriod)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetPreviosPeriodQuestion"),
                    new SqlParameter("@Periods",PrevPeriod)
                };
                ds = Com.ExecuteDataSet("RDD_SetAppraisalQuestion_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetQuestionDetails()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetQuestionDetails")                    
                };
                ds = Com.ExecuteDataSet("RDD_SetAppraisalQuestion_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetCategorywiseQuestionList(int CategoryId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetCategorywiseQuestion"),
                    new SqlParameter("CategoryId",CategoryId)
                };
                ds = Com.ExecuteDataSet("RDD_SetAppraisalQuestion_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public List<Outcls1> SaveAssignCategoryDetails(RDD_AddAppraisalQuestion rDD_Question)
        {
            List<Outcls1> str = new List<Outcls1>();
            if (rDD_Question.EditFlag == false)
            {
                rDD_Question.ActionType = "Insert";
            }
            else
            {
                rDD_Question.ActionType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {                    
                    SqlParameter[] parm = {
                            new SqlParameter("@AppraisalQuestionId",rDD_Question.QuestionId),
                            new SqlParameter("@Period",rDD_Question.Period),
                            new SqlParameter("@CategoryId",rDD_Question.CategoryId),
                            new SqlParameter("@CreatedBy",rDD_Question.CreatedBy),
                            new SqlParameter("@LastUpdatedBy",rDD_Question.LastUpdatedBy),
                            new SqlParameter("@Type",rDD_Question.ActionType),
                            new SqlParameter("@p_ide",rDD_Question.id),
                            new SqlParameter("@Response",rDD_Question.ErrorMsg)
                        };

                    str = Com.ExecuteNonQueryListID("RDD_Insert_Update_AppraisalQuestions", parm);
                    if (str[0].Outtf == true)
                    {
                        int m = 0;
                        if (rDD_Question.EditFlag == true)
                        {
                            rDD_Question.ActionTypeTrans = "";
                        }
                        else
                        {
                            rDD_Question.ActionTypeTrans = "InsertTrans";
                        }
                        while (rDD_Question.rDD_QuestionList.Count > m)
                        {
                            SqlParameter[] ParaDet1 =
                            {
                                    new SqlParameter("@Type",rDD_Question.ActionTypeTrans),
                                    new SqlParameter("@QuestionId",str[0].Id),
                                    new SqlParameter("@Question",rDD_Question.rDD_QuestionList[m].Question)
                                };
                            var det1 = Com.ExecuteNonQuery("RDD_Insert_Update_AppraisalQuestionsTrans", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : Appraisal Question"
                                });
                            }
                            m++;
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return str;
        }

        public List<Outcls1> UpdateAssignCategoryDetails(string Qid, string Question)
        {
            List<Outcls1> str = new List<Outcls1>();
            RDD_AddAppraisalQuestion rDD_Question = new RDD_AddAppraisalQuestion();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] parm = {
                            new SqlParameter("@Question",Question),
                            new SqlParameter("@QuestionTransId",Qid),                            
                            new SqlParameter("@Type","Update"),
                            new SqlParameter("@p_ide",rDD_Question.id),
                            new SqlParameter("@Response",rDD_Question.ErrorMsg)
                        };

                    str = Com.ExecuteNonQueryListID("RDD_Insert_Update_AppraisalQuestions", parm);                    
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return str;
        }

        public List<Outcls> DeleteAppraisalQuestion(int Qid)
        {            
            List<Outcls> outcls = new List<Outcls>();
            try
            {                
                string response = string.Empty;
                SqlParameter[] parm ={ new SqlParameter("@QuestionTransId",Qid),
                         new SqlParameter("@Type","Delete"),
                         new SqlParameter("@p_ide",Qid),
                         new SqlParameter("@Response",response)
                    };
                outcls = Com.ExecuteNonQueryList("RDD_Insert_Update_AppraisalQuestions", parm);
            }
            catch (Exception ex)
            {
                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }

        public List<Outcls1> LaunchAppraisal()
        {
            List<Outcls1> str = new List<Outcls1>();
            RDD_AddAppraisalQuestion rDD_Question = new RDD_AddAppraisalQuestion();
            try
            {
                using(TransactionScope scope=new TransactionScope())
                {
                    SqlParameter[] prm =
                    {
                        new SqlParameter("Type","LaunchAppraisal"),                        
                        new SqlParameter("p_ide",rDD_Question.id),
                        new SqlParameter("Response",rDD_Question.ErrorMsg)
                    };
                    str = Com.ExecuteNonQueryListID("RDD_Insert_Update_AppraisalQuestions", prm);
                    scope.Complete();
                }                
            }
            catch (Exception ex)
            {
                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return str;
        }

        public DataSet GetMailDetails()
        {
            DataSet ds;
            try
            {
                SqlParameter[] prm =
                {
                    new SqlParameter("Type","GetMailDetails")                    
                };
                ds = Com.ExecuteDataSet("RDD_SetAppraisalQuestion_GetData", CommandType.StoredProcedure, prm);
            }
            catch (Exception ex)
            {
                throw;             
            }
            return ds;
        }
    }
}
