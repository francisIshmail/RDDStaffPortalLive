using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
/// <summary>
/// Summary description for WMSclsBoes
/// </summary>
public class MailShot
{
    public string BU { get; set; }
    public string Suject { get; set; }
    public int Response { get; set; }
    public int TargetSale { get; set; }
    public int SaleArchived { get; set; }
    public int countryID { get; set; }
    public int SenderID { get; set; }
    public static SqlConnection thisDBConnection;     // Value is set in Constructor
    //String connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
    MarketingDB exeQuery = new MarketingDB();
    public MailShot()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Boolean  addmailShot(String BU, String Suject, String TargetSale, String countryID, String SenderID)
    {
        String sql = "insert into tblMailShot (BU,Suject,Response,TargetSale,SaleArchived,countryID,SenderID) values ( " + BU + " , '" + Suject + "' , 0  , " + TargetSale + " , 0 , " + countryID + " , " + SenderID + ")";
        return exeQuery.executeDMl(sql);
    }

    public Boolean updatemailShot(String SaleArchived, String Response, String MailShotID)
    {
        String sql = "update dbo.tblMailShot set SaleArchived  =  " + SaleArchived + " , Response  =   " + Response + " where MailShotID =" + MailShotID;
        return exeQuery.executeDMl(sql);
    }




    public DataTable getEmail()
    {
        string sql = "select * from tblEmail";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable gethistory()
    {
        string sql = "select * from gethistory order by modifieddate desc";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable gethistorybyID(String mailshotID)
    {
        string sql = "select * from gethistory where mailshotid =" + mailshotID;
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable getbu()
    {
        string sql = "select * from tblbu order by bu asc";
        return exeQuery.ExecuteTable(sql);
    }


    
}