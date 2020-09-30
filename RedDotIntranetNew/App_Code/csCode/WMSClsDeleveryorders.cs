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
/// Summary description for WMSClsDeleveryorders
/// </summary>
public class WMSClsDeleveryorders
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    public WMSClsDeleveryorders()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GVDolist(string search)
    {
        String sql = "";
        if (search == "")
            sql = "SELECT deliveryOrder.*, customer.customer_name, freight_forwarder.freight_forwarder_name FROM freight_forwarder RIGHT JOIN (customer RIGHT JOIN deliveryOrder ON customer.customer_id=deliveryOrder.customer_id) ON freight_forwarder.freight_forwarder_id=deliveryOrder.freight_forwarder WHERE (((deliveryOrder.status)<>'RESERVED')) ORDER BY deliveryOrder.do_id DESC;";
        else
            sql = "SELECT deliveryOrder.*, customer.customer_name, freight_forwarder.freight_forwarder_name FROM freight_forwarder RIGHT JOIN (customer RIGHT JOIN deliveryOrder ON customer.customer_id=deliveryOrder.customer_id) ON freight_forwarder.freight_forwarder_id=deliveryOrder.freight_forwarder WHERE (((deliveryOrder.status)<>'RESERVED')) and   " + search + " ORDER BY deliveryOrder.do_id DESC;";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable bindData(String doId)
    {
        String sql = " SELECT distinct * from deliveryNote where do_id= " + doId;
        return exeQuery.ExecuteTable(sql);
    }


    public DataTable BindUpdatingData(String DoDetailID)
    {
        String sql = " SELECT distinct * from deliveryNote where do_detail_id= " + DoDetailID;
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable bindpickTicketReport(int doId)
    {
        String sql = " SELECT distinct * from pickTicket where do_id= " + doId;
        return exeQuery.ExecuteTable(sql);
    }
    public DataTable bindDeleveryNoteReport(int doId)
    {
        String sql = " SELECT distinct * from deliveryNote where do_id= " + doId;
        return exeQuery.ExecuteTable(sql);
    }


    public DataTable bindDeliveryAdviceReport(int doId)
    {
        String sql = " SELECT distinct * from deliveryAdavice where do_id= " + doId;
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable bindCustomerInvoiceReport(int doId)
    {
        String sql = " SELECT distinct * from customerInvoice where do_id= " + doId;
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable bindInvoiceReport(int doId)
    {
        String sql = " SELECT distinct * from customerInvoice where do_id= " + doId;
        return exeQuery.ExecuteTable(sql);
    }

    public Boolean Confirmation(string stockid, string boeid, string warehoueid, string locationid, string qty, string Price, string releaseno, string doid, string username)
    {

        int newqty = -int.Parse(qty);

        String sql = "select * from WarehouseStock where warehouse_id=" + warehoueid + " AND stock_id=" + stockid;
        DataTable dt = exeQuery.ExecuteTable(sql);
        if (dt.Rows.Count > 0)
        {
            int oldqty = int.Parse(dt.Rows[0]["quantity"].ToString());
            int updatedQty = oldqty + newqty;
            String sql1 = "update WarehouseStock set quantity =" + updatedQty + " where  warehouse_id=" + warehoueid + " AND stock_id=" + stockid;
            exeQuery.executeDMl(sql1);
        }

        string sql2 = "select * from WarehouseStockBOELocation where warehouse_id =" + warehoueid + "and stock_id =" + stockid + " and boe_id =" + boeid + " and location_id=" + locationid;
        DataTable dt2 = exeQuery.ExecuteTable(sql2);
        if (dt2.Rows.Count > 0)
        {
            int oldqty = int.Parse(dt2.Rows[0]["quantity"].ToString());
            int updatedQty = oldqty + newqty;
            String sql1 = "update WarehouseStockBOELocation set quantity =" + updatedQty + " where warehouse_id =" + warehoueid + "and stock_id =" + stockid + " and boe_id =" + boeid + " and location_id=" + locationid;
            exeQuery.executeDMl(sql1);
        }

        String sql3 = "insert into StockMovement ( Type, stock_id,boe_id,warehouse_id,location_id,quantity,price,transaction_date,transaction_reference,username) values('DO', " + stockid + " ,'" + boeid + "', " + warehoueid + "," + locationid + " ," + qty + "," + Price + ", getdate(), 'RO#: " + releaseno + " DO#:  " + doid + "','" + username + "' );";
        exeQuery.executeDMl(sql3);
        return true;
    }

    public DataTable bindcustomer()
    {
        String sql = "SELECT customer.customer_name, customer.customer_id FROM customer ORDER BY customer.customer_name; ";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable bindFreightForwarder()
    {
        String sql = "SELECT freight_forwarder.freight_forwarder_name, freight_forwarder.freight_forwarder_id FROM freight_forwarder ORDER BY freight_forwarder.freight_forwarder_name; ";
        return exeQuery.ExecuteTable(sql);
    }

    public String createNewDo(String status, String customer_id, String effective_date, String shipping_method, String invoice_number, String last_updated_by, String driver, String vehicle, String container, String notes, String release_no, String release_order, String invoice, String freightForwarder, String shippingReference, String releaseTo)
    {

        String sql = "insert into deliveryOrder  (status , customer_id ,creation_date,effective_date, shipping_method,invoice_number,last_updated_date,last_updated_by,driver,vehicle,container,notes,release_no,release_order,invoice,lastUpdated ,freight_forwarder , shipping_reference,  release_to) values ( '" + status + "' ," + customer_id + " , '" + DateTime.Now + "', '" + effective_date + "', " + shipping_method + " , '" + invoice_number + "','" + DateTime.Now + "','" + last_updated_by + "', '" + driver + "','" + vehicle + "','" + container + "','" + notes + "','" + release_no + "','" + release_order + "','" + invoice + "','" + DateTime.Now + "' ,'" + freightForwarder + "','" + shippingReference + "'," + releaseTo + ");";

        exeQuery.executeDMl(sql);
        return lastDo();
    }

    public String lastDo()
    {
        String sql1 = "select max(do_id) as do_id from  deliveryOrder";
        DataTable dt = exeQuery.ExecuteTable(sql1);
        return dt.Rows[0]["do_id"].ToString();
    }
    public Boolean NewdoDetails(string tempid,String doid, String stockid, String quantity, String itemprice, String itemvolume, String grossweight, String totalprice, String totalgrossweight, String totalvolume, String POnumber, String warehouseid, String locationid, String boeid, String COO, String invoiceqty, String invoiceprice)
    {
        if (String.IsNullOrEmpty(quantity) == true)
            quantity = "0";
        if (String.IsNullOrEmpty(itemprice) == true)
            itemprice = "0";
        if (String.IsNullOrEmpty(itemvolume) == true)
            itemvolume = "0";
        if (String.IsNullOrEmpty(grossweight) == true)
            grossweight = "0";
        if (String.IsNullOrEmpty(totalprice) == true)
            totalprice = "0";
        if (String.IsNullOrEmpty(totalgrossweight) == true)
            totalgrossweight = "0";
        if (String.IsNullOrEmpty(totalvolume) == true)
            totalvolume = "0";
        if (String.IsNullOrEmpty(invoiceqty) == true)
            invoiceqty = "0";
        if (String.IsNullOrEmpty(invoiceprice) == true)
            invoiceprice = "0";

        String sql = " insert into  deliveryOrderDetails (do_id, stock_id,  quantity ,item_price , item_volume , gross_weight , total_price, total_gross_weight , total_volume, PO_number ,warehouse_id, location_id, boe_id , COO ,invoice_qty, invoice_price, lastUpdated) values (" + doid + ", " + stockid + ", " + quantity + ", " + itemprice + ", " + itemvolume + ", " + grossweight + ", " + totalprice + ", " + totalgrossweight + ", " + totalvolume + ", '" + POnumber + "', " + warehouseid + ", " + locationid + ", '" + boeid + "', " + COO + ", " + invoiceqty + ", " + invoiceprice + ", '" + DateTime.Now + "')";
        String sql1 = "delete from tempAllocate where tempid=" + tempid;
        exeQuery.executeDMl(sql);
        return exeQuery.executeDMl(sql1);
    }


    public Boolean updateDoDetails(String doDetailID, String stockid, String quantity, String itemprice, String itemvolume, String grossweight, String totalprice, String totalgrossweight, String totalvolume, String POnumber, String warehouseid, String locationid, String boeid, String COO, String invoiceqty, String invoiceprice)
    {
        String sql = "UPDATE dbo.deliveryOrderDetails SET stock_id = '" + stockid + "', quantity = " + quantity + " ,item_price =   " + itemprice + ",item_volume =" + itemvolume + ",gross_weight =" + grossweight + " ,total_price=  " + totalprice + " ,total_gross_weight = " + totalgrossweight + ",total_volume = " + totalvolume + ",PO_number  = '" + POnumber + "' ,warehouse_id  = " + warehouseid + " ,location_id = " + locationid + " ,boe_id  = '" + boeid + "',COO = " + COO + ",invoice_qty = " + invoiceqty + ", invoice_price =" + invoiceprice + " , lastUpdated = GETDATE() where do_detail_id =" + doDetailID;
        return exeQuery.executeDMl(sql);

    }
    public Boolean DeleteDoDetails(string doDetailID)
    {
        String sql = "Delete from deliveryOrderDetails  where do_detail_id =" + doDetailID;
        return exeQuery.executeDMl(sql);
    }


    public DataTable selectStock(String StockiD)
    {
        String sql = "select * from selectStock  where stock_id =" + StockiD;
        return exeQuery.ExecuteTable(sql);
    }

    public String insertTempAllocate(String stockid, String warehouseid, String locationid, String price, String boeid, String POnumber, String username, string coo, string qty)
    {
        String sql = "insert into tempAllocate ( stock_id , warehouse_id ,  location_id , price  ,  boe_id , PO_number , username,country_id,quantity) values(" + stockid + " ,  " + warehouseid + " ,  " + locationid + " ,  " + price + " ,  '" + boeid + "' ,  '" + POnumber + "' ,  '" + username + "', " + coo + ","+ qty +" )";
        exeQuery.executeDMl(sql);
        // we need to get the latest Temp ID so that we can bind data when we response direct .
        // i select it of the user as here the select is done just after insert .. and tempid 
        //is autoincrement .
        
        string sql1 = "select max(tempid) as tempid from tempAllocate";
        DataTable dt = exeQuery.ExecuteTable(sql1);
        return dt.Rows[0]["tempid"].ToString();
    }

    public DataTable bindTempAllocateData(string tempid)
    {
        String sql = "select * from dbo.AllocateOrderforGrid where tempid=" + tempid;
        return exeQuery.ExecuteTable(sql);
    }
}