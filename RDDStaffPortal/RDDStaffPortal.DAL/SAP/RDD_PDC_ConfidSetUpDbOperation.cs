using RDDStaffPortal.DAL.DataModels.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.SAP
{
    public class RDD_PDC_ConfidSetUpDbOperation
    {
        CommonFunction Com = new CommonFunction();
        public DataSet GetUserList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetEmloyee")
                };
                ds = Com.ExecuteDataSet("RDD_PDCsetting_GetDetails", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetPDCSetUpInfo()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetPdcSeupDetails"),
                    
                };
                ds = Com.ExecuteDataSet("RDD_PDCsetting_GetDetails", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
       
        public RDD_PDCConfigSetUp SavePDC(RDD_PDCConfigSetUp rDD_PDC)
        {
            try
            {
                if (rDD_PDC.Editflag == true)
                {
                    rDD_PDC.ActionType = "Update";
                }
                else
                {
                    rDD_PDC.ActionType = "Insert";
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;


                    var AuthUsersForBankGuarantee = "";
                    if (rDD_PDC.AuthUsersForBankGuarantee != null)
                    {
                        AuthUsersForBankGuarantee = rDD_PDC.AuthUsersForBankGuarantee.TrimEnd(';');
                    }
                    var AuthUsersForSecurityCheque = "";
                    if (rDD_PDC.AuthUsersForSecurityCheque != null)
                    {
                        AuthUsersForSecurityCheque = rDD_PDC.AuthUsersForSecurityCheque.TrimEnd(';');
                    }

                    SqlParameter[] parm = {

                        new SqlParameter("@PDCConfigId",rDD_PDC.PDCConfigId),
                        new SqlParameter("@PDCMaxAllowableDays",rDD_PDC.PDCMaxAllowableDays),
                        new SqlParameter("@AllowSORforOSAmount",rDD_PDC.AllowSORforOSAmount),
                        new SqlParameter("@DaysToWaitForOriginalPDC",rDD_PDC.DaysToWaitForOriginalPDC),
                        new SqlParameter("@AuthUsersForBankGuarantee",AuthUsersForBankGuarantee),
                        new SqlParameter("@AuthUsersForSecurityCheque",AuthUsersForSecurityCheque),
                        new SqlParameter("@CreatedBy",rDD_PDC.CreatedBy),
                        new SqlParameter("@LastUpdatedBy",rDD_PDC.LastUpdatedBy),
                        new SqlParameter("@Type",rDD_PDC.ActionType),
                        new SqlParameter("@p_id",rDD_PDC.ID),
                        new SqlParameter("@p_response",rDD_PDC.ErrorMsg)
                    };

                    List<Outcls1> outcls = new List<Outcls1>();
                    outcls = Com.ExecuteNonQueryListID("RDD_Insert_Update_PDCConfig", parm);
                    if (outcls[0].Id == -1)
                    {
                        outcls[0].Outtf = false;
                    }
                    rDD_PDC.Saveflag = outcls[0].Outtf;
                    rDD_PDC.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_PDC.ErrorMsg = ex.Message;
                rDD_PDC.Saveflag = false;
            }
            return rDD_PDC;
        }
        public RDD_PDCConfigSetUp SaveReason(RDD_PDCConfigSetUp rDD_PDC)
        {
            try
            {
                if (rDD_PDC.Editflag == true)
                {
                    rDD_PDC.ActionType = "Update";
                }
                else
                {
                    rDD_PDC.ActionType = "Insert";
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    int m = 0;
                    List<Outcls1> outcls = new List<Outcls1>();
                    List<Outcls> outcls1 = new List<Outcls>();
                    
                    if (rDD_PDC.ActionType == "Update")
                    {
                        
                        SqlParameter[] parm = {
                            
                            new SqlParameter("@Type","Delete"),
                            new SqlParameter("@p_id",rDD_PDC.ID),
                            new SqlParameter("@p_response",rDD_PDC.ErrorMsg)
                        };
                        outcls1 = Com.ExecuteNonQueryList("RDD_Insert_Update_ChequeBounceReason", parm);
                        if (outcls1[0].Outtf == true)
                        {
                            while (m < rDD_PDC.ChequeBounceReasonList.Count)
                            {
                                SqlParameter[] parm11 = {
                            new SqlParameter("@Reason",rDD_PDC.ChequeBounceReasonList[m].Reason),
                            new SqlParameter("@BlockCustomerAccount",rDD_PDC.ChequeBounceReasonList[m].BlockCustomerAccount),
                            new SqlParameter("@CreatedBy",rDD_PDC.CreatedBy),
                            new SqlParameter("@LastUpdatedBy",rDD_PDC.LastUpdatedBy),
                            new SqlParameter("@Type","Insert"),
                            new SqlParameter("@p_id",rDD_PDC.ID),
                            new SqlParameter("@p_response",rDD_PDC.ErrorMsg)
                        };


                                outcls = Com.ExecuteNonQueryListID("RDD_Insert_Update_ChequeBounceReason", parm11);
                                if (outcls[0].Id == -1)
                                {
                                    outcls[0].Outtf = false;
                                }
                                m++;
                            }

                        }
                        
                    }
                    else
                    {
                        while (m < rDD_PDC.ChequeBounceReasonList.Count)
                        {
                            SqlParameter[] parm = {
                            new SqlParameter("@Reason",rDD_PDC.ChequeBounceReasonList[m].Reason),
                            new SqlParameter("@BlockCustomerAccount",rDD_PDC.ChequeBounceReasonList[m].BlockCustomerAccount),
                            new SqlParameter("@CreatedBy",rDD_PDC.CreatedBy),
                            new SqlParameter("@LastUpdatedBy",rDD_PDC.LastUpdatedBy),
                            new SqlParameter("@Type",rDD_PDC.ActionType),
                            new SqlParameter("@p_id",rDD_PDC.ID),
                            new SqlParameter("@p_response",rDD_PDC.ErrorMsg)
                        };


                            outcls = Com.ExecuteNonQueryListID("RDD_Insert_Update_ChequeBounceReason", parm);
                            if (outcls[0].Id == -1)
                            {
                                outcls[0].Outtf = false;
                            }
                            m++;
                        }
                    }
                    
                    rDD_PDC.Saveflag = outcls[0].Outtf;
                    rDD_PDC.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_PDC.ErrorMsg = ex.Message;
                rDD_PDC.Saveflag = false;
            }
            return rDD_PDC;
        }
    }
   

}
