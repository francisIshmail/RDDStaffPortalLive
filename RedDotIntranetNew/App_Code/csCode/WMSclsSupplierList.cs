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
/// Summary description for WMSclsSupplierList
/// </summary>
public class WMSclsSupplierList
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string supplier_name { get; set; }
    public string supplier_id { get; set; }
    public WMSclsSupplierList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<WMSclsSupplierList> SupplierList()
    {
        List<WMSclsSupplierList> Supplier = new List<WMSclsSupplierList>();
        String sql = "SELECT supplier_name,supplier_id FROM supplier;  ";
        DataTable dt = exeQuery.ExecuteTable(sql);

        foreach (DataRow row in dt.Rows)
        {
            WMSclsSupplierList tempsupplier = new WMSclsSupplierList();
            tempsupplier.supplier_name = row["supplier_name"].ToString();
            tempsupplier.supplier_id = row["supplier_id"].ToString();
            Supplier.Add(tempsupplier);

        }


        return Supplier;
    }
}