
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
    public class RDD_CreditLimit_DbOperation
    {
        CommonFunction Com = new CommonFunction();
        public DataSet GetUserList(string Country)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@p_CountryCode",Country)
                };
                ds = Com.ExecuteDataSet("RDD_BPStatus_GetSAPLoginUserList", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet GetCreditLimitDetails()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","view")
                };
                ds = Com.ExecuteDataSet("RDD_GetCountryList_TempCLLimit", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public DataSet GetCountryList()
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para =
                {
                    new SqlParameter("@Type","GetCountryList")
                };
                ds = Com.ExecuteDataSet("RDD_GetCountryList_TempCLLimit", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }
        public RDD_CreditLimit SaveCreditLimit(RDD_CreditLimit rDD_TempCreditLimit)
        {
            try
            {
                if (rDD_TempCreditLimit.Editflag == true)
                {
                    rDD_TempCreditLimit.ActionType = "Update";
                }
                else
                {
                    rDD_TempCreditLimit.ActionType = "Insert";
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    var catempcl = "0";
                    if (rDD_TempCreditLimit.CAtempCLLimit != null)
                    {
                        catempcl = rDD_TempCreditLimit.CAtempCLLimit;
                    }
                    var cmtempcl = "0";
                    if (rDD_TempCreditLimit.CMtempCLLimit != null)
                    {
                        cmtempcl = rDD_TempCreditLimit.CMtempCLLimit;
                    }
                    var crtempcl = "0";
                    if (rDD_TempCreditLimit.CRtempCLLimit != null)
                    {
                        crtempcl = rDD_TempCreditLimit.CRtempCLLimit;
                    }
                    var hoftempcl = "0";
                    if (rDD_TempCreditLimit.HOFtempCLLimit != null)
                    {
                        hoftempcl = rDD_TempCreditLimit.HOFtempCLLimit;
                    }
                    var CAusername = "";
                    if (rDD_TempCreditLimit.CAUserName != null)
                    {
                        CAusername = rDD_TempCreditLimit.CAUserName.TrimEnd(';');
                    }
                    var CMusername = "";
                    if (rDD_TempCreditLimit.CMUserName != null)
                    {
                        CMusername = rDD_TempCreditLimit.CMUserName.TrimEnd(';');
                    }
                    var CRusername = "";
                    if (rDD_TempCreditLimit.CRUserName != null)
                    {
                        CRusername = rDD_TempCreditLimit.CRUserName.TrimEnd(';');
                    }
                    var HOFusername = "";
                    if (rDD_TempCreditLimit.HOFUserName != null)
                    {
                        HOFusername = rDD_TempCreditLimit.HOFUserName.TrimEnd(';');
                    }
                    var HOISusername = "";
                    if (rDD_TempCreditLimit.HOISUserName != null)
                    {
                        HOISusername = rDD_TempCreditLimit.HOISUserName.TrimEnd(';');
                    }
                    var COOusername = "";
                    if (rDD_TempCreditLimit.COOUserName != null)
                    {
                        COOusername = rDD_TempCreditLimit.COOUserName.TrimEnd(';');
                    }
                    var CEOusername = "";
                    if (rDD_TempCreditLimit.CEOUserName != null)
                    {
                        CEOusername = rDD_TempCreditLimit.CEOUserName.TrimEnd(';');
                    }
                    SqlParameter[] parm = {
                        new SqlParameter("@id",rDD_TempCreditLimit.Id),
                        new SqlParameter("@Country",rDD_TempCreditLimit.Country),
                        new SqlParameter("@CAUserName",CAusername),
                        new SqlParameter("@CATempCLLimit",catempcl),
                        new SqlParameter("@CMUserName",CMusername),
                        new SqlParameter("@CMtempCLLimit",cmtempcl),
                        new SqlParameter("@CRUserName",CRusername),
                        new SqlParameter("@CRtempCLLimit",crtempcl),
                        new SqlParameter("@HOFUserName",HOFusername),
                        new SqlParameter("@HOFtempCLLimit",hoftempcl),
                        new SqlParameter("@HOISUserName",HOISusername),
                        new SqlParameter("@COOUserName",COOusername),
                        new SqlParameter("@CEOUserName",CEOusername),
                        new SqlParameter("@CreatedBy",rDD_TempCreditLimit.CreatedBy),
                        new SqlParameter("@LastUpdatedBy",rDD_TempCreditLimit.LastUpdatedBy),
                        new SqlParameter("@Type",rDD_TempCreditLimit.ActionType),
                        new SqlParameter("@p_id",rDD_TempCreditLimit.IDe),
                        new SqlParameter("@p_response",rDD_TempCreditLimit.ErrorMsg)
                    };

                    List<Outcls1> outcls = new List<Outcls1>();
                    outcls = Com.ExecuteNonQueryListID("RDD_Insert_Update_BpStatus_Sapusers", parm);
                    if (outcls[0].Id == -1)
                    {
                        outcls[0].Outtf = false;
                    }
                    rDD_TempCreditLimit.Saveflag = outcls[0].Outtf;
                    rDD_TempCreditLimit.ErrorMsg = outcls[0].Responsemsg;

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                rDD_TempCreditLimit.ErrorMsg = ex.Message;
                rDD_TempCreditLimit.Saveflag = false;
            }
            return rDD_TempCreditLimit;
        }
    }
}

