using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Threading;

public partial class IntranetNew_Orders_SalesOrderOTP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = 1;//Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='SalesOrderOTP.aspx' And t1.IsActive=1");
            if (count > 0)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               // BindMenuDDL();
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=SOR Code Generator");
            }
        }
        lblMsg.Text = "";
    }

    /// <summary>
    /// To Load menus into dropdownlist control...
    /// </summary>
    /// <param name="ddlMenu"></param>
    private void BindMenuDDL()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS_SOROTP = Db.myGetDS("  Exec RDD_Get_GenerateSORCodeData ");

            if (DS_SOROTP.Tables.Count > 0)
            {

                ddlDatabase.DataSource = DS_SOROTP.Tables[0];
                ddlDatabase.DataTextField = "DBName";
                ddlDatabase.DataValueField = "DBName";
                ddlDatabase.DataBind();

                //ddlCountry.DataSource = DS_SOROTP.Tables[1];
                //ddlCountry.DataTextField = "country";
                //ddlCountry.DataValueField = "countrycode";
                //ddlCountry.DataBind();

                ddlBU.DataSource = DS_SOROTP.Tables[2];
                ddlBU.DataTextField = "BU";
                ddlBU.DataValueField = "BU";
                ddlBU.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindMenuDDL() : " + ex.Message;
        }
    }

    /// <summary>
    /// This webmethod is to bind the database & BU ddl on page load 
    /// </summary>
    /// <returns></returns>
 
    [WebMethod]
    public static string Get_BindDLList( )
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " Exec RDD_Get_GenerateSORCodeData ";
            DataSet DS = Db.myGetDS(sql);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    /// <summary>
    /// this web method is to validate the SOR - Number and get Draft SOR List in case of multiple Draft SOR's with same number
    /// </summary>
    /// <param name="DraftSORNum"></param>
    /// <param name="DBName"></param>
    /// <returns></returns>
 
    [WebMethod]
    public static string Validate_DraftSORNum(string DraftSORNum, string DBName)
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " Exec RDD_Validate_DraftSORNum " + DraftSORNum + "," + DBName;
            DataSet DS = Db.myGetDS(sql);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    /// <summary>
    ///  This web-method is to get list of all approver's for selected Draft SOR
    /// </summary>
    /// <param name="DBName"></param>
    /// <param name="DraftSORDocEntry"></param>
    /// <returns></returns>
    [WebMethod]
    public static string Get_DraftSORApproverList(string DBName, string DraftSORDocEntry)
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(" Exec RDD_GetDraftSORApproverList '" + DBName + "'," + DraftSORDocEntry);
            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    /// <summary>
    /// This web-method is to generate the SOR Approval Code
    /// </summary>
    /// <param name="BU"></param>
    /// <param name="Project"></param>
    /// <param name="DraftSORDocEntry"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GenerateOTP(string BU, string Project, string DraftSORDocEntry)
    {
        string retVal = string.Empty;
        try
        {
            Random _random = new Random();
            retVal = Project.Trim().ToUpper() + "-" + BU.Trim().ToUpper() +"-" + DraftSORDocEntry + '-' + _random.Next(50, 999989).ToString("D6");
            Thread.Sleep(700);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }


    




}