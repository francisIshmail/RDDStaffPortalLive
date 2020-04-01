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
    public class DepartmentsDbOperation

    {  /// <summary>
       ///  This method is to save the RDD_Menus, It accepts RDD_Menus class as argument and returns the message
       /// </summary>
       /// <param name="modules"></param>
       /// <ret
        public string Save(RDD_Departments Dept)
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
                            cmd.CommandText = "DeptName_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_DeptId", SqlDbType.Int).Value = Convert.ToInt16(Dept.DeptId);
                            cmd.Parameters.Add("@p_DeptName", SqlDbType.VarChar, 50).Value = Dept.DeptName;
                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value = Dept.IsActive;
                            // cmd.Parameters.Add("@p_Status",SqlDbType.Bit).Value = Dept.Status;
                            //cmd.Parameters.Add("@p_LoggedInUser", SqlDbType.NVarChar, 50).Value = User.Identity.Name;

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 50).Value = Dept.CreatedBy;

                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
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


        public List<RDD_Departments> GetDeptList()
        {
            List<RDD_Departments> _DeptList = new List<RDD_Departments>();
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
                DataSet dsDept = Db.myGetDS("EXEC dbo.DeptName_GetData");
                if (dsDept.Tables.Count > 0)
                {
                    DataTable dtModule = dsDept.Tables[0];
                    for (int i = 0; i < dsDept.Tables[0].Rows.Count; i++)
                    {
                        RDD_Departments itm = new RDD_Departments();
                        if (dsDept.Tables[0].Rows[i]["DeptId"] != null && !DBNull.Value.Equals(dsDept.Tables[0].Rows[i]["DeptId"]))
                        {
                            itm.DeptId = Convert.ToInt32(dsDept.Tables[0].Rows[i]["DeptId"]);
                        }
                        if (dsDept.Tables[0].Rows[i]["DeptName"] != null && !DBNull.Value.Equals(dsDept.Tables[0].Rows[i]["DeptName"]))
                        {
                            itm.DeptName = dsDept.Tables[0].Rows[i]["DeptName"].ToString();
                        }
                        if (dsDept.Tables[0].Rows[i]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["IsActive"]))
                        {
                            itm.IsActive = Convert.ToBoolean(dsDept.Tables[0].Rows[i]["IsActive"]);
                        }
                        _DeptList.Add(itm);
                    }

                }
            }

            catch (Exception ex)
            {
                _DeptList = null;
            }
            return _DeptList;
        }



        public RDD_Departments GetDeptID(int DeptId)
        {
            RDD_Departments _Dept = new RDD_Departments();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsDept = Db.myGetDS("EXEC DeptName_GetData " + DeptId.ToString());
                if (dsDept.Tables.Count > 0)
                {
                    DataTable dtDept = dsDept.Tables[0];
                    if (dtDept.Rows[0]["DeptId"] != null && !DBNull.Value.Equals(dtDept.Rows[0]["DeptId"]))
                    {
                        _Dept.DeptId = Convert.ToInt32(dtDept.Rows[0]["DeptId"]);
                    }
                    if (dtDept.Rows[0]["DeptName"] != null && !DBNull.Value.Equals(dtDept.Rows[0]["DeptName"]))
                    {
                        _Dept.DeptName = dtDept.Rows[0]["DeptName"].ToString();
                    }
                  
                    if (dtDept.Rows[0]["IsActive"] != null && !DBNull.Value.Equals(dtDept.Rows[0]["IsActive"]))
                    {
                        _Dept.IsActive = Convert.ToBoolean(dtDept.Rows[0]["IsActive"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                _Dept = null;
            }
            return _Dept;


           

            //public JsonResult DeleteItem(int DeptId)
            //{
            //    var result = string.Empty;
            //    try
            //    {
            //        Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

            //        string sql = "Update DepartmentMaster Set IsDeleted=1 Where DeptId=" + DeptId.ToString();
            //        Db.myExecuteSQL(sql);
            //        result = "Record Deleted successfully";

            //    }
            //    catch (Exception ex)
            //    {
            //        result = "Failed to delete : " + ex.Message;
            //    }

            //    return Json(result, JsonRequestBehavior.AllowGet);

            //}


        }

        public string Delete(int DeptId)
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
                            cmd.CommandText = "DeptName_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_DeptId", SqlDbType.Int).Value = Convert.ToInt16(DeptId);
                          
                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
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





    }
}

