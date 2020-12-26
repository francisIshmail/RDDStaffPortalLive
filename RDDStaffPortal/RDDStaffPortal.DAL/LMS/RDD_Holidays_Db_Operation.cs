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
    public class RDD_Holidays_Db_Operation
    {

        CommonFunction Com = new CommonFunction();
        public RDD_Holidays Save(RDD_Holidays rDD_Holidays)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (rDD_Holidays.Editflag==false)
                    {
                        rDD_Holidays.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_Holidays.ActionType = "Update";
                    }
                    SqlParameter[] parm = { new SqlParameter("@HolidayID",rDD_Holidays.HolidayId),
                        new SqlParameter("@CountryCode",rDD_Holidays.CountryCode),
                        new SqlParameter("@HolidayName",rDD_Holidays.HolidayName),
                        new SqlParameter("@HolidayDate",rDD_Holidays.HolidayDate),
                        new SqlParameter("@LoginId",rDD_Holidays.CreatedBy),
                        new SqlParameter("@LoginOn",rDD_Holidays.CreatedOn),
                        new SqlParameter("@Type",rDD_Holidays.ActionType),
                        new SqlParameter("@p_ide",rDD_Holidays.id),
                        new SqlParameter("@Response",rDD_Holidays.ErrorMsg)
                };

                    List<Outcls1> outcls = new List<Outcls1>();
                     outcls = Com.ExecuteNonQueryListID("RDD_Holiday", parm);
                    rDD_Holidays.Saveflag = outcls[0].Outtf;
                    rDD_Holidays.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_Holidays.ErrorMsg = ex.Message;
                rDD_Holidays.Saveflag = false;
            }
            return rDD_Holidays;
        }
        public List<Outcls> DeleteFlag(string HolidayId)
        {
            //RDD_Holidays rDD_Holiday = new RDD_Holidays();
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                
                    string response = string.Empty;
                    SqlParameter[] parm ={ new SqlParameter("@HolidayID",HolidayId),
                         new SqlParameter("@Type","Delete"),
                         new SqlParameter("@p_ide",HolidayId),
                         new SqlParameter("@Response",response)
                    };
                    outcls = Com.ExecuteNonQueryList("RDD_Holiday", parm);
                    //scope.Complete();
                

            }
            catch (Exception ex)
            {

                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }
        public List<RDD_Holidays> GetList()
        {
            List<RDD_Holidays> GetListData = new List<RDD_Holidays>();
            try
            {
                SqlParameter[] parm ={
                         new SqlParameter("@Type","All")  };
                DataSet ds = Com.ExecuteDataSet("RDD_Holidays_GET", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_Holidays()
                        {

                            CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "",
                            HolidayName = !string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? dr["HolidayName"].ToString() : "",
                            HolidayDate = Convert.ToDateTime(dr["HolidayDate"].ToString()),
                            HolidayId = !string.IsNullOrWhiteSpace(dr["HolidayId"].ToString()) ? Convert.ToInt32(dr["HolidayId"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception)
            {
                GetListData.Add(new RDD_Holidays()
                {
                    CountryCode = "",
                    HolidayDate = null,
                    HolidayName = "",
                    HolidayId = 0
                });
            }

            return GetListData;

        }
        public RDD_Holidays GetData(string HolidayId)
        {
            RDD_Holidays rDD_Holiday = new RDD_Holidays();
            try
            {
                SqlParameter[] parm ={
                new SqlParameter("@HolidayId",HolidayId)  ,
                 new SqlParameter("@Type","Single")  };
                DataSet ds = Com.ExecuteDataSet("RDD_Holidays_GET", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_Holiday.CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "";
                        rDD_Holiday.HolidayName = !string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? dr["HolidayName"].ToString() : "";
                        rDD_Holiday.HolidayDate = Convert.ToDateTime(dr["HolidayDate"].ToString());
                        rDD_Holiday.Editflag = true;
                    }
                }
            }
            catch (Exception)
            {
                rDD_Holiday.CountryCode = "";
                rDD_Holiday.HolidayName = "";
                rDD_Holiday.HolidayDate = null;

            }
            return rDD_Holiday;
        }
        public List<SelectListItem> GetCountryList(string UserName)
        {
            List<SelectListItem> GetCountryListData = new List<SelectListItem>();
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@Username",UserName),
                         new SqlParameter("@Type","COUNTRY")  };
                DataSet ds = Com.ExecuteDataSet("RDD_Holidays_GET", CommandType.StoredProcedure, parm);

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
        public List<RDD_Holidays> GetALLDATA()
        {
            List<RDD_Holidays> rDD_Holidays = new List<RDD_Holidays>();

            try
            {
                SqlParameter[] Para = {
                   new SqlParameter("@Type","All")
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Holidays_GET", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_Holidays.Add(new RDD_Holidays()
                        {
                            CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "",
                            HolidayName = !string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? dr["HolidayName"].ToString() : "",
                            HolidayDate = Convert.ToDateTime(dr["HolidayDate"].ToString()),
                            HolidayId = !string.IsNullOrWhiteSpace(dr["HolidayId"].ToString()) ? Convert.ToInt32(dr["HolidayId"].ToString()) : 0,
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                            

                        });
                    }
                }
            }
            catch (Exception)
            {
                rDD_Holidays.Add(new RDD_Holidays()
                {
                    CountryCode = "",
                    HolidayName = "",
                    HolidayDate = null,
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return rDD_Holidays;
        }
        public DataSet GetHRRole(string UserName)
        {
            DataSet ds = null;
            SqlParameter[] parm = { new SqlParameter("@p_Username",UserName) };
            ds = Com.ExecuteDataSet("RDD_GetUserType", CommandType.StoredProcedure,parm);
            
        return ds;
        }
    }
    
}

        
    

