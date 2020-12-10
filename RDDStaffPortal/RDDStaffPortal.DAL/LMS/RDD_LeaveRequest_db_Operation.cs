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


namespace RDDStaffPortal.DAL.LMS
{
    public class RDD_LeaveRequest_Db_Operation
    {
        CommonFunction com = new CommonFunction();
        public RDD_LeaveRequest Save(RDD_LeaveRequest rDD_LeaveRequest)
        {
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
                    SqlParameter[] parm = { new SqlParameter("@LeaveRequestId",rDD_LeaveRequest.LeaveRequestId),
                        new SqlParameter("@EmployeeId",rDD_LeaveRequest.EmployeeId),
                        new SqlParameter("@LeaveTypeId",rDD_LeaveRequest.LeaveTypeId),
                        new SqlParameter("@Reason",rDD_LeaveRequest.Reason),
                        new SqlParameter("@FromDate",rDD_LeaveRequest.FromDate),
                        new SqlParameter("@ToDate",rDD_LeaveRequest.ToDate),
                        new SqlParameter("@NoOfDays",rDD_LeaveRequest.NoOfDays),
                        new SqlParameter("@LeaveStatus",rDD_LeaveRequest.LeaveStatus),
                        new SqlParameter("@ApproverRemarks",rDD_LeaveRequest.ApproverRemarks),
                        new SqlParameter("@IsPrivateLeave",rDD_LeaveRequest.IsPrivateLeave),
                        new SqlParameter("@IsDeleted",rDD_LeaveRequest.IsDeleted),
                        new SqlParameter("@AttachmentUrl",rDD_LeaveRequest.AttachmentUrl),
                        new SqlParameter("@CreatedBy",rDD_LeaveRequest.CreatedBy),
                        new SqlParameter("@CreatedOn",rDD_LeaveRequest.CreatedOn),
                        new SqlParameter("@LastUpdatedBy",rDD_LeaveRequest.LastUpdatedBy),
                        new SqlParameter("@LastUpdatedOn",rDD_LeaveRequest.LastUpdatedOn),
                        new SqlParameter("@Type",rDD_LeaveRequest.ActionType),
                        new SqlParameter("@p_response",response),
                             };

                    List<Outcls> outcls = new List<Outcls>();
                    outcls = com.ExecuteNonQueryList("RDD_LeaveRequest", parm);
                    rDD_LeaveRequest.Saveflag = outcls[0].Outtf;
                    rDD_LeaveRequest.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_LeaveRequest.ErrorMsg = ex.Message;
                rDD_LeaveRequest.Saveflag = false;
            }
            return rDD_LeaveRequest;
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
                            NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? Convert.ToInt32(dr["NoOfDays"].ToString()) : 0,
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
                    NoOfDays = 0,
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
                        rDD_LeaveRequest.NoOfDays = !string.IsNullOrWhiteSpace(dr["NoOfDays"].ToString()) ? Convert.ToInt32(dr["NoOfDays"].ToString()) : 0;
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
                rDD_LeaveRequest.NoOfDays = 0;
                rDD_LeaveRequest.LeaveStatus = "";
                rDD_LeaveRequest.ApproverRemarks = "";
                rDD_LeaveRequest.AttachmentUrl = "";

            }
            return rDD_LeaveRequest;
        }
    }
}


