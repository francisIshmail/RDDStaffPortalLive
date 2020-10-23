using RDDStaffPortal.DAL.DataModels.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.Admin
{
    public class RDD_Approval_TemplatesDBOperation
    {
        CommonFunction Com = new CommonFunction();


        public RDD_Approval_Templates GetDropList(string username, string Eflag)
        {
            RDD_Approval_Templates RDD_Approval = new RDD_Approval_Templates();
            List<SelectListItem> DocumentList = new List<SelectListItem>();
            DocumentList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_username", username),
                new SqlParameter("@p_flag", Eflag)};

                DataSet dsModules = Com.ExecuteDataSet("GetRDD_Approve_DocumentName", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;
                    dtModule = dsModules.Tables[0];
                    drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        DocumentList.Add(new SelectListItem()
                        {
                            Text = !string.IsNullOrWhiteSpace(dr["MenuName"].ToString()) ? dr["MenuName"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["ObjType"].ToString()) ? dr["ObjType"].ToString() : "",
                        });
                    }
                }

            }
            catch (Exception)
            {
                DocumentList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });

            }
            RDD_Approval.DocumentNameList = DocumentList;
            return RDD_Approval;
        }
        public List<Outcls1> Save1(RDD_Approval_Templates RDD_Approval)
        {
            List<Outcls1> str = new List<Outcls1>();
            RDD_Approval.RType = "Insert";
            if (RDD_Approval.EditFlag == true)
            {
                RDD_Approval.RType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                                    new SqlParameter("@p_Template_Id",RDD_Approval.Template_Id),
                                    new SqlParameter("@p_ObjType",RDD_Approval.ObjType),
                                    new SqlParameter("@p_DocumentName",RDD_Approval.DocumentName),
                                    new SqlParameter("@p_Description",RDD_Approval.Description),
                                    new SqlParameter("@p_Status",RDD_Approval.Status),
                                    new SqlParameter("@p_no_of_approvals",RDD_Approval.no_of_approvals),
                                    new SqlParameter("@p_Condition",RDD_Approval.Condition),
                                    new SqlParameter("@p_Condition_Text",RDD_Approval.Condition_Text),
                                    new SqlParameter("@p_CreatedOn",RDD_Approval.CreatedOn),
                                    new SqlParameter("@p_CreatedBy",RDD_Approval.CreatedBy),
                                    new SqlParameter("@p_LastUpdatedOn",RDD_Approval.LastUpdatedOn),
                                    new SqlParameter("@p_LastUpdatedBy",RDD_Approval.LastUpdatedBy),
                                    new SqlParameter("@p_type",RDD_Approval.RType),
                                    new SqlParameter("@p_id",RDD_Approval.id),
                                    new SqlParameter("@p_response",RDD_Approval.Erormsg)



                };

                    str = Com.ExecuteNonQueryListID("RDD_RDD_Approval_Templates_Insert_Update", Para);
                    if (str[0].Outtf == true)
                    {
                        int k = 0;
                        int m = 0;
                        if (RDD_Approval.EditFlag == false)
                        {
                            RDD_Approval.Ptype = "I";
                        }
                        else
                        {
                            RDD_Approval.Ptype = "D";
                            SqlParameter[] ParaDet2 = { new SqlParameter("@p_Template_Id", str[0].Id),
                             new SqlParameter("@p_typ",RDD_Approval.Ptype)};
                            var det1 = Com.ExecuteNonQuery("RDD_Approval_Approvers_Insert_Update_Delete", ParaDet2);

                            SqlParameter[] ParaDet3 = { new SqlParameter("@p_Template_Id", str[0].Id),
                             new SqlParameter("@p_typ",RDD_Approval.Ptype)};
                            var det2 = Com.ExecuteNonQuery("RDD_Approval_Originators_Insert_Update_Delete", ParaDet3);

                            if (det1 == true && det2 == true)
                            {
                                RDD_Approval.Ptype = "U";
                            }
                            else
                            {
                                RDD_Approval.Ptype = "";
                            }

                        }
                        while (RDD_Approval.RDD_Approval_ApproversList.Count > k)
                        {
                            SqlParameter[] ParaDet1 = {
                                            new SqlParameter("@p_Approver_Id",RDD_Approval.RDD_Approval_ApproversList[k].Approver_Id),
                                            new SqlParameter("@p_Template_Id",str[0].Id),
                                            new SqlParameter("@p_Approver",RDD_Approval.RDD_Approval_ApproversList[k].Approver),
                                            new SqlParameter("@p_Approval_Sequence",RDD_Approval.RDD_Approval_ApproversList[k].Approval_Sequence),
                                            new SqlParameter("@p_IsApproval_Mandatory",RDD_Approval.RDD_Approval_ApproversList[k].IsApproval_Mandatory),
                                            new SqlParameter("@p_CreatedOn",RDD_Approval.CreatedOn),
                                            new SqlParameter("@p_CreatedBy",RDD_Approval.CreatedBy),
                                            new SqlParameter("@p_LastUpdatedOn",RDD_Approval.LastUpdatedOn),
                                            new SqlParameter("@p_LastUpdatedBy",RDD_Approval.LastUpdatedBy),
                                            new SqlParameter("@p_typ",RDD_Approval.Ptype)

                            };
                            var det1 = Com.ExecuteNonQuery("RDD_Approval_Approvers_Insert_Update_Delete", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : Approvers Details "
                                });
                                return str;
                            }
                            k++;
                        }
                        while (RDD_Approval.RDD_Approval_OriginatorsList.Count > m)
                        {
                            SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@p_Originator_Id",RDD_Approval.RDD_Approval_OriginatorsList[m].Originator_Id),
                                                new SqlParameter("@p_Template_Id",str[0].Id),
                                                new SqlParameter("@p_Originator",RDD_Approval.RDD_Approval_OriginatorsList[m].Originator),
                                                new SqlParameter("@p_OriginatorName",RDD_Approval.RDD_Approval_OriginatorsList[m].OriginatorName),
                                                new SqlParameter("@p_CreatedOn",RDD_Approval.CreatedOn),
                                                new SqlParameter("@p_CreatedBy",RDD_Approval.CreatedBy),
                                                new SqlParameter("@p_LastUpdatedOn",RDD_Approval.LastUpdatedOn),
                                                new SqlParameter("@p_LastUpdatedBy",RDD_Approval.LastUpdatedBy),
                                                new SqlParameter("@p_typ",RDD_Approval.Ptype)
                            };
                            var det1 = Com.ExecuteNonQuery("RDD_Approval_Originators_Insert_Update_Delete", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : Originators Details "
                                });
                                return str;
                            }
                            m++;
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

        public List<RDD_Approval_Templates> GetALLDATA(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_Approval_Templates> _RDD_Approval = new List<RDD_Approval_Templates>();

            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                     new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "Template_Id"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                        new SqlParameter("@p_type","GetAll"),

                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Approval_Templates_GET_DATA", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_Approval.Add(new RDD_Approval_Templates()
                        {
                            Template_Id = !string.IsNullOrWhiteSpace(dr["Template_Id"].ToString()) ? Convert.ToInt32(dr["Template_Id"].ToString()) : 0,
                            ObjType = !string.IsNullOrWhiteSpace(dr["ObjType"].ToString()) ? dr["ObjType"].ToString() : "",
                            DocumentName = !string.IsNullOrWhiteSpace(dr["DocumentName"].ToString()) ? dr["DocumentName"].ToString() : "",
                            Description = !string.IsNullOrWhiteSpace(dr["Description"].ToString()) ? dr["Description"].ToString() : "",
                            Status = !string.IsNullOrWhiteSpace(dr["Status"].ToString()) ? Convert.ToBoolean(dr["Status"].ToString()) : false,
                            no_of_approvals = !string.IsNullOrWhiteSpace(dr["no_of_approvals"].ToString()) ? Convert.ToInt32(dr["no_of_approvals"].ToString()) : 0,
                            Condition = !string.IsNullOrWhiteSpace(dr["Condition"].ToString()) ? Convert.ToBoolean(dr["Condition"].ToString()) : false,
                            Condition_Text = !string.IsNullOrWhiteSpace(dr["Condition_Text"].ToString()) ? dr["Condition_Text"].ToString() : "",
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt32(dr["TotalCount"].ToString()) : 0,
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt32(dr["RowNum"].ToString()) : 0,

                        });
                    }
                }
            }
            catch (Exception)
            {
                _RDD_Approval.Add(new RDD_Approval_Templates()
                {
                    Template_Id = 0,
                    ObjType = "",
                    DocumentName = "",
                    Description = "",
                    Status = false,
                    no_of_approvals = 0,
                    Condition = false,
                    Condition_Text = "",
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = "",


                });
            }

            return _RDD_Approval;
        }

        public RDD_Approval_Templates GetData(string UserName, int Template_Id, RDD_Approval_Templates RDD_Approval)
        {


            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_Template_Id",Template_Id),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Approval_Templates_GET_DATA", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_Approval.Template_Id = !string.IsNullOrWhiteSpace(dr["Template_Id"].ToString()) ? Convert.ToInt32(dr["Template_Id"].ToString()) : 0;
                        RDD_Approval.ObjType = !string.IsNullOrWhiteSpace(dr["ObjType"].ToString()) ? dr["ObjType"].ToString() : "";
                        RDD_Approval.DocumentName = !string.IsNullOrWhiteSpace(dr["DocumentName"].ToString()) ? dr["DocumentName"].ToString() : "";
                        RDD_Approval.Description = !string.IsNullOrWhiteSpace(dr["Description"].ToString()) ? dr["Description"].ToString() : "";
                        RDD_Approval.Status = !string.IsNullOrWhiteSpace(dr["Status"].ToString()) ? Convert.ToBoolean(dr["Status"].ToString()) : false;
                        RDD_Approval.no_of_approvals = !string.IsNullOrWhiteSpace(dr["no_of_approvals"].ToString()) ? Convert.ToInt32(dr["no_of_approvals"].ToString()) : 0;
                        RDD_Approval.Condition = !string.IsNullOrWhiteSpace(dr["Condition"].ToString()) ? Convert.ToBoolean(dr["Condition"].ToString()) : false;
                        RDD_Approval.Condition_Text = !string.IsNullOrWhiteSpace(dr["Condition_Text"].ToString()) ? dr["Condition_Text"].ToString() : "";
                        RDD_Approval.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Approval.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RDD_Approval.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Approval.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
                    }


                    DataTable dtModule1 = dsModules.Tables[2];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_Approval_Approvers> RDDApprovers = new List<RDD_Approval_Approvers>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDApprovers.Add(new RDD_Approval_Approvers
                        {
                            Approver_Id = !string.IsNullOrWhiteSpace(dr["Approver_Id"].ToString()) ? Convert.ToInt32(dr["Approver_Id"].ToString()) : 0,
                            Template_Id = !string.IsNullOrWhiteSpace(dr["Template_Id"].ToString()) ? Convert.ToInt32(dr["Template_Id"].ToString()) : 0,
                            Approver = !string.IsNullOrWhiteSpace(dr["Approver"].ToString()) ? dr["Approver"].ToString() : "",
                            Approval_Sequence = !string.IsNullOrWhiteSpace(dr["Approval_Sequence"].ToString()) ? Convert.ToInt32(dr["Approval_Sequence"].ToString()) : 0,
                            IsApproval_Mandatory = !string.IsNullOrWhiteSpace(dr["IsApproval_Mandatory"].ToString()) ? Convert.ToBoolean(dr["IsApproval_Mandatory"].ToString()) : false,
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",

                        });
                    }
                    RDD_Approval.RDD_Approval_ApproversList = RDDApprovers;


                    DataTable dtModule2 = dsModules.Tables[1];
                    DataRowCollection drc2 = dtModule2.Rows;
                    List<RDD_Approval_Originators> RDDOriginators = new List<RDD_Approval_Originators>();
                    foreach (DataRow dr in drc2)
                    {
                        RDDOriginators.Add(new RDD_Approval_Originators
                        {
                            Originator_Id = !string.IsNullOrWhiteSpace(dr["Originator_Id"].ToString()) ? Convert.ToInt32(dr["Originator_Id"].ToString()) : 0,
                            Template_Id = !string.IsNullOrWhiteSpace(dr["Template_Id"].ToString()) ? Convert.ToInt32(dr["Template_Id"].ToString()) : 0,
                            Originator = !string.IsNullOrWhiteSpace(dr["Originator"].ToString()) ? dr["Originator"].ToString() : "",
                            OriginatorName = !string.IsNullOrWhiteSpace(dr["OriginatorName"].ToString()) ? dr["OriginatorName"].ToString() : "",
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",

                        });
                    }
                    RDD_Approval.RDD_Approval_OriginatorsList = RDDOriginators;

                }
            }
            catch (Exception ex)
            {

                RDD_Approval.Template_Id = 0;
                RDD_Approval.ObjType = "";
                RDD_Approval.DocumentName = "";
                RDD_Approval.Description = "";
                RDD_Approval.Status = false;
                RDD_Approval.no_of_approvals = 0;
                RDD_Approval.Condition = false;
                RDD_Approval.Condition_Text = "";
                RDD_Approval.CreatedOn = System.DateTime.Now;
                RDD_Approval.CreatedBy = "";
                RDD_Approval.LastUpdatedOn = System.DateTime.Now;
                RDD_Approval.LastUpdatedBy = "";
                RDD_Approval.RDD_Approval_OriginatorsList = null;
                RDD_Approval.RDD_Approval_ApproversList = null;


            }

            return RDD_Approval;


        }


        public DataSet GetApprovaldata(string Object_Type, string Originator, string DocKey)
        {
            DataSet ds;
            try
            {
                SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@Object_Type",Object_Type),
                                                new SqlParameter("@Originator",Originator),
                                                 new SqlParameter("@DocKey",DocKey),

                            };
                ds = Com.ExecuteDataSet("RDD_Check_DocumentApproval", CommandType.StoredProcedure, ParaDet1);

            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;

        }

        public DataSet RDD_Approver_Insert_Records(string Object_Type, string Originator, string DocKey, string OriginatorRemark)
        {
            DataSet ds;
            try
            {
                SqlParameter[] ParaDet1 = {
                                                new SqlParameter("@Object_Type",Object_Type),
                                                new SqlParameter("@Originator",Originator),
                                                new SqlParameter("@DocKey",DocKey),
                                                new SqlParameter("@OriginatorRemark",OriginatorRemark),
                            };
                ds = Com.ExecuteDataSet("RDD_Approver_Insert_Records", CommandType.StoredProcedure, ParaDet1);

            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;

        }

        public List<RDD_APPROVAL_DOC> Get_ApprovalDoc_List(string DBNAme, string ApproverName, int pagesize, int pageno, string psearch,string Objtype)
        {
            List<RDD_APPROVAL_DOC> _RDD_APPROVAL_DOC = new List<RDD_APPROVAL_DOC>();

            try
            {
                SqlParameter[] Para = {
                    
                  
                    new SqlParameter("@p_UserName",ApproverName),
                     new SqlParameter("@SearchCriteria ", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "Template_Id"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                        new SqlParameter("@p_type",Objtype),

                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Get_Document_Approval_List", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_APPROVAL_DOC.Add(new RDD_APPROVAL_DOC()
                        {
                            SRNO = !string.IsNullOrWhiteSpace(dr["SRNO"].ToString()) ? Convert.ToInt32(dr["SRNO"].ToString()) : 0,
                            OBJTYPE = !string.IsNullOrWhiteSpace(dr["OBJTYPE"].ToString()) ? Convert.ToInt32(dr["OBJTYPE"].ToString()) : 0,
                            DocumentName = !string.IsNullOrWhiteSpace(dr["DocType"].ToString()) ? dr["DocType"].ToString() : "",
                            DOC_ID = !string.IsNullOrWhiteSpace(dr["DocKey"].ToString()) ? Convert.ToInt32(dr["DocKey"].ToString()) : 0,
                            DOC_DATE = string.IsNullOrEmpty(dr["DocDate"].ToString())
                ? (DateTime?)null
                : (DateTime?)Convert.ToDateTime(dr["DocDate"].ToString()),
                            CARDNAME = !string.IsNullOrWhiteSpace(dr["CardName"].ToString()) ? dr["CardName"].ToString() : "",
                            DocTotal = !string.IsNullOrWhiteSpace(dr["DocTotal"].ToString()) ? Convert.ToDecimal(dr["DocTotal"].ToString()) : 0,
                            ORIGINATOR = !string.IsNullOrWhiteSpace(dr["ORIGINATOR"].ToString()) ? dr["ORIGINATOR"].ToString() : "",
                            ORG_Remark = !string.IsNullOrWhiteSpace(dr["Remark"].ToString()) ? dr["Remark"].ToString() : "",
                            APPROVER = !string.IsNullOrWhiteSpace(dr["APPROVER"].ToString()) ? dr["APPROVER"].ToString() : "",
                            APPROVAL_DECISION = !string.IsNullOrWhiteSpace(dr["APPROVAL_DECISION"].ToString()) ? dr["APPROVAL_DECISION"].ToString() : "",
                            APPROVAL_DATE = string.IsNullOrEmpty(dr["APPROVAL_DATE"].ToString())
                ? (DateTime?)null
                : (DateTime?)Convert.ToDateTime(dr["APPROVAL_DATE"].ToString()),
                           // dr["APPROVAL_DATE"].ToString()//!string.IsNullOrEmpty(dr["APPROVAL_DATE"].ToString()) ? Convert.ToDateTime(dr["APPROVAL_DATE"].ToString()) : null,


                        });
                    }
                }
            }
            catch (Exception)
            {
                _RDD_APPROVAL_DOC.Add(new RDD_APPROVAL_DOC()
                {
                    OBJTYPE = 0,
                    DocumentName = "",
                    DOC_ID = 0,
                    DOC_DATE = null,
                    CARDNAME = "",
                    DocTotal = 0,
                    ORIGINATOR = "",
                    ORG_Remark = "",
                    APPROVER = "",
                    APPROVAL_DECISION = "",
                    APPROVAL_DATE = null

                });
            }

            return _RDD_APPROVAL_DOC;
        }

        public DataSet Get_Doc_ApproverList(string ObjectType, string DocKey,string LoginUser)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Execute RDD_Doc_Approver_List @ObjectType=" + ObjectType + ",@DocKey=" + DocKey + ",@LoginUser='"+ LoginUser+"'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //"RDD_Approval_Controler_Fill"

        public DataSet GetFillRadioButton()
        {
            DataSet ds = null;
            try
            {               
                SqlParameter[] Para = { };
                ds = Com.ExecuteDataSet("RDD_Approval_Controler_Fill", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }

        public DataSet Get_Doc_ApproverAction(string ID, string Template_ID, string ObjectType, string DocKey, string Approver, string Action, string Remark, DateTime ApprovalDate)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Execute RDD_Doc_Approver_Action @ID=" + ID + ",@Template_ID=" + Template_ID + ",@ObjectType=" + ObjectType + ",@DocKey=" + DocKey + ",@Approver='" + Approver + "',@ApproverAction='" + Action + "',@ApproverRemark='" + Remark + "', @ApprovedDate='" + ApprovalDate + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
