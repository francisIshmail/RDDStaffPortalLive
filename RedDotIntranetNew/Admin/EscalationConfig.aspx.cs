using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_EscalationConfig : System.Web.UI.Page
{
    string query;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadProcessID();
        }
    }

    public void loadProcessID()
    {
        query = "select * from dbo.process_def";
        Db.LoadDDLsWithCon(ddlprocessID, query, "processName", "processId", myGlobal.getIntranetDBConnectionString());
        ddlprocessID.SelectedIndex = 0;
        loadprocessstatus();
        LoadTextBoxes();
    }

    public void loadprocessstatus()
    {
        query = "select * from dbo.processStatus where fk_processId='" + ddlprocessID.SelectedValue + "'";
        Db.LoadDDLsWithCon(ddlProcessStatusID, query, "processStatusName", "processStatusID", myGlobal.getIntranetDBConnectionString());
        LoadTextBoxes();
    }

    public void LoadTextBoxes()
    {
        DataSet ds = new DataSet();
        query = "select * from dbo.EmailEscalation where fk_processID='" + ddlprocessID.SelectedValue + "' and fk_processstatusID='" + ddlProcessStatusID.SelectedValue + "'";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        ds = Db.myGetDS(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblAddEdit.Text = "Edit Data";
            btnDelete.Enabled = true;
            txt1stEsclate.Text = ds.Tables[0].Rows[0]["escalate1Days"].ToString();
            txt1stEmail.Text = ds.Tables[0].Rows[0]["escalate1EmailList"].ToString();
            txt2ndEsclate.Text = ds.Tables[0].Rows[0]["escalate2Days"].ToString();
            txt2ndemail.Text = ds.Tables[0].Rows[0]["escalate2EmailList"].ToString();
        }
        else
        {
            lblAddEdit.Text = "Add Data";
            btnDelete.Enabled = false;
            clear();
        }
    }

    protected void ddlprocessID_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadprocessstatus();
        LoadTextBoxes();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        if (txt1stEsclate.Text == "" || txt2ndEsclate.Text == "" || txt1stEmail.Text == "" || txt2ndemail.Text == "")
        {
            lblError.Text = "Please Fill All Details To Continue";
            return;
        }
        else if (!Util.isValidNumber(txt1stEsclate.Text))
        {
            lblError.Text = "Please supply a valid numeric value for 1st Esclate Days ";
            return;
        }
        else if (!Util.isValidNumber(txt2ndEsclate.Text))
        {
            lblError.Text = "Please supply a valid numeric value for 2nd Esclate Days ";
            return;
        }


        if (lblAddEdit.Text == "Add Data")
        {
            string query;
            query = "insert into dbo.EmailEscalation values(" + ddlprocessID.SelectedValue + "," + ddlProcessStatusID.SelectedValue + "," + txt1stEsclate.Text + ",'" + txt1stEmail.Text + "'," + txt2ndEsclate.Text + ",'" + txt2ndemail.Text + "')";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            Db.myExecuteSQL(query);
            lblError.Text = "Data Successfully Added To The Database";
            lblAddEdit.Text = "Edit Data";
            btnDelete.Enabled = true;
        }
        else if (lblAddEdit.Text == "Edit Data")
        {
            string query;
            query = "update dbo.EmailEscalation set escalate1days='" + txt1stEsclate.Text + "',escalate1Emaillist='" + txt1stEmail.Text + "',escalate2days='" + txt2ndEsclate.Text + "',escalate2Emaillist='" + txt2ndemail.Text + "' where fk_processID='" + ddlprocessID.SelectedValue + "' and fk_processStatusID='" + ddlProcessStatusID.SelectedValue + "'";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            Db.myExecuteSQL(query);
            lblError.Text = "Data Successfully Updated";
            lblAddEdit.Text = "Edit Data";
        }
    }

    public void clear()
    {
        txt1stEmail.Text = "";
        txt1stEsclate.Text = "";
        txt2ndemail.Text = "";
        txt2ndEsclate.Text = "";
    }

    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTextBoxes();
    }

    protected void ddlProcessStatusID_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTextBoxes();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string query;
        query = "delete from dbo.EmailEscalation where fk_processID='" + ddlprocessID.SelectedValue + "' and fk_processStatusID='" + ddlProcessStatusID.SelectedValue + "'";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(query);
        lblError.Text = "Selected Data Successfully Deleted From The Dtabase";
        clear();
        btnDelete.Enabled = false;
        lblAddEdit.Text = "Add Data";
    }
}