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
/// Summary description for WMSclsPrealert
/// </summary>
public class WMSclsPrealert
{
    public WMSclsPrealert()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    WMSSqlHelper exeQuery = new WMSSqlHelper();
    
    public DataTable gvprealert(String whr)
    { 
        String sql ="";
        if (whr == "")
            //sql = "SELECT distinct  Prealert.Prealert_id, Status, Boe_id, eta,supplier_reference,supplier_name,freight_forwarder_name  FROM prealert ,   prealert_detail , supplier , freight_forwarder  where  (prealert.prealert_id  = prealert_detail.prealert_id) and (prealert.supplier_id=supplier.supplier_id) and ( freight_forwarder.freight_forwarder_id =prealert.freight_forwarder )   order by  prealert.prealert_id Desc;";
            sql = "select P.prealert_id,P.Status,P.Boe_id,P.eta,P.supplier_reference,S.supplier_name,F.freight_forwarder_name  from prealert as P left join supplier as S on P.supplier_id=S.supplier_id left join freight_forwarder as F on P.freight_forwarder=F.freight_forwarder_id order by P.prealert_id desc";
        else
            //sql = "SELECT distinct  Prealert.Prealert_id, Status, Boe_id, eta,supplier_reference,supplier_name,freight_forwarder_name  FROM prealert ,   prealert_detail , supplier , freight_forwarder  where  (prealert.prealert_id  = prealert_detail.prealert_id) and (prealert.supplier_id=supplier.supplier_id) and ( freight_forwarder.freight_forwarder_id =prealert.freight_forwarder ) and  " + whr +" order by  prealert.prealert_id Desc ;";
            sql = "select P.prealert_id,P.Status,P.Boe_id,P.eta,P.supplier_reference,S.supplier_name,F.freight_forwarder_name  from prealert as P left join supplier as S on P.supplier_id=S.supplier_id left join freight_forwarder as F on P.freight_forwarder=F.freight_forwarder_id " + whr + " order by P.prealert_id desc";

            return exeQuery.ExecuteTable(sql);
    }


    public DataTable prealert(string prealertID)
    {

        String sql = " select * from prealert inner join supplier on  prealert.supplier_id  = supplier.supplier_id left join  freight_forwarder on freight_forwarder.freight_forwarder_id =prealert.freight_forwarder where  prealert_id = " + prealertID;
        return exeQuery.ExecuteTable(sql);
    }


    public DataTable prealertgrid(string prealertID)
    {
        String sql = " select *, '' as boe_detail_id  from prealert_detail, country, warehouse,location where prealert_detail.COO =country.country_id  and prealert_detail.warehouse_id  = warehouse.warehouse_id and prealert_detail.location_id =  location.location_id and  prealert_detail.prealert_id=   " + prealertID;
        return exeQuery.ExecuteTable(sql);
    } 

    public Boolean DeletePrealertDEtails(String PrealertDetailsID)
    {
        String sql = "Delete from prealert_detail where prealert_detail_id = " + PrealertDetailsID;
        return exeQuery.executeDMl(sql);
    }

    public DataTable EditPrealert(string PrealertDetailsID)
    {
        String sql = "select * from prealert_detail where prealert_detail_id = " + PrealertDetailsID;
        return exeQuery.ExecuteTable(sql);
    }


    public Boolean updatePrealert(string stockid, string coo, string ponumber, string warehouseid, string locationid, string PrealertDetailsID)
    {

        String sql = " update prealert_detail set stock_id ="+stockid +" , COO="+ coo +" , PO_number '"+ponumber +"' , warehouse_id ="+ warehouseid +", location_id="+ locationid +" where prealert_detail_id = " + PrealertDetailsID;
        return exeQuery.executeDMl(sql);
    }

    public Boolean DeletePrealert(String PrealertID)
    {
        String sql = " Delete from prealert_detail where prealert_id = " + PrealertID;
        sql += "; Delete from Prealert where prealert_id = " + PrealertID;
        exeQuery.executeDMl(sql);
               return true;

    }
    public DataTable BIllofEntry(string boedID)
    {

        String sql = " SELECT * FROM billOfEntry  join  supplier on  billOfEntry.supplier_id  = supplier.supplier_id  left join  freight_forwarder on freight_forwarder.freight_forwarder_id =billOfEntry.freight_forwarder where boe_id = '" + boedID + "'";
        return exeQuery.ExecuteTable(sql);
    }


    public DataTable BIllofEntrygrid(string boedID)
    {

        String sql = " select *,'' as prealert_detail_id from billOfEntryDetails, country, warehouse,location  where billOfEntryDetails.COO =country.country_id  and billOfEntryDetails.warehouse_id  = warehouse.warehouse_id  and billOfEntryDetails.location_id =  location.location_id and boe_id =  '" + boedID + "'";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable NewPart( String Search )
    {
        String sql = "";
        if (Search =="")
            sql = "select * from stockItem , supplier where supplier.supplier_id  = stockItem.supplier_id order by  stock_id desc  ;";
        else
            sql = "select * from stockItem , supplier where supplier.supplier_id  = stockItem.supplier_id and  " + Search + " order by  stock_id desc  ;";
        return exeQuery.ExecuteTable(sql);

    }

    public DataTable newprealertID()
    {
        String sql = " select MAX(prealert_id) + 1 as newid  from prealert  ;";
        return exeQuery.ExecuteTable(sql);
    }

 


  
  


    public String insertPrealert(String prealert_id, String status, String boe_id, String creation_date, int shipping_method, String shipping_reference, int delivery_method, String eta, String total_quantity, String supplier_id, String supplier_reference, String freight_forwarder, String comments)
    {
        String sql = "insert into prealert (status ,boe_id,creation_date ,shipping_method,shipping_reference,delivery_method , eta ,total_quantity,supplier_id,supplier_reference,freight_forwarder,comments) values ( '" + status + "' , '" + boe_id + "' ,  getdate()  , " + shipping_method + " , '" + shipping_reference + "' , " + delivery_method + " ,  '" + eta + "'  , " + total_quantity + " , " + supplier_id + " ,  '" + supplier_reference + "' , " + freight_forwarder + " , '" + comments + "') ";
        String sql1 = "select MAX(prealert_id) as prealertid from prealert ;";
        exeQuery.executeDMl(sql);
      DataTable dt=  exeQuery.ExecuteTable(sql1);
      
      return dt.Rows[0]["prealertid"].ToString();

    }

    public Boolean insertPrealertDetails(int prealert_id, int stock_id, int COO, int quantity, String PO_number, int warehouse_id, int location_id)
    {
        String sql = "insert into prealert_detail (prealert_id , stock_id,COO,quantity,PO_number, warehouse_id ,location_id) values (" + prealert_id + "  , " + stock_id + " , " + COO + " , " + quantity + " , '" + PO_number + "' , " + warehouse_id + " , " + location_id + ") ";
        return exeQuery.executeDMl(sql);

    }

    public Boolean ConFirmedPrealertStatus(int prealert_id)
    {
        String sql = "Update  prealert set status ='CONFIRMED' where prealert_id = " + prealert_id + " ";
        return exeQuery.executeDMl(sql);
    }
    public Boolean ReceivePrealertStatus(String  prealert_id, String boeid)
    {
        String sql = "Update  prealert set status ='RECEIVED', boe_id='" + boeid + "'  where prealert_id = " + prealert_id + " ";
        return exeQuery.executeDMl(sql);

    }

    public DataTable Prealert_Unstuffing(int prealert_id)
    {
        String sql = " SELECT * from Prealert_Unstuffing where prealert_id = " + prealert_id + ";";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable receivedgood(String boeid)
    {
        String sql = " select * from boedetail WHERE boe_id = '" + boeid + "';";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable BindReceivedGoodUpdatingData(String BoeDetaiID)
    {
        String sql = " select * from boedetail WHERE boe_detail_id = " + BoeDetaiID ;
        return exeQuery.ExecuteTable(sql);
    }
    public Boolean DeleteBoeDetails(string BoeDetaiID)
    {
        String sql = "delete  from boedetail WHERE boe_detail_id = " + BoeDetaiID;
        return exeQuery.executeDMl (sql);

    }

    public Boolean updatereceivedgood(String warehouseid, String LocationID, String ponumber, String countryid, String qty, String dmg, String pkgs, String height, String width, String length, String vol, String totvol, String grosswt, String totgrosswt, String price, String totprice, String bodetailid)
    {
        if (string.IsNullOrEmpty(qty) == true)
            qty = "0";
        if (string.IsNullOrEmpty(dmg) == true)
            dmg = "0";
        if (string.IsNullOrEmpty(pkgs) == true)
            pkgs = "0";
        if (string.IsNullOrEmpty(height) == true)
            height = "0";
        if (string.IsNullOrEmpty(width) == true)
            width = "0";
        if (string.IsNullOrEmpty(length) == true)
            length = "0";
        if (string.IsNullOrEmpty(totvol) == true)
            totvol = "0";
        if (string.IsNullOrEmpty(grosswt) == true)
            grosswt = "0";
        if (string.IsNullOrEmpty(price) == true)
            price = "0";
        if (string.IsNullOrEmpty(totgrosswt) == true)
            totgrosswt = "0";
        if (string.IsNullOrEmpty(totprice) == true)
            totprice = "0";
      
        String sql = " update billOfEntryDetails set warehouse_id =" + warehouseid + ",location_id =" + LocationID + ",PO_number ='" + ponumber + "',COO =" + countryid + ",quantity=" + qty + ",damaged =" + dmg + ",packages =" + pkgs + ",item_height =" + height + " ,item_width =" + width + ",item_length =" + length + ",item_volume =" + vol + ",total_volume =" + totvol + " ,item_gross_weight =" + grosswt + " ,total_gross_weight =" + totgrosswt + ",item_price =" + price + " ,total_price =" + totprice + " where boe_detail_id =" + bodetailid;
        return exeQuery.executeDMl(sql);
    }
    public Boolean updateBoeDetails(String warehouseid, String LocationID, String ponumber, String countryid, String qty,  String vol, String totvol, String grosswt, String totgrosswt, String price, String totprice, String bodetailid)
    {
        String sql = " update billOfEntryDetails set warehouse_id =" + warehouseid + ",location_id =" + LocationID + ",PO_number ='" + ponumber + "',COO =" + countryid + ",quantity=" + qty + ",item_volume =" + vol + ",total_volume =" + totvol + " ,item_gross_weight =" + grosswt + " ,total_gross_weight =" + totgrosswt + ",item_price =" + price + " ,total_price =" + totprice + " where boe_detail_id =" + bodetailid;
        return exeQuery.executeDMl(sql);
    }


    public Boolean deleteboe(string boeid)
    {
        String sql = "delete from billOfEntryDetails where boe_id ='" + boeid + "'";
        String sql1 = "delete from billOfEntry where boe_id ='" + boeid + "'";
        exeQuery.executeDMl(sql);
        return exeQuery.executeDMl(sql1);
    }


    public Boolean insertbillOfEntryDetails(String prealert_id)
    {
        String sql = "insert into billOfEntryDetails (boe_id,stock_id,quantity,COO, PO_number,warehouse_id,location_id)select boe_id, stock_id ,quantity,COO, PO_number,warehouse_id,location_id from prealert_detail inner join prealert on prealert_detail.prealert_id = prealert.prealert_id  where prealert_detail.prealert_id = " + prealert_id;
      
        exeQuery.executeDMl(sql);
        String sql1 = "select * from stockItem WHERE stock_id= (select top 1 stock_id from  prealert_detail where prealert_id = " + prealert_id + ")";
        String sql2 = "select * from  prealert where prealert_id = " + prealert_id;
        DataTable dt = exeQuery.ExecuteTable(sql1);
        DataTable dt2 = exeQuery.ExecuteTable(sql2);
        if (dt.Rows.Count >= 0)
        {
            String boe_id = dt2.Rows[0]["boe_id"].ToString();

            String item_gross_weight = dt.Rows[0]["gross_weight"].ToString();

            String item_volume = (float.Parse(dt.Rows[0]["length"].ToString()) * float.Parse(dt.Rows[0]["width"].ToString()) * float.Parse(dt.Rows[0]["height"].ToString())).ToString();
            String total_gross_weight = (int.Parse(dt2.Rows[0]["total_quantity"].ToString()) * int.Parse(dt.Rows[0]["gross_weight"].ToString())).ToString();

            String total_price = (float.Parse(dt2.Rows[0]["total_quantity"].ToString()) * float.Parse(dt.Rows[0]["price"].ToString())).ToString();
            String total_volume = (float.Parse(dt2.Rows[0]["total_quantity"].ToString()) * float.Parse(dt.Rows[0]["length"].ToString()) * float.Parse(dt.Rows[0]["width"].ToString()) * float.Parse(dt.Rows[0]["height"].ToString())).ToString();
            String item_height = dt.Rows[0]["height"].ToString();
            String item_width = dt.Rows[0]["width"].ToString();
            String item_length = dt.Rows[0]["length"].ToString();


            String sqlupdate = " Update billOfEntryDetails set boe_id ='" + boe_id + "', item_gross_weight = " + item_gross_weight + ", item_volume = " + item_volume + "  ,total_gross_weight = " + total_gross_weight + "  , total_price =" + total_price + " ,total_volume = " + total_volume + ", item_height = " + item_height + " ,item_width =" + item_width + " ,item_length = " + item_length + "  where stock_id=  " + dt.Rows[0]["stock_id"].ToString() + " ; ";

            exeQuery.executeDMl(sqlupdate);
        }


        return true;
    }

    public Boolean insertbillOfEntry(string prealert_id)
    {
        String sql = "insert into  billOfEntry (boe_id,status,creation_date, prealert_id,shipping_method, shipping_reference ,delivery_method,total_quantity,supplier_id,supplier_reference,freight_forwarder) select boe_id, status,creation_date, prealert_id,shipping_method, shipping_reference, delivery_method,total_quantity,supplier_id,supplier_reference,freight_forwarder from  prealert where prealert_id =  " + prealert_id;
        return exeQuery.executeDMl(sql);
    }

    public Boolean updateArrivalDate(string boeID, String ArrivalDate,string remark)
    {
        String sql = "update  billOfEntry set actual_arrival_date ='" + ArrivalDate + "', remarks ='" + remark + "'  where boe_id='" + boeID + "'";
        return exeQuery.executeDMl(sql);
    }

}






