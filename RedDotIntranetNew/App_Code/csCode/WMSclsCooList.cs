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
/// Summary description for WMSclsCoo
/// </summary>
public class WMSclsCooList
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string country_name { get; set; }
    public string country_id { get; set; }

    public WMSclsCooList()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<WMSclsCooList> countryList()
    {
        List<WMSclsCooList> country = new List<WMSclsCooList>();
       
        String sql = " SELECT country.country_name, country.country_id FROM country; ";
        DataTable dt = exeQuery.ExecuteTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            WMSclsCooList tempCountry = new WMSclsCooList();
             tempCountry.country_name = row["country_name"].ToString();
            tempCountry.country_id = row["country_id"].ToString();
            country.Add(tempCountry);
        }
        return country;
    }
}