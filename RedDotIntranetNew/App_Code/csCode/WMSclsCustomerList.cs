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
/// Summary description for WMSclsCustomerList
/// </summary>
public class WMSclsCustomerList
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string customer_name { get; set; }
    public string customer_id { get; set; }
	public WMSclsCustomerList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<WMSclsCustomerList> customerList()
    {
        List<WMSclsCustomerList> customer = new List<WMSclsCustomerList>();
        String sql = "SELECT customer_name, customer_id FROM customer;  ";
        DataTable dt = exeQuery.ExecuteTable(sql);

        foreach (DataRow row in dt.Rows)
        {
            WMSclsCustomerList tempcustomer = new WMSclsCustomerList();
            tempcustomer.customer_name = row["customer_name"].ToString();
            tempcustomer.customer_id = row["customer_id"].ToString();
            customer.Add(tempcustomer);

        }


        return customer;
    }
}