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
        int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='TargetDesigMasterNew.aspx' and t1.IsActive=1");
        if (count > 0)
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string LoggedInUserName = myGlobal.loggedInUser();
                DataSet ds = null;
                BindDDL();
                //   BindGrid();
            }
        }
        else
        {
            Response.Redirect("Default.aspx?UserAccess=0&FormName=Setup - Reporting Frequency And Targets ");
        }
    }

    private void BindDDL()
    {
        lblMsg.Text = "";
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        DataSet DS = Db.myGetDS("select * from  rddcountrieslist");
        ddlCountry.DataSource = DS;// Table [2] for Countries
        ddlCountry.DataTextField = "country";
        ddlCountry.DataValueField = "countrycode";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, "--SELECT--");

    }

    protected void GRVAutoCLStatusChange_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GRVAutoCLStatusChange_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GRVAutoCLStatusChange_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void GRVAutoCLStatusChange_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GRVAutoCLStatusChange_Sorting(object sender, GridViewSortEventArgs e)
    {


    }

    protected void GRVAutoCLStatusChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblEmpidd = (e.Row.FindControl("lblEmpId") as Label);
                ListBox ddEmail = (e.Row.FindControl("lstEmail") as ListBox);
                ListBox lstemailread = (e.Row.FindControl("lstEmailRead") as ListBox);

                DataSet DS_TM_HODEmail = Db.myGetDS("exec DR_RptEmailAuthority    "+ lblEmpidd.Text );
                if (DS_TM_HODEmail.Tables.Count > 0)
                {
                    if (DS_TM_HODEmail.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < DS_TM_HODEmail.Tables[0].Rows.Count; i++)
                        {

                            ddEmail.Items.Add(new ListItem(DS_TM_HODEmail.Tables[0].Rows[i]["email"].ToString(), DS_TM_HODEmail.Tables[0].Rows[i]["email"].ToString()));
                            //lstemailread.Items.Add(new ListItem(DsForms.Tables[0].Rows[i]["email"].ToString(), DsForms.Tables[0].Rows[i]["email"].ToString()));
                        }

                        string id = lblEmpidd.Text;
                        DataSet ds = Db.myGetDS("select SendReportTO From DSR_ReportingFreqTarget where EmpId='" + id + "'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string email = ds.Tables[0].Rows[0]["SendReportTO"].ToString();
                            string[] Arry = email.Split(',');

                            foreach (string emailarray in Arry)
                            {

                                foreach (ListItem i in ddEmail.Items)
                                {
                                    if (i.Value == emailarray)
                                    {
                                        i.Selected = true;
                                    }
                                }

                            }
                        }

                    }

                }
                
                //DataSet DsForm = Db.myGetDS("exec DR_ReadEmailAuthority " + lblEmpidd.Text );
                if (DS_TM_HODEmail.Tables.Count > 1)
                {

                    if (DS_TM_HODEmail.Tables[1].Rows.Count > 0)
                    {

                        for (int i = 0; i < DS_TM_HODEmail.Tables[1].Rows.Count; i++)
                        {
                            lstemailread.Items.Add(new ListItem(DS_TM_HODEmail.Tables[1].Rows[i]["email"].ToString(), DS_TM_HODEmail.Tables[1].Rows[i]["email"].ToString()));
                        }
                    
                        DataSet ds = Db.myGetDS("select ReportMustReadBy From DSR_ReportingFreqTarget where EmpId='" + lblEmpidd.Text + "'");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string emailrd = ds.Tables[0].Rows[0]["ReportMustReadBy"].ToString();
                            string[] Arryy = emailrd.Split(',');

                            foreach (string emailarra in Arryy)
                            {
                                foreach (ListItem m in lstemailread.Items)
                                {
                                    if (m.Value == emailarra)
                                    {
                                        m.Selected = true;
                                    }
                                }

                            }
                        }
                    }

                }

                }
            }
        
        catch (Exception ex)
        {
            // lblMsg.Text = "Error occured in RowDatBound() : " + ex.Message;
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        DataTable DT = Db.myGetDS("exec DSR_GetCountryWiseReportingFrequencyAndTrgets '" + ddlCountry.SelectedValue + "'").Tables[0]; ;
        if (DT.Rows.Count > 0)
        {
            GRVAutoCLStatusChange.DataSource = DT;
            GRVAutoCLStatusChange.DataBind();

        }
        else 
        {
            GRVAutoCLStatusChange.DataSource =null;
            GRVAutoCLStatusChange.DataBind();
            lblMsg.Text = "No Data Found..";
        }
    }

    protected void btInsert_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow g1 in GRVAutoCLStatusChange.Rows)
        {
            string Empid = (g1.FindControl("lblEmpid") as Label).Text;
            string desigId = (g1.FindControl("lblDesigId") as Label).Text;
            string Visitpermonth = (g1.FindControl("txtvisiepermonth") as TextBox).Text;
            string freofrpt = (g1.FindControl("ddlFrqRPtEdit") as DropDownList).SelectedItem.Text;

            ListBox emailRead = (g1.FindControl("lstEmailRead") as ListBox);
            ListBox email = (g1.FindControl("lstEmail") as ListBox);
            {
                string emailss = string.Empty;
                foreach (ListItem i in email.Items)
                {
                    if (i.Selected == true)
                    {
                        emailss += i.Text + ",";
                    }
                }

                string emaiRead = string.Empty;
                foreach (ListItem i in emailRead.Items)
                {
                    if (i.Selected == true)
                    {
                        emaiRead += i.Text + ",";
                    }
                }



                int reccount = Db.myExecuteScalar("select count(EmpId) from DSR_ReportingFreqTarget where EmpId='" + Empid + "' and countrycode='" + ddlCountry.SelectedValue + "'");
                if (reccount > 0)
                {
                    string sql = "update DSR_ReportingFreqTarget set VisitPerMonth='" + Visitpermonth + "',freqOfRpt='" + freofrpt + "',SendReportTo='" + emailss + "',ReportMustReadBy='"+emaiRead+"' where EmpId='" + Empid + "'";
                    Db.myExecuteSQL(sql);
                }
                else
                {
                    string sqlquery = "insert into DSR_ReportingFreqTarget(EmpId,DesigId,VisitPerMonth,freqOfRpt,SendReportTo,countrycode,CreatedOn,ReportMustReadBy)values('" + Empid + "','" + desigId + "','" + Visitpermonth + "','" + freofrpt + "','" + emailss + "','" + ddlCountry.SelectedValue + "',getdate(),'" + emaiRead + "')";
                    Db.myExecuteSQL(sqlquery);
                }


               // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Save successfully.'); </script>");
              
               lblMsg.Text = "Record Save successfully";
               lblMsg.ForeColor = Color.Red;
            
            }
        }
    }
   
}