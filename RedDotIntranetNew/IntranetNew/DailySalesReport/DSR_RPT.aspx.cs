using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections;


public partial class IntranetNew_DailySalesReport_DSR_RPT : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        //         int count = Db.myExecuteScalar(@"select count(*)  from DSR_CustomerVisitTarget Where isnull(ViewScore,0)=1 And DesigName = ( Select D.Designation
        //															from tejSalesPersonMap S 
        //																Join Designation_Master D On S.Designation =  D.id
        //															Where MembershipUser='"+myGlobal.loggedInUser()+"' ) " );
        //         if (count > 0)
        //         {
        if (!IsPostBack)
        {
            string LoggedInUserName = myGlobal.loggedInUser();
            fillyear();
            fillmonth();
            //   fillGetsalesPerson();
            fillCountry();
            Chart3.Visible = false;
        }
        // }
        //else
        //{
        //    Response.Redirect("Default.aspx?UserAccess=0&FormName=View Score Card");
        //}

    }

    protected void GVScoreCard1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();


            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;PERFORMANCE&nbsp;&nbsp;";
            // HeaderCell.BackColor = Color.Green;
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CUSTOMER&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            // /HeaderCell.BackColor = Color.Blue;
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ITEM STATUS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //HeaderCell.BackColor = Color.BurlyWood;
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;&nbsp;PROSPECTS&nbsp;&nbsp;&nbsp;";
            // HeaderCell.BackColor = Color.Green;
            HeaderCell.ColumnSpan = 2;

            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;FWDS&nbsp;&nbsp;";
            // HeaderCell.BackColor = Color.GreenYellow;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.RowSpan = 2;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;TRGT&nbsp;<br/>(%)&nbsp;&nbsp;";
            // HeaderCell.BackColor = Color.BurlyWood;
            HeaderCell.RowSpan = 2;
            HeaderCell.ColumnSpan = 1;

            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;&nbsp;ACHIEVED&nbsp;&nbsp;&nbsp;";
            HeaderCell.BackColor = Color.Black;
            HeaderCell.RowSpan = 2;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.BorderColor = Color.Red;
          //  HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);


           
            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;&nbsp;BONUS ON &nbsp;<br/> NEW CUST&nbsp;&nbsp;";           
            HeaderCell.BackColor = Color.Black;
           // HeaderCell.ColumnSpan = 1;
            HeaderCell.RowSpan = 2;
            HeaderCell.BorderColor = Color.Red;
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "&nbsp;&nbsp;&nbsp;TOTAL &nbsp;&nbsp;&nbsp;";
            HeaderCell.BackColor = Color.Black;
            HeaderCell.ColumnSpan = 1;
            HeaderCell.RowSpan = 2;
            HeaderCell.BorderColor = Color.Red;
            HeaderGridRow.Cells.Add(HeaderCell);

            

            GVScoreCard1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonth.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Please Select Month .'); </script>");
                return;
            }
            if (ddlcountry.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Please Select Country .'); </script>");
                return;
            }
            if (ddlemp.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Please Select Person .'); </script>");
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();
            string UserName = string.Empty;

            bool IsGetScoreforALL = false;
            if (ddlemp.SelectedItem.Text.Trim().ToUpper() == "ALL")
            {
                IsGetScoreforALL = true;
                UserName = myGlobal.loggedInUser();
            }
            else
            {
                UserName = ddlemp.SelectedItem.Value;
            }
            string month = string.Empty;
            foreach (ListItem i in ddlMonth.Items)
            {
                if (i.Selected == true)
                {
                    //month += i.Text + ",";
                    if (string.IsNullOrEmpty(month))
                    {
                        month = "" + i.Value + "";
                    }
                    else
                    {
                        month += "," + i.Value + "";
                    }
                }

            }
            string cou = string.Empty;
            foreach (ListItem i in ddlcountry.Items)
            {
                if (i.Selected == true)
                {
                    if (string.IsNullOrEmpty(cou))
                    {
                        cou = "" + i.Value + "";
                    }
                    else
                    {
                        cou += "," + i.Value + "";
                    }
                }
            }
            string empname = string.Empty;
            foreach (ListItem i in ddlemp.Items)
            {
                if (i.Selected == true)
                {
                    if (string.IsNullOrEmpty(empname))
                    {

                        empname = "" + i.Value + "";

                    }
                    else
                    {
                        empname += "," + i.Value + "";
                    }
                }
            }


                DataSet ds = Db.myGetDS("Exec DSR_GetScoreCardByNameNEW  '" + month + "','" + ddlyear.SelectedItem.Text + "','" + cou + "', '" + empname + "'");
            //  DataSet ds = Db.myGetDS("Exec DSR_GetScoreCardByName  '" + UserName + "','" + ddlMonth.SelectedValue + "','" + ddlyear.SelectedItem.Text + "'," + IsGetScoreforALL);

            if (ds.Tables.Count > 0)
            {
                GVScoreCard.DataSource = ds.Tables[0];
                GVScoreCard.DataBind();
            }

            if (ds.Tables.Count > 0)
            {
                GVScoreCard1.DataSource = ds.Tables[0];
                GVScoreCard1.DataBind();
            }

            string[] EmplName = new string[ds.Tables[0].Rows.Count];
            double[] Daily = new double[ds.Tables[0].Rows.Count];
            double[] Weekly = new double[ds.Tables[0].Rows.Count];
            double[] Monthly = new double[ds.Tables[0].Rows.Count];
            double[] Targetpoint = new double[ds.Tables[0].Rows.Count];
            string name = string.Empty;
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    EmplName[i] = ds.Tables[0].Rows[i][0].ToString();

                    // Daily[i] = Convert.ToDouble(ds.Tables[0].Rows[i][1].ToString().Replace('%', ' '));
                    // Weekly[i] = Convert.ToDouble(ds.Tables[0].Rows[i][2].ToString().Replace('%', ' '));
                    Monthly[i] = Convert.ToDouble(ds.Tables[0].Rows[i][1].ToString().Replace('%', ' '));
                    Targetpoint[i] = Convert.ToDouble(ds.Tables[0].Rows[i][12].ToString().Replace('%', ' '));
                    // //if (string.IsNullOrEmpty(name))


                }
            }

            //Chart1.ChartAreas[0].AxisX.Interval = 1;
            //Chart1.Series["DAILY VISIT"].Points.DataBindXY(EmplName, Daily);
            //// Chart1.Series["DAILY SCORE"].Points[0].Color = Color.MediumSeaGreen;
            //Chart1.Series["DAILY VISIT"].ChartType = SeriesChartType.Column;
            //Chart1.Series["DAILY VISIT"]["PieLabelStyle"] = "Disabled";
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //Chart1.Legends[0].Enabled = true;
            //Chart1.Series["DAILY VISIT"].ToolTip = "#VALX(#VALY%)";

            //for (int i = 0; i < Daily.Length; i++)
            //{
            //    if (i % 2 == 0)
            //    {

            //        Chart1.Series["DAILY VISIT"].Points[i].Color = System.Drawing.Color.Gold;

            //    }
            //    else
            //    {

            //        Chart1.Series["DAILY VISIT"].Points[i].Color = System.Drawing.Color.Red;

            //    }
            //}


            //Chart2.ChartAreas[0].AxisX.Interval = 1;
            //Chart2.Series["WEEKLY VISIT"].Points.DataBindXY(EmplName, Weekly);
            //// Chart2.Series["WEEKLY SCORE"].Points[1].Color = Color.Red;
            //Chart2.Series["WEEKLY VISIT"].ChartType = SeriesChartType.Column;
            //Chart2.Series["WEEKLY VISIT"]["PieLabelStyle"] = "Disabled";
            //Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //Chart2.Legends[0].Enabled = true;
            //Chart2.Series["WEEKLY VISIT"].ToolTip = "#VALX(#VALY%)";
            //// Chart2.Series["WEEKLY VISIT"].Color = Color.Red;
            ////  Chart2.Series["WEEKLY SCORE"].ToolTip = Weekly();

            //for (int i = 0; i < Weekly.Length; i++)
            //{
            //    if (i % 2 == 0)
            //    {

            //        Chart2.Series["WEEKLY VISIT"].Points[i].Color = System.Drawing.Color.Gold;

            //    }
            //    else
            //    {
            //        Chart2.Series["WEEKLY VISIT"].Points[i].Color = System.Drawing.Color.Red;
            //    }
            //}

            Chart3.Visible = true;

            Chart3.ChartAreas[0].AxisX.Interval = 1;
            Chart3.ChartAreas[0].AxisX.LabelStyle.Angle = -60;
            Chart3.Series["ACHIEVED"].Points.DataBindXY(EmplName, Monthly);
            Chart3.Series["TARGET"].Points.DataBindXY(EmplName, Targetpoint);
            // Chart3.Series["MONTHLY SCORE"].Points[2].Color = Color.BlueViolet;
            Chart3.Series["TARGET"].ChartType = SeriesChartType.Column;
            Chart3.Series["ACHIEVED"].ChartType = SeriesChartType.Column;
            


            Chart3.Series["TARGET"]["PieLabelStyle"] = "Disabled";
            Chart3.Series["ACHIEVED"]["PieLabelStyle"] = "Disabled";
            // Chart3.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart3.Legends[0].Enabled = true;
            Chart3.Series["TARGET"].ToolTip = "#VALX(TARGET:#VALY%)";
            Chart3.Series["ACHIEVED"].ToolTip = "#VALX(ACHIEVED:#VALY%)";
           
         
            //Chart3.Series["MONTHLY VISIT"].Color = Color.MediumSeaGreen;
           // Chart3.Series[0].IsValueShownAsLabel = true;
           

            for (int i = 0; i < Targetpoint.Length; i++)
            {
                if (i % 2 == 0)
                {

                    Chart3.Series["TARGET"].Points[i].Color = System.Drawing.Color.Red;
                   
                    
                }
                else
                {
                    Chart3.Series["TARGET"].Points[i].Color = System.Drawing.Color.Red;

                }
            }




            for (int i = 0; i < Monthly.Length; i++)
            {
                if (i % 2 == 0)
                {


                    Chart3.Series["ACHIEVED"].Points[i].Color = System.Drawing.Color.MediumSeaGreen;
                    
                }
                else
                {

                    Chart3.Series["ACHIEVED"].Points[i].Color = System.Drawing.Color.MediumSeaGreen;
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured () : " + ex.Message;
        }

    }

    protected void fillyear()
    {

        string YR = DateTime.Today.ToString("yyyy");

        DataSet ds = Db.myGetDS(" select distinct(YEAR (VisitDate)) as year  from DailySalesReports");
        if (ds.Tables.Count > 0)
        {
            ddlyear.DataSource = ds;
            ddlyear.DataTextField = "year";
            ddlyear.DataValueField = "year";
            ddlyear.DataBind();

            foreach (ListItem i in ddlyear.Items)
            {
                if (i.Text == YR)
                {
                    i.Selected = true;
                }
                else
                {
                    i.Selected = false;
                }
            }

            // ddlyear.SelectedItem.Text = YR;
            // ddlyear.SelectedItem.Value = YR;
        }
    }

    protected void fillCountry()
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();




        DataSet DS = Db.myGetDS("select c.country,c.countrycode from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID And  Membershipuser='" + myGlobal.loggedInUser() + "'  JOIN  rddcountrieslist C ON SC.country=C.countrycode");
        ddlcountry.DataSource = DS;// Table [2] for Countries
        ddlcountry.DataTextField = "country";
        ddlcountry.DataValueField = "countrycode";
        ddlcountry.DataBind();
        // ddlcountry.Items.Insert(0, "--SELECT--");
    }

    protected void fillmonth()
    {
        string MNT = DateTime.Today.ToString("mm");

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();
        DataSet ds = Db.myGetDS("select distinct (MONTH (VisitDate)) as month,  LEFT(( DateName( Month,VisitDate)),3) as mname from DailySalesReports");
        if (ds.Tables.Count > 0)
        {
            ddlMonth.DataSource = ds;
            ddlMonth.DataTextField = "mname";
            ddlMonth.DataValueField = "month";
            ddlMonth.DataBind();
        }
    }
    protected void fillGetsalesPerson()
    {

        //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        //string LoggedInUserName = myGlobal.loggedInUser();

        //DataSet ds = Db.myGetDS("Exec DSR_GetsalesPerson '" + LoggedInUserName + "'");
        //if (ds.Tables.Count > 0)
        //{
        //    ddlemp.DataSource = ds;
        //    ddlemp.DataTextField = "SalesEmpName";
        //    ddlemp.DataValueField = "membershipuser";
        //    ddlemp.DataBind();
        //}

        //if (ds.Tables.Count > 1) // This is to add "ALL" option in ddl if loggedInUser is manager/top mana
        //{
        //    if (ds.Tables[1].Rows[0][0]!=null)
        //        ddlemp.Items.Insert(0, ds.Tables[1].Rows[0][0].ToString() );
        //}

    }

    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();

        DataSet ds = Db.myGetDS("select Id from tejSalespersonMap where Membershipuser='" + LoggedInUserName + "'");
        int managerId = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"].ToString());


        string items = string.Empty;
        foreach (ListItem i in ddlcountry.Items)
        {
            if (i.Selected == true)
            {
                if (string.IsNullOrEmpty(items))
                {
                    items =  i.Value ;
                }
                else
                {
                    items += "," + i.Value ;
                }
            }

        }

        ds = Db.myGetDS("Exec DSR_EmpUnderManager  '" + items + "','"+myGlobal.loggedInUser()+"' ");
        if (ds.Tables.Count > 0)
        {
            ddlemp.DataSource = ds;
            ddlemp.DataTextField = "alias";
            ddlemp.DataBind();
        }

        //ds = Db.myGetDS("select d.designation from tejSalespersonMap  s  Join designation_master d On d.id =s.designation Where membershipuser='" + LoggedInUserName + "'");
        //string designation = ds.Tables[0].Rows[0]["designation"].ToString();

        //if (designation == "Country Manager" || designation == "Top Management")
        //{
        //    ds = Db.myGetDS("Exec DSR_EmpUnderManager  '" + items + "' ");
        //    if (ds.Tables.Count > 0)
        //    {
        //        ddlemp.DataSource = ds;
        //        ddlemp.DataTextField = "alias";
        //        ddlemp.DataBind();
        //    }

        //    if (ds.Tables.Count > 1) // This is to add "ALL" option in ddl if loggedInUser is manager/top mana
        //    {
        //        if (ds.Tables[1].Rows[0][0] != null)
        //            ddlemp.Items.Insert(0, ds.Tables[1].Rows[0][0].ToString());
        //    }
        //}
        //else
        //{

        //    //not country manager


        //    ds = Db.myGetDS("Exec DSR_EmpUnderManager ('" + items + "' , '" + managerId + "' ");
        //    if (ds.Tables.Count > 0)
        //    {
        //        ddlemp.DataSource = ds;
        //        ddlemp.DataTextField = "alias";

        //        ddlemp.DataBind();
        //    }

        //    if (ds.Tables.Count > 1) // This is to add "ALL" option in ddl if loggedInUser is manager/top mana
        //    {
        //        if (ds.Tables[1].Rows[0][0] != null)
        //            ddlemp.Items.Insert(0, ds.Tables[1].Rows[0][0].ToString());
        //    }
        //}

    }

    protected void GVScoreCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string Scoree = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Score"));
        //    string score = Scoree.Replace("%", string.Empty);

        //    string FinalScore = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FinalScore%"));

        //    if (Convert.ToInt32(score) <= 75)
        //    {
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
        //    }
        //    if (Convert.ToInt32(score) > 75 && Convert.ToInt32(score) <= 99)
        //    {
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.Orange;
        //    }
        //    else if (Convert.ToInt32(score) == 100)
        //    {
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.LightGreen;
        //    }

        //    else if (Convert.ToInt32(score) > 100)
        //    {
        //        e.Row.Cells[4].BackColor = System.Drawing.Color.DarkGreen;
        //    }

        //    if (Convert.ToInt32(FinalScore) <= 75)
        //    {
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
        //    }
        //    if (Convert.ToInt32(FinalScore) > 75 && Convert.ToInt32(score) <= 99)
        //    {
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.Orange;
        //    }
        //    else if (Convert.ToInt32(FinalScore) == 100)
        //    {
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.LightGreen;
        //    }

        //    else if (Convert.ToInt32(FinalScore) > 100)
        //    {
        //        e.Row.Cells[6].BackColor = System.Drawing.Color.DarkGreen;
        //    }

        //}      
    }

    protected void GVScoreCard1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string TargetAchiev = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TargetAchieve"));
            string Targetpoint = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Targetpoint"));
            string TargetAchie = TargetAchiev.Replace('%', ' ');

            if (Targetpoint == "100%")
            {

                e.Row.Cells[11].BackColor = System.Drawing.Color.Black;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
            }


            if (Convert.ToInt32(TargetAchie) < 80)
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.White;

            }
            else
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Black;
                e.Row.Cells[13].BackColor = System.Drawing.Color.Black;
                e.Row.Cells[14].BackColor = System.Drawing.Color.Black;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[14].ForeColor = System.Drawing.Color.White;

            }



        }


    }



}
