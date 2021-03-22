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
    }
}
