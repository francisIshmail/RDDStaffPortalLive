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
public class WMSclsBoes
{
	public WMSclsBoes()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DataTable Gvboes( string search)

    {
        String sql = "";
        if (search =="")
            sql = "SELECT billOfEntry.*, supplier.supplier_name FROM supplier INNER JOIN billOfEntry ON supplier.supplier_id=billOfEntry.supplier_id order by   prealert_id  ";
        else 
         sql = "SELECT billOfEntry.*, supplier.supplier_name FROM supplier INNER JOIN billOfEntry ON supplier.supplier_id=billOfEntry.supplier_id where  "+ search +" order by   prealert_id  ";
        WMSSqlHelper exeQuery = new WMSSqlHelper();
        return exeQuery.ExecuteTable(sql);
    }
}