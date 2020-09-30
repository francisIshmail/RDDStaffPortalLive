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
/// Summary description for WMScls
/// </summary>
public class WMScls
{
   
	public WMScls()
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





    DataTable ExecuteTable(String SQl)
    {
        SqlConnection conn = null;
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["WMSDXBStockDB"].ConnectionString;
        conn = new SqlConnection(connString);
        // String sql = "SELECT prealert.*, supplier.supplier_name, freight_forwarder.freight_forwarder_name FROM supplier RIGHT JOIN (freight_forwarder RIGHT JOIN prealert ON freight_forwarder.freight_forwarder_id=prealert.freight_forwarder) ON supplier.supplier_id=prealert.supplier_id;";
        SqlCommand cmd = new SqlCommand(SQl, conn);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable dt = new DataTable();
        dt.Load(dr);


        return dt;
    }




/*
    public DataTable ExecuteTable(String SQl)
    {
        ADODB.Connection cn = new ADODB.Connection();
        cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;data source=" + System.Web.HttpContext.Current.Server.MapPath("DXBStockDB.accdb");

        cn.Open();

        ADODB.Recordset rs = new ADODB.Recordset();
        rs.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
        rs.CursorType = ADODB.CursorTypeEnum.adOpenStatic;
        rs.LockType = ADODB.LockTypeEnum.adLockBatchOptimistic;
        rs.Open(SQl, cn);
        rs.ActiveConnection = null;
        cn.Close();

        DataTable dt = null;
        dt = RecordSetToDataTable(rs);
        return dt;
    }
    */



     public  DataTable  dtusername()
    {
        String sql = "SELECT user_name FROM users";

        return ExecuteTable(sql);
    }

     public DataTable dtlogin(String username, String password)
     {
         String sql = "SELECT user_name, password FROM users where user_name='" + username + "' and password='" + password + "' ";

         return ExecuteTable(sql);
     }



}