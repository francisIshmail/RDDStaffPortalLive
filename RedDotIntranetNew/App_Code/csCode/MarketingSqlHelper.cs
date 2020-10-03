using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
 
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
/// <summary>
/// Summary description for WMSSqlHelper
/// </summary>
public class MarketingDB
{
    String connString = System.Configuration.ConfigurationManager.ConnectionStrings["MarketingBD"].ConnectionString;
    public MarketingDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable RecordSetToDataTable(ADODB.Recordset objRS)
    {
        System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();
        DataTable objDT = new DataTable();
        objDA.Fill(objDT, objRS);
        return objDT;

    }


    public DataTable ExecuteTable(String SQl)
    {
        SqlConnection conn = null;
       conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand(SQl, conn);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable dt = new DataTable();
        dt.Load(dr);


        return dt;
    }


   public Boolean executeDMl(String SQl)
   {
       SqlConnection conn = null;
      conn = new SqlConnection(connString);
       SqlCommand cmd = new SqlCommand(SQl, conn);
       conn.Open();
       int rowsAffected = cmd.ExecuteNonQuery();

       return true;
   }


}