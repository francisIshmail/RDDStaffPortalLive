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
    }
}
