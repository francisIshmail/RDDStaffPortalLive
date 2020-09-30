using System;
using Ad.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

public partial class IntranetNew_Orders_SalesOrderList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetUserList(string prefix)
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " Exec SO_Get_UserList ";
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
}