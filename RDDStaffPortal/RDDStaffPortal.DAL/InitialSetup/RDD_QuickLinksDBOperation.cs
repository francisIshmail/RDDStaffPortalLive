using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Markup;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public class RDD_QuickLinksDBOperation
    {
        CommonFunction Com = new CommonFunction();


        public RDD_QuickLinks Save(RDD_QuickLinks Rdd_Q)
        {
            RDD_QuickLinks _Rdd_Q = new RDD_QuickLinks();
            List<Outcls> str1 = new List<Outcls>();
            try
            {
                string response = "";
                using (TransactionScope scope = new TransactionScope()){

                    SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",Rdd_Q.UserName),
                    new SqlParameter("@p_URL",Rdd_Q.URL),
                    new SqlParameter("@p_FormName",Rdd_Q.FormName),
                     new SqlParameter("@p_response",response),};
                    str1 = Com.ExecuteNonQueryList("RDD_QuickLinks_Insert", Para);
                    _Rdd_Q.Saveflag = str1[0].Outtf;
                    
                    _Rdd_Q.QuickLinkId =Convert.ToInt32(str1[0].Responsemsg);

                    _Rdd_Q.ErrorMsg = "Save Successfull";
                    scope.Complete();
                }

            }
            catch (Exception)
            {

                _Rdd_Q.Saveflag = false;
                _Rdd_Q.ErrorMsg = "Error Occur";
            }

            return _Rdd_Q;

        }


        public bool DeleteActivity(int QuickLinkId)
        {
            bool t = false;
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@QuickLinkId",QuickLinkId)
                };
                t = Com.ExecuteNonQuery("RDD_QuickLinks_Delete", Para);

            }
            catch (Exception)
            {

                t = false;
            }
            return t;
        }



        public List<RDD_RightSide> GetRightsideTask(string UserName)
        {
            List<RDD_RightSide> _rdd = new List<RDD_RightSide>();
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_username", UserName) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Notification_QuickLinks", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _rdd.Add(new RDD_RightSide()
                        {

                            NavUrl = !string.IsNullOrWhiteSpace(dr["NavUrl"].ToString()) ? dr["NavUrl"].ToString() : "",
                            typs = !string.IsNullOrWhiteSpace(dr["typs"].ToString()) ? dr["typs"].ToString() : "",
                            MenuName = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                        });
                    }

                }
            }
            catch (Exception)
            {

                _rdd = null;
            }

            return _rdd;
        }
        public List<RDD_RightSide> GetRightside(string UserName)
        {
            List<RDD_RightSide> _rdd = new List<RDD_RightSide>();
            try
            {
                SqlParameter[] parm = {new SqlParameter("@p_username",UserName) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_QuickLinks_Get", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _rdd.Add(new RDD_RightSide()
                        {
                          
                            NavUrl = !string.IsNullOrWhiteSpace(dr["NavUrl"].ToString()) ? dr["NavUrl"].ToString() : "",
                            typs = !string.IsNullOrWhiteSpace(dr["typs"].ToString()) ? dr["typs"].ToString() : "",
                            MenuName = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                        });
                    }

                }
            }
            catch (Exception)
            {

                _rdd = null;
            }

            return _rdd;
        }
    }
}
