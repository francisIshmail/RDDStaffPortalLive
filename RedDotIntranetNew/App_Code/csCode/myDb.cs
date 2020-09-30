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
/// Summary description for Db
/// </summary>
public static class Db
{
    public static String constr;

    //public Db(String conn)
    //{
    //    constr = conn; 
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    public static SqlDataReader myGetReader(String Sql)
    {
        SqlDataReader drd;
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlCommand cmd = new SqlCommand(Sql,dbconn);
        drd = cmd.ExecuteReader();
        return drd;
    }

    public static DataSet myGetDS(String Sql)
    {
        DataSet ds = new DataSet();
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlDataAdapter da = new SqlDataAdapter(Sql, dbconn);
        da.Fill(ds, "Table");
        dbconn.Close();

        return ds;
    }

    public static void myExecuteSQL(String Sql)
    {

        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlCommand cmd = new SqlCommand(Sql, dbconn);
        try
        {
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
        }
        catch (Exception ex)
        {
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            throw (ex);
        }
    }

    public static long myExecuteSQLReturnLatestAutoID(String Sql)
    {
        int AutoID = -1;
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlCommand cmd = new SqlCommand(Sql + "; SELECT CAST(scope_identity() as int) AS LastID", dbconn);
        try
        {
            AutoID = (int)cmd.ExecuteScalar();
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            return AutoID;
        }
        catch (Exception ex)
        {
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            throw (ex);
        }
    }

    public static int myExecuteScalar(String Sql)
    {
        int val = -1;
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlCommand cmd = new SqlCommand(Sql, dbconn);
        try
        {
            val=(int)cmd.ExecuteScalar();
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            return val;
        }
        catch (Exception ex)
        {
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            throw (ex);
        }
    }

    public static string myExecuteScalar2(String Sql)
    {
        string val = "-1";
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlCommand cmd = new SqlCommand(Sql, dbconn);
        try
        {
            val = (string)cmd.ExecuteScalar();
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            return val;
        }
        catch (Exception ex)
        {
            cmd.Dispose();
            dbconn.Close();
            dbconn.Dispose();
            throw (ex);
        }
    }

    public static void LoadlstWithCon(ListBox ddl, String qry, String txtFld, String ValFld, String ConnectDbStr)
    {
        constr = ConnectDbStr;
        DataSet Ds = myGetDS(qry);
        if (Ds.Tables.Count > 0)
        {
            ddl.DataSource = Ds.Tables[0];
            ddl.DataTextField = txtFld;
            ddl.DataValueField = ValFld;
            ddl.DataBind();
        }
    }

    public static void LoadDDLsWithCon(DropDownList ddl, String qry, String txtFld, String ValFld, String ConnectDbStr)
    {
        constr = ConnectDbStr;
        DataSet Ds = myGetDS(qry);
        if (Ds.Tables.Count > 0)
        {
            ddl.DataSource = Ds.Tables[0];
            ddl.DataTextField = txtFld;
            ddl.DataValueField = ValFld;
            ddl.DataBind();
        }
    }
  
    public static void LoadDDLsWithConNew(DropDownList ddl, String qry, String txtFld, String ValFld, String ConnectDbStr)
    {
        DataTable tmpTbl;
        ListItem lstItm;
        constr = ConnectDbStr;
        tmpTbl = myGetDS(qry).Tables[0];

        if (tmpTbl.Rows.Count > 0)
        {
            lstItm = new ListItem();
            lstItm.Text = "<Select Database>";
            lstItm.Value = "0";
            ddl.Items.Add(lstItm);

            foreach (DataRow dr in tmpTbl.Rows)
            {
                lstItm = new ListItem();
                lstItm.Text = dr[txtFld].ToString();
                lstItm.Value = dr[ValFld].ToString();
                ddl.Items.Add(lstItm);
            }
        }

    }

    //public static void LoadDDLsWithConRelsellersNCodeView(DropDownList ddl, String qry, String txtFld, String ValFld, String ConnectDbStr)
    //{
    //    DataTable tmpTbl;
    //    ListItem lstItm;
    //    constr = ConnectDbStr;
    //    tmpTbl = myGetDS(qry).Tables[0];

    //    if (tmpTbl.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in tmpTbl.Rows)
    //        {
    //            lstItm = new ListItem();
    //            lstItm.Text = dr[txtFld].ToString() + dr[ValFld].ToString();
    //            lstItm.Value = dr[ValFld].ToString();
    //            ddl.Items.Add(lstItm);
    //        }
    //    }

    //}
    public static void LoadDDLsWithConNew1(DropDownList ddl, String qry, String txtFld, String ValFld, String ConnectDbStr)
    {
        DataTable tmpTbl;
        ListItem lstItm;
        constr = ConnectDbStr;
        tmpTbl = myGetDS(qry).Tables[0];

        ddl.Items.Clear();
        if (tmpTbl.Rows.Count > 0)
        {
            foreach (DataRow dr in tmpTbl.Rows)
            {
                lstItm = new ListItem();
                lstItm.Text = dr[txtFld].ToString();
                lstItm.Value = dr[ValFld].ToString();
                ddl.Items.Add(lstItm);
            }

            lstItm = new ListItem();
            lstItm.Text = "Closed";
            lstItm.Value = "0";
            ddl.Items.Add(lstItm);
        }

    }
}
