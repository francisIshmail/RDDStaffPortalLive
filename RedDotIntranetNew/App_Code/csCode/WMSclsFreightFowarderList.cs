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
/// Summary description for WMSclsFreightFowarderList
/// </summary>
public class WMSclsFreightFowarderList
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string freight_forwarder_name { get; set; }
    public string freight_forwarder_id { get; set; }

	public WMSclsFreightFowarderList()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<WMSclsFreightFowarderList> FreightFowarderList()
    {
        List<WMSclsFreightFowarderList> FreightFowarder = new List<WMSclsFreightFowarderList>();
        String sql = "SELECT freight_forwarder.freight_forwarder_name, freight_forwarder.freight_forwarder_id FROM freight_forwarder ORDER BY freight_forwarder.freight_forwarder_name; ";

        DataTable dt = exeQuery.ExecuteTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            WMSclsFreightFowarderList tempFreightFowarder = new WMSclsFreightFowarderList();
            tempFreightFowarder.freight_forwarder_name = row["freight_forwarder_name"].ToString();
            tempFreightFowarder.freight_forwarder_id = row["freight_forwarder_id"].ToString();
            FreightFowarder.Add(tempFreightFowarder);
        }
        return FreightFowarder;
    }
}