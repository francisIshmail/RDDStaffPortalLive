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
/// Summary description for WMSclsReserveStock
/// </summary>
public class WMSclsReserveStock
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public WMSclsReserveStock()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public DataTable Gvreservestockorder(string search)
    {
        String sql = "";
        if (search == "")
            sql = "SELECT deliveryOrder.status, customer.customer_name, freight_forwarder.freight_forwarder_name, deliveryOrder.do_id, deliveryOrder.customer_id, deliveryOrder.creation_date, deliveryOrder.effective_date, deliveryOrder.last_updated_date, deliveryOrder.last_updated_by FROM freight_forwarder RIGHT JOIN (customer RIGHT JOIN deliveryOrder ON customer.customer_id = deliveryOrder.customer_id) ON freight_forwarder.freight_forwarder_id = deliveryOrder.freight_forwarder WHERE (((deliveryOrder.status)='RESERVED' Or (deliveryOrder.status)='EXPIRED'))  order by deliveryOrder.do_id desc ;";
        else
            sql = "SELECT deliveryOrder.status, customer.customer_name, freight_forwarder.freight_forwarder_name, deliveryOrder.do_id, deliveryOrder.customer_id, deliveryOrder.creation_date, deliveryOrder.effective_date, deliveryOrder.last_updated_date, deliveryOrder.last_updated_by FROM freight_forwarder RIGHT JOIN (customer RIGHT JOIN deliveryOrder ON customer.customer_id = deliveryOrder.customer_id) ON freight_forwarder.freight_forwarder_id = deliveryOrder.freight_forwarder WHERE (((deliveryOrder.status)='RESERVED' Or (deliveryOrder.status)='EXPIRED')) and "+ search +"  order by deliveryOrder.do_id desc ;";
        return exeQuery.ExecuteTable(sql);
    }




    public DataTable reservesorder(int doID)
    {
        String sql = "SELECT * from ReserveOrder where do_id =" + doID;              
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable reservesordergrid(int doID, string username)
    {
        String sql = "select do_detail_id, stock_id,[part_number],location_description,description,[warehouse_id],[quantity],[boe_id] , 0 as flag  from ReserveOrder where  do_id =" + doID + " and do_detail_id <> null  union  select tempID as do_detail_id, Stockid as stock_id ,[part_number],location_description,description,[warehouse_id],[quantity],[boe_id]  , 1 as flag from  [ReserveOrderforGrid] where username ='" + username + "'  ;";
        return exeQuery.ExecuteTable(sql);
    }


    public DataTable ReserveStockReport()
    {
        String sql = "select * from ReserverStokReport";
          return exeQuery.ExecuteTable(sql);
    }


    public DataTable newreserve()
    {
        String Sql = "select max(do_id) as doi_id , getdate() as creattiondate,DATEADD(month, 3 , getdate()) as expriyDate  from deliveryOrder";
                return exeQuery.ExecuteTable(Sql);

    }
    public DataTable custname()
    {
        String sql = "SELECT customer.customer_name, customer.customer_id FROM customer ORDER BY customer.customer_name; ";
       return exeQuery.ExecuteTable(sql);

    }

    public DataTable partnumber()
    {
        String sql = "SELECT DISTINCT stockItem.part_number, stockItem.stock_id FROM stockItem RIGHT JOIN warehouseStock ON stockItem.stock_id=warehouseStock.stock_id WHERE (((warehouseStock.quantity)>0)); ";
       return exeQuery.ExecuteTable(sql);
    }


    public DataTable checkStock(int Stockid)
    {
        String sql = "select * from ReserveStock where stock_id =" + Stockid;
      return exeQuery.ExecuteTable(sql);
    }
    public DataTable Bindgrid(String username )
    {
        String sql = "select * from ReserveOrderforGrid where username ='" + username + "';";
        return exeQuery.ExecuteTable(sql);
    }

    public String getpartnumber(int stockid)
    {

        String sql = "SELECT DISTINCT stockItem.part_number, stockItem.stock_id FROM stockItem RIGHT JOIN warehouseStock ON stockItem.stock_id=warehouseStock.stock_id WHERE warehouseStock.quantity>0 and  stockItem.stock_id = " + stockid + "; ";
        DataTable dt = exeQuery.ExecuteTable(sql);
        String partnumber = dt.Rows[0]["part_number"].ToString();
        return partnumber;
    }
    public Boolean inserttempreserve (String username,String part_number,int Stockid,int warehouse_id,int location_id, int quantity, String boe_id,String Itemprice,int coo)
    {
        String sql = "insert into  tempReserve (username,part_number,Stockid,warehouse_id,location_id,quantity,boe_id,item_price,COO) values ('"+ username + "','"+ part_number + "' , " + Stockid + ", " + warehouse_id + ", " + location_id + " ," + quantity + ",'" + boe_id + "' ,'" + Itemprice + "' ," + coo + ") ";
        return exeQuery.executeDMl(sql);

    }

    public Boolean DeleteTempReserve(String username)
    {
        String sql = "Delete from tempReserve where username='" + username + "'";
         return exeQuery.executeDMl(sql);

    }

    public Boolean updateReserveORAllocate(String stockid,  int newqty)
    {
        String sql = "update deliveryOrderDetails set quantity = " + newqty + "  where stock_id =" + stockid;
        exeQuery.executeDMl(sql);
        return true;
    }



    public Boolean DeleteRevserve(String DoDEtailID, int flag)
    {
        String sql = "";
        if (flag == 1)
            sql = "Delete from tempReserve where tempID='" + DoDEtailID + "'";
        else
            sql = "delete from deliveryOrderDetails where do_detail_id =" + DoDEtailID;

        return exeQuery.executeDMl(sql);

    }
    public Boolean updateOldReserve(string username, string doid)
    {

        /// I am only focus  on the new reserve details when updating an old reserve. I need to ask if the user can modify ... BUt i think in that case 
        /// he will need to simple select that partnumber again. then select a new quantity.in case he want more .
        /// Now i guess the question will be what happen if he want to reduce a the exiting quantity? or delete that reserve ? it is possible  to do ?
       
        String Sql2 = "select * from tempReserve where username='" + username + "';";
        DataTable dt1 = exeQuery.ExecuteTable(Sql2);

        foreach (DataRow row in dt1.Rows)
        {
            String sql3 = "insert into deliveryOrderDetails (do_id,stock_id,quantity,item_price,warehouse_id,location_id,boe_id,COO) values (" + doid + ", " + row["Stockid"].ToString() + ", " + row["quantity"].ToString() + " , '" + row["item_price"].ToString() + "' , " + row["warehouse_id"].ToString() + " , " + row["location_id"].ToString() + " , '" + row["boe_id"].ToString() + "' , " + row["COO"].ToString() + ")";
            exeQuery.executeDMl(sql3);
        }


        String sql4 = "Delete from tempReserve where username='" + username + "';";
        return exeQuery.executeDMl(sql4);

    }

    public Boolean SaveReserve(string username ,int customer_id, String creation_date, String effective_date, String notes, String reserveorder)
    {


        String sql = "  insert into deliveryOrder (status,  customer_id, creation_date,  effective_date,  notes, release_to , release_no) values ( 'RESERVED', " + customer_id + ",'" + creation_date + "', '" + effective_date + "','" + notes + "' ," + customer_id + " ,'" + reserveorder + "') ";
        exeQuery.executeDMl(sql);
        String sql1 = " select max(do_id) as do_id from deliveryOrder";
        DataTable dt = exeQuery.ExecuteTable(sql1);
        int do_id = int.Parse(dt.Rows[0]["do_id"].ToString());
        String Sql2 = "select * from tempReserve where username='" + username + "';";
        DataTable dt1 = exeQuery.ExecuteTable(Sql2);

        foreach (DataRow row in dt1.Rows)
        {
            String sql3 = "insert into deliveryOrderDetails (do_id,stock_id,quantity,item_price,warehouse_id,location_id,boe_id,COO) values (" + do_id + ", " + row["Stockid"].ToString() + ", " + row["quantity"].ToString() + " , '" + row["item_price"].ToString() + "' , " + row["warehouse_id"].ToString() + " , " + row["location_id"].ToString() + " , '" + row["boe_id"].ToString() + "' , " + row["COO"].ToString() + ")";
            exeQuery.executeDMl(sql3);
        }


        String sql4 = "Delete from tempReserve where username='" + username + "';";
        return exeQuery.executeDMl(sql4);
    }

}