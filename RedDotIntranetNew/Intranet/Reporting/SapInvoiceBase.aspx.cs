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

public partial class Intranet_Reporting_SapInvoiceBase : System.Web.UI.Page
{
    string qry;
    int InvIdNow=0;

    //public DataTable RecordSetToDataTable(ADODB.Recordset objRS)
    //{
    //    System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();
    //    DataTable objDT = new DataTable();
    //    objDA.Fill(objDT, objRS);
    //    return objDT;

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        
        if (!IsPostBack)
        {
            Page.Title = " Sap Invoice Base";
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
            Response.Redirect("SAPInvoiceReport.aspx?qType=pdf&qDb=" + ddlDB.SelectedValue.ToString() + "&qInvID=" + InvIdNow.ToString() + "&qdocTypeID=" + "13" + "&qExtParam=" + "Y");

        }

    }
     protected void btnReportExl_Click1(object sender, EventArgs e)
     {
         if (checkDates()) //if both dates  are correct
         {
             Response.Redirect("SAPInvoiceReport.aspx?qType=excel&qDb=" + ddlDB.SelectedValue.ToString() + "&qInvID=" + InvIdNow.ToString() + "&qdocTypeID=" + "13" + "&qExtParam=" + "Y");
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


         if (txtInvID.Text.Trim()=="")
         {
             lblMsg.Text = "Error ! Please enter a valid Numeric in Invoice ID Filed,retry.";
             flg = false;
         }

         if (!Util.isValidNumber(txtInvID.Text.Trim()))
         {
             lblMsg.Text = "Error ! Please enter a valid Numeric in Invoice ID Filed,retry.";
             flg = false;
         }
         else
         {
             InvIdNow =Convert.ToInt32(txtInvID.Text.Trim());
         }


         if (InvIdNow<=0)
         {
             lblMsg.Text = "Error ! Please enter a valid Non Negative Numeric in Invoice ID Filed,retry.";
             flg = false;
         }

         return flg;
     }
 }