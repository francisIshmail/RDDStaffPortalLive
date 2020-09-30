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
/// Summary description for ClsAdmin
/// </summary>
public class ClsAdmin
{
    WMSSqlHelper exeQuery = new WMSSqlHelper();
	public ClsAdmin()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region customer 

    public DataTable bincustomer(string search)
    {
        String sql = "";
        if (string.IsNullOrEmpty(search) == true)
            sql = "select * from dbo.customer order by customer_id desc";
        else
            sql = "select * from dbo.customer where "+ search +"order by customer_id desc";
        return exeQuery.ExecuteTable(sql);

    }

    public Boolean deletecustomer(String customerID)
    {
        String sql = "Delete from customer where customer_id=" + customerID;
        return exeQuery.executeDMl(sql);
    }

    public Boolean UpdateCustomer(String CustName, String contact, String cell, String address1, String address2, String address3, String address4, String address5, String territory, String telephone, String telephone2, String fax1, String email, String post1, String post2, String post3, String post4, String post5, String custID)
    {

        String sql = " update customer set customer_name ='" + CustName + "',customer_contact ='" + contact + "',cell  ='" + cell + "',address1  ='" + address1 + "',address2  ='" + address2 + "',address3  ='" + address3 + "',address4  ='" + address4 + "',address5  ='" + address5 + "',territory  ='" + territory + "',telephone  ='" + telephone + "',telephone2  ='" + telephone2 + "' ,fax1  ='" + fax1 + "',email  ='" + email + "',post1  ='" + post1 + "',post2  ='" + post2 + "',post3  ='" + post3 + "',post4  ='" + post4 + "',post5  ='" + post5 + "' where  customer_id =" + custID + " ;";
        return exeQuery.executeDMl(sql);
    }

    public Boolean insertCustomer(String CustName, String contact, String cell, String address1, String address2, String address3, String address4, String address5, String territory, String telephone, String telephone2, String fax1, String email, String post1, String post2, String post3, String post4, String post5)
    {
        String sql = "insert into customer (customer_name,customer_contact,cell,address1,address2,address3,address4,address5,territory,telephone,telephone2 ,fax1,email,post1,post2,post3,post4,post5 )  values ('" + CustName + "','" + contact + "','" + cell + "','" + address1 + "','" + address2 + "','" + address3 + "','" + address4 + "','" + address5 + "','" + territory + "','" + telephone + "','" + telephone2 + "','" + fax1 + "','" + email + "','" + post1 + "','" + post2 + "','" + post3 + "','" + post4 + "','" + post5 + "')";
        return exeQuery.executeDMl(sql);
    }

    public Boolean deleteCustomer(String custID)
    {
        String sql = "Delete from customer where customer_id =" + custID + " ;";
        return exeQuery.executeDMl(sql);
    }
      


    #endregion

    # region freightforwarder
    public DataTable bindFreightForwarder(string search)
    {
        String sql = "";
        if (string.IsNullOrEmpty (search )==true)
            sql = "select * from dbo.freight_forwarder order by freight_forwarder_id desc";
        else
            sql = "select * from dbo.freight_forwarder where " + search + " order by freight_forwarder_id desc";

        return exeQuery.ExecuteTable(sql);

    }

    public DataTable selectFreightForwarder(String freightID)
    {
        String sql = " select   *  from freight_forwarder where freight_forwarder_id =" + freightID;
        return exeQuery.ExecuteTable(sql);
    }


    public Boolean  updateFreightForwarder(String forwardername, String contact, String cell, String fax, String phone, String email, String address1, String address2, String address3,String freightID)
    {
        String sql = "update freight_forwarder set freight_forwarder_name ='" + forwardername + "', contact  ='" + contact + "', cell  ='" + cell + "', fax  ='" + fax + "', phone ='" + phone + "', email ='" + email + "', address1 ='" + address1 + "', address2 ='" + address2 + "', address3 ='" + address3 + "' where  freight_forwarder_id =" + freightID + ";";
        return exeQuery.executeDMl(sql);
    }


    public Boolean InsertFreightForwarder(String forwardername, String contact, String cell, String fax, String phone, String email, String address1, String address2, String address3)
    {
        String Sql = " insert into  freight_forwarder (freight_forwarder_name, contact, cell, fax, phone, email, address1, address2, address3 ) values ( '" + forwardername + "','" + contact + "','" + cell + "','" + fax + "','" + phone + "','" + email + "','" + address1 + "','" + address2 + "','" + address3 + "')";
        return exeQuery.executeDMl(Sql);
    }

    public Boolean deleteFreightForwarder(String freightID)
    {
        String sql = "Delete from freight_forwarder where freight_forwarder_id =" + freightID;
        return exeQuery.executeDMl(sql);
    }
      

    #endregion 

    #region Supplier 

    public DataTable bindsupplier()
    {
        String sql = "select supplier_id,supplier_name from dbo.supplier order by supplier_name ";
        return exeQuery.ExecuteTable(sql);
    }


    public DataTable Bindsuppliers( String search)
    {

         String sql ="";
        if (string.IsNullOrEmpty (search )== true)
            sql = "select * from supplier order by supplier_id desc";
        else
            sql = "select * from supplier  where " + search + " order by supplier_id desc";
        return exeQuery.ExecuteTable(sql);
    }

    public DataTable Selectsupplier(String supplierID)
    {
        String sql = "select * from supplier where supplier_id = " + supplierID;
        return exeQuery.ExecuteTable(sql);

    }

    public DataTable deleteSupplier(String supplierID)
    {
        String sql = "Delete from  supplier where supplier_id = " + supplierID;
        return exeQuery.ExecuteTable(sql);

    }

    public Boolean updateSupplier(String supplierid, String name, String contact, String phone, String cell, String fax)
    {
        String Sql = "update supplier set    supplier_name ='" + name + "' ,contact_name  ='" + contact + "',phone_number  ='" + phone + "',cell_number  ='" + cell + "',fax_number  ='" + fax + "' where supplier_id =" + supplierid;
        return exeQuery.executeDMl(Sql);
    }
    public Boolean insertSupplier( String name, String contact, String phone, String cell, String fax)
    {
        String Sql = "insert into  supplier (supplier_name ,contact_name,phone_number,cell_number,fax_number) values ( '" + name + "' ,'" + contact + "' ,'" + phone + "' ,'" + cell + "' ,'" + fax + "' ) ";
        return  exeQuery.executeDMl(Sql);
    } 
    #endregion 

        
    #region Country
    public DataTable Bindcountry()
    {
        String sql = "select * from dbo.country  order by country_name";
        return exeQuery.ExecuteTable(sql);

    }
    public DataTable selectCustomer(String custID)
    {
        String sql = "select * from customer where customer_id =" + custID;
        return exeQuery.ExecuteTable(sql);
    }
    public Boolean AddnewCountry(String Country)
    {


        String Sql1 = "insert into country (country_name) values (upper ('" + Country + "'))";
        return exeQuery.executeDMl(Sql1);
    }
    #endregion 

    #region WhareHouse
    public DataTable BindWarehouse(String Search )
    {
        String sql ="";
        if (String.IsNullOrEmpty(Search)== true)
         sql = "select * from  dbo.warehouse order by warehouse_code";
        else
        sql = "select * from  dbo.warehouse where "+ Search +" order by warehouse_code";
        return exeQuery.ExecuteTable(sql);

    }
    public DataTable selectWarehouse(String warehouseID)
    {
        String sql = "select * from  dbo.warehouse where warehouse_id=" + warehouseID;
        return exeQuery.ExecuteTable(sql);
    }

    public Boolean updateWarehouse(String code, String description, String evo, String status , String warehouseID)
    {
        String Sql = "update warehouse set warehouse_code ='" + code + "' description  ='" + description + "'   warehouseEVO   ='" + evo + "'  Status  ='" + status + "' where  warehouse_id ="+ warehouseID;
        return exeQuery.executeDMl(Sql);
    }

    public Boolean insertWrehouse(String code, String description, String evo, String status)
    {
        String Sql = "insert into warehouse( warehouse_code description warehouseEVO Status) values ('" + code + "','" + description + "','" + evo + "','" + status + "');";
 return exeQuery.executeDMl(Sql);
    }

    #endregion 


    #region Product 


    public DataTable SelectStock(String ProductiD)
    {
        String sql = "select * from stockItem where stock_id =" + ProductiD;
        return exeQuery.ExecuteTable(sql);

    }


    public Boolean DeleteStock(String stockId)
    {
        String sql = "Delete  from stockItem where stock_id =" + stockId;
        return exeQuery.executeDMl(sql);
    }
    public Boolean updateStock(String partnumber,String stock_id, String description, String HScode, String price, String PackID, String gross_weight, String length, String width, String height, String supplier_id, String category_id, String product_line)
{
    String sql = " update stockItem set part_number = '" + partnumber + "',   description = '" + description + "', HScode =" + HScode + " ,price =" + price + " ,PackID ='" + PackID + "', gross_weight  =" + gross_weight + ", length  =" + length + ",  width  =" + width + "  ,height  =" + height + ", supplier_id  =" + supplier_id + ", category_id = '" + category_id + "', product_line  = '" + product_line + "' where stock_id = " + stock_id + " ;";
    return exeQuery.executeDMl(sql);
}
    public Boolean insertStock(String description, String partnumber, String HScode, String price, String PackID, String gross_weight, String length, String width, String height, String supplier_id, String category_id, String product_line)
    {
        if (string.IsNullOrEmpty(price) == true)
            price = "0";
        if (string.IsNullOrEmpty(PackID) == true)
            PackID = "0";
        if (string.IsNullOrEmpty(gross_weight) == true)
            gross_weight = "0";
        if (string.IsNullOrEmpty(length) == true)
            length = "0";
        if (string.IsNullOrEmpty(width) == true)
            width = "0";
        if (string.IsNullOrEmpty(height) == true)
            height = "0";
        if (string.IsNullOrEmpty(category_id) == true)
            category_id = "0";

        String sql = " insert into  stockItem (  part_number,description, HScode ,price, PackID ,gross_weight , length , width , height , supplier_id ,category_id, product_line  ) values ('" + partnumber + "','" + description + "','" + HScode + "'," + price + "," + PackID + "," + gross_weight + "," + length + "," + width + "," + height + "," + supplier_id + ",'" + category_id + "','" + product_line + "' )";
        return exeQuery.executeDMl(sql);
    }
   
    #endregion 
    public DataTable BindUsers()
    {
        String sql = " select * from  dbo.[user] order by user_name";
        return exeQuery.ExecuteTable(sql);

    }



}