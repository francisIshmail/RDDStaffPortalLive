using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Text;


public partial class IntranetNew_DailySalesReport_CardCodeReport : System.Web.UI.Page
{
    StringBuilder htmlTable = new StringBuilder();  
    SqlDataAdapter da;  
        DataSet ds = new DataSet();  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)  // IsPostBack Start
        {
         
        }
    }

    protected void fillgv()
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();
        DataSet ds2 = Db.myGetDS("exec SalesPersonScoreCard3 '" + txtstartdate.Text + "' ,'" + LoggedInUserName + "'");

        if (ds2.Tables.Count > 0)
        {
           
            DataSet ds3 = Db.myGetDS("select * from demo2");
            {
                if (ds3.Tables.Count > 0)
                {
                    htmlTable.Append("<table border='1' align='center' >");
                    htmlTable.Append("<tr style='background-color:Chartreuse ; color: Black'><th>Perticular.</th><th>DistinctCount.</th><th>Points.</th></tr>");

                    if (!object.Equals(ds3.Tables[0], null))
                    {
                        if (ds3.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                            {
                                htmlTable.Append("<tr style='color: White;Bordercolor:Black;'>");
                                htmlTable.Append("<td  style='background-color:OliveDrab; color: Black;'>" + ds3.Tables[0].Rows[i]["perticular"] + "</td>");
                                htmlTable.Append("<td style='color: Black ;'>" + ds3.Tables[0].Rows[i]["Points"] + "</td>");
                                htmlTable.Append("<td style='color: Black ;'>" + ds3.Tables[0].Rows[i]["distint_count"] + "</td>");
                              
                                htmlTable.Append("</tr>");
                            }
                            htmlTable.Append("</table>");
                           DBDataPlaceHolder.Controls.Add(new Literal { Text = htmlTable.ToString() });
                        }
                      
                    }
                    else
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td align='center' colspan='4'>There is no Record.</td>");
                        htmlTable.Append("</tr>");
                    }  
                }
            }
        }
    }
    protected void txtstartdate_TextChanged(object sender, EventArgs e)
    {
        fillgv();
    }
}