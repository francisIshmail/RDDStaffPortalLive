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

public partial class Intranet_WMS_statementBase : System.Web.UI.Page
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

        }
    }
       

	protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
	}

    protected void BindGrid()
    {
        if (ddlDB.SelectedItem.Text.ToString() == "No Items")
            lblMsg.Text = "No valid database found for databindings, Please contact administrator";
        else
        {
            if (ddlDB.SelectedValue.ToString() == "KE" || ddlDB.SelectedValue.ToString() == "UG" || ddlDB.SelectedValue.ToString() == "EPZ")  //// field names are different in KE,UG,EPZ (Audit_NO) and in TZ,DU (cAuditNumber)
             qry = "SELECT distinct lar.name,lar.DCLink  FROM  [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvSTTransactionsFull pst INNER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvARAccountsFull  ar ON pst.DrCrAccount = ar.DCLink AND pst.[Module] = 'AR'  LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvStockAndWhseItems ST ON pst.AccountLink = st.StockLink and (pst.WarehouseCode=St.WhseCode or isnull(pst.WarehouseCode,'')='') LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.StkItem S on S.StockLink = pst.AccountLink LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.WHSEMST wh ON pst.WarehouseCode = wh.Code LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvARAuditLinkedAccRep par ON pst.Audit_No = par.Audit_No LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.CLIENT lar ON  lar.DCLink = ar.DCLink LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.SALESREP rep ON pst.RepID = rep.idSalesRep  WHERE (ST.ItemActive=1) AND (pst.TrCode in ('CRN', 'INV', 'IS', 'RC')) order by lar.name ";
            else
             qry = "SELECT distinct lar.name,lar.DCLink  FROM  [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvSTTransactionsFull pst INNER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvARAccountsFull  ar ON pst.DrCrAccount = ar.DCLink AND pst.[Module] = 'AR'  LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvStockAndWhseItems ST ON pst.AccountLink = st.StockLink and (pst.WarehouseCode=St.WhseCode or isnull(pst.WarehouseCode,'')='') LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.StkItem S on S.StockLink = pst.AccountLink LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.WHSEMST wh ON pst.WarehouseCode = wh.Code LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo._bvARAuditLinkedAccRep par ON pst.cAuditNumber = par.cAuditNumber LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.CLIENT lar ON  lar.DCLink = ar.DCLink LEFT OUTER JOIN [" + ddlDB.SelectedItem.Text.ToString() + "].dbo.SALESREP rep ON pst.RepID = rep.idSalesRep  WHERE (ST.ItemActive=1) AND (pst.TrCode in ('CRN', 'INV', 'IS', 'RC')) order by lar.name ";

            Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedItem.Value.ToString());
            lstDealers.DataSource = null;
            lstDealers.Items.Clear();
        
                lstDealers.DataSource = Db.myGetDS(qry).Tables[0];
                lstDealers.DataTextField = "name";
                lstDealers.DataValueField = "DCLink";
                lstDealers.DataBind();

            lblDealerCount.Text = lstDealers.Items.Count.ToString();
        }
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        
       //Session["DCLink"] = ListBox1.SelectedValue;
       //Session["statDb"] = DropDownList1.SelectedValue;


        if (lstDealers.SelectedIndex <0)
        {
            lblMsg.Text = "Error ! Please select dealer from the lisl to proceed.";
            return;
        }
        else
        {
            lblMsg.Text = "Dclink : " + lstDealers.SelectedValue.ToString();
        }
        Session["strdate"] = DateTime.Now.ToString("MM-dd-yyyy");
        Response.Redirect("crystal_Statement.aspx?pDB=" + ddlDB.SelectedItem.Text.Trim() + "&pCountryCode=" + ddlDB.SelectedValue.ToString().Trim() + "&pDCLink=" + lstDealers.SelectedValue.ToString());
    }
    
}