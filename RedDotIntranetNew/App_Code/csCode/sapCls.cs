using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for sapCls
/// </summary>
public static class sapCls
{

    public static string AllCurrencies="##";

    public static string LocalCurrency;
    public static string SystemCurrency;
    public static string VATOUT;
    public static string VATIN;
    public static string EXEMPTOUT;
    public static string EXEMPTIN;
    public static string EVODB;
    public static string SAPDB;
    public static string MASTER_WAREHOUSE;
    public static string DBCode; 

    public static double FixedExchangeRate;

    public static void initGlobals(string pcntrycode)
    {
        string ret = "";
     
         pcntrycode=pcntrycode.ToUpper();  //gets in uppercase

         switch (pcntrycode)
        {
            case "AE":    //DU
                SAPDB = "sapAE";
                DBCode = "AE";
                LocalCurrency = "AED";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "AESAE01";
                VATOUT = "FZE-VO";
                VATIN = "FZE-VI";
                EVODB = "Triangle";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "triangle";
                FixedExchangeRate = 3.67;
                break;
           case "UG":
                SAPDB = "sapUG";
                DBCode = "UG";
                LocalCurrency = "UGX";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "UGSUG01";
                VATOUT = "UGVATOUT";
                VATIN = "UGVATIN";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "UgandaKE";
                FixedExchangeRate = 0.0004;
                break;
           ////case "TZ":  //temp later update below and use below commented and remove this for live
           ////     SAPDB = "sapTZ";
           ////     DBCode = "TZ";
           ////     LocalCurrency = "UGX";
           ////     SystemCurrency = "USD";
           ////     MASTER_WAREHOUSE = "UGSUG01";
           ////     VATOUT = "UGVATOUT";
           ////     VATIN = "UGVATIN";
           ////     EXEMPTOUT = "XO";
           ////     EXEMPTIN = "XI";
           ////     EVODB = "UgandaKE";
           ////     FixedExchangeRate = 0.0004;
           ////     break;
           ////case "KE":  //temp later update below and use below commented and remove this for live
           ////     SAPDB = "sapKE";
           ////     DBCode = "KE";
           ////     LocalCurrency = "UGX";
           ////     SystemCurrency = "USD";
           ////     MASTER_WAREHOUSE = "UGSUG01";
           ////     VATOUT = "UGVATOUT";
           ////     VATIN = "UGVATIN";
           ////     EXEMPTOUT = "XO";
           ////     EXEMPTIN = "XI";
           ////     EVODB = "UgandaKE";
           ////     FixedExchangeRate = 0.0004;
           ////     break;
           case "TZ":
                SAPDB = "sapTZ";
                DBCode = "TZ";
                LocalCurrency = "TZS";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "TZGTZ01";
                VATOUT = "TZVATOUT";
                VATIN = "TZVATIN";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "RedDotTanzania";
                FixedExchangeRate = 1 / 1650;
                break;
           case "KE":
                SAPDB = "sapKE";
                DBCode = "KE";
                LocalCurrency = "KES";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "KEGKE01";
                VATOUT = "KEVATOUT";
                VATIN = "KEVATIN";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "Red Dot Distribution Limited - Kenya";
                FixedExchangeRate = 1 / 88;
                break;
           case "EPZ":
               SAPDB = "sapEPZ";
               DBCode = "EPZ"; 
               LocalCurrency = "KES";
               SystemCurrency = "USD";
               MASTER_WAREHOUSE = "Mstr";
               VATOUT = "EPZVATOUT";
               VATIN = "EPZVATIN";
               EXEMPTOUT = "XO";
               EXEMPTIN = "XI";
               EVODB = "RED DOT DISTRIBUTION EPZ LTD";
               FixedExchangeRate = 1 / 88;
                break;
           case "ZM":
                SAPDB = "sapZM";
                DBCode = "ZM";
                LocalCurrency = "ZMK";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "ZMGZM01";
                VATOUT = "VO16";
                VATIN = "VI16";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "Red Dot Distribution Limited - Kenya";
                FixedExchangeRate = 1 / 10;
                break;
           case "MA":
                SAPDB = "sapML";
                DBCode = "MA";
                LocalCurrency = "MWK";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "MWGMW01";
                VATOUT = "O1";
                VATIN = "VI16";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "Red Dot Distribution Limited - Kenya";
                FixedExchangeRate = 1 / 10;
                break;
           case "TRI":    //DU
                SAPDB = "sapTRI";
                DBCode = "TRI";
                LocalCurrency = "AED";
                SystemCurrency = "USD";
                MASTER_WAREHOUSE = "AESAE01";
                VATOUT = "FZE-VO";
                VATIN = "FZE-VI";
                EVODB = "Triangle";
                EXEMPTOUT = "XO";
                EXEMPTIN = "XI";
                EVODB = "triangle";
                FixedExchangeRate = 3.67;
                break;
           default:
                break;
        }
    }

}

//Select Case Country
//            Case "FZE"
//                LocalCurrency = "AED"
//                SystemCurrency = "USD"
//                VATOUT = "FZE-VO"
//                VATIN = "FZE-VI"
//                EXEMPTOUT = "XO"
//                EXEMPTIN = "XI"
//                EVODB = "Triangle"
//                SAPDB = "TEJEVO2SAP"
//                MASTER_WAREHOUSE = "Mstr"
//                DBCode = "FZE"
//            Case "KE"
//                LocalCurrency = "KES"
//                SystemCurrency = "USD"
//                FixedExchangeRate = 1 / 88
//                VATOUT = "KEVATOUT"
//                VATIN = "KEVATIN"
//                EXEMPTOUT = "XO"
//                EXEMPTIN = "XI"
//                EVODB = "Red Dot Distribution Limited - Kenya"
//                SAPDB = "TEJEVO2SAP"
//                MASTER_WAREHOUSE = "RDDKE"
//                DBCode = "KE"
//            Case "EPZ"
//                LocalCurrency = "KES"
//                SystemCurrency = "USD"
//            Case "UG"
//                LocalCurrency = "UGX"
//                SystemCurrency = "USD"
//                FixedExchangeRate = 0.0004
//                VATOUT = "UGVATOUT"
//                VATIN = "UGVATIN"
//                EXEMPTOUT = "XO"
//                EXEMPTIN = "XI"
//                EVODB = "UgandaKE01DEC2014"
//                SAPDB = "TEJEVO2SAP"
//                MASTER_WAREHOUSE = "RDDUG"
//                DBCode = "UG"
//            Case "TZ"
//                LocalCurrency = "TZS"
//                SystemCurrency = "USD"
//        End Select