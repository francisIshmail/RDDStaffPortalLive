using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
public partial class IntranetNew_Targets_CountryTargets : System.Web.UI.Page
{
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
        if (ddlfrmMonth.SelectedIndex != 0 && ddlyear.SelectedIndex != 0)
        {
            bindgrid();
        }
      
    }

    public void bindgrid()
    {
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("select  count(*) from dbo.tejCountryTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month = " + Convert.ToInt32(ddlfrmMonth.SelectedValue) + " and country='" + rbListCountries.SelectedValue + "' ");
            if (count > 0)
            {
                // lblMsg.Text = "Country Targets Already Entered";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                DataSet DsForms = Db.myGetDS("Select  BU ,cast(revenue as decimal(10,2)) as revenue_targets,cast(gp as decimal(10,2)) as GP_targets  FROM tejCountryTargets where year=" + Convert.ToInt32(ddlyear.SelectedValue) + " and month = " + Convert.ToInt32(ddlfrmMonth.SelectedValue) + " and country='" + rbListCountries.SelectedValue + "' And BU not in ('AEG','C001','D Link','DELL ENTERPRISE','ENGENIUS','ESCAN ANTIVIRUS','GADGITECH', 'HP PSG CONSUMER','HP PSG COMMERCIAL','LENOVO ACCESSORIES','LONG BATT','OPTOMA','REMIX','SAMSUNG CONSUMABLE','SAMSUNG DISPLAY', 'SAMSUNG HARDWARE','SAMSUNG PRINTERS','TOSHIBA') order by bu asc ");
                if (DsForms.Tables[0].Rows.Count > 0)
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
                Gridview1.Visible = true;

            }

            int curmnt = DateTime.Now.Month;
            int curyr = DateTime.Now.Year;
            if (curmnt <= Convert.ToInt32(ddlfrmMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
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

        for (int i = 0; i < 12; i++)
        {
            DateTime nextmnth = month.AddMonths(i);
            ListItem ls = new ListItem();
            ls.Text = nextmnth.ToString("MMM");
            ls.Value = nextmnth.ToString("MM");
            ddlfrmMonth.Items.Add(ls);
           // ddltomonth.Items.Add(ls);
        }

        DateTime mnth = DateTime.Now;
        ddlfrmMonth.Items.Insert(0, new ListItem(" -- Select --", "0"));

        ddlfrmMonth.SelectedValue = mnth.ToString("MM");
       // ddltomonth.SelectedValue = mnth.ToString("MM");


        DateTime nextyr = mnth.AddYears(-1);
        ListItem ls1 = new ListItem();
        ls1.Text = nextyr.ToString("yyyy");
        ls1.Value = nextyr.Year.ToString();
        ddlyear.Items.Add(ls1);
        int nextyr1 = mnth.Year;
        ListItem ls11 = new ListItem();
        ls11.Text = nextyr1.ToString();
        ls11.Value = nextyr1.ToString();
        ddlyear.Items.Add(ls11);
        ddlyear.Items.Insert(0, new ListItem(" -- Select --", "0")); 

        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
       

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int mn = Convert.ToInt32(ddlfrmMonth.SelectedValue);
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
                    revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text), 2); //Convert.ToDouble(txtrevtarget.Text);
                }
                else
                {
                    revtarget = 0.0;
                }

                double gptarget;
                if (txtgptarget.Text != "")
                {
                    gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text), 2);//.ToDouble( txtgptarget.Text);
                }
                else
                {
                    gptarget = 0.0;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("select  count(*) from dbo.tejCountryTargets where year=" + ddlyear.SelectedValue + " and " +
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

            if (rbListCountries.SelectedValue != "")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string mail = "  EXEC SendMailForCountryTagretsAdd   " + ddlyear.SelectedValue + "," + mn.ToString() + ",'" + rbListCountries.SelectedValue + "','" + myGlobal.loggedInUser() + "',1,0" ;
                Db.myExecuteSQL(mail);
            }

            #region "Old code to send email"
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
                //    string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, " Country Targets ( Monthly ) has been changed  ", ForecastDataForMail, true);
            //}
            #endregion

            lblMsg.Text = " Country Targets saved successfully";
                Gridview1.Visible = false;
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
        Gridview1.Visible = false;
        rbListCountries.ClearSelection();
       // ddlfrmMonth.SelectedIndex = 0;
        //ddltomonth.SelectedIndex = 0;
        //ddlyear.SelectedIndex = 0;
        ddlyear.SelectedValue = DateTime.Now.Year.ToString();
        DateTime mnth = DateTime.Now;
      
        ddlfrmMonth.SelectedValue = mnth.ToString("MM");

    }

    protected void btnsave1_Click(object sender, EventArgs e)
    {
        try
        {
            int mn = Convert.ToInt32(ddlfrmMonth.SelectedValue);
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
                    revtarget = System.Math.Round(Convert.ToDouble(txtrevtarget.Text), 2); //Convert.ToDouble(txtrevtarget.Text);
                }
                else
                {
                    revtarget = 0.0;
                }

                double gptarget;
                if (txtgptarget.Text != "")
                {
                    gptarget = System.Math.Round(Convert.ToDouble(txtgptarget.Text), 2);//.ToDouble( txtgptarget.Text);
                }
                else
                {
                    gptarget = 0.0;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("select  count(*) from dbo.tejCountryTargets where year=" + ddlyear.SelectedValue + " and " +
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

            if (rbListCountries.SelectedValue != "")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string mail = "  EXEC SendMailForCountryTagretsAdd   " + ddlyear.SelectedValue + "," + mn.ToString() + ",'" + rbListCountries.SelectedValue + "','" + myGlobal.loggedInUser() + "',1,0";
                Db.myExecuteSQL(mail);
            }

            #region " Old code to send email "
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

            lblMsg.Text = " Country Targets saved successfully";
            Gridview1.Visible = false;
        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
        ClearControl();

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

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbListCountries.SelectedIndex != null && ddlfrmMonth.SelectedIndex != 0)
        {
            bindgrid();
        }

        int curmnt = DateTime.Now.Month;
        int curyr = DateTime.Now.Year;
        if (curmnt <= Convert.ToInt32(ddlfrmMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
        {
            Gridview1.Enabled = true;
        }
        else
        {

            Gridview1.Enabled = false;
        }

    }

    protected void ddlfrmMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex != 0 && rbListCountries.SelectedIndex != null)
        {
            bindgrid();
        }
        int curmnt = DateTime.Now.Month;
        int curyr = DateTime.Now.Year;
        if (curmnt <= Convert.ToInt32(ddlfrmMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
        {
            Gridview1.Enabled = true;
        }
        else
        {

            Gridview1.Enabled = false;
        }

    }
} 