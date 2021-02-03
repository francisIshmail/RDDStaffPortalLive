using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels.LMS;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Mvc;
using System.Configuration;

namespace RDDStaffPortal.DAL.LMS
{
    public class RDD_LeaveApproval_Db_Operation
    {
        CommonFunction com = new CommonFunction();

        public object Com { get; private set; }

        
        public DataSet GetApprovalList(string Username)
        {
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_LoggedInUser",Username)
            };
            ds = com.ExecuteDataSet("RDD_ShowLeaveApproval", CommandType.StoredProcedure, prm);
            return ds;
        }
        public DataSet GetLeaveRequestList(int Loginuserid)
        {
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                 new SqlParameter("@Type","All"),
                new SqlParameter("@EmployeeId",Loginuserid)
            };
            ds = com.ExecuteDataSet("RDD_GetLeaveRequests", CommandType.StoredProcedure, prm);
            return ds;
        }
        public string UpdateAcceptStatus(int LeaveRequestId,string ApproverRemarks,string LoggedInUser)
        {
            string Msg = "";
            try
            {
                SqlParameter[] parm = new SqlParameter[]
                {
                        new SqlParameter("@Type","Approve"),
                        new SqlParameter("@LeaveRequestId",LeaveRequestId),
                        new SqlParameter("@ApproverRemarks",ApproverRemarks),
                        new SqlParameter("@LoggedInUser",LoggedInUser)
                };                
                Msg = Convert.ToString(com.ExecuteScalar("RDD_ApprovalStatus", parm,CommandType.StoredProcedure));
                
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Msg;
        }
        public string UpdateRejectStatus(int LeaveRequestId, string ApproverRemarks,string LoggedInUser)
        {
            string Msg = "";
            try
            {
                SqlParameter[] parm = new SqlParameter[]
                {
                        new SqlParameter("@Type","Decline"),
                        new SqlParameter("@LeaveRequestId",LeaveRequestId),
                        new SqlParameter("@ApproverRemarks",ApproverRemarks),
                        new SqlParameter("@LoggedInUser",LoggedInUser)
                };
                Msg = Convert.ToString(com.ExecuteScalar("RDD_ApprovalStatus", parm, CommandType.StoredProcedure));

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Msg;
        }
        public string DeleteLeaveRequest(int LeaveRequestId)
        {
            string Msg = "";
            try
            {
                SqlParameter[] parm = new SqlParameter[]
                {
                        new SqlParameter("@Type","Delete"),
                        new SqlParameter("@LeaveRequestId",LeaveRequestId)
                };
                Msg = Convert.ToString(com.ExecuteScalar("RDD_DeleteLeaveRequest", parm, CommandType.StoredProcedure));

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Msg;
        }
        public DataSet GetLeaveDetails(int LeaveRequestId)
        {
            DataSet ds;
            try
            {
                SqlParameter[] ParaDet1 = {
                   new SqlParameter("@LeaveRequestId",LeaveRequestId),
                };                                              

                           
                ds = com.ExecuteDataSet("RDD_GetLeaveDetails", CommandType.StoredProcedure, ParaDet1);

            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;

        }
        public string LeaveLedgerEntry(int LeaveRequestId, int EmployeeId, int LeaveTypeId, string NoOfDays,string CreatedBy,string CreatedOn, string ButtonFlag)
        {
            string Msg = "";
            try
            {
                string Type = "";
                string DebitorCredit = "";
                if (ButtonFlag == "Approve")
                {
                    Type = "ApprovedLeave";
                    DebitorCredit = "Deduct";
                }
               
                SqlParameter[] parm2 =
                  {
                    new SqlParameter("@Type","Insert"),
                    new SqlParameter("@LeaveRequestId",LeaveRequestId),
                    new SqlParameter("@EmployeeId",EmployeeId),
                    new SqlParameter("@LeaveTypeId",LeaveTypeId),
                    
                    new SqlParameter("@Types_",Type),
                    new SqlParameter("@CreditDebit",DebitorCredit),
                    new SqlParameter("@NoOfDays",NoOfDays),                  
                   
                    new SqlParameter("@LoginId",CreatedBy),
                    new SqlParameter("@LoginOn",CreatedOn),
                };
                Msg = Convert.ToString(com.ExecuteScalar("RDD_LeaveLedgerEntry", parm2, CommandType.StoredProcedure));
            }


            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Msg;
        }
        public List<SelectListItem> GetEmployee()
        {
            List<SelectListItem> employeeLists = new List<SelectListItem>();
            try
            {
                SqlParameter[] parm =
                {
                    new SqlParameter("@Type","GetEmployeeName")
                };
                DataSet ds = com.ExecuteDataSet("RDD_GetLeaveRequests", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        employeeLists.Add(new SelectListItem()
                        {
                            Value = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? dr["EmployeeId"].ToString() : "0",
                            Text = !string.IsNullOrWhiteSpace(dr["Fullname"].ToString()) ? dr["Fullname"].ToString() : "0",
                        });
                    }
                }
                return employeeLists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SelectListItem> GetLeaveTypeName(int loginuserId)
        {
            List<SelectListItem> leavetypeLists = new List<SelectListItem>();
            try
            {
                SqlParameter[] parm =
                {
                    new SqlParameter("@loginuserId",loginuserId)
                };
                DataSet ds = com.ExecuteDataSet("RDD_GetLeaveTypeAsPerCountry", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        leavetypeLists.Add(new SelectListItem()
                        {
                            Value = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? dr["LeaveTypeId"].ToString() : "0",
                            Text = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "0",
                        });
                    }
                }
                return leavetypeLists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GetWeeklyOff> GetWeeklyOffDay(int EmployeeId)
        {
            List<GetWeeklyOff> WeeklyOffDays = new List<GetWeeklyOff>();
            try
            {
                SqlParameter[] parm =
                {
                    new SqlParameter("@Type","GetWeeklyOffDay"),
                    new SqlParameter("@EmployeeId",EmployeeId)

                };
                DataSet ds = com.ExecuteDataSet("RDD_GetWeeklyOffDay", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        WeeklyOffDays.Add(new GetWeeklyOff()
                        {
                            LeaveRules = !string.IsNullOrWhiteSpace(dr["LeaveCalculationRule"].ToString()) ? dr["LeaveCalculationRule"].ToString() : "0",
                            WeeklyOff = !string.IsNullOrWhiteSpace(dr["WeeklyOFFDay"].ToString()) ? dr["WeeklyOFFDay"].ToString() : "0",
                        });
                    }
                }
                return WeeklyOffDays;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetHolidayCountryWise(int EmployeeId)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@Type","GetHolidayCountrywise"),
                new SqlParameter("@EmployeeId",EmployeeId)

            };
            ds = com.ExecuteDataSet("RDD_GetWeeklyOffDay", CommandType.StoredProcedure, prm);
            return ds;
        }
        public DataSet GetLeaveBalance(int EmployeeId, int LeaveTypeId)
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@Type","Show"),
                new SqlParameter("@EmployeeId",EmployeeId),
                new SqlParameter("@LeaveTypeId",LeaveTypeId)

            };

            ds = com.ExecuteDataSet("RDD_LeaveLedgerEntry", CommandType.StoredProcedure, prm);
            return ds;
        }
        public DataSet GetAnnualLeaveBalance(int EmployeeId)
        {
            DataSet ds = new DataSet();

            SqlParameter[] prm =
            {

                new SqlParameter("@p_EmployeeId",EmployeeId),


            };

            ds = com.ExecuteDataSet("RDD_LMS_GetLeaveSummary", CommandType.StoredProcedure, prm);
            return ds;
        }

        public int GetEmployeeIdByLoginName(string LoginName)
        {
            int EmployeeId = 0;
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
                        cmd.CommandText = "RDD_GetEmployeeIdByLoginName";
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        cmd.Parameters.Add("@p_LoginName", SqlDbType.NVarChar, 50).Value = LoginName;

                        cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        EmployeeId = (int)cmd.Parameters["@p_EmployeeId"].Value;
                        cmd.Dispose();
                        transaction.Commit();

                    }

                    catch (Exception ex)
                    {
                        EmployeeId = 0;
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
            return EmployeeId;

        }
    }
}
