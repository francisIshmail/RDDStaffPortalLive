using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;

public partial class IntranetNew_DesignationsTarget_TargetDesigMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='TargetDesigMaster.aspx' and t1.IsActive=1");
            if (count > 0)
            {
                if (!IsPostBack)
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    string LoggedInUserName = myGlobal.loggedInUser();
                    //  fillgv();
                    DataSet ds = null;

                    BindGrid();
                    if (Request.QueryString["Action"] == "SUBMIT")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'U are Not Allowed to Enter Same Designation.'); </script>");
                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=Setup - Reporting Frequency And Targets ");
            }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        DropDownList ddldesi1 = GRVAutoCLStatusChange.FooterRow.FindControl("ddldesi") as DropDownList;
        DataSet DsForms = Db.myGetDS("select ID,Designation from designation_master");
        ddldesi1.DataSource = DsForms.Tables[0];
        ddldesi1.DataTextField = "Designation";
        ddldesi1.DataValueField = "ID";
     
        ddldesi1.DataBind();
        ddldesi1.Items.Insert(0, "--Select--");

    }
    protected void GRVAutoCLStatusChange_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "Add")
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

         //   TextBox ddldesiid = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("ddldesiid");
            DropDownList ddldesi = (DropDownList)GRVAutoCLStatusChange.FooterRow.FindControl("ddldesi");
            DropDownList ddlFreqRpt = (DropDownList)GRVAutoCLStatusChange.FooterRow.FindControl("ddlFreqRpt");
            TextBox txtminperday = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtminperday");
          
            TextBox txtweek = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtweek");
            TextBox txtmon = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtmon");
            TextBox txtDistinct = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtDistinct");
            TextBox txtRpt = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtRpt");
            TextBox txtcus = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtcus");
            CheckBox chkfooter = (CheckBox)GRVAutoCLStatusChange.FooterRow.FindControl("chkfooter");

            if (ddldesi.Text == "--Select--")
            {
                lblMsg.Text = "Please select Designation";
                    return;
            }

            if (ddlFreqRpt.Text == "--Select--")
            {
                lblMsg.Text = "Please select freq Of Rpt";
                return;
            }
            string chkview = "";
            if (chkfooter.Checked == true)
            {
                chkview = "1";
            }
            else
            {
                chkview = "0";
            }


            string sql = "select count(*) From DSR_CustomerVisitTarget where DesigId='" + ddldesi.Text + "'";

                            int retValue = Db.myExecuteScalar(sql);
                            if (retValue > 0)
                            {
                                //lblMsg.Text = "You are Not allowed to enter same Designation";
                                //return;
                                Response.Redirect("TargetDesigMaster.aspx?Action=SUBMIT");      
                            }

            //if (txtfunnelFooter.Text == "")
            //{
            //    lblMsg.Text = "Please Enter Status";
            //    return;
            //}
           // string query = "insert into DSR_DesignationTargets(DesigId,DesigName,FreqOfRpt,MinPerDay,WeeklyTarget,MonthlyTarget,DistinctTarget,RepeatTarget,NewCustomers)values('"+ddldesi.SelectedItem.Value+"','" + ddldesi.SelectedItem.Text + "','" + ddlFreqRpt.SelectedItem.Text + "','" + txtminperday.Text + "','" + txtweek.Text + "','" + txtmon.Text + "','" + txtDistinct.Text + "','" + txtRpt.Text + "','" + txtcus.Text + "')";
                            string query = "insert into DSR_CustomerVisitTarget(DesigId,DesigName,FreqOfRpt,DailyTarget,WeeklyTarget,MonthlyTarget,DistinctTarget,RepeatTarget,NewCustomers,ViewScore)values('" + ddldesi.SelectedItem.Value + "','" + ddldesi.SelectedItem.Text + "','" + ddlFreqRpt.SelectedItem.Text + "','" + txtminperday.Text + "','" + txtweek.Text + "','" + txtmon.Text + "','" + txtDistinct.Text + "','" + txtRpt.Text + "','" + txtcus.Text + "','" + chkview + "')";
            Db.myExecuteSQL(query);
            lblMsg.Text = "Record saved successfully";

            BindGrid();
        }
        else if (e.CommandName == "Update")
        {

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");

            DropDownList ddldesiEdit = (DropDownList)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("ddldesiEdit");
            DropDownList ddlFrqRPtEdit = (DropDownList)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("ddlFrqRPtEdit");
            TextBox txtminperdayEdit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtminperdayEdit");
            TextBox txtweekEdit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtweekEdit");
            TextBox txtmonEdit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtmonEdit");
            TextBox txtDistinctedit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtDistinctedit");
            TextBox txtRptEdit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtRptEdit");
            TextBox txtcusedit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtcusedit");

            CheckBox chkedit = (CheckBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("chkedit");
            string chkview = "";
            if (chkedit.Checked == true)
            {
                chkview = "1";
            }
            else
            {
                chkview = "0";
            }


            string query = "update  DSR_CustomerVisitTarget set FreqOfRpt='" + ddlFrqRPtEdit.SelectedItem.Text + "',DailyTarget='" + txtminperdayEdit.Text + "',WeeklyTarget='" + txtweekEdit.Text + "',MonthlyTarget='" + txtmonEdit.Text + "',DistinctTarget='" + txtDistinctedit.Text + "',RepeatTarget='" + txtRptEdit.Text + "',NewCustomers='" + txtcusedit.Text + "',ViewScore='" + chkview + "' where TargetID='" + lblID.Text + "'";
            Db.myExecuteSQL(query);
            BindGrid();
            Response.Redirect("TargetDesigMaster.aspx");
            //lblMsg.Text = "Record Updated successfully";

        }

        else if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");

            string query = "Delete from   DSR_CustomerVisitTarget  where TargetId='" + lblID.Text + "'";
            Db.myExecuteSQL(query);
            lblMsg.Text = "Record Deleted successfully";
            BindGrid();

           }
        }
    protected void GRVAutoCLStatusChange_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GRVAutoCLStatusChange.EditIndex = -1;
            BindGrid();
            GRVAutoCLStatusChange.FooterRow.Visible = false;
            Response.Redirect("TargetDesigMaster.aspx");
        }
        //try
        //{
        //    GRVAutoCLStatusChange.ShowFooter = true;
        //    GRVAutoCLStatusChange.FooterRow.Visible = true;

             //    DropDownList ddldesiEdit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("ddldesiEdit")) as DropDownList;
        //    DropDownList ddlFrqRPtEdit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("ddlFrqRPtEdit")) as DropDownList;
        //    TextBox txtminperdayEdit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("txtminperdayEdit")) as TextBox;
        //    TextBox txtweekEdit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("txtweekEdit")) as TextBox;
        //    TextBox txtmonEdit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("txtmonEdit")) as TextBox;
        //    TextBox txtDistinctedit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("txtDistinctedit")) as TextBox;
        //    TextBox txtRptEdit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("txtRptEdit")) as TextBox;
        //    TextBox txtcusedit = (GRVAutoCLStatusChange.Rows[e.RowIndex].FindControl("txtcusedit")) as TextBox;




             //    string Cutname = ddldesiEdit.Text;
        //    string Permeet = ddlFrqRPtEdit.Text;
        //    string email = txtminperdayEdit.Text;
        //    string contno = txtweekEdit.Text;
        //    string desi = txtmonEdit.Text;
        //    string actiondone = txtDistinctedit.Text;
        //    string expbuss = txtRptEdit.Text;
        //    string callstatus = txtcusedit.Text;

             //    int RewardSettingLineID = Convert.ToInt32(GRVAutoCLStatusChange.DataKeys[e.RowIndex].Value.ToString());
        //    DataRow DR = myGlobal.dt_temp.Select("VisitId=" + RewardSettingLineID.ToString()).FirstOrDefault();
        //    if (DR != null)
        //    {
        //        DR["DesigName"] = Cutname;
        //        DR["FreqOfRpt"] = Permeet;
        //        DR["DailyTarget"] = email;
        //        DR["WeeklyTarget"] = contno;
        //        DR["MonthlyTarget"] = desi;
        //        DR["DistinctTarget"] = actiondone;
        //        DR["RepeatTarget"] = expbuss;
        //        DR["NewCustomers"] = callstatus;


             //    }
        //    myGlobal.dt_temp.AcceptChanges();
        //    GRVAutoCLStatusChange.EditIndex = -1;
        //    GRVAutoCLStatusChange.DataSource = myGlobal.dt_temp;
        //    GRVAutoCLStatusChange.DataBind();

             //}


        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on RowUpdating, please retry.." + ex.Message;
        }
    }
    protected void GRVAutoCLStatusChange_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GRVAutoCLStatusChange.EditIndex = -1;
            BindGrid();
            GRVAutoCLStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Cancel RowEdit, please retry.." + ex.Message;
        }
    }
    protected void GRVAutoCLStatusChange_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
          
            GRVAutoCLStatusChange.EditIndex = e.NewEditIndex;
            GRVAutoCLStatusChange.ShowFooter = false;
            GRVAutoCLStatusChange.FooterRow.Visible = false;
           BindGrid();


            //   GRVAutoCLStatusChange.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEdit, please retry.." + ex.Message;
        }
    }

    protected void BindGrid()
    {
       
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();
            DataTable DT = Db.myGetDS("SELECT TargetId,DesigId,DesigName,FreqOfRpt,DailyTarget,WeeklyTarget,MonthlyTarget,DistinctTarget,RepeatTarget,NewCustomers,ViewScore  FROM DSR_CustomerVisitTarget").Tables[0]; ;
            //   DataSet ds = Db.myGetDS("select Distinct(VisitDate),VisitId from DailySalesReports where CreatedBy='" + LoggedInUserName + "'");
            if (DT.Rows.Count > 0)
            {
                GRVAutoCLStatusChange.DataSource = DT;
                GRVAutoCLStatusChange.DataBind();
              
            }
            else
            {
                DataTable TblReward = new DataTable();
                DataColumn colTargetId = TblReward.Columns.Add("TargetId", typeof(Int32));
                DataColumn colDesigId = TblReward.Columns.Add("DesigId", typeof(Int32));
                DataColumn colDesigName = TblReward.Columns.Add("DesigName", typeof(string));
                DataColumn colFreqOfRpt = TblReward.Columns.Add("FreqOfRpt", typeof(string));
                DataColumn colMinPerDay = TblReward.Columns.Add("DailyTarget", typeof(Int32));
                DataColumn colWeeklyTarget = TblReward.Columns.Add("WeeklyTarget", typeof(Int32));
                DataColumn colMonthlyTarget = TblReward.Columns.Add("MonthlyTarget", typeof(Int32));
                // DataColumn colVistpe = TblReward.Columns.Add("VisitType", typeof(string));
                DataColumn colDistinctTarget = TblReward.Columns.Add("DistinctTarget", typeof(string));
                DataColumn colRepeat = TblReward.Columns.Add("RepeatTarget", typeof(string));
                DataColumn colNewCustomers = TblReward.Columns.Add("NewCustomers", typeof(Int32));
                DataColumn colViewScore = TblReward.Columns.Add("ViewScore", typeof(string));

                TblReward.Rows.Add(TblReward.NewRow());
                GRVAutoCLStatusChange.DataSource = TblReward;
                GRVAutoCLStatusChange.DataBind();
                GRVAutoCLStatusChange.Rows[0].Cells.Clear();
                GRVAutoCLStatusChange.Rows[0].Cells.Add(new TableCell());
                //DataRow dr = DT.NewRow();  // add new row

                //dr["TargetId"] = 0;
                //dr["DesigId"] = 0;
                //dr["DesigName"] = "";
                //dr["FreqOfRpt"] = "";
                //dr["DailyTarget"] = 0;
                //dr["WeeklyTarget"] = 0;
                //dr["MonthlyTarget"] = 0;

                //dr["DistinctTarget"] = "";
                //dr["RepeatTarget"] = "";
                //dr["NewCustomers"] = 0;

                //DT.Rows.Add(dr);
                //GRVAutoCLStatusChange.DataSource = DT;
                //GRVAutoCLStatusChange.DataBind();
                //}
            }        
    }
    private void BindGridAddNew()
    {
        try
        {
            DataTable TblReward = new DataTable();
            DataColumn colTargetId = TblReward.Columns.Add("TargetId", typeof(Int32));
            DataColumn colDesigId = TblReward.Columns.Add("DesigId", typeof(Int32));
            DataColumn colDesigName = TblReward.Columns.Add("DesigName", typeof(string));
            DataColumn colFreqOfRpt = TblReward.Columns.Add("FreqOfRpt", typeof(string));
            DataColumn colMinPerDay = TblReward.Columns.Add("DailyTarget", typeof(Int32));
            DataColumn colWeeklyTarget = TblReward.Columns.Add("WeeklyTarget", typeof(Int32));
            DataColumn colMonthlyTarget = TblReward.Columns.Add("MonthlyTarget", typeof(Int32));
            // DataColumn colVistpe = TblReward.Columns.Add("VisitType", typeof(string));
            DataColumn colDistinctTarget = TblReward.Columns.Add("DistinctTarget", typeof(string));
            DataColumn colRepeat = TblReward.Columns.Add("RepeatTarget", typeof(string));
            DataColumn colNewCustomers = TblReward.Columns.Add("NewCustomers", typeof(Int32));
            DataColumn colViewScore = TblReward.Columns.Add("ViewScore", typeof(string));

            TblReward.Rows.Add(TblReward.NewRow());
            GRVAutoCLStatusChange.DataSource = TblReward;
            GRVAutoCLStatusChange.DataBind();
            GRVAutoCLStatusChange.Rows[0].Cells.Clear();
            GRVAutoCLStatusChange.Rows[0].Cells.Add(new TableCell());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindGridAddNew : " + ex.Message;
        }
    }

    protected void GRVAutoCLStatusChange_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BindGrid();
            GRVAutoCLStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Row Deleting " + ex.Message;
        }
    }




    protected void GRVAutoCLStatusChange_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            String sortExp = e.SortExpression.ToString();
            String sortDir = e.SortDirection.ToString();

            String sortExpV = (String)ViewState["sortExpression"];
            String sortDirV = (String)ViewState["sortDirection"];

            if (sortExpV != null && sortExp == sortExpV)
            {
                if (sortDirV == "Asc")
                    ViewState["sortDirection"] = "Desc";
                else
                    ViewState["sortDirection"] = "Asc";
            }
            else
            {
                ViewState["sortExpression"] = sortExp;
                ViewState["sortDirection"] = "Asc";
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in sorting() : " + ex.Message;
        }
    }
    protected void GRVAutoCLStatusChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow && GRVAutoCLStatusChange.EditIndex == e.Row.RowIndex)
           // if (e.Row.RowType == DataControlRowType.DataRow)
            {
              //  if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddldesi1 = (e.Row.FindControl("ddldesiEdit") as DropDownList);
                    Label lbldesigName = (e.Row.FindControl("lbldesigName") as Label);

                    DataSet DsForms = Db.myGetDS("select ID,Designation from designation_master");
                    ddldesi1.DataSource = DsForms.Tables[0];
                    ddldesi1.DataTextField = "Designation";
                    ddldesi1.DataValueField = "ID";

                    ddldesi1.DataBind();
                    ddldesi1.Items.Insert(0, "--Select--");

                    ddldesi1.SelectedItem.Text = lbldesigName.Text;
                    ddldesi1.SelectedItem.Value = lbldesigName.Text;

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowDatBound() : " + ex.Message;
        }
    }
}