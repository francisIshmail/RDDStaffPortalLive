using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class IntranetNew_Funnel_FunnelSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='FunnelSetup.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    DataSet DS = Db.myGetDS(" select salesperson, alias as Name from tejsalespersonMap Where IsActive=1 ");
                    if (DS.Tables.Count > 0)
                    {
                        ddlSalesperson.DataSource = DS.Tables[0];
                        ddlSalesperson.DataTextField = "Name";
                        ddlSalesperson.DataValueField = "salesperson";
                        ddlSalesperson.DataBind();
                        ddlSalesperson.Items.Insert(0, new ListItem("-- Select --", "0"));
                        ddlSalesperson.SelectedIndex = 0;

                        lblDesignation.Text = "";
                        // ; select ID,Designation from Designation_Master
                        //rddListDesign.DataSource = DS.Tables[1];
                        //rddListDesign.DataTextField = "Designation";
                        //rddListDesign.DataValueField = "ID";
                        //rddListDesign.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Funnel Setup");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }
    protected void ddlSalesperson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlSalesperson.SelectedItem.Text != "-- Select --")
            {
                string salesperson= ddlSalesperson.SelectedItem.Value;
                BindCountry(salesperson);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured ddlSalesperson_SelectedIndexChanged : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropDownList1 = (e.Row.FindControl("ddlAccess") as DropDownList);

            DropDownList1.Items.Insert(0, new ListItem("--Select--", "0"));
            DropDownList1.Items.Insert(1, new ListItem("NO ACCESS", "NO ACCESS"));
            DropDownList1.Items.Insert(2, new ListItem("FULL ACCESS", "FULL ACCESS"));
            DropDownList1.Items.Insert(3, new ListItem("READ ONLY", "READ ONLY"));

            DropDownList1.Font.Bold = true;
            //Select the Access of Customer in DropDownList
            string Access = (e.Row.FindControl("lblAccess") as Label).Text;
            DropDownList1.Items.FindByValue(Access).Selected = true;
            if (Access == "FULL ACCESS")
            {
                DropDownList1.ForeColor = Color.Green;
            }
            else if (Access == "NO ACCESS")
            {
                DropDownList1.ForeColor = Color.Red;
                DropDownList1.Font.Bold = false;
            }
        }
    }  

    private void BindGrid(string salesperson, string country)
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(" exec GetBU '" + salesperson + "','" + country + "'");
            Gridview1.DataSource = DS;
            Gridview1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured BindGrid : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    private void BindCountry(string salesperson)
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS("select sc.country as countrycode,c.Country from dbo.Sales_Employee_country sc Join rddcountrieslist c on sc.country=c.countrycode Where SalesEmpId='" + salesperson + "'  ;  select S.salesperson, S.alias as Name , D.id DesigId, D.Designation,S.membershipuser  from tejsalespersonMap S	left Join designation_master d on S.Designation=d.id Where IsActive=1 And S.salesperson='" + salesperson + "' ");
            if (DS.Tables.Count > 0)
            {
                rddlistcountries.DataSource = DS.Tables[0];
                rddlistcountries.DataTextField = "Country";
                rddlistcountries.DataValueField = "countrycode";
                rddlistcountries.DataBind();

                if (rddlistcountries.Items.Count > 0)
                {
                    rddlistcountries.SelectedIndex = 0;
                    rddlistcountries_SelectedIndexChanged(null, null);
                }

                try
                {
                    if (DS.Tables.Count == 2)  /// Assign Designation
                    {
                        if (DS.Tables[1].Rows.Count > 0)
                        {
                            string designation = DS.Tables[1].Rows[0]["Designation"].ToString();
                            string MembershipUserName = DS.Tables[1].Rows[0]["membershipuser"].ToString();

                            lblDesignation.Text = designation;
                            lblMembershipUserName.Text = MembershipUserName;
                            //rddListDesign.Items.FindByValue(desigId).Selected = true;
                            //rddListDesign.Items.FindByText(designation).Selected = true;
                        }
                    }
                    else
                    {
                        lblDesignation.Text = "";
                    }
                }
                catch { }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindCountry() : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    protected void rddlistcountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        string salesperson = ddlSalesperson.SelectedItem.Value;
        string country = rddlistcountries.SelectedItem.Value;
        BindGrid(salesperson, country);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.Red;

            if (ddlSalesperson.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select salesperson.";
                return;
            }

            //if (rddListDesign.SelectedIndex == -1)
            //{
            //    lblMsg.Text = "Please select designation.";
            //    return;
            //}

            if (rddlistcountries.SelectedIndex == -1)
            {
                lblMsg.Text = "Please select country.";
                return;
            }

            if (Gridview1.Rows.Count== 0)
            {
                lblMsg.Text = "No data found to save.";
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            string salesperson = ddlSalesperson.SelectedItem.Value;
            //string designation = rddListDesign.SelectedItem.Value;
            string country = rddlistcountries.SelectedItem.Value;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                GridViewRow row = Gridview1.Rows[i];
                Label lblBU = (Label)row.FindControl("lblBU");
                DropDownList ddlAccess = (DropDownList)row.FindControl("ddlAccess");
                string Access=ddlAccess.SelectedItem.Text;
                string BU=lblBU.Text;

                string sql = "";
                sql = " if exists ( select * from funnelsetup where salesperson='"+salesperson+"' And country='"+country+"' And BU='"+BU+"' ) ";
                sql = sql + " Update funnelsetup  set Access='" + Access + "',  lastupdatedby='" + myGlobal.loggedInUser() + "',lastupdatedon=getdate()	where salesperson='" + salesperson + "' And country='" + country + "' And BU='" + BU + "' ";
                sql = sql + " else Insert into funnelsetup(salesperson,country,MembershipUserName,BU,Access,createdby,createdon)	values ('" + salesperson + "','" + country + "','"+ lblMembershipUserName.Text +"','"+ BU + "','" + Access + "','" + myGlobal.loggedInUser() + "',getdate())";

                Db.myExecuteSQL(sql);
            }
           
            BindGrid(salesperson, country);
            lblMsg.Text = "Record saved successfully.";
            lblMsg.ForeColor = Color.Green;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnSave_Click() : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }
}