using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public partial class IntranetNew_MarketingPlan_MarketingListData : System.Web.UI.Page
{
    string sqlquery = "";
    string LoggedInUserName = myGlobal.loggedInUser();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgv();
            BindDDL();
        }
    }
    private void BindDDL()
    {
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int PLID = Convert.ToInt32(Request.QueryString["GVid"]);
            DataSet ds = Db.myGetDS("select *  from rddCountriesList");

            ddlCountry.DataSource = ds.Tables[0];
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "CountryCode";

            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--SELECT--");


        }
        catch (Exception ex)
        {

        }

    }
    protected void fillgv()
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        DataSet ds2 = Db.myGetDS("exec GetUserAuthorizationForMPlan '" + LoggedInUserName + "'");
        string App = ds2.Tables[0].Rows[0]["Column1"].ToString();

        if (App == "Approver") //APPROVER START
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet ds = Db.myGetDS("select * FROM MarketingPlan where planStatus!='Draft' order by PlanId desc");
            if (ds.Tables.Count > 0)
            {
                GvList.DataSource = ds.Tables[0];
                GvList.DataBind();
                Session["DATA"] = ds.Tables[0];
            }
        }

        else
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //   DataSet ds = Db.myGetDS("select PlanId,SourceOfFund,CountryName,StartDate,EndDate,planStatus,CreatedOn,BalanceFromApp,ApprovedBy,Vendor,VendorApprovedAmt,RDDApprovedAmt,UsedAmount,BalanceAmount,Description,ApprovalStatus FROM MarketingPlan where CreatedBy in (select distinct originator from Marketing_Authentication where  Flag = 'A'  and  ( '" + LoggedInUserName + "' in (select  value from SplitString(Approver,',')))  OR ( '" + LoggedInUserName + "' in ( select  value from SplitString(Originator,',')))) order by PlanId desc");
            lblMsg.Text = "";
            DataSet ds1 = Db.myGetDS("exec GetMarketingDataforOriginator '" + LoggedInUserName + "'");
            if (ds1.Tables.Count > 0)
            {
                GvList.DataSource = ds1.Tables[0];
                Session["DATA"] = ds1.Tables[0];
                GvList.DataBind();
            }
            else
            {
                lblMsg.Text = "You are not authorize user to view marketing plans";

            }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GvList.SelectedRow;
        Label lblid = (Label)row.FindControl("lblplanid");

        string id = lblid.Text;
        Response.Redirect("MarketingPlan-Master.aspx?GVid=" + id);
    }

  

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();     
    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(); 
    }


    public void BindGrid()
    {
      try
        {

            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = "";

            //sql = "select PlanId,SourceOfFund,CountryName,StartDate,EndDate,CreatedOn,ApprovedBy,planStatus,BalanceFromApp,Vendor,VendorApprovedAmt,RDDApprovedAmt,UsedAmount,BalanceAmount,Description,ApprovalStatus FROM MarketingPlan where CreatedBy in (select distinct originator from Marketing_Authentication where  Flag = 'A'  and  ( '" + LoggedInUserName + "' in (select  value from SplitString(Approver,',')))  OR ( '" + LoggedInUserName + "' in ( select  value from SplitString(Originator,',')))) order by PlanId desc ";
          sql = " select * from MarketingPlan ";
          if (ddlstatus.SelectedItem.Text != "--SELECT--" && ddlCountry.SelectedItem.Text == "--SELECT--")
            {
               // sql = sql + " and ApprovalStatus='" + ddlstatus.SelectedItem.Text + "'";
                sql = sql + " where ApprovalStatus='" + ddlstatus.SelectedItem.Text + "' order by PlanId desc";
            }
          else if (ddlCountry.SelectedItem.Text != "--SELECT--" &&  ddlstatus.SelectedItem.Text == "--SELECT--")
          {

              //sql = sql + " and CountryName='" + ddlCountry.SelectedItem.Text + "'";
              sql = sql + " where CountryName='" + ddlCountry.SelectedItem.Text + "' order by PlanId desc";

          }

          else if (ddlCountry.SelectedItem.Text != "--SELECT--" && ddlstatus.SelectedItem.Text != "--SELECT--")
          {

              sql = sql + " where CountryName='" + ddlCountry.SelectedItem.Text + "'  and  ApprovalStatus='" + ddlstatus.SelectedItem.Text + "' order by PlanId desc";
          }
            DataSet ds = Db.myGetDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GvList.DataSource = ds.Tables[0];

                GvList.DataBind();
                GvList.Visible = true;
            }
            else
            {
               // lblMsg.Text = "THERE IS NO DATA FOUND AS PER  STATUS";
                GvList.Visible = false;

            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnexporttoex_Click(object sender, EventArgs e)
    {

        try
        {
            if (Session["DATA"] != null)
            {
                DataTable dt = (DataTable)Session["DATA"];
                string attachment = "attachment; filename=FunnelDeals_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Export : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }
  
    protected void GvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}


