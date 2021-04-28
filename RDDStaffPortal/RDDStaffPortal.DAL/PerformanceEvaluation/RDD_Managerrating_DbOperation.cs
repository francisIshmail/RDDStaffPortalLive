using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_Managerrating_DbOperation
    {
        CommonFunction Com = new CommonFunction();

        public DataSet GetDetailsForManager(string LoginName)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetDetailsForManager"),
                    new SqlParameter("LoginName",LoginName)
                };
                ds = Com.ExecuteDataSet("RDD_GetManagerDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetDetailsForManagerByChangePeriods(string LoginName, string Qperiod)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetDetailsForManagerByPeriod"),
                    new SqlParameter("LoginName",LoginName),
                    new SqlParameter("Periods",Qperiod)
                };
                ds = Com.ExecuteDataSet("RDD_GetManagerDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetEmployeeRating(int EmpId,string Qperiods)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeRating"),
                    new SqlParameter("EmployeeId",EmpId),
                    new SqlParameter("Periods",Qperiods)
                };
                ds = Com.ExecuteDataSet("RDD_GetManagerDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetQuestionList(int CategoryId, string Qperiod, int EmpId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetQuestionByCategory"),
                    new SqlParameter("CategoryId",CategoryId),
                    new SqlParameter("EmployeeId",EmpId),
                    new SqlParameter("Periods",Qperiod)
                };
                ds = Com.ExecuteDataSet("RDD_GetManagerDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public List<Outcls1> SaveManagerRating(RDD_EmployeeRating rDD_MngAppraisal)
        {
            List<Outcls1> str = new List<Outcls1>();
            rDD_MngAppraisal.ActionType = "ManagerInsert";              
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    int m = 0;                                    
                    while (rDD_MngAppraisal.rDD_EmpAppraisalList.Count > m)
                    {
                        SqlParameter[] ParaDet1 =
                        {
                            new SqlParameter("@Type",rDD_MngAppraisal.ActionType),
                            new SqlParameter("@AppraisalTransId",rDD_MngAppraisal.rDD_EmpAppraisalList[m].AppraisalTransId),
                            new SqlParameter("@ManagerRating",rDD_MngAppraisal.rDD_EmpAppraisalList[m].ManagerRating),
                            new SqlParameter("@ManagerComment",rDD_MngAppraisal.rDD_EmpAppraisalList[m].ManagerComment),
                            new SqlParameter("@Editflag",rDD_MngAppraisal.EditFlag),
                            new SqlParameter("@p_ide",rDD_MngAppraisal.id),
                            new SqlParameter("@Response",rDD_MngAppraisal.ErrorMsg)
                        };
                        str = Com.ExecuteNonQueryListID("RDD_Insert_Update_PerformanceAppraisal", ParaDet1);
                        if (str[0].Outtf == false)
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

        public List<Outcls1> FinalSaveManagerRating(int EmployeeId, int Year, string Period)
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
                            new SqlParameter("@Type","ManagerFinalSubmit"),
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

        public DataSet GetMailDetails(int EmployeeId, string Period, int Year)
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
                ds = Com.ExecuteDataSet("RDD_GetManagerDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, prm);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetManagerRatingViewOnClickUrl(string UrlId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeRatingOnClickUrl"),
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
