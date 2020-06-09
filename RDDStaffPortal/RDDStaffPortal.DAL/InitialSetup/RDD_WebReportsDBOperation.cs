using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public  class RDD_WebReportsDBOperation
    {

        CommonFunction Com = new CommonFunction();

        public List<RDD_WebReportsList> GetRDD_WebReportList( int pagesize, int pageno, string psearch, string username = null)
        {
            List<RDD_WebReportsList> Objlist = new List<RDD_WebReportsList>();
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_Search", psearch)
                        , new SqlParameter("@p_PageNo", pageno),
                new SqlParameter("@p_PageSize",pagesize),
                new SqlParameter("@p_SortColumn", "reportTitle"),
                new SqlParameter("@p_SortOrder", "ASC"),
                new SqlParameter("@p_username",username)
                };

                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("Retrive_RDD_webReportTypes", CommandType.StoredProcedure, parm);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_WebReportsList()
                        {
                            reportTitle = dr["reportTitle"].ToString(),
                            repTypeId = Convert.ToInt32(dr["repTypeId"].ToString()),
                            TotalCount = Convert.ToInt32(dr["TotalCount"].ToString()),
                            bgcolor = dr["bgcolor"].ToString(),
                            IsAlreadyMapped = Convert.ToBoolean(dr["IsAlreadyMapped"].ToString()),                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Objlist = null;
            }



            return Objlist;
        }

        public List<RDD_WebReportsList> GetRDD_WebReportUserList(string username)
        {
            List<RDD_WebReportsList> Objlist = new List<RDD_WebReportsList>();
            try
            {
                SqlParameter[] parm = {
                new SqlParameter("@p_username",username)
                };
                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_WebReports_User", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_WebReportsList()
                        {
                            reportTitle = dr["reportTitle"].ToString(),
                            repTypeId = Convert.ToInt32(dr["fk_repTypeId"].ToString()),
                           
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Objlist = null;
            }
            return Objlist;
        }


        public RDD_WebReportsUser Save(RDD_WebReportsUser URep)
        {
            RDD_WebReportsUser _WebRep = new RDD_WebReportsUser();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    int i = URep.WebRepLists.Count;

                    while (i > 0)
                    {
                        SqlParameter[] sqlpar1 = { new SqlParameter("@p_userName", URep.userName),
                            new SqlParameter("@p_fk_repTypeId", URep.WebRepLists[i-1].fk_repTypeId),


                        };
                        _WebRep.saveflag = Com.ExecuteNonQuery("RDD_WebReportsUser_Insert", sqlpar1);
                       
                        i--;
                       
                    }
                    scope.Complete();
                }
                _WebRep.errormsg = "Save Succesfully";
               
            }
            catch (Exception)
            {

                _WebRep.errormsg = "Error";
                _WebRep.saveflag = false;
            }
           
            return _WebRep;
        }

        public bool DeleteActivity(string Username,int Code)
        {

            bool t = false;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_code",Code),
               
                new SqlParameter("@p_username",Username)
                   };
                t = Com.ExecuteNonQuery("Delete_RDD_WebReportsUser", parm);
            }
            catch (Exception)
            {

                t = false;
            }


            return t;
        }

    }

}
