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
    public class RDD_LMSReport_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        public List<SelectListItem> GetCountryList()
        {

            List<SelectListItem> GetCountryListData = new List<SelectListItem>();
            GetCountryListData.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0"
            });

            try
            {
                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","COUNTRY")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LMSReport", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetCountryListData.Add(new SelectListItem()
                        {
                            Text = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return GetCountryListData;
        }
        public List<SelectListItem> GetDepartmentByCountry(string country)
        {
            List<SelectListItem> GetDepartmentByCountryData = new List<SelectListItem>();
            GetDepartmentByCountryData.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0"
            });

            try
            {
                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","Department")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LMSReport", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetDepartmentByCountryData.Add(new SelectListItem()
                        {
                            Text = !string.IsNullOrWhiteSpace(dr["DeptName"].ToString()) ? dr["DeptName"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["DeptId"].ToString()) ? dr["DeptId"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return GetDepartmentByCountryData;
        }
        public List<SelectListItem> GetEmployeeByDept(int DeptId)
        {
            List<SelectListItem> GetEmployeeByDeptData = new List<SelectListItem>();
            GetEmployeeByDeptData.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0"
            });

            try
            {
                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","Employees" +
                         "")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LMSReport", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetEmployeeByDeptData.Add(new SelectListItem()
                        {
                            Text = !string.IsNullOrWhiteSpace(dr["FullName"].ToString()) ? dr["FullName"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? dr["EmployeeId"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return GetEmployeeByDeptData;
        }
        public DataSet DepartmentByCountry(string Country)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","Department"),
                    new SqlParameter("@Country",Country)
                };
                ds = Com.ExecuteDataSet("RDD_LMSReport", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet EmployeeByDept(int DeptId, string cid)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","Employees"),
                    new SqlParameter("@Dept",DeptId),
                    new SqlParameter("@Country",cid)
                };
                ds = Com.ExecuteDataSet("RDD_LMSReport", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public List<RDD_LMSReport> GetAllData(int Empid)
        {
            List<RDD_LMSReport> rDD_LMSReport = new List<RDD_LMSReport>();

            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","Search"),
                    new SqlParameter("@Employee",Empid)

                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_LMSReport", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_LMSReport.Add(new RDD_LMSReport()
                        {
                            //EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0,
                            LeaveName = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "",
                            FromDate = Convert.ToDateTime(dr["FromDate"].ToString()),
                            ToDate = Convert.ToDateTime(dr["ToDate"].ToString()),
                            NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? Convert.ToDecimal(dr["NoOfDays"].ToString()) : 0,
                            Remarks = !string.IsNullOrWhiteSpace(dr["Remarks"].ToString()) ? dr["Remarks"].ToString() : "",


                        });
                    }
                }
            }
            catch (Exception ex)
            {
                rDD_LMSReport.Add(new RDD_LMSReport()
                {
                    EmployeeId = 0,
                    FromDate = null,
                    ToDate = null,
                    LeaveName = "",
                    NoOfDays = 0,
                    Remarks = "",
                });
            }
            return rDD_LMSReport;
        }
    }
}
