using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Intranet_EVO_DealerManagement : System.Web.UI.Page
{
    string query;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!Page.IsPostBack)
        {
            Loaddefaults();
            bindDDLCountry();
            BindGrid();
        }
    }

    protected void BindFileds(string dlrId)
    {

        query = "select C.Country,D.DealerID,D.fk_CountryID,D.ContactPerson,D.CompanyName,D.Email1,D.Email2,D.Phone,D.Cell,D.byUserName,D.ModifiedDate from dbo.tblDealer D left join dbo.tblCountry C on D.fk_CountryID=C.CountryID where dealerID=" + lblEditDealerId.Text;  //colums are : DeptId,DeptName,LastUpdated
        Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
        SqlDataReader drd = Db.myGetReader(query);

        if (drd.HasRows)
        {
            drd.Read();

            //txtVendor.Text = drd["planVendor"].ToString();
            ddlCountry.SelectedIndex = -1;
            ddlCountry.Items.FindByValue(drd["fk_CountryID"].ToString()).Selected = true;
            txtContactPerson.Text = drd["ContactPerson"].ToString();
            txtCompany.Text = drd["CompanyName"].ToString();
            txtEmail1.Text = drd["Email1"].ToString();

            if (drd["Email2"].ToString() != "NA")
                txtEmail2.Text = drd["Email2"].ToString();
            else
                txtEmail2.Text = "";

            if (drd["Phone"].ToString() != "NA")
                txtPh.Text = drd["Phone"].ToString();
            else
                txtPh.Text = "";

            if (drd["Cell"].ToString() != "NA")
                txtCell.Text = drd["Cell"].ToString();
            else
                txtCell.Text = "";

        }
        drd.Close();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(ddlCountry.SelectedIndex<0)
        {
            lblError.Text = "Error ! No country selected for the dealer.";
            return;
        }

        if (txtContactPerson.Text.Trim() == "")
        {
            lblError.Text = "Error ! Contact Person filed can not be empty, Please supply a value and then save.";
            return;
        }

        if (txtCompany.Text.Trim() == "")
        {
            lblError.Text = "Error ! Company Name filed can not be empty, Please supply a value and then save.";
            return;
        }

        if (txtEmail1.Text.Trim() == "")
        {
            lblError.Text = "Error ! Email Id 1 is mandatory, filed can not be empty, Please supply a value and then save.";
            return;
        }

        if (txtEmail2.Text.Trim() == "")
        {
            txtEmail2.Text = "NA";
        }

        if (txtPh.Text.Trim() == "")
        {
            txtPh.Text = "NA";
        }

        if (txtCell.Text.Trim() == "")
        {
            txtCell.Text = "NA";
        }

        try
        {
            if (btnSave.Text == "Save New")  //insert new case
            {
                query = string.Format("insert into dbo.tblDealer(fk_CountryID,ContactPerson,CompanyName,Email1,Email2,Phone,Cell,byUserName)  values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')", ddlCountry.SelectedItem.Value.ToString(), txtContactPerson.Text, txtCompany.Text, txtEmail1.Text, txtEmail2.Text, txtPh.Text, txtCell.Text, myGlobal.loggedInUser());
                Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
                Db.myExecuteSQL(query);
                lblError.Text = "Dealer Successfully added";
                Loaddefaults();
                BindGrid();
            }
            else  //update case
            {
                query = string.Format("update dbo.tblDealer set fk_CountryID={0},ContactPerson='{1}',CompanyName='{2}',Email1='{3}',Email2='{4}',Phone='{5}',Cell='{6}',byUserName='{7}',ModifiedDate='{8}' where dealerID= " + lblEditDealerId.Text , ddlCountry.SelectedItem.Value.ToString(), txtContactPerson.Text, txtCompany.Text, txtEmail1.Text, txtEmail2.Text, txtPh.Text, txtCell.Text, myGlobal.loggedInUser(),DateTime.Now.ToString("MM-dd-yyyy"));;
                Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
                Db.myExecuteSQL(query);
                lblError.Text = "Dealer Successfully updated";
                Loaddefaults();
                BindGrid();
            }
        }
        catch (Exception exp)
        {
            lblError.Text = "Error Saving Dealer ! " + exp.Message;
        }
    
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Loaddefaults();
        BindGrid();
    }

    private void Loaddefaults()
    {
        btnSave.Text = "Save New";
        txtContactPerson.Text = "";
        txtCompany.Text = "";
        txtEmail1.Text = "";
        txtEmail2.Text = "";
        txtPh.Text = "";
        txtCell.Text = "";

        if (ddlCountry.Items.Count > 0)
            ddlCountry.SelectedIndex = 0;

        lblEditDealerId.Text="-1";
        //lblError.Text = "";
    }

    protected void lnkNewCountry_Click(object sender, EventArgs e)
    {
        PanelCountry.Visible = true;
        txtNewCountry.Text = "";
        Loaddefaults();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        if (txtNewCountry.Text.Trim() == "")
        {
            lblError.Text = "Error ! country name filed can not be empty, Please supply a value and then save.";
            return;
        }


    }
    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        PanelCountry.Visible = false;
        txtNewCountry.Text = "";
    }

    

    protected void btnSaveCountry_Click(object sender, EventArgs e)
    {
        if (txtNewCountry.Text.Trim() == "")
        {
            lblError.Text = "Error ! It's Mandatory to supply value for Country field.  ";
            return;
        }

        //look for existence

        //if (Util.lookItemTextFiledExistence(ddlCountry, txtNewCountry.Text.Trim().ToUpper()) == true)
        if (ddlCountry.Items.FindByText(txtNewCountry.Text.Trim().ToUpper()) != null)   
        {
                lblError.Text = "Country already exists in the list.  ";
                return;
            }

        try
        {

            query = string.Format("insert into dbo.tblCountry(country) VALUES('{0}')", txtNewCountry.Text.Trim().ToUpper());
            Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
            Db.myExecuteSQL(query);
            bindDDLCountry();
            lblError.Text = "Country Successfully added";
        }
        catch (Exception exp)
        {
            lblError.Text = "Error Saving New Country ! " + exp.Message;
        }
    }

    private void bindDDLCountry()
    {
        Db.LoadDDLsWithCon(ddlCountry, "select CountryID,Country from dbo.tblCountry order by country", "Country", "CountryID", myGlobal.getRDDMarketingMailsDBConnectionString());
        lblCntryCnt.Text = ddlCountry.Items.Count.ToString();
    }

    protected void BindGrid()
    {
        String summarySQL;
        String sortExp = (String)ViewState["sortExpression"];
        String sortDir = (String)ViewState["sortDirection"];

        if (sortExp == null || sortExp == "")
        {
            summarySQL = "select C.Country,D.DealerID,D.fk_CountryID,D.ContactPerson,D.CompanyName,D.Email1,D.Email2,D.Phone,D.Cell,D.byUserName,D.ModifiedDate from dbo.tblDealer D left join dbo.tblCountry C on D.fk_CountryID=C.CountryID order by C.Country,D.CompanyName";  //colums are : DeptId,DeptName,LastUpdated
        }
        else
        {
            summarySQL = "select C.Country,D.DealerID,D.fk_CountryID,D.ContactPerson,D.CompanyName,D.Email1,D.Email2,D.Phone,D.Cell,D.byUserName,D.ModifiedDate from dbo.tblDealer D left join dbo.tblCountry C on D.fk_CountryID=C.CountryID order by " + sortExp + " " + sortDir;
        }
        Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
        Grid1.DataSource = Db.myGetDS(summarySQL);
        Grid1.DataBind();

        lblDealersCnt.Text = Grid1.Rows.Count.ToString();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        PanelCountry.Visible = false;
        txtNewCountry.Text = "";

        LinkButton btn = sender as LinkButton;

        foreach (GridViewRow row in Grid1.Rows)
        {
            Control ctrl = row.FindControl("lnkEdit") as LinkButton;
            if (ctrl != null)
            {
                LinkButton btn1 = (LinkButton)ctrl;
                if (btn.ClientID == btn1.ClientID)
                {
                    btnSave.Text = "Update";

                    Label lblId = (Label)row.FindControl("lblDealerId") as Label;
                    lblEditDealerId.Text = lblId.Text; //updateable dealer id
                    BindFileds(lblId.Text);
                    row.BackColor = System.Drawing.Color.LightGreen;
                    break;
                }
            }
        }
    }

    protected void lnkDel_Click(object sender, EventArgs e)
    {
        PanelCountry.Visible = false;
        txtNewCountry.Text = "";

        LinkButton btn = sender as LinkButton;

        foreach (GridViewRow row in Grid1.Rows)
        {
            Control ctrl = row.FindControl("lnkDel") as LinkButton;
            if (ctrl != null)
            {
                LinkButton btn1 = (LinkButton)ctrl;
                if (btn.ClientID == btn1.ClientID)
                {
                    btnSave.Text = "Update";

                    Label lblId = (Label)row.FindControl("lblDealerId") as Label;
                    
                    String sqlQry = "delete from  dbo.tblDealer where DealerId=" + lblId.Text;
                            Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
                            Db.myExecuteSQL(sqlQry);
                            lblError.Text = "Dealer record has been deleted successfully";
                            break;
                  }
            }
        }
        BindGrid();
        Loaddefaults();
    }

    protected void Grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        String sortExp = e.SortExpression.ToString();
        String sortDir = e.SortDirection.ToString();

        String sortExpV = (String)ViewState["sortExpression"];
        String sortDirV = (String)ViewState["sortDirection"];

        if (sortExpV != null && sortExp == sortExpV)
        {
            if (sortDirV == "Asc")
                ViewState["sortDirection"] = "Desc";
            else
                ViewState["sortDirection"] = "Asc";
        }
        else
        {
            ViewState["sortExpression"] = sortExp;
            ViewState["sortDirection"] = "Asc";
        }

        BindGrid();
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    
    
   
         

   
}