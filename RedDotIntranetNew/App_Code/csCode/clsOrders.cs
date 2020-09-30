using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsDealer1
/// </summary>
public class clsOrders
{
    SqlCommand cmdHSoft;
    SqlConnection connHSoft;
    //SqlDataAdapter sdaHSoft;
    //SqlDataReader sdrHSoft;

    string strQuery = string.Empty;
    private string strOutput = string.Empty;

    public clsOrders()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsOrder(string vendor, string comments, int Ordertatus, string usr, DateTime lastModified, string usrRole, string ordTyp)
    {
        int intResult = 0;
        using (connHSoft = new SqlConnection(myGlobal.getIntranetDBConnectionString()))
        {
            using (cmdHSoft = new SqlCommand("addOrder", connHSoft))
            {
                cmdHSoft.CommandType = CommandType.StoredProcedure;

                cmdHSoft.Parameters.AddWithValue("@vendor", vendor);
                cmdHSoft.Parameters.AddWithValue("@comments", comments);
                cmdHSoft.Parameters.AddWithValue("@fk_orderStatusId", Ordertatus);
                cmdHSoft.Parameters.AddWithValue("@orderByUser", usr);
                cmdHSoft.Parameters.AddWithValue("@lastModified", lastModified);
                cmdHSoft.Parameters.AddWithValue("@userRole", usrRole);
                cmdHSoft.Parameters.AddWithValue("@orderType", ordTyp);

                try
                {
                    cmdHSoft.Connection.Open();

                    intResult = Convert.ToInt32(cmdHSoft.ExecuteNonQuery());

                    Console.WriteLine("Number of Rows: " + intResult);
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message + ": [" + cmdHSoft.CommandText + "]");
                }
            }
        }
        return intResult;
    }

    
    public int InsPO(int OrderRequestLink, string vendor, string partNum, int quantity, string details, DateTime lastModified, string usr)
    {
        int intResult = 0;
        using (connHSoft = new SqlConnection(myGlobal.getIntranetDBConnectionString()))
        {
            using (cmdHSoft = new SqlCommand("addPO", connHSoft))
            {
                cmdHSoft.CommandType = CommandType.StoredProcedure;

                cmdHSoft.Parameters.AddWithValue("@OrderRequestLink", OrderRequestLink);
                cmdHSoft.Parameters.AddWithValue("@vendor", vendor);
                cmdHSoft.Parameters.AddWithValue("@partNum", partNum);
                cmdHSoft.Parameters.AddWithValue("@quantity", quantity);
                cmdHSoft.Parameters.AddWithValue("@details", details);
                cmdHSoft.Parameters.AddWithValue("@lastModified", lastModified);
                cmdHSoft.Parameters.AddWithValue("@orderByUser", usr);

                try
                {
                    cmdHSoft.Connection.Open();

                    intResult = Convert.ToInt32(cmdHSoft.ExecuteNonQuery());

                    Console.WriteLine("Number of Rows: " + intResult);
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message + ": [" + cmdHSoft.CommandText + "]");
                }
            }
        }
        return intResult;
    }

    public int InsRO(int OrderRequestLink, string vendor, string partNum, int quantity,float price,string details, DateTime lastModified, string usr)
    {
        int intResult = 0;
        using (connHSoft = new SqlConnection(myGlobal.getIntranetDBConnectionString()))
        {
            using (cmdHSoft = new SqlCommand("addRO", connHSoft))
            {
                cmdHSoft.CommandType = CommandType.StoredProcedure;

                cmdHSoft.Parameters.AddWithValue("@OrderRequestLink", OrderRequestLink);
                cmdHSoft.Parameters.AddWithValue("@vendor", vendor);
                cmdHSoft.Parameters.AddWithValue("@partNum", partNum);
                cmdHSoft.Parameters.AddWithValue("@quantity", quantity);
                cmdHSoft.Parameters.AddWithValue("@pricec", price);
                cmdHSoft.Parameters.AddWithValue("@details", details);
                cmdHSoft.Parameters.AddWithValue("@lastModified", lastModified);
                cmdHSoft.Parameters.AddWithValue("@orderByUser", usr);

                try
                {
                    cmdHSoft.Connection.Open();

                    intResult = Convert.ToInt32(cmdHSoft.ExecuteNonQuery());

                    Console.WriteLine("Number of Rows: " + intResult);
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message + ": [" + cmdHSoft.CommandText + "]");
                }
            }
        }
        return intResult;
    }

}
