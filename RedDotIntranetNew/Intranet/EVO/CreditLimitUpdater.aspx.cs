using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Intranet_EVO_CreditLimitUpdater : System.Web.UI.Page
{
    int rws = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        btnUpdate.Attributes.Add("onClick", "return getConfirmation();");

        if (!IsPostBack)
        {
            Db.LoadDDLsWithCon(ddlDB, "select * from dbo.SqlConnectionServers where databaseName Not like 'websiteDb%'", "databaseName", "CountryCode", myGlobal.ConnectionString);

            if (ddlDB.Items.Count > 0)
            {
                ddlDB.SelectedIndex = 0;
                lblDbCount.Text = ddlDB.Items.Count.ToString();
            }
            else
            {
                ddlDB.Items.Add("No Items");
                ddlDB.SelectedIndex = 0;
                lblDbCount.Text = "0";
            }

            BindGrid();


            //select a.histId,a.prevRate,a.currRate,a.databaseName,a.updatedByUser,a.lastModified,b.idCurrencyHist,b.NewRate from (select autoindex as histId,1 as 'jid',prevRate,currRate,databaseName,updatedByUser,lastModified from tej.dbo.CreditLimitUpdateHistory where autoindex=(select max(autoindex) from tej.dbo.CreditLimitUpdateHistory where lower(databaseName)='Triangle')) as a join (select 1 as 'jid',idCurrencyHist,fSellRate as NewRate from [Triangle].dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from [Triangle].dbo.CurrencyHist)) as b on a.jid=b.jid
            //gets
            // histId prevRate  currRate    databaseName User  lastModified currencyid  Newrate
            // 4      10.00000	20.00000	Triangle	Vishav	 date           26	        20
        }
    }
    protected void BindGrid()
    {
        String summarySQL;
        DataSet ds;

        //summarySQL = "select a.histId,a.prevRate,a.currRate,a.databaseName,a.updatedByUser,a.lastModified,b.idCurrencyHist,b.NewRate from (select autoindex as histId,1 as 'jid',prevRate,currRate,databaseName,updatedByUser,lastModified from tej.dbo.CreditLimitUpdateHistory where autoindex=(select max(autoindex) from tej.dbo.CreditLimitUpdateHistory where lower(databaseName)='" + ddlDB.SelectedItem.Text + "')) as a join (select 1 as 'jid',idCurrencyHist,fSellRate as NewRate from [" + ddlDB.SelectedItem.Text + "].dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from [" + ddlDB.SelectedItem.Text + "].dbo.CurrencyHist)) as b on a.jid=b.jid";
        
        summarySQL = "select autoindex as histId,prevRate,currRate,databaseName,updatedByUser,lastModified from tej.dbo.CreditLimitUpdateHistory where autoindex=(select max(autoindex) from tej.dbo.CreditLimitUpdateHistory where lower(databaseName)='" + ddlDB.SelectedItem.Text + "')";
        summarySQL +=" ; select idCurrencyHist,fSellRate as NewRate from [" + ddlDB.SelectedItem.Text + "].dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from [" + ddlDB.SelectedItem.Text + "].dbo.CurrencyHist)";

        //select autoindex as histId,prevRate,currRate,databaseName,updatedByUser,lastModified from tej.dbo.CreditLimitUpdateHistory where autoindex=(select max(autoindex) from tej.dbo.CreditLimitUpdateHistory where lower(databaseName)='Triangle')
        //select idCurrencyHist,fSellRate as NewRate from [Triangle].dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from [Triangle].dbo.CurrencyHist)

       //Db.constr= myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString());

       if (ddlDB.SelectedValue.ToString() == "KE")
           Db.constr = myGlobal.getConnectionStringForDB("KEModify");
       else
           Db.constr = myGlobal.getConnectionStringForDB("TZModify");
  
       ds = Db.myGetDS(summarySQL);

       Grid1.DataSource = ds.Tables[0];
        Grid1.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
            lblNoHistMsg.Visible = false;
        else
            lblNoHistMsg.Visible = true;


        if (ds.Tables[1].Rows.Count > 0)
        {
            lblRate.Text = " " + ds.Tables[1].Rows[0]["NewRate"].ToString() + " ";
            lblRateId.Text = ds.Tables[1].Rows[0]["idCurrencyHist"].ToString();
        }
        else
        {
            lblRate.Text = "0";
            lblRateId.Text = "0";
        }
    }
    protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //javascript validation attribute added on page load for confirmation
        
        //check for the valid login and the permission
        string loggiRole = "creditLimitUpdate";
        if (!myGlobal.isCurrentUserOnRole(loggiRole))
        {
            Message.Show(this,"Sorry! update permission denied to current user for this application");
            return;
        }
        
        lblMsg.Text = "";
        rws = 0;

        int procedureSuccess = 0;      //procedure returns 1 if success, 0 if failed

        try
        {
            procedureSuccess = updateCreditLimit(ddlDB.SelectedItem.Text, myGlobal.loggedInUser());
            
            if (rws > 0)
                rws = rws - 1; //exclude insertion count into history table
        }
        catch(Exception exp)
        {
            lblMsg.Text = exp.Message;
        }
        finally{

            if (procedureSuccess == 0)
                lblMsg.Text += "Credit Limit Updatation failed!!!! Please retry or contact your administrator";
            else
                lblMsg.Text = "Credit Limit Updated Successfully for (" + rws.ToString() + ") customers in database " + ddlDB.SelectedItem.Text ;

            BindGrid();
        }
    }

    public int updateCreditLimit(String db, string usr)
    {
        String connStr="";

        if (ddlDB.SelectedValue.ToString() == "KE")
            connStr = myGlobal.getConnectionStringForDB("EVOKEModify");
        else
            connStr = myGlobal.getConnectionStringForDB("EVOTZModify");

        int ret = 0;
        SqlConnection connHSoft = new SqlConnection(connStr);

        SqlCommand cmdHSoft = new SqlCommand("tej.dbo.CreditLimitUpdater", connHSoft);
        cmdHSoft.CommandType = CommandType.StoredProcedure;

        //@db nvarchar(255),@usr nvarchar(50),@getResult int OUTPUT)

                SqlParameter db_SQL = cmdHSoft.Parameters.Add("@db", SqlDbType.VarChar, 100);
                db_SQL.Direction = ParameterDirection.Input;
                db_SQL.Value = db;

                SqlParameter usr_SQL = cmdHSoft.Parameters.Add("@usr", SqlDbType.VarChar,50);
                usr_SQL.Direction = ParameterDirection.Input;
                usr_SQL.Value = usr;


                SqlParameter getResult_SQL = cmdHSoft.Parameters.Add("@getResult", SqlDbType.Int);
                getResult_SQL.Direction = ParameterDirection.Output;
                //getResult_SQL.Value = getResult;

                try
                {
                    cmdHSoft.Connection.Open();

                    rws = Convert.ToInt32(cmdHSoft.ExecuteNonQuery());
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message + ": [" + cmdHSoft.CommandText + "]");
                }

                ret = Convert.ToInt32(getResult_SQL.Value);
                return  ret;
    }
}