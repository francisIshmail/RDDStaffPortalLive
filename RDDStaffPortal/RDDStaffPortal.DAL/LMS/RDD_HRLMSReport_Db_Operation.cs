using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels.LMS;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.LMS
{
    public class RDD_HRLMSReport_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
       
        public DataSet GetDropdownList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetCountryDeptEmp")                   
                };
                ds = Com.ExecuteDataSet("RDD_LMSHRReport", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet LeaveTypeByCountry(string Country)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetLeaveType"),
                    new SqlParameter("@CountryCode",Country)
                };
                ds = Com.ExecuteDataSet("RDD_LMSHRReport", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetAllData(string Empid,string Cid,string DeptId,string LeaveTypeId)
        {            
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetReport"),
                    new SqlParameter("@EmployeeId",Empid),
                    new SqlParameter("@CountryCode",Cid),
                    new SqlParameter("@DeptId",DeptId),
                    new SqlParameter("@LeaveTypeId",LeaveTypeId)
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_LMSHRReport", CommandType.StoredProcedure, Para);
                return dsModules;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
