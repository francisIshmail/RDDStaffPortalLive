using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Intranet_MasterFunnel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
          //  loadDownloadMenu();
        //}

        if (Session["sessionMacName"] == null)
        {
            Session["sessionMacName"] = "";
        }
        if (Session["sessionMacName"].ToString() != Session.SessionID.ToString())
        {
            if (myGlobal.loggedInUser() != "")
            {
                FormsAuthentication.SetAuthCookie(myGlobal.loggedInUser(), false);
                Session["sessionMacName"] = Session.SessionID.ToString();
            }
        }
    }

    private void loadDownloadMenu()
    {
        string txt, hrf, pth;
        pth = Server.MapPath("~/webReportsDownloadMenu.xml");
        DataSet dsxml = new DataSet();
        dsxml.ReadXml(pth);
        for (int w = 0; w < dsxml.Tables[0].Rows.Count; w++)
        {
            txt = dsxml.Tables[0].Rows[w]["txt"].ToString();
            hrf = dsxml.Tables[0].Rows[w]["lnk"].ToString();
         
            HtmlGenericControl spn = new HtmlGenericControl("span");
            spn.InnerText = txt;
        
            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", hrf);
            anchor.Controls.Add(spn);

            HtmlGenericControl li = new HtmlGenericControl("li");
            ulDownloadList.Controls.Add(li);
                
            li.Controls.Add(anchor);
        }
    }

    protected void lbhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/home.aspx");
    }


    protected void lbPruchaseOrder_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011");
    }

    protected void lbReleaseOrders_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10012");
    }

    protected void lbMarketingPlans_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/viewOrdersMKT.aspx?wfTypeId=10031");
    }


    protected void lbWMS_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/WMS/Main.aspx");
    }
    protected void lbCreditReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/Reporting/creditReportBase.aspx");
    }

    protected void lbCustomerStatement_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/Reporting/statementBase.aspx");
    }

    protected void lbSellOutReporte_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/Reporting/sellOut.aspx");
    }

    protected void lbCreditLimit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/EVO/CreditLimitUpdater.aspx");
    }

    protected void LbAddOnceStock_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/EVO/AddOnce.aspx");
    }

    protected void LbProjectCode_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Intranet/EVO/ProjectCodeChanger.aspx");
    }
    protected void lbMailShot_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Intranet/Marketing/Home.aspx");
    }
    protected void LbnewMailShot_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/Marketing/Sendmail.aspx");
    }
    protected void lbtallyExport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/tallyExport.aspx");
    }
    protected void lbweeklygp_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=weekly");
    }
    protected void lbStockSheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=automatedStockSheet");
    }
    protected void lbSalesFunnel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=salesFunnel");
    }
    protected void lbStockAge_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=stockAge");
    }
    protected void lbDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=dashboard");
    }
    protected void lbPMDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=pmDashboard");
    }
    protected void lbCMDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/webReporting/DownloadReps.aspx?repType=cmDashboard");
    }
    protected void lbReportAdmin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/subAdmin/ReportAdmin.aspx");
    }

}
