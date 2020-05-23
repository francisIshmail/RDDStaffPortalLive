using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL.InitialSetup;

namespace RDDStaffPortal.DAL
{
    class Common
    {

        CommonFunction Com = new CommonFunction();

        /// <summary>
        ///  This function is used to get the log of table by key
        /// </summary>
        /// <param name="PrimaryKey"> PrimaryKey Column Name </param>
        /// <param name="TableName">Table Name</param>
        /// <returns></returns>
        ///
        public List<RDD_Log> GetChangeLog(int PrimaryKey, string TableName)
        {
            DataTable dtLog = new DataTable();
            List<RDD_Log> objLog = new List<RDD_Log>();

            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_PrimaryKey", PrimaryKey) ,
                                    new SqlParameter("@p_TableName", TableName) };

                DataSet dsLog = Com.ExecuteDataSet("RDD_GetChangeLog", CommandType.StoredProcedure, parm);
                if (dsLog.Tables.Count > 0)
                {
                    dtLog = dsLog.Tables[0];
                    DataRowCollection drc = dtLog.Rows;
                    foreach (DataRow dr in drc)
                    {
                        objLog.Add(new RDD_Log()
                        {
                            ColName = dr["ColName"].ToString(),
                            ColDescription = dr["ColDescription"].ToString(),
                            OldValue = dr["OldValue"].ToString(),
                            NewValue = dr["NewValue"].ToString(),
                            ChangedBy = dr["ChangedBy"].ToString(),
                            ChangedOn = Convert.ToDateTime(dr["ChangedOn"])
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                objLog = null;
            }

            return objLog;

        }

    }
}
