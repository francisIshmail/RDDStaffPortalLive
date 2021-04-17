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

        public DataSet GetEmployeeRating(int EmpId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeRating"),
                    new SqlParameter("EmployeeId",EmpId)                    
                };
                ds = Com.ExecuteDataSet("RDD_GetManagerDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
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
                rDD_EmpAppraisal.ActionType = "ManagerInsert";
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
                        if (rDD_EmpAppraisal.ActionTypeTrans == "Update")
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
    }
}
