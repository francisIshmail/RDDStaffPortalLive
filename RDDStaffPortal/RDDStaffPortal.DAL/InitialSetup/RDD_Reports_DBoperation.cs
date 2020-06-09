using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.InitialSetup
{
   
    public  class RDD_Reports_DBOperation
    {
        CommonFunction Com = new CommonFunction();


        public  List<RDD_Reports> GetReportList(string UserName)
        {
            List<RDD_Reports> _Rdd = new List<RDD_Reports>();

            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", UserName) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_GetReportsToDownload", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _Rdd.Add(new RDD_Reports()
                        {
                            fileName1 = !string.IsNullOrWhiteSpace(dr["fileName"].ToString()) ? dr["fileName"].ToString():"",
                           reportcategory = !string.IsNullOrWhiteSpace(dr["reportcategory"].ToString()) ? dr["reportcategory"].ToString():"",
                            reportFilePath = !string.IsNullOrWhiteSpace(dr["reportFilePath"].ToString()) ? dr["reportFilePath"].ToString():"",
                            reportTitle = !string.IsNullOrWhiteSpace(dr["reportTitle"].ToString()) ? dr["reportTitle"].ToString():"",
                            reportType   = !string.IsNullOrWhiteSpace(dr["reportType"].ToString()) ? dr["reportType"].ToString():"",
                           fileurl= DateTime.Now.ToString("MM-dd-yyyy")
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _Rdd = null;
            }
            return _Rdd;

        }
    }
}
