using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class IntranetNew_BPStatus_PVLists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDDL();
            BindGrid();
        }

    }

    public void BindDDL()
    {
        try
        {
             Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
             string sql = "EXEC  PV_GetCountriesAndCurrencies '"+myGlobal.loggedInUser()+"' ";
            DataSet DS = new DataSet();
            DS = Db.myGetDS(sql);
            if (DS.Tables.Count > 0)
            {
                ddlCountry.DataSource = DS.Tables[0];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "Country";
                ddlCountry.DataBind();
            }

            Db.constr = myGlobal.getMembershipDBConnectionString();
            SqlDataReader rdr = Db.myGetReader(" select isnull(PVCountry,'') PVCountry From dbo.aspnet_Users U where username='" + myGlobal.loggedInUser() + "'");
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ddlCountry.SelectedItem.Text = rdr["PVCountry"].ToString();
                    ddlCountry.SelectedItem.Value = rdr["PVCountry"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindDDL() : " + ex.Message;
        }
    }

    [WebMethod]
    public static string[] GetVendors(string prefix, string country)
    {
        List<string> vendors = new List<string>();
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = " select distinct VendorEmployee  from PV where Country='" + country + "' And VendorEmployee  like '%" + prefix + "%' ";
        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            vendors.Add(string.Format("{0}", rdr["VendorEmployee"]));
        }
        return vendors.ToArray();

    }

    [WebMethod]
    public static string[] GetBanks(string prefix, string country)
    {
        List<string> vendors = new List<string>();
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = " select distinct isnull(BankName,'') As BankName from PV where  Country='" + country + "' And isnull(BankName,'')<>'' And isnull(BankName,'') like '%" + prefix + "%' ";
        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            vendors.Add(string.Format("{0}", rdr["BankName"]));
        }
        return vendors.ToArray();
    }

    public void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            DataSet DS = Db.myGetDS(" EXEC PV_GetPVLists '" + myGlobal.loggedInUser() + "','"+txtVendorName.Text+"','"+txtBankName.Text+"','"+txtPVDate.Text+"','"+txtToPVDate.Text+"','"+txtPayRefNo.Text+"'  ");
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    grvPVLists.DataSource = DS;
                    grvPVLists.DataBind();

                    Session["PVLists"] = DS.Tables[0];
                }
                else
                {
                    lblMsg.Text = "  No data found, please change selection criteria and retry..";
                    return;
                }
            }
            else
            {
                lblMsg.Text = "  No data found, please change selection criteria and retry..";
                return;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindGrid() " + ex.Message;
        }

    }

    protected void grvPVLists_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = grvPVLists.SelectedRow;
            Label lblPVId = (Label)row.FindControl("lblPVId");
            Response.Redirect("PV.aspx?PVID=" + lblPVId.Text);

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on selectedIndex() " + ex.Message;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on BtnSearch_Click() " + ex.Message;
        }
    }

    protected void BtnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (grvPVLists.Rows.Count <= 0)
            {
                lblMsg.Text = "  No datafound, please change selection criteria and retry..";
                return;
            }

            if (Session["PVLists"] != null)
            {
                DataTable dt = (DataTable)Session["PVLists"];
                string attachment = "attachment; filename=PVLists_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on BtnExportToExcel_Click() " + ex.Message;
        }
    }
}