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

namespace RDDStaffPortal.DAL.LMS
{
    public class RDD_LeaveRequest_Db_Operation
    {
        CommonFunction com = new CommonFunction();
        public List<Outcls1> Save(RDD_LeaveRequest rDD_LeaveRequest)
        {
            List<Outcls1> outcls = new List<Outcls1>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    if (!rDD_LeaveRequest.Editflag)
                    {
                        rDD_LeaveRequest.ActionType = "Insert";

                    }
                    else
                    {
                        rDD_LeaveRequest.ActionType = "Update";
                    }
                    SqlParameter[] parm = {
                        
                        new SqlParameter("@EmployeeId",rDD_LeaveRequest.EmployeeId),
                        new SqlParameter("@LeaveTypeId",rDD_LeaveRequest.LeaveTypeId),
                        new SqlParameter("@Reason",rDD_LeaveRequest.Reason),
                        new SqlParameter("@FromDate",rDD_LeaveRequest.FromDate),
                        new SqlParameter("@ToDate",rDD_LeaveRequest.ToDate),
                        new SqlParameter("@NoOfDays",rDD_LeaveRequest.NoOfDays),
                        new SqlParameter("@LeaveStatus",rDD_LeaveRequest.LeaveStatus),
                        new SqlParameter("@ApproverRemarks",rDD_LeaveRequest.ApproverRemarks),
                        new SqlParameter("@IsPrivateLeave",rDD_LeaveRequest.IsPrivateLeave),                       
                        new SqlParameter("@AttachmentUrl",rDD_LeaveRequest.AttachmentUrl),
                        new SqlParameter("@LoginId",rDD_LeaveRequest.CreatedBy),
                        new SqlParameter("@LoginDate",rDD_LeaveRequest.CreatedOn),                        
                        new SqlParameter("@Type",rDD_LeaveRequest.ActionType),
                        new SqlParameter("@LeaveRequestId",rDD_LeaveRequest.LeaveRequestId),
                        new SqlParameter("@p_id",rDD_LeaveRequest.LeaveRequestId),
                        new SqlParameter("@p_response",response),
                    };

                    
                    outcls = com.ExecuteNonQueryListID("RDD_LeaveRequest", parm);
                    rDD_LeaveRequest.LeaveRequestId = outcls[0].Id;
                    if (outcls[0].Outtf == true && outcls[0].Id != -1)
                    {
                        rDD_LeaveRequest.ActionType = "Insert";
                        int m = 0;
                        int n = 0;
                        if (rDD_LeaveRequest.Editflag == true)
                        {
                            SqlParameter[] paraDet3 =
                            {
                                 new SqlParameter("@LeaveRequestId",outcls[0].Id),
                            };
                            var det3 = com.ExecuteNonQuery("RDD_DeleteLeaveRequestDetails", paraDet3);
                        }
                        else
                        {
                            SqlParameter[] ParaDet2 = {

                                new SqlParameter("@p_LeaveRequestId",outcls[0].Id),
                                new SqlParameter("@p_LoggedInUser",rDD_LeaveRequest.CreatedBy),

                            };
                            var det2 = com.ExecuteNonQuery("RDD_LeaveRequestApproval", ParaDet2);
                            if (det2 == false)
                            {
                                outcls.Clear();
                                outcls.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : Leave Request Approver Details "
                                });

                            }
                        }


                            while (rDD_LeaveRequest.LeaveRequestDetailsList.Count > m)
                            {
                                SqlParameter[] ParaDet1 = {
                                new SqlParameter("@Type",rDD_LeaveRequest.ActionType),
                                new SqlParameter("@LeaveRequestId",outcls[0].Id),
                                new SqlParameter("@LeaveDate",rDD_LeaveRequest.LeaveRequestDetailsList[m].LeaveDate),
                                new SqlParameter("@LeaveDayType",rDD_LeaveRequest.LeaveRequestDetailsList[m].LeaveDayType),
                                new SqlParameter("@LeaveDay",rDD_LeaveRequest.LeaveRequestDetailsList[m].LeaveDay),
                                new SqlParameter("@LoginId",rDD_LeaveRequest.LeaveRequestDetailsList[m].CreatedBy),
                                new SqlParameter("@LoginDate",rDD_LeaveRequest.LeaveRequestDetailsList[m].CreatedOn)

                                };
                                var det1 = com.ExecuteNonQuery("RDD_LeaveRequestDetail", ParaDet1);
                                if (det1 == false)
                                {
                                    outcls.Clear();
                                    outcls.Add(new Outcls1
                                    {
                                        Outtf = false,
                                        Id = -1,
                                        Responsemsg = "Error occured : Leave Request Details "
                                    });

                                }
                                m++;
                            }                         


                        
                        //rDD_LeaveRequest.Saveflag = outcls[0].Outtf;
                        //rDD_LeaveRequest.ErrorMsg = outcls[0].Responsemsg;
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                rDD_LeaveRequest.ErrorMsg = ex.Message;
                rDD_LeaveRequest.Saveflag = false;
                rDD_LeaveRequest.LeaveRequestId = -1;
            }
            return outcls;
        }
        public List<RDD_LeaveRequest> GetList()
        {
            List<RDD_LeaveRequest> GetListData = new List<RDD_LeaveRequest>();
            try
            {
                SqlParameter[] parm ={
                         new SqlParameter("@Type","All")  };
                DataSet ds = com.ExecuteDataSet("RDD_GetLeaveRequests", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_LeaveRequest()
                        {

                            EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0,
                            LeaveTypeId = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? Convert.ToInt32(dr["LeaveTypeId"].ToString()) : 0,
                            FromDate = Convert.ToDateTime(dr["FromDate"].ToString()),
                            ToDate = Convert.ToDateTime(dr["ToDate"].ToString()),
                            NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? (dr["NoOfDays"].ToString()) : "",
                            LeaveStatus = !string.IsNullOrWhiteSpace(dr["LeaveStatus"].ToString()) ? dr["LeaveStatus"].ToString() : "",
                            ApproverRemarks = !string.IsNullOrWhiteSpace(dr["ApproverRemarks"].ToString()) ? dr["ApproverRemarks"].ToString() : "",
                            AttachmentUrl = !string.IsNullOrWhiteSpace(dr["AttachmentUrl"].ToString()) ? dr["AttachmentUrl"].ToString() : "",
                            LeaveRequestId = !string.IsNullOrWhiteSpace(dr["LeaveRequestId"].ToString()) ? Convert.ToInt32(dr["LeaveRequestId"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                GetListData.Add(new RDD_LeaveRequest()
                {
                    EmployeeId = 0,
                    LeaveTypeId = 0,
                    FromDate = null,
                    ToDate = null,
                    NoOfDays = "",
                    LeaveStatus = "",
                    ApproverRemarks = "",
                    AttachmentUrl = "",
                    LeaveRequestId = 0
                });
            }

            return GetListData;

        }
        public RDD_LeaveRequest GetData(string LeaveRequestId)
        {
            RDD_LeaveRequest rDD_LeaveRequest = new RDD_LeaveRequest();
            try
            {
                SqlParameter[] parm ={
                new SqlParameter("@LeaveRequestId",LeaveRequestId)  ,
                 new SqlParameter("Type","Single")  };
                DataSet ds = com.ExecuteDataSet("RDD_GetLeaveRequests", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        rDD_LeaveRequest.EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0;
                        rDD_LeaveRequest.LeaveTypeId = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? Convert.ToInt32(dr["LeaveTypeId"].ToString()) : 0;
                        rDD_LeaveRequest.FromDate = Convert.ToDateTime(dr["FromDate"].ToString());
                        rDD_LeaveRequest.ToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                        rDD_LeaveRequest.NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? dr["NoOfDays"].ToString() : "";
                        rDD_LeaveRequest.LeaveStatus = !string.IsNullOrWhiteSpace(dr["LeaveStatus"].ToString()) ? dr["LeaveStatus"].ToString() : "";
                        rDD_LeaveRequest.ApproverRemarks = !string.IsNullOrWhiteSpace(dr["ApproverRemarks"].ToString()) ? dr["ApproverRemarks"].ToString() : "";
                        rDD_LeaveRequest.AttachmentUrl = !string.IsNullOrWhiteSpace(dr["AttachmentUrl"].ToString()) ? dr["AttachmentUrl"].ToString() : "";
                        rDD_LeaveRequest.Editflag = true;
                    }
                }
            }
            catch (Exception ex)
            {

                rDD_LeaveRequest.EmployeeId = 0;
                rDD_LeaveRequest.LeaveTypeId = 0;
                rDD_LeaveRequest.FromDate = null;
                rDD_LeaveRequest.ToDate = null;
                rDD_LeaveRequest.NoOfDays = "";
                rDD_LeaveRequest.LeaveStatus = "";
                rDD_LeaveRequest.ApproverRemarks = "";
                rDD_LeaveRequest.AttachmentUrl = "";

            }
            return rDD_LeaveRequest;
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
                            Text= !string.IsNullOrWhiteSpace(dr["Fullname"].ToString()) ? dr["Fullname"].ToString() : "0",
                        });
                    }
                }
                return employeeLists;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<Rdd_comonDrop> GetEmployeeModal()
        {
            List<Rdd_comonDrop> employeeLists = new List<Rdd_comonDrop>();
            try
            {
                SqlParameter[] parm =
                {
                    new SqlParameter("@Type","GetEmployeeNameForModal")
                };
                DataSet ds = com.ExecuteDataSet("RDD_GetLeaveRequests", CommandType.StoredProcedure, parm);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        employeeLists.Add(new Rdd_comonDrop()
                        {
                            Code = !string.IsNullOrWhiteSpace(dr["EmployeeIde"].ToString()) ? dr["EmployeeIde"].ToString() : "0",
                            CodeName = !string.IsNullOrWhiteSpace(dr["Fullname"].ToString()) ? dr["Fullname"].ToString() : "0",
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
        public List<SelectListItem> GetEmployeeAutoComplete(string name ,int EmployeeId)
        {
            List<SelectListItem> employeeLists = new List<SelectListItem>();
            try
            {
                SqlParameter[] parm =
                {
                    new SqlParameter("@Type","GetEmployeeNameAutoCheck"),
                    new SqlParameter("@Name",name),
                    new SqlParameter("@EmployeeId",EmployeeId),
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
                            Value = !string.IsNullOrWhiteSpace(dr["EmployeeIde"].ToString()) ? dr["EmployeeIde"].ToString() : "0",
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
        //public List<SelectListItem> GetLeaveTypeName(int loginuserId)
        //{
        //    List<SelectListItem> leavetypeLists = new List<SelectListItem>();
        //    try
        //    {
        //        SqlParameter[] parm =
        //        {
        //            new SqlParameter("@loginuserId",loginuserId)
        //        };
        //        DataSet ds = com.ExecuteDataSet("RDD_GetLeaveTypeAsPerCountry", CommandType.StoredProcedure, parm);

        //        if (ds.Tables.Count > 0)
        //        {
        //            DataTable dt = ds.Tables[0];
        //            DataRowCollection drc = dt.Rows;
        //            foreach (DataRow dr in drc)
        //            {
        //                leavetypeLists.Add(new SelectListItem()
        //                {
        //                    Value = !string.IsNullOrWhiteSpace(dr["LeaveTypeId"].ToString()) ? dr["LeaveTypeId"].ToString() : "0",
        //                    Text = !string.IsNullOrWhiteSpace(dr["LeaveName"].ToString()) ? dr["LeaveName"].ToString() : "0",
        //                });
        //            }
        //        }
        //        return leavetypeLists;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public DataSet CountryLeaveType(int EmployeeId)        {            DataSet ds = null;            try            {                SqlParameter[] Para =               {                    new SqlParameter("@loginuserId",EmployeeId),                };                ds = com.ExecuteDataSet("RDD_GetLeaveTypeAsPerCountry", CommandType.StoredProcedure, Para);            }            catch (Exception)            {                throw;            }            return ds;

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
                          LeaveRules= !string.IsNullOrWhiteSpace(dr["LeaveCalculationRule"].ToString()) ? dr["LeaveCalculationRule"].ToString() : "0",
                          WeeklyOff  = !string.IsNullOrWhiteSpace(dr["WeeklyOFFDay"].ToString()) ? dr["WeeklyOFFDay"].ToString() : "0",
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
        public DataSet GetHRRole(string UserName)
        {
            DataSet ds = null;
            SqlParameter[] parm = { new SqlParameter("@p_Username", UserName) };
            ds = com.ExecuteDataSet("RDD_GetUserType", CommandType.StoredProcedure, parm);

            return ds;
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
        public DataSet GetLeaveBalance(int EmployeeId,int LeaveTypeId)
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


    }
}


