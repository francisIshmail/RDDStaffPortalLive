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
    public class RDD_LeaveType_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        public RDD_LeaveType Save(RDD_LeaveType rDD_LeaveType)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (!rDD_LeaveType.Editflag)
                    {
                        rDD_LeaveType.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_LeaveType.ActionType = "Update";
                    }
                    SqlParameter[] parm = { new SqlParameter("@LeaveTypeId",rDD_LeaveType.LeaveTypeId),
                        new SqlParameter("@LeaveCode",rDD_LeaveType.LeaveCode),
                        new SqlParameter("@LeaveName",rDD_LeaveType.LeaveName),
                        new SqlParameter("@AccruedRule",rDD_LeaveType.AccruedRule),
                        new SqlParameter("@AccruedDays",rDD_LeaveType.AccruedDays),
                        new SqlParameter("@MinimumLeaveBalanceCondition",rDD_LeaveType.MinimumLeaveBalanceCondition),
                        new SqlParameter("@IsLeaveLapseAtEndOfYear",rDD_LeaveType.IsLeaveLapseAtEndOfYear),
                        new SqlParameter("@MaximumLeavecarryForwardToNextYear",rDD_LeaveType.MaximumLeavecarryForwardToNextYear),
                        new SqlParameter("@AllowMaximumNegativeLeaveDays",rDD_LeaveType.AllowMaximumNegativeLeaveDays),
                        new SqlParameter("@CountryCode",rDD_LeaveType.Country),
                        new SqlParameter("@IsDeleted",rDD_LeaveType.IsDeleted),
                        new SqlParameter("@LoginId",rDD_LeaveType.CreatedBy),
                        new SqlParameter("@LoginOn",rDD_LeaveType.CreatedOn),
                        new SqlParameter("@Type",rDD_LeaveType.ActionType),
                        new SqlParameter("@p_ide",rDD_LeaveType.LeaveTypeId),
                        new SqlParameter("@Response",rDD_LeaveType.ErrorMsg)
                        
                };

                    List<Outcls1> outcls = new List<Outcls1>();
                    //rDD_LeaveType.Saveflag = Com.ExecuteNonQuery("RDD_LEAVETYPE", parm);
                    outcls = Com.ExecuteNonQueryListID("RDD_LEAVETYPE", parm);
                    rDD_LeaveType.Saveflag = outcls[0].Outtf;
                    rDD_LeaveType.LeaveTypeId = outcls[0].Id;
                    rDD_LeaveType.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_LeaveType.ErrorMsg = ex.Message;
                rDD_LeaveType.Saveflag = false;
                rDD_LeaveType.LeaveTypeId = -1;
            }
            return rDD_LeaveType;
        }
        public List<Outcls> DeleteFlag(int LeaveTypeId)
        {
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                //using (TransactionScope scope = new TransactionScope())

                string response = string.Empty;
                SqlParameter[] parm ={ new SqlParameter("@LeaveTypeId",LeaveTypeId),
                         new SqlParameter("@Type","Delete"),
                         new SqlParameter("@p_ide",LeaveTypeId),
                         new SqlParameter("@Response",response)
                    };
                outcls = Com.ExecuteNonQueryList("RDD_LEAVETYPE", parm);
                //scope.Complete();


            }
            catch (Exception ex)
            {

                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }
        public List<SelectListItem> GetCountryList()
        {
            List<SelectListItem> GetCountryListData = new List<SelectListItem>();
            GetCountryListData.Add(new SelectListItem()            {                Text = "--Select--",                Value = "0"            });
            try
            {

                SqlParameter[] parm ={
                        //new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","COUNTRY")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LEAVETYPE_GET", CommandType.StoredProcedure, parm);

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
        public List<RDD_LeaveType> GetALLDATA()
        {
            List<RDD_LeaveType> rDD_LeaveType = new List<RDD_LeaveType>();

            try
            {
                SqlParameter[] Para = {
                   new SqlParameter("@Type","All")
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_LEAVETYPE_GET", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_LeaveType.Add(new RDD_LeaveType()
                        {
                            LeaveTypeId = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? Convert.ToInt32(dr["LeaveTypeId"].ToString()) : 0,
                            Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            LeaveCode = !string.IsNullOrWhiteSpace(dr["LeaveCode"].ToString()) ? dr["LeaveCode"].ToString() : "",
                            LeaveName = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "",
                            AccruedRule = !string.IsNullOrWhiteSpace(dr["AccruedRule"].ToString()) ? dr["AccruedRule"].ToString() : "",
                            AccruedDays = !string.IsNullOrWhiteSpace(dr["AccruedDays"].ToString()) ? Convert.ToDecimal(dr["AccruedDays"].ToString()) : 0,
                            
                            IsLeaveLapseAtEndOfYear =  Convert.ToBoolean(dr["IsLeaveLapseAtEndOfYear"].ToString()) ,
                            MaximumLeavecarryForwardToNextYear = !string.IsNullOrWhiteSpace(dr["MaximumLeavecarryForwardToNextYear"].ToString()) ? dr["MaximumLeavecarryForwardToNextYear"].ToString() : "",
                            AllowMaximumNegativeLeaveDays = !string.IsNullOrWhiteSpace(dr["AllowMaximumNegativeLeaveDays"].ToString()) ? Convert.ToDecimal(dr["AllowMaximumNegativeLeaveDays"].ToString()) : 0,
                            CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "",
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
                rDD_LeaveType.Add(new RDD_LeaveType()
                {
                    Country = "",
                    CountryCode = "",
                    LeaveCode = null,
                    LeaveName = "",
                    AccruedRule  = "",
                    AccruedDays = 0,
                   
                    IsLeaveLapseAtEndOfYear = true,
                    MaximumLeavecarryForwardToNextYear = "",
                    AllowMaximumNegativeLeaveDays = 0,
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return rDD_LeaveType;
        }
        public List<RDD_LeaveType> GetList()
        {
            List<RDD_LeaveType> GetListData = new List<RDD_LeaveType>();
            try
            {
                SqlParameter[] parm ={
                         new SqlParameter("@Type","Update")  };
                DataSet ds = Com.ExecuteDataSet("RDD_LEAVETYPE", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_LeaveType()
                        {

                            LeaveName = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "",
                            AccruedRule = !string.IsNullOrWhiteSpace(dr["AccruedRule"].ToString()) ? dr["AccruedRule"].ToString() : "",
                            AccruedDays = !string.IsNullOrWhiteSpace(dr["AccruedDays"].ToString()) ? Convert.ToDecimal(dr["AccruedDays"].ToString()) : 0,
                            IsLeaveLapseAtEndOfYear = Convert.ToBoolean(dr["IsLeaveLapseAtEndOfYear"].ToString()),
                            MaximumLeavecarryForwardToNextYear = !string.IsNullOrWhiteSpace(dr["MaximumLeavecarryForwardToNextYear"].ToString()) ? dr["MaximumLeavecarryForwardToNextYear"].ToString() : "",
                            AllowMaximumNegativeLeaveDays = !string.IsNullOrWhiteSpace(dr["AllowMaximumNegativeLeaveDays"].ToString()) ? Convert.ToDecimal(dr["AllowMaximumNegativeLeaveDays"].ToString()) : 0,
                        });
                    }
                }
            }
            catch(Exception)
            {
                GetListData.Add(new RDD_LeaveType()
                {
                    LeaveName = "",
                    AccruedRule = null,
                    AccruedDays = 0,
                    IsLeaveLapseAtEndOfYear = true,
                    MaximumLeavecarryForwardToNextYear = "",
                    AllowMaximumNegativeLeaveDays = 0

                });
            }
            return GetListData;
        }
    }
}
