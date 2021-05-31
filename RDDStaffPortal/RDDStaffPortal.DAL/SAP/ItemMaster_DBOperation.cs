using RDDStaffPortal.DAL.DataModels.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.SAP
{
    public class ItemMaster_DBOperation
    {
        CommonFunction Com = new CommonFunction();
        public static string sCode = "", sIPAdress = "", sServerName = "", sDBName1 = "", sDBPassword = "", sDBUserName = "", sB1Password = "", sB1UserName = "";
        public static string SAPServer = "", LICServer = "";
        public static SAPbobsCOM.Company mCompany, oCompanyAE, oCompanyUG, oCompanyTZ, oCompanyKE, oCompanyZM, oCompanyML, oCompanyTRI;
        public static bool ConnectToSAP(ref SAPbobsCOM.Company _oCompany, string sDBName)
        {
            try
            {
                string SAPconstring = string.Empty;

                if (sDBName == "SAPAE")
                {
                    SAPconstring = Global.getAppSettingsDataForKey("SAPCompanyConnectCredsSAPAE");
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

                    _oCompany = new SAPbobsCOM.Company();

                    _oCompany.UseTrusted = false;
                    _oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                    _oCompany.Server = SAPServer;
                    _oCompany.LicenseServer = LICServer;
                    _oCompany.CompanyDB = sDBName1;
                    _oCompany.UserName = sB1UserName;
                    _oCompany.Password = sB1Password;
                    _oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                    _oCompany.DbUserName = sDBUserName;
                    _oCompany.DbPassword = sDBPassword;


                    int iErrCode = 0;
                    int iCounter = 0;

                    do
                    {

                        iErrCode = _oCompany.Connect();
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
                    while (_oCompany.Connected == false);

                    if (iErrCode != 0)
                    {
                        string strErr;
                        _oCompany.GetLastError(out iErrCode, out strErr);
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

        public DataSet Connet_To_SAPDB(string dbname)
        {
            DataSet result_ds = new DataSet();
            DataTable t1 = new DataTable("table");
            t1.Columns.Add("Result");
            t1.Columns.Add("Message");

            try
            {
                int ErrorCode;
                string ErrMessage;

                string[] DB_NameList = dbname.Split(';');
               
                for (int i = 0; i < DB_NameList.Length; i++)
                {
                    if (DB_NameList[i] == "SAPAE")
                    {
                        if (ConnectToSAP(ref oCompanyAE, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPKE")
                    {
                        if (ConnectToSAP(ref oCompanyKE, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyKE.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPTZ")
                    {
                        if (ConnectToSAP(ref oCompanyTZ, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyTZ.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPUG")
                    {
                        if (ConnectToSAP(ref oCompanyUG, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyUG.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPZM")
                    {
                        if (ConnectToSAP(ref oCompanyZM, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyZM.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPML")
                    {
                        if (ConnectToSAP(ref oCompanyML, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            if (oCompanyML == null)
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode- DB not connected");
                            else
                            {
                                oCompanyML.GetLastError(out ErrorCode, out ErrMessage);
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            }
                        }
                    }
                    else if (DB_NameList[i] == "SAPTRI")
                    {
                        if (ConnectToSAP(ref oCompanyTRI, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            if (oCompanyTRI == null)
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode- DB not connected");
                            else
                            {
                                oCompanyTRI.GetLastError(out ErrorCode, out ErrMessage);
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            }
                        }
                    }

                }
                result_ds.Tables.Add(t1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result_ds;
        }

        public DataSet Get_BindDDLList(string Type, string Value)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("EXEC RDD_Part_Get_ComboxList '" + Type + "','" + Value + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Part_ToAddNew_Value(string insfor, string value, string descr, string type)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("EXEC RDD_Part_ToAddNew_Value '" + insfor + "','" + value + "','" + descr + "'," + type);

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Add_ItemToSAPDB(string DBList, string itmCode, string itmDesc, int mfrId, int itmGrpId, string itmGrpCode, string itmBU, string itmProductCategory, string itmPL, string itmProductGrp, double Lenght, double Width, double Height, double Weight,string HSCode)
        {
            DataSet result_ds = new DataSet();
            DataTable t1 = new DataTable("table");

            t1.Columns.Add("Result");
            t1.Columns.Add("Message");

            try
            {
                int ErrorCode;
                string ErrMessage;
                SAPbobsCOM.Items oItem;

                string[] DB_NameList = DBList.Split(';');

                for (int i = 0; i < DB_NameList.Length; i++)
                {
                    if (DB_NameList[i] == "SAPAE")
                        mCompany = oCompanyAE;
                    else if (DB_NameList[i] == "SAPKE")
                        mCompany = oCompanyKE;
                    else if (DB_NameList[i] == "SAPTZ")
                        mCompany = oCompanyTZ;
                    else if (DB_NameList[i] == "SAPUG")
                        mCompany = oCompanyUG;
                    else if (DB_NameList[i] == "SAPZM")
                        mCompany = oCompanyZM;
                    else if (DB_NameList[i] == "SAPML")
                        mCompany = oCompanyML;
                    else if (DB_NameList[i] == "SAPTRI")
                        mCompany = oCompanyTRI;

                    if (mCompany.Connected == true)
                    {
                        SAPCls.initGlobals(DB_NameList[i]);
                        oItem = (SAPbobsCOM.Items)mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

                        oItem.ItemCode = itmCode;
                        oItem.ItemName = itmDesc;
                        oItem.ItemsGroupCode = itmGrpId; //sends group id

                        oItem.UserFields.Fields.Item("FirmCode").Value = mfrId.ToString();  // this is group code
                        oItem.UserFields.Fields.Item("U_BU").Value = itmGrpCode;  // this is group code
                        oItem.UserFields.Fields.Item("U_Model").Value = "NA";

                        oItem.UserFields.Fields.Item("U_Category").Value = itmProductCategory;   //this is product category
                        oItem.UserFields.Fields.Item("U_ProdLine").Value = itmPL;  //this is product Line
                        oItem.UserFields.Fields.Item("U_DashboardCategory").Value = itmProductGrp; //this is product Group
                        oItem.UserFields.Fields.Item("U_HSCode").Value = HSCode; //For HS Code

                        double _Lenght, _Width, _Height, _Weight;
                        _Lenght = Lenght / 100;
                        _Width = Width / 100;
                        _Height = Height / 100;
                        //Volume = Lenght * Width * Height

                        oItem.PurchaseUnitLength = Lenght / 100;// length
                        oItem.PurchaseUnitWidth = Width / 100;  //Width
                        oItem.PurchaseUnitHeight = Height / 100;  //Height

                        // oItem.PurchaseUnitVolume = Volume


                        oItem.PurchaseUnitWeight = (Weight * 1000); //Weight

                        oItem.SalesVATGroup = SAPCls.VATOUT;
                        oItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass;
                        oItem.DefaultWarehouse = SAPCls.MASTER_WAREHOUSE;

                        int Result = oItem.Add();
                        if (Result != 0)
                        {
                            mCompany.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", "Error : DB-[" + DB_NameList[i] + "] Failed to add item:[" + itmCode + "] " + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                        else
                        {
                            t1.Rows.Add("True", "DB -[" + DB_NameList[i] + "] Successfuly Add Item :[" + itmCode + "]");
                        }
                    }
                    else
                    {
                        mCompany.GetLastError(out ErrorCode, out ErrMessage);
                        t1.Rows.Add("False", "Error : DB-[" + DB_NameList[i] + "] Failed to Connect : " + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                    }
                }
                result_ds.Tables.Add(t1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result_ds;
        }

    }
}
