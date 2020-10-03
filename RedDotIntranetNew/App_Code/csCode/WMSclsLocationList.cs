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
/// Summary description for LocationList
/// </summary>
public class WMSclsLocationList
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string location_description { get; set; }
    public string location_id { get; set; }
    public WMSclsLocationList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<WMSclsLocationList> locationList()
      {
        List<WMSclsLocationList> location = new List<WMSclsLocationList>();
       String sql = "SELECT location.location_description, location.location_id FROM location;  ";
        DataTable dt = exeQuery.ExecuteTable(sql);

       foreach (DataRow row in dt.Rows)
       {
           WMSclsLocationList tempLocation = new WMSclsLocationList();
           tempLocation.location_description = row["location_description"].ToString();
           tempLocation.location_id = row["location_id"].ToString();
           location.Add(tempLocation);
        
        }


       return location;
    }

}