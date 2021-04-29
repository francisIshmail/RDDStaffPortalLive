using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.PerformanceEvaluation
{
    public class RDD_QuestionCategory_DbOperation
    {
        CommonFunction Com = new CommonFunction();
        public DataSet GetDepartmentList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetDepartment")
                };
                ds = Com.ExecuteDataSet("RDD_QuestionCategory_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetCategoryDetails()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetCategoryDetails")
                };
                ds = Com.ExecuteDataSet("RDD_QuestionCategory_GetData", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public RDD_QuestionCategory SaveCategory(RDD_QuestionCategory rDD_Category)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (rDD_Category.EditFlag == false)
                    {
                        rDD_Category.ActionType = "Insert";
                    }
                    else
                    {
                        rDD_Category.ActionType = "Update";
                    }
                    SqlParameter[] parm = {
                        new SqlParameter("@CategoryId",rDD_Category.CategoryId),
                        new SqlParameter("@CategoryName",rDD_Category.CategoryName.ToLower()),
                        new SqlParameter("@DeptId",rDD_Category.DepartmentId.TrimEnd(',')),                        
                        new SqlParameter("@CreatedBy",rDD_Category.CreatedBy),                       
                        new SqlParameter("@Type",rDD_Category.ActionType),
                        new SqlParameter("@p_ide",rDD_Category.id),
                        new SqlParameter("@Response",rDD_Category.ErrorMsg)
                    };

                    List<Outcls1> outcls = new List<Outcls1>();
                    outcls = Com.ExecuteNonQueryListID("RDD_Insert_Update_QuestionCategory", parm);
                    rDD_Category.SaveFlag = outcls[0].Outtf;
                    rDD_Category.ErrorMsg = outcls[0].Responsemsg;
                    if (outcls[0].Id == -1)
                    {
                        rDD_Category.SaveFlag = false;
                    }
                    else
                    {
                        rDD_Category.SaveFlag = true;
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_Category.ErrorMsg = ex.Message;
                rDD_Category.SaveFlag = false;
            }
            return rDD_Category;
        }

        public List<Outcls> DeleteCategory(int Categoryid)
        {
            //RDD_Holidays rDD_Holiday = new RDD_Holidays();
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                //using (TransactionScope scope = new TransactionScope())

                string response = string.Empty;
                SqlParameter[] parm ={ new SqlParameter("@CategoryId",Categoryid),
                         new SqlParameter("@Type","Delete"),
                         new SqlParameter("@p_ide",Categoryid),
                         new SqlParameter("@Response",response)
                    };
                outcls = Com.ExecuteNonQueryList("RDD_Insert_Update_QuestionCategory", parm);
                
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
