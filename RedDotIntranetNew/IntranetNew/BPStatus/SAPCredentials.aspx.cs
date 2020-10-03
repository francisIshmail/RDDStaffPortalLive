using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

public partial class IntranetNew_BPStatus_SAPCredentials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='SAPCredentials.aspx' and t1.IsActive=1");
                if (count > 0)
                {
                    BindDDL();
                    SqlDataReader rdr = Db.myGetReader(" Select * from dbo.BPStatus_SAP_StaffUserMapping where StaffUserName='" + myGlobal.loggedInUser() + "'");
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            txtSAPLoginName.Text = rdr["SAPUserName"].ToString();
                            txtSAPAEpwd.Text = rdr["SAPAEPassword"].ToString();
                            txtSAPKEpwd.Text = rdr["SAPKEPassword"].ToString();
                            txtSAPTZpwd.Text = rdr["SAPTZPassword"].ToString();
                            txtSAPUGpwd.Text = rdr["SAPUGPassword"].ToString();
                            txtSAPZMpwd.Text = rdr["SAPZMPassword"].ToString();
                            if (!string.IsNullOrEmpty(rdr["DefaultDB"].ToString()))
                            {
                                ddlDBName.SelectedItem.Text = rdr["DefaultDB"].ToString();
                                ddlDBName.SelectedItem.Value = rdr["DefaultDB"].ToString();
                            }
                            if (!string.IsNullOrEmpty(rdr["DefaultCountry"].ToString()))
                            {
                                ddlCountry.SelectedItem.Text = rdr["DefaultCountry"].ToString();
                                ddlCountry.SelectedItem.Value = rdr["DefaultCountry"].ToString();
                            }
                        }
                    }

                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=SAP Login Credentials");
                }
            }
        }
        catch (Exception ex) 
        { 
            
        }
    }

    private void BindDDL()
    {
        try
        {
            //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string qry = " select '--Select--' as Country union select Country from dbo.BPStatus_ActivateForCountry Where IsActive=1";

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(qry);
            ddlCountry.DataSource = DS.Tables[0];  // Table [0] for Deal Status
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "Country";
            ddlCountry.DataBind();

        }
        catch (Exception ex)
        {
        }
    }

    [WebMethod]
    public static string SaveData( string defaultDB, string defaultCountry, string SAPUserName, string SAPAEpwd, string SAPKEpwd, string SAPTZpwd, string SAPUGpwd, string SAPZMpwd)
    {
        string ReturnMsg = "1";
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " EXEC BPStatus_SaveSAPStaffUserMapping '" + myGlobal.loggedInUser() + "','" + SAPUserName + "','" + SAPAEpwd + "','" + SAPKEpwd + "','" + SAPTZpwd + "','" + SAPUGpwd + "','" + SAPZMpwd + "','" + defaultDB + "','"+defaultCountry+"'";
            Db.myExecuteSQL(sql);
        }
        catch (Exception ex)
        {
            ReturnMsg = " Exception occured in saveData : " + ex.Message;
        }
        return ReturnMsg; 
    }


}