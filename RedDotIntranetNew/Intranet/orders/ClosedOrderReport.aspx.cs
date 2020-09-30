using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Intranet_orders_ClosedOrderReport : System.Web.UI.Page
{
    DataSet dsGlb ; // , drd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lblcnt.Text = "0";
        }
    }

    protected void btnViewData_Click(object sender, EventArgs e)
    {
        lblUseQryNow.Text = "";
        lblFileName.Text = "";
        lblMsg.Text = "";
        lblCnt.Text = "0";
        GridView1.DataSource = null;

        if (checkDates()) //if both dates  are correct
        {

            lblUseQryNow.Text = "Exec [dbo].[GetClosedPOReport] '" + txtFromDate.Text + "','" + txtToDate.Text + "'";

            lblFileName.Text = "ClosedPOReport-" + txtFromDate.Text + "-" + txtToDate.Text;

            Db.constr = myGlobal.getIntranetDBConnectionString();
            GridView1.DataSource = Db.myGetDS(lblUseQryNow.Text).Tables[0];
            GridView1.DataBind();

            lblCnt.Text = GridView1.Rows.Count.ToString();

            if (GridView1.Rows.Count > 0)
                btnReportExl.Enabled = true;
            else
            {
                btnReportExl.Enabled = false;
                lblMsg.Text = "Sorry ! no data available for this period, Try another period.";
            }
        }
    }

    protected void btnReportExl_Click(object sender, EventArgs e)
    {
        lblUseQryNow.Text = "";
        lblFileName.Text = "";
        lblMsg.Text = "";
        //lblCnt.Text = "0";
        //GridView1.DataSource = null;

        if (checkDates()) //if both dates  are correct
        {

            lblUseQryNow.Text = "Exec [dbo].[GetClosedPOReport] '" + txtFromDate.Text + "','" + txtToDate.Text + "'";

            lblFileName.Text = "ClosedPOReport-" + txtFromDate.Text + "-" + txtToDate.Text;

            Db.constr = myGlobal.getIntranetDBConnectionString();
            DataTable pdt = Db.myGetDS(lblUseQryNow.Text).Tables[0];
            ExportToExcel(pdt, lblFileName.Text);
        }
    }

    void ExportToExcel(DataTable dt, string FileName)
    {
        if (dt.Rows.Count > 0)
        {
            string filename = FileName + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);

            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");



            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }
    }
    private Boolean checkDates()
    {
        Boolean flg = true;

        if (flg == true && Util.IsValidDate(txtFromDate.Text.Trim()) == false)
        {
            lblMsg.Text = "Error ! Please enter a valid date in FROM-DATE filed and retry.";
            flg = false;
        }

        if (flg == true && Util.IsValidDate(txtToDate.Text.Trim()) == false)
        {
            lblMsg.Text = "Error ! Please enter a valid date in TO-DATE filed and retry.";
            flg = false;
        }

        if (flg == true && Convert.ToDateTime(txtToDate.Text.Trim()) < Convert.ToDateTime(txtFromDate.Text.Trim()))
        {
            lblMsg.Text = "Error ! TO-DATE can not be smaller than FROM-DATE.";
            flg = false;
        }

        return flg;
    }
}