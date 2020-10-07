//using ICSharpCode;
using Microsoft.CSharp;
using System.IO;
using System.Text;
using System.Deployment;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System;


public partial class _orderImportPage : System.Web.UI.Page
{
    string msg, filePath, importOrderType, importOrderUser;
    int importOrderID,processTypeId;

    protected void Page_Load(object sender, EventArgs e)
    {
        importOrderUser = myGlobal.loggedInUser();
        try
        {
            if (Request.QueryString["importOrderType"] != null)
            {
                importOrderType = Request.QueryString["importOrderType"].ToString();
            }
            if (Request.QueryString["importOrderID"] != null)
            {
                importOrderID =Convert.ToInt32(Request.QueryString["importOrderID"]);
            }

            if (importOrderType.ToUpper() == "PO")
            {
                btnImportPO.Visible = true;
                btnImportRO.Visible = false;
                if(importOrderID==0)
                  lblImportTitle.Text = "Import New Purchase Order from Excel File";
                else
                  lblImportTitle.Text = "ReImport Purchase Order from Excel File";
            }
            else
            {
                btnImportPO.Visible = false;
                btnImportRO.Visible = true;
                if (importOrderID == 0)
                    lblImportTitle.Text = "Import New Release Order from Excel File";
                else
                    lblImportTitle.Text = "ReImport Release Order from Excel File";
            }
        }
        catch(Exception exp)
        {
            lblImportTitle.Text = "Error ! " + exp.Message;
        }
    }

    private void closeMe()
    {
        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "sCancel", "window.opener.location.href = window.opener.location.href;self.close()", true);
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "sCancel", "window.opener.location.href = window.opener.location.href;", true);
    }

    protected void btnImportRO_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
                string pthPhysical = MapPath("~/excelFileUpload/");
                filePath = pthPhysical + "tmpExcel.xlsx";

                if (System.IO.Directory.Exists(pthPhysical))
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pthPhysical);
                    System.IO.FileInfo[] rgFiles = di.GetFiles("*.*");
                    foreach (System.IO.FileInfo fi in rgFiles)
                    {
                        if (fi.Name == "tmpExcel.xlsx")
                        {
                            fi.Delete();
                        }
                    }
                }
                
            FileUpload1.SaveAs(pthPhysical + "tmpExcel.xlsx");
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            Excel.IExcelDataReader excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream);
            if (excelReader.ResultsCount < 1)
            {
                lbImportlMsg.Text = "Invalid ! Excel Version. Can't process ";
                return;
            }
            DataSet result1 = excelReader.AsDataSet();

            //send order id as 0 for new else editable id for edit
            //msg = importOrder.writeROToDbFromExcel(result1, importOrderID ,System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString.ToString());
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString.ToString();
            processTypeId = Db.myExecuteScalar("select processId from process_def where upper(processAbbr)='" + importOrderType + "'");

            msg = importOrder.writeROToDbFromExcel(result1, importOrderID, importOrderUser,processTypeId, System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString.ToString());
            lbImportlMsg.Text = msg;
            closeMe();
        }
        else
            lbImportlMsg.Text = "Invalid Attempt ! Please select an Excel file ";
    }


    protected void btnImportPO_Click(object sender, EventArgs e)
    {
        
        if (FileUpload1.HasFile) // Test result.
        { 
                string pthPhysical = MapPath("~/excelFileUpload/");
                filePath = pthPhysical + "tmpExcel.xlsx";
                
                if (System.IO.Directory.Exists(pthPhysical))
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(pthPhysical);
                    System.IO.FileInfo[] rgFiles = di.GetFiles("*.*");
                    foreach (System.IO.FileInfo fi in rgFiles)
                    {
                        if (fi.Name == "tmpExcel.xlsx")
                        {
                            fi.Delete();
                        }
                    }
                }
                
            FileUpload1.SaveAs(pthPhysical + "tmpExcel.xlsx");
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            Excel.IExcelDataReader excelReader = Excel.ExcelReaderFactory.CreateOpenXmlReader(stream);
            if (excelReader.ResultsCount < 1)
            {
                  lbImportlMsg.Text ="Invalid ! Excel Version. Can't process ";
                return;
            }
            DataSet result1 = excelReader.AsDataSet();

            //send order id as 0 for new else editable id for edit

            //msg = importOrder.writePOToDbFromExcel(result1, importOrderID, System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString.ToString());
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString.ToString();
            processTypeId = Db.myExecuteScalar("select processId from process_def where upper(processAbbr)='" + importOrderType + "'");
            msg = importOrder.writePOToDbFromExcel(result1, importOrderID, importOrderUser,processTypeId, System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString.ToString());

            lbImportlMsg.Text = msg;
            closeMe();
        }
        else
            lbImportlMsg.Text = "Invalid Attempt ! Please select an Excel file ";
    }
    
}