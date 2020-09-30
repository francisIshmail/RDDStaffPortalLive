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
using System.Configuration;
using System.IO;
using System.Data;


public partial class IntranetNew_DailySalesReport_ViewReportNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (!IsPostBack)
            {

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                //int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='ViewReportNew.aspx' and t1.IsActive=1");
               // if (count > 0)
                //{
                    BindDataOnPageLoad();
              //  }
               // else
               // {
                    //Response.Redirect("Default.aspx?UserAccess=0&FormName=Read Report");
                //}
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = "Failed OnPage_Load :- " + ex.Message;
        }

    }

    private void BindDataOnPageLoad()
    {
        string LoggedInUserName = myGlobal.loggedInUser();

        if (rdbUnread.Checked)
        {
            DataSet ds = Db.myGetDS("select Id from tejSalespersonMap where Membershipuser = '" + LoggedInUserName + "'");
            string managerid = ds.Tables[0].Rows[0]["Id"].ToString();

            int Reccount = Db.myExecuteScalar(" EXEC DSR_GetUnReadReportCount  "+ managerid);
            lblunread.Text = Convert.ToInt32(Reccount).ToString();

            ds = Db.myGetDS("exec DSR_EMPRPTUNREAD '" + managerid + "'");
            GvdisplayRecord.DataSource = ds;
            GvdisplayRecord.DataBind();
            ShowingGroupingDataInGridView(GvdisplayRecord.Rows, 0, 3);
        }
        else if (rdbRead.Checked)
        {
            DataSet ds = Db.myGetDS("select Id from tejSalespersonMap where Membershipuser = '" + LoggedInUserName + "'");
            string managerid = ds.Tables[0].Rows[0]["Id"].ToString();

            ds = Db.myGetDS("exec DSR_EMPRPTREAD '" + managerid + "'");

            GvdisplayRecord.DataSource = ds;
            GvdisplayRecord.DataBind();
            ShowingGroupingDataInGridView(GvdisplayRecord.Rows, 0, 3);
        }

    }

    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)  
        {  
            if (totalColumns == 0) return;  
            int i, count = 1;
            if (gridViewRows.Count > 0)
            {
                var ctrl = gridViewRows[0].Cells[startIndex];
                for (i = 1; i < gridViewRows.Count; i++)
                {
                    TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
                    if (ctrl.Text == nextTbCell.Text)
                    {
                        count++;
                        nextTbCell.Visible = false;
                    }
                    else
                    {
                        if (count > 1)
                        {
                            ctrl.RowSpan = count;
                        }
                        count = 1;
                        ctrl = gridViewRows[i].Cells[startIndex];
                    }
                }
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                }
                count = 1;
            }
            
    }

    protected void Edit(object sender, EventArgs e)
    {

        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            foreach (GridViewRow g1 in GvdisplayRecord.Rows)
            {
                lblmessage.Text = "";
                string createdby = ((((Label)(((LinkButton)sender).NamingContainer as GridViewRow).FindControl("lblcreatedby")).Text));
                string date = ((((Label)(((LinkButton)sender).NamingContainer as GridViewRow).FindControl("lblvisitdate")).Text));
                string IsRead = ((((Label)(((LinkButton)sender).NamingContainer as GridViewRow).FindControl("lblisread")).Text));

                int IsLoginUserIsPMForSalesEmp = Db.myExecuteScalar(" exec DSR_CheckIfLoginUserIsAssignedAsPM '" + myGlobal.loggedInUser() + "','" + createdby + "'");
                Session["IsLoginUserIsPMForSalesEmp"] = IsLoginUserIsPMForSalesEmp;

                if (IsRead == "False")
                {

                    DataSet ds = Db.myGetDS("exec DSR_DATEWISERPT '" + createdby + "', '" + date + "'");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GvEmpDatewiseRpt.DataSource = ds;
                        GvEmpDatewiseRpt.DataBind();
                        btnSave.Visible = true;
                        foreach (GridViewRow gvr in GvEmpDatewiseRpt.Rows)
                        {
                            if (IsLoginUserIsPMForSalesEmp == 0) // if login user is cm  them cm comment should be enable & pm comment should be disable
                            {
                                ((TextBox)gvr.FindControl("txtcomments")).Enabled = true;
                                ((TextBox)gvr.FindControl("txtPMcomments")).Enabled = false ;
                            }
                            else
                            {
                                ((TextBox)gvr.FindControl("txtcomments")).Enabled = false;
                                if (((TextBox)gvr.FindControl("txtPMcomments")).Text == "")
                                {
                                    ((TextBox)gvr.FindControl("txtPMcomments")).Enabled = true;
                                }
                                else
                                {
                                    ((TextBox)gvr.FindControl("txtPMcomments")).Enabled = false;
                                }
                            }
                        }
                    }

                    btnSave.Enabled = true;

                    popup.Show();
                }
                else if (IsRead == "True")
                {

                    DataSet ds = Db.myGetDS("exec DSR_DATEWISERPTREAD '" + createdby + "', '" + date + "'");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GvEmpDatewiseRpt.DataSource = ds;
                        GvEmpDatewiseRpt.DataBind();
                        btnSave.Visible = false;

                        if (IsLoginUserIsPMForSalesEmp > 0) // if logged in User is PM them allow him/her to update comments
                        {
                            foreach (GridViewRow gvr in GvEmpDatewiseRpt.Rows)
                            {
                                ((TextBox)gvr.FindControl("txtcomments")).Enabled = false;
                                if (((TextBox)gvr.FindControl("txtPMcomments")).Text == "")
                                {
                                    ((TextBox)gvr.FindControl("txtPMcomments")).Enabled = true;
                                    btnSave.Visible = true;
                                }
                                else
                                {
                                    ((TextBox)gvr.FindControl("txtPMcomments")).Enabled = false;
                                }
                            }
                            
                        }
                        //foreach (GridViewRow gvr in GvEmpDatewiseRpt.Rows)
                        //{
                        //    ((TextBox)gvr.FindControl("txtcomments")).Enabled = false;
                        //}
                    }
                  popup.Show();

                }
            }

        }
    }

    protected void Save(object sender, EventArgs e)
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            string Visit_Ids = string.Empty;

            if (Session["IsLoginUserIsPMForSalesEmp"] != null && Convert.ToInt32(Session["IsLoginUserIsPMForSalesEmp"]) > 0)
            {

                foreach (GridViewRow g2 in GvEmpDatewiseRpt.Rows)
                {
                    string Visitid = (g2.FindControl("lblVisitId") as Label).Text;
                    string comment = (g2.FindControl("txtPMcomments") as TextBox).Text;
                    if (!string.IsNullOrEmpty(comment))
                    {
                        string sql = "update DailySalesReports set PM_Comments='" + comment + "',PM_ReportReadBy='" + myGlobal.loggedInUser() + "',PM_ReportReadOn=getdate() where VisitId='" + Visitid + "'";
                        Db.myExecuteSQL(sql);

                        lblmessage.Text = "Save Succesfully";
                        lblmessage.ForeColor = Color.Red;
                        popup.Show();

                        if (string.IsNullOrEmpty(Visit_Ids))
                        {
                            Visit_Ids = Visitid;
                        }
                        else
                        {
                            Visit_Ids = Visit_Ids + "," + Visitid;
                        }
                    }
                }

                #region "Send Mail "
                try
                {
                    if (!string.IsNullOrEmpty(Visit_Ids))
                    {
                        Db.myExecuteSQL("EXEC DSR_SendReadReportEmailByPM '" + myGlobal.loggedInUser() + "','" + Visit_Ids + "'");
                    }
                }
                catch { }

                #endregion

            }
            else  /// Below code will be executed when manager enters his remarks
            {
                foreach (GridViewRow g2 in GvEmpDatewiseRpt.Rows)
                {
                    string Visitid = (g2.FindControl("lblVisitId") as Label).Text;
                    string comment = (g2.FindControl("txtcomments") as TextBox).Text;
                    if (string.IsNullOrEmpty(comment))
                    {
                        lblmessage.Text = "Kindly Enter Comment";
                        lblmessage.ForeColor = Color.Red;
                        popup.Show();
                    }
                    else
                    {
                        string sql = "update DailySalesReports set IsRead=1,Comments='" + comment + "',ReportReadBy='" + myGlobal.loggedInUser() + "',ReportReadOn=getdate() where VisitId='" + Visitid + "'";
                        Db.myExecuteSQL(sql);

                        lblmessage.Text = "Save Succesfully";
                        lblmessage.ForeColor = Color.Red;
                        btnSave.Enabled = false;
                        popup.Show();
                    }

                    if (string.IsNullOrEmpty(Visit_Ids))
                    {
                        Visit_Ids = Visitid;
                    }
                    else
                    {
                        Visit_Ids = Visit_Ids + "," + Visitid;
                    }
                }

                #region "Send Mail "
                try
                {
                    if (!string.IsNullOrEmpty(Visit_Ids))
                    {
                        Db.myExecuteSQL("EXEC DSR_SendReadReportEmail '" + myGlobal.loggedInUser() + "','" + Visit_Ids + "'");
                    }
                }
                catch { }

                #endregion
            }

           // BindDataOnPageLoad();

        }
        catch (Exception ex)
        {
            lblmessage.Text = "Failed () :- " + ex.Message;
        }
    }
   

}