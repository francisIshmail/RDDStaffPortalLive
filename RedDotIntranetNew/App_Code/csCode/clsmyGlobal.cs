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
using System.IO;


/// <summary>
/// Summary description for clsGlobal
/// </summary>
public class myGlobal
{
    static SqlCommand cmdHSoft;
    static SqlConnection connHSoft;
    static string srvPrefix;

    public static int autoId_temp=0;
    public static SqlDataAdapter adap_temp;
    public static DataTable dt_temp;

    

	public myGlobal()
	{
		// TODO: Add constructor logic here
	}

    public static string ConnectionString
    {
        get
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["connstrWebsiteDB"].ConnectionString;
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get Database Connection string from Web Config File. Contact the site Administrator" + ex);
            }
        }
    }

    public static int trimFileLength 
    {
        get
        {
            //keep base path min-max lenght (110) 
            //file name auto creation  min-max lenght (70)
            //cut short actual filename max lenght (60)           so, 110+70+60+4=244  (should be <255 chars)
            return 60;  //pik only first 60 chars of file name
        }
    }
    
    public static string getSiteIPwithPortNo()
    {
        return System.Configuration.ConfigurationManager.AppSettings["IPnPort"];
    }
    public static string getcurrentMembershipDBName()
    {
        return System.Configuration.ConfigurationManager.AppSettings["currentMembershipDB"];
    }
    public static string getAppSettingsDataForKey(string apkey)
    {
        return System.Configuration.ConfigurationManager.AppSettings[apkey];
    }
    public static string getFileExtensionOnlyWithDot(string fln)
    {
        return fln.Substring(fln.LastIndexOf("."), fln.Length - (fln.LastIndexOf(".")));
    }
    public static string getFileNameWithoutExtension(string fln)
    {
        return fln.Substring(0, fln.LastIndexOf("."));
    }


    public static int removeFile(string pth)
    {
        try {

            FileInfo TheFile = new FileInfo(pth);
            if (TheFile.Exists) 
            {
              File.Delete(pth);
            } 
            else 
            {
              throw new FileNotFoundException();
             }
        }

        catch (FileNotFoundException ex)
        {
        }
        catch (Exception ex)
        {
        }

        return 0;
  
    }
    public static string loggedInUser()
    {
        return HttpContext.Current.User.Identity.Name;
    }
    public static Boolean isCurrentUserOnRole(string roleToCompare)
    {
        Boolean flg=false;
        string[] roleNames = Roles.GetRolesForUser(); // Roles.GetRolesForUser("tej");
        if (roleNames.Length > 0)
        {
            foreach (string str in roleNames)
            {
                if (str.ToLower().Contains(roleToCompare.ToLower()))
                {
                    flg = true;
                    break;  
                }
            }

        }
            //int index2 = Array.IndexOf(array, "banana");
        return flg;
    }
    public static string loggedInRole()
    {
        string[] roleNames = Roles.GetRolesForUser(); // Roles.GetRolesForUser("tej");
        if (roleNames.Length > 0)
            return roleNames[0].ToString();
        else
            return "";
    }
    public static string[] loggedInRoleList()
    {
        string[] roleNames = Roles.GetRolesForUser(); // Roles.GetRolesForUser("tej");
        for (int i = 0; i < roleNames.Length; i++)
            roleNames[i] = roleNames[i].ToUpper();
            return roleNames;
    }
    public static string loggedInRoleUserBased(string usr)
    {
        string[] roleNames = Roles.GetRolesForUser(usr); // Roles.GetRolesForUser("tej");
        return roleNames[0].ToString();
    }
    public static Boolean verifyConnectionToServer(string cnStr)
    {
        Boolean flg = false;
        try
        {
            SqlConnection sCon = new SqlConnection(cnStr);
            sCon.Open();
            sCon.Close();
            flg = true;
        }
        catch (Exception exo)
        {
            flg = false;
        }
        return flg;
    }
    //public static string getDBServerPrefix(string dbName)
    public static string getDBServerPrefix(string dbName)
    {
        String qry = "select serverPrefix from dbo.SqlConnectionServers where lower(databaseName)=lower('" + dbName + "')";
        Db.constr = ConnectionString;
        SqlDataReader drd = Db.myGetReader(qry);
        if (drd.HasRows)
        {
            drd.Read();
            qry = drd["serverPrefix"].ToString();
            drd.Close();
        }
        return qry;
    }
    public static string BaseSqlServer()
    {
        String qry = "select serverPrefix from dbo.SqlConnectionServers where lower(databaseName)=lower('websiteDB')";
        Db.constr = ConnectionString;
        SqlDataReader drd = Db.myGetReader(qry);
        if (drd.HasRows)
        {
            drd.Read();
            qry = drd["serverPrefix"].ToString();
            drd.Close();
        }
        return qry;
    }

    public static string getRequestValue(String dlrLoginName,String rqstFld)
    {
        String qry = "select "+rqstFld+" from dbo.dealer where dealerUid='" + dlrLoginName + "'";
        Db.constr =ConnectionString;
        SqlDataReader drd = Db.myGetReader(qry);
        if (drd.HasRows)
        {
            drd.Read();
            qry = "";
            qry = drd[rqstFld].ToString();
            drd.Close();
        }
        return qry;
    }
    public static string loggedInUserCountry()
    {
        return getRequestValue(loggedInUser(), "dealerCountry");
    }
    public static string loggedInUserCountryCode()
    {
        return getRequestValue(loggedInUser(), "dealerCountryCode");
    }
    public static int loggedInUserEvoLink()
    {
        return Convert.ToInt32(getRequestValue(loggedInUser(), "dealerEVOLink"));
    }
    public static string loggedInUserDb()
    {
        return getRequestValue(loggedInUser(), "dealerEVODB");
    }

   

    //public static string loggedInUserEmail()
    //{
    //    return getRequestValue(loggedInUser(), "dealerEmail");
    //}
    public static string loggedInUserEmail()
    {
        return membershipUserEmail(loggedInUser());
    }

    public static string loggedInUserCompany()
    {
        return getRequestValue(loggedInUser(), "dealerCompany");
    }
    public static string loggedInUserPhone()
    {
        return getRequestValue(loggedInUser(), "dealerPhone");
    }
    
    public static string getUserDbConnectionString()
    {
        String cntrycode = loggedInUserCountryCode();
        
        return getConnectionStringForDB(cntrycode);
    }

    public static string getConnectionStringForDB(string cntrycode)
    {
        string ret = "";

        if (cntrycode == "DU" || cntrycode == "ET" || cntrycode == "AN" || cntrycode == "BO" || cntrycode == "MA" || cntrycode == "MO" || cntrycode == "NA" || cntrycode == "RW" || cntrycode == "SR" || cntrycode == "ZA" || cntrycode == "ZM")
            cntrycode = "TRI";

        switch (cntrycode)
        {
            case "EVOTej":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOTej"];
                break;
            case "KEModify":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOKEModify"];
                break;
            case "TZModify":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOTZModify"];
                break;
            case "CC":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOCC"];
                break;
            case "OB1":
                ret = System.Configuration.ConfigurationManager.AppSettings["OB1"];
                break;
            case "EVO":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVO"];
                break;
            case "KE":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOKE"];
                break;
            case "UG":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOUG"];
                break;
            case "EPZ":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOEPZ"];
                break;
            case "TZ":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOTZ"];
                break;
            case "TRI":
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOJA"];
                break;
            default:
                ret = System.Configuration.ConfigurationManager.AppSettings["EVOJA"];  //DU
                break;
        }

        return ret;
    }
    public static string getConnectionStringForSapDBs(string cntrycode)
    {
        string ret = "";
        // cntrycode == "MA" || cntrycode == "ZA" ||  --Pramod : Remove this from below OR conditions as we have separate database for Malawi & Zammbia so added that
        if (cntrycode == "TRI" || cntrycode == "DU" || cntrycode == "ET" || cntrycode == "AN" || cntrycode == "BO" ||  cntrycode == "MO" || cntrycode == "NA" || cntrycode == "RW" || cntrycode == "SR" || cntrycode == "ZM")
            cntrycode = "AE";

        switch (cntrycode)
        {
            case "KE":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPKE"];
                break;
            case "UG":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPUG"];
                break;
            case "EPZ":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPEPZ"];
                break;
            case "TZ":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPTZ"];
                break;
            case "AE":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPAE"];
                break;
            case "ZA":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPZM"];
                break;
            case "MA":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPML"];
                break;
            case "SAPReportDB":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPReportDB"];
                break;
            case "SAPSRVCrystalLogCreds":
                ret = System.Configuration.ConfigurationManager.AppSettings["SAPSRVCrystalLogCreds"];
                break;
            case "TEJSAP":
                ret = System.Configuration.ConfigurationManager.AppSettings["TejSap"];
                break;
            default:
                ret = System.Configuration.ConfigurationManager.AppSettings["TejSap"];
                break;
        }

        return ret;
    }

    //public static string getConnectionStringForSapDBs(string sapDB)
    //{
    //    string ret = "";
    //    ret = System.Configuration.ConfigurationManager.AppSettings[sapDB];
    //    return ret;
    //}
    public static string getCredsForSapCompany(string credName)
    {
        string ret = "";
        ret = System.Configuration.ConfigurationManager.AppSettings[credName];
        return ret;
    }
    
    public static string getMembershipDBConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
    }
    public static string getIntranetDBConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["connstrIntranetDB"].ConnectionString;
    }
    public static string getRDDMarketingDBConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["connstrRDDMarketingDB"].ConnectionString;
    }
    public static string getRDDMarketingMailsDBConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["MarketingMailsDB"].ConnectionString;
    }
    
    public static string getSystemConfigValue(string parameterName)
    {
        string parameterValue;

        Db.constr = ConnectionString;
        SqlDataReader drd;
        drd = Db.myGetReader("select * from sysConfig where parameterName='" + parameterName + "'");
        drd.Read();
         parameterValue = drd["parameterValue"].ToString();
        drd.Close();

        return parameterValue;
    }
    
    public static int Seed
    {
        get
        {
            try
            {
                return 11111;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get license key. please contact to website administrator" + ex);
            }
        }
    }
    public static void writeToMailLog(string evnt,string recp,string msg,int errLvl,int errCode,string errDesc)
    {
        String qry = String.Format("insert into emailLog(eventType,recipientMailId,matter,errorLevel,errorCode,errorDesc,lastModified) values('{0}','{1}','{2}',{3},{4},'{5}','{6}')",evnt, recp, msg, errLvl, errCode, errDesc, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));

        Db.constr = ConnectionString;
        Db.myExecuteSQL(qry);
    }

    public static void writeToEventLog(string recp,string evnt, string msg)
    {
        String qry = String.Format("INSERT INTO eventLog (userID,eventType,description,lastModified) values('{0}','{1}','{2}','{3}')", recp, evnt, msg, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
        Db.constr = ConnectionString;
        Db.myExecuteSQL(qry);
    }
    public static void writeTodoLog(string recp, string evnt, string msg)
    {
        String qry =String.Format("INSERT INTO toDo(userID,taskType,arg1,lastModified) values('{0}','{1}','{2}','{3}')", recp, evnt, msg, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
        Db.constr = ConnectionString;
        Db.myExecuteSQL(qry);
    }

    public static string sendRoleBasedMail(string url, string strMsg, string userEmail, string newescalate, string toMailList,String fls)
    {
        if (toMailList.Trim() == "")
            toMailList = userEmail;

        strMsg += "<br/><br/>Please follow up the order link using your Login credentials: ";
        strMsg += "<br/>" + url;
        strMsg += "<br/><br/>Best Regards,<br/>Red Dot Distribution";

        strMsg = "<br/> <b>WorkFlow System Update</b><br/><br/>" + strMsg;

        ////Close this code line befor live
        //strMsg += "<br/><br/>In Actuall Mails will be sent to role/country/BU Basedmails : " + toMailList + " , CC (user itself) : " + userEmail; 

        //toMailList = "victor@eternatec.com";
        //userEmail = "vishav@eternatec.com"; //Overwriting email, just remove these two lines later 

        string msg="";
        //open later
        string ccCentralLogistic = "";
        ccCentralLogistic = myGlobal.getAppSettingsDataForKey("orderStatusCCAtAllLevelsTo");
        if (ccCentralLogistic != "")
        {
            if (userEmail == "")
               userEmail = ccCentralLogistic;
            else
             userEmail = userEmail + ";" + ccCentralLogistic;
        }
        msg = Mail.SendMultipleAttach(getSystemConfigValue("websiteEmailer"), toMailList, userEmail, "Task Updation", strMsg, true, "", fls);
        //msg = "success fixed.";
        return msg;
    }

    
    public static string sendRoleBasedMail11(string url, string strMsg, string userRole, string userEmail)
    {
        strMsg += "<br/><br/>Please follow up the following order link:";
        strMsg += "<br/>" + url;
        strMsg += "<br/><br/>Best Regards,<br/>Red Dot Distribution";

        string sql;
        sql = "select RE.emailList from dbo.orderescalate as OE join dbo.roleBasedEmails as RE on OE.escalateLevelId=RE.fk_escalateLevelId where OE.role='" + userRole + "'";
        Db.constr = getIntranetDBConnectionString();
        SqlDataReader dr = Db.myGetReader(sql);

        string toList = "", msg;
        while (dr.Read())
        {
            toList = dr[0].ToString();
        }

        msg = Mail.SendCC(getSystemConfigValue("websiteEmailer"), toList, userEmail, "Task Update", strMsg, true);
        //msg = Mail.SendCC(getSystemConfigValue("websiteEmailer"), "manu@eternatec.com", "manu@eternatec.com", "Task Update", strMsg, true);
        return msg;
    }
    public static string membershipUserEmail(string username)
    {
        try
        {
            return Membership.GetUser(username).Email;
        }
        catch(Exception expps)
        {
             return "vishav@eternate.com";
        }
          
    }

    public static void updateSapAddonceLogTable(string FunctionUsed, string codeValue, string descValue, int createdInDBAE, int createdInDBUG, int createdInDBTZ, int createdInDBKE, int createdInDBZM, string createdBy)
    {

        string sq = "insert into tejSap.dbo.tblAddOnceLog(FunctionUsed,codeValue,descValue,createdInDBAE,createdInDBUG,createdInDBTZ,createdInDBKE,createdInDBZM,createdBy) values('" + FunctionUsed + "','" + codeValue + "','" + descValue + "'," + createdInDBAE + "," + createdInDBUG + "," + createdInDBTZ + "," + createdInDBKE + "," + createdInDBZM + ",'" + createdBy + "')";
        Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP");
        Db.myExecuteSQL(sq);
        
    }
   

    public static string sendMailToNewUser(string dept, string userId, string pwd, string ToEmailId)
    {
        string strMsg;
        strMsg = "<br/><br/>Warm Welcome to <b>Red Dot Distribution</b> website.";
        strMsg += "<br/><br/>" + "Dear User You have been registered on our website. You have been enrolled under Role/Department : <b>" + dept + "</b><br/><br/> You can login to our site using the following credentials <br/><br/>";

        strMsg += "URL Link : " + getSiteIPwithPortNo() + " <br/><br/>";

        strMsg += "User ID  : <b>" + userId + "</b> <br/><br/>";
        strMsg += "Password : <b>" + pwd + "</b> <br/><br/>";

        strMsg += "This is system generated password , Please change it as per your convenience using <b>Changepassword</b> Password option on the top right corner of the page<br/><br/>";

        strMsg += "Link to change password: " + getSiteIPwithPortNo() + "/Login.aspx/change-password.aspx" + " <br/><br/>";
        strMsg += "<br/><br/>Best Regards,<br/>Red Dot Distribution";

        //ToEmailId = "kuldip@eternatec.com";  //over writes mail id for test purpose

        string msg = "";
        msg = Mail.Send(getSystemConfigValue("websiteEmailer"), ToEmailId, "Registered on Red Dot Distribution website", strMsg, true);
        return msg;
    }
}

