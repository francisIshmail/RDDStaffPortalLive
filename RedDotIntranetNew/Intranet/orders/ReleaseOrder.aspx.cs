using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Intranet_orders_ReleaseOrder : System.Web.UI.Page
{
    int deleteRow;
    string ordType;

    protected void Page_Load(object sender, EventArgs e)
    {
        string qorderId, qtype;
        qorderId = Request.QueryString["poId"].ToString();
        qtype = Request.QueryString["ptype"].ToString();

        if (!IsPostBack)
        {
            Db.LoadDDLsWithCon(ddlvendorGlb, "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 order by cValue", "cValue", "idInvSegValue", myGlobal.getConnectionStringForDB("TZ"));
            freshDataGrid();
        }
        lblMsg.Text = "";
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        ordType = "RO"; //check here for ordre type

        TextBox tmptxtPartNo;
        TextBox tmptxtDescription;
        TextBox tmptxtQty;
        TextBox tmptxtComments;

        clsOrders objQt = new clsOrders();
        int orderIDLatest = 0;
        string msg = "";
        string qsent = "No Order could be requested ";
        int cnt=0;
        
        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmptxtPartNo = (rw.FindControl("txtPartNo") as TextBox);
            tmptxtDescription = (rw.FindControl("txtDescription") as TextBox);
            tmptxtQty = (rw.FindControl("txtQty") as TextBox);
            tmptxtComments = (rw.FindControl("txtComments") as TextBox);

            if (tmptxtPartNo.Text.Trim() != "")
            {
                if (!isnumericval(tmptxtQty.Text) || Convert.ToInt32(tmptxtQty.Text) <= 0)
                {
                    if (msg == "")
                        msg = msg + "<br>" + ddlvendorGlb.SelectedItem.Text + ",   PartNo. : '" + tmptxtPartNo.Text + "' ,  Quantity : " + tmptxtQty.Text + "(quantity value error)<br>";
                    else
                        msg = msg + ddlvendorGlb.SelectedItem.Text + ",   PartNo. : '" + tmptxtPartNo.Text + "' ,  Quantity : " + tmptxtQty.Text + "(quantity value error)<br>";
                }
                else
                {
                    if (cnt == 0)
                    {
                        objQt.InsOrder(ddlvendorGlb.SelectedItem.Text, tmptxtComments.Text, 1, myGlobal.loggedInUser(), DateTime.Now,myGlobal.loggedInRole(),ordType);
                        Db.constr = myGlobal.getIntranetDBConnectionString();
                        orderIDLatest = Db.myExecuteScalar("select max(orderId) from dbo.orderRequest");
                    }


                    objQt.InsRO(orderIDLatest, ddlvendorGlb.SelectedItem.Text, tmptxtPartNo.Text, Convert.ToInt32(tmptxtQty.Text),5, tmptxtDescription.Text + " , " + tmptxtComments.Text, DateTime.Now, myGlobal.loggedInUser());

                    if (cnt == 0)
                    {
                        qsent = "Orders Successfully sent for approval to concerned department<br>";
                        qsent = qsent + (cnt + 1).ToString() + ". " + ddlvendorGlb.SelectedItem.Text + ",   PartNo. : " + tmptxtPartNo.Text + " ,  Quantity : " + tmptxtQty.Text + " , Description :  " + tmptxtDescription.Text + " , " + tmptxtComments.Text + "<br>";
                    }
                    else
                        qsent = qsent + (cnt + 1).ToString() + ". " + ddlvendorGlb.SelectedItem.Text + ",   PartNo. : " + tmptxtPartNo.Text + " ,  Quantity : " + tmptxtQty.Text + " , Description :  " + tmptxtDescription.Text + " , " + tmptxtComments.Text + "<br>";

                    cnt = cnt + 1;
                }
            }
         }

        //lblMsg.Text = qsent;
        if (cnt>0)
        {
            //orderIDLatest
            string sql, userRole, userEmail,orderUrl,currentUrl;
            userRole = myGlobal.loggedInRole();
            userEmail = myGlobal.membershipUserEmail(myGlobal.loggedInUser());

            sql = "select u.*,v.itmCnt from (select OD.*,OS.orderStatusName from dbo.orderRequest as OD join dbo.orderStatus as OS on OD.fk_orderStatusId=OS.orderStatusID ) as u join (select orderRequestLink,count(orderRequestLink)as itmCnt from dbo.ReleaseOrders group by orderRequestLink) as v on u.orderId=v.orderRequestLink where u.orderId=" + orderIDLatest + "";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            DataTable dtMail=new DataTable();
            dtMail = Db.myGetDS(sql).Tables[0];

            string strMessage = string.Empty;
            strMessage = "<b>Following order has been latest updated by user: " + myGlobal.loggedInUser() + "</b><br/>";
            strMessage += "<br/><b>Order Details: </b><br/>";
            strMessage += "<br/>Order Status: " + dtMail.Rows[0]["orderStatusName"].ToString();
            strMessage += "<br/>Vendor: " + dtMail.Rows[0]["Vendor"].ToString();
            strMessage += "<br/>No. of items: " + dtMail.Rows[0]["itmCnt"].ToString();
            strMessage += "<br/>User Comments: " + dtMail.Rows[0]["comments"].ToString();
            strMessage += "<br/>User Email: " + userEmail;

            currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            orderUrl = currentUrl.Substring(0, currentUrl.LastIndexOf("/")+1) + "releaseOrderDetails.aspx?oId=" + orderIDLatest;

            string message = myGlobal.sendRoleBasedMail(orderUrl, strMessage, userRole, userEmail,"","");
            Message.Show(this, message);
        }
        
        if(msg!="")
            lblMsg.Text = lblMsg.Text  + " <br> Few Items could not be ordered due to error in data supplied by requester : " + msg;

        Message.Show(this, qsent);
        freshDataGrid();

    }

    protected void btnAddRow_Click(object sender, ImageClickEventArgs e)
    {
        deleteRow = -1;
        copyDataToTableAddNewRow();
    }
    

    private void freshDataGrid()
    {
        ViewState["CurrentTable"] = GetTableAtLoadOnly();
        GridView1.DataSource = (DataTable)ViewState["CurrentTable"];
        GridView1.DataBind();
        LoadGridVendorsDDL();
    }

  
    protected void btnClearAll_Click(object sender, ImageClickEventArgs e)
    {
        freshDataGrid();
    }

    private void copyDataToTableAddNewRow()
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];

        Label tmplblSerial;
        //TextBox tmptxtManufacturer;
        TextBox tmptxtPartNo;
        TextBox tmptxtDescription;
        TextBox tmptxtQty;
        TextBox tmptxtComments;
       // DropDownList ddlPbx;


        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmptxtQty = (rw.FindControl("txtQty") as TextBox);

            if (deleteRow >= 0)
                tmptxtQty.Text = "0";

            if (!isnumericval(tmptxtQty.Text))
            {
                lblMsg.Text = "Please supply a valid numeric value for quantity field in row :" + (rw.RowIndex+1).ToString();
                return;
            }
        }

        int i = 0;
        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmplblSerial = (Label)rw.FindControl("lblSerial") as Label;
            //ddlPbx = (rw.FindControl("ddlvendor") as DropDownList);
            //tmptxtManufacturer = (rw.FindControl("txtManufacturer") as TextBox);
            tmptxtPartNo = (rw.FindControl("txtPartNo") as TextBox);
            tmptxtDescription = (rw.FindControl("txtDescription") as TextBox);
            tmptxtQty = (rw.FindControl("txtQty") as TextBox);
            tmptxtComments = (rw.FindControl("txtComments") as TextBox);

            //tmptxtManufacturer.Text= ddlPbx.SelectedItem.Text;

            dt.Rows[i]["Serial"] = Convert.ToInt32(tmplblSerial.Text);
            //dt.Rows[i]["Manufacturer"] =tmptxtManufacturer.Text;
            dt.Rows[i]["PartNo"] =tmptxtPartNo.Text;
            dt.Rows[i]["Description"] =tmptxtDescription.Text;
            dt.Rows[i]["Qty"] =Convert.ToInt32(tmptxtQty.Text);
            dt.Rows[i]["Comments"] = tmptxtComments.Text;
            
            i++;
        }

        if (deleteRow >= 0)
        {
            //delete new row
            dt.Rows.RemoveAt(deleteRow);
            deleteRow = -1;

            i = 0;
            for(i=0;i<dt.Rows.Count;i++)
            {
                dt.Rows[i]["Serial"] = (i + 1);
            }
        }
        else
        {
            //Add new row
            //dt.Rows.Add((i + 1), "", "", "", 0, "");
            dt.Rows.Add((i + 1), "", "", 0, "");
        }

        ViewState["CurrentTable"] = dt;

        GridView1.DataSource = (DataTable)ViewState["CurrentTable"];
        GridView1.DataBind();
        LoadGridVendorsDDL();
    }
    
    static DataTable GetTableAtLoadOnly()
    {
        DataTable tbl = new DataTable();
        tbl.Columns.Add("Serial", typeof(int));
        //tbl.Columns.Add("Manufacturer", typeof(string));
        tbl.Columns.Add("PartNo", typeof(string));
        tbl.Columns.Add("Description", typeof(string));
        tbl.Columns.Add("Qty", typeof(int));
        tbl.Columns.Add("Comments", typeof(string));

        int rws = 2;

        for (int i = 1; i <= rws; i++)
            tbl.Rows.Add(i, "", "", 0, "");
            //tbl.Rows.Add(i, "", "", "", 0, "");


        return tbl;
    }

    private void LoadGridVendorsDDL()
    {

        DropDownList ddltmp;
        TextBox tmptxtManufacturer;

        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmptxtManufacturer = (rw.FindControl("txtManufacturer") as TextBox);
            ddltmp = (rw.FindControl("ddlvendor") as DropDownList);
            if (ddltmp != null)
            {
                ddltmp.DataSource = ddlvendorGlb.Items;
                ddltmp.DataBind();

                if (tmptxtManufacturer != null && tmptxtManufacturer.Text.Trim() != "")
                    ddltmp.Items.FindByText(tmptxtManufacturer.Text).Selected = true;
            }
        }
    }

    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;

        foreach (GridViewRow row in GridView1.Rows)
        {

            Control ctrl = row.FindControl("imgBtnClose") as ImageButton;
            if (ctrl != null)
            {
                ImageButton btn1 = (ImageButton)ctrl;
                if (btn.ClientID == btn1.ClientID)
                {
                    deleteRow=row.RowIndex;
                    copyDataToTableAddNewRow();
                 
                }
                
            }


        }
    }

    public bool isnumericval(string pstr)
    {
        if (pstr.Trim().Length < 1)
            return false;

        int len = pstr.Length;
        char ch = pstr[0];
        if (ch == '-')
        {
            pstr = pstr.Substring(1, len - 1);

        }
        return isnumericfunction(pstr);

    }

    public bool isnumericfunction(string sh)
    {
        bool flg = true;
        if (sh != " ")
        {
            foreach (char ch in sh)
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
        return flg; ;
    }


    
}