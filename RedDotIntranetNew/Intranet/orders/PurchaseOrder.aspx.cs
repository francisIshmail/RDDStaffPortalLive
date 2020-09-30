using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Intranet_orders_PurchaseOrder : System.Web.UI.Page
{
    int deleteRow;
    string ordType,fls, FilesForuploadTrack,nxtStatusName,refId,mailList = "", nxtStatus = "";
    string qorderId, qtype, qrls;
    int wfProcessId;
    int nxtProcessStatusID, nxtRole;
    long poId,newRequestId;
    string BaseDBCode = "TRI",tsql; // basically we keep this primary database for vendor and items to be listed
    SqlDataReader sDrd;
    Boolean flgWork;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            qorderId=Request.QueryString["poId"].ToString();
            qtype=Request.QueryString["ptype"].ToString();
            wfProcessId = Convert.ToInt32(Request.QueryString["wfProcessId"]);
            ordType = Request.QueryString["ordType"].ToString();
            qrls = Request.QueryString["qrls"].ToString();

            Session["processAbbr"] = "PO-" + ordType;
            Session["currStatusId"] = "2"; //we start from second stage as form input

            lblPOType.Text = ordType;

            lblMsg.Text = "";
            lblPermissionMsg.Text = "";

            if (!IsPostBack)
            {
                //if (ddlDB.Items.Count > 0 && lblPOType.Text == "BTB")
                //{
                //    ddlDB.SelectedIndex = 0;
                //    loadDdlCustomers();

                //    panelDbNCust.Visible = true;
                //}
                //else
                //{
                //    panelDbNCust.Visible = false;
                //}

                //tsql ="select * from dbo.SqlConnectionServers where databaseName Not like 'websiteDb%'";
                tsql = "select * from dbo.SqlConnectionServers where countryCode2 in (select data from dbo.MySplit((select dbCode from dbo.orderSystemUserMapping where lower(membershipUserRoleName)=lower('" + qrls + "') and lower(membershipusername)=lower('" + myGlobal.loggedInUser() + "')),','))";
                //Db.LoadDDLsWithCon(ddlDB, tsql, "databaseName", "CountryCode2", myGlobal.getIntranetDBConnectionString());
                Db.LoadDDLsWithCon(ddlDB, tsql, "Country", "CountryCode2", myGlobal.getIntranetDBConnectionString());

                if (lblPOType.Text.ToUpper() == "BTB")
                {

                    if (ddlDB.Items.Count > 0)
                    {
                        ddlDB.SelectedIndex = 0;
                        loadDdlCustomers();

                        panelDbNCust.Visible = true;
                    }
                    else
                    {
                        lblPermissionMsg.Text = "Sorry! You have no permissions to make order request for " + lblPOType.Text.ToUpper();
                        tblMain.Visible = false;
                    }
                }
                else
                {
                    panelDbNCust.Visible = false;
                }

                if (lblPOType.Text.ToUpper() == "RUNRATE-COMPANY")    //in this case PM is the creator who can work for all dbs, but specific BU'S
                    ddlDB.Enabled = false;


                loadDdlVendor();
                freshDataGrid();

                Db.LoadDDLsWithCon(ddlCBNName, "select * from cbnNamesList order by id", "cbnName", "Id", myGlobal.getIntranetDBConnectionString());

            } //ispostback ends
        
            GridCalculations();
            if (ddlvendorGlb.Items.Count <= 0)
            {
                lblMsg.Text = "Logged in user possibly has no Vendors list in his/her permissions. Can't order for any vendor as of now. Kindly contact system administrator for further queries.";
                btnSubmit.Visible = false;
                btnDraft.Visible = false;
            }
        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message + " , Kindly retry";
        }
    }
    protected void loadDdlVendor()
    {
        
        if (lblPOType.Text.ToUpper() == "RUNRATE-COMPANY")
            tsql = "select BUId,BUName + '[' + BUGroupName + ']' + '[' +  vendorName + ']' + '[' +  vendorAccount + ']' as BU from tej.[dbo].[TblVendorsBUMapping] where BUName in (select data from dbo.MySplit((select BU from dbo.orderSystemUserMapping where lower(membershipUserRoleName)=lower('" + qrls + "') and lower(membershipusername)=lower('" + myGlobal.loggedInUser() + "')),',')) order by BUName";
         else
            tsql = "select BUId,BUName + '[' + BUGroupName + ']' + '[' +  vendorName + ']' + '[' +  vendorAccount + ']' as BU from tej.[dbo].[TblVendorsBUMapping] order by BUName";

        //Note that the copy of table tej.[dbo].[TblVendorsBUMapping]  is on rddapps also, It is orginally used from EVO for other apps

        Db.LoadDDLsWithCon(ddlvendorGlb, tsql, "BU", "BUId", myGlobal.getIntranetDBConnectionString());    
        lblVendorCount.Text = ddlvendorGlb.Items.Count.ToString();
        
        getVendorNameFromSelectedItem();
    }

    protected void getVendorNameFromSelectedItem()
    {
        //added vendor Account field Logic

        if (ddlvendorGlb.Items.Count <= 0)
            return;

        int idx1, idx2, idx3, idx4, idx5;

        //  string sVal="APC   [A001]   [APC DISTRIBUTION LTD]   [APC500]";

        lblBU.Text = ddlvendorGlb.SelectedItem.Text.Substring(0, (ddlvendorGlb.SelectedItem.Text.IndexOf('['))).Trim();

        idx1 = ddlvendorGlb.SelectedItem.Text.IndexOf('[') + 1;
        idx2 = ddlvendorGlb.SelectedItem.Text.IndexOf(']');

        lblItemGroup.Text = ddlvendorGlb.SelectedItem.Text.Substring(idx1, (idx2 - idx1));


        idx3 = ddlvendorGlb.SelectedItem.Text.IndexOf('[',idx2) + 1;
        idx4 = ddlvendorGlb.SelectedItem.Text.IndexOf(']', idx3);

        lblVendorForBU.Text = ddlvendorGlb.SelectedItem.Text.Substring(idx3, (idx4 - idx3));

        idx5 = ddlvendorGlb.SelectedItem.Text.LastIndexOf('[') + 1;
        lblVendorAcct.Text = ddlvendorGlb.SelectedItem.Text.Substring(idx5, (ddlvendorGlb.SelectedItem.Text.Length - idx5 - 1));
    }

    protected void getVendorNameFromSelectedItem_Org()
    {
        if (ddlvendorGlb.Items.Count <= 0)
            return;

        int idx1,idx2;

        lblBU.Text = ddlvendorGlb.SelectedItem.Text.Substring(0, (ddlvendorGlb.SelectedItem.Text.IndexOf('[') - 1)).Trim();

        idx1 = ddlvendorGlb.SelectedItem.Text.IndexOf('[') + 1;
        idx2 = ddlvendorGlb.SelectedItem.Text.IndexOf(']');
        lblItemGroup.Text = ddlvendorGlb.SelectedItem.Text.Substring(idx1, (idx2 - idx1));

        idx1=ddlvendorGlb.SelectedItem.Text.LastIndexOf('[')+1;
        lblVendorForBU.Text = ddlvendorGlb.SelectedItem.Text.Substring(idx1, (ddlvendorGlb.SelectedItem.Text.Length-idx1-1));
    }

    protected void loadDdlCustomers()
    {
        if (lblPOType.Text.ToUpper() == "BTB")
        {
            //Db.LoadDDLsWithCon(ddlCustList, "select DCLink,Name from client order by Name", "Name", "DCLink", myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString()));
            Db.LoadDDLsWithCon(ddlCustList, "select DCLink,Name + '[' + Account + ']' as Name from client order by Name", "Name", "DCLink", myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString()));
            lblCustCount.Text = ddlCustList.Items.Count.ToString();
        }
    }
    protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadDdlCustomers();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox tmptxtQty = (TextBox)e.Row.FindControl("txtQty");
            tmptxtQty.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

            TextBox tmptxtCurrPrice = (TextBox)e.Row.FindControl("txtCurrPrice");
            tmptxtCurrPrice.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

            Label tmplblAmountTotal = (Label)e.Row.FindControl("lblAmountTotal");
            tmplblAmountTotal.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtQty.Text) * Convert.ToDouble(tmptxtCurrPrice.Text)));

            TextBox tmptxtRebatePerUnit = (TextBox)e.Row.FindControl("txtRebatePerUnit");
            tmptxtRebatePerUnit.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

            Label tmplblCostAfterRebate = (Label)e.Row.FindControl("lblCostAfterRebate");
            tmplblCostAfterRebate.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtCurrPrice.Text) - Convert.ToDouble(tmptxtRebatePerUnit.Text)));

            Label tmplblTotalCostAfterRebate = (Label)e.Row.FindControl("lblTotalCostAfterRebate");
            tmplblTotalCostAfterRebate.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtQty.Text) * Convert.ToDouble(tmplblCostAfterRebate.Text)));

            TextBox tmptxtSelleingPrice = (TextBox)e.Row.FindControl("txtSelleingPrice");
            tmptxtSelleingPrice.Attributes.Add("onBlur", "updateValues(" + (e.Row.RowIndex).ToString() + ");");

            Label tmplblTotalSelleing = (Label)e.Row.FindControl("lblTotalSelleing");
            tmplblTotalSelleing.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtQty.Text) * Convert.ToDouble(tmptxtSelleingPrice.Text)));

            Label tmplblMargin = (Label)e.Row.FindControl("lblMargin");

            if (Convert.ToDouble(tmplblTotalSelleing.Text) == 0 || Convert.ToDouble(tmplblTotalCostAfterRebate.Text) == 0)
                tmplblMargin.Text = "0";
            else
                tmplblMargin.Text = String.Format("{0:0.00}", ((Convert.ToDouble(tmplblTotalSelleing.Text) - Convert.ToDouble(tmplblTotalCostAfterRebate.Text)) / (Convert.ToDouble(tmplblTotalCostAfterRebate.Text) )* 100));

        }
    }
    
    private void GridCalculations()  //send -1 as parameter value for all rows Other wise works for single row
    {
            foreach (GridViewRow grw in GridView1.Rows)
            {
                TextBox tmptxtQty = (TextBox)grw.FindControl("txtQty");
                tmptxtQty.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                TextBox tmptxtCurrPrice = (TextBox)grw.FindControl("txtCurrPrice");
                tmptxtCurrPrice.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                Label tmplblAmountTotal = (Label)grw.FindControl("lblAmountTotal");
                tmplblAmountTotal.Text = String.Format("{0:0.00}", (Convert.ToDouble(tmptxtQty.Text) * Convert.ToDouble(tmptxtCurrPrice.Text)));

                TextBox tmptxtRebatePerUnit = (TextBox)grw.FindControl("txtRebatePerUnit");
                tmptxtRebatePerUnit.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                Label tmplblCostAfterRebate = (Label)grw.FindControl("lblCostAfterRebate");
                tmplblCostAfterRebate.Text = String.Format("{0:0.00}",(Convert.ToDouble(tmptxtCurrPrice.Text) - Convert.ToDouble(tmptxtRebatePerUnit.Text)));

                Label tmplblTotalCostAfterRebate = (Label)grw.FindControl("lblTotalCostAfterRebate");
                tmplblTotalCostAfterRebate.Text = String.Format("{0:0.00}",(Convert.ToDouble(tmptxtQty.Text) * Convert.ToDouble(tmplblCostAfterRebate.Text)));

                TextBox tmptxtSelleingPrice = (TextBox)grw.FindControl("txtSelleingPrice");
                tmptxtSelleingPrice.Attributes.Add("onBlur", "updateValues(" + (grw.RowIndex).ToString() + ");");

                Label tmplblTotalSelleing = (Label)grw.FindControl("lblTotalSelleing");
                tmplblTotalSelleing.Text = String.Format("{0:0.00}",(Convert.ToDouble(tmptxtQty.Text) * Convert.ToDouble(tmptxtSelleingPrice.Text)));

                Label tmplblMargin = (Label)grw.FindControl("lblMargin");
                //tmplblMargin.Text = String.Format("{0:0.00}", ((Convert.ToDouble(tmplblTotalSelleing.Text) - Convert.ToDouble(tmplblTotalCostAfterRebate.Text)) / (Convert.ToDouble(tmplblTotalCostAfterRebate.Text)) * 100));

                if (Convert.ToDouble(tmplblTotalSelleing.Text) == 0 || Convert.ToDouble(tmplblTotalCostAfterRebate.Text) == 0)
                    tmplblMargin.Text = "0";
                else
                    tmplblMargin.Text = String.Format("{0:0.00}", ((Convert.ToDouble(tmplblTotalSelleing.Text) - Convert.ToDouble(tmplblTotalCostAfterRebate.Text)) / (Convert.ToDouble(tmplblTotalCostAfterRebate.Text)) * 100));

            }
    }

    private Boolean varifyMandatoryUploadCase()
    {
        Boolean flg = false;
        int cnt = 0;
        HttpFileCollection files = Request.Files;
        HttpPostedFile postFile;

        for (int i = 0; i < files.Count; i++)
        {
            postFile = files[i];
            if (postFile.ContentLength > 0)
            {
                cnt++;
            }
        }

        if (cnt > 0)
            flg = true;

        return flg;
    }

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        Session["currStatusId"] = "1"; //we start from second stage as form input
        flgWork = false;
        
        try
        {
          flgWork = workNow();
        }
        catch (Exception exps)
        {
            lblMsg.Text = exps.Message;
            //MsgBoxControl1.show(lblMsg.Text,"Error!! ");
        }

        //if (!flgWork)
        //    return;


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session["currStatusId"] = "2"; //we start from second stage as form input
        flgWork = false;

        try
        {
            flgWork = workNow();
        }
        catch (Exception exps)
        {
            lblMsg.Text = exps.Message;
            //MsgBoxControl1.show(lblMsg.Text, "Error!! ");
        }
        
        //if (!flgWork)
        //    return;
    }

    private Boolean setMandatoryUploadCheckedStatus()
    {
        Boolean flg = false;
        int cnt = 0;
        HttpFileCollection files = Request.Files;
        HttpPostedFile postFile;

        for (int i = 0; i < files.Count; i++)
        {
            postFile = files[i];
            if (postFile.ContentLength > 0)
            {
                cnt++;
                chkUploadFilesWish.Checked = true;
                break;
            }
        }

        if (cnt > 0)
            flg = true;

        return flg;
    }
    private Boolean workNow()
    {
        lblMsg.Text = "";
        int cr=0;

        string prevPart = "";

        if (chkUploadFilesWish.Checked == false)
            setMandatoryUploadCheckedStatus();
        
        
        //verify db connectivity to Rddapps server.
        if (!myGlobal.verifyConnectionToServer(myGlobal.getIntranetDBConnectionString()))
        {
            lblMsg.Text = "Website server connection error, Please retry later or contact system administrator for assistance.";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            return false;
        }

        if (ddlCBNName.SelectedItem.Text.ToLower() == "not selected")
        {
            lblMsg.Text = "Sorry!!! can't process as field CBN Name is not selected from the list.";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            return false;
        }


        txtFpoRef.Text = txtFpoRef.Text.Trim();
        if (txtFpoRef.Text == "")
        {
            txtFpoRef.Text = "UNASSIGNED";
        }

        txtOrderCmnts.Text = txtOrderCmnts.Text.Trim();
        if (txtOrderCmnts.Text.Length >= 3900)
        {
            lblMsg.Text = "Comments field value can't exceed 4000 characters.";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            return false;
        }

        if (chkUploadFilesWish.Checked==true)
        {
            if (!varifyMandatoryUploadCase())
            {
                //lblMsg.Text = "You have checked 'Upload New Files' option but no files are selected to be uploaded or you may Un-check 'Upload New Files' option to proceed !!";
                lblMsg.Text = "Warning ! You attempted to upload few files before, kindly select your files again or you may Un-check 'Upload New Files' option to proceed without uploading your files!!";
                //MsgBoxControl1.show(lblMsg.Text, "Warning !");
                return false;
            }
        }
        
        //if (txtOrderCmnts.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Error! It's Mandatory to supply value for Comments field.  ";
        //    //MsgBoxControl1.show(lblMsg.Text, "Error !!");
        //    return false;
        //}

        if (txtOrderCmnts.Text.Trim() == "")
        {
            txtOrderCmnts.Text = "No Comments.";
        }

        if (txtOrderCmnts.Text.Trim().IndexOf("'") >= 0)
        {
            lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!");
            return false;
        }

        if (txtOrderCmnts.Text.Trim().IndexOf("\"") >= 0)
        {
            lblMsg.Text = "Invalid Character occurs \" in field Comments, Char( \" ) not supported.";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!");
            return false;
        }

        if (GridView1.Rows.Count == 0)
        {
            lblMsg.Text = "Error! No Item row created, can't submitt";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!");
            return false;
        }

        foreach (GridViewRow rw in GridView1.Rows)
        {
            cr = rw.RowIndex + 1;

            string tmpPartNO, tmpDesc;
            double tmpQty, tmpCurrPrice, tmpRebatePerUnit, tmpSellingPrice;
            tmpPartNO = (rw.FindControl("txtPartNo") as TextBox).Text.Trim();
            tmpDesc = (rw.FindControl("txtDescription") as TextBox).Text.Trim();
            tmpQty = Convert.ToDouble((rw.FindControl("txtQty") as TextBox).Text.Trim());
            tmpCurrPrice = Convert.ToDouble((rw.FindControl("txtCurrPrice") as TextBox).Text.Trim());
            tmpRebatePerUnit = Convert.ToDouble((rw.FindControl("txtRebatePerUnit") as TextBox).Text.Trim()); 
            tmpSellingPrice = Convert.ToDouble((rw.FindControl("txtSelleingPrice") as TextBox).Text.Trim());

            

            if (tmpPartNO.Trim() == prevPart)
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Field Part no. can't be duplicate as alreay ordered above";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }

            if (tmpPartNO.Trim() == "")
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Field Part no. can't be empty or delete entire row";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }
            if (tmpDesc.Trim() == "")
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Field Part Description can't be empty or delete entire row";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }
            if (!Util.isValidNumber(tmpQty.ToString()) || tmpQty==0)
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Supply a valid numeric value > 0 for field Qty or delete entire row";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }
            if (!Util.isValidDecimalNumber(tmpCurrPrice.ToString()) || tmpQty == 0)
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Supply a valid numeric value > 0 for field Current Price or delete entire row";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }
            if (!Util.isValidDecimalNumber(tmpRebatePerUnit.ToString()))
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Supply a valid numeric value for field Rebate Per Unit or delete entire row";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }
            if (!Util.isValidDecimalNumber(tmpSellingPrice.ToString()) || tmpQty==0)
            {
                lblMsg.Text = "Error! on Row [" + cr.ToString() + "] , Supply a valid numeric value > 0 for field Selling Price or delete entire row";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return false;
            }

            prevPart = tmpPartNO; //set value to prevPartno in loop
        }

        string sqlMainOrder,sqlOrderLines,sql,constr,msg;
        
       // int poId;
        string vendor, evoPONO, PODate, reqDelDate,pdbCode;
        string cmts,lastUpdated,orderCmnts;
        double orderTotal=0, orderTotalCostAfterRebate=0, orderTotalSelling=0, orderMargin=0;

         
        constr = myGlobal.getIntranetDBConnectionString();

        ////////////////////////////
        SqlDataReader dr;
        string pEmail = "", pCustomer = "", pCustAcct = "", pCountry = "", pVendor, ByUser, pmgr;
        
        //qry = "select * from dealer where dealerUid='" + myGlobal.loggedInUser() + "'";
        //Db.constr = myGlobal.ConnectionString;
        //dr = Db.myGetReader(qry);

        //while (dr.Read())
        //{
        //    dealerCompany = dr["dealerEVOName"].ToString();
        //    dealerCountry = dr["dealerCountry"].ToString();
        //    dealerPhone = dr["dealerPhone"].ToString();
        //    dealerEmail = dr["dealerEmail"].ToString();
        //}

        pVendor = lblVendorForBU.Text; //ddlvendorGlb.SelectedItem.Text;

        if (lblPOType.Text == "BTB")   //wfProcessId == 10011
        {
            string cst = "";
            cst = ddlCustList.SelectedItem.Text.Substring(0, ddlCustList.SelectedItem.Text.IndexOf('['));
            pCustomer = cst; //ddlCustList.SelectedItem.Text;

            int idx1,idx2;
            idx1=ddlCustList.SelectedItem.Text.IndexOf('[');
            idx2=ddlCustList.SelectedItem.Text.IndexOf(']');
            cst = ddlCustList.SelectedItem.Text.Substring(idx1+1,(idx2- idx1-1));
            pCustAcct = cst;
            pdbCode = ddlDB.SelectedValue.ToString();
        }
        else
        {
            pCustomer = "NA";
            pCustAcct = "NA";
            //pdbCode = BaseDBCode;
            pdbCode = ddlDB.SelectedValue.ToString();
        }

        if (wfProcessId == 10013)   //if this is RUNRATE-COMPANY then we update pm field value as well
            pmgr = myGlobal.loggedInUser();
        else
            pmgr = "UNASSIGNED";

        pCountry = ddlDB.SelectedValue.ToString();
        pEmail = myGlobal.loggedInUserEmail();
        ByUser = myGlobal.loggedInUser();
        ////////////////////////////////

        ////select p.processStatusId,p.processStatusName,p.nextprocessStatusID,q.processStatusName as nextProcessStatusName,p.nextRole,r.roleName,r.emailList as roleEmail from processStatus as p left join processStatus as q on q.processStatusID=p.nextprocessStatusID and q.fk_processId=p.fk_processId left join dbo.roles as r on p.nextRole=r.roleId where p.processStatusId=2 and p.fk_processId=10011

        Db.constr = constr;
        //sql = "select p.processStatusId as currProcessStatusId,p.processStatusName as currProcessStatusName,p.nextProcessStatusID,q.processStatusName as nextProcessStatusName,p.nextRole as nextRoleId,r.roleName as nextRoleName,r.emailList as nextRoleEmail from processStatus as p left join processStatus as q on q.processStatusID=p.nextprocessStatusID and q.fk_processId=p.fk_processId left join dbo.roles as r on p.nextRole=r.roleId where p.processStatusId=" + Session["currStatusId"].ToString() + " and p.fk_processId=" + wfProcessId.ToString();
        sql = "select p.processStatusId as currProcessStatusId,p.processStatusName as currProcessStatusName,p.nextProcessStatusID,q.processStatusName as nextProcessStatusName,p.nextRole as nextRoleId,r.roleName as nextRoleName,r.emailList" 
            + pdbCode + "  as nextRoleEmail from processStatus as p left join processStatus as q on q.processStatusID=p.nextprocessStatusID and q.fk_processId=p.fk_processId left join dbo.roles as r on p.nextRole=r.roleId where p.processStatusId=" 
            + Session["currStatusId"].ToString() + " and p.fk_processId=" + wfProcessId.ToString();

        dr = Db.myGetReader(sql);
        dr.Read();
        nxtProcessStatusID = Convert.ToInt32(dr["nextProcessStatusID"]);
        nxtRole = Convert.ToInt32(dr["nextRoleId"]);
        Session["currStatusName"] = dr["currProcessStatusName"].ToString();
        nxtStatus = dr["nextProcessStatusName"].ToString();
        mailList = dr["nextRoleEmail"].ToString();
        dr.Close();

        ////////////-------overwrites emails list according to new system---------------------

        sql = "exec [dbo].[getEmaillistforPOEscalation] " + Session["currStatusId"].ToString() + ",'" + lblBU.Text.ToUpper() + "','" + pdbCode + "',1," + wfProcessId.ToString();   // 0/1 stands for direction 0 is prev, 1 is next

        Db.constr = constr;
        dr = Db.myGetReader(sql);

        if(dr.HasRows)
            mailList = "";  //if it enters loop , clear old emails list from old qry

        while (dr.Read())
        {
            

            if (dr["Email"] != DBNull.Value)
            {
                if (mailList == "")
                    mailList = dr["Email"].ToString();
                else
                    mailList += ";" + dr["Email"].ToString();
            }
        }
        dr.Close();

        /////////////////////////////////////////////////////////

        vendor = lblVendorForBU.Text; //ddlvendorGlb.SelectedItem.Text;
        
        //txtOrderCmnts.Text = "Vendor - " + vendor + ", " + txtOrderCmnts.Text;
        txtOrderCmnts.Text = txtOrderCmnts.Text;
        
        orderCmnts = checkSingleQuote(txtOrderCmnts.Text.Trim());
        evoPONO = "UNASSIGNED";
        PODate = DateTime.Now.ToString("MM-dd-yyyy");
        reqDelDate = DateTime.Now.ToString("MM-dd-yyyy");

        //sqlMainOrder = "INSERT INTO PurchaseOrders(vendor,evoPoNo,PoDate,reqDelDate,total,totalCostAfterRebate,totalSelling,margin) VALUES('" + vendor + "','" + evoPONO + "','" + PODate + "','" + reqDelDate + "'," + orderTotal + "," + orderTotalCostAfterRebate + "," + orderTotalSelling + "," + orderMargin + ")";
        //sqlMainOrder = "INSERT INTO PurchaseOrders(vendor,comments,evoPoNo,PoDate,reqDelDate,bu,customerName,dbCode,ProductManager,fpoNo,createdBy) VALUES('" + vendor + "','" + orderCmnts + "','" + evoPONO + "','" + PODate + "','" + reqDelDate + "','" + lblBU.Text + "','" + pCustomer + "','" + pdbCode + "','" + pmgr + "','" + txtFpoRef.Text + "','" + myGlobal.loggedInUser() + "') ";
        
        //sqlMainOrder = "INSERT INTO PurchaseOrders(vendor,comments,evoPoNo,PoDate,reqDelDate,bu,customerName,dbCode,ProductManager,fpoNo,createdBy,custAcct) VALUES('" + vendor + "','" + orderCmnts + "','" + evoPONO + "','" + PODate + "','" + reqDelDate + "','" + lblBU.Text + "','" + pCustomer + "','" + pdbCode + "','" + pmgr + "','" + txtFpoRef.Text + "','" + myGlobal.loggedInUser() + "','" + pCustAcct + "') ";

        sqlMainOrder = "INSERT INTO PurchaseOrders(vendor,comments,evoPoNo,PoDate,reqDelDate,bu,customerName,dbCode,ProductManager,fpoNo,createdBy,custAcct,vendorAcct,cbnName) VALUES('" + vendor + "','" + orderCmnts + "','" + evoPONO + "','" + PODate + "','" + reqDelDate + "','" + lblBU.Text + "','" + pCustomer + "','" + pdbCode + "','" + pmgr + "','" + txtFpoRef.Text + "','" + myGlobal.loggedInUser() + "','" + pCustAcct + "','" + lblVendorAcct.Text.Trim() + "','" + ddlCBNName.SelectedItem.Text + "') ";

        Db.constr = constr;
        poId=Db.myExecuteSQLReturnLatestAutoID(sqlMainOrder);

        //sql = "select max(poId) from PurchaseOrders";
        //Db.constr = constr;
        //poId = Db.myExecuteScalar(sql);

        //order insertion into request table
        lastUpdated = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");

        //cmts = "Vendor - " + vendor + ", New Order";
        cmts = orderCmnts;
        //qry = "insert into [dbo].[orderRequest] values(" + v22 + ",'',2,'" + importOrderUser + "','" + cdate + "',1,'PO','" + v1 + "')";
        sql = "insert into processRequest(refId,refValue,comments,fk_statusId,fk_EscalateLevelId,ByUser,lastModified,fk_processId) values(" + poId.ToString() + ",'" + evoPONO + "','" + cmts + "'," + nxtProcessStatusID + "," + nxtRole + ",'" + ByUser + "','" + lastUpdated + "'," + wfProcessId.ToString() + ")";
        Db.constr = constr;
        //Db.myExecuteSQL(sql);
        newRequestId = Db.myExecuteSQLReturnLatestAutoID(sql);

        string linePartNo,linePartDesc,lineOrderType;
        int rowNum;
        double lineQty,lineCurrPrice, lineAmountTotal, lineRebatePerUnit, lineCostAfterRebate, lineTotCostAfterRebate, lineSellingPrice, lineTotSelling, lineMarginPerc;

        rowNum = 0;
        sqlOrderLines = "begin BEGIN TRANSACTION ";
        foreach (GridViewRow rw in GridView1.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                rowNum = rowNum + 1;
                linePartNo =checkSingleQuote((rw.FindControl("txtPartNo") as TextBox).Text);
                linePartDesc =checkSingleQuote((rw.FindControl("txtDescription") as TextBox).Text);
                lineQty = Convert.ToDouble((rw.FindControl("txtQty") as TextBox).Text);
                lineCurrPrice = Convert.ToDouble((rw.FindControl("txtCurrPrice") as TextBox).Text);
                lineAmountTotal = Convert.ToDouble((rw.FindControl("lblAmountTotal") as Label).Text);
                lineRebatePerUnit = Convert.ToDouble((rw.FindControl("txtRebatePerUnit") as TextBox).Text);
                lineCostAfterRebate = Convert.ToDouble((rw.FindControl("lblCostAfterRebate") as Label).Text);
                lineTotCostAfterRebate = Convert.ToDouble((rw.FindControl("lblTotalCostAfterRebate") as Label).Text);
                lineSellingPrice = Convert.ToDouble((rw.FindControl("txtSelleingPrice") as TextBox).Text);
                lineTotSelling = Convert.ToDouble((rw.FindControl("lblTotalSelleing") as Label).Text);
                lineMarginPerc = Convert.ToDouble((rw.FindControl("lblMargin") as Label).Text);
                lineOrderType = (rw.FindControl("ddlOrderType") as DropDownList).SelectedItem.Text;
                //lineComment = (rw.FindControl("txtComments") as TextBox).Text;

                orderTotal = orderTotal + lineAmountTotal;
                orderTotalCostAfterRebate = orderTotalCostAfterRebate + lineTotCostAfterRebate;
                orderTotalSelling = orderTotalSelling + lineTotSelling;
                orderMargin=((orderTotalSelling-orderTotalCostAfterRebate)/orderTotalCostAfterRebate)*100;

                sqlOrderLines = sqlOrderLines + "  " + "INSERT INTO PurchaseOrderlines(fk_poId,lineNum,customerName,region,partNo,smallDescription,qty,currPrice ,amountTotal ,rebatePerUnit,CostAfterRebate ,totalCostAfterRebate,sellingPrice,totalSelling,margin,orderType) VALUES(" + poId.ToString() + "," + rowNum + ",'" + pCustomer + "','" + pCountry + "','" + linePartNo + "','" + linePartDesc + "'," + lineQty + "," + lineCurrPrice + "," + lineAmountTotal + "," + lineRebatePerUnit + "," + lineCostAfterRebate + "," + lineTotCostAfterRebate + "," + lineSellingPrice + "," + lineTotSelling + "," + lineMarginPerc + ",'" + lineOrderType + "')";

            }
        }
        sqlOrderLines = sqlOrderLines + "  IF @@ERROR = 0 COMMIT TRANSACTION ELSE ROLLBACK TRANSACTION end";
        //insert order lines
        Db.constr = constr;
        Db.myExecuteSQL(sqlOrderLines);

        //update totals in purchaseOrder
        sqlMainOrder = "UPDATE PurchaseOrders set total=" + orderTotal + ",totalCostAfterRebate=" + orderTotalCostAfterRebate + ",totalSelling=" + orderTotalSelling + ",margin=" + orderMargin + " where poId=" + poId.ToString() + "";
        Db.constr = constr;
        Db.myExecuteSQL(sqlMainOrder);

        
        //insert into process track

        //int tmp;
        //sql = "select max(processRequestId) from processRequest";
        //Db.constr = constr;
        //tmp = Db.myExecuteScalar(sql);
        
        //sql = "insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + newRequestId.ToString() + "," + Session["currStatusId"].ToString() + "," + nxtProcessStatusID + "," + nxtRole + ",'" + ByUser + "','Created','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtOrderCmnts.Text.Trim() + "'," + wfProcessId + ")";
        sql = "insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId,mailTo,ccTo) values(" + newRequestId.ToString() + "," + Session["currStatusId"].ToString() + "," + nxtProcessStatusID + "," + nxtRole + ",'" + ByUser + "','Created','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtOrderCmnts.Text.Trim() + "'," + wfProcessId + ",'" + mailList + "','" + myGlobal.membershipUserEmail(myGlobal.loggedInUser()) +"')";
        Db.constr = constr;
        Db.myExecuteSQL(sql);


        //////sql = "select p.processStatusName,r.emailList from processStatus as p join dbo.roles as r on p.prevRole=r.roleId where processStatusId=" + nxtProcessStatusID + " and fk_processId=" + wfProcessId;
        //sql = "select p.processStatusName,r.emailList from processStatus as p join dbo.roles as r on p.nextRole=r.roleId where processStatusId=" + nxtProcessStatusID + " and fk_processId=" + wfProcessId;
        //Db.constr = myGlobal.getIntranetDBConnectionString();
        //dr = Db.myGetReader(sql);
        //string mailList = "", nxtStatus = "";
        //while (dr.Read())
        //{
        //    nxtStatus = dr["processStatusName"].ToString();
        //    mailList = dr["emailList"].ToString();
        //}
        //dr.Close();
        ////////////////////////////new code////////////////////////////

        refId = poId.ToString();
        nxtStatusName = nxtStatus;

        SaveFileAtWebsiteLocation("/excelFileUpload/wfUploads/");  //this fills fls variable with all the files uploaded, whch can be mailed further

        sql = "";

        if (FilesForuploadTrack.Trim() != "")
        {
            string[] sfsl = FilesForuploadTrack.Split(';');
            for (int x = 0; x < sfsl.Length; x++)
            {
                if (sql.Trim() == "")
                    sql = "insert into [dbo].[uploadTrack](fk_refId,fk_StatusId,fk_processId,srNo,websiteFilePath,ByUser) values(" + refId + "," + Session["currStatusId"].ToString() + "," + wfProcessId + "," + (x + 1).ToString() + ",'" + sfsl[x] + "','" + myGlobal.loggedInUser().ToString() + "')";
                else
                    sql += " ; " + "insert into [dbo].[uploadTrack](fk_refId,fk_StatusId,fk_processId,srNo,websiteFilePath,ByUser) values(" + refId + "," + Session["currStatusId"].ToString() + "," + wfProcessId + "," + (x + 1).ToString() + ",'" + sfsl[x] + "','" + myGlobal.loggedInUser().ToString() + "')";
            }
        }

        if (sql.Trim() != "") //if it is not empty then only run
        {
            Db.constr = myGlobal.getIntranetDBConnectionString();
            Db.myExecuteSQL(sql);
        }
        //////////////////////////////////////////////////////////////////////////////

        if (Session["currStatusId"].ToString() == "1")  //draft case , no email should be sent
        {
            lblMsg.Text = "Order has been drafted successfully, No email intimation applicable in draft case";
        }
        else
        {
            //send mail
            msg = sendMailForNewOrderCreation(pVendor, pCustomer, pCountry, ByUser, nxtStatus, mailList, myGlobal.loggedInUserEmail(), lblPOType.Text, fls);
            //msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, (userEmail + ";" + txtInitimationMailId.Text), newescalateLevelId, mailList, fls);

            msg += " , Order Form Filled successfully!";
            lblMsg.Text = msg;

        }

        //MsgBoxControl1.show(msg,"Success!!");

        txtOrderCmnts.Text = "";
        txtFpoRef.Text = "UNASSIGNED";
        loadDdlVendor();
        freshDataGrid();
        chkUploadFilesWish.Checked = false;
        return true;  //if reaches end line
    }
    
    private string sendMailForNewOrderCreation(string vendorName,string cust,string cntry,string requester,string nxtStatus,string mailList,string sndCC,string pOrdType,string pfls)
    {
        string strtmp="";

        strtmp = "<br/><b>WorkFlow System Update<b><br/><br/> ";

        strtmp += "New Purchase Order Request<br/><br/>";
        strtmp += "<b>" + pOrdType + "</b> Purchase Order has been created.<br/><br/>";
        strtmp += "Order has been added to Order System for further processing and the current stage is : <b>" + nxtStatus + "</b><br/><br/>";

        strtmp += "Order For Vendor : <b>" + vendorName + "</b><br/><br/>";
        
        if(pOrdType=="BTB")
         strtmp += "Reference Customer : <b>" + cust + "</b><br/>";

        strtmp += "FPO Reference : <b>" + txtFpoRef.Text.Trim() + "</b><br/>";
        strtmp += "Region : <b>" + cntry + "</b><br/>";
        strtmp += "Order Placed By User : <b>" + requester + "</b><br/>";
        strtmp += "Updation comments : <b>" + txtOrderCmnts.Text + "</b><br/>";


        if (pfls != "")
            strtmp += "<b>Files are attached to this mail for the reference  </b><br/>";

        strtmp += "<br/><br/>Please follow up the order link using your Login credentials: ";
        //strtmp += "<br/>" + myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/viewOrdersPO.aspx"; 
        strtmp += "<br/>" + myGlobal.getSiteIPwithPortNo() + "/Intranet/orders/viewOrdersPO.aspx"; 

        strtmp += "<br/><br/>Best Regards,<br/>Red Dot Distribution";

        ////Close this code line befor live

        //strtmp += "<br/><br/>In Actuall Mails will be sent to role/country/BU Basedmails : " + mailList + " , CC (user itself) : " + sndCC;
        //mailList = "victor@eternatec.com"; //actual comes as parameter 
        //sndCC = "vishav@eternatec.com";
         

        /////dont use this///string msg = Mail.SendCC(myGlobal.getSystemConfigValue("websiteEmailer"), mailList, sndCC, "New Order", strtmp, true);

        string msg = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), mailList, sndCC, "New Order", strtmp, true, "", pfls);
        
        return msg;
    }

    

    protected string checkSingleQuote(string val)
    {
        string retVal;
        retVal = val.Replace("'", "''");
        return retVal;
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
        LoadGridDdlPartNo();
    }
      
    protected void btnClearAll_Click(object sender, ImageClickEventArgs e)
    {
        freshDataGrid();
    }
    
    
    private void copyDataToTableAddNewRow()
    {
        //Serial,PartNo,Description,Qty,CurrPrice,AmountTotal,RebatePerUnit,CostAfterRebate,TotalCostAfterRebate,SelleingPrice,TotalSelleing,Margin,OrderType,Comments

        DataTable dt = (DataTable)ViewState["CurrentTable"];

        Label tmplblSerial;
        //TextBox tmptxtManufacturer;
        Label tmplblFindPartLike;
        TextBox tmptxtPartNo;
        TextBox tmptxtDescription;
        TextBox tmptxtQty;
        TextBox tmptxtCurrPrice;
        Label tmplblAmountTotal;
        TextBox tmptxtRebatePerUnit;
        Label tmplblCostAfterRebate;
        Label tmplblTotalCostAfterRebate;
        TextBox tmptxtSelleingPrice;
        Label tmplblTotalSelleing;
        Label tmplblMargin;
        //TextBox tmptxtOrderType;
        DropDownList tmpDdlOrderType;
        TextBox tmptxtComments;

        // DropDownList ddlPbx;


        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmplblFindPartLike = (rw.FindControl("lblFindPartLike") as Label);
            tmptxtQty = (rw.FindControl("txtQty") as TextBox);
            tmptxtCurrPrice = (rw.FindControl("txtCurrPrice") as TextBox);
            tmplblAmountTotal = (rw.FindControl("lblAmountTotal") as Label);
            tmptxtRebatePerUnit = (rw.FindControl("txtRebatePerUnit") as TextBox);
            tmplblCostAfterRebate = (rw.FindControl("lblCostAfterRebate") as Label);
            tmplblTotalCostAfterRebate = (rw.FindControl("lblTotalCostAfterRebate") as Label);
            tmptxtSelleingPrice = (rw.FindControl("txtSelleingPrice") as TextBox);
            tmplblTotalSelleing = (rw.FindControl("lblTotalSelleing") as Label);
            tmplblMargin = (rw.FindControl("lblMargin") as Label);
            //tmptxtOrderType = (rw.FindControl("txtOrderType") as TextBox);
            tmpDdlOrderType = (rw.FindControl("ddlOrderType") as DropDownList);

            if (deleteRow == rw.RowIndex)
            {
                tmptxtQty.Text = "0";
                tmptxtCurrPrice.Text = "0";
                tmplblAmountTotal.Text = "0";
                tmptxtRebatePerUnit.Text = "0";
                tmplblCostAfterRebate.Text = "0";
                tmplblTotalCostAfterRebate.Text = "0";
                tmptxtSelleingPrice.Text = "0";
                tmplblTotalSelleing.Text = "0";
                tmplblMargin.Text = "0";
                //tmptxtOrderType.Text = "Stock";
                if (tmpDdlOrderType.Items.Count > 0)
                 tmpDdlOrderType.SelectedIndex = 0;
            }
            
            if (!Util.isValidNumber(tmptxtQty.Text))
            {
                lblMsg.Text = "Error ! Please supply a valid numeric value for quantity field in row :" + (rw.RowIndex + 1).ToString() + ", decimal values are not allowed for qty field";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return;
            }
            if (!Util.isValidDecimalNumber(tmptxtCurrPrice.Text))
            {
                lblMsg.Text = "Error ! Please supply a valid numeric value for Current Price field in row :" + (rw.RowIndex + 1).ToString();
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return;
            }
            
            if (!Util.isValidDecimalNumber(tmptxtRebatePerUnit.Text))
            {
                lblMsg.Text = "Error ! Please supply a valid numeric value for Rebate Per Unit field in row :" + (rw.RowIndex + 1).ToString();
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return;
            }
            
            if (!Util.isValidDecimalNumber(tmptxtSelleingPrice.Text))
            {
                lblMsg.Text = "Error ! Please supply a valid numeric value for Selling Price field in row :" + (rw.RowIndex + 1).ToString();
                //MsgBoxControl1.show(lblMsg.Text, "Error !!");
                return;
            }
            
        }

        int i = 0;
        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmplblSerial = (Label)rw.FindControl("lblSerial") as Label;
            //ddlPbx = (rw.FindControl("ddlvendor") as DropDownList);
            //tmptxtManufacturer = (rw.FindControl("txtManufacturer") as TextBox);
            tmplblFindPartLike = (rw.FindControl("lblFindPartLike") as Label);
            tmptxtPartNo = (rw.FindControl("txtPartNo") as TextBox);
            tmptxtDescription = (rw.FindControl("txtDescription") as TextBox);
            
            tmptxtQty = (rw.FindControl("txtQty") as TextBox);
            tmptxtCurrPrice = (rw.FindControl("txtCurrPrice") as TextBox);
            tmplblAmountTotal = (rw.FindControl("lblAmountTotal") as Label);
            tmptxtRebatePerUnit = (rw.FindControl("txtRebatePerUnit") as TextBox);
            tmplblCostAfterRebate = (rw.FindControl("lblCostAfterRebate") as Label);
            tmplblTotalCostAfterRebate = (rw.FindControl("lblTotalCostAfterRebate") as Label);
            tmptxtSelleingPrice = (rw.FindControl("txtSelleingPrice") as TextBox);
            tmplblTotalSelleing = (rw.FindControl("lblTotalSelleing") as Label);
            tmplblMargin = (rw.FindControl("lblMargin") as Label);
            //tmptxtOrderType = (rw.FindControl("txtOrderType") as TextBox);
            tmpDdlOrderType = (rw.FindControl("ddlOrderType") as DropDownList);
            tmptxtComments = (rw.FindControl("txtComments") as TextBox);

            //tmptxtManufacturer.Text= ddlPbx.SelectedItem.Text;

            dt.Rows[i]["Serial"] = Convert.ToInt32(tmplblSerial.Text);
            //dt.Rows[i]["Manufacturer"] =tmptxtManufacturer.Text;
            dt.Rows[i]["txtFind"] = tmplblFindPartLike.Text;
            dt.Rows[i]["PartNo"] = tmptxtPartNo.Text;
            dt.Rows[i]["Description"] = tmptxtDescription.Text;
            dt.Rows[i]["Qty"] = Convert.ToInt32(tmptxtQty.Text);

            //Serial,PartNo,Description,Qty,CurrPrice,AmountTotal,RebatePerUnit,CostAfterRebate,TotalCostAfterRebate,SelleingPrice,TotalSelleing,Margin,OrderType,Comments

            dt.Rows[i]["CurrPrice"] = Convert.ToDouble(tmptxtCurrPrice.Text);
            dt.Rows[i]["AmountTotal"] = Convert.ToDouble(tmplblAmountTotal.Text);
            dt.Rows[i]["RebatePerUnit"] = Convert.ToDouble(tmptxtRebatePerUnit.Text);
            dt.Rows[i]["CostAfterRebate"] = Convert.ToDouble(tmplblCostAfterRebate.Text);
            dt.Rows[i]["TotalCostAfterRebate"] = Convert.ToDouble(tmplblTotalCostAfterRebate.Text);
            dt.Rows[i]["SelleingPrice"] = Convert.ToDouble(tmptxtSelleingPrice.Text);
            dt.Rows[i]["TotalSelleing"] = Convert.ToDouble(tmplblTotalSelleing.Text);
            dt.Rows[i]["Margin"] = Convert.ToDouble(tmplblMargin.Text);
            //dt.Rows[i]["OrderType"] = tmptxtOrderType.Text;
            
            if(tmpDdlOrderType.Items.Count>0)
            dt.Rows[i]["OrderType"] = tmpDdlOrderType.SelectedItem.Text;
            
            dt.Rows[i]["Comments"] = tmptxtComments.Text;

            i++;
        }

        if (deleteRow >= 0)
        {
            //delete new row
            dt.Rows.RemoveAt(deleteRow);
            deleteRow = -1;

            i = 0;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Serial"] = (i + 1);
            }
        }
        else
        {
            //dt.Rows.Add((i + 1), "", "", 0, "");
            dt.Rows.Add((i + 1),"AP50", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "");
        }

        ViewState["CurrentTable"] = dt;

        GridView1.DataSource = (DataTable)ViewState["CurrentTable"];
        GridView1.DataBind();

        LoadGridDdlPartNo();
    }
    
    static DataTable GetTableAtLoadOnly()
    {
        //Serial,PartNo,Description,Qty,CurrPrice,AmountTotal,RebatePerUnit,CostAfterRebate,TotalCostAfterRebate,SelleingPrice,TotalSelleing,Margin,OrderType,Comments

        DataTable tbl = new DataTable();
        tbl.Columns.Add("Serial", typeof(int));
        tbl.Columns.Add("txtFind", typeof(string));
        tbl.Columns.Add("PartNo", typeof(string));
        tbl.Columns.Add("Description", typeof(string));
        tbl.Columns.Add("Qty", typeof(int));
        tbl.Columns.Add("CurrPrice", typeof(double));
        tbl.Columns.Add("AmountTotal", typeof(double));
        tbl.Columns.Add("RebatePerUnit", typeof(double));
        tbl.Columns.Add("CostAfterRebate", typeof(double));
        tbl.Columns.Add("TotalCostAfterRebate", typeof(double));
        tbl.Columns.Add("SelleingPrice", typeof(double));
        tbl.Columns.Add("TotalSelleing", typeof(double));
        tbl.Columns.Add("Margin", typeof(double));
        tbl.Columns.Add("OrderType", typeof(string));
        tbl.Columns.Add("Comments", typeof(string));

        int rws = 2;

        for (int i = 1; i <= rws; i++)
            tbl.Rows.Add(i, "AP50", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "");

        return tbl;
    }

    protected void imgBtnFind_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;

        if (ddlvendorGlb.Items.Count <= 0)
            return;

        DropDownList tmpddlPartNo;
        //DropDownList tmpDdlOrderType;
        TextBox tmptxtFindPartLike;
        Label tmpItemsCnt, tmplblFindPartLike;

        foreach (GridViewRow rw in GridView1.Rows)
        {
            Control ctrl = rw.FindControl("imgBtnFind") as ImageButton;
            if (ctrl != null)
            {

                ImageButton btn1 = (ImageButton)ctrl;
                if (btn.ClientID == btn1.ClientID)
                {
                    tmpddlPartNo = (rw.FindControl("ddlPartNo") as DropDownList);
                    tmptxtFindPartLike = (rw.FindControl("txtFindPartLike") as TextBox);
                    tmpItemsCnt = (rw.FindControl("lblItemsCnt") as Label);
                    tmplblFindPartLike = (rw.FindControl("lblFindPartLike") as Label);

                    if (tmptxtFindPartLike.Text.Trim().Length == 0)
                        tmptxtFindPartLike.Text = "AP50";

                    if (tmpddlPartNo != null)
                    {
                        //Db.LoadDDLsWithCon(tmpddlPartNo, "Select StockLink,Part=csimpleCode+' : '+Description_1 From stkItem Where ItemGroup='" + lblItemGroup.Text + "' Order By Part", "Part", "StockLink", myGlobal.getConnectionStringForDB(BaseDBCode));
                        Db.LoadDDLsWithCon(tmpddlPartNo, "Select StockLink,Part=Code+' : '+Description_1 From stkItem Where ItemGroup='" + lblItemGroup.Text + "' and cSimpleCode like '" + tmptxtFindPartLike.Text + "%' Order By Part", "Part", "StockLink", myGlobal.getConnectionStringForDB(BaseDBCode));
                        //lblItemsCountForSelectedGroup.Text = " Items In List : " + tmpddlPartNo.Items.Count.ToString();

                        tmpItemsCnt.Text = tmpddlPartNo.Items.Count.ToString();
                        TextBox tmptxtpartno = (TextBox)rw.FindControl("txtPartNo");
                        TextBox tmptxtdesc = (TextBox)rw.FindControl("txtDescription");

                        tmptxtpartno.Text = ""; //remove old part data
                        tmptxtdesc.Text = "";

                        tmplblFindPartLike.Text = tmptxtFindPartLike.Text;
                        //int txxxx= tmpddlPartNo.Items.Count;
                        //if (tmptxtpartno == null || tmptxtpartno.Text.Trim() == "")
                        //{
                            if (tmpddlPartNo.Items.Count > 0)
                            {
                                string pNo, Desc;
                                string[] separator = new string[] { " : " };
                                string[] arr = tmpddlPartNo.SelectedItem.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                                pNo = arr[0];
                                Desc = arr[1];

                                tmptxtpartno.Text = pNo;
                                tmptxtdesc.Text = Desc;
                            }
                        //}

                            if (tmpddlPartNo.Items.Count > 0)
                            {
                                if (tmptxtpartno != null && tmptxtpartno.Text.Trim() != "")
                                {
                                    int idx = 0;
                                    for (int i = 0; i < tmpddlPartNo.Items.Count; i++)
                                    {
                                        if (tmpddlPartNo.Items[i].Text.IndexOf(tmptxtpartno.Text) > -1)
                                        {
                                            idx = i;
                                            break;
                                        }
                                    }
                                    tmpddlPartNo.SelectedIndex = idx;
                                }
                            }
                    }
                }
            }
        }

        //DataTable dt;
        //dt = (DataTable)ViewState["CurrentTable"];

        //foreach (GridViewRow rw in GridView1.Rows)
        //{
        //    tmpDdlOrderType = (rw.FindControl("ddlOrderType") as DropDownList);

        //    tmpDdlOrderType.Items.Clear();
        //    tmpDdlOrderType.Items.Add(lblPOType.Text);
        //    if (dt.Rows[rw.RowIndex]["OrderType"].ToString() == "")
        //    {
        //        tmpDdlOrderType.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        tmpDdlOrderType.Items.FindByText(dt.Rows[rw.RowIndex]["OrderType"].ToString()).Selected = true;
        //    }
        //}

    }

    private void LoadGridDdlPartNo()
    {
        if (ddlvendorGlb.Items.Count <= 0)
            return;

        DropDownList tmpddlPartNo;
        DropDownList tmpDdlOrderType;
        TextBox tmptxtFindPartLike;
        Label tmpItemsCnt, tmplblFindPartLike;
        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmpddlPartNo = (rw.FindControl("ddlPartNo") as DropDownList);
            tmptxtFindPartLike = (rw.FindControl("txtFindPartLike") as TextBox);
            tmpItemsCnt = (rw.FindControl("lblItemsCnt") as Label);
            tmplblFindPartLike = (rw.FindControl("lblFindPartLike") as Label);

            if (tmptxtFindPartLike.Text.Trim().Length == 0)
                tmptxtFindPartLike.Text = "AP50";

            if (tmpddlPartNo != null)
            {
                //Db.LoadDDLsWithCon(tmpddlPartNo, "Select StockLink,Part=csimpleCode+' : '+Description_1 From stkItem Where ItemGroup='" + lblItemGroup.Text + "' Order By Part", "Part", "StockLink", myGlobal.getConnectionStringForDB(BaseDBCode));
                Db.LoadDDLsWithCon(tmpddlPartNo, "Select StockLink,Part=Code+' : '+Description_1 From stkItem Where ItemGroup='" + lblItemGroup.Text + "' and cSimpleCode like '" + tmptxtFindPartLike.Text + "%' Order By Part", "Part", "StockLink", myGlobal.getConnectionStringForDB(BaseDBCode));
                //lblItemsCountForSelectedGroup.Text = " Items In List : " + tmpddlPartNo.Items.Count.ToString();
                
                tmpItemsCnt.Text = tmpddlPartNo.Items.Count.ToString();
                TextBox tmptxtpartno = (TextBox)rw.FindControl("txtPartNo");
                TextBox tmptxtdesc = (TextBox)rw.FindControl("txtDescription");

                tmplblFindPartLike.Text = tmptxtFindPartLike.Text;

                //int txxxx= tmpddlPartNo.Items.Count;
                if (tmptxtpartno == null || tmptxtpartno.Text.Trim() == "")
                {
                    if (tmpddlPartNo.Items.Count > 0)
                    {
                        string pNo, Desc;
                        string[] separator = new string[] { " : " };
                        string[] arr = tmpddlPartNo.SelectedItem.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        pNo = arr[0];
                        Desc = arr[1];

                        tmptxtpartno.Text = pNo;
                        tmptxtdesc.Text = Desc;
                    }
                }

                if (tmpddlPartNo.Items.Count > 0)  //this condition was added on 23-jan-2014 by vishav
                {
                    if (tmptxtpartno != null && tmptxtpartno.Text.Trim() != "")
                    {
                        int idx = 0;
                        for (int i = 0; i < tmpddlPartNo.Items.Count; i++)
                        {
                            if (tmpddlPartNo.Items[i].Text.IndexOf(tmptxtpartno.Text) > -1)
                            {
                                idx = i;
                                break;
                            }
                        }
                        tmpddlPartNo.SelectedIndex = idx;
                    }
                }
            }
        }

        DataTable dt;
        dt = (DataTable)ViewState["CurrentTable"];

        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmpDdlOrderType = (rw.FindControl("ddlOrderType") as DropDownList);

            tmpDdlOrderType.Items.Clear();
            tmpDdlOrderType.Items.Add(lblPOType.Text);
            if (dt.Rows[rw.RowIndex]["OrderType"].ToString() == "")
            {
                tmpDdlOrderType.SelectedIndex = 0;
            }
            else
            {
                tmpDdlOrderType.Items.FindByText(dt.Rows[rw.RowIndex]["OrderType"].ToString()).Selected = true;
            }
        }
       
    }

    //private void LoadGridDdlPartNo1()
    //{

    //    DropDownList ddltmp;
    //    TextBox tmptxtPartNO;

    //    foreach (GridViewRow rw in GridView1.Rows)
    //    {
    //        tmptxtPartNO = (rw.FindControl("txtPartNo") as TextBox);
    //        ddltmp = (rw.FindControl("ddlPartNo") as DropDownList);
    //        if (ddltmp != null)
    //        {
    //            if (tmptxtPartNO != null && tmptxtPartNO.Text.Trim() != "")
    //            {
    //                int idx = 0;
    //                for (int i = 0; i < ddltmp.Items.Count; i++)
    //                {
    //                    if (ddltmp.Items[i].Text.IndexOf(tmptxtPartNO.Text) > -1)
    //                    {
    //                        idx = i;
    //                    }
    //                }
    //                ddltmp.SelectedIndex = idx;
    //            }
    //            //ddltmp.Items.FindByText(tmptxtPartNO.Text).Selected = true;
    //        }
    //    }
    //}

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

    protected void ddlvendorGlb_SelectedIndexChanged(object sender, EventArgs e)
    {
        getVendorNameFromSelectedItem();

        TextBox tmptxtpartno, tmptxtdesc,tmptxtFindPartLike;
        Label tmpItemsCnt;
        DropDownList tmpddl;

        //freshDataGrid();
        foreach (GridViewRow rw in GridView1.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                tmptxtFindPartLike = (rw.FindControl("txtFindPartLike") as TextBox);

                if (tmptxtFindPartLike.Text.Trim().Length == 0)
                    tmptxtFindPartLike.Text = "AP50";

                tmpddl = (DropDownList)rw.FindControl("ddlPartNo");
                //Db.LoadDDLsWithCon(tmpddl, "Select StockLink,Part=csimpleCode+' : '+Description_1 From stkItem Where ItemGroup='" + lblItemGroup.Text + "' Order By Part", "Part", "StockLink", myGlobal.getConnectionStringForDB(BaseDBCode));
                Db.LoadDDLsWithCon(tmpddl, "Select StockLink,Part=Code+' : '+Description_1 From stkItem Where ItemGroup='" + lblItemGroup.Text + "' and cSimpleCode like '" + tmptxtFindPartLike.Text + "%' Order By Part", "Part", "StockLink", myGlobal.getConnectionStringForDB(BaseDBCode));

                //lblItemsCountForSelectedGroup.Text = " Items In List : " + tmpddl.Items.Count.ToString();
                tmptxtpartno = (TextBox)rw.FindControl("txtPartNo");
                tmptxtpartno.Text = "";
                tmptxtdesc = (TextBox)rw.FindControl("txtDescription");
                tmptxtdesc.Text = "";
                tmpItemsCnt = (rw.FindControl("lblItemsCnt") as Label);
                tmpItemsCnt.Text = tmpddl.Items.Count.ToString();

                if (tmpddl.Items.Count > 0)
                {
                    //char[] separator = new char[] { ':' };
                    string pNo, Desc;
                    string[] separator = new string[] { " : " };
                    string[] arr = tmpddl.SelectedItem.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    pNo = arr[0];
                    Desc = arr[1];

                    tmptxtpartno.Text = pNo;    
                    tmptxtdesc.Text = Desc;
                }
            }
        }
    }
    protected void ddlPartNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        foreach (GridViewRow row in GridView1.Rows)
        {

            DropDownList ddl1 = row.FindControl("ddlPartNo") as DropDownList;
            if (ddl1 != null)
            {
                if (ddl.ClientID==ddl1.ClientID)
                {
                    if (ddl.Items.Count > 0)
                    {
                        string pNo, Desc;
                        string[] separator = new string[] { " : " };
                        string[] arr = ddl1.SelectedItem.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        pNo = arr[0];
                        Desc = arr[1];

                        TextBox tmptxtpartno = (TextBox)row.FindControl("txtPartNo");
                        tmptxtpartno.Text = pNo;
                        TextBox tmptxtdesc = (TextBox)row.FindControl("txtDescription");
                        tmptxtdesc.Text = Desc;
                    }
                }

            }
        }
    }
    public Byte[] GetFileContent(System.IO.Stream inputstm)
    {
        Stream fs = inputstm;
        BinaryReader br = new BinaryReader(fs);
        Int32 lnt = Convert.ToInt32(fs.Length);
        byte[] bytes = br.ReadBytes(lnt);
        return bytes;
    }

    private void SaveFileAtWebsiteLocation(string saveFileAtWebSitePath)
    {

        String pth, dirPhyPth;
        pth = saveFileAtWebSitePath + poId.ToString() + "-" + lblPOType.Text + "/";

        dirPhyPth = Server.MapPath("~" + pth);

        if (!System.IO.Directory.Exists(dirPhyPth))
        {
            System.IO.Directory.CreateDirectory(dirPhyPth);
        }

        saveFileAtWebSitePath = pth;  //new path


        String phySavePth;
        HttpPostedFile postFile;

        string ImageName = string.Empty;

        byte[] path;

        string[] keys;

        fls = "";
        FilesForuploadTrack = "";
        try
        {

            string contentType = string.Empty;

            //byte[] imgContent=null;

            string[] PhotoTitle;

            string PhotoTitlename, trimmedNameWithExt;
            int pikMaxFileName = myGlobal.trimFileLength;

            HttpFileCollection files = Request.Files;

            keys = files.AllKeys;
            string tmpPth;
            int cnt = 0;
            for (int i = 0; i < files.Count; i++)
            {
                trimmedNameWithExt = "";
                postFile = files[i];
                if (postFile.ContentLength > 0)
                {
                    contentType = postFile.ContentType;
                    path = GetFileContent(postFile.InputStream);
                    ImageName = System.IO.Path.GetFileName(postFile.FileName);
                    PhotoTitle = ImageName.Split('.');
                    PhotoTitlename = PhotoTitle[0];

                    if (PhotoTitlename.Length > pikMaxFileName)
                        PhotoTitlename = PhotoTitlename.Substring(0, pikMaxFileName);

                    trimmedNameWithExt = PhotoTitlename + "." + PhotoTitle[1];

                    cnt++;

                    tmpPth = "";
                    tmpPth = Session["processAbbr"].ToString() + "-" + refId + "-" + Session["currStatusName"].ToString() + "-" + myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-FL" + cnt.ToString() + "-";
                    //tmpPth = myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-FL" + cnt.ToString() + "-";

                    phySavePth = Server.MapPath("~" + saveFileAtWebSitePath + tmpPth) + trimmedNameWithExt;
                    postFile.SaveAs(phySavePth);

                    if (fls.Trim() == "")
                        fls = phySavePth;
                    else
                        fls += ";" + phySavePth;

                    if (FilesForuploadTrack.Trim() == "")
                        FilesForuploadTrack = saveFileAtWebSitePath + tmpPth + trimmedNameWithExt;
                    else
                        FilesForuploadTrack += ";" + saveFileAtWebSitePath + tmpPth + trimmedNameWithExt;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}