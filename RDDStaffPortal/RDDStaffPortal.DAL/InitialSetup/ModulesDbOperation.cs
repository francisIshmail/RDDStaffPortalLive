using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels;
using System.Configuration;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.InitialSetup
{
    public class ModulesDbOperation
    {
        CommonFunction Com = new CommonFunction();
        /// <summary>
        ///  This method is to save the RDD_Modules, It accepts RDD_Module class as argument and returns the message
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
        /// 

        public string GetProfilimg(string UserName)
        {
            byte[] file1 = null;
            string base64String = null;
            SqlParameter[] parm = { new SqlParameter("@p_LoginName",UserName),
                    };
            DataSet dsModules = Com.ExecuteDataSet("RDD_GetEmployeeIdByimage", CommandType.StoredProcedure, parm);
            if (dsModules.Tables.Count > 0)
            {
                DataTable dtModule = dsModules.Tables[0];
                DataRowCollection drc = dtModule.Rows;
                if (dtModule.Rows.Count > 0)
                {
                    foreach (DataRow dr in drc)
                    {
                        if (dr["ImagePath"] != null && dr["ImagePath"].ToString().Length > 0)

                        {
                            file1 = (byte[])dr["ImagePath"];
                        }
                    }

                }
                base64String = Convert.ToBase64String(file1);
            }
            return base64String;
        }
        public string Save( RDD_Modules modules )
        {
            string response = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlTransaction transaction;
                    using (transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_Modules_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_ModuleId", SqlDbType.Int).Value = modules.ModuleId;
                            cmd.Parameters.Add("@p_ModuleName", SqlDbType.NVarChar,120).Value = modules.ModuleName;

                            if (modules.cssClass == null)
                                cmd.Parameters.Add("@p_cssClass", SqlDbType.NVarChar, 150).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("@p_cssClass", SqlDbType.NVarChar,150).Value = modules.cssClass;

                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value =modules.IsActive;

                            if (modules.CreatedBy==null)
                                cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar,50).Value = modules.CreatedBy;

                            cmd.Parameters.Add("@p_response", SqlDbType.NVarChar,1000).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_response"].Value.ToString();
                            cmd.Dispose();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            response = "Error occured : " + ex.Message;
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;
        }

       public  string save1(RDD_Modules modules)
        {
            List<Outcls> str = new List<Outcls>();
            string response = string.Empty;
            try
            {
                SqlParameter[] Para = { 
                new SqlParameter("@p_ModuleId",modules.ModuleId),
                new SqlParameter("@p_ModuleName",modules.ModuleName),
                new SqlParameter("@p_cssClass",modules.cssClass),
                new SqlParameter("@p_IsActive",modules.IsActive),
                new SqlParameter("@p_CreatedBy",modules.CreatedBy),
                new SqlParameter("@p_response",response),
                };
                str= Com.ExecuteNonQueryList("RDD_Modules_InsertUpdate", Para);              
               response = str[0].Responsemsg;                               
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;            
        }
        public RDD_Modules GetModuleById(int ModuleId)
        {
            RDD_Modules _Module = new RDD_Modules();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsModules = Db.myGetDS(" Exec RDD_Modules_GetData " + ModuleId);
                if(dsModules.Tables.Count>0)
                {
                    DataTable dtModule = dsModules.Tables[0];

                    if (dtModule.Rows[0]["ModuleId"] != null && !DBNull.Value.Equals(dtModule.Rows[0]["ModuleId"]))
                    {
                        _Module.ModuleId = Convert.ToInt32(dtModule.Rows[0]["ModuleId"]);
                    }
                    if (dtModule.Rows[0]["ModuleName"] != null && !DBNull.Value.Equals(dtModule.Rows[0]["ModuleName"]))
                    {
                        _Module.ModuleName = dtModule.Rows[0]["ModuleName"].ToString();
                    }
                    if (dtModule.Rows[0]["cssClass"] != null && !DBNull.Value.Equals(dtModule.Rows[0]["cssClass"]))
                    {
                        _Module.cssClass = dtModule.Rows[0]["cssClass"].ToString();
                    }
                    if (dtModule.Rows[0]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[0]["IsActive"]))
                    {
                        _Module.IsActive = Convert.ToBoolean(dtModule.Rows[0]["IsActive"]);
                    }
                    if (dtModule.Rows[0]["CreatedBy"] != null && !DBNull.Value.Equals(dtModule.Rows[0]["CreatedBy"]))
                    {
                        _Module.CreatedBy = dtModule.Rows[0]["CreatedBy"].ToString();
                    }
                    if (dtModule.Rows[0]["CreatedOn"] != null && !DBNull.Value.Equals(dtModule.Rows[0]["CreatedOn"]))
                    {
                        _Module.CreatedOn = Convert.ToDateTime(dtModule.Rows[0]["CreatedOn"]);
                    }
                }
            }
            catch (Exception ex)
            {
                _Module = null;
            }
            return _Module;

        }

        public List<RDD_Modules> GetModulesList3(int levels) {
            List<RDD_Modules> _ModuleList = new List<RDD_Modules>();
            try
            {
                DataSet dsModules = Com.ExecuteDataSet("Select * from vw_rdd_modules  where Levels='"+levels+"'");
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _ModuleList.Add(new RDD_Modules()
                        {
                            ModuleId = !string.IsNullOrWhiteSpace(dr["MenuId"].ToString()) ? Convert.ToInt32(dr["MenuId"].ToString()) : 0,
                            ModuleName = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                           
                        });
                    }

                }

            }
            catch (Exception)
            {

                _ModuleList = null;
            }
            
            return _ModuleList;
        }

        public List<RDD_Modules> GetModuleList1()
        {
            List<RDD_Modules> _ModuleList = new List<RDD_Modules>();
            try
            {
                SqlParameter[] parm =  { };                
                DataSet dsModules = Com.ExecuteDataSet("RDD_Modules_GetData",CommandType.StoredProcedure,parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach(DataRow dr in drc)
                    {
                        _ModuleList.Add(new RDD_Modules()
                        {
                            ModuleId =!string.IsNullOrWhiteSpace(dr["ModuleId"].ToString())?Convert.ToInt32(dr["ModuleId"].ToString()):0,
                            ModuleName = !string.IsNullOrWhiteSpace(dr["ModuleName"].ToString()) ? dr["ModuleName"].ToString() : "",
                            cssClass = !string.IsNullOrWhiteSpace(dr["cssClass"].ToString()) ? dr["cssClass"].ToString() : "",
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            CreatedOn =!string.IsNullOrWhiteSpace(dr["Createdon"].ToString())? Convert.ToDateTime(dr["Createdon"].ToString()): Convert.ToDateTime(System.DateTime.Now),
                            IsActive =!string.IsNullOrWhiteSpace(dr["IsActive"].ToString())? Convert.ToBoolean(dr["IsActive"].ToString()):false
                        });                           
                    }
                   
                }
            }
            catch (Exception ex)
            {
                _ModuleList = null;
            }
            return _ModuleList;

        }

        public List<RDD_firstDashBoard> GetFirstDashBoards(string UserId)
        {
            List<RDD_firstDashBoard> _FirstDash = new List<RDD_firstDashBoard>();
            try
            {
                SqlParameter[] parm = { };


                //DataSet dsModules = Com.ExecuteDataSet("select * from RDD_VW_Menu order by MOduleid");
                DataSet dsModules = Com.ExecuteDataSet("select * from RDD_First_DashBoard  where Userid='" + UserId+"'order by Displayseq");
                //SqlParameter[] sqlpar =  {new SqlParameter("@UserId",Username) ,
                //new SqlParameter("@Role",Role) };
                //DataSet dsModules = Com.ExecuteDataSet("RDD_RetriveMenu", CommandType.StoredProcedure, sqlpar);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;

                           var     colcss = "col-12 col-sm-6 col-md-3";
                    if (dtModule.Rows.Count == 4)
                    {
                        colcss = "col-12 col-sm-6 col-md-3";
                    }
                    else if (dtModule.Rows.Count == 3)
                    {
                        colcss = "col-12 col-sm-6 col-md-4";
                    }
                    else if (dtModule.Rows.Count == 2)
                    {
                        colcss = "col-12 col-sm-6 col-md-6";
                    }
                    else if (dtModule.Rows.Count == 1)
                    {
                        colcss = "col-12 col-sm-6 col-md-12";
                    }
                        
                    foreach (DataRow dr in drc)
                    {
                        _FirstDash.Add(new RDD_firstDashBoard()
                        {
                            colcss= colcss,
                            FirstText = !string.IsNullOrWhiteSpace(dr["FirstText"].ToString()) ? dr["FirstText"].ToString() : "",                                                      
                            SecondText = !string.IsNullOrWhiteSpace(dr["SecondText"].ToString()) ? dr["SecondText"].ToString() : "",
                            cssclass = !string.IsNullOrWhiteSpace(dr["cssclass"].ToString()) ? dr["cssclass"].ToString() : "",
                            DisplaySeq= !string.IsNullOrWhiteSpace(dr["DisplaySeq"].ToString()) ?Convert.ToInt32(dr["DisplaySeq"].ToString()) : 0,
                            FirstValue =!string.IsNullOrWhiteSpace(dr["FirstValue"].ToString()) ? Convert.ToDecimal(dr["FirstValue"].ToString()) : 0,
                            SecondValue = !string.IsNullOrWhiteSpace(dr["SecondValue"].ToString()) ? Convert.ToDecimal(dr["SecondValue"].ToString()) : 0,
                            perValue= !string.IsNullOrWhiteSpace(dr["perValue"].ToString()) ? Convert.ToInt32(dr["perValue"].ToString()) : 0,

                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _FirstDash = null;
            }
            return _FirstDash;
        }

        public List<RDD_Menus> GetModuleList2(string Username,string Role)
        {
            List<RDD_Menus> _ModuleList = new List<RDD_Menus>();
            try
            {
                SqlParameter[] parm = { };


                //DataSet dsModules = Com.ExecuteDataSet("select * from RDD_VW_Menu order by MOduleid");
                //DataSet dsModules = Com.ExecuteDataSet("select * from RDD_Menus where IsDeleted=0 order by Levels,DisplaySeq");
                SqlParameter[] sqlpar =  {new SqlParameter("@UserId",Username) ,
                new SqlParameter("@Role",Role) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_RetriveMenu", CommandType.StoredProcedure, sqlpar);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _ModuleList.Add(new RDD_Menus()
                        {
                            ModuleId = !string.IsNullOrWhiteSpace(dr["ModuleId"].ToString()) ? Convert.ToInt32(dr["ModuleId"].ToString()) : 0,                          
                            MenuId = !string.IsNullOrWhiteSpace(dr["MenuId"].ToString()) ? Convert.ToInt32(dr["MenuId"].ToString()) : 0,
                            MenuName = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                            URL = !string.IsNullOrWhiteSpace(dr["URL"].ToString()) ? dr["URL"].ToString() : "",
                            MenuCssClass= !string.IsNullOrWhiteSpace(dr["cssclass"].ToString()) ? dr["cssclass"].ToString() : "",
                            Levels =!string.IsNullOrWhiteSpace(dr["Levels"].ToString()) ? Convert.ToInt32(dr["Levels"].ToString()) : 0,
                            QuickLink= !string.IsNullOrWhiteSpace(dr["QuickLink"].ToString()) ? Convert.ToInt32(dr["QuickLink"].ToString()) : 0,
                            //ModuleId = !string.IsNullOrWhiteSpace(dr["ModuleId"].ToString()) ? Convert.ToInt32(dr["ModuleId"].ToString()) : 0,
                            //ModuleName = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                            //MenuId = !string.IsNullOrWhiteSpace(dr["MenuId"].ToString()) ? Convert.ToInt32(dr["MenuId"].ToString()) : 0,
                            //MenuName = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                            //URL= !string.IsNullOrWhiteSpace(dr["URL"].ToString()) ? dr["URL"].ToString() : "",

                        });
                    }

                }

            }
            catch (Exception ex)
            {
                _ModuleList = null;
            }
            
            return _ModuleList;

        }


        public List<RDD_Modules> GetModuleList()
        {
            List<RDD_Modules> _ModuleList = new List<RDD_Modules>();
            
            try
            {

                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsModules = Db.myGetDS(" Exec RDD_Modules_GetData ");
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    for (int i = 0; i < dtModule.Rows.Count; i++)
                    {
                        RDD_Modules _Module = new RDD_Modules();

                        if (dtModule.Rows[i]["ModuleId"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["ModuleId"]))
                        {
                            _Module.ModuleId = Convert.ToInt32(dtModule.Rows[i]["ModuleId"]);
                        }
                        if (dtModule.Rows[i]["ModuleName"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["ModuleName"]))
                        {
                            _Module.ModuleName = dtModule.Rows[i]["ModuleName"].ToString();
                        }
                        if (dtModule.Rows[i]["cssClass"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["cssClass"]))
                        {
                            _Module.cssClass = dtModule.Rows[i]["cssClass"].ToString();
                        }
                        if (dtModule.Rows[i]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["IsActive"]))
                        {
                            _Module.IsActive = Convert.ToBoolean(dtModule.Rows[i]["IsActive"]);
                        }
                        if (dtModule.Rows[i]["CreatedBy"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["CreatedBy"]))
                        {
                            _Module.CreatedBy = dtModule.Rows[i]["CreatedBy"].ToString();
                        }
                        if (dtModule.Rows[i]["CreatedOn"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["CreatedOn"]))
                        {
                            _Module.CreatedOn = Convert.ToDateTime(dtModule.Rows[i]["CreatedOn"]);
                        }
                        _ModuleList.Add(_Module);
                    }
                }
            }
            catch(Exception ex)
            {
                _ModuleList = null;
            }
            return _ModuleList;

        }


        public List<RDD_DashBoard_Main> GetDashBoarMain(string Username,string Role)
        {

            List<RDD_DashBoard_Main> _MainDash = new List<RDD_DashBoard_Main>();
            try
            {
                SqlParameter[] parm = { };



                // DataSet dsModules = Com.ExecuteDataSet("select  * from RDD_DashBoardTemp");
                SqlParameter[] sqlpar =  {new SqlParameter("@UserId",Username) ,
                new SqlParameter("@Role",Role) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_RetriveDashBoard", CommandType.StoredProcedure, sqlpar);


                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;                                                  
                    foreach (DataRow dr in drc)
                    {
                       _MainDash.Add(new RDD_DashBoard_Main()
                        {                           
                            DashId = !string.IsNullOrWhiteSpace(dr["DashId"].ToString()) ? dr["DashId"].ToString() : "",
                           DashName = !string.IsNullOrWhiteSpace(dr["DashName"].ToString()) ? dr["DashName"].ToString() : "",
                          // colcss = colcss,
                           ColumNames = !string.IsNullOrWhiteSpace(dr["ColumNames"].ToString()) ? dr["ColumNames"].ToString() : "",
                           FieldName = !string.IsNullOrWhiteSpace(dr["FieldName"].ToString()) ? dr["FieldName"].ToString() : "",
                           cssclass = !string.IsNullOrWhiteSpace(dr["cssclass"].ToString()) ? dr["cssclass"].ToString() : "",
                           TypeOfChart = !string.IsNullOrWhiteSpace(dr["TypeOfChart"].ToString()) ? dr["TypeOfChart"].ToString() : "",
                           Url = !string.IsNullOrWhiteSpace(dr["Url"].ToString()) ? dr["Url"].ToString() : "",
                          NoOfColumn = !string.IsNullOrWhiteSpace(dr["NoOfColumn"].ToString()) ?Convert.ToInt32(dr["NoOfColumn"].ToString()) : 0,
                       });
                    }

                }
            }
            catch (Exception ex)
            {

                _MainDash = null;
            }
            return _MainDash;
        }

    }
}
