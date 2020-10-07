using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

public partial class Intranet_EVO_AddDatabaseColumn : System.Web.UI.Page
{
    public static string constr;
    SqlConnection con = new SqlConnection();

    public string DataType;
    public string DataSize;
    public string DefaultValue;
    public string Increment;
    public string IsNull;
    public string ColumnName;
    public string query;
    public string query1;
    public string ActiveUser;
    public string JAstatus="Not Updated";
    public string TZstatus = "Not Updated";
    public string KEstatus = "Not Updated";
    public string EPZstatus = "Not Updated";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           
        }
    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        try
        {
            connect();
            getTables();
            con.Close();
        }
        catch (Exception ex)
        {
            Message.Show(Page, ex.Message);
        }
        lblMsg.Visible = true;
        pnlAddField.Enabled = true;
        pnlDatabase.Enabled = true;
        pnlView.Enabled = true;

        lblColumnCount.Visible = true;
        lblTableCount.Visible = true;
        lblTableCount.Text ="" + ddlTableName.Items.Count.ToString() + " Table(s)";
        lblColumnCount.Text = "" + ddlColumnName.Items.Count.ToString() + " Column(s)";


    }


    public void connect()
    {
        string DBCode;
        DBCode = ddlDB.SelectedValue.ToString();
        constr = myGlobal.getConnectionStringForDB(DBCode);
        con.ConnectionString = constr;
    }

    public void getTables()
    {
        string query;
        query = "SELECT * FROM [" + ddlDB.SelectedItem.ToString() + "].sys.tables order by name asc";
        Db.LoadDDLsWithCon(ddlTableName, query, "name", "name", constr);
        ddlTableName.SelectedIndex = 0;
        loadcolumn();
        //DataSet ds = new DataSet();
        //Db.constr = constr;
        //ds = Db.myGetDS(query);
        //ddlTableName.DataSource = ds.Tables[0];
        //ddlTableName.DataBind();
    }


    public void loadcolumn()
    {
        string query;
        con.ConnectionString = constr;
        query = "select column_name from information_schema.columns where table_name = '" + ddlTableName.SelectedItem.ToString() + "'order by column_name";
        Db.LoadDDLsWithCon(ddlColumnName, query, "column_name", "column_name", constr);
    }


    protected void ddlTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadcolumn();
        lblColumnCount.Text = "" + ddlColumnName.Items.Count.ToString() + " Column(s)";
    }


    protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear();
        lblMsg.Visible = false;
        pnlView.Enabled = false;
        pnlAddField.Enabled = false;
        pnlDatabase.Enabled = false;
        lblColumnCount.Visible = false;
        lblTableCount.Visible = false;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        ActiveUser = myGlobal.loggedInUser();

        /////////DATABASE ARRAY/////////
        ArrayList arrDB = new ArrayList(4);

        string[] database = new string[4];

        if (chkDU.Checked == true)
        {
            arrDB.Add("JA");
        }
        if (chkTZ.Checked == true)
        {
            arrDB.Add("TZ");
        }
        if (chkKE.Checked == true)
        {
            arrDB.Add("KE");
        }
        if (chkEPZ.Checked == true)
        {
            arrDB.Add("EPZ");
        }


        //////////////REQUIRED CHECKS///////////////

        if (txtName.Text == "")
        {
            lblError.Text = "Column Name Can't Be Left Blank";
            return;
        }
        else
        {
            ListItem tt = ddlColumnName.Items.FindByText(txtName.Text);
            if (tt != null)
            {
                lblError.Text = "Same Column Name Exist In Table Use A Different Column Name";
                return;
            }
        }



        ///////DATATYPE CHECKS/////


        if (ddlDataType.SelectedItem.Text == "nvarchar")
        {
            if (txtSize.Text == "")
            {
                lblError.Text = "Please Specify Data Size";
                return;
            }
            else if (!Util.isValidNumber(txtSize.Text))
            {
                if (txtSize.Text != "MAX")
                {
                    lblError.Text = "Please supply a valid numeric value for Datasize  ";
                    return;
                }
            }

            else if (Util.isValidNumber(txtDefault.Text))
            {
                lblError.Text = "Default Value Should Be A String";
                return;
            }
            else if (Convert.ToInt32(txtSize.Text) < 1 || Convert.ToInt32(txtSize.Text) > 255)
            {
                lblError.Text = "Size Should Be Greater Than 0 And Less Than 255 Characters";
                return;
            }
        }

        else if (ddlDataType.SelectedItem.Text == "nchar")
        {
            if (txtSize.Text == "")
            {
                lblError.Text = "Please Specify Data Size";
                return;
            }

            else if (!Util.isValidNumber(txtSize.Text))
            {
                    lblError.Text = "Please supply a valid numeric value for Datasize  ";
                    return;
            }

            else if (Util.isValidNumber(txtDefault.Text))
            {
                lblError.Text = "Default Value Should Be A String";
                return;
            }

            else if (Convert.ToInt32(txtSize.Text) < 1 || Convert.ToInt32(txtSize.Text) > 255)
            {
                lblError.Text = "Size Should Be Greater Than 0 And Less Than 255 Characters";
                return;
            }
        }
        else if (ddlDataType.SelectedItem.Text == "text")
        {
            if (Util.isValidNumber(txtDefault.Text))
            {
                lblError.Text = "Default Value Should Be A String";
                return;
            }
        }
        else if (ddlDataType.SelectedItem.Text == "int")
        {
            if (!Util.isValidNumber(txtDefault.Text))
            {
                if (txtDefault.Text != "")
                {
                    lblError.Text = "Please supply a valid numeric value for Default value  ";
                    return;
                }
            }
        }
        else if (ddlDataType.SelectedItem.Text == "bit")
        {
            if (txtDefault.Text != "0" && txtDefault.Text != "1")
            {
                if (txtDefault.Text != "")
                {
                    lblError.Text = "Only 0 or 1 Could Be Given As Default Value For INT Datatype";
                    return;
                }
            }
        }
        else if (ddlDataType.SelectedItem.Text == "datetime")
        {
            if (!Util.IsValidDate(txtDefault.Text))
            {
                if (txtDefault.Text != "")
                {
                    lblError.Text = "Please supply a valid date for Default date(mm-dd-yyyy)";
                    return;
                }
            }
        }
        else if (ddlDataType.SelectedItem.Text == "numeric")
        {
            if (txtNumeric1.Text == "" || txtNumeric2.Text == "")
            {
                lblError.Text = "Please Supply Size In Both Fields";
                return;
            }
            else if (!Util.isValidDecimalNumber(txtDefault.Text))
            {
                if (txtDefault.Text != "")
                {
                    lblError.Text = "Please supply a valid numeric value for Default value  ";
                    return;
                }

                if (Convert.ToInt32(txtNumeric1.Text) < 1 || Convert.ToInt32(txtNumeric1.Text) > 20)
                {
                    lblError.Text = "Value Can't be Less Than 1 And Greater Than 20";
                    return;
                }
                if (Convert.ToInt32(txtNumeric2.Text) < 0 || Convert.ToInt32(txtNumeric2.Text) > 10)
                {
                    lblError.Text = "Decimal Value cant Be Less Than 0 And Greater Than 10";
                    return;
                }
            }
        }




        ///////////QUERIES//////////////



        if (ddlDataType.SelectedItem.Value == "text")
        {
            DataType = ddlDataType.SelectedItem.Value;
            DefaultValue = txtDefault.Text;
            IsNull = ddlNull.SelectedItem.Text;
            ColumnName = txtName.Text;
            if (ddlNull.SelectedItem.Text == "NULL")
            {
                if (txtDefault.Text == "")
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " " + IsNull + "";
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " DEFAULT '" + DefaultValue + "' ";
                }
            }
            else if (ddlNull.SelectedItem.Text == "NOT NULL")
            {
                if (txtDefault.Text == "")
                {
                    lblError.Text = "Default Value Can't Be Null For Not Null Column";
                    return;
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                }
            }
        }


        else if (ddlDataType.SelectedItem.Text == "int")
        {

            DataType = ddlDataType.SelectedItem.Value;
            DefaultValue = txtDefault.Text;
            IsNull = ddlNull.SelectedItem.Text;
            Increment = ddlIncrement.SelectedItem.Value;
            ColumnName = txtName.Text;
            if (ddlIncrement.SelectedItem.ToString() == "Yes")
            {
                query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " " + Increment + "";
            }
            else if (ddlIncrement.SelectedItem.ToString() == "No")
            {
                if (ddlNull.SelectedItem.Text.ToUpper() == "NOT NULL")
                {
                    if (txtDefault.Text == "")
                    {
                        lblError.Text = "Default Value Can't Be Null For Not Null Column";
                        return;
                    }
                    else
                    {
                        query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                    }
                }
                else if (ddlNull.SelectedItem.Text.ToUpper() == "NULL")
                {
                    if (txtDefault.Text == "")
                    {
                        query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + DataSize + ") " + IsNull + " ";
                    }
                    else
                    {
                        query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " DEFAULT '" + DefaultValue + "' " + IsNull + " ";
                    }
                }
            }

        }


        else if (ddlDataType.SelectedItem.Text == "nchar")
        {
            DataType = ddlDataType.SelectedItem.Value;
            DefaultValue = txtDefault.Text;
            DataSize = txtSize.Text;
            IsNull = ddlNull.SelectedItem.Text;
            ColumnName = txtName.Text;

            if (ddlNull.SelectedItem.Text == "NULL")
            {
                if (txtDefault.Text == "")
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + DataSize + ") " + IsNull + " ";
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " (" + DataSize + ") " + IsNull + " DEFAULT '" + DefaultValue + "' ";
                }
            }
            else if (ddlNull.SelectedItem.Text == "NOT NULL")
            {
                if (txtDefault.Text == "")
                {
                    lblError.Text = "Default Value Can't Be Null For Not Null Column";
                    return;
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + DataSize + ") DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                }
            }

        }


        else if (ddlDataType.SelectedItem.Text == "nvarchar")
        {
            DataType = ddlDataType.SelectedItem.Value;
            DefaultValue = txtDefault.Text;
            DataSize = txtSize.Text;
            IsNull = ddlNull.SelectedItem.Text;
            ColumnName = txtName.Text;

            if (ddlNull.SelectedItem.Text == "NULL")
            {
                if (txtDefault.Text == "")
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + DataSize + ") " + IsNull + " ";
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " (" + DataSize + ") " + IsNull + " DEFAULT '" + DefaultValue + "' ";
                }
            }
            else if (ddlNull.SelectedItem.Text == "NOT NULL")
            {
                if (txtDefault.Text == "")
                {
                    lblError.Text = "Default Value Can't Be Null For Not Null Column";
                    return;
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + DataSize + ") DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                }
            }

        }
        else if (ddlDataType.SelectedItem.Text == "bit")
        {
            DataType = ddlDataType.SelectedItem.Value;
            DefaultValue = txtDefault.Text;
            DataSize = txtSize.Text;
            IsNull = ddlNull.SelectedItem.Text;
            ColumnName = txtName.Text;

            if (ddlNull.SelectedItem.Text == "NULL")
            {
                if (txtDefault.Text == "")
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " " + IsNull + " ";
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " " + IsNull + " DEFAULT '" + DefaultValue + "' ";
                }
            }
            else if (ddlNull.SelectedItem.Text == "NOT NULL")
            {
                if (txtDefault.Text == "")
                {
                    lblError.Text = "Default Value Can't Be Null For Not Null Column";
                    return;
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                }
            }

        }


        else if (ddlDataType.SelectedItem.Text == "datetime")
        {
            DataType = ddlDataType.SelectedItem.Text;
            DefaultValue = txtDefault.Text;
            IsNull = ddlNull.SelectedItem.Text;
            ColumnName = txtName.Text;

            if (ddlNull.SelectedItem.Text == "NULL")
            {
                if (txtDefault.Text == "")
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " " + IsNull + " ";
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " " + IsNull + " DEFAULT '" + DefaultValue + "' ";
                }
            }
            else if (ddlNull.SelectedItem.Text == "NOT NULL")
            {
                if (txtDefault.Text == "")
                {
                    lblError.Text = "Default Value Can't Be Null For Not Null Column";
                    return;
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                }
            }

        }
        else if (ddlDataType.SelectedItem.Text == "numeric")
        {
            string FirstValue;
            string DecimalValue;
            DataType = ddlDataType.SelectedItem.Value;
            FirstValue = txtNumeric1.Text;
            DecimalValue = txtNumeric2.Text;
            DefaultValue = txtDefault.Text;
            IsNull = ddlNull.SelectedItem.Text;
            ColumnName = txtName.Text;


            if (ddlNull.SelectedItem.Text == "NULL")
            {
                if (txtDefault.Text == "")
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + FirstValue + "," + DecimalValue + ") " + IsNull + " ";
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + " (" + FirstValue + "," + DecimalValue + ") " + IsNull + " DEFAULT '" + DefaultValue + "' ";
                }
            }
            else if (ddlNull.SelectedItem.Text == "NOT NULL")
            {
                if (txtDefault.Text == "")
                {
                    lblError.Text = "Default Value Can't Be Null For Not Null Column";
                    return;
                }
                else
                {
                    query = "alter table " + ddlTableName.SelectedItem.ToString() + " ADD " + ColumnName + " " + DataType + "(" + FirstValue + "," + DecimalValue + ") DEFAULT '" + DefaultValue + "' " + ddlNull.SelectedItem.Text + " ";
                }
            }
        }

        ////////MAIN INSERTIONS//////

        for (int i = 0; i < arrDB.Count; i++)
        {
            int cmdExc = 0;
            bool cmdStatus;
            string DBCode;
            DBCode = arrDB[i].ToString();
            constr = myGlobal.getConnectionStringForDB(DBCode);


            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.ExecuteNonQuery();
                cmdStatus = true;
                cmdExc += 1;
                con.Close();
            }
            catch (Exception ex)
            {
                cmdStatus = false;
                lblError.Text = "Entries Could Not Be Done In All Databases Due To Database Error Try Again Or Contact Database Administrator";
            }


            ///////GETTING STATUS//////////



            if (DBCode == "JA")
            {
                if (cmdStatus == true)
                {
                    JAstatus = "Updated";
                }
                else
                {
                    lblError.Text = "Entries Could Not Be Done In All Databases Due To Database Error Try Again Or Contact Database Administrator";
                }
            }
            else if (DBCode == "TZ")
            {
                if (cmdStatus == true)
                {
                    TZstatus = "Updated";
                }
                else
                {
                    lblError.Text = "Entries Could Not Be Done In All Databases Due To Database Error Try Again Or Contact Database Administrator";
                }
            }
            else if (DBCode == "KE")
            {
                if (cmdStatus == true)
                {
                    KEstatus = "Updated";
                }
                else
                {
                    lblError.Text = "Entries Could Not Be Done In All Databases Due To Database Error Try Again Or Contact Database Administrator";
                }
            }
            else if (DBCode == "EPZ")
            {
                if (cmdStatus == true)
                {
                    EPZstatus = "Updated";
                }
                else
                {
                    lblError.Text = "Entries Could Not Be Done In All Databases Due To Database Error Try Again Or Contact Database Administrator";
                }
            }
        }


        query = query.Replace("'", "''");
        query1 = "Insert into [Triangle].dbo.tblLogNewColumn values('" + ddlTableName.SelectedItem.Text + "' , '" + txtName.Text + "' , '" + query + "' , '" + ddlAction.SelectedItem.Text + "' , '" + ActiveUser + "','" + JAstatus + "','" + KEstatus + "','" + EPZstatus + "','" + TZstatus + "','" + DateTime.Now + "'  )";
        constr = myGlobal.getConnectionStringForDB("DU");
        SqlConnection con1 = new SqlConnection(constr);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand(query1, con1);
        cmd1.ExecuteNonQuery();

        pnlView.Enabled = false;
        pnlAddField.Enabled = false;
        pnlDatabase.Enabled = false;

        if (lblError.Text != "Entries Could Not Be Done In All Databases Due To Database Error Try Again Or Contact Database Administrator")
        {
            lblSuccess.Visible = true;
            lblSuccess.Text = "Column:- " + txtName.Text + " Successfully Added To Table:- " + ddlTableName.SelectedItem.Text + "In Selected database(s)";
        }       
    }  

protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDefault.Text="";
        txtNumeric1.Text = "";
        txtNumeric2.Text = "";
        txtSize.Text = "";

        if (ddlDataType.SelectedItem.Text == "int")
        {
            lblnotApplicable.Visible = true;
            txtSize.Visible = false;
            ddlIncrement.Visible = true;
            lblNotApplicable1.Visible = false;
            lblFirst.Visible = false;
            lblDecimal.Visible = false;
            txtNumeric1.Visible = false;
            txtNumeric2.Visible = false;
        }
        else if (ddlDataType.SelectedItem.Text == "datetime")
        {
            lblnotApplicable.Visible = true;
            txtSize.Visible = false;
            lblFirst.Visible = false;
            lblDecimal.Visible = false;
            txtNumeric1.Visible = false;
            txtNumeric2.Visible = false;
        }
        else if (ddlDataType.SelectedItem.Text == "text")
        {
            txtSize.Visible = false;
            txtDefault.Visible = true;
            lblNotApplicable1.Visible = true;
            ddlNull.Visible = true;
            ddlIncrement.Visible = false;
            lblnotApplicable.Visible = true;
            lblFirst.Visible = false;
            lblDecimal.Visible = false;
            txtNumeric1.Visible = false;
            txtNumeric2.Visible = false;
        }
            else if (ddlDataType.SelectedItem.Text == "numeric")
        {
            txtSize.Visible = false;
            txtDefault.Visible = true;
            lblNotApplicable1.Visible = true;
            ddlNull.Visible = true;
            ddlIncrement.Visible = false;
            lblnotApplicable.Visible = true;
            lblFirst.Visible = true;
            lblDecimal.Visible = true;
            txtNumeric1.Visible = true;
            txtNumeric2.Visible = true;
        }
        else if (ddlDataType.SelectedItem.Text == "bit")
        {
            txtSize.Visible = false;
            txtDefault.Visible = true;
            lblNotApplicable1.Visible = true;
            ddlNull.Visible = true;
            ddlIncrement.Visible = false;
            lblnotApplicable.Visible = true;
            lblFirst.Visible = false;
            lblDecimal.Visible = false;
            txtNumeric1.Visible = false;
            txtNumeric2.Visible = false;
        }
        else
        {
            txtSize.Visible = true;
            txtDefault.Visible = true;
            lblNotApplicable1.Visible = true;
            ddlNull.Visible = true;
            ddlIncrement.Visible = false;
            lblnotApplicable.Visible = false;
            lblFirst.Visible = false;
            lblDecimal.Visible = false;
            txtNumeric1.Visible = false;
            txtNumeric2.Visible = false;
        }

    }

        public void clear()
        {
            lblError.Text = "";
            txtDefault.Text = "";
            txtName.Text = "";
            txtSize.Text = "";
        }
        protected void ddlIncrement_Load(object sender, EventArgs e)
        {
            if (ddlDataType.SelectedItem.Text == "int"  && ddlIncrement.SelectedItem.Text=="Yes")
            {
                txtDefault.Enabled = false;
                ddlNull.Enabled = false;
            }
            else
            {
                txtDefault.Enabled = true;
                ddlNull.Enabled = true;
            }
        }
}



