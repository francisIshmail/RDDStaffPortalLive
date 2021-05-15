using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.Report
{
   public class VendorReportDBOperation
    {
        CommonFunction Com = new CommonFunction();
        public bool RDD_VendorReport_Refresh()
        {
            bool t = false;
            try
            {
                SqlParameter[] parm = { };

               t= Com.ExecuteNonQuery("RDD_VendorReport_RefreshWeeklyGP", parm);
            }
            catch (Exception)
            {

                return t;
            }
            

            return t;
        }
        public DataSet GetCannonReport(string BU,DateTime FromDate,DateTime Todate, string Country_code)
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_FromDate",FromDate),
                    new SqlParameter("@p_ToDate",Todate),
                    new SqlParameter("@p_BU",BU),
                    new SqlParameter("@p_country_code",Country_code)
                };
                
                dsModules = Com.ExecuteDataSet("RDD_VendorReport_GetData", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {

                dsModules = null;
            }

            return dsModules;
        }
        //Vendor_Report_Dropdown_Fill 

        public DataSet DropDownFill()
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = { 
                };

                dsModules = Com.ExecuteDataSet("Vendor_Report_Dropdown_Fill", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {

                dsModules = null;
            }

            return dsModules;
        }

    }
}
