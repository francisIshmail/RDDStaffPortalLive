using RDDStaffPortal.DAL.DataModels.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.SAP
{
   
    public class RDD_PDCEntryDbOperation
    {

        public DataSet GetDatabaseList()
        {
            CommonFunction Com = new CommonFunction();
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    
                };
                ds = Com.ExecuteDataSet("RDD_PDC_GetDatabaseLists", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet GetCustomerNameList(string DbName)
        {
            CommonFunction Com = new CommonFunction();
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@p_DBName",DbName)
                };
                ds = Com.ExecuteDataSet("RDD_PDC_GetCustomerLists", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet GetEmpName(string Name)
        {
            CommonFunction Com = new CommonFunction();
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetEmployeeName"),
                    new SqlParameter("@LoginName",Name)
                };
                ds = Com.ExecuteDataSet("RDD_GetDetailsForPDCEntry", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet GetBankNameList(string DbName)
        {
            CommonFunction Com = new CommonFunction();
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@p_DBName",DbName)
                };
                ds = Com.ExecuteDataSet("RDD_PDC_GetBankLists", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
    }
}
