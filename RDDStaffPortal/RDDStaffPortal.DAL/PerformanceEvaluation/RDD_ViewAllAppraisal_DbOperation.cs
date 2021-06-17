using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.CommonFunction;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_ViewAllAppraisal_DbOperation
    {
        CommonFunction Com = new CommonFunction();

        public DataSet GetDetailsForHR()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeDetailsForHR")                    
                };
                ds = Com.ExecuteDataSet("RDD_GetEmpMngDetailsForHR_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetEmployeeDetailsOnPeriod(string Qperiod)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeDetailsOnPeriod"),
                    new SqlParameter("@Periods",Qperiod)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmpMngDetailsForHR_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetEmployeeManagerRating(int EmpId, string Qperiod)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeManagerRating"),
                    new SqlParameter("@Periods",Qperiod),
                    new SqlParameter("@EmployeeId",EmpId)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmpMngDetailsForHR_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetEmployeeManagerRatingCategoryWise(int EmpId, string Qperiod,int CategoryId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeManagerRatingCategoryWise"),
                    new SqlParameter("@Periods",Qperiod),
                    new SqlParameter("@EmployeeId",EmpId),
                    new SqlParameter("@CategoryId",CategoryId)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmpMngDetailsForHR_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetEmployeeManagerRatingPeriodWise(int EmpId, string Qperiod)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("Type","GetEmployeeManagerRatingPeriodWise"),
                    new SqlParameter("@Periods",Qperiod),
                    new SqlParameter("@EmployeeId",EmpId)
                };
                ds = Com.ExecuteDataSet("RDD_GetEmpMngDetailsForHR_EmpMngAppraisalRating", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
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
