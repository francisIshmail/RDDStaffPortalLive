using RDDStaffPortal.DAL.DataModels.Voucher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.Voucher
{
    public class RDD_PV_VoucherDbOperation
    {
        CommonFunction Com = new CommonFunction();


        public RDD_PV GetDropList(string username)
        {

            RDD_PV RPV = new RDD_PV();
            List<SelectListItem> CountryList = new List<SelectListItem>();
            List<SelectListItem> CurrencyList = new List<SelectListItem>();
            List<SelectListItem> DBNameList = new List<SelectListItem>();
            List<SelectListItem> PayMethodList = new List<SelectListItem>();
            List<SelectListItem> VtypList = new List<SelectListItem>();

            CountryList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            CurrencyList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            PayMethodList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            DBNameList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            VtypList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });


            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_LoggedInUser", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_PV_GetCountriesAndCurrencies", CommandType.StoredProcedure, parm);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;

                    try
                    {
                        dtModule = dsModules.Tables[0];
                        drc = dtModule.Rows;

                        foreach (DataRow dr in drc)
                        {
                            CountryList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {
                        CountryList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });

                    }
                    try
                    {

                        dtModule = dsModules.Tables[1];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            CurrencyList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["CurrCode"].ToString()) ? dr["CurrCode"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["CurrCode"].ToString()) ? dr["CurrCode"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        CurrencyList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }
                    try
                    {
                        dtModule = dsModules.Tables[2];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            DBNameList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        DBNameList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }

                    try
                    {
                        dtModule = dsModules.Tables[3];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            PayMethodList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["PaymentMethod"].ToString()) ? dr["PaymentMethod"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["PaymentMethod"].ToString()) ? dr["PaymentMethod"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        PayMethodList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }


                    try
                    {
                        dtModule = dsModules.Tables[4];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            VtypList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["VoucherStatus"].ToString()) ? dr["VoucherStatus"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["VoucherStatus"].ToString()) ? dr["VoucherStatus"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        VtypList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }
                    try
                    {
                        dtModule = dsModules.Tables[5];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            RPV.VType = !string.IsNullOrWhiteSpace(dr["VType"].ToString()) ? dr["VType"].ToString() : "";
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    try
                    {
                        dtModule = dsModules.Tables[6];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            RPV.RefNo = !string.IsNullOrWhiteSpace(dr[0].ToString()) ? dr[0].ToString() : "";
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

            }
            catch (Exception)
            {

                CurrencyList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                CountryList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                DBNameList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                PayMethodList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                VtypList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
            }





            RPV.CurrencyList = CurrencyList;
            RPV.CountryList = CountryList;
            RPV.DBNameList = DBNameList;
            RPV.PayMethList = PayMethodList;
            RPV.VTypeList = VtypList;

            return RPV;
        }
        public List<Outcls1> ChangeVoucherStatus(string VoucherStatus, int PVID)
        {
            int id = 0;
            string Erormsg = string.Empty;
            List<Outcls1> str = new List<Outcls1>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_PVId",PVID),
                        new SqlParameter("@p_DocStatus",VoucherStatus),
                        new SqlParameter("@p_type","Voucher"),
                        new SqlParameter("@p_id",id),
                        new SqlParameter("@p_response",Erormsg)
                };

                    str = Com.ExecuteNonQueryListID("RDD_PV_Insert_Update_Delete", Para);

                    scope.Complete();
                }
            }
            catch (Exception ex)
            {

                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return str;
        }

        public List<Outcls1> Save1(RDD_PV RPV)
        {
            List<Outcls1> str = new List<Outcls1>();
            RPV.RType = "Insert";
            if (RPV.EditFlag == true)
            {
                RPV.RType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_PVId",RPV.PVId),
                        new SqlParameter("@p_Country",RPV.Country),
                        new SqlParameter("@p_RefNo",RPV.RefNo),
                        new SqlParameter("@p_DocStatus",RPV.DocStatus),
                        new SqlParameter("@p_VType",RPV.VType),
                        new SqlParameter("@p_DBName",RPV.DBName),
                        new SqlParameter("@p_Currency",RPV.Currency),
                        new SqlParameter("@p_VendorCode",RPV.VendorCode),
                        new SqlParameter("@p_VendorEmployee",RPV.VendorEmployee),
                        new SqlParameter("@p_Benificiary",RPV.Benificiary),
                        new SqlParameter("@p_RequestedAmt",RPV.RequestedAmt),
                        new SqlParameter("@p_ApprovedAmt",RPV.ApprovedAmt),
                        new SqlParameter("@p_BeingPayOf",RPV.BeingPayOf),
                        new SqlParameter("@p_PayRequestDate",RPV.PayRequestDate),
                        new SqlParameter("@p_BankCode",RPV.BankCode),
                        new SqlParameter("@p_BankName",RPV.BankName),
                        new SqlParameter("@p_PayMethod",RPV.PayMethod),
                        new SqlParameter("@p_PayRefNo",RPV.PayRefNo),
                        new SqlParameter("@p_PayDate",RPV.PayDate),
                        new SqlParameter("@p_FilePath",RPV.FilePath),
                        new SqlParameter("@p_ClosedDate",RPV.ClosedDate),
                        new SqlParameter("@p_CAappStatus",RPV.CAappStatus),
                        new SqlParameter("@p_CAappRemarks",RPV.CAappRemarks),
                        new SqlParameter("@p_CAapprovedBy",RPV.CAapprovedBy),
                        new SqlParameter("@p_CAapprovedOn",RPV.CAapprovedOn),
                        new SqlParameter("@p_CMappStatus",RPV.CMappStatus),
                        new SqlParameter("@p_CMappRemarks",RPV.CMappRemarks),
                        new SqlParameter("@p_CMapprovedBy",RPV.CMapprovedBy),
                        new SqlParameter("@p_CMapprovedOn",RPV.CMapprovedOn),
                        new SqlParameter("@p_CFOappStatus",RPV.CFOappStatus),
                        new SqlParameter("@p_CFOappRemarks",RPV.CFOappRemarks),
                        new SqlParameter("@p_CFOapprovedBy",RPV.CFOapprovedBy),
                        new SqlParameter("@p_CFOapprovedOn",RPV.CFOapprovedOn),
                        new SqlParameter("@p_CreatedOn",RPV.CreatedOn),
                        new SqlParameter("@p_CreatedBy",RPV.CreatedBy),
                        new SqlParameter("@p_LastUpdatedOn",RPV.LastUpdatedOn),
                        new SqlParameter("@p_LastUpdatedBy",RPV.LastUpdatedBy),
                        new SqlParameter("@p_type",RPV.RType),
                        new SqlParameter("@p_id",RPV.id),
                        new SqlParameter("@p_response",RPV.Erormsg)



                };

                    str = Com.ExecuteNonQueryListID("RDD_PV_Insert_Update_Delete", Para);
                    if (str[0].Outtf == true)
                    {

                        int k = 0;
                        if (RPV.EditFlag == false)
                        {
                            RPV.Ptype = "I";
                        }
                        else
                        {
                            RPV.Ptype = "D";
                            SqlParameter[] ParaDet2 = { new SqlParameter("@p_PVId", str[0].Id),
                             new SqlParameter("@p_typ",RPV.Ptype)};
                            var det1 = Com.ExecuteNonQuery("RDD_PVLinesInsert_Update_Delete", ParaDet2);
                            if (det1 == true)
                            {
                                RPV.Ptype = "U";
                            }
                            else
                            {
                                RPV.Ptype = "";
                            }

                        }
                        while (RPV.RDD_PVLinesDetails.Count > k)
                        {
                            SqlParameter[] ParaDet1 = {
                                                 new SqlParameter("@p_PVLineId",RPV.RDD_PVLinesDetails[k].PVLineId),
                                            new SqlParameter("@p_PVId",str[0].Id),
                                            new SqlParameter("@p_LineRefNo",RPV.RDD_PVLinesDetails[k].LineRefNo),
                                            new SqlParameter("@p_Date",RPV.RDD_PVLinesDetails[k].Date),
                                            new SqlParameter("@p_Description",RPV.RDD_PVLinesDetails[k].Description),
                                            new SqlParameter("@p_Amount",RPV.RDD_PVLinesDetails[k].Amount),
                                            new SqlParameter("@p_Remarks",RPV.RDD_PVLinesDetails[k].Remarks),
                                            new SqlParameter("@p_FilePath",RPV.RDD_PVLinesDetails[k].FilePath),
                                            new SqlParameter("@p_CreatedOn",RPV.CreatedOn),
                                            new SqlParameter("@p_CreatedBy",RPV.CreatedBy),
                                            new SqlParameter("@p_LastUpdatedOn",RPV.LastUpdatedOn),
                                            new SqlParameter("@p_LastUpdatedBy",RPV.LastUpdatedBy),
                                            new SqlParameter("@p_typ",RPV.Ptype)

                            };

                            var det1 = Com.ExecuteNonQuery("RDD_PVLinesInsert_Update_Delete", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : All Row Mandatory Details "
                                });
                                return str;
                            }

                            k++;
                        }

                    }
                    scope.Complete();
                }

            }
            catch (Exception ex)
            {
                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });

            }
            return str;
        }

        public List<Outcls1> Delete1(int PVId)
        {
            List<Outcls1> str = new List<Outcls1>();
            string RType = "Delete1";
            string response = string.Empty;
            int id = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_PVId",PVId),
                        new SqlParameter("@p_type",RType),
                         new SqlParameter("@p_id",id),
                         new SqlParameter("@p_response",response),
                };
                    str = Com.ExecuteNonQueryListID("RDD_PV_Insert_Update_Delete", Para);
                    scope.Complete();

                }
            }
            catch (Exception ex)
            {

                str.Add(new Outcls1

                {
                    Outtf = false,
                    Id = id,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }


            return str;
        }

        public List<RDD_PV> GetALLDATA(string UserName, int pagesize, int pageno, string psearch, string SearchCon)
        {
            List<RDD_PV> _RPVList = new List<RDD_PV>();

            try
            {
                SqlParameter[] Para = {
                      new SqlParameter("@SearchCriteria",SearchCon),
                    new SqlParameter("@p_UserName",UserName),
                     new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "Country"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                        new SqlParameter("@p_type","GetAll"),

                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_PV_GET_DATA", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RPVList.Add(new RDD_PV()
                        {
                            IsDraft = !string.IsNullOrWhiteSpace(dr["IsDraft"].ToString()) ? Convert.ToBoolean(dr["IsDraft"].ToString()) : true,
                            ApprovalStatus = !string.IsNullOrWhiteSpace(dr["ApprovalStatus"].ToString()) ? dr["ApprovalStatus"].ToString() : "",
                            AprovedBy = !string.IsNullOrWhiteSpace(dr["AprovedBy"].ToString()) ? dr["AprovedBy"].ToString() : "",
                            PVId = !string.IsNullOrWhiteSpace(dr["PVId"].ToString()) ? Convert.ToInt32(dr["PVId"].ToString()) : 0,
                            Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            RefNo = !string.IsNullOrWhiteSpace(dr["RefNo"].ToString()) ? dr["RefNo"].ToString() : "",
                            DocStatus = !string.IsNullOrWhiteSpace(dr["DocStatus"].ToString()) ? dr["DocStatus"].ToString() : "",
                            VType = !string.IsNullOrWhiteSpace(dr["VType"].ToString()) ? dr["VType"].ToString() : "",
                            DBName = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                            Currency = !string.IsNullOrWhiteSpace(dr["Currency"].ToString()) ? dr["Currency"].ToString() : "",
                            VendorCode = !string.IsNullOrWhiteSpace(dr["VendorCode"].ToString()) ? dr["VendorCode"].ToString() : "",
                            VendorEmployee = !string.IsNullOrWhiteSpace(dr["VendorEmployee"].ToString()) ? dr["VendorEmployee"].ToString() : "",
                            Benificiary = !string.IsNullOrWhiteSpace(dr["Benificiary"].ToString()) ? dr["Benificiary"].ToString() : "",
                            RequestedAmt = !string.IsNullOrWhiteSpace(dr["RequestedAmt"].ToString()) ? Convert.ToDecimal(dr["RequestedAmt"].ToString()) : 0,
                            ApprovedAmt = !string.IsNullOrWhiteSpace(dr["ApprovedAmt"].ToString()) ? Convert.ToDecimal(dr["ApprovedAmt"].ToString()) : 0,
                            BeingPayOf = !string.IsNullOrWhiteSpace(dr["BeingPayOf"].ToString()) ? dr["BeingPayOf"].ToString() : "",
                            PayRequestDate = !string.IsNullOrWhiteSpace(dr["PayRequestDate"].ToString()) ? Convert.ToDateTime(dr["PayRequestDate"].ToString()) : System.DateTime.Now,
                            BankCode = !string.IsNullOrWhiteSpace(dr["BankCode"].ToString()) ? dr["BankCode"].ToString() : "",
                            BankName = !string.IsNullOrWhiteSpace(dr["BankName"].ToString()) ? dr["BankName"].ToString() : "",
                            PayMethod = !string.IsNullOrWhiteSpace(dr["PayMethod"].ToString()) ? dr["PayMethod"].ToString() : "",
                            PayRefNo = !string.IsNullOrWhiteSpace(dr["PayRefNo"].ToString()) ? dr["PayRefNo"].ToString() : "",
                            PayDate = !string.IsNullOrWhiteSpace(dr["PayDate"].ToString()) ? Convert.ToDateTime(dr["PayDate"].ToString()) : System.DateTime.Now,
                            FilePath = !string.IsNullOrWhiteSpace(dr["FilePath"].ToString()) ? dr["FilePath"].ToString() : "",
                            ClosedDate = !string.IsNullOrWhiteSpace(dr["ClosedDate"].ToString()) ? Convert.ToDateTime(dr["ClosedDate"].ToString()) : System.DateTime.Now,
                            
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt32(dr["TotalCount"].ToString()) : 0,
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt32(dr["RowNum"].ToString()) : 0,
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,

                        });
                    }
                }
            }
            catch (Exception)
            {
                _RPVList.Add(new RDD_PV()
                {
                    PVId = 0,
                    Country = "",
                    RefNo = "",
                    DocStatus = "",
                    VType = "",
                    DBName = "",
                    Currency = "",
                    VendorCode = "",
                    VendorEmployee = "",
                    Benificiary = "",
                    RequestedAmt = 0,
                    ApprovedAmt = 0,
                    BeingPayOf = "",
                    PayRequestDate = System.DateTime.Now,
                    BankCode = "",
                    BankName = "",
                    PayMethod = "",
                    PayRefNo = "",
                    PayDate = System.DateTime.Now,
                    FilePath = "",
                    ClosedDate = System.DateTime.Now,
                   
                });

            }

            return _RPVList;
        }

        public RDD_PV GetData(string UserName, int PVid, RDD_PV RPV)
        {


            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_PVId",PVid),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_PV_GET_DATA", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RPV.Doc_Object = !string.IsNullOrWhiteSpace(dr["Doc_Object"].ToString()) ? Convert.ToInt32(dr["Doc_Object"].ToString()) : 0;
                        RPV.AprovedBy = !string.IsNullOrWhiteSpace(dr["AprovedBy"].ToString()) ? dr["AprovedBy"].ToString() : "";
                        RPV.IsDraft = !string.IsNullOrWhiteSpace(dr["IsDraft"].ToString()) ? Convert.ToBoolean(dr["IsDraft"].ToString()) : true;
                        RPV.ApprovalStatus = !string.IsNullOrWhiteSpace(dr["ApprovalStatus"].ToString()) ? dr["ApprovalStatus"].ToString() : "";
                        RPV.PVId = !string.IsNullOrWhiteSpace(dr["PVId"].ToString()) ? Convert.ToInt32(dr["PVId"].ToString()) : 0;
                        RPV.Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "";
                        RPV.RefNo = !string.IsNullOrWhiteSpace(dr["RefNo"].ToString()) ? dr["RefNo"].ToString() : "";
                        RPV.DocStatus = !string.IsNullOrWhiteSpace(dr["DocStatus"].ToString()) ? dr["DocStatus"].ToString() : "";
                        RPV.VType = !string.IsNullOrWhiteSpace(dr["VType"].ToString()) ? dr["VType"].ToString() : "";
                        RPV.DBName = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "";
                        RPV.Currency = !string.IsNullOrWhiteSpace(dr["Currency"].ToString()) ? dr["Currency"].ToString() : "";
                        RPV.VendorCode = !string.IsNullOrWhiteSpace(dr["VendorCode"].ToString()) ? dr["VendorCode"].ToString() : "";
                        RPV.VendorEmployee = !string.IsNullOrWhiteSpace(dr["VendorEmployee"].ToString()) ? dr["VendorEmployee"].ToString() : "";
                        RPV.Benificiary = !string.IsNullOrWhiteSpace(dr["Benificiary"].ToString()) ? dr["Benificiary"].ToString() : "";
                        RPV.RequestedAmt = !string.IsNullOrWhiteSpace(dr["RequestedAmt"].ToString()) ? Convert.ToDecimal(dr["RequestedAmt"].ToString()) : 0;
                        RPV.ApprovedAmt = !string.IsNullOrWhiteSpace(dr["ApprovedAmt"].ToString()) ? Convert.ToDecimal(dr["ApprovedAmt"].ToString()) : 0;
                        RPV.BeingPayOf = !string.IsNullOrWhiteSpace(dr["BeingPayOf"].ToString()) ? dr["BeingPayOf"].ToString() : "";
                        RPV.PayRequestDate = !string.IsNullOrWhiteSpace(dr["PayRequestDate"].ToString()) ? Convert.ToDateTime(dr["PayRequestDate"].ToString()) : System.DateTime.Now;
                        RPV.BankCode = !string.IsNullOrWhiteSpace(dr["BankCode"].ToString()) ? dr["BankCode"].ToString() : "";
                        RPV.BankName = !string.IsNullOrWhiteSpace(dr["BankName"].ToString()) ? dr["BankName"].ToString() : "";
                        RPV.PayMethod = !string.IsNullOrWhiteSpace(dr["PayMethod"].ToString()) ? dr["PayMethod"].ToString() : "";
                        RPV.PayRefNo = !string.IsNullOrWhiteSpace(dr["PayRefNo"].ToString()) ? dr["PayRefNo"].ToString() : "";
                        RPV.PayDate = !string.IsNullOrWhiteSpace(dr["PayDate"].ToString()) ? Convert.ToDateTime(dr["PayDate"].ToString()) : System.DateTime.Now;
                        RPV.FilePath = !string.IsNullOrWhiteSpace(dr["FilePath"].ToString()) ? dr["FilePath"].ToString() : "";
                        RPV.ClosedDate = !string.IsNullOrWhiteSpace(dr["ClosedDate"].ToString()) ? Convert.ToDateTime(dr["ClosedDate"].ToString()) : System.DateTime.Now;
                        
                        RPV.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RPV.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                    }


                    DataTable dtModule1 = dsModules.Tables[1];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_PVLines> RDDLines = new List<RDD_PVLines>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDLines.Add(new RDD_PVLines
                        {
                            //,Date,Description,Amount,Remarks,FilePath
                            Description = !string.IsNullOrWhiteSpace(dr["Description"].ToString()) ? dr["Description"].ToString() : "",
                            PVId = !string.IsNullOrWhiteSpace(dr["LineRefNo"].ToString()) ? Convert.ToInt32(dr["LineRefNo"].ToString()) : 0,
                            Amount = !string.IsNullOrWhiteSpace(dr["Amount"].ToString()) ? Convert.ToDecimal(dr["Amount"].ToString()) : 0,
                            Remarks = !string.IsNullOrWhiteSpace(dr["Remarks"].ToString()) ? dr["Remarks"].ToString() : "",
                            FilePath = !string.IsNullOrWhiteSpace(dr["FilePath"].ToString()) ? dr["FilePath"].ToString() : "",
                            PVLineId = !string.IsNullOrWhiteSpace(dr["PVLineId"].ToString()) ? Convert.ToInt32(dr["PVLineId"].ToString()) : 0,
                            Date = !string.IsNullOrWhiteSpace(dr["Date"].ToString()) ? Convert.ToDateTime(dr["Date"].ToString()) : System.DateTime.Now,

                        });
                    }
                    RPV.RDD_PVLinesDetails = RDDLines;

                }
            }
            catch (Exception ex)
            {

                RPV.PVId = 0;
                RPV.Country = "";
                RPV.RefNo = "";
                RPV.DocStatus = "";
                RPV.VType = "";
                RPV.DBName = "";
                RPV.Currency = "";
                RPV.VendorCode = "";
                RPV.VendorEmployee = "";
                RPV.Benificiary = "";
                RPV.RequestedAmt = 0;
                RPV.ApprovedAmt = 0;
                RPV.BeingPayOf = "";
                RPV.PayRequestDate = System.DateTime.Now;
                RPV.BankCode = "";
                RPV.BankName = "";
                RPV.PayMethod = "";
                RPV.PayRefNo = "";
                RPV.PayDate = System.DateTime.Now;
                RPV.FilePath = "";
                RPV.ClosedDate = System.DateTime.Now;
                RPV.CAappStatus = "";
                RPV.CAappRemarks = "";
                RPV.CAapprovedBy = "";
                RPV.CAapprovedOn = System.DateTime.Now;
                RPV.CMappStatus = "";
                RPV.CMappRemarks = "";
                RPV.CMapprovedBy = "";
                RPV.CMapprovedOn = System.DateTime.Now;
                RPV.CFOappStatus = "";
                RPV.CFOappRemarks = "";
                RPV.CFOapprovedBy = "";
                RPV.CFOapprovedOn = System.DateTime.Now;
                RPV.CreatedBy = "";
                RPV.CreatedOn = System.DateTime.Now;

            }

            return RPV;


        }


        public DataSet GetVendor(string DBName, string Vtype)
        {
            DataSet ds = null;
            try
            {
                int v = 0;
                if (Vtype == "Vendor")
                {
                    v = 1;
                }
                SqlParameter[] Para = {

                    new SqlParameter("@p_DBName",DBName),
                    new SqlParameter("@p_IsVendor",v),
                };
                ds = Com.ExecuteDataSet("RDD_PV_GetVendors_Employees", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;
        }


        public DataSet GetBank(string DBName)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para = {

                    new SqlParameter("@DBName",DBName),

                };
                ds = Com.ExecuteDataSet("RDD_PV_GetBankLists", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;
        }

        public DataSet GetRefNo(string Country)
        {
            DataSet ds = null;
            try
            {

                ds = Com.ExecuteDataSet("select dbo.GetPVRefNo('" + Country + "')");
            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }
        public DataSet GetVendorAgeing(string DBName, string BP)
        {
            DataSet ds = null;
            try
            {

                SqlParameter[] Para = {

                    new SqlParameter("@DBName",DBName),
                    new SqlParameter("@BP",BP)

                };
                ds = Com.ExecuteDataSet("rddVendordue", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }
    }
}
