using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using SAPbobsCOM;
using SAPbouiCOM;

public partial class IntranetNew_BPStatus_ChangeBPStatus : System.Web.UI.Page
{
    public static SAPbobsCOM.Company oCompanyAE, oCompanyKE, oCompanyTZ, oCompanyUG, oCompanyZM;
    public static SAPbobsCOM.CompanyService oCompanyService;
    //SAPbobsCOM.SBObob oSBObob;
    public static bool connFlag = true; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='ChangeBPStatus.aspx' and t1.IsActive=1");
            if (count > 0)
            {
                BindDDL();
                try
                {
                    SqlDataReader rdr = Db.myGetReader(" Select DefaultDB,DefaultCountry from dbo.BPStatus_SAP_StaffUserMapping where StaffUserName='" + myGlobal.loggedInUser() + "'");
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if (!string.IsNullOrEmpty(rdr["DefaultDB"].ToString()))
                            {
                                ddlDBName.SelectedItem.Text = rdr["DefaultDB"].ToString();
                                ddlDBName.SelectedItem.Value = rdr["DefaultDB"].ToString();
                            }
                            if (!string.IsNullOrEmpty(rdr["DefaultCountry"].ToString()))
                            {
                                ddlRegion.SelectedItem.Text = rdr["DefaultCountry"].ToString();
                                ddlRegion.SelectedItem.Value = rdr["DefaultCountry"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=Change Customer Status In SAP");
            }

        }

    }

    private void BindDDL()
    {
        try
        {
           
            string qry = " select '--Select--' as Country union select Country from dbo.BPStatus_ActivateForCountry Where IsActive=1";
           
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(qry);
            ddlRegion.DataSource = DS.Tables[0];  // Table [0] for Deal Status
            ddlRegion.DataTextField = "Country";
            ddlRegion.DataValueField = "Country";
            ddlRegion.DataBind();

        }
        catch (Exception ex)
        {
        }
    }

  [WebMethod]
  public static string[] GetCustomers(string prefix, string dbname, string region, string transstatus, string clstatus)
  {
        List<string> customers = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = " Exec BPStatus_GetCustomerForAutoSearchNew '" + prefix + "','" + dbname + "','" + region + "'";

        if (!string.IsNullOrEmpty(transstatus) && transstatus != "--Select--")
            qry = qry + ",'" + transstatus + "'";
        else
            qry = qry + ",''";

        if (!string.IsNullOrEmpty(clstatus) && clstatus != "--Select--")
            qry = qry + ",'" + clstatus + "'";
        else
            qry = qry + ",''";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            customers.Add(string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}#{10}#{11}#{12}#{13}#{14}#{15}#{16}#{17}", rdr["CardName"], rdr["CardCode"], rdr["TransStatus"], rdr["TRemark"], rdr["CLStatus"], rdr["CLRemark"], rdr["AcctBalance"], rdr["Softhold"], rdr["Hardhold"], rdr["Blocked"], rdr["PayMethod"], rdr["CreditLimit"], rdr["CLExpiryDate"], rdr["CLUpdateDate"], rdr["CLExpiryExtension"], rdr["tempCL"], rdr["tempCLExpiryDate"], rdr["tempCLRemark"]));
        }

        return customers.ToArray();
    }


  //[WebMethod]
  //public static List<Customers> GetCustomerList(string prefix, string dbname, string region, string transstatus, string clstatus)
  //{
  //    List<Customers> customers = new List<Customers>();

  //    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

  //    string qry = " Exec BPStatus_GetCustomerForAutoSearch '" + prefix + "','" + dbname + "','" + region + "'";

  //    if (!string.IsNullOrEmpty(transstatus) && transstatus != "--Select--")
  //        qry = qry + ",'" + transstatus + "'";
  //    else
  //        qry = qry + ",''";

  //    if (!string.IsNullOrEmpty(clstatus) && clstatus != "--Select--")
  //        qry = qry + ",'" + clstatus + "'";
  //    else
  //        qry = qry + ",''";

  //    SqlDataReader rdr = Db.myGetReader(qry);
  //    while (rdr.Read())
  //    {
  //        customers.Add(new Customers
  //        {
  //            cardcode = rdr["CardCode"].ToString(),
  //            cardname = rdr["CardName"].ToString(),
  //            TransStatus = rdr["TransStatus"].ToString(),
  //            TransRemarks = rdr["TRemark"].ToString(),
  //            CLStatus = rdr["CLStatus"].ToString(),
  //            CLRemarks = rdr["CLRemark"].ToString(),
  //            AccountBalance = Convert.ToDouble(rdr["AcctBalance"]),
  //        });
  //        //customers.Add(string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", rdr["CardName"], rdr["CardCode"], rdr["TransStatus"], rdr["TRemark"], rdr["CLStatus"], rdr["CLRemark"], rdr["AcctBalance"]));
  //    }

  //    return customers;
  //}

  [WebMethod]
  public static TCLStatus[] GetTransStatus(string dbname, string region)
  {
      List<TCLStatus> stsList = new List<TCLStatus>();
      try
      {
          DataSet DS = Db.myGetDS(" EXEC BPStatus_GetToStatusToBindDDL '" + dbname + "','" + region + "','" + myGlobal.loggedInUser() + "' ");
          if (DS.Tables.Count > 0)
          {
              foreach (DataRow dtrow in DS.Tables[0].Rows)
              {
                  TCLStatus sts = new TCLStatus();
                  sts.status = dtrow["ToTransStatus"].ToString();
                  stsList.Add(sts);
              }
          }
      }
      catch { }

      return stsList.ToArray();
  }

  [WebMethod]
  public static TCLStatus[] GetCLStatus(string dbname, string region)
  {
      List<TCLStatus> stsList = new List<TCLStatus>();
      try
      {
          DataSet DS = Db.myGetDS(" EXEC BPStatus_GetToStatusToBindDDL '" + dbname + "','" + region + "','" + myGlobal.loggedInUser() + "' ");

          if (DS.Tables.Count > 1)
          {
              foreach (DataRow dtrow in DS.Tables[1].Rows)
              {
                  TCLStatus sts = new TCLStatus();
                  sts.status = dtrow["ToCLStatus"].ToString();
                  stsList.Add(sts);
              }
          }
      }
      catch { }

      return stsList.ToArray();
  }

  [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public static string UpdateCLStatus(SAPStatus obj)
  {
      string ReturnMsg = "1";
      try
      {
          Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
          string DBName = obj.dbname;
         // DBName = "SAPKE-TEST";
          string sql = @"select SAPUserName, case '" + DBName + @"' when 'SAPTZ' then SAPTZPassword
                                                                when 'SAPTZ-TEST' then SAPTZPassword 
									    when 'SAPKE' then SAPKEPassword 
                                        when 'SAPKE-TEST' then SAPKEPassword 
									    when 'SAPAE' then SAPAEPassword 
                                        when 'SAPAE-TEST' then SAPAEPassword 
									    when 'SAPUG' then SAPUGPassword
                                        when 'SAPUG-TEST' then SAPUGPassword 
									    when 'SAPZM' then SAPZMPassword 
                                        when 'SAPZM-TEST' then SAPZMPassword 
									    end SAPPassword
                from dbo.BPStatus_SAP_StaffUserMapping 
                where staffusername='" + myGlobal.loggedInUser() + "'";

          string SAPUserName = "", SAPPassword = "";
          SqlDataReader rdr = Db.myGetReader(sql);
          if (rdr.HasRows)
          {
              while (rdr.Read())
              {
                  SAPUserName = rdr["SAPUserName"].ToString();
                  SAPPassword = rdr["SAPPassword"].ToString();
              }
          }

          if (!string.IsNullOrEmpty(SAPUserName) && !string.IsNullOrEmpty(SAPPassword))
          {
              if (DBName == "SAPAE")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsAE");
                  if (oCompanyAE == null)
                      ConnectSAPDB(ref oCompanyAE, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyAE != null && oCompanyAE.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyAE, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyAE != null)
                  {
                      ReturnMsg = UpdateCLStatusInSAP(oCompanyAE, obj);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPAE using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPKE" )
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsKE");
                  if (oCompanyKE == null)
                      ConnectSAPDB(ref oCompanyKE, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyKE != null && oCompanyKE.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyKE, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyKE != null)
                  {
                      ReturnMsg = UpdateCLStatusInSAP(oCompanyKE, obj);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPKE using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPTZ")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsTZ");
                  if (oCompanyTZ == null)
                      ConnectSAPDB(ref oCompanyTZ, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyTZ != null && oCompanyTZ.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyTZ, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyTZ != null)
                  {
                      ReturnMsg = UpdateCLStatusInSAP(oCompanyTZ, obj);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPTZ using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPUG")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsUG");
                  if (oCompanyUG == null)
                      ConnectSAPDB(ref oCompanyUG, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyUG != null && oCompanyUG.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyUG, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyUG != null)
                  {
                      ReturnMsg = UpdateCLStatusInSAP(oCompanyUG, obj);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPUG using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPZM")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsZM");
                  if (oCompanyZM == null)
                      ConnectSAPDB(ref oCompanyZM, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyZM != null && oCompanyZM.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyTZ, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyZM != null)
                  {
                      ReturnMsg = UpdateCLStatusInSAP(oCompanyZM, obj);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPZM using DI API, Please try again";
                  }
              }
          }
          else
          {
              ReturnMsg = "Kindly map SAP credentials with logged in user.";
          }
      }
      catch (Exception E)
      {
          ReturnMsg = "Exception occured : " + E.Message;
      }
      return ReturnMsg;

  }

  private static string UpdateCLStatusInSAP(SAPbobsCOM.Company oCmpny, SAPStatus obj)
  {
      string returnMsg = "";
      try
      {
          SAPbobsCOM.BusinessPartners oBP = (SAPbobsCOM.BusinessPartners)oCmpny.GetBusinessObject(BoObjectTypes.oBusinessPartners);
          if (oBP.GetByKey(obj.cardcode))
          {
              if (obj.clstatus != "--select--") ///  to change the CL Status
              {
                  oBP.UserFields.Fields.Item("U_CLStatus").Value = obj.clstatus.Substring(0, 1);
                  oBP.UserFields.Fields.Item("U_CLStatusRemark").Value = obj.clstatusremarks;
              }

              if (obj.tempcl > 0)
              {
                  oBP.UserFields.Fields.Item("U_temp_cl").Value = obj.tempcl;
                  oBP.UserFields.Fields.Item("U_temp_expdate").Value = obj.tempclexpirydate;
                  oBP.UserFields.Fields.Item("U_tempCLRemark").Value = obj.tempclremarks;
              }
              else
              {
                  // To Reset Temp Limit Fields
                  oBP.UserFields.Fields.Item("U_temp_cl").SetNullValue();//  .Value = DBNull.Value;
                  oBP.UserFields.Fields.Item("U_temp_expdate").SetNullValue(); //.Value = DBNull.Value;
                  oBP.UserFields.Fields.Item("U_temp_CL_Used").SetNullValue(); //.Value = DBNull.Value;
                  oBP.UserFields.Fields.Item("U_temp_CL_BAL").SetNullValue(); //.Value = DBNull.Value;
                  oBP.UserFields.Fields.Item("U_tempCLRemark").SetNullValue();
              }

              if (obj.creditlimit > 0)
              {
                  oBP.CreditLimit = obj.creditlimit;
                  oBP.MaxCommitment = obj.creditlimit;
                  oBP.UserFields.Fields.Item("U_cl_expiry").Value = obj.clexpirydate;
                  oBP.UserFields.Fields.Item("U_CLUpdateDate").Value = obj.clupdatedate;
                  if(obj.cLexpiryextension!="--select--")
                  {
                      oBP.UserFields.Fields.Item("U_CLExpiryExntension").Value = obj.cLexpiryextension;
                  }
              }
              int retValue = oBP.Update();
              if (retValue == 0)
              {
                  returnMsg = "1";
              }
              else
              {
                  returnMsg = oCmpny.GetLastErrorDescription();
                  // oCmpny.GetLastError(errCod, errMsg);
              }
          }
      }
      catch (Exception ex)
      {
          returnMsg = " Exception occured : " + obj.cardcode+ " : " + ex.Message;
      }
      return returnMsg;

  }
  //[WebMethod]
  [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)] 
  public static string UpdateTransStatus(SAPStatus obj)
  {
      string ReturnMsg = "0 - Unable to connect";
      try
      {
          Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
          string DBName = obj.dbname;
          //DBName = "SAPKE-TEST";  /// REMOVE THIS BEFORE DEPLOTING IT TO LIVE 
          string sql = @"select SAPUserName, case '" + DBName + @"' when 'SAPTZ' then SAPTZPassword
                                                                when 'SAPTZ-TEST' then SAPTZPassword 
									    when 'SAPKE' then SAPKEPassword 
                                        when 'SAPKE-TEST' then SAPKEPassword 
									    when 'SAPAE' then SAPAEPassword 
                                        when 'SAPAE-TEST' then SAPAEPassword 
									    when 'SAPUG' then SAPUGPassword
                                        when 'SAPUG-TEST' then SAPUGPassword 
									    when 'SAPZM' then SAPZMPassword 
                                        when 'SAPZM-TEST' then SAPZMPassword 
									    end SAPPassword
                from dbo.BPStatus_SAP_StaffUserMapping 
                where staffusername='" + myGlobal.loggedInUser() + "'";

          string SAPUserName = "", SAPPassword = "";
          SqlDataReader rdr = Db.myGetReader(sql);
          if (rdr.HasRows)
          {
              while (rdr.Read())
              {
                  SAPUserName = rdr["SAPUserName"].ToString();
                  SAPPassword = rdr["SAPPassword"].ToString();
              }
          }

          if (!string.IsNullOrEmpty(SAPUserName) && !string.IsNullOrEmpty(SAPPassword))
          {
              if (DBName == "SAPAE")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsAE");
                  if (oCompanyAE == null)
                      ConnectSAPDB(ref oCompanyAE, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyAE != null && oCompanyAE.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyAE, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyAE != null)
                  {
                      ReturnMsg = UpdateTransStatusInSAP(oCompanyAE, obj.cardcode, obj.tstatus, obj.tstatusremarks);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPAE using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPKE" )
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsKE");
                  if (oCompanyKE == null)
                      ConnectSAPDB(ref oCompanyKE, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyKE != null && oCompanyKE.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyKE, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyKE != null)
                  {
                      ReturnMsg = UpdateTransStatusInSAP(oCompanyKE, obj.cardcode, obj.tstatus, obj.tstatusremarks);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPKE using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPTZ")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsTZ");
                  if (oCompanyTZ == null)
                      ConnectSAPDB(ref oCompanyTZ, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyTZ != null && oCompanyTZ.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyTZ, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyTZ != null)
                  {
                      ReturnMsg = UpdateTransStatusInSAP(oCompanyTZ, obj.cardcode, obj.tstatus, obj.tstatusremarks);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPTZ using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPUG")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsUG");
                  if (oCompanyUG == null)
                      ConnectSAPDB(ref oCompanyUG, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyUG != null && oCompanyUG.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyUG, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyUG != null)
                  {
                      ReturnMsg = UpdateTransStatusInSAP(oCompanyUG, obj.cardcode, obj.tstatus, obj.tstatusremarks);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPUG using DI API, Please try again";
                  }
              }
              else if (DBName == "SAPZM" || DBName=="SAPZM-TEST")
              {
                  string SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsZM");
                  SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsZM-TEST");
                  
                  if (oCompanyZM == null)
                      ConnectSAPDB(ref oCompanyZM, SAPconstring, SAPUserName, SAPPassword);
                  else if (oCompanyZM != null && oCompanyZM.CompanyDB != DBName)
                      ConnectSAPDB(ref oCompanyZM, SAPconstring, SAPUserName, SAPPassword);

                  if (oCompanyZM != null)
                  {
                      ReturnMsg = UpdateTransStatusInSAP(oCompanyZM, obj.cardcode, obj.tstatus, obj.tstatusremarks);
                  }
                  else
                  {
                      ReturnMsg = "Unable to connect to SAPZM using DI API, Please try again";
                  }
              }
          }
          else
          {
              ReturnMsg = "Kindly map SAP credentials with logged in user.";
          }

      }
      catch (Exception E)
      {
          ReturnMsg = "Exception occured : " + E.Message;
      }

      return ReturnMsg;
  }

  public static string UpdateTransStatusInSAP(SAPbobsCOM.Company oCmpny, string CardCode, string NewTransStatus, string Remarks)
  {
      string returnMsg = "";
      try
      {
          SAPbobsCOM.BusinessPartners oBP = (SAPbobsCOM.BusinessPartners)oCmpny.GetBusinessObject(BoObjectTypes.oBusinessPartners);
          if (oBP.GetByKey(CardCode))
          {
              oBP.UserFields.Fields.Item("U_status").Value = NewTransStatus.Substring(0, 1);
              oBP.UserFields.Fields.Item("U_StatusRemark").Value = Remarks;
              int retValue = oBP.Update();
              if (retValue == 0)
              {
                  returnMsg = "1";
              }
              else
              {
                  returnMsg = oCmpny.GetLastErrorDescription();
                 // oCmpny.GetLastError(errCod, errMsg);
              }
          }
      }
      catch (Exception ex)
      {
          returnMsg = " Exception occured : " + CardCode + " : " + ex.Message;
      }
      return returnMsg;
  }

  /// <summary>
  ///  This function is for making connection to SAP database using DI API
  /// </summary>
  /// <param name="oCompany"> company object </param>
  /// <param name="conn"> connection parameters </param>
  /// <param name="DB"> DB Name </param>
  public static void ConnectSAPDB(ref SAPbobsCOM.Company oCompany, string conn, string SAPUserName,string SAPPassword)
  {
      
      try
      {
          connFlag = true;    
          string[] connElements = conn.Split(';');
          int lRetCode;

          oCompany = new SAPbobsCOM.Company();
          oCompany.Server = connElements[0];
          oCompany.DbUserName = connElements[1];
          oCompany.DbPassword = connElements[2];
          oCompany.CompanyDB = connElements[3];

          oCompany.UserName = SAPUserName; //connElements[4];
          oCompany.Password = SAPPassword; // connElements[5];
          oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
          oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
          oCompany.UseTrusted = false;
          oCompany.LicenseServer = connElements[6];

          lRetCode = oCompany.Connect();
          oCompanyService = oCompany.GetCompanyService();
          //oSBObob = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);
          if (lRetCode == 0)
          {
              connFlag = true;
          }
          else
          {
              connFlag = false;
          }
      }
      catch (Exception ex)
      {
          connFlag = false;
      }
  }

  //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
  //{
  //    try
  //    {
  //        Db.constr=myGlobal.getAppSettingsDataForKey("tejSAP");
  //        DataSet DS = Db.myGetDS(" EXEC BPStatus_GetToStatusToBindDDL '"+ddlDBName.SelectedItem.Text+"','"+ddlRegion.SelectedItem.Text+"','"+myGlobal.loggedInUser()+"' ");
  //        if (DS.Tables.Count > 0)
  //        {
  //            ddlNewTStatus.DataSource = DS.Tables[0];
  //            ddlNewTStatus.DataTextField = "ToTransStatus";
  //            ddlNewTStatus.DataValueField = "ToTransStatus";
  //            ddlNewTStatus.DataBind();

  //            if (DS.Tables.Count > 1)
  //            {
  //                ddlNewCLStatus.DataSource = DS.Tables[1];
  //                ddlNewCLStatus.DataTextField = "ToCLStatus";
  //                ddlNewCLStatus.DataValueField = "ToCLStatus";
  //                ddlNewCLStatus.DataBind();
  //            }
  //        }
  //    }
  //    catch (Exception)
  //    {
  //        throw;
  //    }
  //}

}


//public class Customers
//{
//    public string cardcode { get; set; }
//    public string cardname { get; set; }
//    public string TransStatus { get; set; }
//    public string TransRemarks { get; set; }
//    public string CLStatus { get; set; }
//    public string CLRemarks { get; set; }
//    public double AccountBalance { get; set; }
//}



public class TCLStatus
{
    public string status { get; set; }
}

public class SAPStatus
{
    public string dbname { get; set; }
    public string cardcode { get; set; }
    public string tstatus { get; set; }
    public string tstatusremarks { get; set; }
    public string clstatus { get; set; }
    public string clstatusremarks { get; set; }
    public double creditlimit { get; set; }
    public DateTime clexpirydate { get; set; }
    public DateTime clupdatedate { get; set; }
    public string cLexpiryextension { get; set; }
    public double tempcl { get; set; }
    public DateTime tempclexpirydate { get; set; }
    public string tempclremarks { get; set; }
}