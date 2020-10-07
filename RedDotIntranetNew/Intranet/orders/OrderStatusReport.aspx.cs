using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Intranet_orders_OrderStatusReport : System.Web.UI.Page
{
    DataSet dsGlb ; // , drd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtPONo.Text = "";
            lblcnt.Text = "0";
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        string tsql = "select refId,refValue,(refValue+'   (' + convert(varchar(10),refId)+ ')') as ShowVal from dbo.processRequest where refValue='"+txtPONo.Text.Trim()+"'"; //fetch first role for the process
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dsGlb = Db.myGetDS(tsql);
        lstOrders.DataSource = dsGlb.Tables[0];
        lstOrders.DataTextField = "ShowVal";
        lstOrders.DataValueField = "refId";
        lstOrders.DataBind();
        lblcnt.Text = lstOrders.Items.Count.ToString();

    }
    protected void lstOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tsql = "select ROW_NUMBER() Over (Order by A.autoindex) As [SrNo], A.*,B.processStatusName as action_Stage from dbo.processStatusTrack A join  dbo.processStatus B on B.processStatusID=A.action_StatusID and A.fk_processID=B.fk_processID where fk_processRequestId=(select processRequestId from dbo.processRequest where refId=" + lstOrders.SelectedValue.ToString() + ")  order by autoindex";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        GridEscalationHistory.DataSource = Db.myGetDS(tsql).Tables[0];
        GridEscalationHistory.DataBind();
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

            lblUseQryNow.Text = "Exec [dbo].[GetClosedPOStatusReport] '" + txtFromDate.Text + "','" + txtToDate.Text + "'";

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