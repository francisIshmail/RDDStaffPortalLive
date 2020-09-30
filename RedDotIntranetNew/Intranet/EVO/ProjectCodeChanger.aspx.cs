using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Intranet_EVO_ProjectCodeChanger : System.Web.UI.Page
{
    string qry;
    SqlDataReader drd;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        btnUpdate.Attributes.Add("onClick", "return getConfirmation();");

        if (!IsPostBack)
        {
            //Db.LoadDDLsWithCon(ddlDB, "select * from dbo.SqlConnectionServers where databaseName Not like 'websiteDb%'", "databaseName", "CountryCode", myGlobal.ConnectionString);
        }
            
    }
    protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
       // txtAuditNo.Text = "";
        resetFields();
    }
    private void resetFields()
    {
        lblInvoice.Text = "";
        lblInvoiceId.Text = "";
        lblPrjCode.Text = "";
        lblPrjId.Text = "";
        
        ddlPrjs.Items.Clear();
        lblPrjsCount.Text = "0";
        ddlPrjs.Enabled = false;
        
        btnUpdate.Enabled = false;
        lblUpdate.Text = "";
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        resetFields();

        if (txtAuditNo.Text.Trim() == "")
        {
            lblMsg.Text = "Invalid Value! Audit No. can't be blank";
            return;
        }

        if (ddlDB.SelectedValue.ToString() == "KE" || ddlDB.SelectedValue.ToString() == "UG" || ddlDB.SelectedValue.ToString() == "EPZ")  //// field names are different in KE,UG,EPZ (Audit_NO) and in TZ,DU (cAuditNumber)
            qry = "SELECT B.ProjectCode,C.AutoIndex as InvId,A.* FROM dbo.PostGL as A join dbo.Project as B on A.Project=B.ProjectLink join dbo.InvNum as C on A.Reference=C.InvNumber WHERE A.Audit_No='" + txtAuditNo.Text.Trim() + "'";
        else
            qry = "SELECT B.ProjectCode,C.AutoIndex as InvId,A.* FROM dbo.PostGL as A join dbo.Project as B on A.Project=B.ProjectLink join dbo.InvNum as C on A.Reference=C.InvNumber WHERE A.cAuditNumber='" + txtAuditNo.Text.Trim() + "'";

        
        Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedItem.Value.ToString());
        drd=Db.myGetReader(qry);
        if (drd.HasRows)
        {
            drd.Read();

            if (drd["Reference"] != DBNull.Value)
                lblInvoice.Text = drd["Reference"].ToString();  //gets invoice

            if (drd["InvId"] != DBNull.Value)
                lblInvoiceId.Text = drd["InvId"].ToString();  //gets invoice id

            if (drd["ProjectCode"] != DBNull.Value)
                lblPrjCode.Text = drd["ProjectCode"].ToString(); //gets project code

            if (drd["Project"] != DBNull.Value)
                lblPrjId.Text = drd["Project"].ToString();  //gets project id

            drd.Close();
        }
        else
        {
            lblMsg.Text = "Error : No Audit No. found in selected database table, Please retry using a different Audit No.";
            return;
        }

        //still verify

        if (lblInvoiceId.Text.Trim() == "")
        {
            lblMsg.Text = "Error : Invoice not found in selected database table, Please retry using a different Audit No.";
            return;
        }


        if (lblPrjCode.Text.Trim() == "")
        {
            lblMsg.Text = "Error : Project Code not found in selected database table, Please retry using a different Audit No.";
            return;
        }

        Db.LoadDDLsWithCon(ddlPrjs, "SELECT * FROM dbo.Project", "ProjectCode", "ProjectLink", myGlobal.getConnectionStringForDB(ddlDB.SelectedItem.Value.ToString()));
        if (ddlPrjs.Items.Count > 0)
        {
            ddlPrjs.SelectedIndex = 0;
            lblPrjsCount.Text = ddlPrjs.Items.Count.ToString();
        }
        
        btnUpdate.Enabled = true;
        ddlPrjs.Enabled = true;
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (ddlPrjs.SelectedIndex >= 0)
        {
            string qryRun; 
            qryRun = "begin BEGIN TRANSACTION ";
            qryRun += " UPDATE dbo.InvNum SET ProjectID=" + ddlPrjs.SelectedItem.Value.ToString() + " WHERE InvNumber='" + lblInvoice.Text + "' ;";
            qryRun += " UPDATE dbo._btblInvoiceLines SET iLineProjectID=" + ddlPrjs.SelectedItem.Value.ToString() + " WHERE iInvoiceID=" + lblInvoiceId.Text + " ;";
            qryRun += " UPDATE dbo.PostGL SET Project=" + ddlPrjs.SelectedItem.Value.ToString() + " WHERE Reference='" + lblInvoice.Text + "'  And Project>0 ;";
            qryRun += " UPDATE dbo.PostST SET Project=" + ddlPrjs.SelectedItem.Value.ToString() + " WHERE Reference='" + lblInvoice.Text + "' ;";
            qryRun +=" IF @@ERROR = 0 COMMIT TRANSACTION ELSE ROLLBACK TRANSACTION end ";

            Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedItem.Value.ToString());
            try
            {
                Db.myExecuteSQL(qryRun);

                lblMsg.Text="Conversion Done Successfully";
            }
            catch (Exception exp)
            {
                lblMsg.Text = "Error : Conversion could not be done due to : "  + exp.Message ;
            }

            lblUpdate.Text = "InvNum table updated";
            lblUpdate.Text += "<br> GL/ST table updated";
            lblUpdate.Text += "<br> Project Code changed to " + ddlPrjs.SelectedItem.Text + "  successfully" ;
            lblUpdate.Text += "<br><b>You must run GL-Relink in Evo<b>";

            btnUpdate.Enabled = false;
            ddlPrjs.Enabled = false;
        }
        else
        {
            lblMsg.Text = "Error : New Project Code not slected from the list.";
        }
    }
   
}