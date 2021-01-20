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
    public class RDD_LeaveAdjustment_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        public RDD_LeaveAdjustment Save(RDD_LeaveAdjustment rDD_LeaveAdjustment)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (!rDD_LeaveAdjustment.Editflag)
                    {
                        rDD_LeaveAdjustment.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_LeaveAdjustment.ActionType = "Update";
                    }
                    SqlParameter[] parm = { new SqlParameter("@LeaveLedgerId",rDD_LeaveAdjustment.LeaveLedgerId),
                        new SqlParameter("@EmployeeId",rDD_LeaveAdjustment.EmployeeId),
                        new SqlParameter("@LeaveTypeId",rDD_LeaveAdjustment.LeaveTypeId),
                        new SqlParameter("@CreditDebit",rDD_LeaveAdjustment.CreditDebit),
                        new SqlParameter("@NoOfDays",rDD_LeaveAdjustment.NoOfDays),
                        new SqlParameter("@Remarks",rDD_LeaveAdjustment.Remarks),
                        new SqlParameter("@LoginId",rDD_LeaveAdjustment.CreatedBy),
                        new SqlParameter("@LoginOn",rDD_LeaveAdjustment.CreatedOn),
                        new SqlParameter("@Type",rDD_LeaveAdjustment.ActionType),
                        new SqlParameter("@p_ide",rDD_LeaveAdjustment.LeaveLedgerId),
                        new SqlParameter("@Response",rDD_LeaveAdjustment.ErrorMsg)

                };

                    List<Outcls1> outcls = new List<Outcls1>();
                    //rDD_LeaveAdjustment.Saveflag = Com.ExecuteNonQuery("RDD_LeaveLedgers", parm);
                    outcls = Com.ExecuteNonQueryListID("RDD_LeaveLedgers", parm);
                    rDD_LeaveAdjustment.Saveflag = outcls[0].Outtf;
                    rDD_LeaveAdjustment.LeaveTypeId = outcls[0].Id;
                    rDD_LeaveAdjustment.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_LeaveAdjustment.ErrorMsg = ex.Message;
                rDD_LeaveAdjustment.Saveflag = false;
                rDD_LeaveAdjustment.LeaveTypeId = -1;
            }
            return rDD_LeaveAdjustment;
        }
        public List<SelectListItem> GetEmployeeList()
        {
            List<SelectListItem> GetEmployeeListData = new List<SelectListItem>();
            GetEmployeeListData.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0"
            });
            try
            {
                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","Employee")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LeaveLedger_Get", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetEmployeeListData.Add(new SelectListItem()
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
            return GetEmployeeListData;
        }
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
                DataSet ds = Com.ExecuteDataSet("RDD_LeaveLedger_Get", CommandType.StoredProcedure, parm);

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
        public List<SelectListItem> GetDeptList()
        {            
            List<SelectListItem> GetDepartmentListData = new List<SelectListItem>();

            GetDepartmentListData.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0"
            });

            try
            {
                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","Department")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LeaveLedger_Get", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetDepartmentListData.Add(new SelectListItem()
                        {
                            Value = !string.IsNullOrWhiteSpace(dr["DeptId"].ToString()) ? dr["DeptId"].ToString() : "",
                            Text = !string.IsNullOrWhiteSpace(dr["DeptName"].ToString()) ? dr["DeptName"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetDepartmentListData;
        }
        public List<SelectListItem> GetLeaveTypeList()
        {
            List<SelectListItem> GetLeaveTypeListData = new List<SelectListItem>();

            //GetLeaveTypeListData.Add(new SelectListItem()
            //{
            //    Text = "--Select--",
            //    Value = "0"
            //});

            try
            {
                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","LeaveType")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LeaveLedger_Get", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetLeaveTypeListData.Add(new SelectListItem()
                        {
                            Value = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? dr["LeaveTypeId"].ToString() : "",
                            Text = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetLeaveTypeListData;
        }
        public List<RDD_LeaveAdjustment> GetAllData(int Empid)
        {
            List<RDD_LeaveAdjustment> rDD_LeaveAdjustment = new List<RDD_LeaveAdjustment>();

            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","Search"),
                    new SqlParameter("@EmployeeId",Empid)
                    
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_LeaveLedger_Get", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_LeaveAdjustment.Add(new RDD_LeaveAdjustment()
                        {
                            LeaveLedgerId = !string.IsNullOrWhiteSpace(dr["LeaveLedgerId"].ToString()) ? Convert.ToInt32(dr["LeaveLedgerId"].ToString()) : 0,
                            LeaveName = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "",
                            LeaveTypeId = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? Convert.ToInt32(dr["LeaveTypeId"].ToString()) : 0,
                            EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0,
                            FullName = !string.IsNullOrWhiteSpace(dr["FullName"].ToString()) ? dr["FullName"].ToString() : "",
                            NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? Convert.ToDecimal(dr["NoOfDays"].ToString()) : 0,
                            Remarks = !string.IsNullOrWhiteSpace(dr["Remarks"].ToString()) ? dr["Remarks"].ToString() : "",
                            //CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            //CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            //LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            //LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                rDD_LeaveAdjustment.Add(new RDD_LeaveAdjustment()
                {
                    LeaveName = "",
                    Remarks = "",
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return rDD_LeaveAdjustment;
        }
        public List<RDD_LeaveAdjustment> ShowAllData()
        {
            List<RDD_LeaveAdjustment> rDD_LeaveAdjustment = new List<RDD_LeaveAdjustment>();
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","Read"),
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_LeaveLedger_Get", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_LeaveAdjustment.Add(new RDD_LeaveAdjustment()
                        {
                            LeaveLedgerId = !string.IsNullOrWhiteSpace(dr["LeaveLedgerId"].ToString()) ? Convert.ToInt32(dr["LeaveLedgerId"].ToString()) : 0,
                            LeaveName = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "",
                            LeaveTypeId = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? Convert.ToInt32(dr["LeaveTypeId"].ToString()) : 0,
                            EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0,
                            FullName = !string.IsNullOrWhiteSpace(dr["FullName"].ToString()) ? dr["FullName"].ToString() : "",
                            NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? Convert.ToDecimal(dr["NoOfDays"].ToString()) : 0,
                            Remarks = !string.IsNullOrWhiteSpace(dr["Remarks"].ToString()) ? dr["Remarks"].ToString() : "",
                            //CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            //CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            //LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            //LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                rDD_LeaveAdjustment.Add(new RDD_LeaveAdjustment()
                {
                    LeaveName = "",
                    Remarks = null,
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return rDD_LeaveAdjustment;

        }
        public DataSet CountryLeaveType(int EmployeeId)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
               {
                    new SqlParameter("@loginuserId",EmployeeId),
                };
                ds = Com.ExecuteDataSet("RDD_GetLeaveTypeAsPerCountry", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
            
        }
    }
    
}
