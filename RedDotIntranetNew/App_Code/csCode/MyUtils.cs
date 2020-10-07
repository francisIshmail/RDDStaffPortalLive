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
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for MyUtills
/// </summary>
public static class Util
{
    private static String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp", ".pdf" };

    public static bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(strIn,
               @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
    }

    public static Boolean lookItemTextFiledExistence(DropDownList pddl, string pstr)
    {
        Boolean flg = false;

        if (pddl.Items.Count <= 0)
            return flg = false;
        else
        {
            for (int h = 0; h < pddl.Items.Count; h++)
            {
                if (pddl.Items[h].Text == pstr)
                {
                    flg = true;
                    break;
                }
            }
        }

        return flg;
    }
    public static bool isValidNumber(String pstr) ///Function  supports non decimal numeric values  only, negative no. are supported
    {
        bool flg = true;
        int len = 0;

        pstr = pstr.Trim();
        len = pstr.Trim().Length;

        if (len > 0)
        {
            char chr = pstr[0];
            if (chr == '-')
            {
                pstr = pstr.Substring(1, pstr.Length - 1); // Starting position is also subtracted from the length
            }

            foreach (char ch in pstr)
            {
                if (!char.IsNumber(ch))
                {
                    flg = false;
                    break;
                }
            }
        }
        else
        {
            flg = false;
        }

        return flg;
    }

    public static bool isValidDecimalNumber(String pstr) //Function supports decimal numeric values also
    {
        bool flg = true;
        int len = 0;
        int dots = 0;

        pstr = pstr.Trim();
        len = pstr.Trim().Length;

        if (len > 0)
        {
            char chr = pstr[0];
            if (chr == '-')
            {
                pstr = pstr.Substring(1, pstr.Length - 1); // Starting position is also subtracted from the length
            }

            foreach (char ch in pstr)
            {
                if (!char.IsNumber(ch) && ch != '.')
                {
                    flg = false;
                    break;
                }
                if (ch == '.')
                    dots = dots + 1;

                if (dots > 1)
                {
                    flg = false;
                    break;
                }
            }
        }
        else
        {
            flg = false;
        }

        return flg;
    }
    public static String uploadAnyFile(FileUpload FileUpload1, String folderPath)
    {
        String ret;
        try
        {
        if (FileUpload1.HasFile)
        {
            //string fpath = Server.MapPath("../Uploads/" + FileUpload1.FileName);
            string fpath = folderPath + FileUpload1.FileName;
            //Since  Server.MapPath gets path for the current files. So, ../ is used to go back one step 
            FileUpload1.SaveAs(fpath);
            ret = "File uploaded successfully";
        }
        else
        {
            ret = "Please select a file with valid file extenstion.";
        }

        return ret;
        }
        catch (Exception exp)
        {
            ret = exp.Message;
        }

        return ret;
    }
    public static String uploadImageFile(FileUpload FileUpload1, String folderPath)
    {

        String ret;
        try
        {
            if (FileUpload1.HasFile)
            {
                if (filetypecheck(FileUpload1.FileName))
                {
                    //string fpath = Server.MapPath("../Uploads/" + FileUpload1.FileName);
                    string fpath = folderPath + FileUpload1.FileName;
                    //Since  Server.MapPath gets path for the current files. So, ../ is used to go back one step 
                    FileUpload1.SaveAs(fpath);
                    ret = "File uploaded successfully";
                }
                else
                {
                    ret = "Please select a valid image file";
                }

            }
            else
            {
                ret = "Please select a file with valid file extenstion.";
            }

            return ret; 
        }
        catch (Exception exp)
        {
            ret = exp.Message;
        }

        return ret;
    }
        
    private static bool filetypecheck(string fn)
    {
        string ext = Path.GetExtension(fn);
        Boolean fileOK = false;

        for (int i = 0; i < allowedExtensions.Length; i++)
        {
            if (ext.ToLower() == allowedExtensions[i])
            {
                fileOK = true;
                break;
            }
            else
                fileOK = false;
        }

        return fileOK;
    }

    private static bool filetypecheckold(string fn)
    {
        string ext = Path.GetExtension(fn);
        switch (ext.ToLower())
        {
            case ".gif":
                return true;
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".png":
                return true;
            case ".bmp":
                return true;
            default:
                return false;
        }
    }
    public static String get_MailAddresses()
    {
        return myGlobal.getSystemConfigValue("creditReviewEmail");
        /*
        String ret;
        Db.constr = Global.ConnectionString;
        SqlDataReader drd;
        drd = Db.myGetReader("select * from sysConfig where parameterName='creditReviewEmail'");
        drd.Read();
        ret = drd["parameterValue"].ToString();
        drd.Close();
        return ret;
        */
    }
    public static String get_imagesFolderLocation()
    {
        return myGlobal.getSystemConfigValue("imagesFolderLocation");
        /*
        String ret;
        Db.constr = Global.ConnectionString;
        SqlDataReader drd;
        drd = Db.myGetReader("select * from sysConfig where parameterName='imagesFolderLocation'");
        drd.Read();
        ret = drd["parameterValue"].ToString();
        drd.Close();
        return ret;
        */
    }
    public static String get_excelFolderLocation()
    {
        return myGlobal.getSystemConfigValue("excelFolderLocation");
        /*
        String ret;
        Db.constr = Global.ConnectionString;
        SqlDataReader drd;
        drd = Db.myGetReader("select * from sysConfig where parameterName='excelFolderLocation'");
        drd.Read();
        ret = drd["parameterValue"].ToString();
        drd.Close();
        return ret;
        */
    }
    public static String get_custStatementLocation()
    {
        return myGlobal.getSystemConfigValue("custStatementLocation");
        /*
        String ret;
        Db.constr = Global.ConnectionString;
        SqlDataReader drd;
        drd = Db.myGetReader("select * from sysConfig where parameterName='custStatementLocation'");
        drd.Read();
        ret = drd["parameterValue"].ToString();
        drd.Close();
        return ret;
        */
    }

    public static bool IsValidDate1(string dateString, out DateTime? result, string[] supportedFormats) 
    {       
        #region "argument validation"       
        if (dateString == null)     
        {         
            throw new ArgumentNullException("dateString");     
        }     
        if (supportedFormats == null)     
        {
            throw new ArgumentNullException("supportedFormats");     
        }       
        #endregion       
        try    
        {
            result = DateTime.ParseExact(dateString,supportedFormats,System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None);
            return true;     
        }    
        catch(FormatException)  
        {   
            //dateString is not a valid date in any of the allowed formats 
            result = null; 
            return false;   
        }
    }

    public static bool IsValidDate(string sdate)
    {
        DateTime dt;
        bool isDate = true;
        try
        {
            dt = DateTime.Parse(sdate);
        }
        catch
        {
            isDate = false;
        }
        return isDate;
    } 

}
