using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Targets_SalesEmployeeMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.tejSalespersonMap where Isactive=1");
            if (count > 0)
            {
                BindGrid();
               
            }
          

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                //int count = Db.LoadlstWithCon("select * from dbo.rddCountriesList");

                //  RadioButtonList countrylst = new RadioButtonList();
                // Db.LoadlstWithCon("select * from dbo.rddCountriesList");
                string sql;
                sql = "select * from dbo.rddCountriesList";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                DataTable dtRoles = new DataTable();
                dtRoles = Db.myGetDS(sql).Tables[0];
                chkListCountries.DataSource = dtRoles;
                chkListCountries.DataTextField = "Country";
                chkListCountries.DataValueField = "CountryCode";
                chkListCountries.DataBind();
                populatecombos();
        }

       
        //ddlDesignation.Items.Insert(0, new ListItem("-- Select --", "0"));
       
        //ddlManager.Items.Insert(0, new ListItem("-- Select --", "0"));
       
        //ddlMemUser.Items.Insert(0, new ListItem("-- Select --", "0"));
        selcountry.Value = "";

        lblMsg.Text = "";
    }
    public void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DsForms = Db.myGetDS("select ID,alias,salesperson,email,desid,designation,manid,manager,membershipuser,forecast_from from(select a.id,a.alias,a.salesperson,a.email,a.designation desid,d.designation,a.manager manid,(select c.alias from tejSalespersonMap as c where c.id=a.manager) manager ,a.membershipuser,a.forecast_from,a.isactive from tejSalespersonMap a left outer join  designation_master d on d.id =a.designation) rslt where isnull(rslt.isactive,0)=1 Order by alias");
            if (DsForms.Tables.Count > 0)
            {
                Gridview1.DataSource = DsForms.Tables[0];
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindGrid() : " + ex.Message;
        }
    }

    public void populatecombos()
    {
        string tsql = "select distinct Id,designation from designation_master";
        //Db.LoadDDLsWithCon(ddlDB, tsql, "databaseName", "CountryCode2", myGlobal.getIntranetDBConnectionString());

        Db.LoadDDLsWithCon(ddlDesignation, tsql, "designation", "id", myGlobal.getAppSettingsDataForKey("tejSAP"));
        ddlDesignation.Items.Insert(0, new ListItem("-- Select --", "0"));
        ddlDesignation.SelectedIndex = 0;

        Db.LoadDDLsWithCon(ddlManager, "select id,alias from dbo.tejSalespersonMap  where isactive=1 ", "alias", "id", myGlobal.getAppSettingsDataForKey("tejSAP"));

        ddlManager.Items.Insert(0, new ListItem("-- Select --", "0"));
        ddlManager.SelectedIndex = 0;


        string usrsql = "select UserName,username from aspnet_users";
        //Db.LoadDDLsWithCon(ddlDB, tsql, "databaseName", "CountryCode2", myGlobal.getIntranetDBConnectionString());
        Db.LoadDDLsWithCon(ddlMemUser, usrsql, "UserName", "UserName", myGlobal.getMembershipDBConnectionString());
        ddlMemUser.Items.Insert(0, new ListItem("-- Select --", "0"));

        ddlMemUser.SelectedIndex = 0;
            

    }

    public void populateconboforupd(int id)
    {
       Db.LoadDDLsWithCon(ddlManager, "select id,alias from dbo.tejSalespersonMap where id <> "+ id +" and isactive=1 ", "alias", "id", myGlobal.getAppSettingsDataForKey("tejSAP"));
       
       ddlManager.Items.Insert(0, new ListItem("-- Select --", "0"));
       // ddlManager.SelectedIndex = 0;
 }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            if (BtnSave.Text == "Save")
            {

                long LeavePeriodID = Db.myExecuteSQLReturnLatestAutoID("Insert into dbo.tejSalespersonMap (alias,salesperson,Email,designation,manager,createdby,createdon,Membershipuser,forecast_from,isactive) Values ('" + txtname.Text + "','" + txtshortname.Text + "','" + txtemail.Text + "', '" + ddlDesignation.SelectedValue + "','" + ddlManager.SelectedValue + "', '" + myGlobal.loggedInUser() + "',GETDATE(), '" + ddlMemUser.SelectedValue + "','" + txtforecastfrm.Text + "',1)");

                if (LeavePeriodID > 0)
                {
                    string[] conarray = selcountry.Value.Split(',');
                   // selcountry.Value = lblselcountry;
                    foreach (var conlist in conarray)
                    {
                        if (conlist.ToString() != "")
                        {
                            Db.myExecuteSQL("Insert into dbo.Sales_Employee_country (salesempid,country) Values ( '" + txtshortname.Text + "' , '" + conlist + "')");
                         }
                    }
                    
                    lblMsg.Text = " Sales Employee saved successfully";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Form saved successfully'); </script>");
                }
            }
            else if (BtnSave.Text == "Update")
            {
                Db.myExecuteSQL("Update dbo.tejSalespersonMap Set alias='" + txtname.Text + "',salesperson='" + txtshortname.Text + "',Email='" + txtemail.Text + "',designation='" + ddlDesignation.SelectedValue + "',manager='" + ddlManager.SelectedValue + "',LastUpdatedOn=GETDATE() , LastUpdatedBy='" + myGlobal.loggedInUser() + "',Membershipuser='"+ ddlMemUser.SelectedValue  +"',forecast_from='"+ txtforecastfrm.Text +"'  Where ID= " + lblMenuID.Text);
                Db.myExecuteSQL("delete  from  sales_Employee_country where salesempid='" + txtshortname.Text + "'");
               // Db.myExecuteSQL("update  dbo.Sales_Employee_country set country= '" + selcountry.Value + "' where salesempid ="+ lblMenuID.Text +"");

                string s = "";

                //if (selcountry.Value == "")
                //{
                for (int itm = 0; itm < chkListCountries.Items.Count; itm++)
                {
                    if (chkListCountries.Items[itm].Selected)
                    {
                        s += chkListCountries.Items[itm].Value + ",";
                    }

                }
                //selcountry.Value = s;
                
                
                string[] conarray = s.Split(',');
                // selcountry.Value = lblselcountry;
                foreach (var conlist in conarray)
                {
                    if (conlist.ToString() != "")
                    {
                        Db.myExecuteSQL("Insert into dbo.Sales_Employee_country (salesempid,country) Values ('" + txtshortname.Text + "', '" + conlist + "')");
                    }
                }

                lblMsg.Text = " Form updated successfully";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Form updated successfully'); </script>");
                BtnSave.Text = "Save";
            }
            BindGrid();
            populatecombos();
            ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
    }


    public void ClearControl()
    {
        lblMenuID.Text = "";
        ddlDesignation.SelectedIndex= 0;
        ddlManager.SelectedIndex = 0;
        ddlMemUser.SelectedIndex = 0;
        txtname.Text="";
        txtshortname.Text = "";
        txtemail.Text = "";
        txtforecastfrm.Text = "";
        pnlFormList.Visible = true;
        BtnSave.Text = "Save";
        Gridview1.SelectedIndex = -1;
        chkListCountries.ClearSelection();
        txtid.Text = "";
    }


    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Update";
            lblMsg.Text = "";
            pnlFormList.Visible = false;
            GridViewRow row = Gridview1.SelectedRow;
            lblMsg.Text = "";
            Label lblID = (Label)row.FindControl("lblID");
            Label lblname = (Label)row.FindControl("lblname");
            Label lblshortnm = (Label)row.FindControl("lblshortnm");
            Label lblemail = (Label)row.FindControl("lblemail");
            Label lbldesignation = (Label)row.FindControl("lbldesignationid");
            Label lblmanager = (Label)row.FindControl("lblmanagerid");
           Label membuser = (Label)row.FindControl("lblmembuser");
           Label forecastfrm = (Label)row.FindControl("lblforecastfrom");
            string ID = lblID.Text;
            string name = lblname.Text;
            string sname = lblshortnm.Text;
            string email = lblemail.Text;
            string designation = lbldesignation.Text;
            string manager = lblmanager.Text;
            string memusr=membuser.Text;
            string forecstfrm=forecastfrm.Text;
            txtid.Text = ID;
            lblMenuID.Text = ID;
            txtname.Text = name;
            txtshortname.Text = sname;
            txtemail.Text = email;
            populateconboforupd(Convert.ToInt32(ID));

            if (designation == ""|| designation=="0")
            {
                ddlDesignation.SelectedIndex = 0;
            }
            else
            {
                ddlDesignation.SelectedValue = designation;
            }

            if (manager == "" || manager=="0")
            {
                ddlManager.SelectedIndex = 0;
            }
            else
            {
                ddlManager.SelectedValue = manager;
            }
            

            if (memusr == "")
            {
                ddlMemUser.SelectedIndex = 0;
            }
            else
            {
                ddlMemUser.SelectedValue = memusr;
            }
           
            txtforecastfrm.Text = forecstfrm;
            string sql;
            sql = "select country from dbo.Sales_Employee_country where SalesEmpId='" + sname + "'";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable dtsalescountry = new DataTable();
            dtsalescountry = Db.myGetDS(sql).Tables[0];

            //string lblselcountry = dtsalescountry.Rows[0][1].ToString();
            //string[] conarray = lblselcountry.Split(',');
            //selcountry.Value = lblselcountry;
            foreach (DataRow tabrow in dtsalescountry.Rows)
            {
                foreach (var conlist in tabrow.ItemArray)
                {
                    if (conlist.ToString() != "")
                    {
                        chkListCountries.Items.FindByValue(conlist.ToString()).Selected = true;
                        selcountry.Value = selcountry.Value + conlist.ToString() + ",";
                    }
                }
            }

            //for (int i = 0; i < dtsalescountry.Rows.Count; i++)
            //{
            //  //  chkListCountries.SelectedItem = dtsalescountry.Rows[i][1];
            //    chkListCountries.Items.FindByValue(conlist).Selected = true;
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Gridview1_SelectedIndexChanged() " + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("salesemployeemaster.aspx", true);
        ClearControl();
        lblMsg.Text = "";
    }

    protected void chkListCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        string s = "";
        
        //if (selcountry.Value == "")
        //{
            for (int itm=0;itm< chkListCountries.Items.Count;itm++)
            {
                if (chkListCountries.Items[itm].Selected)
                {
                    s+= chkListCountries.Items[itm].Value +",";
                }

            }
            selcountry.Value = s;
        //}
        //else
        //{
        //    selcountry.Value = selcountry.Value + "," + chkListCountries.SelectedItem.Value;
        //}

       

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                string ids = e.CommandArgument.ToString();
                Db.myExecuteSQL("update  dbo.tejSalespersonMap set IsActive=0  Where ID= " + ids);
               // Db.myExecuteSQL("delete  from  sales_Employee_country where salesempid=" + ids + "");
               
                lblMsg.Text = " Record Deleted successfully";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "GridView1_RowCommand() " + ex.Message;
        }

    }
    
    protected void Griview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // cancel the automatic delete action
        e.Cancel = true;
        BindGrid();

    }

    protected void Griview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridview1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void txtshortname_TextChanged(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonMap where salesperson='" +txtshortname.Text + "' ");
        if (count > 0)
        {
           txtshortname.Text = "";
            txtshortname.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Duplicate Short Name'); </script>");
            
        }
        else
        {
            if (txtemail.Text == "")
            {
                txtemail.Text = txtshortname.Text + "@reddotdistribution.com";
            }
        }
    }
}