using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.IO;



public partial class IntranetNew_MarketingPlan_MarketingPlan_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)  // IsPostBack Start
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int PLID = Convert.ToInt32(Request.QueryString["GVid"]);
            string LoggedInUserName = myGlobal.loggedInUser();

            DataSet ds2 = Db.myGetDS("exec GetUserAuthorizationForMPlan '" + LoggedInUserName + "'");
            string App = ds2.Tables[0].Rows[0]["Column1"].ToString();

            if (App == "Approver") //APPROVER START
            {
                int count = Convert.ToInt32(Request.QueryString["GVid"]);
                if (count > 0)
                {
                    BtnSave.Text = "Update";
                    ddlCountry.Enabled = false;
                    ddlsourcefd.Enabled = false;
                    ddlBU.Enabled = false;
                    txtrefno.Enabled = false;
                    ddlappstatus.Enabled = false;
                    txtapprmk.Enabled = false;
                    txtrefno.Enabled = false;
                    BindDDL();


                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    DataSet ds = Db.myGetDS("select * from MarketingPlan where  PlanId=" + Request.QueryString["GVid"]+"");


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlsourcefd.Text = ds.Tables[0].Rows[0]["SourceOfFund"].ToString();
                        txtrefno.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();


                        if (ds.Tables[0].Rows[0]["planStatus"].ToString() == "Open")
                        {
                            if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Rejected")
                            {
                                ddlplanstatus.SelectedItem.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                                ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();

                                ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                                txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                                txtappamount.Enabled = false;
                                txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                                txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();
                                txtrddappamt.Enabled = true;
                                ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                                ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();

                                txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                                txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                                lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                                txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                                txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                                txtdesc.Enabled = false;
                                txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                                txtrddBalAmt.Enabled = false;
                                txtbalfromapp.Enabled = false;
                                ddlappstatus.Enabled = false;
                                txtapprmk.Enabled = false;
                                ddlplanstatus.Enabled = false;
                                txtstartdate.Enabled = false;
                                txtEndDate.Enabled = false;
                                btnaddow.Visible = false;
                                BtnSave.Visible = false;
                                BtnCancel.Visible = true;
                                txtrddappamt.Enabled = false;
                                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                                DataSet DSMPanLines = Db.myGetDS("select L.Status1,L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.ApproverRemark,L.Status from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + Request.QueryString["GVid"]);
                                if (DSMPanLines.Tables.Count > 0)
                                {
                                    GvPlan.DataSource = DSMPanLines.Tables[0];
                                    GvPlan.DataBind();
                                    ViewState["CurrentTable"] = DSMPanLines.Tables[0];
                                    foreach (GridViewRow gvr in GvPlan.Rows)
                                    {
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;

                                        ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
                                    }
                                }
                            }
                            else if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() != "Rejected" )
                            {
                               

                                ddlplanstatus.SelectedItem.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                                ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();

                                ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                                txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                                txtappamount.Enabled = false;
                                txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                                txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();
                               
                                txtrddappamt.Enabled = true;
                                ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                                ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();

                                txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                                txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                                lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                                txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                                txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                                txtdesc.Enabled = false;
                                txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                                txtrddBalAmt.Enabled = false;
                                txtbalfromapp.Enabled = false;
                                ddlappstatus.Enabled = true;
                                txtapprmk.Enabled = true;
                                ddlplanstatus.Enabled = false;
                                txtstartdate.Enabled = false;
                                txtEndDate.Enabled = false;
                                btnaddow.Visible = false;
                                BtnSave.Visible = true;
                                BtnCancel.Visible = true;

                                if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Approved")
                                {
                                    ddlappstatus.Enabled = false;
                                    txtrddappamt.Enabled = false;
                                    txtapprmk.Enabled = false;
                                }
                                else
                                {
                                    ddlappstatus.Enabled = true ;
                                    txtrddappamt.Enabled = true;
                                    txtapprmk.Enabled = true;

                                }
                               
                              Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                                DataSet DSMPanLines = Db.myGetDS("select L.Status1,L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.ApproverRemark,L.Status from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + Request.QueryString["GVid"]);
                                if (DSMPanLines.Tables.Count > 0)
                                {
                                    GvPlan.DataSource = DSMPanLines.Tables[0];
                                    GvPlan.DataBind();
                                    ViewState["CurrentTable"] = DSMPanLines.Tables[0];
                                    foreach (GridViewRow gvr in GvPlan.Rows)
                                    {
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;
                                        string status = (gvr.FindControl("ddlstatus") as DropDownList).Text;
                                        if (status == "Pending")
                                        {
                                            ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = true;
                                        }
                                        else
                                        {
                                            ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                                        }
                                        //((DropDownList)gvr.FindControl("ddlstatus")).Enabled = true;
                                        ((TextBox)gvr.FindControl("txtAppremark")).Enabled = true;
                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
                                    }
                                }
                            }
                           
                            else
                            {
                                ddlplanstatus.SelectedItem.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                                ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();

                                ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                                txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                                txtappamount.Enabled = false;
                                txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                                txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();
                                txtrddappamt.Enabled = true;
                                ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                                ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();

                                txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                                txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                                lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                                txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                                txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                                txtdesc.Enabled = false;
                                txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                                txtrddBalAmt.Enabled = false;
                                txtbalfromapp.Enabled = false;
                                ddlappstatus.Enabled = true;
                                txtapprmk.Enabled = true;
                                ddlplanstatus.Enabled = false;
                                txtstartdate.Enabled = false;
                                txtEndDate.Enabled = false;
                                btnaddow.Visible = false;

                                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                                DataSet DSMPanLines = Db.myGetDS("select L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.ApproverRemark,L.Status from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + Request.QueryString["GVid"]);
                                if (DSMPanLines.Tables.Count > 0)
                                {
                                    GvPlan.DataSource = DSMPanLines.Tables[0];
                                    GvPlan.DataBind();
                                    ViewState["CurrentTable"] = DSMPanLines.Tables[0];
                                    foreach (GridViewRow gvr in GvPlan.Rows)
                                    {
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;

                                        ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = true;
                                        ((TextBox)gvr.FindControl("txtAppremark")).Enabled = true;
                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
                                    }
                                }
                                // }
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["planStatus"].ToString() == "Closed Unpaid" || ds.Tables[0].Rows[0]["planStatus"].ToString() == "Closed Paid")
                        {
                            ddlplanstatus.SelectedItem.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                            ddlplanstatus.Enabled = false;
                            ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();
                            ddlBU.Enabled = false;
                            ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                            ddlappstatus.Enabled = false;
                            txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                            txtappamount.Enabled = false;
                            txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                            txtstartdate.Enabled = false;
                            txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();
                            txtrddappamt.Enabled = false;
                            ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                            ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                            ddlCountry.Enabled = false;
                            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                            txtEndDate.Enabled = false;
                            txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                            txtrddBalAmt.Enabled = false;
                            lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                            lbltodaydate.Enabled = false;
                            txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                            txtbalfromapp.Enabled = false;
                            txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                            txtdesc.Enabled = false;
                            txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                            txtapprmk.Enabled = false;
                            BtnSave.Visible = false;
                            btnaddow.Visible = false;
                            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                            DataSet DSMPanLines1 = Db.myGetDS("select L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.ApproverRemark,L.Status  from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + PLID);
                            if (DSMPanLines1.Tables.Count > 0)
                            {

                                GvPlan.DataSource = DSMPanLines1.Tables[0];
                                GvPlan.DataBind();


                                ViewState["CurrentTable"] = DSMPanLines1.Tables[0];
                                foreach (GridViewRow gvr in GvPlan.Rows)
                                {
                                    ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                    ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;

                                    ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                                    ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                    ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
                                }
                            }

                        }
                       
                    }
                }
            }  
                              
                //APPROVER END
            else   //// UPDATE MODE START
            {

                int count = Convert.ToInt32(Request.QueryString["GVid"]);
                if (count > 0)
                {
                    BtnSave.Text = "Update";
                    ddlCountry.Enabled = false;
                    ddlsourcefd.Enabled = false;
                    ddlBU.Enabled = false;
                    txtrefno.Enabled = false;
                    ddlappstatus.Enabled = false;
                    txtapprmk.Enabled = false;
                    txtrefno.Enabled = false;
                    BindDDL();

                    
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    DataSet ds = Db.myGetDS("select * from MarketingPlan where PlanId=" + Request.QueryString["GVid"]);


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlsourcefd.Text = ds.Tables[0].Rows[0]["SourceOfFund"].ToString();
                        txtrefno.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();

                        if (ds.Tables[0].Rows[0]["planStatus"].ToString() == "Draft")
                        {
                            ddlplanstatus.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                            ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();

                            ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                            txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                            txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                            txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();

                            ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                            ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();

                            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                            txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                            lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                            txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                            txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                            txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                            txtrddBalAmt.Enabled = false;
                            txtbalfromapp.Enabled = false;
                            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                            DataSet DSMPanLines = Db.myGetDS("select L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.Status,L.ApproverRemark  from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + Request.QueryString["GVid"]);
                            if (DSMPanLines.Tables.Count > 0)
                            {
                                GvPlan.DataSource = DSMPanLines.Tables[0];
                                GvPlan.DataBind();
                                ViewState["CurrentTable"] = DSMPanLines.Tables[0];

                            }

                        }
                        else if (ds.Tables[0].Rows[0]["planStatus"].ToString() == "Open" || ds.Tables[0].Rows[0]["planStatus"].ToString() == "On Hold")
                        {

                            #region "Old code to enable & desable"

                            ddlplanstatus.SelectedItem.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                            ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();

                            string plsstatus = ddlplanstatus.SelectedItem.Text;
                            

                            ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                            txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                            txtappamount.Enabled = false;
                            txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                            txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();
                            txtrddappamt.Enabled = true;
                            ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                            ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();

                            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                            txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                            lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                            txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                            txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                            txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                            
                            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                            DataSet DSMPanLines = Db.myGetDS("select L.Status1,L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.ApproverRemark,L.Status from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + Request.QueryString["GVid"]);
                            if (DSMPanLines.Tables.Count > 0)
                            {
                                GvPlan.DataSource = DSMPanLines.Tables[0];
                                GvPlan.DataBind();
                                ViewState["CurrentTable"] = DSMPanLines.Tables[0];
                            }
                            foreach (GridViewRow gvr in GvPlan.Rows)
                            {
                                  DropDownList ddlstatus1 = ((DropDownList)gvr.FindControl("ddlstatus1"));
                                  if (ddlstatus1.SelectedItem.Text == "Closed")
                                  {

                                      ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                      ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                      ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                      ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                      ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                      ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                      ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                      ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;
                                      ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
                                  }

                            }
                            if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Approved")
                            {
                                txtdesc.Enabled = false;
                                txtrddBalAmt.Enabled = false;
                                txtbalfromapp.Enabled = false;
                                ddlappstatus.Enabled = false;
                                txtapprmk.Enabled = false;
                                ddlplanstatus.Enabled = true;
                                txtstartdate.Enabled = false;
                                txtEndDate.Enabled = false;
                                btnaddow.Visible = true;
                                BtnSave.Visible = true;
                                BtnCancel.Visible = true;
                                txtrddappamt.Enabled = false;

                                txtappamount.Enabled = false;
                                txtrddappamt.Enabled = false;


                              
                                foreach (GridViewRow gvr in GvPlan.Rows)
                                {
                                    /*CHANGES DONE ON 15-10-19 */
                                   
                                    DropDownList ddlstatus1 = ((DropDownList)gvr.FindControl("ddlstatus1"));
                                    if (ddlstatus1.SelectedItem.Text == "Closed")
                                    {

                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;
                                    }
                                    else
                                    {
                                        ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = true;
                                    
                                    }

                                    /*end*/

                                    DropDownList ddlstatus = ((DropDownList)gvr.FindControl("ddlstatus"));

                                    if (ddlstatus1.SelectedItem.Text == "Closed")
                                   ///// if (ddlstatus.SelectedItem.Text == "Approved" || ddlstatus.SelectedItem.Text == "Rejected")
                                    {
                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;
                                    }
                                    else
                                    {
                                        ((Button)gvr.FindControl("btnDel")).Enabled = true;
                                    }
                                    ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                                    ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                                }

                            }
                            /////////////////////////////////////////////////

                            else if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Rejected")
                            {
                                txtdesc.Enabled = false;
                                txtrddBalAmt.Enabled = false;
                                txtbalfromapp.Enabled = false;
                                ddlappstatus.Enabled = false;
                                txtapprmk.Enabled = false;
                                ddlplanstatus.Enabled = false;
                                txtstartdate.Enabled = false;
                                txtEndDate.Enabled = false;
                                btnaddow.Visible = false;
                                BtnSave.Visible = false;
                                BtnCancel.Visible = true;
                                txtrddappamt.Enabled = false;

                                foreach (GridViewRow gvr in GvPlan.Rows)
                                {
                                    DropDownList ddlstatus = ((DropDownList)gvr.FindControl("ddlstatus"));
                                    if (ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Rejected" || ds.Tables[0].Rows[0]["ApprovalStatus"].ToString() == "Approved")
                                    //if (ddlstatus.SelectedItem.Text == "Approved" || ddlstatus.SelectedItem.Text == "Rejected")
                                    {
                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;

                                        ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                                    }
                                    else
                                    {
                                        ((Button)gvr.FindControl("btnDel")).Enabled = true;
                                    }

                                }

                            }
                            else
                            {
                                // var draftname = ddlplanstatus.Items[0];
                                // ddlplanstatus.Enabled = false;
                                // draftname.Enabled = false;
                                txtstartdate.Enabled = false;
                                txtEndDate.Enabled = false;
                                txtrddBalAmt.Enabled = false;
                                txtbalfromapp.Enabled = false;
                                txtrddappamt.Enabled = false;


                                if (ds.Tables[0].Rows[0]["planStatus"].ToString() == "On Hold")
                                {
                                    GvPlan.Enabled = false;
                                }
                                else
                                {

                                    GvPlan.Enabled = true;



                                }




                            }

                       
                            #endregion




                        }
                        else if (ds.Tables[0].Rows[0]["planStatus"].ToString() == "Closed Unpaid" || ds.Tables[0].Rows[0]["planStatus"].ToString() == "Closed Paid")
                            {
                                ddlplanstatus.SelectedItem.Text = ds.Tables[0].Rows[0]["planStatus"].ToString();
                                ddlplanstatus.Enabled = false;
                                ddlBU.SelectedItem.Text = ds.Tables[0].Rows[0]["Vendor"].ToString();
                                ddlBU.Enabled = false;
                                ddlappstatus.Text = ds.Tables[0].Rows[0]["ApprovalStatus"].ToString();
                                ddlappstatus.Enabled = false;
                                txtappamount.Text = ds.Tables[0].Rows[0]["VendorApprovedAmt"].ToString();
                                txtappamount.Enabled = false;
                                txtstartdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy");
                                txtstartdate.Enabled = false;
                                txtrddappamt.Text = ds.Tables[0].Rows[0]["RDDApprovedAmt"].ToString();
                                txtrddappamt.Enabled = false;
                                ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                                ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();
                                ddlCountry.Enabled = false;
                                txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EndDate"]).ToString("MM/dd/yyyy");
                                txtEndDate.Enabled = false;
                                txtrddBalAmt.Text = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                                txtrddBalAmt.Enabled = false;
                                lbltodaydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                                lbltodaydate.Enabled = false;
                                txtbalfromapp.Text = ds.Tables[0].Rows[0]["BalanceFromApp"].ToString();
                                txtbalfromapp.Enabled = false;
                                txtdesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                                txtdesc.Enabled = false;
                                txtapprmk.Text = ds.Tables[0].Rows[0]["ApproverRemark"].ToString();
                                txtapprmk.Enabled = false;
                                BtnSave.Visible = false;
                                btnaddow.Visible = false;
                                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                                DataSet DSMPanLines1 = Db.myGetDS("select L.PlanLineId,L.VenderPoNo,L.LineRefNo,L.SAPPONo,L.ActivityDate,L.Description,L.Vendor,L.Amount,L.CountryName,L.Country,L.ApproverRemark,L.Status  from MarketingPlanLines L,MarketingPlan P where L.PlanId=P.PlanId and ISNULL(L.Flag, 0) = 0 and P.PlanId=" + PLID);
                                if (DSMPanLines1.Tables.Count > 0)
                                {

                                    GvPlan.DataSource = DSMPanLines1.Tables[0];
                                    GvPlan.DataBind();


                                    ViewState["CurrentTable"] = DSMPanLines1.Tables[0];
                                    foreach (GridViewRow gvr in GvPlan.Rows)
                                    {
                                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;

                                        ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                                        ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                                    }
                                }

                            }
                        }
                    }

                    
                
                else //ORIGINATOR
                {


                    lbltodaydate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    txtapprmk.Enabled = false;
                    ddlappstatus.Enabled = false;
                    txtrddBalAmt.Enabled = false;
                    txtbalfromapp.Enabled = false;
                    txtapprmk.Enabled = false;
                    txtrefno.Enabled = false;
                    txtbalfromapp.Enabled = false;
                    txtrddBalAmt.Enabled = false;

                    // Plan status - Bind ddl values ( OPEN /DRAFT /....)

                    BindDDL();

                }
            }
        }  //IsPostBackEnd
    }  
    
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtrddappamt.Text == string.Empty)
            //{
            //   // lblMsg.Text = "RDD Approved Amount Should be greater than 0";
            //    lblMsg.Visible = true;
            //}
            if (txtrefno.Text == string.Empty)
            {
                lblMsg.Text = "Please Select Country";
            }

            else
            {
                AddNewRowToGrid();
            }
        }

        catch
        {

        }

    }

    private void AddNewRowToGrid()
    {

        // string query = "select BUName from [dbo].[GetMarketingRefNo] where  country like '" + ddlCountry.SelectedItem.Text + "'";

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        int count = Convert.ToInt32(Request.QueryString["GVid"]);
        if (count == 0)
        {
            string refno1 = txtrefno.Text;
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values 
                        TextBox box1 = (TextBox)GvPlan.Rows[rowIndex].Cells[1].FindControl("txtpono");
                        TextBox box2 = (TextBox)GvPlan.Rows[rowIndex].Cells[2].FindControl("txtsappono");
                        TextBox box3 = (TextBox)GvPlan.Rows[rowIndex].Cells[3].FindControl("txtgvdate");
                        TextBox box4 = (TextBox)GvPlan.Rows[rowIndex].Cells[4].FindControl("txtgvdesc");
                        TextBox box5 = (TextBox)GvPlan.Rows[rowIndex].Cells[5].FindControl("txtgvvendor");
                        TextBox box6 = (TextBox)GvPlan.Rows[rowIndex].Cells[6].FindControl("txtgvamt");
                        DropDownList box7 = (DropDownList)GvPlan.Rows[rowIndex].Cells[7].FindControl("ddlgvcountry");
                        Label box8 = (Label)GvPlan.Rows[rowIndex].Cells[8].FindControl("lblplanlineid");

                        TextBox box9 = (TextBox)GvPlan.Rows[rowIndex].Cells[9].FindControl("txtAppremark");
                        DropDownList box10 = (DropDownList)GvPlan.Rows[rowIndex].Cells[10].FindControl("ddlstatus1");
               
                        drCurrentRow = dtCurrentTable.NewRow();
                        // drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["PlanLineId"] = box8.Text;
                        drCurrentRow["LineRefNo"] = refno1 + "-" + (i + 1);
                        dtCurrentTable.Rows[i - 1]["VenderPoNo"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["SAPPONo"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["ActivityDate"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Description"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Vendor"] = box5.Text;
                        dtCurrentTable.Rows[i - 1]["Amount"] = box6.Text;
                        dtCurrentTable.Rows[i - 1]["Country"] = box7.Text;
                        dtCurrentTable.Rows[i - 1]["ApproverRemark"] = box9.Text;
                       dtCurrentTable.Rows[i - 1]["Status1"] = box10.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    GvPlan.DataSource = dtCurrentTable;
                    GvPlan.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks  
            SetPreviousData();
            //SetInitialRow();
        }
        else
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                string refno = txtrefno.Text;
                DataTable dtCurrentTable = new DataTable(); //(DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                //foreach()

                dtCurrentTable.Columns.Add(new DataColumn("PlanLineId", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("VenderPoNo", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("SAPPONo", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("ActivityDate", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Description", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Vendor", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Amount", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Country", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("LineRefNo", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("CountryName", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("Status", typeof(string)));
                dtCurrentTable.Columns.Add(new DataColumn("ApproverRemark", typeof(string)));

               dtCurrentTable.Columns.Add(new DataColumn("Status1", typeof(string)));


                int MaxSerialNo = 0;

                for (int i = 0; i < GvPlan.Rows.Count; i++)
                {

                    GridViewRow row = GvPlan.Rows[i];

                    TextBox txtpono = (TextBox)row.FindControl("txtpono");
                    TextBox txtsappono = (TextBox)row.FindControl("txtsappono");
                    TextBox txtgvdate = (TextBox)row.FindControl("txtgvdate");
                    TextBox txtgvdesc = (TextBox)row.FindControl("txtgvdesc");
                    TextBox txtgvvendor = (TextBox)row.FindControl("txtgvvendor");
                    TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                    DropDownList ddlgvcountry = (DropDownList)row.FindControl("ddlgvcountry");
                    Label lblplanlineid = (Label)row.FindControl("lblplanlineid");
                    Label lblKey = (Label)row.FindControl("lblKey");
                    DropDownList ddlstatus = (DropDownList)row.FindControl("ddlstatus");
                    TextBox txtapprmk = (TextBox)row.FindControl("txtAppremark");

                   DropDownList ddlstatus1 = (DropDownList)row.FindControl("ddlstatus1");
                  


                   
                    if (lblplanlineid.Text.Trim() != "0" || !string.IsNullOrEmpty(txtgvdesc.Text.Trim()))
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["PlanLineId"] = lblplanlineid.Text;
                        drCurrentRow["VenderPoNo"] = txtpono.Text;
                        drCurrentRow["SAPPONo"] = txtsappono.Text;
                        drCurrentRow["ActivityDate"] = txtgvdate.Text;
                        drCurrentRow["Description"] = txtgvdesc.Text;
                        drCurrentRow["Vendor"] = txtgvvendor.Text;
                        drCurrentRow["Amount"] = txtgvamt.Text;
                        drCurrentRow["Country"] = ddlgvcountry.SelectedItem.Text;
                        drCurrentRow["LineRefNo"] = lblKey.Text;
                        drCurrentRow["CountryName"] = ddlgvcountry.SelectedItem.Text;
                        drCurrentRow["Status"] = ddlstatus.SelectedItem.Text;
                        drCurrentRow["ApproverRemark"] = txtapprmk.Text;
                        drCurrentRow["Status1"] = ddlstatus1.SelectedItem.Text;
                       
                        dtCurrentTable.Rows.Add(drCurrentRow);

                        try
                        {
                            if (!string.IsNullOrEmpty(lblKey.Text))
                            {
                                string[] str = lblKey.Text.Split('-');
                                int num = 0;
                                bool isNumeric = int.TryParse(str[1], out num);
                                if (num > 0)
                                {
                                    if (num > MaxSerialNo)
                                    {
                                        MaxSerialNo = num;
                                    }
                                }


                            }

                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }


                drCurrentRow = dtCurrentTable.NewRow();
                // dr["RowNumber"] = 1;
                drCurrentRow["PlanLineId"] = "0";
                drCurrentRow["VenderPoNo"] = string.Empty;
                drCurrentRow["SAPPONo"] = string.Empty;
                // drCurrentRow["ActivityDate"] = string.Empty;
                drCurrentRow["Description"] = string.Empty;
                drCurrentRow["Vendor"] = string.Empty;
                drCurrentRow["Amount"] = "0";
                drCurrentRow["Country"] = string.Empty;
                drCurrentRow["LineRefNo"] = refno + "-" + (MaxSerialNo + 1).ToString();
                drCurrentRow["ApproverRemark"] = string.Empty;
                drCurrentRow["Status1"] = "Planned";
                dtCurrentTable.Rows.Add(drCurrentRow);

                GvPlan.DataSource = dtCurrentTable;
                GvPlan.DataBind();



                foreach (GridViewRow gvr in GvPlan.Rows)
                {
                    DropDownList ddlstatus1 = (DropDownList)gvr.FindControl("ddlstatus1");
                    if (ddlstatus1.SelectedItem.Text == "Closed")
                   // DropDownList ddlstatus = (DropDownList)gvr.FindControl("ddlstatus");
                    //if (ddlstatus.SelectedItem.Text == "Approved" || ddlstatus.SelectedItem.Text == "Rejected")
                    {
                        ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                        ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                        ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                        ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                        ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                        ((TextBox)gvr.FindControl("txtpono")).Enabled = false;
                        ((TextBox)gvr.FindControl("txtsappono")).Enabled = false;
                        ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
                        ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                        ((Button)gvr.FindControl("btnDel")).Enabled = false;
                    }

                }


                ////for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                ////{
                ////    DataRow dr = dtCurrentTable.Rows[i];
                ////    if (dr["PlanLineId"].ToString() == "0" && string.IsNullOrEmpty(dr["Description"].ToString()))
                ////    {
                ////        dr.Delete();
                ////    }
                ////}
                ////dtCurrentTable.AcceptChanges();

                //drCurrentRow = dtCurrentTable.NewRow();
                //// dr["RowNumber"] = 1;
                //drCurrentRow["PlanLineId"] = "0";
                //drCurrentRow["VenderPoNo"] = string.Empty;
                //drCurrentRow["SAPPONo"] = string.Empty;
                //// drCurrentRow["ActivityDate"] = string.Empty;
                //drCurrentRow["Description"] = string.Empty;
                //drCurrentRow["Vendor"] = string.Empty;
                //drCurrentRow["Amount"] = "0";
                //drCurrentRow["Country"] = string.Empty;
                //drCurrentRow["LineRefNo"] = refno + "-" + (dtCurrentTable.Rows.Count+1).ToString();


                #region "Old code"



                //for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                //{
                //    TextBox box1 = (TextBox)GvPlan.Rows[rowIndex].Cells[1].FindControl("txtpono");
                //    TextBox box2 = (TextBox)GvPlan.Rows[rowIndex].Cells[2].FindControl("txtsappono");
                //    TextBox box3 = (TextBox)GvPlan.Rows[rowIndex].Cells[3].FindControl("txtgvdate");
                //    TextBox box4 = (TextBox)GvPlan.Rows[rowIndex].Cells[4].FindControl("txtgvdesc");
                //    TextBox box5 = (TextBox)GvPlan.Rows[rowIndex].Cells[5].FindControl("txtgvvendor");
                //    TextBox box6 = (TextBox)GvPlan.Rows[rowIndex].Cells[6].FindControl("txtgvamt");
                //    DropDownList box7 = (DropDownList)GvPlan.Rows[rowIndex].Cells[7].FindControl("ddlgvcountry");
                //    Label box8 = (Label)GvPlan.Rows[rowIndex].Cells[8].FindControl("lblplanlineid");

                //    drCurrentRow = dtCurrentTable.NewRow();
                //    // drCurrentRow["RowNumber"] = i + 1;
                //    dtCurrentTable.Rows[i - 1]["PlanLineId"] = box8.Text;
                //    drCurrentRow["LineRefNo"] = refno + "-" + (i + 1);
                //    dtCurrentTable.Rows[i - 1]["VenderPoNo"] = box1.Text;
                //    dtCurrentTable.Rows[i - 1]["SAPPONo"] = box2.Text;
                //    dtCurrentTable.Rows[i - 1]["ActivityDate"] = box3.Text;
                //    dtCurrentTable.Rows[i - 1]["Description"] = box4.Text;
                //    dtCurrentTable.Rows[i - 1]["Vendor"] = box5.Text;
                //    dtCurrentTable.Rows[i - 1]["Amount"] = box6.Text;
                //    dtCurrentTable.Rows[i - 1]["Country"] = box7.Text;
                //    rowIndex++;



                //    drCurrentRow = dtCurrentTable.NewRow();
                //    // dr["RowNumber"] = 1;
                //    drCurrentRow["PlanLineId"] = "0";
                //    drCurrentRow["VenderPoNo"] = string.Empty;
                //    drCurrentRow["SAPPONo"] = string.Empty;
                //    // drCurrentRow["ActivityDate"] = string.Empty;
                //    drCurrentRow["Description"] = string.Empty;
                //    drCurrentRow["Vendor"] = string.Empty;
                //    drCurrentRow["Amount"] = "0";
                //    drCurrentRow["Country"] = string.Empty;
                //    drCurrentRow["LineRefNo"] = refno + "-" + (i + 1);
                //    // rowIndex++;
                //}

                #endregion

                //dtCurrentTable.Rows.Add(drCurrentRow);
                //ViewState["CurrentTable"] = dtCurrentTable;
                //GvPlan.DataSource = dtCurrentTable;
                //GvPlan.DataBind();


                //}

                //else
                //{
                //    Response.Write("ViewState is null");
                //}
                ////Set Previous Data on Postbacks  
                //SetPreviousData();

            }
        }
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox box1 = (TextBox)GvPlan.Rows[rowIndex].Cells[1].FindControl("txtpono");

                    box1.Text = dt.Rows[i]["VenderPoNo"].ToString();

                    TextBox box2 = (TextBox)GvPlan.Rows[rowIndex].Cells[2].FindControl("txtgvamt");

                    box2.Text = dt.Rows[i]["Amount"].ToString();

                    TextBox box3 = (TextBox)GvPlan.Rows[rowIndex].Cells[3].FindControl("txtgvdate");

                    box3.Text = dt.Rows[i]["ActivityDate"].ToString();


                    TextBox box4 = (TextBox)GvPlan.Rows[rowIndex].Cells[4].FindControl("txtsappono");

                    box4.Text = dt.Rows[i]["SAPPONo"].ToString();

                    TextBox box5 = (TextBox)GvPlan.Rows[rowIndex].Cells[5].FindControl("txtgvdesc");

                    box5.Text = dt.Rows[i]["Description"].ToString();



                    TextBox box6 = (TextBox)GvPlan.Rows[rowIndex].Cells[6].FindControl("txtgvvendor");

                    box6.Text = dt.Rows[i]["Vendor"].ToString();


                    DropDownList box7 = (DropDownList)GvPlan.Rows[rowIndex].Cells[7].FindControl("ddlgvcountry");

                    box7.Text = dt.Rows[i]["Country"].ToString();

                    Label box8 = (Label)GvPlan.Rows[rowIndex].Cells[8].FindControl("lblplanlineid");

                    box8.Text = dt.Rows[i]["PlanLineId"].ToString();

                    Label box9 = (Label)GvPlan.Rows[rowIndex].Cells[9].FindControl("lblstatus1");

                    box9.Text = dt.Rows[i]["Status1"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    protected void txtgvamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtrddappamt.Text == string.Empty)
            {
                lblMsg.Text = "Please Enter RDD Approved Amount";
            }

            else
            {
                double total = 0;
                foreach (GridViewRow gvr in GvPlan.Rows)
                {
                    TextBox tb = (TextBox)gvr.Cells[1].FindControl("txtgvamt");
                    string status = (gvr.FindControl("ddlstatus") as DropDownList).Text;
                    if (status != "Rejected")
                    {
                        double sum;
                        if (double.TryParse(tb.Text.Trim(), out sum))
                        {
                            total += sum;
                        }
                    }
                }
                //Display  the Totals in the Footer row  
                GvPlan.FooterRow.Cells[1].Text = total.ToString();
                string s = GvPlan.FooterRow.Cells[1].Text;


                string RDDAPPAMT = txtrddappamt.Text;
                string APPAMT = txtappamount.Text;
                txtrddBalAmt.Text = (Convert.ToDecimal(RDDAPPAMT) - Convert.ToDecimal(s)).ToString();
                txtbalfromapp.Text = (Convert.ToDecimal(APPAMT) - Convert.ToDecimal(s)).ToString();

            }
        }
        catch { }

    }

    protected void GvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Button btn = (e.Row.FindControl("btnDel") as Button);
            if (BtnSave.Text == "Update")
            {
                btn.Enabled = true;
            }
            else
            {
                btn.Visible = false;
            }

            SqlConnection conn;
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet DsForms = new DataSet();
            conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP"));
            // DataSet ds = Db.myGetDS("select *  from rddCountriesList");
            DropDownList DropDownList1 = (e.Row.FindControl("ddlgvcountry") as DropDownList);

            cmd = new SqlCommand(" select '--select--' country, '--select--' CountryCode union all select country,CountryCode  from rddCountriesList", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            DropDownList1.DataSource = dt;

            DropDownList1.DataTextField = "Country";
            DropDownList1.DataValueField = "CountryCode";
            DropDownList1.DataBind();



            Label lblgrvCountry = (Label)e.Row.FindControl("lblgrvCountry");
            DropDownList1.SelectedItem.Text = lblgrvCountry.Text;

      

           // Label lblremark = (Label)e.Row.FindControl("lblremark");
           

            DataRowView data = (DataRowView)e.Row.DataItem;
         
            TextBox txtgvType = (TextBox)e.Row.FindControl("txtAppremark");


            DropDownList ddlgvstatus = (e.Row.FindControl("ddlstatus") as DropDownList);
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            ddlgvstatus.SelectedItem.Text = lblstatus.Text;

            DropDownList ddlgvstatus1 = (e.Row.FindControl("ddlstatus1") as DropDownList);
            Label lblstatus1 = (Label)e.Row.FindControl("lblstatus1");
            ddlgvstatus1.SelectedItem.Text = lblstatus1.Text;



            TextBox txtAppremarkk = (e.Row.FindControl("ApproverRemark") as TextBox);
            Label lblremark = (Label)e.Row.FindControl("lblremark");
            
                      
            //for login user
            int status = 1;
            if (status == 1)
            {
               

              if (ddlgvstatus.SelectedItem.Text == "")
              {
                  ddlgvstatus1.Enabled = true;
                  txtgvType.Enabled = false;
                  ddlgvstatus.Enabled = false;
              }

              else if (ddlgvstatus.SelectedItem.Text == "Closed")
              {
                  ddlgvstatus1.Enabled = false;
                  txtgvType.Enabled = false;
                  ddlgvstatus.Enabled = false;
              }
              else
              {
                  txtgvType.Enabled = false;
                  ddlgvstatus.Enabled = false;
                  ddlgvstatus1.Enabled = true;
              }

            }
        }
    }

    private void SetInitialRow()
    {


        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        txtrefno.Text = Db.myExecuteScalar2("Select dbo.GetMarketingRefNo('" + ddlCountry.SelectedItem.Value + "')");
        string refno = txtrefno.Text;
        DataTable dt = new DataTable();
        DataRow dr = null;
        // dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("PlanLineId", typeof(string)));
        dt.Columns.Add(new DataColumn("VenderPoNo", typeof(string)));
        dt.Columns.Add(new DataColumn("SAPPONo", typeof(string)));
        dt.Columns.Add(new DataColumn("ActivityDate", typeof(string)));
        dt.Columns.Add(new DataColumn("Description", typeof(string)));
        dt.Columns.Add(new DataColumn("Vendor", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Country", typeof(string)));
        dt.Columns.Add(new DataColumn("CountryName", typeof(string)));
       dt.Columns.Add(new DataColumn("Status", typeof(string)));
        dt.Columns.Add(new DataColumn("LineRefNo", typeof(string)));
        dt.Columns.Add(new DataColumn("ApproverRemark", typeof(string)));
        dt.Columns.Add(new DataColumn("Status1", typeof(string)));

        dr = dt.NewRow();
        // dr["RowNumber"] = 1;
        dr["PlanLineId"] = "0";
        dr["VenderPoNo"] = string.Empty;
        dr["SAPPONo"] = string.Empty;
        dr["ActivityDate"] = string.Empty;
        dr["Description"] = string.Empty;
        dr["Vendor"] = string.Empty;
        dr["Amount"] = "0";
        dr["Country"] = string.Empty;
        //dr["CountryName"] = string.Empty;
       
        dr["Status"] = string.Empty;
        dr["Status1"] = "Planned";
        dr["LineRefNo"] = refno + -+1;
        dr["ApproverRemark"] = string.Empty;

        dt.Rows.Add(dr);
        //Store the DataTable in ViewState  
        ViewState["CurrentTable"] = dt;
        GvPlan.DataSource = dt;
        GvPlan.DataBind();


    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedItem.Text == "--select--")
        {
            GvPlan.Visible = false;
        }
        else
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            txtrefno.Text = Db.myExecuteScalar2("Select dbo.GetMarketingRefNo('" + ddlCountry.SelectedItem.Value + "')");
            SetInitialRow();

        }
    }
    protected void Status1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (GvPlan.Rows.Count > 0)
        {
            foreach (GridViewRow g2 in GvPlan.Rows)
            {
                string status = (g2.FindControl("ddlstatus1") as DropDownList).Text;
                if (status == "Closed")
                {
                    ((TextBox)g2.FindControl("txtgvdate")).Enabled = false;
                    ((DropDownList)g2.FindControl("ddlgvcountry")).Enabled = false;
                    ((TextBox)g2.FindControl("txtgvvendor")).Enabled = false;
                    ((TextBox)g2.FindControl("txtgvdesc")).Enabled = false;
                    ((TextBox)g2.FindControl("txtgvamt")).Enabled = false;
                    ((TextBox)g2.FindControl("txtpono")).Enabled = false;
                    ((TextBox)g2.FindControl("txtsappono")).Enabled = false;
                }
                else
                {
                    ((TextBox)g2.FindControl("txtgvdate")).Enabled = true;
                    ((DropDownList)g2.FindControl("ddlgvcountry")).Enabled = true;
                    ((TextBox)g2.FindControl("txtgvvendor")).Enabled = true;
                    ((TextBox)g2.FindControl("txtgvdesc")).Enabled = true;
                    ((TextBox)g2.FindControl("txtgvamt")).Enabled = true;
                    ((TextBox)g2.FindControl("txtpono")).Enabled = true;
                    ((TextBox)g2.FindControl("txtsappono")).Enabled = true;
                }
            }

        }
    }

    private void BindDDL()
    {
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");       
            DataSet ds = Db.myGetDS("select *  from rddCountriesList");

            ddlCountry.DataSource = ds.Tables[0];
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "CountryCode";

            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--SELECT--");

            //string query = "select BUName from [dbo].[GetMarketingRefNo] where  country like '"+ddlCountry.SelectedItem.Text+"'";

            Db.constr = myGlobal.getIntranetDBConnectionString();
            DataSet DsForms = Db.myGetDS("select BUName from [dbo].[VendorBUDef]");

            if (DsForms.Tables.Count > 0)
            {
                ddlBU.DataSource = DsForms.Tables[0];
                ddlBU.DataTextField = "BUName";
                ddlBU.DataValueField = "BUName";


                ddlBU.DataBind();
                ddlBU.Items.Insert(0, "--SELECT--");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error BindDDL : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      //  try
      //  {
            if (BtnSave.Text == "Update")
            {

                string LoggedInUserName = myGlobal.loggedInUser();
                int PLID = Convert.ToInt32(Request.QueryString["GVid"]);
                DataSet ds2 = Db.myGetDS("exec GetUserAuthorizationForMPlan '" + LoggedInUserName + "'");
                string App = ds2.Tables[0].Rows[0]["Column1"].ToString();
                //APRROVER//
                if (App == "Approver")
                {
                    string rddamt = Convert.ToDecimal(txtrddappamt.Text).ToString();
                    string appamt = Convert.ToDecimal(txtappamount.Text).ToString();
                    if (Convert.ToDecimal(rddamt) > Convert.ToDecimal(appamt))
                    {
                        lblMsg.Text = "RDD Approved Amt Should be less than Approved Amt";
                        return;
                    }
                    if (Convert.ToDecimal(rddamt) == 0 && ddlappstatus.SelectedItem.Text=="Approved")
                    {
                        lblMsg.Text = "RDD Approved Amt Should Not be 0";
                        return;
                    }
                    if (txtapprmk.Text == string.Empty && ddlappstatus.SelectedItem.Text!="Pending")
                    {
                        lblMsg.Text = "Approver Remark Should not Be Empty";
                        return;
                    }
                    if (Convert.ToDecimal(rddamt) > 0 && ddlappstatus.Text=="Pending")
                    {
                        lblMsg.Text = "Approval Status Should be Approved";
                        return;
                    }
                    if (Convert.ToDecimal(rddamt) < 0 && ddlappstatus.SelectedItem.Text == "Approved")
                    {
                        lblMsg.Text = "RDD Approved Amt Should be greater than 0";
                        return;
                    } 

                    else
                    {
                      
                        foreach (GridViewRow gvr in GvPlan.Rows)
                        {
                            string status = (gvr.FindControl("ddlstatus") as DropDownList).Text;
                            if (ddlappstatus.SelectedItem.Text != "Approved" && ( status=="Approved" || status=="Rejected" ) )
                            {
                                lblMsg.Text = "You can not approv row level data.";
                                return;
                            }

                        }

                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        string sqll = "update MarketingPlan set RDDApprovedAmt='" + txtrddappamt.Text + "',BalanceAmount='" + txtrddBalAmt.Text + "',ApprovedBy='" + LoggedInUserName + "',ApprovalStatus='" + ddlappstatus.SelectedItem.Text + "',ApproverRemark='" + txtapprmk.Text + "',ApprovedOn='" + DateTime.Now.ToString() + "' where PlanId='" + PLID + "'";
                        Db.myExecuteSQL(sqll);

                        
                        foreach (GridViewRow gvr in GvPlan.Rows)
                        {
                            string apprmk = (gvr.FindControl("txtAppremark") as TextBox).Text;
                            string status = (gvr.FindControl("ddlstatus") as DropDownList).Text;
                            string planLineiId = (gvr.FindControl("lblplanlineid") as Label).Text;

                            if (apprmk == string.Empty && status != "Pending" && ddlappstatus.SelectedItem.Text != "Rejected")
                            {
                                lblMsg.Text = "row level  Remark Should Not Be Empty ";
                                return;
                            }

                            string sql = "update MarketingPlanLines set Status='" + status + "',ApprovedBy='" + LoggedInUserName + "',ApprovedOn='" + DateTime.Now.ToString() + "',ApproverRemark='" + apprmk + "'where PlanId='" + PLID + "' and PlanLineId='" + planLineiId + "'";
                            Db.myExecuteSQL(sql);
                        }

                        //// Qry to update balances
                        string qry = @" Declare @AmtUsed numeric(19,2);    set  @AmtUsed = (select sum(Amount) from marketingPlanLines where Status<>'Rejected' and  PlanId=" + PLID + @" And Flag=0 ) ;   
                                         Update marketingPlan Set  UsedAmount=@AmtUsed  , BalanceAmount= ( RDDApprovedAmt  - isnull(@AmtUsed,0) ),  BalanceFromApp = ( VendorApprovedAmt - isnull(@AmtUsed,0)  )  where   PlanId=" + PLID + "  ;    ";

                        Db.myExecuteSQL(qry);

                        ClearControl();
                        lblMsg.Text = "Marketing Plan Updated successfully.";
                        BtnSave.Text = "Save";
                    }

                }
                //FOR DRAFT AND ON HOLD CONDITION//
                else
                {
                    if (ddlplanstatus.SelectedItem.Text == "Draft" || ddlplanstatus.SelectedItem.Text == "On Hold")
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        PLID = Convert.ToInt32(Request.QueryString["GVid"]);

                        string createdon = DateTime.Now.ToString("MM/dd/yyyy");

                        double TotalGvamt = 0;
                        foreach (GridViewRow g1 in GvPlan.Rows)
                        {

                            TextBox tb = (TextBox)g1.Cells[1].FindControl("txtgvamt");
                            double sum;
                            if (double.TryParse(tb.Text.Trim(), out sum))
                            {
                                TotalGvamt += sum;
                            }
                        }
                        // GvPlan.FooterRow.Cells[1].Text = TotalGvamt.ToString();

                        //string usegvAmt = GvPlan.FooterRow.Cells[1].Text;
                        string usegvAmt = TotalGvamt.ToString();
                        string appAmt = txtappamount.Text;
                        if (Convert.ToDecimal(usegvAmt) > Convert.ToDecimal(appAmt))
                        {

                            lblMsg.Text = "used Amt should be less than Approved Amt";
                            return;
                        }
                        if (GvPlan.Rows.Count > 0)
                        {
                            foreach (GridViewRow g1 in GvPlan.Rows)
                            {

                                string VendorPO = (g1.FindControl("txtpono") as TextBox).Text;
                                string sappo = (g1.FindControl("txtsappono") as TextBox).Text;
                                string actDate = (g1.FindControl("txtgvdate") as TextBox).Text;
                                string desc = (g1.FindControl("txtgvdesc") as TextBox).Text;
                                string vendor = (g1.FindControl("txtgvvendor") as TextBox).Text;
                                string Amt = (g1.FindControl("txtgvamt") as TextBox).Text;
                                string country = (g1.FindControl("ddlgvcountry") as DropDownList).Text;
                                string countryname = (g1.FindControl("ddlgvcountry") as DropDownList).SelectedItem.Text;
                                string status = (g1.FindControl("ddlstatus") as DropDownList).Text;
                                string apprem = (g1.FindControl("txtAppremark") as TextBox).Text;
                                string gvKey = (g1.FindControl("lblKey") as Label).Text;

                                string planLineiId = (g1.FindControl("lblplanlineid") as Label).Text;

                                if (!string.IsNullOrEmpty(planLineiId) && planLineiId != "0")
                                {
                                    string sql = "Update MarketingPlan set planStatus='" + ddlplanstatus.SelectedItem.Text + "',VendorApprovedAmt='" + txtappamount.Text + "',RDDApprovedAmt='" + txtrddappamt.Text + "',BalanceAmount='" + txtrddBalAmt.Text + "',BalanceFromApp='" + txtbalfromapp.Text + "',StartDate='" + txtstartdate.Text + "',EndDate='" + txtEndDate.Text + "',Description='" + txtdesc.Text + "',UsedAmount='" + usegvAmt + "' where PlanId='" + PLID + "'";
                                    Db.myExecuteSQL(sql);

                                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                                    sql = "Update MarketingPlanLines set VenderPONo='" + VendorPO + "',SAPPONo='" + sappo + "',ActivityDate='" + actDate + "',Description='" + desc + "',Vendor='" + vendor + "',Amount='" + Amt + "',Country='" + country + "',CountryName='" + countryname + "',LineRefNo='" + gvKey + "'  where PlanId='" + PLID + "' and PlanLineId='" + planLineiId + "'";
                                    Db.myExecuteSQL(sql);

                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(Amt))
                                        Amt = "0";
                                    if (Convert.ToDecimal(Amt) > 0 && desc != "")
                                    {
                                        string sql = "insert into MarketingPlanLines(PlanId,LineRefNo,VenderPONo,SAPPONo,ActivityDate,Description,Vendor,Amount,Country,CountryName,Status,ApproverRemark,CreatedOn,CreatedBy,Flag)values('" + PLID + "','" + gvKey + "','" + VendorPO + "','" + sappo + "','" + actDate + "','" + desc + "','" + vendor + "','" + Amt + "','" + country + "','" + countryname + "','Pending','" + apprem + "',getdate(),'" + myGlobal.loggedInUser() + "',0)";
                                        // string sql = "insert into MarketingPlanLines(PlanId,LineRefNo,VenderPONo,SAPPONo,ActivityDate,Description,Vendor,Amount,Country,Status)values('" + PLID + "','" + gvKey + "','" + VendorPO + "','" + sappo + "','" + actDate + "','" + desc + "','" + vendor + "','" + Amt + "','" + country + "','" + status + "' )";
                                        Db.myExecuteSQL(sql);
                                    }

                                }

                            }

                            //// Qry to update balances
                            string qry = @" Declare @AmtUsed numeric(19,2);    set  @AmtUsed = (select sum(Amount) from marketingPlanLines where Status<>'Rejected' and  PlanId=" + PLID + @" And Flag=0 ) ;   
                                         Update marketingPlan Set  UsedAmount=@AmtUsed  , BalanceAmount= ( RDDApprovedAmt  - isnull(@AmtUsed,0) ),  BalanceFromApp = ( VendorApprovedAmt - isnull(@AmtUsed,0)  )  where  PlanId=" + PLID + "  ;    ";

                            Db.myExecuteSQL(qry);

                            // myGlobal.SendMarketingPLanForApprover("pramod@reddotdistribution.com", "pramod@reddotdistribution.com", "pramod", Convert.ToInt32(PLID));

                            ClearControl();
                            lblMsg.Text = "Marketing Plan Updated successfully.";
                            BtnSave.Text = "Save";
                        }
                        else
                        {
                            string sql = "Update MarketingPlan set planStatus='" + ddlplanstatus.SelectedItem.Text + "',VendorApprovedAmt='" + txtappamount.Text + "',RDDApprovedAmt='" + txtrddappamt.Text + "',BalanceAmount='" + txtrddBalAmt.Text + "',BalanceFromApp='" + txtbalfromapp.Text + "',StartDate='" + txtstartdate.Text + "',EndDate='" + txtEndDate.Text + "',Description='" + txtdesc.Text + "',UsedAmount='" + usegvAmt + "' where PlanId='" + PLID + "'";
                            Db.myExecuteSQL(sql);

                        }


                        ClearControl();
                        lblMsg.Text = "Marketing Plan Updated successfully.";
                        BtnSave.Text = "Save";

                    }
                    //FOR OPEN CONDITION//

                    else
                    {
                        if (ddlplanstatus.SelectedItem.Text == "Open" || ddlplanstatus.SelectedItem.Text == "Closed Unpaid" || ddlplanstatus.SelectedItem.Text == "Closed Paid")
                        {
                            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                            PLID = Convert.ToInt32(Request.QueryString["GVid"]);

                            string createdon = DateTime.Now.ToString("MM/dd/yyyy");
                            double TotalGvamt = 0;
                            foreach (GridViewRow g1 in GvPlan.Rows)
                            {

                                TextBox tb = (TextBox)g1.Cells[1].FindControl("txtgvamt");
                                double sum;
                                if (double.TryParse(tb.Text.Trim(), out sum))
                                {
                                    TotalGvamt += sum;
                                }
                            }
                            // GvPlan.FooterRow.Cells[1].Text = TotalGvamt.ToString();

                            //string usegvAmt = GvPlan.FooterRow.Cells[1].Text;
                            string usegvAmt = TotalGvamt.ToString();
                            string appAmt = txtappamount.Text;
                            if (Convert.ToDecimal(usegvAmt) > Convert.ToDecimal(appAmt))
                            {

                                lblMsg.Text = "used Amt should be less than Approved Amt";
                                return;
                            }

                           
                            if (GvPlan.Rows.Count > 0)
                            {
                                foreach (GridViewRow g1 in GvPlan.Rows)
                                {

                                    string VendorPO = (g1.FindControl("txtpono") as TextBox).Text;
                                    string sappo = (g1.FindControl("txtsappono") as TextBox).Text;
                                    string actDate = (g1.FindControl("txtgvdate") as TextBox).Text;
                                    string desc = (g1.FindControl("txtgvdesc") as TextBox).Text;
                                    string vendor = (g1.FindControl("txtgvvendor") as TextBox).Text;
                                    string Amt = (g1.FindControl("txtgvamt") as TextBox).Text;
                                    string country = (g1.FindControl("ddlgvcountry") as DropDownList).Text;
                                    string countryname = (g1.FindControl("ddlgvcountry") as DropDownList).SelectedItem.Text;
                                   string status1 = (g1.FindControl("ddlstatus1") as DropDownList).Text;
                                    string apprem = (g1.FindControl("txtAppremark") as TextBox).Text;
                                    string gvKey = (g1.FindControl("lblKey") as Label).Text;

                                    string planLineiId = (g1.FindControl("lblplanlineid") as Label).Text;


                                    if (!string.IsNullOrEmpty(planLineiId) && planLineiId != "0")
                                    {
                                     
                                        string sql = "Update MarketingPlan set planStatus='" + ddlplanstatus.SelectedItem.Text + "',VendorApprovedAmt='" + txtappamount.Text + "',RDDApprovedAmt='" + txtrddappamt.Text + "',BalanceAmount='" + txtrddBalAmt.Text + "',BalanceFromApp='" + txtbalfromapp.Text + "',StartDate='" + txtstartdate.Text + "',EndDate='" + txtEndDate.Text + "',Description='" + txtdesc.Text + "',UsedAmount='" + usegvAmt + "' where PlanId='" + PLID + "'";
                                        Db.myExecuteSQL(sql);

                                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                                        sql = "Update MarketingPlanLines set Status1='"+status1+"' , VenderPONo='" + VendorPO + "',SAPPONo='" + sappo + "',ActivityDate='" + actDate + "',Description='" + desc + "',Vendor='" + vendor + "',Amount='" + Amt + "',Country='" + country + "',CountryName='" + countryname + "',LineRefNo='" + gvKey + "'  where PlanId='" + PLID + "' and PlanLineId='" + planLineiId + "'";
                                        Db.myExecuteSQL(sql);


                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(Amt) > 0 && desc != "")
                                        {

                                            string sql = "insert into MarketingPlanLines(PlanId,LineRefNo,VenderPONo,SAPPONo,ActivityDate,Description,Vendor,Amount,Country,CountryName,Status,ApproverRemark,CreatedOn,CreatedBy,Flag,Status1)values('" + PLID + "','" + gvKey + "','" + VendorPO + "','" + sappo + "','" + actDate + "','" + desc + "','" + vendor + "','" + Amt + "','" + country + "','" + countryname + "','Pending','" + apprem + "',getdate(),'" + myGlobal.loggedInUser() + "',0,'" + status1 + "')";
                                            // string sql = "insert into MarketingPlanLines(PlanId,LineRefNo,VenderPONo,SAPPONo,ActivityDate,Description,Vendor,Amount,Country,Status)values('" + PLID + "','" + gvKey + "','" + VendorPO + "','" + sappo + "','" + actDate + "','" + desc + "','" + vendor + "','" + Amt + "','" + country + "','" + status + "' )";
                                            Db.myExecuteSQL(sql);

                                        }
                                    }

                                }
                            }
                            else
                            {
                                string sql = "Update MarketingPlan set planStatus='" + ddlplanstatus.SelectedItem.Text + "',VendorApprovedAmt='" + txtappamount.Text + "',RDDApprovedAmt='" + txtrddappamt.Text + "',BalanceAmount='" + txtrddBalAmt.Text + "',BalanceFromApp='" + txtbalfromapp.Text + "',StartDate='" + txtstartdate.Text + "',EndDate='" + txtEndDate.Text + "',Description='" + txtdesc.Text + "',UsedAmount='" + usegvAmt + "' where PlanId='" + PLID + "'";
                                Db.myExecuteSQL(sql);
                            }
                        }

                        //// Qry to update balances
                        string qry = @" Declare @AmtUsed numeric(19,2);    set  @AmtUsed = (select sum(Amount) from marketingPlanLines where Status<>'Rejected' and  PlanId=" + PLID + @" And Flag=0 ) ;   
                                         Update marketingPlan Set  UsedAmount=@AmtUsed  , BalanceAmount= ( RDDApprovedAmt  - isnull(@AmtUsed,0) ),  BalanceFromApp = ( VendorApprovedAmt - isnull(@AmtUsed,0)  )  where  PlanId=" +PLID+"  ;    ";

                        Db.myExecuteSQL(qry);

                        ClearControl();
                        lblMsg.Text = "Marketing Plan Updated successfully.";
                        BtnSave.Text = "Save";
                        // myGlobal.SendMarketingPLanForApprover("pramod@reddotdistribution.com", "pramod@reddotdistribution.com", "pramod", Convert.ToInt32(PLID));
                    }


                    ClearControl();
                    lblMsg.Text = "Marketing Plan Updated successfully.";
                    BtnSave.Text = "Save";

                }

            }

                //////for BTN==="SAVE""
            else
            {


                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                DateTime StartDate = Convert.ToDateTime(txtstartdate.Text);
                int appAmt = Convert.ToInt32(txtappamount.Text);
                int RddAppAmt = Convert.ToInt32(txtrddappamt.Text);
                if (StartDate > EndDate)
                {
                    lblMsg.Text = "EndDate Should be Greater Than Startdate";
                    return;
                }
                if (RddAppAmt > appAmt)
                {
                    lblMsg.Text = "RDD Approved Amt Should be Less than Approved Amount";
                    return;
                }


                string a = GvPlan.Rows[0].Cells[2].Text;

                string LoggedInUserName = myGlobal.loggedInUser();
                string createdon = DateTime.Now.ToString("MM/dd/yyyy");
                double total = 0;

                foreach (GridViewRow gvr in GvPlan.Rows)
                {

                    TextBox tb = (TextBox)gvr.Cells[1].FindControl("txtgvamt");
                    TextBox desc = (TextBox)gvr.Cells[6].FindControl("txtgvdesc");
                    double sum;
                    if (double.TryParse(tb.Text.Trim(), out sum))
                    {
                        total += sum;
                    }
                }
                //Display  the Totals in the Footer row  
                GvPlan.FooterRow.Cells[1].Text = total.ToString();
                string usedAmt = GvPlan.FooterRow.Cells[1].Text;

                //  string description = GvPlan.FooterRow.Cells[6].Text; //&nbsp;



                decimal RDDAPP = Convert.ToDecimal(txtrddappamt.Text);

                if (Convert.ToDecimal(total) > Convert.ToDecimal(appAmt))
                {

                    lblMsg.Text = "used Amt should be less than Approved Amt";
                    return;
                }


                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                // ,BalanceAmount,BalanceFromAp -- Calcualtre here and use these values to save
                Decimal BalanceAmount = ( RDDAPP - Convert.ToDecimal(total) );

                /*insert record in marketingplan*/
                #region"Old Code to save"
                //string sqlquery = "insert into MarketingPlan(SourceOfFund,RefNo,Country,CountryName,Vendor,VendorApprovedAmt,RDDApprovedAmt,BalanceAmount,BalanceFromApp,UsedAmount,Description,planStatus,ApprovalStatus,ApprovedBy,ApprovedOn,ApproverRemark,StartDate,EndDate,IsDraft,CreatedOn,CreatedBy,LastUpdatedOn,LastUpdateBy)values('" + ddlsourcefd.SelectedItem.Text + "', dbo.GetMarketingRefNo('" + ddlCountry.SelectedValue + "'),'" + ddlCountry.SelectedValue + "','" + ddlCountry.SelectedItem.Text + "','" + ddlBU.SelectedItem.Text + "','" + txtappamount.Text + "','" + txtrddappamt.Text + "','" + txtrddBalAmt.Text + "','" + txtbalfromapp.Text + "','" + usedAmt + "','" + txtdesc.Text + "','" + ddlplanstatus.SelectedItem.Text + "','" + ddlappstatus.SelectedItem.Text + "','0',0,'','" + txtstartdate.Text + "','" + txtEndDate.Text + "',0,getdate(),'" + myGlobal.loggedInUser() + "',0,'')";
                //Db.myExecuteSQL(sqlquery);
                /*to key primary key from marketingplan to store at  marketinglist*/
                //SqlConnection conn;
                //conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP"));
                //sqlquery = "select MAX(PlanId) from MarketingPlan";
                //conn.Open();
                //SqlCommand cmd = new SqlCommand(sqlquery, conn);

                //SqlDataReader dr = cmd.ExecuteReader();
                //if (dr.Read())
                //{
                //    string val = dr[0].ToString();
                //    hdnplanid.Value = val;
                //}
                ///*End*/
                #endregion


                string sqlquery =  " Declare @PlanId int , @RefNo varchar(20) ;  insert into MarketingPlan(SourceOfFund,RefNo,Country,CountryName,Vendor,VendorApprovedAmt,RDDApprovedAmt,BalanceAmount,BalanceFromApp,UsedAmount,Description,planStatus,ApprovalStatus,ApprovedBy,ApprovedOn,ApproverRemark,StartDate,EndDate,IsDraft,CreatedOn,CreatedBy,LastUpdatedOn,LastUpdateBy)values('" + ddlsourcefd.SelectedItem.Text + "', dbo.GetMarketingRefNo('" + ddlCountry.SelectedValue + "'),'" + ddlCountry.SelectedValue + "','" + ddlCountry.SelectedItem.Text + "','" + ddlBU.SelectedItem.Text + "','" + txtappamount.Text + "','" + txtrddappamt.Text + "','" + txtrddBalAmt.Text + "','" + txtbalfromapp.Text + "','" + usedAmt + "','" + txtdesc.Text + "','" + ddlplanstatus.SelectedItem.Text + "','" + ddlappstatus.SelectedItem.Text + "','0',0,'','" + txtstartdate.Text + "','" + txtEndDate.Text + "',0,getdate(),'" + myGlobal.loggedInUser() + "',0,'')  ;  ";

                sqlquery = sqlquery + "    set @PlanId=  @@Identity  ;  set @RefNo= ( select RefNo From  MarketingPlan Where PlanId=@PlanId )  ;    ";


              

                /*TO UPLOAD FILE*/



                String pth, dirPhyPth, flsavePth, flsavePthvendor;
                pth = "/excelFileUpload/Marketing/";

                string cmts = "";
                int sts, esc;
                pth = pth + "planId" + "dbo.GetMarketingRefNo('" + ddlCountry.SelectedValue + "')" + "/";

                dirPhyPth = Server.MapPath("~" + pth);

                if (!System.IO.Directory.Exists(dirPhyPth))
                {
                    System.IO.Directory.CreateDirectory(dirPhyPth);
                }
                if (FileUpload1.HasFile)
                {
                    flsavePth = Server.MapPath("~" + pth) + FileUpload1.FileName;
                    lblFile.Text = pth + FileUpload1.FileName;
                    FileUpload1.SaveAs(flsavePth);
                }






                string Pid = hdnplanid.Value;
                int rowcount = 0;
                foreach (GridViewRow g1 in GvPlan.Rows)
                {

                    string VendorPO = (g1.FindControl("txtpono") as TextBox).Text;
                    string sappo = (g1.FindControl("txtsappono") as TextBox).Text;
                    string actDate = (g1.FindControl("txtgvdate") as TextBox).Text;
                    string desc = (g1.FindControl("txtgvdesc") as TextBox).Text;
                    string vendor = (g1.FindControl("txtgvvendor") as TextBox).Text;
                    string Amt = (g1.FindControl("txtgvamt") as TextBox).Text;
                    string country = (g1.FindControl("ddlgvcountry") as DropDownList).SelectedValue;
                    string countryname = (g1.FindControl("ddlgvcountry") as DropDownList).SelectedItem.Text;
                    string status1 = (g1.FindControl("ddlstatus1") as DropDownList).Text;
                    string apprem = (g1.FindControl("txtAppremark") as TextBox).Text;
                    string gvKey = (g1.FindControl("lblKey") as Label).Text;

                    if (!string.IsNullOrEmpty(desc) && !string.IsNullOrEmpty(Amt))
                    {

                        if (Convert.ToDecimal(Amt) > 0 && desc != "")
                        {
                            //string[] keyarr = gvKey.Split('-');
                            //sqlquery = "select count(*) + 1 from MarketingPlanLines where PlanId= @PlanId";

                            //sqlquery = "insert into MarketingPlanLines(PlanId,LineRefNo,VenderPONo,SAPPONo,ActivityDate,Description,Vendor,Amount,Country,CountryName,Status,ApproverRemark,CreatedOn,CreatedBy,Flag)values('" + Pid + "', dbo.GetMarketingRefNo('" + ddlCountry.SelectedValue + "'),'" + VendorPO + "','" + sappo + "','" + actDate + "','" + desc + "','" + vendor + "','" + Amt + "','" + country + "','" + countryname + "','" + status + "','" + apprem + "',getdate(),'" + myGlobal.loggedInUser() + "',0)";
                            //Db.myExecuteSQL(sqlquery);
                           

                            sqlquery = sqlquery + "insert into MarketingPlanLines(PlanId,LineRefNo,VenderPONo,SAPPONo,ActivityDate,Description,Vendor,Amount,Country,CountryName,Status,Status1,ApproverRemark,CreatedOn,CreatedBy,Flag,FileName)values(@PlanId, @RefNo +'-'+ '" + (rowcount + 1).ToString() + "','" + VendorPO + "','" + sappo + "','" + actDate + "','" + desc + "','" + vendor + "','" + Amt + "','" + country + "','" + countryname + "','Pending','" + status1 + "','" + apprem + "',getdate(),'" + myGlobal.loggedInUser() + "',0,'"+lblfilename.Text+"')";
                            rowcount = rowcount + 1;

                           
                          
                        


                            //if (FileUpload1.HasFile)
                            //{
                            //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/excelFileUpload/Marketing/") + FileUpload1.FileName);

                            //}
                            //DataTable dt = new DataTable();
                            //dt.Columns.Add("File", typeof(string));
                            //dt.Columns.Add("Size", typeof(string));
                            //dt.Columns.Add("Type", typeof(string));

                            //foreach (string strFile in Directory.GetFiles(Server.MapPath("~/excelFileUpload/Marketing/")))
                            //{
                            //    FileInfo fi = new FileInfo(strFile);
                            //    dt.Rows.Add(fi.Name, fi.Length, fi.Extension);

                            //}
                            //GridView1.DataSource = dt;
                            //GridView1.DataBind();



                        }
                    }
                }

                if (sqlquery.Length > 0)
                {
                    Db.myExecuteSQL(sqlquery);
                }

                ClearControl();
                lblMsg.Text = "Marketing Plan saved successfully.";
            }

            string p = ddlplanstatus.SelectedItem.Text;
            if (p != "Draft")
            {
                //myGlobal.SendMarketingPLanForApprover("pramod@reddotdistribution.com", "pramod@reddotdistribution.com", "pramod", Convert.ToInt32(Pid));


                ClearControl();
                lblMsg.Text = "Marketing Plan saved successfully.";
            }
            else
            {

                ClearControl();
                lblMsg.Text = "Marketing Plan saved successfully.";
            }

        }


      //  catch { }
    

    public void ClearControl()
    {
        txtappamount.Text = "";
        txtapprmk.Text = "";
        txtbalfromapp.Text = "";
        lbltodaydate.Text = "";
        txtdesc.Text = "";
        txtrddappamt.Text = "0";
        txtrddBalAmt.Text = "";
        txtrefno.Text = "";
        txtstartdate.Text = "";
        // ddlappstatus.Text = "";
        // ddlCountry.Text = "";
        //ddlplanstatus.Text = "";
        // ddlsourcefd.Text = "";
        txtEndDate.Text = "";
        lblMsg.Text = string.Empty;

        // GvPlan.Rows.Clear();
        GvPlan.DataSource = null;
        GvPlan.DataBind();

        try
        {
            ddlCountry.SelectedIndex = -1;
            ddlCountry.SelectedItem.Text = "--SELECT--";
        }
        catch { }
        //try
        //{
        //    ddlplanstatus.SelectedIndex = -1;
        //    ddlplanstatus.SelectedItem.Text = "--SELECT--";
        //}
        //catch { }
        try
        {
            ddlsourcefd.SelectedIndex = -1;
            ddlsourcefd.SelectedItem.Text = "--SELECT--";
        }
        catch { }
        try
        {
            ddlBU.SelectedIndex = -1;
            ddlBU.SelectedItem.Text = "--SELECT--";
        }
        catch { }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MarketingListData.aspx");
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32(Request.QueryString["GVid"]);

        string PlanLineId = (((Label)(((Button)sender).NamingContainer as GridViewRow).FindControl("lblplanlineid")).Text);
        string LineRefKey = (((Label)(((Button)sender).NamingContainer as GridViewRow).FindControl("lblKey")).Text);


        #region"Old Code"

        if (!string.IsNullOrEmpty(PlanLineId) && PlanLineId != "0")
        {
           
            string query = "update MarketingPlanLines set Flag=1 where PlanId='" + ID + "' and PlanLineId='" + PlanLineId + "'";
            Db.myExecuteSQL(query);

        }

        string refno = txtrefno.Text;
        DataTable dtCurrentTable = new DataTable(); //(DataTable)ViewState["CurrentTable"];
        DataRow drCurrentRow = null;
        //foreach()

        dtCurrentTable.Columns.Add(new DataColumn("PlanLineId", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("VenderPoNo", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("SAPPONo", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("ActivityDate", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("Description", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("Vendor", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("Amount", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("Country", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("LineRefNo", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("CountryName", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("Status", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("ApproverRemark", typeof(string)));
        dtCurrentTable.Columns.Add(new DataColumn("Status1", typeof(string)));


        for (int i = 0; i < GvPlan.Rows.Count; i++)
        {

            GridViewRow row = GvPlan.Rows[i];

            TextBox txtpono = (TextBox)row.FindControl("txtpono");
            TextBox txtsappono = (TextBox)row.FindControl("txtsappono");
            TextBox txtgvdate = (TextBox)row.FindControl("txtgvdate");
            TextBox txtgvdesc = (TextBox)row.FindControl("txtgvdesc");
            TextBox txtgvvendor = (TextBox)row.FindControl("txtgvvendor");
            TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
            DropDownList ddlgvcountry = (DropDownList)row.FindControl("ddlgvcountry");
            Label lblplanlineid = (Label)row.FindControl("lblplanlineid");
            Label lblKey = (Label)row.FindControl("lblKey");

            Label lblgrvCountry = (Label)row.FindControl("lblgrvCountry");
            Label lblstatus = (Label)row.FindControl("lblstatus");
            TextBox txtAppremark = (TextBox)row.FindControl("txtAppremark");
            Label lblstatus1 = (Label)row.FindControl("lblstatus1");


            if (lblplanlineid.Text.Trim() != PlanLineId.Trim() && lblplanlineid.Text.Trim() != "0")
            {
                if (lblplanlineid.Text.Trim() != "0" || !string.IsNullOrEmpty(txtgvdesc.Text.Trim()))
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["PlanLineId"] = lblplanlineid.Text;
                    drCurrentRow["VenderPoNo"] = txtpono.Text;
                    drCurrentRow["SAPPONo"] = txtsappono.Text;
                    drCurrentRow["ActivityDate"] = txtgvdate.Text;
                    drCurrentRow["Description"] = txtgvdesc.Text;
                    drCurrentRow["Vendor"] = txtgvvendor.Text;
                    drCurrentRow["Amount"] = txtgvamt.Text;
                    drCurrentRow["Country"] = ddlgvcountry.SelectedItem.Text;
                    drCurrentRow["LineRefNo"] = lblKey.Text;

                    drCurrentRow["CountryName"] = ddlgvcountry.SelectedItem.Text;
                    drCurrentRow["Status"] = lblstatus.Text;
                    drCurrentRow["ApproverRemark"] = txtAppremark.Text;
                    drCurrentRow["Status1"] = lblstatus1.Text;

                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
            }
            else if (LineRefKey != lblKey.Text.Trim() && lblplanlineid.Text.Trim() == "0")
            {
                if (lblplanlineid.Text.Trim() != "0" || !string.IsNullOrEmpty(txtgvdesc.Text.Trim()))
                {
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["PlanLineId"] = lblplanlineid.Text;
                    drCurrentRow["VenderPoNo"] = txtpono.Text;
                    drCurrentRow["SAPPONo"] = txtsappono.Text;
                    drCurrentRow["ActivityDate"] = txtgvdate.Text;
                    drCurrentRow["Description"] = txtgvdesc.Text;
                    drCurrentRow["Vendor"] = txtgvvendor.Text;
                    drCurrentRow["Amount"] = txtgvamt.Text;
                    drCurrentRow["Country"] = ddlgvcountry.SelectedItem.Text;
                    drCurrentRow["LineRefNo"] = lblKey.Text;

                    drCurrentRow["CountryName"] = ddlgvcountry.SelectedItem.Text;
                    drCurrentRow["Status"] = lblstatus.Text;
                    drCurrentRow["Status1"] = lblstatus1.Text;
                    drCurrentRow["ApproverRemark"] = txtAppremark.Text;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                   
                }
            }
        }

        GvPlan.DataSource = dtCurrentTable;
        GvPlan.DataBind();

        foreach (GridViewRow gvr in GvPlan.Rows)
        {

            DropDownList ddlstatus = (DropDownList)gvr.FindControl("ddlstatus");
            if (ddlstatus.SelectedItem.Text == "Approved" || ddlstatus.SelectedItem.Text == "Rejected")
            {
                ((TextBox)gvr.FindControl("txtgvdate")).Enabled = false;
                ((DropDownList)gvr.FindControl("ddlgvcountry")).Enabled = false;
                ((TextBox)gvr.FindControl("txtgvvendor")).Enabled = false;
                ((TextBox)gvr.FindControl("txtgvdesc")).Enabled = false;
                ((TextBox)gvr.FindControl("txtgvamt")).Enabled = false;
                ((TextBox)gvr.FindControl("txtpono")).Enabled = true;
                ((TextBox)gvr.FindControl("txtsappono")).Enabled = true;
                ((DropDownList)gvr.FindControl("ddlstatus")).Enabled = false;
                ((TextBox)gvr.FindControl("txtAppremark")).Enabled = false;
                ((Button)gvr.FindControl("btnDel")).Enabled = false;
                ((DropDownList)gvr.FindControl("ddlstatus1")).Enabled = false;
            }

        }

        //if (PlanLineId == "0")
        {
            double total = 0;
            foreach (GridViewRow gvr in GvPlan.Rows)
            {
                TextBox tb = (TextBox)gvr.Cells[1].FindControl("txtgvamt");
                string status = (gvr.FindControl("ddlstatus") as DropDownList).Text;
                if (status != "Rejected" && status != "")
                {
                    double sum;
                    if (double.TryParse(tb.Text.Trim(), out sum))
                    {
                        total += sum;
                    }
                }
            }

            //Display  the Totals in the Footer row  
            //GvPlan.FooterRow.Cells[1].Text = total.ToString();
            //string s = GvPlan.FooterRow.Cells[1].Text;


            string RDDAPPAMT = txtrddappamt.Text;
            string APPAMT = txtappamount.Text;
            txtrddBalAmt.Text = (Convert.ToDecimal(RDDAPPAMT) - Convert.ToDecimal(total)).ToString();
            txtbalfromapp.Text = (Convert.ToDecimal(APPAMT) - Convert.ToDecimal(total)).ToString();
        }

        #endregion

    }


    protected void GvPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPlan.PageIndex = e.NewPageIndex;

    }




    
    protected void Button1_Click1(object sender, EventArgs e)
    {

    



        if (FileUpload1.HasFile)
        {
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/excelFileUpload/Marketing/") + FileUpload1.FileName);

        }
        DataTable dt = new DataTable();
        dt.Columns.Add("File", typeof(string));
        dt.Columns.Add("Size", typeof(string));
        dt.Columns.Add("Type", typeof(string));

        foreach (string strFile in Directory.GetFiles(Server.MapPath("~/excelFileUpload/Marketing/")))
        {
            FileInfo fi = new FileInfo(strFile);
            dt.Rows.Add(fi.Name, fi.Length, fi.Extension);
            
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();
       
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
            Response.TransmitFile(Server.MapPath("~/excelFileUpload/Marketing/")+e.CommandArgument);
            Response.End();
         
        }
    }
}
