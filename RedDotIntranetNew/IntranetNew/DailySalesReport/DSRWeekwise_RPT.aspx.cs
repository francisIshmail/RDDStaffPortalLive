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
using System.Web.Security;


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
            fillyear();
            fillmonth();
            //   fillGetsalesPerson();
            fillCountry();
        }
        // }
        //else
        //{
        //    Response.Redirect("Default.aspx?UserAccess=0&FormName=View Score Card");
        //}

    }



    protected void GVScoreCard_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //TableCell HeaderCell = new TableCell();


            //HeaderCell.Text = "";
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);




            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;TARGET&TTTTT&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);



            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;VISIT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;PHONE IN&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PHONE OUT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;EMAILS&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;

            //HeaderGridRow.Cells.Add(HeaderCell);



            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;WHATSAPP&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;NEW CUSTOMER&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;ACHIEVED %&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ACHIEVED %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;ACHIEVED %&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;ACHIEVED %&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;ACHIEVED %&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ACHIEVED %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //// HeaderCell.BackColor = Color.Green;
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.BorderColor = Color.White;
            //HeaderCell.BackColor = Color.White;
            //HeaderCell.ForeColor = Color.White;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //GVScoreCard.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }


    protected void btnexporttoex_Click(object sender, EventArgs e)
    {

        try
        {
            if (Session["DATA"] != null)
            {
                DataTable dt = (DataTable)Session["DATA"];
                string attachment = "attachment; filename=FunnelDeals_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Export : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }
  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
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

            DataSet ds = Db.myGetDS("exec DSR_GetReportWeekWiseByWEEK '" + month + "','" + ddlyear.SelectedItem.Text + "','" + cou + "', '" + empname + "'");

            if (ds.Tables.Count > 0)
            {
                GVScoreCard.DataSource = ds.Tables[0];
                GVScoreCard.DataBind();
                Session["DATA"] = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            // lblms.Text = "Error occured in fillgvSummary() " + ex.Message;
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

  
}
