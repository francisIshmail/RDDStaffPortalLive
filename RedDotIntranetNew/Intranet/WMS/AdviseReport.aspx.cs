using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Intranet_WMS_AdviseReportt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack )
            Page.Title = "Advise Report";
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        List<AdviceRpt> adviceReport = new List<AdviceRpt>();
        AdviceRpt tempAdvice = new AdviceRpt();

        tempAdvice.marksNumber = txtMarksNumber.Text;
        tempAdvice.Qty = txtQty.Text;
        tempAdvice.WeightKgs = txtWeightKgs.Text;
        tempAdvice.VolumeCbm = txtVolumeCbm.Text;
        tempAdvice.Description = txtDescription.Text;
        tempAdvice.ExitPoint = txtExitPoint.Text;
        tempAdvice.Destination = txtDestination.Text;
        tempAdvice.Amount = txtvalue.Text;
        if (chkImport.Checked == true)
            tempAdvice.import = "YES";
        if (chkTemp.Checked == true)
            tempAdvice.temporary = "YES";
        if (chkExport.Checked == true)
            tempAdvice.export = "YES";
        if (chkReExport.Checked == true)
            tempAdvice.reExport = "YES";
        if (ChkOther.Checked == true)
            tempAdvice.other = "YES";
        if (Chkalcohol.Checked == true)
            tempAdvice.alcohol = "YES";
        if (Chkftt.Checked == true)
            tempAdvice.ftt = "YES";
        if (chkBankG.Checked == true)
            tempAdvice.bankG = "YES";
        if (chkCreditAC.Checked == true)
            tempAdvice.creditAC = "YES";
        if (chkDeposit.Checked == true)
            tempAdvice.deposit = "YES";
        if (chkCDTBank.Checked == true)
            tempAdvice.cdtBank = "YES";
        if (chkCDRCash.Checked == true)
            tempAdvice.cdrCash = "YES";
        if (chkFZTranfer.Checked == true)
            tempAdvice.FZTranfer = "YES";
        if (chkStanG.Checked == true)
            tempAdvice.stanG = "YES";
        
        adviceReport.Add(tempAdvice);

        Session[RunningCache.AdviceRpt] = adviceReport;

        Response.Redirect("Crystal_DeliveryAdvice.aspx?doid=" + Request.QueryString["doid"]);
    }
}