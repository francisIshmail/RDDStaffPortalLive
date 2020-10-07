using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_BPStatus_PVSetup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='PVSetup.aspx' and t1.IsActive=1");
                if (count > 0)
                {
                    txtEmpDisplayName.Text = "";
                    lblMsg.Text = "";
                    BindUsers();

                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    DataTable DTCountry = Db.myGetDS(" select Country from rddcountrieslist ").Tables[0];
                    ddlCountry.DataSource = DTCountry;// Table [2] for Countries
                    ddlCountry.DataTextField = "Country";
                    ddlCountry.DataValueField = "Country";
                    ddlCountry.DataBind();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=PV - Setup");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in pageLoad() " + ex.Message;
        }
    }

    public void BindUsers()
    {
        try
        {
            Db.LoadDDLsWithCon(ddlEmployees, " select '--Select--' As UserName, '--Select--' As DisplayName  union all select UserName, isnull(DisplayName,UserName) As DisplayName  from aspnet_users u where isnull(IsDeleted,0)=0 ", "DisplayName", "UserName", myGlobal.getMembershipDBConnectionString());
            if (ddlEmployees.Items.Count > 0)
                ddlEmployees.SelectedIndex = 0;
            else
            {
                ddlEmployees.Items.Add("No Rows");
                ddlEmployees.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindUsers() " + ex.Message;
        }
    }

    protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtEmpDisplayName.Text = ddlEmployees.SelectedItem.Text;
            Db.constr=myGlobal.getMembershipDBConnectionString();

            DataSet DSpvauthorization = Db.myGetDS(" select PVRole,PVCountry,PVApprovalCountries,DisplayName,UserName from aspnet_users where username='"+ddlEmployees.SelectedItem.Value+"'");
            if (DSpvauthorization.Tables.Count > 0)
            {
                if (DSpvauthorization.Tables[0].Rows.Count > 0)
                {
                    if (DSpvauthorization.Tables[0].Rows[0]["PVRole"] != null && !DBNull.Value.Equals(DSpvauthorization.Tables[0].Rows[0]["PVRole"]))
                    {
                        string UserRole = DSpvauthorization.Tables[0].Rows[0]["PVRole"].ToString();
                        if (UserRole == "CA")
                        {
                            rbCA.Checked = true;
                            rbCM.Checked = false;
                            rbEmp.Checked = false;
                            rbCFO.Checked = false;
                        }
                        else if (UserRole == "CM")
                        {
                            rbCM.Checked = true;
                            rbCA.Checked = false;
                            rbEmp.Checked = false;
                            rbCFO.Checked = false;
                        }
                        else if (UserRole == "CFO")
                        {
                            rbCFO.Checked = true;
                            rbCM.Checked = false;
                            rbEmp.Checked = false;
                            rbCA.Checked = false;
                        }
                        else
                        {
                            rbEmp.Checked = true;
                            rbCM.Checked = false;
                            rbCA.Checked = false;
                            rbCFO.Checked = false;
                        }
                    }
                    else
                    {
                        rbEmp.Checked = true;
                        rbCM.Checked = false;
                        rbCA.Checked = false;
                        rbCFO.Checked = false;
                    }

                    foreach (ListItem cntry in ddlCountry.Items)
                    {
                        cntry.Selected = false;
                    }

                    if (DSpvauthorization.Tables[0].Rows[0]["PVApprovalCountries"] != null && !DBNull.Value.Equals(DSpvauthorization.Tables[0].Rows[0]["PVApprovalCountries"]))
                    {
                        string[] pvcountries = DSpvauthorization.Tables[0].Rows[0]["PVApprovalCountries"].ToString().Split(';');

                        foreach (string country in pvcountries)
                        {
                            foreach (ListItem cntry in ddlCountry.Items)
                            {
                                if (cntry.Value == country.ToUpper())
                                {
                                    cntry.Selected = true;
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    foreach (ListItem cntry in ddlCountry.Items)
                    //    {
                    //        cntry.Selected = false;
                    //    }
                    //}
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in employee selection() " + ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text="";
            if (ddlEmployees.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Employee";
                return;
            }
            if ( string.IsNullOrEmpty(txtEmpDisplayName.Text) )
            {
                lblMsg.Text = "Please enter Employee Name";
                return;
            }

            if (rbEmp.Checked == false && rbCA.Checked == false && rbCM.Checked == false && rbCFO.Checked == false)
            {
                lblMsg.Text = "Please select Role";
                return;
            }
            string Country = string.Empty;
            foreach (ListItem ctry in ddlCountry.Items)
            {
                if (ctry.Selected)
                {
                    if (string.IsNullOrEmpty(Country))
                        Country = "" + ctry.Text + "";
                    else
                        Country = Country + ";" + ctry.Text + "";
                }
            }

            if (string.IsNullOrEmpty(Country))
            {
                lblMsg.Text = "Please select Country";
                return;
            }
            string Role = string.Empty;
            if (rbCA.Checked)
                Role = "CA";
            else if (rbCM.Checked)
                Role = "CM";
            else if (rbCFO.Checked)
                Role = "CFO";
            else if (rbEmp.Checked)
                Role = "Employee";

            Db.constr = myGlobal.getMembershipDBConnectionString();

            string sql = "Update aspnet_users set DisplayName='" + txtEmpDisplayName.Text + "',PVRole='" + Role + "',PVCountry='" + Country + "',PVApprovalCountries='" + Country + "' Where UserName='" + ddlEmployees.SelectedItem.Value + "'";
            Db.myExecuteSQL(sql);

            string selectedUser = ddlEmployees.SelectedItem.Value;
            BindUsers();
            ddlEmployees.SelectedItem.Value = selectedUser;
            ddlEmployees.SelectedItem.Text = txtEmpDisplayName.Text;

            lblMsg.Text = "Record updated successfully";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in update () : " + ex.Message;
        }
    }

}