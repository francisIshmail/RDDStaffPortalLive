using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public  class DashBoardDbOperation
    {
        CommonFunction Com = new CommonFunction();
        public string save1(RDD_DashBoard Dash)
        {
            List<Outcls> str = new List<Outcls>();
            string response = string.Empty;
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_DashboardId",Dash.DashboardId),
                    new SqlParameter("@p_DashboardName",Dash.DashboardName),
                    new SqlParameter("@p_ModuleId",Dash.ModuleId),
                    new SqlParameter("@p_cssClass",Dash.cssClass),
                    new SqlParameter("@p_URL",Dash.URL),
                    new SqlParameter("@p_DisplaySeq",Dash.DisplaySeq),
                    new SqlParameter("@p_IsDefault",Dash.IsDefault),
                    new SqlParameter("@p_CreatedBy",Dash.CreatedBy),
                    new SqlParameter("@p_Levels",Dash.Levels),
                    new SqlParameter("@p_response",response),
                };
                str = Com.ExecuteNonQueryList("RDD_Dashboard_InsertUpdate", Para);
                response = str[0].Responsemsg;
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;
        }
        public string DeleteDashBoard(int menuid)
        {
            List<Outcls> str = new List<Outcls>();
            string response = string.Empty;
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_DashboardId",menuid),
                    new SqlParameter("@p_response",response),
                };
                str = Com.ExecuteNonQueryList("RDD_Dashboard_Delete", Para);
                response = str[0].Responsemsg;
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;
        }
    }
}
