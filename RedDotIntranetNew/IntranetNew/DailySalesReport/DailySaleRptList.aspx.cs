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


public partial class IntranetNew_DailySalesReport_DailySaleRptList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            string LoggedInUserName = myGlobal.loggedInUser();
            txtstartdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
              
            fillgv();
        }

    }

    protected void fillgv()
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();

        DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports where IsActive=1 and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
        if (ds.Tables.Count > 0)
        {
            Gvdata.DataSource = ds.Tables[0];
           
            Gvdata.DataBind();

        }

     

        else
        {
            lblMsg.Text = "No Any Record Found";
        }




    }
    protected void Gvdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gvdata.PageIndex = e.NewPageIndex;
        fillgv();
    }

    protected void btnexporttoex_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["DATA"] != null)
            {
                DataTable dt = (DataTable)Session["DATA"];
                string attachment = "attachment; filename=DailySaleRpt_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
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
    protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        //string LoggedInUserName = myGlobal.loggedInUser();
        //if (ddllist.SelectedItem.Text == "Active")
        //{
        //    DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports  where  IsActive=1 and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
        //    if (ds.Tables.Count > 0)
        //    {
        //        Gvdata.DataSource = ds.Tables[0];
        //        Session["DATA"] = ds.Tables[0];
        //        Gvdata.DataBind();

        //    }
        //}
        //else if (ddllist.SelectedItem.Text == "Deleted")
        //{
        //    DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports where  IsActive=0 and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
        //    if (ds.Tables.Count > 0)
        //    {
        //        Gvdata.DataSource = ds.Tables[0];
        //        Session["DATA"] = ds.Tables[0];
        //        Gvdata.DataBind();

        //    }
        //}

        //else if (ddllist.SelectedItem.Text == "ALL" || ddllist.SelectedItem.Text=="--SELECT--")
        //{
        //    DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports   where  CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");

        //   // convert(datetime,Date)as [YYYYMMDD]
        //  //  DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports   where  CreatedOn=convert('" + txtstartdate.Text + "',Date)as  [YYYYMMDD]  and CreatedOn=convert('" + txtEndDate.Text + "',Date)as  [YYYYMMDD] and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
        //    if (ds.Tables.Count > 0)
        //    {
        //        Gvdata.DataSource = ds.Tables[0];
        //        Session["DATA"] = ds.Tables[0];
        //        Gvdata.DataBind();

        //    }
        //}
        
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();
        if (ddllist.SelectedItem.Text == "Active")
        {
            DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports   where  CONVERT(VARCHAR(10), CreatedOn, 101) >= CONVERT(VARCHAR(10), '" + txtstartdate.Text + "', 101) and CONVERT(VARCHAR(10), CreatedOn, 101) <= CONVERT(VARCHAR(10),'" + txtEndDate.Text + "', 101)  and CreatedBy='" + LoggedInUserName + "' and IsActive=1  order by VisitDate desc");
            if (ds.Tables.Count > 0)
            {
                Gvdata.DataSource = ds.Tables[0];
               
                Gvdata.DataBind();

            }
        }
        else if (ddllist.SelectedItem.Text == "Deleted")
        {
            DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports   where  CONVERT(VARCHAR(10), CreatedOn, 101) >= CONVERT(VARCHAR(10), '" + txtstartdate.Text + "', 101) and CONVERT(VARCHAR(10), CreatedOn, 101) <= CONVERT(VARCHAR(10),'" + txtEndDate.Text + "', 101)  and CreatedBy='" + LoggedInUserName + "' and IsActive=0 order by VisitDate desc");
            if (ds.Tables.Count > 0)
            {
                Gvdata.DataSource = ds.Tables[0];
               
                Gvdata.DataBind();

            }
        }

        else if (ddllist.SelectedItem.Text == "ALL" || ddllist.SelectedItem.Text == "--SELECT--")
        {
            DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports   where  CONVERT(VARCHAR(10), CreatedOn, 101) >= CONVERT(VARCHAR(10), '" + txtstartdate.Text + "', 101) and CONVERT(VARCHAR(10), CreatedOn, 101) <= CONVERT(VARCHAR(10),'" + txtEndDate.Text + "', 101)  and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");

            // convert(datetime,Date)as [YYYYMMDD]
            //  DataSet ds = Db.myGetDS("select * from dbo.DailySalesReports   where  CreatedOn=convert('" + txtstartdate.Text + "',Date)as  [YYYYMMDD]  and CreatedOn=convert('" + txtEndDate.Text + "',Date)as  [YYYYMMDD] and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
            if (ds.Tables.Count > 0)
            {
                Gvdata.DataSource = ds.Tables[0];
           
                Gvdata.DataBind();

            }
        }
        
    }
}