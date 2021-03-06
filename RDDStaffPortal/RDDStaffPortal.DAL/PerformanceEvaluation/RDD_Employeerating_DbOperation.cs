using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_Employeerating_DbOperation
    {
        CommonFunction Com = new CommonFunction();
        public DataSet GetEmployeeDetails(string LoginName)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeDetails"),
                    new SqlParameter("LoginName",LoginName)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetCategoryList(string LoginName)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetCategory"),
                    new SqlParameter("LoginName",LoginName)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetQuestionList(int CategoryId,string Loginname, string Qperiod)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetQuestion"),
                    new SqlParameter("CategoryId",CategoryId),
                    new SqlParameter("LoginName",Loginname),
                    new SqlParameter("Periods",Qperiod)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetCategoryWiseDetailsOnClickUrl(int CategoryId, string UrlId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetCategoryWiseDetailsOnClickUrl"),
                    new SqlParameter("CategoryId",CategoryId),
                    new SqlParameter("UrlIds",UrlId)                    
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeManagerDetailsOnClickURL_PerformanceAppraisal", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public List<Outcls1> SaveEmployeeRating(RDD_EmployeeRating rDD_EmpAppraisal)
        {
            List<Outcls1> str = new List<Outcls1>();
            if (rDD_EmpAppraisal.EditFlag == false)
            {
                rDD_EmpAppraisal.ActionType = "Insert";
            }
            else
            {
                rDD_EmpAppraisal.ActionType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] parm = {
                            new SqlParameter("@AppraisalId",rDD_EmpAppraisal.AppraisalId),
                            new SqlParameter("@Period",rDD_EmpAppraisal.Period),
                            new SqlParameter("@CategoryId",rDD_EmpAppraisal.CategoryId),
                            new SqlParameter("@EmployeeId",rDD_EmpAppraisal.EmployeeId),
                            new SqlParameter("@Year",rDD_EmpAppraisal.Year),
                            new SqlParameter("@Emp_SubmittedBy",rDD_EmpAppraisal.Emp_SubmittedBy),                            
                            new SqlParameter("@Type",rDD_EmpAppraisal.ActionType),
                            new SqlParameter("@p_ide",rDD_EmpAppraisal.id),
                            new SqlParameter("@Response",rDD_EmpAppraisal.ErrorMsg)
                        };

                    str = Com.ExecuteNonQueryListID("RDD_Insert_Update_PerformanceAppraisal", parm);
                    if (str[0].Outtf == true)
                    {                        
                        int m = 0;
                        if (rDD_EmpAppraisal.EditFlag == true)
                        {
                            rDD_EmpAppraisal.ActionTypeTrans = "Update";
                        }
                        else
                        {
                            rDD_EmpAppraisal.ActionTypeTrans = "Insert";
                        }
                        if(rDD_EmpAppraisal.ActionTypeTrans== "Update")
                        {
                            SqlParameter[] prms =
                            {
                                new SqlParameter("@Type","Delete"),
                                new SqlParameter("@AppraisalId",str[0].Id)
                            };
                            var det = Com.ExecuteNonQuery("RDD_Insert_Update_PerformanceAppraisalTrans", prms);
                        }
                        
                        
                        while (rDD_EmpAppraisal.rDD_EmpAppraisalList.Count > m)
                        {
                            SqlParameter[] ParaDet1 =
                            {
                                    new SqlParameter("@Type",rDD_EmpAppraisal.ActionTypeTrans),
                                    new SqlParameter("@AppraisalId",str[0].Id),
                                    new SqlParameter("@EmployeeRating",rDD_EmpAppraisal.rDD_EmpAppraisalList[m].EmployeeRating),
                                    new SqlParameter("@EmployeeComment",rDD_EmpAppraisal.rDD_EmpAppraisalList[m].EmployeeComment),
                                    new SqlParameter("@QuestionId",rDD_EmpAppraisal.rDD_EmpAppraisalList[m].QuestionId)
                                };
                            var det1 = Com.ExecuteNonQuery("RDD_Insert_Update_PerformanceAppraisalTrans", ParaDet1);
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

        public List<Outcls1> FinalSaveEmployeeRating(int EmployeeId, int Year, string Period,string Loginname)
        {
            List<Outcls1> str = new List<Outcls1>();
            RDD_AddAppraisalQuestion rDD_Question = new RDD_AddAppraisalQuestion();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] parm = {
                            new SqlParameter("@EmployeeId",EmployeeId),
                            new SqlParameter("@Period",Period),
                            new SqlParameter("@Year",Year),
                            new SqlParameter("@Emp_SubmittedBy",Loginname),
                            new SqlParameter("@Type","FinalSubmit"),
                            new SqlParameter("@p_ide",rDD_Question.id),
                            new SqlParameter("@Response",rDD_Question.ErrorMsg)
                        };

                    str = Com.ExecuteNonQueryListID("RDD_Insert_Update_PerformanceAppraisal", parm);
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
        public DataSet GetMailDetails(int EmployeeId,string Period,int Year)
        {
            DataSet ds;
            try
            {                
                SqlParameter[] prm =
                {
                    new SqlParameter("Type","GetMailDetails"),
                    new SqlParameter("EmployeeId",EmployeeId),
                    new SqlParameter("Period",Period),
                    new SqlParameter("Year",Year)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, prm);
            }
            catch(Exception ex)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetDetailsByYearAndPeriod(string Qperiod,string Loginname)
        {
            DataSet ds = null;
            try
            {                
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetDetailsByYearAndPeriod"),
                    new SqlParameter("Periods",Qperiod),
                    new SqlParameter("LoginName",Loginname)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetDetailsByClickUrl(string UrlId,string Loginname)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeManagerRatingOnClickUrl"),
                    new SqlParameter("UrlIds",UrlId),
                    new SqlParameter("LoginName",Loginname)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeManagerDetailsOnClickURL_PerformanceAppraisal", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public List<Outcls1> SavePdf(RDD_EmployeeRating rDD_AppraisalPdfUpload)
        {
            List<Outcls1> str = new List<Outcls1>();
            rDD_AppraisalPdfUpload.ActionType = "SavePDF";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] parm = {                            
                            new SqlParameter("@Period",rDD_AppraisalPdfUpload.Period),                            
                            new SqlParameter("@EmployeeId",rDD_AppraisalPdfUpload.EmployeeId),
                            new SqlParameter("@Year",rDD_AppraisalPdfUpload.Year),
                            new SqlParameter("@Emp_SubmittedBy",rDD_AppraisalPdfUpload.Emp_SubmittedBy),
                            new SqlParameter("@AttachmentUrl",rDD_AppraisalPdfUpload.AttachmentUrl),
                            new SqlParameter("@Type",rDD_AppraisalPdfUpload.ActionType),
                            new SqlParameter("@p_ide",rDD_AppraisalPdfUpload.id),
                            new SqlParameter("@Response",rDD_AppraisalPdfUpload.ErrorMsg)
                        };

                    str = Com.ExecuteNonQueryListID("RDD_Insert_Update_PerformanceAppraisal", parm);                    
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

        public DataSet GetMailDetailsForPDF(RDD_EmployeeRating rDD_AppraisalPdfUpload)
        {
            DataSet ds;
            try
            {
                SqlParameter[] prm =
                {
                    new SqlParameter("Type","GetMailForPDF"),
                    new SqlParameter("Period",rDD_AppraisalPdfUpload.Period),
                    new SqlParameter("Year",rDD_AppraisalPdfUpload.Year),
                    new SqlParameter("EmployeeId",rDD_AppraisalPdfUpload.EmployeeId)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, prm);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

        public DataSet DownloadPDF(string Periods,string LoginName)
        {
            DataSet ds;
            try
            {
                SqlParameter[] prm =
                {
                    new SqlParameter("Type","DownloadPDF"),
                    new SqlParameter("Periods",Periods),
                    new SqlParameter("LoginName",LoginName)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, prm);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

        public DataSet GeneratePDF(string UrlId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmpMngRatingPDFformat"),
                    new SqlParameter("UrlIds",UrlId)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeManagerDetailsOnClickURL_PerformanceAppraisal", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
    }
}
