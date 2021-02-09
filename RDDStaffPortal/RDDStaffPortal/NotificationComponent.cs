using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal
{
    [AllowAnonymous]
    public class NotificationComponent
    {
        private string _UserName = "";
        string conStr = ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
        // Register notification (will add sql dependency)
        public void RegisterNotification(DateTime currentTime)
        
        {
            // Fetch connexion string from web.config file
            // string conStr = ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            // Create sql command needed for sql dependency
            string sqlCommand = "select DOC_ID,APPROVER from [dbo].[RDD_APPROVAL_DOC] where CreatedOn >  @p_current_time ";//"SELECT [ID],[Name] from [dbo].[tblEmployee] where [AddedOn] >  @p_current_time ";
            //"RDD_DashBoard_Notification";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd;
                using (cmd = new SqlCommand(sqlCommand, con))


                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                   // cmd.Parameters.AddWithValue("@p_username", "alfarid");
                    cmd.Parameters.AddWithValue("@p_current_time", currentTime);
                    if (con.State != System.Data.ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.Notification = null;
                    // Create an sql dependency object with the sql command we just have created for revieved notification from the sql server
                    SqlDependency sqlDep = new SqlDependency(cmd);
                    // To recieve notifications we will need to subscribe the change OnChange so that when the sql command produces a different result the OnChange event will be fired
                    // sqlDep.OnChange += sqlDep_OnChange;
                    sqlDep.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    // Execute sql command here
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // nothing need to add here now
                    }
                }
                    
            }
        }

        //private void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        SqlDependency sqlDep = sender as SqlDependency;
        //        sqlDep.OnChange -= sqlDep_OnChange;

        //        // from here we will send notification message to clients
        //        var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //        notificationHub.Clients.All.notify("added");
        //        string UserName="";
        //        // re-register notification
        //        RegisterNotification(DateTime.Now);

        //    }
        //}
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
           // var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            if (e.Info == SqlNotificationInfo.Insert ||
                  e.Info == SqlNotificationInfo.Delete ||
                  e.Info == SqlNotificationInfo.Update)
            {
                NotificationHub.SendMessages();
            }
        }
        public DataSet GetContacts(DateTime afterDate,string UserName)
        {
            DataSet ds = null;
            try
            {
                SqlConnection SqlConn;
                SqlDataAdapter da;
                SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@p_current_time",afterDate),
                                                new SqlParameter("@p_username",UserName),};
                using (SqlConn = new SqlConnection(conStr))
                {
                    
                        SqlConn.Open();
                        da = new SqlDataAdapter("RDD_DashBoard_Notification", SqlConn);
                        da.SelectCommand.CommandTimeout = 0;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddRange(ParaDet1);
                        ds = new DataSet();
                        da.Fill(ds);
                    }

            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }

    }
}