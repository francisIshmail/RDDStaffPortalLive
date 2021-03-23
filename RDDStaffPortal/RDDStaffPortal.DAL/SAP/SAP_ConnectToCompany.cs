using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels.SAP;

namespace RDDStaffPortal.DAL.SAP
{
   public class SAP_ConnectToCompany
    {
        public static string sCode = "", sIPAdress = "", sServerName = "", sDBName1 = "", sDBPassword = "", sDBUserName = "", sB1Password = "", sB1UserName = "";
        public static string SAPServer = "", LICServer = "";
        public static SAPbobsCOM.Company mCompany;
        public static bool ConnectToSAP(string sDBName)
        {
            try
            {
                string SAPconstring = string.Empty;

                if (sDBName == "SAPAE")
                {
                    SAPconstring =Global.getAppSettingsDataForKey("SAPCompanyConnectCredsSAPAE");
                }
                else if (sDBName == "SAPKE")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsKE");
                }
                else if (sDBName == "SAPTZ")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsTZ");
                }
                else if (sDBName == "SAPUG")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsUG");
                }
                else if (sDBName == "SAPZM")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsZM");
                }
                else if (sDBName == "SAPML")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsML");
                }
                else if (sDBName == "SAPTRI")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsTRI");
                }
                if (string.IsNullOrEmpty(SAPconstring) == false)
                {
                    string[] connElements = SAPconstring.Split(';');

                    SAPServer = connElements[0];
                    sDBUserName = connElements[1];
                    sDBPassword = connElements[2];
                    sDBName1 = connElements[3];
                    LICServer = connElements[6]; //"192.168.56.131:30000";// connElements[6]; ; //
                    sB1UserName = connElements[4];
                    sB1Password = connElements[5];

                    mCompany = new SAPbobsCOM.Company();

                    mCompany.UseTrusted = false;
                    mCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                    mCompany.Server = SAPServer;
                    mCompany.LicenseServer = LICServer;
                    mCompany.CompanyDB = sDBName1;
                    mCompany.UserName = sB1UserName;
                    mCompany.Password = sB1Password;
                    mCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                    mCompany.DbUserName = sDBUserName;
                    mCompany.DbPassword = sDBPassword;


                    int iErrCode = 0;
                    int iCounter = 0;

                    do
                    {

                        iErrCode = mCompany.Connect();
                        if (iErrCode != 0)
                        {
                            iCounter = iCounter + 1;
                            if (iCounter > 10)  
                            {
                                break;
                            }
                            System.Threading.Thread.Sleep(iCounter * 50);
                            GC.Collect();
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(iCounter * 50);
                            GC.Collect();

                            break;
                        }
                    }
                    while (mCompany.Connected == false);

                    if (iErrCode != 0)
                    {
                        string strErr;
                        mCompany.GetLastError(out iErrCode, out strErr);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
