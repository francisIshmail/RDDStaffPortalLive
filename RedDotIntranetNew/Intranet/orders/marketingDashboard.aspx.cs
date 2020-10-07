using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_orders_marketingDashboard : System.Web.UI.Page
{
    string sql,filterExp;
    DataTable dtFundsInPipeline;
    DataTable dtAccruedApprovedAmt;
    DataTable dtTotalExpense;
    DataTable dtClaimAmt;
    DataTable dtUnClaimAmt;
    DataTable dtChartPmMarketing;
    DataTable dtChartLocationWise;
    DataTable dtChartActivityType;

    protected void Page_Load(object sender, EventArgs e)
    {
       //MaintainScrollPositionOnPostBack = true;
        if (!IsPostBack)
        {
            bindDdlYear();
            calculateFields();
            bindCharts();
        }
        
    }

    protected void bindDdlYear()
    {
        sql = "select distinct(planYear) from workFlowPlans";
        Db.LoadDDLsWithCon(ddlYear, sql, "planYear", "planYear", myGlobal.getIntranetDBConnectionString());
        if (ddlYear.Items.Count > 0)
        {
            ddlYear.Items.Insert(0, new ListItem("All", "0"));
        }
        else
            ddlYear.Items.Add(new ListItem("All", "0"));
    }

    protected void calculateFields()
    {
        if (ddlYear.SelectedItem.Text == "All")
        {
            filterExp = "";
        }
        else
        {
            filterExp ="planYear=" + ddlYear.SelectedItem.Text;
        }

        double fundsInPipeline = 0, accruedApprovedAmt = 0, totalExpense = 0, claimAmt = 0, fundsForMKT = 0, unclaimAmt=0,MBAccountAmt=0;

        //calculate funds in pipeline
        sql = "select WP.planYear,SUM(WP.VendorApprovedAmount) as 'FundsInPipeline' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId where fk_StatusId<=2 and fk_StatusId<>0 group by WP.PlanYear";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtFundsInPipeline = new DataTable();
        dtFundsInPipeline = Db.myGetDS(sql).Tables[0];
        if (dtFundsInPipeline.Rows.Count > 0)
        {
            object objFundsInPipeline = dtFundsInPipeline.Compute("SUM(FundsInPipeline)", "" + filterExp + "");
            if (objFundsInPipeline != DBNull.Value)
            {
                fundsInPipeline = Convert.ToDouble(objFundsInPipeline);
                lblFundsPipeline.Text = fundsInPipeline.ToString();
            }
            else
            {
                lblFundsPipeline.Text = "0";
            }
        }
        else
        {
            lblFundsPipeline.Text = "0";
        }


        //calculate accrued approved amount
        sql = "select WP.planYear,SUM(TA.ActivityVendorAmount) as 'ActivityVendorAmount' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex where PR.fk_StatusId>5 group by WP.planYear";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtAccruedApprovedAmt = new DataTable();
        dtAccruedApprovedAmt = Db.myGetDS(sql).Tables[0];
        if (dtAccruedApprovedAmt.Rows.Count > 0)
        {
            object objAccruedApprovedAmt = dtAccruedApprovedAmt.Compute("SUM(ActivityVendorAmount)", "" + filterExp + "");
            if (objAccruedApprovedAmt != DBNull.Value)
            {
                accruedApprovedAmt = Convert.ToDouble(objAccruedApprovedAmt);
                lblAccruedApprovedAmt.Text = accruedApprovedAmt.ToString();
            }
            else
            {
                lblAccruedApprovedAmt.Text = "0";
            }


        }
        else
        {
            lblAccruedApprovedAmt.Text = "0";
        }


        //calculate total expense
        sql = "select WP.planYear,SUM(TAD.Costed) as 'Costed' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex Join TblActivitiesDetails as TAD on TAD.fk_ActivitySno=TA.sno where TAD.mufFormFilledStatus='Yes' group by WP.planYear";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtTotalExpense = new DataTable();
        dtTotalExpense = Db.myGetDS(sql).Tables[0];
        if (dtTotalExpense.Rows.Count > 0)
        {
            object objTotalExpense = dtTotalExpense.Compute("SUM(Costed)", "" + filterExp + "");
            if (objTotalExpense != DBNull.Value)
            {
                totalExpense = Convert.ToDouble(objTotalExpense);
                lblExpenses.Text = totalExpense.ToString();
            }
            else
            {
                lblExpenses.Text = "0";
            }
        }
        else
        {
            lblExpenses.Text = "0";
        }


        //total claimed amount
        sql = "select WP.planYear,SUM(TAD.Costed) as 'Costed' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex Join TblActivitiesDetails as TAD on TAD.fk_ActivitySno=TA.sno where PR.fk_StatusId>10 and TAD.mufFormFilledStatus='Yes' group by WP.planYear";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtClaimAmt = new DataTable();
        dtClaimAmt = Db.myGetDS(sql).Tables[0];
        if (dtClaimAmt.Rows.Count > 0)
        {
            object objClaimAmt = dtClaimAmt.Compute("SUM(Costed)", "" + filterExp + "");
            if (objClaimAmt != DBNull.Value)
            {
                claimAmt = Convert.ToDouble(objClaimAmt);
                lblClaimAmount.Text = claimAmt.ToString();
            }
            else
            {
                lblClaimAmount.Text = "0";
            }
        }
        else
        {
            lblClaimAmount.Text = "0";
        }


        //total unclaimed amount
        sql = "select WP.planYear,SUM(TAD.Costed) as 'Costed' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex Join TblActivitiesDetails as TAD on TAD.fk_ActivitySno=TA.sno where PR.fk_StatusId>5 and TAD.mufFormFilledStatus='Yes' group by WP.planYear";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtUnClaimAmt = new DataTable();
        dtUnClaimAmt = Db.myGetDS(sql).Tables[0];
        if (dtUnClaimAmt.Rows.Count > 0)
        {
            object objUnClaimAmt = dtUnClaimAmt.Compute("SUM(Costed)", "" + filterExp + "");
            if (objUnClaimAmt != DBNull.Value)
            {
                unclaimAmt = Convert.ToDouble(objUnClaimAmt);
                lblUnclaimAmount.Text = (unclaimAmt - claimAmt).ToString();
            }
            else
            {
                lblUnclaimAmount.Text = "0";
            }
        }
        else
        {
            lblUnclaimAmount.Text = "0";
        }


        //funds available for MKT
        fundsForMKT = (accruedApprovedAmt/2)-totalExpense; 
        lblFundsAvailableForMKT.Text = Convert.ToString(fundsForMKT);

        //amount in MB account
        MBAccountAmt = (accruedApprovedAmt / 2);
        lblMBAccountAmt.Text = Convert.ToString(MBAccountAmt);

    }


    protected void bindCharts()
    {
        bindChartAccruedFunds();
        bindChartClaims();
        bindChartPMMarketing();
        bindChartLocationWise();
        bindChartActivityType();

    }

    protected void bindChartAccruedFunds()
    {
        DataTable dtChart = new DataTable();
        dtChart.Columns.Add("Field", typeof(string));
        dtChart.Columns.Add("Amount", typeof(double));

        DataRow drExpense = dtChart.NewRow();
        drExpense["Field"] = "Expenses";
        drExpense["Amount"] = Convert.ToDouble(lblExpenses.Text);
        dtChart.Rows.Add(drExpense);

        DataRow drMKTFunds = dtChart.NewRow();
        drMKTFunds["Field"] = "Funds for MKT";
        drMKTFunds["Amount"] = Convert.ToDouble(lblFundsAvailableForMKT.Text);
        dtChart.Rows.Add(drMKTFunds);

        DataRow drMBAccountAmt = dtChart.NewRow();
        drMBAccountAmt["Field"] = "MB Account Amount";
        drMBAccountAmt["Amount"] = Convert.ToDouble(lblMBAccountAmt.Text);
        dtChart.Rows.Add(drMBAccountAmt);

        if (Convert.ToDouble(lblExpenses.Text) != 0 && Convert.ToDouble(lblFundsAvailableForMKT.Text) != 0 && Convert.ToDouble(lblMBAccountAmt.Text) != 0)
        {
            DataView dv = dtChart.DefaultView;
            chartAccruedFunds.Series["Series1"].Points.DataBindXY(dv, "Field", dv, "Amount");
            lblMsgChartAccruedFunds.Text = "";
        }
        else
        {
            lblMsgChartAccruedFunds.Text = "No chart available for current statistics";
        }
    }

    protected void bindChartClaims()
    {
        DataTable dtChart = new DataTable();
        dtChart.Columns.Add("Field", typeof(string));
        dtChart.Columns.Add("Amount", typeof(double));

        DataRow drClaimAmt = dtChart.NewRow();
        drClaimAmt["Field"] = "Claimed Amount";
        drClaimAmt["Amount"] = Convert.ToDouble(lblClaimAmount.Text);
        dtChart.Rows.Add(drClaimAmt);

        DataRow drUnClaimAmt = dtChart.NewRow();
        drUnClaimAmt["Field"] = "Unclaimed Amount";
        drUnClaimAmt["Amount"] = Convert.ToDouble(lblUnclaimAmount.Text);
        dtChart.Rows.Add(drUnClaimAmt);

        if (Convert.ToDouble(lblClaimAmount.Text) != 0 || Convert.ToDouble(lblUnclaimAmount.Text) != 0)
        {
            DataView dv = dtChart.DefaultView;
            chartClaims.Series["Series1"].Points.DataBindXY(dv, "Field", dv, "Amount");
            lblMsgChartClaims.Text = "";
        }
        else
        {
            lblMsgChartClaims.Text = "No chart available for current statistics";
        }

    }

    protected void bindChartPMMarketing()
    {
        //fetch data in table
        sql = "select WP.planYear,TAD.ActivityDesc,SUM(TAD.Costed) as 'Costed' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex Join TblActivitiesDetails as TAD on TAD.fk_ActivitySno=TA.sno where PR.fk_StatusId>5 and TAD.mufFormFilledStatus='Yes' group by WP.planYear,TAD.ActivityDesc";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtChartPmMarketing = new DataTable();
        dtChartPmMarketing = Db.myGetDS(sql).Tables[0];

        //bind PM v/s MKT chart
        double PMAmt, MKTAmt;
        string whrClause;
        if (dtChartPmMarketing.Rows.Count > 0)
        {
            if (filterExp == "")
            {
                whrClause = "ActivityDesc='PM'";
            }
            else
            {
                whrClause = "ActivityDesc='PM' and "+ filterExp +"";
            }

            
            object obj = dtChartPmMarketing.Compute("SUM(Costed)", "" + whrClause + "");
            if (obj != DBNull.Value)
            {
                PMAmt =Convert.ToDouble(obj);
            }
            else
            {
                PMAmt = 0;
            }


            if (filterExp == "")
            {
                whrClause = "ActivityDesc='Marketing'";
            }
            else
            {
                whrClause = "ActivityDesc='Marketing' and " + filterExp + "";
            }

            obj = dtChartPmMarketing.Compute("SUM(Costed)", "" + whrClause + "");
            if (obj != DBNull.Value)
            {
                MKTAmt = Convert.ToDouble(obj);
            }
            else
            {
                MKTAmt = 0;
            }

        }
        else
        {
            PMAmt = 0;
            MKTAmt = 0;
        }

        DataTable dtChart = new DataTable();
        dtChart.Columns.Add("Field", typeof(string));
        dtChart.Columns.Add("Amount", typeof(double));

        DataRow drPMAmt = dtChart.NewRow();
        drPMAmt["Field"] = "PM Amount";
        drPMAmt["Amount"] = Convert.ToDouble(PMAmt);
        dtChart.Rows.Add(drPMAmt);

        DataRow drMKTAmt = dtChart.NewRow();
        drMKTAmt["Field"] = "Marketing Amount";
        drMKTAmt["Amount"] = Convert.ToDouble(MKTAmt);
        dtChart.Rows.Add(drMKTAmt);

        if (PMAmt != 0 && MKTAmt!= 0)
        {
            DataView dv = dtChart.DefaultView;
            chartPMMarketing.Series["Series1"].Points.DataBindXY(dv, "Field", dv, "Amount");
            lblMsgChartPMMarketing.Text = "";
        }
        else
        {
            lblMsgChartPMMarketing.Text = "No chart available for current statistics";
        }

    }

    protected void bindChartLocationWise()
    {
        //fetch data in table
        sql = "select WP.planYear,TAD.ActivityLocation,SUM(TAD.Costed) as 'Costed' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex Join TblActivitiesDetails as TAD on TAD.fk_ActivitySno=TA.sno where PR.fk_StatusId>5 and TAD.mufFormFilledStatus='Yes' group by WP.planYear,TAD.ActivityLocation";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtChartLocationWise = new DataTable();
        dtChartLocationWise = Db.myGetDS(sql).Tables[0];

        if (dtChartLocationWise.Rows.Count > 0)
        {
            DataView dv = dtChartLocationWise.DefaultView;
            if (filterExp != "")
            {
                dv.RowFilter = "" + filterExp + "";
            }

            chartLocation.Series["Series1"].Points.DataBindXY(dv, "ActivityLocation", dv, "Costed");
            lblMsgChartLocation.Text = "";
        }
        else
        {
            lblMsgChartLocation.Text = "No chart available for current statistics";
        }

    }


    protected void bindChartActivityType()
    {
        //fetch data in table
        sql = "select WP.planYear,TAD.ActivityType,SUM(TAD.Costed) as 'Costed' from workFlowPlans as WP Join processRequest as PR On WP.autoIndex=PR.refId Join TblActivities as TA on TA.fk_workflowPlansId=WP.autoIndex Join TblActivitiesDetails as TAD on TAD.fk_ActivitySno=TA.sno where PR.fk_StatusId>5 and TAD.mufFormFilledStatus='Yes' group by WP.planYear,TAD.ActivityType";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dtChartActivityType = new DataTable();
        dtChartActivityType = Db.myGetDS(sql).Tables[0];

        if (dtChartActivityType.Rows.Count > 0)
        {
            DataView dv = dtChartActivityType.DefaultView;
            if (filterExp != "")
            {
                dv.RowFilter = "" + filterExp + "";
            }

            chartActivityType.Series["Series1"].Points.DataBindXY(dv, "ActivityType", dv, "Costed");
            lblMsgChartActivityType.Text = "";
        }
        else
        {
            lblMsgChartActivityType.Text = "No chart available for current statistics";
        }

    }


    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        calculateFields();
        bindCharts();
        TabContainer1.ActiveTabIndex = 0;
    }
}


