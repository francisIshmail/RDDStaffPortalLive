using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.Admin
{
   public class RDD_Welcome_MessageDBOperation
    {
        CommonFunction Com = new CommonFunction();
        public List<Outcls1> save(RDD_Welcome_Message rDD_Welcome)
        {
            List<Outcls1> str = new List<Outcls1>();
            byte[] file;

           
                using (var stream = new FileStream(rDD_Welcome.Welcome_image1, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }
              
           
            

            rDD_Welcome.ActionType = "Insert";
            if (rDD_Welcome.EditFlag == true)
            {
                rDD_Welcome.ActionType = "Update";
            }
            try
            {
                SqlParameter[] Para = {
                                    new SqlParameter("@p_WelcomeId", rDD_Welcome.Welcome_id),
                                    new SqlParameter("@p_Welcome_Message", rDD_Welcome.Welcome_Message),
                                     new SqlParameter("@p_Welcome_title", rDD_Welcome.Welcome_title),
                                     new SqlParameter("@p_Welcome_Image", file),
                                     new SqlParameter("@p_type", rDD_Welcome.ActionType),
                                     new SqlParameter("@p_Loginon",rDD_Welcome.Loginon),
                                    new SqlParameter("@p_Loginid", rDD_Welcome.Loginid),
                                    new SqlParameter("@p_IsActive",rDD_Welcome.IsActive)
                                    ,new SqlParameter("@p_imgbool",rDD_Welcome.imgbool),
                                     new SqlParameter("@p_id", rDD_Welcome.Welcome_id),
                                     new SqlParameter("@p_response", rDD_Welcome.Errormsg),




            };

                str = Com.ExecuteNonQueryListID("RDD_Welcome_Message_Insert_update_Delete", Para);
            }
            catch (Exception ex)
            {

                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }

            

            return str;
        }

        public List<Outcls1> DeleteActivity( int Welcome_id)
        {
            List<Outcls1> outcls = new List<Outcls1>();
            int Wel_id = 0;
            try
            {
                
                string response = string.Empty;
                SqlParameter[] p = {
                      new SqlParameter("@p_WelcomeId", Welcome_id),
                        new SqlParameter("@p_type","DeleteAct"),
                       new SqlParameter("@p_id", Wel_id),
                                     new SqlParameter("@p_response", response),
                                   
                    };
                


                outcls = Com.ExecuteNonQueryListID("RDD_Welcome_Message_Insert_update_Delete", p);
            }
            catch (Exception ex)
            {

                outcls.Add(new Outcls1

                {
                    Outtf = false,
                    Id = Wel_id,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return outcls;
        }

       public   RDD_Welcome_Message_User InsertUserrActivity(RDD_Welcome_Message_User rdd)
        {
            RDD_Welcome_Message_User rdd_user = new RDD_Welcome_Message_User();

            try
            {
                SqlParameter[] para = {

                     new SqlParameter("@p_UserName",rdd.UserName),
                      new SqlParameter("@p_Welcome_Id",rdd.Welcome_Id),
                       new SqlParameter("@p_Welcome_Read",rdd.Read),
                     new SqlParameter("@p_Welcome_Reminder",rdd.Reminder)
                    };
                Com.ExecuteNonQuery("RDD_Welcome_Message_UserBase_Insert", para);
                rdd.Saveflag = true;
            }
            catch (Exception)
            {

                rdd.Saveflag = false;
            }
            
            
            return rdd_user;

        }
        public RDD_Welcome_Message GetData(int Welcome_id)
        {
            RDD_Welcome_Message rDD_Welcome = new RDD_Welcome_Message();
            try
            {
                SqlParameter[] p = { new SqlParameter("@p_type", "Single"), new SqlParameter("@p_welcome_id",Welcome_id) };
                DataSet dsModules = Com.ExecuteDataSet("Rdd_Welcome_Message_Get", CommandType.StoredProcedure, p);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_Welcome.IsActive=!string.IsNullOrWhiteSpace(dr["IsActive"].ToString()) ? Convert.ToBoolean(dr["IsActive"].ToString()) :true;
                        rDD_Welcome.Welcome_id = !string.IsNullOrWhiteSpace(dr["Welcome_id"].ToString()) ? Convert.ToInt32(dr["Welcome_id"].ToString()) : 0;
                        rDD_Welcome.Welcome_image = (byte[])dr["Welcome_Image"];
                        rDD_Welcome.Welcome_Message = !string.IsNullOrWhiteSpace(dr["Welcome_Message"].ToString()) ? dr["Welcome_Message"].ToString() : "";
                        rDD_Welcome.EditFlag = true;
                        rDD_Welcome.Welcome_title = !string.IsNullOrWhiteSpace(dr["Welcome_title"].ToString()) ? dr["Welcome_title"].ToString() : "";
                    }
                }
            }
            catch (Exception)
            {

                rDD_Welcome = null;
            }
           
            return rDD_Welcome;
        }

        public List<RDD_Welcome_Message> GetDataNotify(string UserName)
        {
            List<RDD_Welcome_Message> rDD_Welcome_s = new List<RDD_Welcome_Message>();
            try
            {
                SqlParameter[] p = {
                       
                     new SqlParameter("@p_type", "notify"),
                     new SqlParameter("@p_username",UserName)
                    };
                DataSet dsModules = Com.ExecuteDataSet("Rdd_Welcome_Message_Get", CommandType.StoredProcedure, p);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                       
                        
                        rDD_Welcome_s.Add(new RDD_Welcome_Message
                        {
                            Welcome_title = !string.IsNullOrWhiteSpace(dr["Welcome_title"].ToString()) ? dr["Welcome_title"].ToString() : "",
                            Welcome_image = (byte[])dr["Welcome_Image"],
                            Welcome_image1 = Convert.ToBase64String((byte[])dr["Welcome_Image"]),
                        Welcome_Message = !string.IsNullOrWhiteSpace(dr["Welcome_Message"].ToString()) ? dr["Welcome_Message"].ToString() : "",
                            Welcome_id = !string.IsNullOrWhiteSpace(dr["Welcome_id"].ToString()) ? Convert.ToInt32(dr["Welcome_id"].ToString()) : 0,
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                rDD_Welcome_s = null;
            }

            return rDD_Welcome_s;
        }

        public List<RDD_Welcome_Message> GetDataALL(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_Welcome_Message> rDD_Welcome_s = new List<RDD_Welcome_Message>();
            try
            {
                SqlParameter[] p = {
                       new SqlParameter("@p_search",psearch),
                     new SqlParameter("@p_pagesize",pagesize),
                     new SqlParameter("@p_pageno",pageno),
                     new SqlParameter("@p_SortColumn","Welcomeid"),
                     new SqlParameter("@p_SortOrder","ASC"),
                     new SqlParameter("@p_type", "Get"),
                    };
                DataSet dsModules = Com.ExecuteDataSet("Rdd_Welcome_Message_Get", CommandType.StoredProcedure, p);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_Welcome_s.Add(new RDD_Welcome_Message
                        {
                            Welcome_title= !string.IsNullOrWhiteSpace(dr["Welcome_title"].ToString()) ? dr["Welcome_title"].ToString() : "",
                            Welcome_Message = !string.IsNullOrWhiteSpace(dr["Welcome_Message"].ToString()) ? dr["Welcome_Message"].ToString() : "",
                            Welcome_id = !string.IsNullOrWhiteSpace(dr["Welcome_id"].ToString()) ? Convert.ToInt32(dr["Welcome_id"].ToString()) :0,
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                rDD_Welcome_s = null;
            }
            
                return rDD_Welcome_s;
        }
    }
}
