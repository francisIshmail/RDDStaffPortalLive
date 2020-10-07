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
/// Summary description for WMSClsStock
/// </summary>
public class WMSClsStock
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
	public WMSClsStock()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DataTable gvStockbyWerahouse()
    {

        String sql = "SELECT [DO DRAFT only details].SumOfquantity as draftonly , stockItem.part_number, stockItem.description, warehouse.description AS description_warehouse, warehouseStock.warehouse_id, warehouseStock.stock_id, warehouseStock.quantity, warehouseStock.price, warehouseStock.creation_date, [quantity]-[SumofQuantity] AS Avail, [quantity]*[length]*[width]*[height] AS CBM, [quantity]*[gross_weight] AS totweight FROM warehouse INNER JOIN ((stockItem INNER JOIN warehouseStock ON stockItem.stock_id = warehouseStock.stock_id) LEFT JOIN [DO DRAFT only details] ON (warehouseStock.warehouse_id = [DO DRAFT only details].warehouse_id) AND (warehouseStock.stock_id = [DO DRAFT only details].stock_id)) ON warehouse.warehouse_id = warehouseStock.warehouse_id WHERE (((warehouseStock.quantity)<>0)) order by  warehouseStock.warehouse_id;";
       return exeQuery.ExecuteTable(sql);
    }


    public DataTable stockbyboe(int stockid, int warehouseid)
    {

        String sql = "select * from StockbyBoe where  warehouse_id = " + warehouseid + " and  stock_id =" + stockid + "  ;";
        return exeQuery.ExecuteTable(sql);
    }

     public DataTable stocksheetrpt()
    {

        String sql = "select * from StocksheetRpt";
        return exeQuery.ExecuteTable(sql);
    }


     public DataTable bindlocation()
     {
         String Sql = "select location_id,location_description  from location order by location_description";
         return exeQuery.ExecuteTable(Sql);
     }

     public DataTable BindWarehouse()
     {
         String sql = "select warehouse_id, description   from warehouse order by description";
         return exeQuery.ExecuteTable(sql);
     }

     public Boolean adjuststock(int qtyToAdjust, String StokbyboePrice, int warehouseid, int stockid, int locationid, String boeid, int qtyAvailable, String comment)
     {
         String Sql1 = "SELECT * FROM warehouseStockBOELocation WHERE (warehouse_id=" + warehouseid + ") AND (location_id=" + locationid + ") AND (stock_id=" + stockid + ") AND (boe_id='" + boeid + "')";
         DataTable dt1 = exeQuery.ExecuteTable(Sql1);
         float price = 0;
         int qty = 0;
         if (dt1.Rows.Count > 0)
         {
             price = float.Parse(dt1.Rows[0]["price"].ToString());
             qty = int.Parse(dt1.Rows[0]["quantity"].ToString());
         }
         int newqty = qty + qtyToAdjust;
         float newprice = ((price * qty) + (qtyToAdjust + float.Parse(StokbyboePrice))) / (qty + qtyToAdjust);

         String Sql = "update warehouseStockBOELocation set quantity = " + newqty + " , price =" + newprice + " WHERE (warehouse_id=" + warehouseid + ") AND (location_id=" + locationid + ") AND (stock_id=" + stockid + ") AND (boe_id='" + boeid + "')";
         exeQuery.executeDMl(Sql);


         String sql2 = "SELECT * FROM warehouseStock WHERE (warehouse_id=" + warehouseid + ") AND (stock_id=" + stockid + ")";
         DataTable dt2 = exeQuery.ExecuteTable(sql2);
         float price1 = 0;
         int qty1 = 0;
         if (dt2.Rows.Count > 0)
         {
             price1 = float.Parse(dt1.Rows[0]["price"].ToString());
             qty1 = int.Parse(dt1.Rows[0]["quantity"].ToString());
         }
         int newqty1 = qty1 + qtyToAdjust;
         float newprice1 = ((price1 * qty1) + (qtyToAdjust + float.Parse(StokbyboePrice))) / (qty1 + qtyToAdjust);

         String Sql3 = "update warehouseStock set quantity = " + newqty + " , price =" + newprice + "WHERE (warehouse_id=" + warehouseid + ") AND (stock_id=" + stockid + ")";
         exeQuery.executeDMl(Sql3);


         String sql4 = "insert into StockMovement ( Type, stock_id,boe_id,warehouse_id,location_id,quantity,price,transaction_date,transaction_reference,comments) values('ADJ', " + stockid + " ,'" + boeid + "', " + warehouseid + "," + locationid + " ," + qtyToAdjust + "," + StokbyboePrice + ", getdate(), 'Starting qty " + qtyAvailable + "','" + comment + "' );";
         return exeQuery.executeDMl(sql4);
     }


     public Boolean StockXFR(int Sourcewarehoueid, string stockid, int Destwarehoueid, int qtyToXfr, String StokbyboePrice , String boeid, int Sourcelocationid, int Destlocationid, String comment)
     {

        // warehouse Transfer 

         if (Sourcewarehoueid != Destwarehoueid)
         {
             String sql = "SELECT * FROM warehouseStock WHERE (warehouse_id=" + Sourcewarehoueid + ") AND (stock_id=" + stockid + ")";
             DataTable dt = exeQuery.ExecuteTable(sql);
             int newqty = 0;
             if (dt.Rows.Count > 0)
             {
                 newqty = int.Parse(dt.Rows[0]["quantity"].ToString()) + qtyToXfr;
                 String sql1 = "update warehouseStock set quantity = " + newqty + " , creation_date =getdate () WHERE (warehouse_id=" + Sourcewarehoueid + ") AND (stock_id=" + stockid + ")";
                 exeQuery.executeDMl(sql1);
             }



             String Sql2 = "SELECT * FROM warehouseStock WHERE (warehouse_id=" + Destwarehoueid + ") AND (stock_id=" + stockid + ")";
             DataTable dt2=  exeQuery.ExecuteTable(Sql2);
             int newqty1 =0 ;
             float  newprice1 =0;
             if (dt2.Rows .Count > 0)
             {
                 newqty1 = int.Parse(dt2.Rows[0]["quantity"].ToString()) +qtyToXfr;
                 newprice1 = ((  (float.Parse(dt2.Rows[0]["price"].ToString()) * int.Parse(dt2.Rows[0]["quantity"].ToString())) + (qtyToXfr + float.Parse(StokbyboePrice ))) /(qtyToXfr + int.Parse(dt2.Rows[0]["quantity"].ToString())));
                 String sql2 = "update warehouseStock set quantity = " + newqty1 + " , price= "+ newprice1 +" creationDate =getdate() WHERE (warehouse_id=" + Destwarehoueid + ") AND (stock_id=" + stockid + ")";
                 exeQuery.executeDMl(sql2);
             }
             else
             {
                 String sql3 = "insert into  warehouseStock (warehouse_id,stock_id,quantity,price,creation_date) values ("+Destwarehoueid +" , "+stockid+" , "+qtyToXfr+" ,"+StokbyboePrice+", getdate() )";
                 exeQuery.executeDMl(sql3);
               
             }

             // warehouseStockBOELocation transfer 
             String sql4 = "SELECT * FROM warehouseStockBOELocation WHERE (warehouse_id="+ Sourcewarehoueid  + ") AND (stock_id=" + stockid + ") AND (boe_id='" +  boeid + "') AND (location_id=" + Sourcelocationid+");";
              DataTable dt4 = exeQuery.ExecuteTable(sql4);
             int newqty4= 0;
             if (dt4.Rows.Count > 0)
             {
                 newqty4= int.Parse(dt4.Rows[0]["quantity"].ToString()) - qtyToXfr ;
                String sql5 = "update warehouseStockBOELocation set quantity ="+newqty4+" , creation_date= getdate() where  (warehouse_id="+ Sourcewarehoueid  + ") AND (stock_id=" + stockid + ") AND (boe_id='" +  boeid + "') AND (location_id=" + Sourcelocationid+");";
               exeQuery.executeDMl(sql5);
             }
             String sql6 = "SELECT * FROM warehouseStockBOELocation WHERE (warehouse_id="+ Sourcewarehoueid  + ") AND (stock_id=" + stockid + ") AND (boe_id='" +  boeid + "') AND (location_id=" + Destlocationid+");";
             DataTable dt6 = exeQuery.ExecuteTable(sql4);
             int newqty5= 0;
             if (dt6.Rows.Count >0)
             {
                 newqty5 = int.Parse(dt6.Rows[0]["quantity"].ToString()) + qtyToXfr ;
                 String sql7 = "update warehouseStockBOELocation set quantity ="+newqty4+" , creation_date= getdate() WHERE (warehouse_id="+ Sourcewarehoueid  + ") AND (stock_id=" + stockid + ") AND (boe_id='" +  boeid + "') AND (location_id=" + Destlocationid+");";
                  exeQuery.executeDMl(sql7);
             }

             else 
             {
              String    Sql8 = "insert into warehouseStockBOELocation (warehouse_id,location_id,stock_id,boe_id,price,quantity,creation_date) values ("+ Destwarehoueid +", "+ Destlocationid +" , "+ stockid +","+ boeid +", "+ StokbyboePrice + ", "+qtyToXfr +" , getdate())";
               exeQuery.executeDMl(Sql8);

             }
   
             // Save in Stokmovement now 

              String sql9 = "insert into StockMovement ( Type, stock_id,boe_id,warehouse_id,location_id,quantity,price,transaction_date,transaction_reference,comments) values('XFR', " + stockid + " ,'" + boeid + "', " + Sourcewarehoueid  + "," + Sourcelocationid + " ," + qtyToXfr  + "," + StokbyboePrice + ", getdate(), 'To Warehouse " + Destwarehoueid  + " Location " + Destlocationid +"','" + comment + "' );";
             String sql10 = "insert into StockMovement ( Type, stock_id,boe_id,warehouse_id,location_id,quantity,price,transaction_date,transaction_reference,comments) values('XFR', " + stockid + " ,'" + boeid + "', " + Destwarehoueid   + "," + Destlocationid  + " ," + qtyToXfr  + "," + StokbyboePrice + ", getdate(), 'From Warehouse " + Sourcewarehoueid   + " Location " + Sourcelocationid  +"','" + comment + "' );";

             exeQuery.executeDMl(sql9);
             exeQuery.executeDMl(sql10);
         }
            
         return true ;
     }

     public DataTable  stookSheet()
     {
         String sql = "DELETE  FROM stocksheet;";
         exeQuery.executeDMl(sql);
         String sql1 = "insert into  stocksheet SELECT stocksheetSQL.*,[W].[warehouseEVO], getdate() AS refreshDate FROM stocksheetSQL,warehouse W  WHERE [warehouse]=W.[description]   ORDER BY Part,warehouse";
         exeQuery.executeDMl(sql1);
         String sql3 = "select * from stocksheet";
         return exeQuery.ExecuteTable(sql3);
     }


     public DataTable docStages()
     {
         String sql = "select * from WMS.dbo.docStages";
         return exeQuery.ExecuteTable(sql);
     }

     public DataTable excelStockSheet()
     {
         String sql = "select * from excelStockSheet";
         return exeQuery.ExecuteTable(sql);
     }

     public DataTable DoList()
     {
         String sql = "select * from deliveryOrder";
         return exeQuery.ExecuteTable(sql);
     }

     public DataTable checkexperibleReservation(String doid)
     {
         String sql = "select * from expireableReservations where DO# =" + doid;
         return exeQuery.ExecuteTable(sql);
     }

     public Boolean updateStockStatus(String doid)
     {
         String sql = "update   deliveryOrder set Status ='EXPIRED' where  do_id=" + doid;
        return  exeQuery.executeDMl(sql);

     }

     public DataTable reserveitem()
     {
         String sql = "select * from reservedItems";
         return exeQuery.ExecuteTable(sql);
     }
}

