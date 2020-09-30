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
/// Summary description for WMSclsStocklist
/// </summary>
public class WMSclsStocklist
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string part_number { get; set; }
    public string stock_id { get; set; }

	public WMSclsStocklist()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<WMSclsStocklist> PartnumberList()
    {
        List<WMSclsStocklist> Partnumber = new List<WMSclsStocklist>();
        
     
        String sql = " SELECT stockItem.part_number, stockItem.stock_id FROM stockItem;";
        DataTable dt = exeQuery.ExecuteTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            WMSclsStocklist tempPartnummber = new WMSclsStocklist();
            tempPartnummber.part_number = row["part_number"].ToString();
            tempPartnummber.stock_id = row["stock_id"].ToString();
            Partnumber.Add(tempPartnummber);
        }
        return Partnumber;
    }
}