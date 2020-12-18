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
                    if (!rDD_Holidays.Editflag)
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
                        new SqlParameter("@IsDeleted",rDD_Holidays.IsDeleted),
                        new SqlParameter("@CreatedBy",rDD_Holidays.CreatedBy),
                        new SqlParameter("@CreatedOn",rDD_Holidays.CreatedOn),
                        new SqlParameter("@LastUpdatedBy",rDD_Holidays.LastUpdatedBy),
                        new SqlParameter("@LastUpdatedOn",rDD_Holidays.LastUpdatedOn),
                        new SqlParameter("@Type",rDD_Holidays.ActionType),
                        new SqlParameter("@Response",response)  
                };

                    List<Outcls> outcls = new List<Outcls>();
                    outcls = Com.ExecuteNonQueryList("RDD_Holiday", parm);
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
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    SqlParameter[] parm ={ new SqlParameter("@HolidayID",HolidayId),
                         new SqlParameter("@Type","Delete"), };
                    outcls = Com.ExecuteNonQueryList("RDD_Holiday", parm);
                    scope.Complete();
                }

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
                         new SqlParameter("@Type","Read")  };
                DataSet ds = Com.ExecuteDataSet("RDD_Holidays_GET", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_Holidays()
                        {

                            CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? Convert.ToInt32(dr["CountryCode"].ToString()) : 0,
                            HolidayName = !string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? dr["HolidayName"].ToString() : "",
                            HolidayDate = Convert.ToDateTime(dr["HolidayDate"].ToString()) ,
                            HolidayId = !string.IsNullOrWhiteSpace(dr["HolidayId"].ToString()) ? Convert.ToInt32(dr["HolidayId"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception)
            {
                GetListData.Add(new RDD_Holidays()
                {
                    CountryCode = 0,
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
                 new SqlParameter("@p_Action","Single")  };
                DataSet ds = Com.ExecuteDataSet("RDD_Holidays_GET", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_Holiday.CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? Convert.ToInt32(dr["CountryCode"].ToString()) : 0;
                        rDD_Holiday.HolidayName = !string.IsNullOrWhiteSpace(dr["HolidayName"].ToString()) ? dr["HolidayName"].ToString() : "";
                        rDD_Holiday.HolidayDate = Convert.ToDateTime(dr["HolidayDate"].ToString()) ;
                        rDD_Holiday.Editflag = true;
                    }
                }
            }
            catch (Exception)
            {
                rDD_Holiday.CountryCode = 0;
                rDD_Holiday.HolidayName = "";
                rDD_Holiday.HolidayDate = null;
                    
            }
            return rDD_Holiday;
        }
    }
}
