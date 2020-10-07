using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.IO;

public partial class IntranetNew_Targets_Forecast : System.Web.UI.Page
{
    double RevTargetTotal = 0, RevForecastRRTotal = 0, RevForecastBTBTotal = 0, GPTargetTotal = 0, GPForecastRRTotal = 0, GPForecastBTBTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindddl();
        }
        lblMsg.Text = "";

        GridCalculations();

    }

    private void GridCalculations()  //send -1 as parameter value for all rows Other wise works for single row
    {
        try
        {
            if (Gridview1.Rows.Count > 0)
            {
                RevForecastRRTotal=0;
                RevForecastBTBTotal=0;
                GPForecastBTBTotal = 0;
                GPForecastRRTotal = 0;

                foreach (GridViewRow grw in Gridview1.Rows)
                {
                    TextBox txtrevenuetarget = (TextBox)grw.FindControl("txtrevenuetarget");
                    txtrevenuetarget.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                    TextBox txtrevenuetargetbtb = (TextBox)grw.FindControl("txtrevenuetargetbtb");
                    txtrevenuetargetbtb.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                    Label lblrevenue = (Label)grw.FindControl("lblrevenue");
                    TextBox txtforcastvstarget = (TextBox)grw.FindControl("txtforcastvstarget");

                    if (string.IsNullOrEmpty(txtrevenuetarget.Text))
                    {
                        txtrevenuetarget.Text = "0";
                    }
                    if (string.IsNullOrEmpty(txtrevenuetargetbtb.Text))
                    {
                        txtrevenuetargetbtb.Text = "0";
                    }

                    if (Convert.ToDouble(lblrevenue.Text) > 0)
                    {
                        txtforcastvstarget.Text = Convert.ToDouble(((Convert.ToDouble(txtrevenuetarget.Text) + Convert.ToDouble(txtrevenuetargetbtb.Text)) / Convert.ToDouble(lblrevenue.Text)) * 100).ToString();
                    }
                    else
                    {
                        txtforcastvstarget.Text = "0.00";
                    }

                    TextBox txtgptargetsbtb = (TextBox)grw.FindControl("txtgptargetsbtb");
                    txtgptargetsbtb.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                    TextBox txtgptargets = (TextBox)grw.FindControl("txtgptargets");
                    txtgptargets.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                    Label lblgp = (Label)grw.FindControl("lblgp");
                    TextBox txtforcastvsgptarget = (TextBox)grw.FindControl("txtforcastvsgptarget");

                    if (string.IsNullOrEmpty(txtgptargets.Text))
                    {
                        txtgptargets.Text = "0";
                    }
                    if (string.IsNullOrEmpty(txtgptargetsbtb.Text))
                    {
                        txtgptargetsbtb.Text = "0";
                    }

                    if (Convert.ToDouble(lblrevenue.Text) > 0)
                    {
                        txtforcastvsgptarget.Text = Convert.ToDouble(((Convert.ToDouble(txtgptargets.Text) + Convert.ToDouble(txtgptargetsbtb.Text)) / Convert.ToDouble(lblgp.Text)) * 100).ToString();
                    }
                    else
                    {
                        txtforcastvsgptarget.Text = "0.00";
                    }

                    if (!string.IsNullOrEmpty(txtrevenuetarget.Text)) // Rev Forecast RR
                    {
                        RevForecastRRTotal = RevForecastRRTotal + Convert.ToDouble(txtrevenuetarget.Text);
                    }

                    if (!string.IsNullOrEmpty(txtrevenuetargetbtb.Text)) // Rev Forecast BTB
                    {
                        RevForecastBTBTotal = RevForecastBTBTotal + Convert.ToDouble(txtrevenuetargetbtb.Text);
                    }

                    if (!string.IsNullOrEmpty(txtgptargetsbtb.Text)) // GP Forecast BTB
                    {
                        GPForecastBTBTotal = GPForecastBTBTotal + Convert.ToDouble(txtgptargetsbtb.Text);
                    }

                    if (!string.IsNullOrEmpty(txtgptargets.Text)) // GP Forecast RR
                    {
                        GPForecastRRTotal = GPForecastRRTotal + Convert.ToDouble(txtgptargets.Text);
                    }
                }

                //Gridview1.FooterRow.Cells[1].Text = Math.Round(RevTargetTotal, 2).ToString();
                Gridview1.FooterRow.Cells[2].Text = Math.Round(RevForecastRRTotal, 2).ToString();
                Gridview1.FooterRow.Cells[3].Text = Math.Round(RevForecastBTBTotal, 2).ToString();
                //Gridview1.FooterRow.Cells[4].Text = Math.Round(GPTargetTotal, 2).ToString();
                Gridview1.FooterRow.Cells[5].Text = Math.Round(GPForecastRRTotal, 2).ToString();
                Gridview1.FooterRow.Cells[6].Text = Math.Round(GPForecastBTBTotal, 2).ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in GridCalculations() : " + ex.Message;
        }
    }

    protected void rbListCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlyear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && (ddlsalesperson.SelectedIndex != 0 || ddlsalesperson.SelectedItem.Text!="--Select--" ))
            {
                bindgrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in rbListCountries_SelectedIndexChanged() :"+ ex.Message;
        }
    }

    public void bindgrid()
    {
        try
        {
            if (Convert.ToInt32(ddlMonth.SelectedValue) <= Convert.ToInt32(ddltomonth.SelectedValue))
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                // lblMsg.Text = "Country Targets Already Entered";
                int count1 = Db.myExecuteScalar("select  count(*) from dbo.tejSalespersonTargets where year=" + ddlyear.SelectedValue + " and month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' ");
                if (count1 > 0)
                {
                    int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalesForecast where year=" + ddlyear.SelectedValue + " and month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' ");
                    if (count > 0)
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        string sql = " EXEC getForecastforMonth " + Convert.ToInt32(ddlMonth.SelectedValue) + "," + ddlyear.SelectedValue + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "',"+ 1 +"" ;

         //               DataSet DsForms = Db.myGetDS("Select a.BU,CAST(b.revenue AS decimal(10,2)) as Revenue,CAST(a.RevenueRR  AS decimal(10,2)) as RevenueRR,cast(a.RevenueBTB  AS decimal(10,2)) as RevenueBTB, " +
         //                   " cast(b.gp  AS decimal(10,2)) as gp,cast(a.GPRR  AS decimal(10,2)) as GPRR, cast(a.GPBTB  AS decimal(10,2)) as GPBTB,cast(a.RevenuePER  AS decimal(10,2)) AS forcastvsterget,cast(a.GPBPER  AS decimal(10,2)) AS forcastvsgptarget " +
         //                       " FROM tejSalesForecast as a,tejSalespersonTargets as b " +
         //" where a.year=" + ddlyear.SelectedValue + " and a.month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and a.country='" + rbListCountries.SelectedValue + "' and a.salesperson='" + ddlsalesperson.SelectedValue + "' and " +
         //" b.year=" + ddlyear.SelectedValue + " and b.month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and b.country='" + rbListCountries.SelectedValue + "' and a.salesperson=b.salesperson and a.bu=b.bu   " +
         // " order by bu asc ");
                        
                        DataSet DsForms = Db.myGetDS(sql);
                        if (DsForms.Tables.Count > 0)
                        {
                            Gridview1.DataSource = DsForms.Tables[0];
                            Gridview1.DataBind();
                            Gridview1.Visible = true;
                        }
                    }
                    else
                    {
                        //DataSet DsForms = Db.myGetDS("Select b.BU,CAST(b.revenue AS decimal(10,2)) as Revenue,0.00 as RevenueRR,0.00 as RevenueBTB, " +
                        //   " cast(b.gp  AS decimal(10,2)) as gp,0.00 as GPRR, 0.00 as GPBTB,0.00 AS forcastvsterget,0.00 AS forcastvsgptarget " +
                        //       " FROM tejSalespersonTargets as b " +
                        //    " where "+
                        //    " b.year=" + ddlyear.SelectedValue + " and b.month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and b.country='" + rbListCountries.SelectedValue + "' and b.salesperson='"+ ddlsalesperson.SelectedValue +"' " +
                        //     " order by bu asc ");
                        
                        string sql = " EXEC getForecastforMonth " + Convert.ToInt32(ddlMonth.SelectedValue) + "," + ddlyear.SelectedValue + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "'," + 0 + "";
                        DataSet DsForms = Db.myGetDS(sql);

                        if (DsForms.Tables.Count > 0)
                        {
                            Gridview1.DataSource = DsForms.Tables[0];
                            Gridview1.DataBind();
                            Gridview1.Visible = true;
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "";
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                    DataTable dtMailSettings = new DataTable();
                    SqlConnection conn;
                    SqlDataAdapter adp = new SqlDataAdapter();
                    SqlCommand cmd;

       //             DataSet DsForms = Db.myGetDS("Select a.BU,0.00 as Revenue,CAST(a.RevenueRR  AS decimal(10,2)) as RevenueRR,cast(a.RevenueBTB  AS decimal(10,2)) as RevenueBTB, " +
       //                     " 0.00 as gp,cast(a.GPRR  AS decimal(10,2)) as GPRR, cast(a.GPBTB  AS decimal(10,2)) as GPBTB,cast(a.RevenuePER  AS decimal(10,2)) AS forcastvsterget,cast(a.GPBPER  AS decimal(10,2)) AS forcastvsgptarget " +
       //                         " FROM tejSalesForecast as a" +
       //  " where a.year=" + ddlyear.SelectedValue + " and a.month = " + Convert.ToInt32(ddlMonth.SelectedValue) + " and a.country='" + rbListCountries.SelectedValue + "' and a.salesperson='" + ddlsalesperson.SelectedValue + "'  " +
       //" order by bu asc ");

                    string sql = " EXEC getForecastforMonthnew  " + Convert.ToInt32(ddlMonth.SelectedValue) + ", " + ddlyear.SelectedValue + " ,'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "'";
                    DataSet DsForms = Db.myGetDS(sql);
                    if (DsForms.Tables[0].Rows.Count > 0)
                    {
                        Gridview1.DataSource = DsForms.Tables[0];
                        Gridview1.DataBind();
                    }
                    else
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        using (conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP")))
                        {
                            using (cmd = new SqlCommand("tejGetBUforecast", conn))
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
                }

                int curmnt = DateTime.Now.Month;
                int curyr = DateTime.Now.Year;
                if (curmnt <= Convert.ToInt32(ddlMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue) && curmnt <= Convert.ToInt32(ddltomonth.SelectedValue))
                {
                    Gridview1.Enabled = true;
                }
                else
                {
                    Gridview1.Enabled = false;
                }
            }
            else
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                DataTable dtMailSettings = new DataTable();
                SqlConnection conn;
                SqlDataAdapter adp = new SqlDataAdapter();
                SqlCommand cmd;
                DataSet DsForms = new DataSet();
                using (conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP")))
                {
                    using (cmd = new SqlCommand("tejGetBUforecast", conn))
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
        try
        {
            DateTime month = Convert.ToDateTime("1/1/2018");

            for (int i = 0; i < 12; i++)
            {
                DateTime nextmnth = month.AddMonths(i);
                ListItem ls = new ListItem();
                ls.Text = nextmnth.ToString("MMM");
                ls.Value = nextmnth.ToString("MM");
                ddlMonth.Items.Add(ls);
                ddltomonth.Items.Add(ls);
            }
            DateTime mnth = DateTime.Now;

            ddlMonth.Items.Insert(0, new ListItem(" -- Select-- ", "0"));

            ddltomonth.Items.Insert(0, new ListItem(" -- Select-- ", "0"));
            ddlMonth.SelectedValue = mnth.ToString("MM");
            ddltomonth.SelectedValue = mnth.ToString("MM");

            DateTime year = DateTime.Now;

            //for (int i = 0; i < 1; i++)
            //{
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
            // }
            ddlyear.SelectedValue = DateTime.Now.Year.ToString();

            string ForecastSuperuser = myGlobal.getAppSettingsDataForKey("ForecastSuperuser");

            string[] ForecastSuperuserList = ForecastSuperuser.Split(';');

            string sql = string.Empty;

            foreach (string usrs in ForecastSuperuserList)
            {
                if (myGlobal.loggedInUser().ToLower().Contains(usrs.ToLower()))
                {
                    sql = "select salesperson,alias from dbo.tejSalespersonMap where isactive=1  ; select salesperson,alias from dbo.tejSalespersonMap where isactive=1 and membershipuser='" + myGlobal.loggedInUser() + "' ";
                }
            }

            if (string.IsNullOrEmpty(sql))
            {
                // sql = "select id,alias from dbo.tejSalespersonMap where isactive=1 and membershipuser='" + myGlobal.loggedInUser() + "'";
                //sql = "select salesperson,alias from dbo.tejSalespersonMap where isactive=1 and manager in(select ID from dbo.tejSalespersonMap where membershipuser='" + myGlobal.loggedInUser() + "')" +
                //      " union all  select salesperson,alias from dbo.tejSalespersonMap where isactive=1 and membershipuser='" + myGlobal.loggedInUser() + "'    ; select salesperson,alias from dbo.tejSalespersonMap where isactive=1 and membershipuser='" + myGlobal.loggedInUser() + "' ";
                sql= " EXEC GetSalesTeamToEnterForecast  '"+myGlobal.loggedInUser()+"'";
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = new DataSet();
            DataTable dtsales = new DataTable();
            DS = Db.myGetDS(sql);
            if (DS.Tables.Count > 0)
            {
                ddlsalesperson.DataSource = DS.Tables[0];
                ddlsalesperson.DataTextField = "alias";
                ddlsalesperson.DataValueField = "salesperson";
                ddlsalesperson.DataBind();
                ddlsalesperson.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            //// To select salesperson as Logged in user
            try
            {
                if (DS.Tables[1].Rows.Count > 0)
                {
                    ddlsalesperson.SelectedItem.Value = DS.Tables[1].Rows[0]["salesperson"].ToString();
                    ddlsalesperson.SelectedItem.Text = DS.Tables[1].Rows[0]["alias"].ToString();
                    ddlsalesperson_SelectedIndexChanged(null, null);
                }
                else
                {
                    ddlsalesperson.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ddlsalesperson.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Bindddl() :" + ex.Message;
        }
    }

    public bool Validate()
    {
        bool IsAllDataCorrect = true;
        try
        {
            lblMsg.Text = "";
            bool IsDataChanged = false;

            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                GridViewRow row = Gridview1.Rows[i];
                Label lblBU = (Label)row.FindControl("lblID");

                Label lblrevenue = (Label)row.FindControl("lblrevenue");  // Revenue Target
                Label lblgp = (Label)row.FindControl("lblgp");             // GP Target

                TextBox txtrevtarget = (TextBox)row.FindControl("txtrevenuetarget"); // Rev Forecast RR
                TextBox txtgptarget = (TextBox)row.FindControl("txtgptargets");     //  GP Forecast RR
                TextBox txtrevtargetbtb = (TextBox)row.FindControl("txtrevenuetargetbtb"); // Rev Forecast BTB
                TextBox txtgptargetbtb = (TextBox)row.FindControl("txtgptargetsbtb");   // GP Forecast BTB
                string BU = lblBU.Text;

                double RevForecastRR = 0.0, RevForecastBTB = 0.0, GPForecastRR = 0.0, GPForecastBTB = 0.0;

                if (txtrevtarget.Text.Trim() != "")
                {
                    RevForecastRR = Convert.ToDouble(txtrevtarget.Text.Trim());
                }

                if (txtgptarget.Text.Trim() != "")
                {
                    GPForecastRR = Convert.ToDouble(txtgptarget.Text.Trim());
                }

                if (RevForecastRR > 0 && GPForecastRR <= 0)
                {
                    lblMsg.Text = "Please enter GP Forecast RR for " + BU;
                    IsAllDataCorrect = false;
                }

                if (RevForecastRR <= 0 && GPForecastRR > 0)
                {
                    lblMsg.Text = "Please enter Rev Forecast RR for " + BU;
                    IsAllDataCorrect = false;
                }

                if (txtrevtargetbtb.Text.Trim() != "")
                {
                    RevForecastBTB = Convert.ToDouble(txtrevtargetbtb.Text.Trim());
                }
                if (txtgptargetbtb.Text.Trim() != "")
                {
                    GPForecastBTB = Convert.ToDouble(txtgptargetbtb.Text.Trim());
                }

                if (RevForecastBTB > 0 && GPForecastBTB <= 0)
                {
                    lblMsg.Text = "Please enter GP Forecast BTB for " + BU;
                    IsAllDataCorrect = false; 
                }
                if (RevForecastBTB <= 0 && GPForecastBTB > 0)
                {
                    lblMsg.Text = "Please enter Rev Forecast BTB for " + BU;
                    IsAllDataCorrect = false;
                }

                if (RevForecastRR > 0 || RevForecastBTB > 0)
                {
                    IsDataChanged = true;
                }
            }

            if (ddlsalesperson.SelectedItem.Text == "-- Select --")
            {
                lblMsg.Text = "Please select salesperson";
                IsAllDataCorrect = false;
            }
            else if (rbListCountries == null || rbListCountries.SelectedItem.Value == "")
            {
                lblMsg.Text = "Please select country";
                IsAllDataCorrect = false;
            }
            else if (Gridview1.Rows.Count == 0)
            {
                lblMsg.Text = "No data found to save";
                IsAllDataCorrect = false;
            }
            else if (IsDataChanged == false)
            {
                lblMsg.Text = "Please enter Forecast for atleast one BU.";
                IsAllDataCorrect = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured in Validation:"+ ex.Message;
        }

        return IsAllDataCorrect;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnsave1.Enabled = false;
            BtnSave.Enabled = false;

            bool isAllDataCorrect = Validate();
            if (isAllDataCorrect == false)
            {
                btnsave1.Enabled = true;
                BtnSave.Enabled = true;
                return;
            }
            int mn = Convert.ToInt32(ddlMonth.SelectedValue);
            
            for (int i = 0; i < Gridview1.Rows.Count; i++)
            {
                GridViewRow row = Gridview1.Rows[i];
                Label lblBU = (Label)row.FindControl("lblID");

                Label lblrevenue = (Label)row.FindControl("lblrevenue");  // Revenue Target
                Label lblgp = (Label)row.FindControl("lblgp");             // GP Target

                TextBox txtrevtarget = (TextBox)row.FindControl("txtrevenuetarget"); // Rev Forecast RR
                TextBox txtgptarget = (TextBox)row.FindControl("txtgptargets");     //  GP Forecast RR
                TextBox txtrevtargetbtb = (TextBox)row.FindControl("txtrevenuetargetbtb"); // Rev Forecast BTB
                TextBox txtgptargetbtb = (TextBox)row.FindControl("txtgptargetsbtb");   // GP Forecast BTB
                TextBox txtrevtargetpercent = (TextBox)row.FindControl("txtforcastvstarget");   // Rev Forecast Vs Rev Trget
                TextBox txtgptargetpercent = (TextBox)row.FindControl("txtforcastvsgptarget");  // GP Forecast Vs GP Target
                string BU = lblBU.Text;

                double revForecastRR = 0.0;
                if (txtrevtarget.Text != "")
                {
                    revForecastRR = Convert.ToDouble(txtrevtarget.Text);
                }

                double revForecastBTB = 0.0;
                if (txtrevtargetbtb.Text != "")
                {
                    revForecastBTB = Convert.ToDouble(txtrevtargetbtb.Text);
                }

                double RevTarget = 0.0;
                if (lblrevenue.Text != "")
                {
                    RevTarget = Convert.ToDouble(lblrevenue.Text);
                }

                double revtargetpercent = 0.0;
                if (RevTarget != 0)
                {
                    revtargetpercent = ((revForecastRR + revForecastBTB) / RevTarget) * 100;
                }

                double gpTarget = 0.0;
                if (lblgp.Text != "")
                {
                    gpTarget = Convert.ToDouble(lblgp.Text);
                }

                double gpForecastRR = 0.0;
                if (txtgptarget.Text != "")
                {
                    gpForecastRR = Convert.ToDouble(txtgptarget.Text);
                }

                double gpForecastBTB = 0.0;
                if (txtgptargetbtb.Text != "")
                {
                    gpForecastBTB = Convert.ToDouble(txtgptargetbtb.Text);
                }

                double gppercent = 0.0;
                if (gpTarget != 0)
                {
                    gppercent = ((gpForecastRR + gpForecastBTB) / gpTarget) * 100;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalesForecast where year=" + ddlyear.SelectedValue + " and month = " + mn + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' and BU ='" + BU + "'");
                if (count > 0)
                {
                    Db.myExecuteSQL("update  dbo.tejSalesForecast set RevenueRR=" + revForecastRR + ",RevenueBTB=" + revForecastBTB + ",GPRR=" + gpForecastRR + ",GPBTB=" + gpForecastBTB + ",RevenuePER=" + revtargetpercent + ",GPBPER=" + gppercent + " , LastUpdatedOn=getdate(),LastUpdatedBy='"+myGlobal.loggedInUser()+"' " +
                                        "where year=" + ddlyear.SelectedValue + " and month = " + mn + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' and BU ='" + BU + "'");
                }
                else
                {
                    Db.myExecuteSQL("insert into dbo.tejSalesForecast(Year,Month,country,salesperson,BU,RevenueRR,RevenueBTB,GPRR,GPBTB,RevenuePER,GPBPER,CreatedOn,CreatedBy) values (" +
                        " " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + BU + "'," + revForecastRR + "," + revForecastBTB + ", " + gpForecastRR + "," + gpForecastBTB + "," + revtargetpercent + "," + gppercent + ",getdate(),'"+myGlobal.loggedInUser()+"')");
                }
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable dtMailSettings = new DataTable();
            SqlConnection conn;
            SqlDataAdapter adp = new SqlDataAdapter();
            SqlCommand cmd;
            DataSet DsForms = new DataSet();
            using (conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP")))
            {
                using (cmd = new SqlCommand("setForecastforMonthFromSalesPersonForecast", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@p_Month", SqlDbType.SmallInt).Value = mn;
                    cmd.Parameters.Add("@p_Year", SqlDbType.Int).Value = ddlyear.SelectedValue;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            if (rbListCountries.SelectedValue != "" && ddlyear.SelectedItem.Text != "" && ddlsalesperson.SelectedValue != "")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string mail = "  EXEC getForecastDataToSendEmail  " + ddlyear.SelectedItem.Text + " , " + mn.ToString() + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + ddlsalesperson.SelectedItem.Text + "','" + myGlobal.loggedInUser() + "'";
                Db.myExecuteSQL(mail);
            }

            #region " Old code to send email "
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
                //                string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids,  ddlMonth.SelectedItem.Text +"-"+ddlyear.SelectedItem.Text+" forecast has been added/updated for " + ddlsalesperson.SelectedItem.Text + " by " + myGlobal.loggedInUser(), ForecastDataForMail, true);
                            //            }
            #endregion


            lblMsg.Text = " Forcast saved successfully";

            Gridview1.Visible = false;
            ClearControl();

            btnsave1.Enabled = true;
            BtnSave.Enabled = true;

        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
            btnsave1.Enabled = true;
            BtnSave.Enabled = true;
        }
    }

    public string GetGridviewData(GridView gv)
    {
        StringBuilder strBuilder = new StringBuilder();
        try
        {
            StringWriter strWriter = new StringWriter(strBuilder);
            HtmlTextWriter htw = new HtmlTextWriter(strWriter);
            gv.RenderControl(htw);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in GetGridviewData() : " + ex.Message;
        }
        return strBuilder.ToString();
    }
    
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
        return;
    }

    public void ClearControl()
    {
        try
        {
            pnlFormList.Visible = true;
            BtnSave.Text = "Save";
            Gridview1.SelectedIndex = -1;
            rbListCountries.ClearSelection();
            rbListCountries.Visible = false;


            DateTime mnth = DateTime.Now;
            ddlMonth.SelectedValue = mnth.ToString("MM");
            ddltomonth.SelectedValue = mnth.ToString("MM");
            ddlyear.SelectedValue = DateTime.Now.Year.ToString();

            ddlsalesperson.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ClearControl() " + ex.Message ;
        }
    }

    //protected void Gridview1_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int row = e.RowIndex;

    //    TextBox revtar = (TextBox)Gridview1.Rows[row].FindControl("txtrevenuetarget");

    //    TextBox fortarper = (TextBox)Gridview1.Rows[row].FindControl("txtforcastvstarget");
    //    fortarper.Text = revtar.Text;

    //}



    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TextBox tmptxtQty = (TextBox)e.Row.FindControl("txtrevenuetarget");
    //        //tmptxtQty.Attributes.Add("onBlur", "updateValuesNew(" + (e.Row.RowIndex).ToString() + ",'');");

    //        //TextBox tmptxtCurrPrice = (TextBox)e.Row.FindControl("txtrevenuetargetbtb");
    //        //tmptxtCurrPrice.Attributes.Add("onBlur", "updateValuesNew(" + (e.Row.RowIndex).ToString() + ",'');");

    //        //Label tmplblAmountTotal = (Label)e.Row.FindControl("txtforcastvstarget");
    //        //tmplblAmountTotal.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtQty.Text) + Convert.ToDouble(tmptxtCurrPrice.Text)));

    //        //TextBox tmptxtRebatePerUnit = (TextBox)e.Row.FindControl("txtgptargets");
    //        //tmptxtRebatePerUnit.Attributes.Add("onBlur", "updateValuesNew(" + (e.Row.RowIndex).ToString() + ",'');");

    //        //TextBox tmplblCostAfterRebate = (TextBox)e.Row.FindControl("txtgptargetsbtb");
    //        //tmplblCostAfterRebate.Attributes.Add("onBlur", "updateValuesNew(" + (e.Row.RowIndex).ToString() + ",'');");

    //        //Label tmplblTotalCostAfterRebate = (Label)e.Row.FindControl("txtforcastvsgptarget");
    //        //tmplblTotalCostAfterRebate.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtRebatePerUnit.Text) + Convert.ToDouble(tmplblCostAfterRebate.Text)));

           
    //    }
    //}
    



    //protected void txtrevenuetarget_TextChanged(object sender, EventArgs e)
    //{
    //    int row = Gridview1.EditIndex;

    //    TextBox revtar = (TextBox)Gridview1.Rows[row].FindControl("txtrevenuetarget");

    //    TextBox fortarper = (TextBox)Gridview1.Rows[row].FindControl("txtforcastvstarget");
    //    fortarper.Text = revtar.Text;
    //}


    //protected void txtrevenuetargetbtb_TextChanged(object sender, EventArgs e)
    //{

    //    TextBox revtar = (TextBox)Gridview1.Rows[Gridview1.EditIndex].FindControl("txtrevenuetargetbtb");

    //    TextBox fortarper = (TextBox)Gridview1.Rows[Gridview1.EditIndex].FindControl("txtforcastvstarget");
    //    fortarper.Text =fortarper.Text+ revtar.Text;
    //}
  
    // protected void txtgptargets_TextChanged(object sender, EventArgs e)
    //{

    //    TextBox revtar = (TextBox)Gridview1.Rows[Gridview1.EditIndex].FindControl("txtgptargets");

    //    TextBox fortarper = (TextBox)Gridview1.Rows[Gridview1.EditIndex].FindControl("txtforcastvsgptarget");
    //    fortarper.Text = revtar.Text;
    //}
  
    // protected void txtgptargetsbtb_TextChanged(object sender, EventArgs e)
    //{

    //    TextBox revtar = (TextBox)Gridview1.Rows[Gridview1.EditIndex].FindControl("txtgptargetsbtb");

    //    TextBox fortarper = (TextBox)Gridview1.Rows[Gridview1.EditIndex].FindControl("txtforcastvsgptarget");
    //    fortarper.Text = fortarper.Text+ revtar.Text;
    //}

    protected void ddlsalesperson_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //int count = Db.LoadlstWithCon("select * from dbo.rddCountriesList");

            //  RadioButtonList countrylst = new RadioButtonList();
            // Db.LoadlstWithCon("select * from dbo.rddCountriesList");
            string sql;
            sql = "select a.country CountryCode ,c.Country  Country from  sales_Employee_country a,rddCountriesList c where a.country=c.CountryCode and  a.salesempid='" + ddlsalesperson.SelectedValue + "'";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable dtRoles = new DataTable();
            dtRoles = Db.myGetDS(sql).Tables[0];
            rbListCountries.DataSource = dtRoles;
            rbListCountries.DataTextField = "Country";
            rbListCountries.DataValueField = "CountryCode";
            rbListCountries.DataBind();
            rbListCountries.Visible = true;
            int curmnt = DateTime.Now.Month;
            int curyr = DateTime.Now.Year;
            if (curmnt <= Convert.ToInt32(ddlMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
            {
                Gridview1.Enabled = true;
            }
            else
            {

                Gridview1.Enabled = false;
            }

            if (rbListCountries.Items.Count >= 1)
            {
                rbListCountries.SelectedIndex = 0;
            }

            if (ddlyear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && rbListCountries.SelectedValue != "" && ddltomonth.SelectedIndex != 0)
            {
                bindgrid();
                GridCalculations();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in ddlsalesperson_SelectedIndex() :" + ex.Message;
        }
    }

    protected void btnsave1_Click(object sender, EventArgs e)
    {
        try
        {
                lblMsg.Text = "";
                btnsave1.Enabled = false;
                BtnSave.Enabled = false;

                bool isAllDataCorrect = Validate();
                if (isAllDataCorrect == false)
                {
                    btnsave1.Enabled = true;
                    BtnSave.Enabled = true;
                    return;
                }
                int mn =Convert.ToInt32( ddlMonth.SelectedValue);
                    for (int i = 0; i < Gridview1.Rows.Count; i++)
                    {
                        GridViewRow row = Gridview1.Rows[i];
                        Label lblBU = (Label)row.FindControl("lblID");

                        Label lblrevenue = (Label)row.FindControl("lblrevenue");  // Revenue Target
                        Label lblgp = (Label)row.FindControl("lblgp");             // GP Target

                        TextBox txtrevtarget = (TextBox)row.FindControl("txtrevenuetarget"); // Rev Forecast RR
                        TextBox txtgptarget = (TextBox)row.FindControl("txtgptargets");     //  GP Forecast RR
                        TextBox txtrevtargetbtb = (TextBox)row.FindControl("txtrevenuetargetbtb"); // Rev Forecast BTB
                        TextBox txtgptargetbtb = (TextBox)row.FindControl("txtgptargetsbtb");   // GP Forecast BTB
                        TextBox txtrevtargetpercent = (TextBox)row.FindControl("txtforcastvstarget");   // Rev Forecast Vs Rev Trget
                        TextBox txtgptargetpercent = (TextBox)row.FindControl("txtforcastvsgptarget");  // GP Forecast Vs GP Target
                        string BU = lblBU.Text;
                       
                        double revForecastRR=0.0;
                        if (txtrevtarget.Text != "")
                        {
                            revForecastRR = Convert.ToDouble(txtrevtarget.Text);
                        }

                        double revForecastBTB=0.0;
                        if (txtrevtargetbtb.Text != "")
                        {
                            revForecastBTB = Convert.ToDouble(txtrevtargetbtb.Text);
                        }

                        double RevTarget=0.0;
                        if (lblrevenue.Text != "")
                        {
                            RevTarget = Convert.ToDouble(lblrevenue.Text);
                        }

                        double revtargetpercent=0.0;
                        if (RevTarget != 0)
                        {
                            revtargetpercent = ((revForecastRR + revForecastBTB) / RevTarget ) * 100;
                        }

                        double gpTarget = 0.0;
                        if (lblgp.Text != "")
                        {
                            gpTarget = Convert.ToDouble(lblgp.Text);
                        }

                        double gpForecastRR=0.0;
                        if (txtgptarget.Text != "")
                        {
                            gpForecastRR = Convert.ToDouble(txtgptarget.Text);
                        }

                        double gpForecastBTB=0.0;
                        if (txtgptargetbtb.Text != "")
                        {
                            gpForecastBTB = Convert.ToDouble(txtgptargetbtb.Text);
                        }
                      
                        double gppercent=0.0;
                        if (gpTarget != 0)
                        {
                            gppercent = ((gpForecastRR + gpForecastBTB) / gpTarget) * 100;
                        }



                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        int count = Db.myExecuteScalar("select  count(*) from dbo.tejSalesForecast where year=" + ddlyear.SelectedValue + " and month = " + mn + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' and BU ='" + BU + "'");
                        if (count > 0)
                        {
                            Db.myExecuteSQL("update  dbo.tejSalesForecast set RevenueRR=" + revForecastRR + ",RevenueBTB=" + revForecastBTB + ",GPRR=" + gpForecastRR + ",GPBTB=" + gpForecastBTB + ",RevenuePER=" + revtargetpercent + ",GPBPER=" + gppercent + ", LastUpdatedOn=GETDATE(),LastUpdatedBy='"+myGlobal.loggedInUser()+"'" +
                                                "where year=" + ddlyear.SelectedValue + " and month = " + mn + " and country='" + rbListCountries.SelectedValue + "' and salesperson='" + ddlsalesperson.SelectedValue + "' and BU ='" + BU + "'");
                        }
                        else
                        {
                            Db.myExecuteSQL("insert into dbo.tejSalesForecast(Year,Month,country,salesperson,BU,RevenueRR,RevenueBTB,GPRR,GPBTB,RevenuePER,GPBPER,CreatedOn,CreatedBy) values (" +
                                " " + ddlyear.SelectedValue + "," + mn + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + BU + "'," + revForecastRR + "," + revForecastBTB + ", " + gpForecastRR + "," + gpForecastBTB + "," + revtargetpercent + "," + gppercent + ",GETDATE(),'"+myGlobal.loggedInUser()+"')");
                        }
                    }

                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    DataTable dtMailSettings = new DataTable();
                    SqlConnection conn;
                    SqlDataAdapter adp = new SqlDataAdapter();
                    SqlCommand cmd;
                    DataSet DsForms = new DataSet();
                    using (conn = new SqlConnection(myGlobal.getAppSettingsDataForKey("tejSAP")))
                    {
                        using (cmd = new SqlCommand("setForecastforMonthFromSalesPersonForecast", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@p_Month", SqlDbType.SmallInt).Value = mn;
                            cmd.Parameters.Add("@p_Year", SqlDbType.Int).Value = ddlyear.SelectedValue;
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (rbListCountries.SelectedValue != "" && ddlyear.SelectedItem.Text != "" && ddlsalesperson.SelectedValue!="")
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        string mail = "  EXEC getForecastDataToSendEmail  " + ddlyear.SelectedItem.Text + " , " + mn.ToString() + ",'" + rbListCountries.SelectedValue + "','" + ddlsalesperson.SelectedValue + "','" + ddlsalesperson.SelectedItem.Text + "','" + myGlobal.loggedInUser() + "'";
                        Db.myExecuteSQL(mail);
                    }
                    
                    #region "Old code to send email "
                    //                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    //                    DataSet Dsemails = Db.myGetDS(@" select receipient_emailId from EmailConfig
                    //                                        union all
                    //                                        select email from tejsalespersonmap where MembershipUser= '"+myGlobal.loggedInUser()+"' Union all	select email from tejsalespersonmap where id  = (  select top 1 manager from tejsalespersonmap where MembershipUser= '"+myGlobal.loggedInUser()+"' )");
                    //                    string emailids = "";
                    //                    if (Dsemails.Tables.Count > 0)
                    //                    {
                    //                        if (Dsemails.Tables[0].Rows.Count > 0)
                    //                        {
                    //                            for (int i = 0; i < Dsemails.Tables[0].Rows.Count; i++)
                    //                            {
                    //                                if (string.IsNullOrEmpty(emailids))
                    //                                {
                    //                                    emailids = Dsemails.Tables[0].Rows[i][0].ToString();
                    //                                }
                    //                                else
                    //                                {
                    //                                    emailids = emailids + "," + Dsemails.Tables[0].Rows[i][0].ToString();
                    //                                }
                    //                            }
                    //                        }
                    //                    }

                    //                    string ForecastDataForMail = GetGridviewData(Gridview1).ToString();
                    //                    if (!string.IsNullOrEmpty(ForecastDataForMail.Trim()) && !string.IsNullOrEmpty(emailids.Trim()))
                    //                    {
                    //                        string msg = Mail.Send(myGlobal.getSystemConfigValue("websiteEmailer"), emailids, "  Forecast has been Added/Updated for " + ddlsalesperson.SelectedItem.Text + " by " + myGlobal.loggedInUser(), ForecastDataForMail, true);
                    //                    }
                    #endregion

                    lblMsg.Text = " Forcast saved successfully";
                       
                    Gridview1.Visible = false;
                    ClearControl();
                   
                    btnsave1.Enabled = true;
                    BtnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            Gridview1.Visible = false;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;

            btnsave1.Enabled = true;
            BtnSave.Enabled = true;
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddltomonth.SelectedValue = ddlMonth.SelectedValue;
            ddltomonth.SelectedItem.Text = ddlMonth.SelectedItem.Text;

            if (ddlyear.SelectedIndex != 0 && rbListCountries.SelectedValue != "" && (ddlsalesperson.SelectedIndex != 0 || ddlsalesperson.SelectedItem.Value !="--Select--" ) && ddltomonth.SelectedIndex != 0)
            {
                bindgrid();
                GridCalculations();
            }
            int curmnt = DateTime.Now.Month;
            int curyr = DateTime.Now.Year;
            if (curmnt <= Convert.ToInt32(ddlMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
            {
                Gridview1.Enabled = true;
            }
            else
            {
                Gridview1.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ddlMonthSelection : " + ex.Message;
        }
    }

    protected void ddltomonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlyear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0 && (ddlsalesperson.SelectedIndex != 0  || ddlsalesperson.SelectedItem.Text=="--Select--" )  && rbListCountries.SelectedValue != "")
            {
                bindgrid();
                
            }
            int curmnt = DateTime.Now.Month;
            int curyr = DateTime.Now.Year;
            if (curmnt <= Convert.ToInt32(ddltomonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
            {
                Gridview1.Enabled = true;
            }
            else
            {

                Gridview1.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ddlTOMonth Selection : " + ex.Message;
        }
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonth.SelectedIndex != 0 && rbListCountries.SelectedValue != "" && ( ddlsalesperson.SelectedIndex != 0  || ddlsalesperson.SelectedItem.Text=="--Select--" ) && ddltomonth.SelectedIndex != 0)
            {
                bindgrid();
                GridCalculations();
            }
            int curmnt = DateTime.Now.Month;
            int curyr = DateTime.Now.Year;
            if (curmnt <= Convert.ToInt32(ddlMonth.SelectedValue) && curyr <= Convert.ToInt32(ddlyear.SelectedValue))
            {
                Gridview1.Enabled = true;
            }
            else
            {
                Gridview1.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ddlYear selection: " + ex.Message;
        }
    }


    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label LblRevTarget = (Label)e.Row.FindControl("lblrevenue"); // Rev Target
                if (!string.IsNullOrEmpty(LblRevTarget.Text))
                {
                    RevTargetTotal = RevTargetTotal + Convert.ToDouble(LblRevTarget.Text);
                }

                TextBox txtrevenuetarget = (TextBox)e.Row.FindControl("txtrevenuetarget");  // Rev Forecast RR
                if (!string.IsNullOrEmpty(txtrevenuetarget.Text))
                {
                    RevForecastRRTotal = RevForecastRRTotal + Convert.ToDouble(txtrevenuetarget.Text);
                }
                txtrevenuetarget.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

                TextBox txtrevenuetargetbtb = (TextBox)e.Row.FindControl("txtrevenuetargetbtb");  // Rev Forecast BTB
                if (!string.IsNullOrEmpty(txtrevenuetargetbtb.Text))
                {
                    RevForecastBTBTotal = RevForecastBTBTotal + Convert.ToDouble(txtrevenuetargetbtb.Text);
                }
                txtrevenuetargetbtb.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

                Label lblgpTarget = (Label)e.Row.FindControl("lblgp"); // GP Target
                if (!string.IsNullOrEmpty(lblgpTarget.Text))
                {
                    GPTargetTotal = GPTargetTotal + Convert.ToDouble(lblgpTarget.Text);
                }

                TextBox txtgptargetsbtb = (TextBox)e.Row.FindControl("txtgptargetsbtb");  // GP Forecast BTB
                if (!string.IsNullOrEmpty(txtgptargetsbtb.Text))
                {
                    GPForecastBTBTotal = GPForecastBTBTotal + Convert.ToDouble(txtgptargetsbtb.Text);
                }
                txtgptargetsbtb.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

                TextBox txtgptargets = (TextBox)e.Row.FindControl("txtgptargets");  // GP Forecast RR
                if (!string.IsNullOrEmpty(txtgptargets.Text))
                {
                    GPForecastRRTotal = GPForecastRRTotal + Convert.ToDouble(txtgptargets.Text);
                }
                txtgptargets.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");
            
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblRevTargetTotal = (Label)e.Row.FindControl("lblRevTargetTotal");
                lblRevTargetTotal.Text = RevTargetTotal.ToString();

                Label lblRevForecastRRTotal = (Label)e.Row.FindControl("lblRevForecastRRTotal");
                lblRevForecastRRTotal.Text = RevForecastRRTotal.ToString();

                Label lblRevForecastBTBTotal = (Label)e.Row.FindControl("lblRevForecastBTBTotal");
                lblRevForecastBTBTotal.Text = RevForecastBTBTotal.ToString();

                Label lblGPTargetTotal = (Label)e.Row.FindControl("lblGPTargetTotal");
                lblGPTargetTotal.Text = GPTargetTotal.ToString();

                Label lblGPForecastRRTotal = (Label)e.Row.FindControl("lblGPForecastRRTotal");
                lblGPForecastRRTotal.Text = GPForecastRRTotal.ToString();

                Label lblGPForecastBTBTotal = (Label)e.Row.FindControl("lblGPForecastBTBTotal");
                lblGPForecastBTBTotal.Text = GPForecastBTBTotal.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error In Gridview1_RowDataBound() : " + ex.Message;
        }
    }

    protected void BtnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string salespersons = string.Empty;
            for (int j = 0; j < ddlsalesperson.Items.Count ; j++)
            {
                if (string.IsNullOrEmpty(salespersons))
                {
                    salespersons =  ddlsalesperson.Items[j].Value ;
                }
                else
                {
                    salespersons = salespersons + "," + ddlsalesperson.Items[j].Value ;
                }
            }

            DataTable DT = Db.myGetDS( " EXEC GetForecastDataToExportInExcel "+ ddlMonth.SelectedItem.Value +","+ ddlyear.SelectedItem.Text +",'"+ rbListCountries.SelectedValue +"','"+ salespersons+ "' " ).Tables[0];
            string attachment = "attachment; filename=Forecast_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in DT.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in DT.Rows)
            {
                tab = "";
                for (i = 0; i < DT.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();


        }
        catch (Exception ex)
        {
            //lblMsg.Text = "Error occured to export data in excel";
        }
    }
}