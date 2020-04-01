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
   public  class DesignationDbOperation
    {
        public string Save(RDD_Designation Desig)
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
                            cmd.CommandText = "DesigName_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("@p_DesigId", SqlDbType.Int).Value = Convert.ToInt16(Desig.DesigId);
                            cmd.Parameters.Add("@p_DesigName", SqlDbType.VarChar, 50).Value = Desig.DesigName;
                            cmd.Parameters.Add("@p_IsActive", SqlDbType.Bit).Value = Desig.IsActive;
             
                           
                                cmd.Parameters.Add("@p_CreatedBy", SqlDbType.NVarChar, 50).Value = Desig.CreatedBy;

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


        public List<RDD_Designation> GetDesigList()
        {
            List<RDD_Designation> _DesigList = new List<RDD_Designation>();
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
                DataSet dsDesig = Db.myGetDS("EXEC dbo.DesigName_GetData");
                if (dsDesig.Tables.Count > 0)
                {
                    DataTable dtModule = dsDesig.Tables[0];
                    for (int i = 0; i < dsDesig.Tables[0].Rows.Count; i++)
                    {
                        RDD_Designation itm = new RDD_Designation();
                        if (dsDesig.Tables[0].Rows[i]["DesigId"] != null && !DBNull.Value.Equals(dsDesig.Tables[0].Rows[i]["DesigId"]))
                        {
                            itm.DesigId = Convert.ToInt32(dsDesig.Tables[0].Rows[i]["DesigId"]);
                        }
                        if (dsDesig.Tables[0].Rows[i]["DesigName"] != null && !DBNull.Value.Equals(dsDesig.Tables[0].Rows[i]["DesigName"]))
                        {
                            itm.DesigName = dsDesig.Tables[0].Rows[i]["DesigName"].ToString();
                        }
                        if (dsDesig.Tables[0].Rows[i]["IsActive"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["IsActive"]))
                        {
                            itm.IsActive = Convert.ToBoolean(dsDesig.Tables[0].Rows[i]["IsActive"]);
                        }
                        _DesigList.Add(itm);
                    }

                }
            }

            catch (Exception ex)
            {
                _DesigList = null;
            }
            return _DesigList;
        }



        public RDD_Designation GetDesigID(int DesigId)
        {
            RDD_Designation _Desig = new RDD_Designation();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dsDesig = Db.myGetDS("EXEC DesigName_GetData " + DesigId.ToString());
                if (dsDesig.Tables.Count > 0)
                {
                    DataTable dtDesig = dsDesig.Tables[0];
                    if (dtDesig.Rows[0]["DeptId"] != null && !DBNull.Value.Equals(dtDesig.Rows[0]["DeptId"]))
                    {
                        _Desig.DesigId = Convert.ToInt32(dtDesig.Rows[0]["DeptId"]);
                    }
                    if (dtDesig.Rows[0]["DeptName"] != null && !DBNull.Value.Equals(dtDesig.Rows[0]["DeptName"]))
                    {
                        _Desig.DesigName = dtDesig.Rows[0]["DeptName"].ToString();
                    }
                   
                    if (dtDesig.Rows[0]["IsActive"] != null && !DBNull.Value.Equals(dtDesig.Rows[0]["IsActive"]))
                    {
                        _Desig.IsActive = Convert.ToBoolean(dtDesig.Rows[0]["IsActive"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                _Desig = null;
            }
            return _Desig;


            //public JsonResult GetDeptID(int DeptId)
            //{
            //    Department itm = new Department();
            //    Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            //    DataSet DS = Db.myGetDS("EXEC DeptName_GetData " + DeptId.ToString());
            //    if (DS.Tables.Count > 0)
            //    {
            //        if (DS.Tables[0].Rows.Count > 0)
            //        {
            //            if (DS.Tables[0].Rows[0]["DeptId"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DeptId"]))
            //            {
            //                itm.DeptId = Convert.ToInt32(DS.Tables[0].Rows[0]["DeptId"]);
            //            }
            //            if (DS.Tables[0].Rows[0]["DeptName"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["DeptName"]))
            //            {
            //                itm.DeptName = DS.Tables[0].Rows[0]["DeptName"].ToString();
            //            }
            //        }
            //    }

            //    string value = string.Empty;
            //    value = JsonConvert.SerializeObject(itm, Formatting.Indented, new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //    });
            //    return Json(itm, JsonRequestBehavior.AllowGet);
            //}

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

        public string Delete(int DesigId)
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
                            cmd.CommandText = "DesigName_Delete";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_DesigId", SqlDbType.Int).Value = Convert.ToInt16(DesigId);
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

