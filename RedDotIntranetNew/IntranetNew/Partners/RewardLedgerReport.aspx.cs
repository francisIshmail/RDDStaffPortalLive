using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using OfficeOpenXml;

public partial class IntranetNew_Partners_RewardLedgerReport : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDatabaseDDL(ddlDatabase);
            LblRewardLedger.Visible = false;
            lblRewardSummery.Visible = false;
            BtnDownloadLedger.Enabled = false;
            lblMsg.Text = "";
        }
    }

    private void BindDatabaseDDL(DropDownList ddlDatabase)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlDatabase, "Select '--Select--' as Name, '00' as 'Name' Union select Name,Name from sys.databases Where Name in ('SAPAE','SAPTZ','SAPUG','SAPKE','SAPZM')", "Name", "Name", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlDatabase.Items.Count > 0)
                ddlDatabase.SelectedIndex = 0;
            else
            {
                ddlDatabase.Items.Add("No Rows");
                ddlDatabase.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindDatabaseDDL():" + ex.Message;
        }
    }

    private void BindCustomers(DropDownList ddlCustomers)
    {
        try
        {
            string SQL = " Select '00' as CardCode, '--Select--' as CardName Union Select distinct t1.CardCode,t1.CardName from tejSAP.dbo.Reward_Points t0 Join [" + ddlDatabase.SelectedItem.Text + "].dbo.OCRD t1 On t0.CardCode COLLATE DATABASE_DEFAULT=t1.CardCode  COLLATE DATABASE_DEFAULT And t0.TransType In ('13','14') ";

            Db.LoadDDLsWithCon(ddlCustomers, SQL , "CardName", "CardCode", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlCustomers.Items.Count > 0)
                ddlCustomers.SelectedIndex = 0;
            else
            {
                ddlCustomers.Items.Add("No Rows");
                ddlCustomers.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindCustomers():" + ex.Message;
        }
    }

    protected void ddlDatabase_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtCardCode.Text = "";
            if (ddlDatabase.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Database..";
                return;
            }

            BindCustomers(ddlCustomer);

            try
            {
                LblRewardLedger.Visible = false;
                lblRewardSummery.Visible = false;
                BtnDownloadLedger.Enabled = false;
                grvRewardSummery.Visible = false;
                grvDetailLedger.Visible = false;
            }
            catch { }

        }
        catch (Exception ex )
        {
            lblMsg.Text = "Error occured in BindCustomers():" + ex.Message;
        }
    }

    protected void BtnGetLedger_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDatabase.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Database";
                return;
            }
            if (ddlCustomer.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Customer";
                return;
            }

            string FromDate = txtFromDt.Text.Trim();
            string ToDate = txtToDt.Text.Trim();

            if (string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
            {
                lblMsg.Text = "Please either enter both(from & To ) Dates Or none";
                return;
            }

            if (!string.IsNullOrEmpty(FromDate) && string.IsNullOrEmpty(ToDate))
            {
                lblMsg.Text = "Please either enter both(from & To ) Dates Or none";
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS;
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
            {
                DS = Db.myGetDS("  EXEC  getRewardPointLedgerReport  '" + ddlDatabase.SelectedItem.Text + "','" + txtCardCode.Text + "','" + FromDate + "','" + ToDate + "','M'");
            }
            else
            {
                DS = Db.myGetDS("  EXEC  getRewardPointLedgerReport  '" + ddlDatabase.SelectedItem.Text + "','" + txtCardCode.Text + "',NULL,NULL,'M'");
            }

            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    grvRewardSummery.Visible = true;
                    grvDetailLedger.Visible = true;

                    grvRewardSummery.DataSource = DS.Tables[0];
                    grvRewardSummery.DataBind();

                    grvDetailLedger.DataSource = DS.Tables[1];
                    grvDetailLedger.DataBind();

                    LblRewardLedger.Visible = true;
                    lblRewardSummery.Visible = true;
                    BtnDownloadLedger.Enabled = true;
                }
            }
            else
            {
                lblMsg.Text = "No data found....";
                LblRewardLedger.Visible = false;
                lblRewardSummery.Visible = false;
                BtnDownloadLedger.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnGetLedger_Click():" + ex.Message;
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCustomer.SelectedItem.Text != "--Select--")
            {
                txtCardCode.Text = ddlCustomer.SelectedItem.Value;
            }
            try
            {
                LblRewardLedger.Visible = false;
                lblRewardSummery.Visible = false;
                BtnDownloadLedger.Enabled = false;
                grvRewardSummery.Visible = false;
                grvDetailLedger.Visible = false;
            }
            catch { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ddlCustomer_SelectedIndexChanged():" + ex.Message;
        }
    }

    void ExportToExcel(string FileName)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        DataSet DS;
        if (!string.IsNullOrEmpty(txtFromDt.Text) && !string.IsNullOrEmpty(txtToDt.Text))
        {
            DS = Db.myGetDS("  EXEC  getRewardPointLedgerReport  '" + ddlDatabase.SelectedItem.Text + "','" + txtCardCode.Text + "','" + txtFromDt.Text + "','" + txtToDt.Text + "','M'");
        }
        else
        {
            DS = Db.myGetDS("  EXEC  getRewardPointLedgerReport  '" + ddlDatabase.SelectedItem.Text + "','" + txtCardCode.Text + "',NULL,NULL,'M'");
        }
        if (DS.Tables.Count > 0)
        {
            DataTable dt = DS.Tables[1];
            string attachment = "attachment; filename=" + FileName + ".xls";
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

    protected void BtnDownloadLedger_Click(object sender, EventArgs e)
    {
        try
        {
            if (grvDetailLedger.Rows.Count > 0)
            {
                ExportToExcel(txtCardCode.Text + " Ledger report");
            }
            else
            {
                lblMsg.Text = " No Ledger report to download";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " BtnDownloadLedger_Click()  : "+ ex.Message ;
        }
    }
}