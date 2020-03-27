using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels;
using System.Configuration;

namespace RDDStaffPortal.DAL.InitialSetup
{
    public class ModulesDbOperation
    {
        
        /// <summary>
        ///  This method is to save the RDD_Modules, It accepts RDD_Module class as argument and returns the message
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
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

    }
}
