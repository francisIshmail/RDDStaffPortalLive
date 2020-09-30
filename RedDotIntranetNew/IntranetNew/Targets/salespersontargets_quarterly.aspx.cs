using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Net.Mail;

public partial class IntranetNew_Targets_salespersontargets_quarterly : System.Web.UI.Page
{
    public static int curquater ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindddl();

        }
        lblMsg.Text = "";

    }

    protected void rbListCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 && ddlQuarter.SelectedIndex != 0 && ddlsalesperson.SelectedIndex != 0)
        {
            bindgrid();
        }
    }

    public void bindgrid()
    {
        int frmmnt = 0, tomnt = 0;
        try
        {
            if (ddlQuarter.SelectedValue == "1")
            {
                frmmnt = 1;
                tomnt = 3;
            }
            else if (ddlQuarter.SelectedValue == "2")
            {
                frmmnt = 4;
                tomnt = 6;
            }
            else if (ddlQuarter.SelectedValue == "3")
            {
                frmmnt = 7;
                tomnt = 9;
            }
            else if (ddlQuarter.SelectedValue == "4")
            {
                frmmnt = 10;
                tomnt = 12;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month between " + frmmnt + " and " + tomnt + "  and salesperson='" + ddlsalesperson.SelectedValue + "' and country='" + rbListCountries.SelectedValue + "' ");
            if (count > 0)
            {
                // lblMsg.Text = "Country Targets Already Entered";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                DataSet DsForms = Db.myGetDS("Select  BU ,cast(sum(revenue)as decimal(10,2)) as revenue_targets,cast(sum(gp)as decimal(10,2)) as GP_targets  FROM tejSalespersonTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month between " + frmmnt + " and " + tomnt + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' And BU not in ('AEG','C001','D Link','DELL ENTERPRISE','ENGENIUS','ESCAN ANTIVIRUS','GADGITECH', 'HP PSG CONSUMER','HP PSG COMMERCIAL','LENOVO ACCESSORIES','LONG BATT','OPTOMA','REMIX','SAMSUNG CONSUMABLE','SAMSUNG DISPLAY', 'SAMSUNG HARDWARE','SAMSUNG PRINTERS','TOSHIBA') group by bu order by revenue_targets desc, bu asc ");
                if (DsForms.Tables.Count > 0)
                {
                    Gridview1.DataSource = DsForms.Tables[0];
                    Gridview1.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                // DataSet DsForms = Db.myGetDS("Select ItmsGrpNam as BU ,'' revenue_targets,'' GP_targets FROM SAPAE.dbo.OITB Where ItmsGrpNam Not in ('C001','D Link','ESCAN ANTIVIRUS','GADGITECH','HP CONSUMABLES')");

                DataTable dtMailSettings = new DataTable();
                SqlConnection conn;
                SqlDataAdapter adp = new SqlDataAdapter();
                SqlCommand cmd;
                DataSet DsForms = new DataSet();
                using (conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP")))
                {
                    using (cmd = new SqlCommand("tejGetBU", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        try
                        {
                            conn.Open();
                            adp.SelectCommand = cmd;
                            adp.Fill(DsForms);

                        }
                        catch (Exception Ex)
                        {
                            throw new Exception(Ex.Message + ": [" + cmd.CommandText + "]");
                        }
                    }
                }

                if (DsForms.Tables.Count > 0)
                {
                    Gridview1.DataSource = DsForms.Tables[0];
                    Gridview1.DataBind();
                }

            }
            int curyr = DateTime.Now.Year;
            if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue)  && curyr <= Convert.ToInt32(ddlyear.SelectedValue) && ddlsalesperson.SelectedIndex!=0 && rbListCountries.SelectedValue!="" )
            {
                Gridview1.Enabled = true;
            }
            else
            {
                Gridview1.Enabled = false;
            }
            Gridview1.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in rbListCountries_SelectedIndexChanged() : " + ex.Message;
        }
    }

    public string GetGridviewData(GridView gv)
    {
        StringBuilder strBuilder = new StringBuilder();
        StringWriter strWriter = new StringWriter(strBuilder);
        HtmlTextWriter htw = new HtmlTextWriter(strWriter);
        gv.RenderControl(htw);
        return strBuilder.ToString();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
        return;
    }

    public void Bindddl()
    {

        ListItem lssel = new ListItem();
        lssel.Text = "-- Select --"; //nextmnth.ToString("MMM");
        lssel.Value = "0"; //nextmnth.ToString("MM");
        ddlQuarter.Items.Add(lssel);


        ListItem ls = new ListItem();
        ls.Text = "Q1"; //nextmnth.ToString("MMM");
        ls.Value = "1"; //nextmnth.ToString("MM");
        ddlQuarter.Items.Add(ls);

        ListItem ls1 = new ListItem();
        ls1.Text = "Q2"; //nextmnth.ToString("MMM");
        ls1.Value = "2"; //nextmnth.ToString("MM");
        ddlQuarter.Items.Add(ls1);

        ListItem ls2 = new ListItem();
        ls2.Text = "Q3"; //nextmnth.ToString("MMM");
        ls2.Value = "3"; //nextmnth.ToString("MM");
        ddlQuarter.Items.Add(ls2);


        ListItem ls3 = new ListItem();
        ls3.Text = "Q4"; //nextmnth.ToString("MMM");
        ls3.Value = "4"; //nextmnth.ToString("MM");
        ddlQuarter.Items.Add(ls3);

        //    ddltomonth.Items.Add(ls);
        //}

        DateTime getmnth = DateTime.Now;
        string mnth = getmnth.ToString("MM");
        if (Convert.ToInt32(mnth) <= 3)
        {
            ddlQuarter.SelectedIndex = 1;
            curquater = 1;
        }
        else if (Convert.ToInt32(mnth) <= 6)
        {
            ddlQuarter.SelectedIndex = 2;
            curquater = 2;
        }
        else if (Convert.ToInt32(mnth) <= 9)
        {
            ddlQuarter.SelectedIndex = 3;
            curquater = 3;
        }
        else if (Convert.ToInt32(mnth) > 9)
        {
            ddlQuarter.SelectedIndex = 4;
            curquater = 4;
        }



        ddlyear.Items.Add("2017");
        ddlyear.SelectedIndex = 0;

        DateTime year = Convert.ToDateTime("1/1/2016");

        for (int i = 0; i < 1; i++)
        {
            DateTime nextyr = getmnth.AddYears(i);
            ListItem yrls1 = new ListItem();
            yrls1.Text = nextyr.ToString("yyyy");
            yrls1.Value = nextyr.Year.ToString();
            ddlyear.Items.Add(yrls1);
        }

        ddlyear.SelectedValue = DateTime.Now.Year.ToString();

        string ForecastSuperuser = myGlobal.getAppSettingsDataForKey("ForecastSuperuser");

        string[] ForecastSuperuserList = ForecastSuperuser.Split(';');

        string sql = string.Empty;

        foreach (string usrs in ForecastSuperuserList)
        {
            if (myGlobal.loggedInUser().ToLower().Contains(usrs.ToLower()))
            {
                sql = "select salesperson,alias from dbo.tejSalespersonMap where isactive=1 ";
            }
        }
        if (string.IsNullOrEmpty(sql))
        {
            // sql = "select id,alias from dbo.tejSalespersonMap where isactive=1 and membershipuser='" + myGlobal.loggedInUser() + "'";
            sql = "select salesperson,alias from dbo.tejSalespersonMap where isactive=1 and manager in(select ID from dbo.tejSalespersonMap where membershipuser='" + myGlobal.loggedInUser() + "')" +
             " union all  select salesperson,alias from dbo.tejSalespersonMap where isactive=1 and membershipuser='" + myGlobal.loggedInUser() + "'";
        }

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        DataTable dtsales = new DataTable();
        dtsales = Db.myGetDS(sql).Tables[0];
        ddlsalesperson.DataSource = dtsales;
        ddlsalesperson.DataTextField = "alias";
        ddlsalesperson.DataValueField = "salesperson";
        ddlsalesperson.DataBind();
        ddlsalesperson.Items .Insert (0,new ListItem("-- Select --","0"));
        ddlsalesperson.SelectedIndex = 0;
        
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int frmmnt = 0, tomnt = 0;
        try
        {
            if (ddlQuarter.SelectedValue == "1")
            {
                frmmnt = 1;
                tomnt = 3;
            }
            else if (ddlQuarter.SelectedValue == "2")
            {
                frmmnt = 4;
                tomnt = 6;
            }
            else if (ddlQuarter.SelectedValue == "3")
            {
                frmmnt = 7;
                tomnt = 9;
            }
            else if (ddlQuarter.SelectedValue == "4")
            {
                frmmnt = 10;
                tomnt = 12;
            }


            if (frmmnt <= tomnt)
            {
                for (int mn = frmmnt; mn <= tomnt; mn++)
                {
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        GridViewRow row = Gridview1.Rows[i];
                        Label lblBU = (Label)row.FindControl("lblID");
                        TextBox txtrevtarget = (TextBox)row.FindControl("txtrevenuetarget");
                        TextBox txtgptarget = (TextBox)row.FindControl("txtgptargets");
                        string BU = lblBU.Text;
                        double revtarget;
                        if (txtrevtarget.Text != "")
                        {
                            revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text) / 3, 2); //Convert.ToDouble(txtrevtarget.Text);
                        }
                        else
                        {
                            revtarget = 0.0;
                        }

                        double gptarget;
                        if (txtgptarget.Text != "")
                        {
                            gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text) / 3, 2);// Convert.ToDouble(txtgptarget.Text);
                        }
                        else
                        {
                            gptarget = 0.0;
                        }

                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonTargets where year=" + ddlyear.SelectedValue + " and month = " + mn + " and salesperson='" + ddlsalesperson.SelectedValue + "' and bu= '" + BU + "' and country='" + rbListCountries.SelectedValue + "' ");
                        if (count > 0)
                        {
                            Db.myExecuteSQL("update  dbo.tejSalespersonTargets set revenue=" + revtarget + ",gp=" + gptarget + " where year= " + ddlyear.SelectedValue + " and month=" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "' and salesperson='" + ddlsalesperson.SelectedValue + "' ");

                        }
                        else
                        {
                            Db.myExecuteSQL("insert into dbo.tejSalespersonTargets(year,month,country,salesperson,bu,revenue,gp) values ( " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + BU + "'," + revtarget + "," + gptarget + ")");

                        }
                    }
                }
            }

            string Mail = "  EXEC SendMailForSalespersonTagretsAdd  " + ddlyear.SelectedValue + ", 0 ,'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + ddlsalesperson.SelectedItem.Text + "','" + myGlobal.loggedInUser() + "',0," + ddlQuarter.SelectedValue;
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            Db.myExecuteSQL(Mail);

            #region" Old code to send email"

//            DataSet Dsemails = Db.myGetDS(@" select receipient_emailId from EmailConfig
//                                        union all
//                                        select email from tejsalespersonmap where MembershipUser= '" + myGlobal.loggedInUser() + "' Union all	select email from tejsalespersonmap where id  = (  select top 1 manager from tejsalespersonmap where MembershipUser= '" + myGlobal.loggedInUser() + "' )");
//            string emailids = "";
//            if (Dsemails.Tables.Count > 0)
//            {
//                if (Dsemails.Tables[0].Rows.Count > 0)
//                {
//                    for (int i = 0; i < Dsemails.Tables[0].Rows.Count; i++)
//                    {
//                        if (string.IsNullOrEmpty(emailids))
//                        {
//                            emailids = Dsemails.Tables[0].Rows[i][0].ToString();
//                        }
//                        else
//                        {
//                            emailids = emailids + "," + Dsemails.Tables[0].Rows[i][0].ToString();
//                        }
//                    }
//                }
//            }

//            string ForecastDataForMail = GetGridviewData(Gridview1).ToString();
//            if (!string.IsNullOrEmpty(ForecastDataForMail.Trim()) && !string.IsNullOrEmpty(emailids.Trim()))
//            {
//                string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, " SalesPerson Targets (Quarterly) has been changed ", ForecastDataForMail, true);
            //            }

            #endregion

            lblMsg.Text = " Sales Person Targets saved successfully";
        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
        Gridview1.Visible = false;
        ClearControl();


        #region " Old code "
                //        int frmmnt=0,tomnt=0;
                //        try
                //        {
                //            if (ddlQuarter.SelectedValue == "1")
                //            {
                //                frmmnt = 1;
                //                tomnt = 3;
                //            }
                //            else if (ddlQuarter.SelectedValue == "2")
                //            {
                //                frmmnt = 4;
                //                tomnt = 6;
                //            }
                //            else if (ddlQuarter.SelectedValue == "3")
                //            {
                //                frmmnt = 7;
                //                tomnt = 9;
                //            }
                //            else if (ddlQuarter.SelectedValue == "4")
                //            {
                //                frmmnt = 10;
                //                tomnt = 12;
                //            }


                //            if (frmmnt <= tomnt)
                //            {
                //                for (int mn = frmmnt; mn <= tomnt; mn++)
                //                {
                //                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                //                    {
                //                        GridViewRow row = Gridview1.Rows[i];
                //                        Label lblBU = (Label)row.FindControl("lblID");
                //                        TextBox txtrevtarget = (TextBox)row.FindControl("txtrevenuetarget");
                //                        TextBox txtgptarget = (TextBox)row.FindControl("txtgptargets");
                //                        string BU = lblBU.Text;
                //                        double revtarget;
                //                        if (txtrevtarget.Text != "")
                //                        {
                //                            revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text) / 3, 2); //Convert.ToDouble(txtrevtarget.Text);
                //                        }
                //                        else
                //                        {
                //                            revtarget = 0.0;
                //                        }

                //                        double gptarget;
                //                        if (txtgptarget.Text != "")
                //                        {
                //                            gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text) / 3, 2);// Convert.ToDouble(txtgptarget.Text);
                //                        }
                //                        else
                //                        {
                //                            gptarget = 0.0;
                //                        }

                //                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                //                        int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonTargets where year=" + ddlyear.SelectedValue + " and month = " + mn + " and salesperson='" + ddlsalesperson.SelectedValue + "' and bu= '" + BU + "' and country='" + rbListCountries.SelectedValue + "' ");
                //                        if (count > 0)
                //                        {
                //                            Db.myExecuteSQL("update  dbo.tejSalespersonTargets set revenue=" + revtarget + ",gp=" + gptarget + " where year= " + ddlyear.SelectedValue + " and month=" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "' and salesperson='" + ddlsalesperson.SelectedValue + "' ");

                //                        }
                //                        else
                //                        {
                //                            Db.myExecuteSQL("insert into dbo.tejSalespersonTargets(year,month,country,salesperson,bu,revenue,gp) values ( " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + BU + "'," + revtarget + "," + gptarget + ")");
                //                        }
                //                    }
                //                }
                //            }

                //            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                //            DataSet Dsemails = Db.myGetDS(@" select receipient_emailId from EmailConfig
                //                                        union all
                //                                        select email from tejsalespersonmap where MembershipUser= '"+myGlobal.loggedInUser()+"' Union all	select email from tejsalespersonmap where id  = (  select top 1 manager from tejsalespersonmap where MembershipUser= '"+myGlobal.loggedInUser()+"' )");
                //            string emailids = "";
                //            if (Dsemails.Tables.Count > 0)
                //            {
                //                if (Dsemails.Tables[0].Rows.Count > 0)
                //                {
                //                    for (int i = 0; i < Dsemails.Tables[0].Rows.Count; i++)
                //                    {
                //                        if (string.IsNullOrEmpty(emailids))
                //                        {
                //                            emailids = Dsemails.Tables[0].Rows[i][0].ToString();
                //                        }
                //                        else
                //                        {
                //                            emailids = emailids + "," + Dsemails.Tables[0].Rows[i][0].ToString();
                //                        }
                //                    }
                //                }
                //            }

                //            string ForecastDataForMail = GetGridviewData(Gridview1).ToString();
                //            if (!string.IsNullOrEmpty(ForecastDataForMail.Trim()) && !string.IsNullOrEmpty(emailids.Trim()))
                //            {
                //                string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, " SalesPerson Targets (Quarterly) has been changed ", ForecastDataForMail, true);
                //            } 
                //            lblMsg.Text = " Sales Person Targets saved successfully";
                //        }
                //        catch (Exception ex)
                //        {
                //            Gridview1.Visible = false;
                //            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
                //        }
                //        Gridview1.Visible = false;
                //        ClearControl();
                        //       // Response.Redirect("salespersontargets.aspx", false);

        #endregion

    }

    //protected void getGridValues()
    //{

    //    try
    //    {
    //        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
    //        int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month = " + frm) + " and salesperson='" + ddlsalesperson.SelectedValue + "' and country='" + rbListCountries.SelectedValue + "' ");
    //        if (count > 0)
    //        {
    //            // lblMsg.Text = "Country Targets Already Entered";
    //            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
    //            DataSet DsForms = Db.myGetDS("Select  BU ,revenue as revenue_targets,gp as GP_targets FROM tejSalespersonTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' order by bu asc ");
    //            if (DsForms.Tables.Count > 0)
    //            {
    //                Gridview1.DataSource = DsForms.Tables[0];
    //                Gridview1.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Text = "";
    //            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
    //            // DataSet DsForms = Db.myGetDS("Select ItmsGrpNam as BU ,'' revenue_targets,'' GP_targets FROM SAPAE.dbo.OITB Where ItmsGrpNam Not in ('C001','D Link','ESCAN ANTIVIRUS','GADGITECH','HP CONSUMABLES')");

    //            DataTable dtMailSettings = new DataTable();
    //            SqlConnection conn;
    //            SqlDataAdapter adp = new SqlDataAdapter();
    //            SqlCommand cmd;
    //            DataSet DsForms = new DataSet();
    //            using (conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP")))
    //            {
    //                using (cmd = new SqlCommand("tejGetBU", conn))
    //                {
    //                    cmd.CommandType = CommandType.StoredProcedure;



    //                    try
    //                    {
    //                        conn.Open();
    //                        adp.SelectCommand = cmd;
    //                        adp.Fill(DsForms);

    //                    }
    //                    catch (Exception Ex)
    //                    {
    //                        throw new Exception(Ex.Message + ": [" + cmd.CommandText + "]");
    //                    }
    //                }
    //            }





    //            if (DsForms.Tables.Count > 0)
    //            {
    //                Gridview1.DataSource = DsForms.Tables[0];
    //                Gridview1.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = "Error in rbListCountries_SelectedIndexChanged() : " + ex.Message;
    //    }
    //}
    public void ClearControl()
    {

        //pnlFormList.Visible = true;
        BtnSave.Text = "Save";
        Gridview1.SelectedIndex = -1;
        rbListCountries.ClearSelection();

        rbListCountries.Visible = false;

        ddlQuarter.SelectedIndex = curquater;
       // ddltomonth.SelectedIndex = 0;
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();

        ddlsalesperson.SelectedIndex = 0;
        Gridview1.DataSource = "";

    }
    protected void ddlsalesperson_SelectedIndexChanged(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        //int count = Db.LoadlstWithCon("select * from dbo.rddCountriesList");

        //  RadioButtonList countrylst = new RadioButtonList();
        // Db.LoadlstWithCon("select * from dbo.rddCountriesList");
        string sql;
        sql = "select a.country CountryCode ,c.Country  Country from  sales_Employee_country a,rddCountriesList c where a.country=c.CountryCode and  a.salesEmpid='" + ddlsalesperson.SelectedValue + "'";
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        DataTable dtRoles = new DataTable();
        dtRoles = Db.myGetDS(sql).Tables[0];
        rbListCountries.DataSource = dtRoles;
        rbListCountries.DataTextField = "Country";
        rbListCountries.DataValueField = "CountryCode";
        rbListCountries.DataBind();
        rbListCountries.Visible = true;
        if (ddlyear.SelectedIndex != 0 && ddlQuarter.SelectedIndex != 0 && rbListCountries.SelectedValue != "")
        {
            bindgrid();
        }

        int curyr = DateTime.Now.Year;
        if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue) && ddlsalesperson.SelectedIndex != 0 && rbListCountries.SelectedValue != "")
        {
            Gridview1.Enabled = true;
        }
        else
        {
            Gridview1.Enabled = false;
        } 

    }
    //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getGridValues();

    //}
    //protected void ddltomonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getGridValues();
    //}
    protected void btnsave1_Click(object sender, EventArgs e)
    {
        int frmmnt = 0, tomnt = 0;
        try
        {
            if (ddlQuarter.SelectedValue == "1")
            {
                frmmnt = 1;
                tomnt = 3;
            }
            else if (ddlQuarter.SelectedValue == "2")
            {
                frmmnt = 4;
                tomnt = 6;
            }
            else if (ddlQuarter.SelectedValue == "3")
            {
                frmmnt = 7;
                tomnt = 9;
            }
            else if (ddlQuarter.SelectedValue == "4")
            {
                frmmnt = 10;
                tomnt = 12;
            }


            if (frmmnt <= tomnt)
            {
                for (int mn = frmmnt; mn <= tomnt; mn++)
                {
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        GridViewRow row = Gridview1.Rows[i];
                        Label lblBU = (Label)row.FindControl("lblID");
                        TextBox txtrevtarget = (TextBox)row.FindControl("txtrevenuetarget");
                        TextBox txtgptarget = (TextBox)row.FindControl("txtgptargets");
                        string BU = lblBU.Text;
                        double revtarget;
                        if (txtrevtarget.Text != "")
                        {
                            revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text) / 3, 2); //Convert.ToDouble(txtrevtarget.Text);
                        }
                        else
                        {
                            revtarget = 0.0;
                        }

                        double gptarget;
                        if (txtgptarget.Text != "")
                        {
                            gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text) / 3, 2);// Convert.ToDouble(txtgptarget.Text);
                        }
                        else
                        {
                            gptarget = 0.0;
                        }

                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonTargets where year=" + ddlyear.SelectedValue + " and month = " + mn + " and salesperson='" + ddlsalesperson.SelectedValue + "' and bu= '" + BU + "' and country='" + rbListCountries.SelectedValue + "' ");
                        if (count > 0)
                        {
                            Db.myExecuteSQL("update  dbo.tejSalespersonTargets set revenue=" + revtarget + ",gp=" + gptarget + " where year= " + ddlyear.SelectedValue + " and month=" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "' and salesperson='" + ddlsalesperson.SelectedValue + "' ");
                        }
                        else
                        {
                            Db.myExecuteSQL("insert into dbo.tejSalespersonTargets(year,month,country,salesperson,bu,revenue,gp) values ( " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + BU + "'," + revtarget + "," + gptarget + ")");
                        }
                    }
                }
            }

            string Mail = "  EXEC SendMailForSalespersonTagretsAdd  " + ddlyear.SelectedValue + ", 0 ,'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + ddlsalesperson.SelectedItem.Text + "','" + myGlobal.loggedInUser() + "',0," + ddlQuarter.SelectedValue;
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            Db.myExecuteSQL(Mail);

            #region "Old code to send email"
            //            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
//            DataSet Dsemails = Db.myGetDS(@" select receipient_emailId from EmailConfig
//                                        union all
//                                        select email from tejsalespersonmap where MembershipUser= '" + myGlobal.loggedInUser() + "' Union all	select email from tejsalespersonmap where id  = (  select top 1 manager from tejsalespersonmap where MembershipUser= '" + myGlobal.loggedInUser() + "' )");
//            string emailids = "";
//            if (Dsemails.Tables.Count > 0)
//            {
//                if (Dsemails.Tables[0].Rows.Count > 0)
//                {
//                    for (int i = 0; i < Dsemails.Tables[0].Rows.Count; i++)
//                    {
//                        if (string.IsNullOrEmpty(emailids))
//                        {
//                            emailids = Dsemails.Tables[0].Rows[i][0].ToString();
//                        }
//                        else
//                        {
//                            emailids = emailids + "," + Dsemails.Tables[0].Rows[i][0].ToString();
//                        }
//                    }
//                }
//            }

//            string ForecastDataForMail = GetGridviewData(Gridview1).ToString();
//            if (!string.IsNullOrEmpty(ForecastDataForMail.Trim()) && !string.IsNullOrEmpty(emailids.Trim()))
//            {
//                string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, " SalesPerson Targets (Quarterly) has been changed ", ForecastDataForMail, true);
            //            }
            #endregion

            lblMsg.Text = " Sales Person Targets saved successfully";
        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
        Gridview1.Visible = false;
        ClearControl();
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsalesperson.SelectedIndex != 0 && ddlQuarter.SelectedIndex != 0 && rbListCountries.SelectedValue != "")
        {
            bindgrid();
        }

        int curyr = DateTime.Now.Year;
        if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue) && ddlsalesperson.SelectedIndex != 0 && rbListCountries.SelectedValue != "")
        {
            Gridview1.Enabled = true;
        }
        else
        {
            Gridview1.Enabled = false;
        }  
    }
    protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlyear.SelectedIndex!=0 && ddlsalesperson.SelectedIndex!=0 && rbListCountries.SelectedValue!="")
        {
            bindgrid();
        }
        
        int curyr = DateTime.Now.Year;
        if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue) && ddlsalesperson.SelectedIndex!=0 && rbListCountries.SelectedValue!="")
        {
            Gridview1.Enabled = true;
        }
        else
        {
            Gridview1.Enabled = false;
        }   
    }
}