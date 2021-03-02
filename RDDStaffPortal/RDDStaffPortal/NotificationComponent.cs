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
       
        string conStr = ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
        // Register notification (will add sql dependency)
        public void RegisterNotification(DateTime currentTime,string UserName)        
        {
            // Fetch connexion string from web.config file
            // string conStr = ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            // Create sql command needed for sql dependency
            
            
            
            string sp_string = "select DOC_ID,Notification_Type from [dbo].[Rdd_Notification_Log] ";//-- where [Rdd_Notification_Log].Notification_Createdon >= @p_current_time and  [Rdd_Notification_Log].Notification_AssignTo = @p_username
            //"SELECT [ID],[Name] from [dbo].[tblEmployee]";
            // sp_string = "[dbo].[RDD_DashBoard_Notification]";
            OnChangeEventHandler handler = (sender, e) =>
            {
                string theSymbol = UserName;
                // Handle the event (for example, invalidate this cache entry).
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                if (e.Info   == SqlNotificationInfo.Insert)
                {
                    SqlDependency sqlDep = sender as SqlDependency;
                   //sqlDep.OnChange -= handler;

                    // from here we will send notification message to clients
                    // var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    notificationHub.Clients.All.notify("added");

                    // re-register notification
                    RegisterNotification(DateTime.Now,UserName);

                }
                else if (e.Info == SqlNotificationInfo.Update)
                {
                    SqlDependency sqlDep = sender as SqlDependency;
                  // sqlDep.OnChange -= handler;

                    // from here we will send notification message to clients
                    //  var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    notificationHub.Clients.All.notify("added");
                   
                    // re-register notification
                    RegisterNotification(DateTime.Now, UserName);
                }

            };
            using (SqlConnection con = new SqlConnection(conStr))
            {
                //SqlCommand cmd;

                //    using (cmd = new SqlCommand(sp_string, con))
                //    {

                SqlCommand cmd = new SqlCommand(sp_string, con);
                cmd.Parameters.AddWithValue("@p_username", UserName);
                cmd.Parameters.AddWithValue("@p_current_time", currentTime);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
               
                    
                    cmd.Notification = null;
               
                // Create an sql dependency object with the sql command we just have created for revieved notification from the sql server
                SqlDependency sqlDep = new SqlDependency(cmd);
                // To recieve notifications we will need to subscribe the change OnChange so that when the sql command produces a different result the OnChange event will be fired

                    sqlDep.OnChange += handler;
                
               
               
                //  sqlDep.OnChange += new OnChangeEventHandler(dependency_OnChange);

                // Execute sql command here
                
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // nothing need to add here now
                    }
                // }
                cmd.Dispose();


            }
        }

        //private void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Info == SqlNotificationInfo.Insert)
        //    {
        //        SqlDependency sqlDep = sender as SqlDependency;
        //        sqlDep.OnChange -= sqlDep_OnChange;
               
        //        // from here we will send notification message to clients
        //        var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //        notificationHub.Clients.All.notify("added");
        //        string UserName = "";
        //        // re-register notification
        //        RegisterNotification(DateTime.Now);

        //    }else if(e.Info == SqlNotificationInfo.Update)
        //    {
        //        SqlDependency sqlDep = sender as SqlDependency;
        //        sqlDep.OnChange -= sqlDep_OnChange;

        //        // from here we will send notification message to clients
        //        var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //        notificationHub.Clients.All.notify("Approver");
        //        string UserName = "";
        //        // re-register notification
        //        RegisterNotification(DateTime.Now);
        //    }
        //}
        
        public DataSet GetContacts(DateTime afterDate,string UserName,string Flag)
        {
            DataSet ds = null;
            try
            {
                SqlConnection SqlConn;
                SqlDataAdapter da;
                SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@p_current_time",afterDate),
                                                new SqlParameter("@p_username",UserName),
                 new SqlParameter("@p_flag",Flag),};
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

        public DataSet Get_All_Notification_Page(DateTime afterDate, string UserName, string Flag,int PageNo,int PageSize,string  p_search)
        {
            DataSet ds = null;
            try
            {
                SqlConnection SqlConn;
                SqlDataAdapter da;
                SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@p_current_time",afterDate),
                                                new SqlParameter("@p_username",UserName),
                                                new SqlParameter("@p_search",p_search),
                                                new SqlParameter("@p_pageno",PageNo),
                                                new SqlParameter("@p_pagesize",PageSize),
                 new SqlParameter("@p_flag",Flag),};
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
        public bool Notification_Status_change(int Doc_id, string UserName,string decision,string Notification_type)
        {
            bool tf = false;
            try
            {
                SqlConnection SqlConn;
                SqlCommand sqlcmd= new SqlCommand();
                string SqlCommondText;
                
                SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@p_doc_id",Doc_id),
                                                new SqlParameter("@p_username",UserName),
                 new SqlParameter("@p_Notification_type",Notification_type),
                new SqlParameter("@p_decsion",decision)};
                using (SqlConn = new SqlConnection(conStr))
                {

                    SqlConn.Open();
                    SqlCommondText = "RDD_Dashborad_Notification_Change";
                    using (sqlcmd = new SqlCommand(SqlCommondText, SqlConn))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddRange(ParaDet1);

                      int i=  sqlcmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            tf = true;
                        }
                        else
                        {
                            tf = false;
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {

                tf = false;
            }
            return tf;
        }
    }
}