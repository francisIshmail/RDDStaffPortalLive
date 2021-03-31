using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DataSet GetQuestionList(int CategoryId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetQuestion"),
                    new SqlParameter("CategoryId",CategoryId)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmployeeDetails_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
    }
}
