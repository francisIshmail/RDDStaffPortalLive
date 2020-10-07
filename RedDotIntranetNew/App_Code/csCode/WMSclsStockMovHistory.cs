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
/// <summary>
/// Summary description for WMSclsStockMovHistory
/// </summary>
public class WMSclsStockMovHistory
{
	public WMSclsStockMovHistory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GVStockMoveHIst()
    {
        String sql = "SELECT stockMovement.*, stockItem.part_number, stockItem.description, warehouse.description AS description_warehouse ,location_description FROM stockItem INNER JOIN (warehouse INNER JOIN stockMovement ON warehouse.warehouse_id=stockMovement.warehouse_id) ON stockItem.stock_id=stockMovement.stock_id inner join dbo.location on location.location_id =stockMovement.location_id ORDER BY transaction_date DESC; ";

        WMSSqlHelper exeQuery = new WMSSqlHelper();
        return exeQuery.ExecuteTable(sql);
    }
}