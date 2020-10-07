using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class IntranetNew_BPStatus_PVNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // select CurrCode from SAPAE.dbo.OCRN -- Currencies
            if (!IsPostBack)
            {

                Bindddl();
                SetInitialRow();
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = "Error occured in Page_Load : " + Ex.Message;
        }

    }

    public void Bindddl()
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = "select '--Select--' As Country union all select Country from tejSAP.dbo.rddcountrieslist ;  select '--Select--' As Currency union all select CurrCode from SAPAE.dbo.OCRN";
            DataSet DS = new DataSet();
            DS = Db.myGetDS(sql);
            if (DS.Tables.Count > 0)
            {
                ddlCountry.DataSource = DS.Tables[0];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "Country";
                ddlCountry.DataBind();

                if (DS.Tables.Count > 1)
                {
                    ddlCurrency.DataSource = DS.Tables[1];
                    ddlCurrency.DataTextField = "Currency";
                    ddlCurrency.DataValueField = "Currency";
                    ddlCurrency.DataBind();
                }
            }

            ddlStatus.SelectedItem.Text = "Open";
            ddlStatus.SelectedItem.Value = "Open";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Bindddl : " + ex.Message;
        }
    }

    private void SetInitialRow()
    {
        try
        {
            //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            //txtrefno.Text = Db.myExecuteScalar2("Select dbo.GetMarketingRefNo('" + ddlCountry.SelectedItem.Value + "')");
            //string refno = txtrefno.Text;
            lblMsg.Text = "";
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("PVLineId", typeof(int)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("FilePath", typeof(string)));

            dr = dt.NewRow();
            dr["PVLineId"] = "0";
            dr["Date"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Amount"] = "0";
            dr["Remarks"] = string.Empty;
            dr["FilePath"] = string.Empty;

            dt.Rows.Add(dr);
            gvPVLines.DataSource = dt;
            gvPVLines.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured SetInitialRow() : " + ex.Message;
        }
    }
    /// <summary>
    /// This function is to ADD new row in gridview , If SrNo is 0 then Add Row Event, If SrNo is greater than 0 then Delete Row Event
    /// </summary>
    /// <param name="SrNo"></param>
    private void AddNewRowToGrid(int SrNo)
    {
        try
        {

            lblMsg.Text = "";
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("PVLineId", typeof(int)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("FilePath", typeof(string)));

            bool AddEmpltyRow = true;
            for (int i = 0; i < gvPVLines.Rows.Count; i++)
            {
                GridViewRow row = gvPVLines.Rows[i];

                Label LoopSrNo = (Label)row.FindControl("lblsrno");
                Label lblPVLineId = (Label)row.FindControl("lblPVLineId");
                TextBox txtdate = (TextBox)row.FindControl("txtdate");
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblFilePath = (Label)row.FindControl("lblFilePath");

                FileUpload grvfupload = (FileUpload)row.FindControl("fUploadAddattachment");

                if (string.IsNullOrEmpty(txtDescription.Text) && string.IsNullOrEmpty(txtdate.Text) && !string.IsNullOrEmpty(txtgvamt.Text))
                {
                    AddEmpltyRow = false;
                }
                else
                {
                    if (SrNo != Convert.ToInt32(LoopSrNo.Text))
                    {
                        dr = dt.NewRow();
                        dr["PVLineId"] = lblPVLineId.Text;
                        dr["Date"] = txtdate.Text;
                        dr["Description"] = txtDescription.Text;
                        dr["Amount"] = txtgvamt.Text;
                        dr["Remarks"] = txtRemarks.Text;
                        if (grvfupload.HasFile)
                        {
                            dr["FilePath"] = grvfupload.FileName;
                        }
                        else
                        {
                            dr["FilePath"] = lblFilePath.Text;
                        }
                        dt.Rows.Add(dr);
                    }

                }
            }
            if (AddEmpltyRow == true && SrNo == 0)
            {
                dr = dt.NewRow();
                dr["PVLineId"] = "0";
                dr["Date"] = string.Empty;
                dr["Description"] = string.Empty;
                dr["Amount"] = "0";
                dr["Remarks"] = string.Empty;
                dr["FilePath"] = string.Empty;
                dt.Rows.Add(dr);
                gvPVLines.DataSource = dt;
                gvPVLines.DataBind();
            }

            if (gvPVLines.Rows.Count > 1 && SrNo > 0)
            {
                gvPVLines.DataSource = dt;
                gvPVLines.DataBind();
            }

            //if (gvPVLines.Rows.Count > 1 || AddEmpltyRow==true )
            //{
            //    gvPVLines.DataSource = dt;
            //    gvPVLines.DataBind();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured AddNewRowToGrid() : " + ex.Message;
        }
    }


    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlCountry.SelectedItem.Text != "--Select--")
            {
                txtRefNo.Text = Db.myExecuteScalar2("select dbo.GetPVRefNo('" + ddlCountry.SelectedItem.Text + "') As RefNo");
            }
            else
            {
                txtRefNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured ddlCountry_SelectedIndexChanged() : " + ex.Message;
        }
    }



   
}