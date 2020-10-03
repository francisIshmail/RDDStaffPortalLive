using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;

public partial class Intranet_Reporting_SapIncomeStatementBase : System.Web.UI.Page
{
    string qry;

    public DataTable RecordSetToDataTable(ADODB.Recordset objRS)
    {
        System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();
        DataTable objDT = new DataTable();
        objDA.Fill(objDT, objRS);
        return objDT;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        
        if (!IsPostBack)
        {
            Page.Title = " Satement Base";
            Db.LoadDDLsWithConNew(ddlDB, "select * from dbo.SqlConnectionServers where databaseName Not like 'websiteDb%'", "Country", "CountryCode2", myGlobal.ConnectionString);

            if (ddlDB.Items.Count > 0)
            {
                ddlDB.SelectedIndex = 0;
                lblDbCount.Text = (ddlDB.Items.Count - 1).ToString();
            }
            else
            {
                ddlDB.Items.Add("No Items");
                ddlDB.SelectedIndex = 0;
                lblDbCount.Text = "0";
            }

           // BindGrid();
        }
    }
       

	protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindGrid();
        lblMsg.Text = ddlDB.SelectedItem.Value.ToString();
	}

     protected void btnReport_Click1(object sender, EventArgs e)
    {

        if (checkDates())  //if both dates  are correct
        {
            Response.Redirect("SAPIncomeStatementReport.aspx?qType=pdf&qPeriod=" + "xxx" + "&qDb=" + ddlDB.SelectedValue.ToString() + "&qFromDt=" + txtFromDate.Text.Trim() + "&qToDt=" + txtToDate.Text.Trim());
        }

    }
     protected void btnReportExl_Click1(object sender, EventArgs e)
     {
         if (checkDates()) //if both dates  are correct
         {
             Response.Redirect("SAPIncomeStatementReport.aspx?qType=excel&qPeriod=" + "xxx" + "&qDb=" + ddlDB.SelectedValue.ToString() + "&qFromDt=" + txtFromDate.Text.Trim() + "&qToDt=" + txtToDate.Text.Trim());
         }
     }
     
    private Boolean checkDates()
     {
         Boolean flg = true;


         if (flg == true && ddlDB.SelectedIndex == 0)
             {
                 lblMsg.Text = "Error ! Please select Database/Country from the list.";
                 flg = false;
             }

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