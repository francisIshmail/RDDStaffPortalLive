using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels;


namespace RDDStaffPortal.DAL.InitialSetup
{
    public class MenusDbOperation
    {
        /// <summary>
        ///  This method is to save the RDD_Menus, It accepts RDD_Menus class as argument and returns the message
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
        public string Save(RDD_Menus menus)
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
                            cmd.CommandText = "RDD_Menus_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_MenuId", SqlDbType.Int).Value = menus.MenuId;
                            cmd.Parameters.Add("@p_MenuName", SqlDbType.NVarChar,150).Value = menus.MenuName;
                            cmd.Parameters.Add("@p_ModuleId", SqlDbType.Int).Value = menus.ModuleId;
                            cmd.Parameters.Add("@p_cssClass", SqlDbType.NVarChar,50).Value = menus.MenuCssClass;
                            cmd.Parameters.Add("@p_URL", SqlDbType.NVarChar,1000).Value = menus.URL;
                            cmd.Parameters.Add("@p_DisplaySeq", SqlDbType.Int).Value = menus.DisplaySeq;
                            cmd.Parameters.Add("@p_IsDefault", SqlDbType.Bit).Value = menus.IsDefault;
                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.VarChar,50).Value = menus.CreatedBy;
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

        public RDD_Menus GetMenusById(int MenuId)
        {
            RDD_Menus _Menus = new RDD_Menus();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsMenus = Db.myGetDS(" Exec RDD_Menus_GetData " + MenuId);
                if (dsMenus.Tables.Count > 0)
                {
                    DataTable dtMenus = dsMenus.Tables[0];

                    if (dtMenus.Rows[0]["MenuId"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["MenuId"]))
                    {
                        _Menus.MenuId = Convert.ToInt32(dtMenus.Rows[0]["MenuId"]);
                    }
                    if (dtMenus.Rows[0]["MenuName"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["MenuName"]))
                    {
                        _Menus.MenuName = dtMenus.Rows[0]["MenuName"].ToString();
                    }
                    if (dtMenus.Rows[0]["MenuCssClass"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["MenuCssClass"]))
                    {
                        _Menus.MenuCssClass = dtMenus.Rows[0]["MenuCssClass"].ToString();
                    }
                    if (dtMenus.Rows[0]["URL"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["URL"]))
                    {
                        _Menus.URL = dtMenus.Rows[0]["URL"].ToString() ;
                    }
                    if (dtMenus.Rows[0]["DisplaySeq"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["DisplaySeq"]))
                    {
                        _Menus.DisplaySeq = Convert.ToInt32(dtMenus.Rows[0]["DisplaySeq"]);
                    }
                    if (dtMenus.Rows[0]["ModuleId"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["ModuleId"]))
                    {
                        _Menus.ModuleId = Convert.ToInt32(dtMenus.Rows[0]["ModuleId"]);
                    }
                    if (dtMenus.Rows[0]["ModuleName"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["ModuleName"]))
                    {
                        _Menus.ModuleName = dtMenus.Rows[0]["ModuleName"].ToString() ;
                    }
                    if (dtMenus.Rows[0]["ModuleCssClass"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["ModuleCssClass"]))
                    {
                        _Menus.ModuleCssClass = dtMenus.Rows[0]["ModuleCssClass"].ToString();
                    }
                    if (dtMenus.Rows[0]["IsDefault"] != null && !DBNull.Value.Equals(dtMenus.Rows[0]["IsDefault"]))
                    {
                        _Menus.IsDefault = Convert.ToBoolean(dtMenus.Rows[0]["IsDefault"]);
                    }

                }
            }
            catch (Exception ex)
            {
                _Menus= null;
            }
            return _Menus;

        }

        public List<RDD_Menus> GetMenuList()
        {
            List<RDD_Menus> _MenusList = new List<RDD_Menus>();

            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsMenus = Db.myGetDS(" Exec RDD_Menus_GetData ");
                if (dsMenus.Tables.Count > 0)
                {
                    DataTable dtMenus = dsMenus.Tables[0];
                    for (int i = 0; i < dtMenus.Rows.Count; i++)
                    {
                        RDD_Menus _Menus = new RDD_Menus();

                        if (dtMenus.Rows[i]["MenuId"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["MenuId"]))
                        {
                            _Menus.MenuId = Convert.ToInt32(dtMenus.Rows[i]["MenuId"]);
                        }
                        if (dtMenus.Rows[i]["MenuName"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["MenuName"]))
                        {
                            _Menus.MenuName = dtMenus.Rows[i]["MenuName"].ToString();
                        }
                        if (dtMenus.Rows[i]["MenuCssClass"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["MenuCssClass"]))
                        {
                            _Menus.MenuCssClass = dtMenus.Rows[i]["MenuCssClass"].ToString();
                        }
                        if (dtMenus.Rows[i]["URL"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["URL"]))
                        {
                            _Menus.URL = dtMenus.Rows[i]["URL"].ToString();
                        }
                        if (dtMenus.Rows[i]["DisplaySeq"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["DisplaySeq"]))
                        {
                            _Menus.DisplaySeq = Convert.ToInt32(dtMenus.Rows[i]["DisplaySeq"]);
                        }
                        if (dtMenus.Rows[i]["ModuleId"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["ModuleId"]))
                        {
                            _Menus.ModuleId = Convert.ToInt32(dtMenus.Rows[i]["ModuleId"]);
                        }
                        if (dtMenus.Rows[i]["ModuleName"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["ModuleName"]))
                        {
                            _Menus.ModuleName = dtMenus.Rows[i]["ModuleName"].ToString();
                        }
                        if (dtMenus.Rows[i]["ModuleCssClass"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["ModuleCssClass"]))
                        {
                            _Menus.ModuleCssClass = dtMenus.Rows[i]["ModuleCssClass"].ToString();
                        }
                        if (dtMenus.Rows[i]["IsDefault"] != null && !DBNull.Value.Equals(dtMenus.Rows[i]["IsDefault"]))
                        {
                            _Menus.IsDefault = Convert.ToBoolean(dtMenus.Rows[i]["IsDefault"]);
                        }

                        _MenusList.Add(_Menus);
                    }
                }
            }
            catch (Exception ex)
            {
                _MenusList = null;
            }
            return _MenusList;

        }

    }
}
