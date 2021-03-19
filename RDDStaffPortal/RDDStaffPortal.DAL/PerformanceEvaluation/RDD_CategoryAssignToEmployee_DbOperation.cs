using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL.DataModels;
using System.Transactions;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_CategoryAssignToEmployee_DbOperation
    {
        CommonFunction Com = new CommonFunction();

        public DataSet GetCatEmpDetails()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetData")
                };
                ds = Com.ExecuteDataSet("RDD_Appraisal_AssignCategorytoEmployee_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public List<Rdd_comonDrop> GetUserListAuto(string psearch)
        {
            List<Rdd_comonDrop> _UserList = new List<Rdd_comonDrop>();
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_search", psearch), new SqlParameter("@Type", "GetEmployeeListAutocomplete") };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Appraisal_AssignCategorytoEmployee_GetData", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _UserList.Add(new Rdd_comonDrop()
                        {
                            Code = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? dr["EmployeeId"].ToString() : "",
                            CodeName = !string.IsNullOrWhiteSpace(dr["EmployeeName"].ToString()) ? dr["EmployeeName"].ToString() : "",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _UserList = null;
            }

            return _UserList;
        }

        public DataSet GetCategoryList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetCategoryList")
                };
                ds = Com.ExecuteDataSet("RDD_Appraisal_AssignCategorytoEmployee_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public RDD_CategoryAssignToEmployee SaveAssignCategoryDetails(RDD_CategoryAssignToEmployee rDD_AssignCategory)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (rDD_AssignCategory.EditFlag == false)
                    {
                        rDD_AssignCategory.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_AssignCategory.ActionType = "Update";
                    }
                    SqlParameter[] parm = {
                        new SqlParameter("@CategoryAssignId",rDD_AssignCategory.CategoryAssignId),
                        new SqlParameter("@CategoryId",rDD_AssignCategory.CategoryId.TrimEnd(',')),
                        new SqlParameter("@EmployeeId",rDD_AssignCategory.EmployeeId),                        
                        new SqlParameter("@CreatedBy",rDD_AssignCategory.CreatedBy),
                        new SqlParameter("@LastUpdatedBy",rDD_AssignCategory.LastUpdatedBy),
                        new SqlParameter("@Type",rDD_AssignCategory.ActionType),
                        new SqlParameter("@p_ide",rDD_AssignCategory.id),
                        new SqlParameter("@Response",rDD_AssignCategory.ErrorMsg)
                    };

                    List<Outcls1> outcls = new List<Outcls1>();
                    outcls = Com.ExecuteNonQueryListID("RDD_Appraisal_AssignCategorytoEmployee_Insert_Update_Delete", parm);
                    rDD_AssignCategory.SaveFlag = outcls[0].Outtf;
                    rDD_AssignCategory.ErrorMsg = outcls[0].Responsemsg;
                    if (outcls[0].Id == -1)
                    {
                        rDD_AssignCategory.SaveFlag = false;
                    }
                    else
                    {
                        rDD_AssignCategory.SaveFlag = true;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_AssignCategory.ErrorMsg = ex.Message;
                rDD_AssignCategory.SaveFlag = false;
            }
            return rDD_AssignCategory;
        }

        public List<Outcls> DeleteAssignCategory(int EmployeeId)
        {
            //RDD_Holidays rDD_Holiday = new RDD_Holidays();
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                //using (TransactionScope scope = new TransactionScope())

                string response = string.Empty;                
                SqlParameter[] parm ={ new SqlParameter("@EmployeeId",EmployeeId),
                         new SqlParameter("@Type","Delete"),
                         new SqlParameter("@p_ide",EmployeeId),
                         new SqlParameter("@Response",response)
                    };
                outcls = Com.ExecuteNonQueryList("RDD_Appraisal_AssignCategorytoEmployee_Insert_Update_Delete", parm);

            }
            catch (Exception ex)
            {

                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }
    }
}
