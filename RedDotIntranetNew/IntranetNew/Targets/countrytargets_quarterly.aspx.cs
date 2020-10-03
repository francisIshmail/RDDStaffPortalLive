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

public partial class IntranetNew_Targets_countrytargets_quarterly : System.Web.UI.Page
{

    public static int curquater;

    protected void Page_Load(object sender, EventArgs e)
    {

    
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //int count = Db.LoadlstWithCon("select * from dbo.rddCountriesList");
           
              //  RadioButtonList countrylst = new RadioButtonList();
               // Db.LoadlstWithCon("select * from dbo.rddCountriesList");
            string sql;
            sql = "select * from dbo.rddCountriesList";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable dtRoles = new DataTable();
            dtRoles = Db.myGetDS(sql).Tables[0];
            rbListCountries.DataSource = dtRoles;
            rbListCountries.DataTextField = "Country";
            rbListCountries.DataValueField = "CountryCode";
            rbListCountries.DataBind();

            Bindddl();
        }
        lblMsg.Text = "";

    }

    protected void rbListCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 && ddlQuarter.SelectedIndex != 0)
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
            int count = Db.myExecuteScalar("select  count(*) from dbo.tejCountryTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month between " + frmmnt + " and " + tomnt + "  and country='" + rbListCountries.SelectedValue + "' ");
            if (count > 0)
            {
                // lblMsg.Text = "Country Targets Already Entered";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                DataSet DsForms = Db.myGetDS("Select  BU ,cast(sum(revenue)as decimal(10,2)) as revenue_targets,cast(sum(gp)as decimal(10,2)) as GP_targets  FROM tejCountryTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month between " + frmmnt + " and " + tomnt + " and country='" + rbListCountries.SelectedValue + "' And BU not in ('AEG','C001','D Link','DELL ENTERPRISE','ENGENIUS','ESCAN ANTIVIRUS','GADGITECH', 'HP PSG CONSUMER','HP PSG COMMERCIAL','LENOVO ACCESSORIES','LONG BATT','OPTOMA','REMIX','SAMSUNG CONSUMABLE','SAMSUNG DISPLAY', 'SAMSUNG HARDWARE','SAMSUNG PRINTERS','TOSHIBA')   group by bu order by bu asc ");
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

            if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue))
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

    public void Bindddl()
    {
       
        
        DateTime month = Convert.ToDateTime("1/1/2018");

        //for (int i = 0; i < 4; i++)
        //{
        //    DateTime nextmnth = month.AddMonths(i);
        ListItem lssel = new ListItem();
        lssel.Text = "-- Select --"; //nextmnth.ToString("MMM");
        lssel.Value = "0"; //nextmnth.ToString("MM");
        ddlQuarter.Items.Add(lssel);


            ListItem ls = new ListItem();
            ls.Text = "Q1"; //nextmnth.ToString("MMM");
            ls.Value ="1"; //nextmnth.ToString("MM");
            ddlQuarter.Items.Add(ls);

            ListItem ls1 = new ListItem();
            ls1.Text = "Q2"; //nextmnth.ToString("MMM");
            ls1.Value = "2"; //nextmnth.ToString("MM");
            ddlQuarter .Items.Add(ls1);

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
        else if (Convert.ToInt32(mnth) <=9)
        {
            ddlQuarter.SelectedIndex = 3;
            curquater = 3;
        }
        else if (Convert.ToInt32(mnth) >9)
        {
            ddlQuarter.SelectedIndex = 4;
            curquater = 4;
        }

        //ddlfrmMonth.SelectedValue = mnth.ToString("MM");
        //ddltomonth.SelectedValue = mnth.ToString("MM");
            DateTime mnth1 = DateTime.Now;


         DateTime nextyr = mnth1.AddYears(-1);
        ListItem lsyr = new ListItem();
        lsyr.Text = nextyr.ToString("yyyy");
        lsyr.Value = nextyr.Year.ToString();
        ddlyear.Items.Add(lsyr);
        int nextyr1 = mnth1.Year;
        ListItem ls11 = new ListItem();
        ls11.Text = nextyr1.ToString();
        ls11.Value = nextyr1.ToString();
        ddlyear.Items.Add(ls11);

        ddlyear.Items.Insert(0,new ListItem("-- Select--","0"));
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
       

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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int frmmnt=0,tomnt=0;
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
                            revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text) / 3, 2);
                        }
                        else
                        {
                            revtarget = 0.0;
                        }

                        double gptarget;
                        if (txtgptarget.Text != "")
                        {
                            gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text) / 3, 2); //Convert.ToDouble(txtgptarget.Text);
                        }
                        else
                        {
                            gptarget = 0.0;
                        }

                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        int count = Db.myExecuteScalar("select  count(*) from dbo.tejCountryTargets where year=" + ddlyear.SelectedValue + " AND  " +
                            " month =" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "'  ");
                        if (count > 0)
                        {
                            Db.myExecuteSQL("update  dbo.tejCountryTargets set revenue=" + revtarget + ",gp=" + gptarget + " " +
                                               "where year= " + ddlyear.SelectedValue + " and month=" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "'");

                        }
                        else
                        {
                            Db.myExecuteSQL("insert into dbo.tejCountryTargets(year,month,country,bu,revenue,gp) values (" +
                                " " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + BU + "'," + revtarget + "," + gptarget + ")");
                        }
                    }
                }
            }

            if (rbListCountries.SelectedValue != "")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string mail = "  EXEC SendMailForCountryTagretsAdd   " + ddlyear.SelectedValue + ",0,'" + rbListCountries.SelectedValue + "','" + myGlobal.loggedInUser() + "',0," + ddlQuarter.SelectedValue;
                Db.myExecuteSQL(mail);
            }

            #region "Old code to send mail"
            //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //DataSet Dsemails = Db.myGetDS("select * from EmailConfig  ");
            //string emailids = "";
            //if (Dsemails.Tables.Count > 0)
            //{
            //    emailids = Dsemails.Tables[0].Rows[0][0].ToString();
            //}

            //string ForecastDataForMail = GetGridviewData(Gridview1).ToString();

            //if (!string.IsNullOrEmpty(ForecastDataForMail.Trim()) && !string.IsNullOrEmpty(emailids.Trim()))
            //{
            //    string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, " Country Targets ( Quarterly ) has been changed  ", ForecastDataForMail, true);
            //}
            #endregion

            Gridview1.Visible = false;
            lblMsg.Text = " Country Targets saved successfully";
        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
        ClearControl();
    }

    public void ClearControl()
    {

       // pnlFormList.Visible = true;
        BtnSave.Text = "Save";
        Gridview1.SelectedIndex = -1;
        rbListCountries.ClearSelection();
        ddlQuarter.SelectedIndex = curquater;
        //ddltomonth.SelectedIndex = 0;
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
       

    }

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
                            revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text) / 3, 2);
                        }
                        else
                        {
                            revtarget = 0.0;
                        }

                        double gptarget;
                        if (txtgptarget.Text != "")
                        {
                            gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text) / 3, 2); //Convert.ToDouble(txtgptarget.Text);
                        }
                        else
                        {
                            gptarget = 0.0;
                        }

                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        int count = Db.myExecuteScalar("select  count(*) from dbo.tejCountryTargets where year=" + ddlyear.SelectedValue + " AND  " +
                            " month =" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "'  ");
                        if (count > 0)
                        {
                            Db.myExecuteSQL("update  dbo.tejCountryTargets set revenue=" + revtarget + ",gp=" + gptarget + " " +
                                               "where year= " + ddlyear.SelectedValue + " and month=" + mn + " and country='" + rbListCountries.SelectedValue + "' and bu='" + BU + "'");
                        }
                        else
                        {
                            Db.myExecuteSQL("insert into dbo.tejCountryTargets(year,month,country,bu,revenue,gp) values (" +
                                " " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + BU + "'," + revtarget + "," + gptarget + ")");
                        }
                    }
                }
            }

            if (rbListCountries.SelectedValue != "")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string mail = "  EXEC SendMailForCountryTagretsAdd   " + ddlyear.SelectedValue + ",0,'" + rbListCountries.SelectedValue + "','" + myGlobal.loggedInUser() + "',0," + ddlQuarter.SelectedValue;
                Db.myExecuteSQL(mail);
            }

            #region "Old code to send mail"
            //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //DataSet Dsemails = Db.myGetDS("select * from EmailConfig  ");
            //string emailids = "";
            //if (Dsemails.Tables.Count > 0)
            //{
            //    emailids = Dsemails.Tables[0].Rows[0][0].ToString();
            //}

            ////MailMessage msg = new MailMessage();
            ////msg.Body = GetGridviewData(Gridview1).ToString();
            ////clsEmail clml = new clsEmail();
            ////clml.EmailConfig(emailids, "", msg, "", "");

            //string ForecastDataForMail = GetGridviewData(Gridview1).ToString();
            //if (!string.IsNullOrEmpty(ForecastDataForMail.Trim()) && !string.IsNullOrEmpty(emailids.Trim()))
            //{
            //    string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, " Country Targets ( Quarterly ) has been changed  ", ForecastDataForMail, true);
            //}
            #endregion

            Gridview1.Visible = false;
            lblMsg.Text = " Country Targets saved successfully";
        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
        ClearControl();
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbListCountries.SelectedIndex != null && ddlQuarter.SelectedIndex != 0)
        {
            bindgrid();
        }
       


           int curyr = DateTime.Now.Year;
           if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
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
        if (rbListCountries.SelectedIndex != null && ddlyear.SelectedIndex != 0)
        {
            bindgrid();
        }
        int curyr = DateTime.Now.Year;
        if (curquater <= Convert.ToInt32(ddlQuarter.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
        {
            Gridview1.Enabled = true;
        }
        else
        {
            Gridview1.Enabled = false;
        }   
    }
} 