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
/// Summary description for WMSclsWarehouseList
/// </summary>
public class WMSclsWarehouseList
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public string description { get; set; }
    public string warehouse_id { get; set; }

	public WMSclsWarehouseList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<WMSclsWarehouseList> WarehouseList()
    {
        List<WMSclsWarehouseList> Warehouse = new List<WMSclsWarehouseList>();
        
        String sql = " SELECT description, warehouse_id FROM warehouse;  ";
        DataTable dt = exeQuery.ExecuteTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            WMSclsWarehouseList tempWarehouse = new WMSclsWarehouseList();
           tempWarehouse.description = row["description"].ToString();
            tempWarehouse.warehouse_id = row["warehouse_id"].ToString();
            Warehouse.Add(tempWarehouse);
        }
        return Warehouse;
    }
}