<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="AddDatabaseColumn.aspx.cs" Inherits="Intranet_EVO_AddDatabaseColumn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<%--MAIN TABLES--%>

    <table width="100%"> 
        <tr>
            <td align="center" width="80%" bgcolor="#800000">
                <h1><font color="white">Add To Database</font></h1>
            </td>
        </tr>
    </table>
     
    <br />
       
    <asp:Panel ID="pnlMain" Runat="server" BackColor="#cccccc">
        
        
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <table width="100%">
                    <tr>
                        <td align="right" width="70%">
                            <asp:Label ID="lblMsg1" runat="server" 
                                Text="Select A Database To Connect" Font-Bold="True" Font-Size="15px"></asp:Label>
                                    <asp:DropDownList ID="ddlDB" runat="server" Width="250px" 
                                AutoPostBack="True" Font-Bold="true" BackColor="#F2F2F2" 
                                onselectedindexchanged="ddlDB_SelectedIndexChanged" >
                                        <asp:ListItem Value="JA">Triangle</asp:ListItem>
                                        <asp:ListItem Value="TZ">RedDotTanzania</asp:ListItem>
                                        <asp:ListItem Value="EPZ">RED DOT DISTRIBUTION EPZ LTD</asp:ListItem>
                                        <asp:ListItem Value="KE">Red Dot Distribution Limited - Kenya</asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnConnect" runat="server" Font-Bold="true" ForeColor="#990033" 
                                    onclick="btnConnect_Click" Text="Connect To Database" width="160px" />
                            </td>                      
                            
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblSuccess" runat="server" Font-Bold="True" ForeColor="#003399" 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            
                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Underline="True" 
                                ForeColor="#003366" 
                                Text="Database Connected Please Fill Data And Click Create Button" 
                                Visible="False"></asp:Label>
                            
                        </td>
                    </tr>
                </table>
        

                <%--MAIN TABLES--%>

                <table width="100%">
                    <tr style="height:40px" >
                        <td valign="top">
                            <asp:Panel ID="pnlView" runat="server" GroupingText="View Details" 
                                Enabled="False">
                                <table width="100%" align="center">
                                    <tr valign="top">
                                        <td align="right">
                                            <b>Select Table:-</b>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlTableName" runat="server" Width="155px" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlTableName_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                            <asp:Label ID="lblTableCount" runat="server" Font-Bold="True" 
                                                ForeColor="#003366" Visible="False"></asp:Label>
                                        </td>

                                        <td align="right">
                                            <b>Column List:-</b>
                                        </td>
                                
                                        <td>
                                            <asp:DropDownList ID="ddlColumnName" runat="server" Width="155px" 
                                                AutoPostBack="True" style="margin-left: 0px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblColumnCount" runat="server" Font-Bold="True" 
                                                ForeColor="#003366" Visible="False"></asp:Label>
                                        </td>

                                        <td align="right">
                                            <b>Action:-</b>
                                        </td>
                                
                                        <td>
                                            <asp:DropDownList ID="ddlAction" runat="server" Width="155px" 
                                                AutoPostBack="True" style="margin-left: 0px">
                                                <asp:ListItem>Update</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>

                <table width="100%">
                    <tr>
                        <td>
                            &nbsp<asp:Label ID="lblError" runat="server" Font-Bold="True" 
                                ForeColor="Maroon"></asp:Label>
                        </td>
                    </tr>
                </table>



                <table width="100%">
                    <tr>
                        <td width="65%">
                            <asp:Panel ID="pnlAddField" runat="server" GroupingText="Add Field" Enabled="False">
                                <table width="100%" style="height:170px">
                                    <tr>
                                        <td align="left" style="width:20%">
                                            <b>Column Name:-</b>
                                        </td>
                                        <td style="width:80%" colspan="4">
                                            <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="width:20%">
                                            <asp:Label ID="Label1" runat="server" Text="Datatype" Font-Bold="True"></asp:Label>
                                        </td>

                                        <td style="width:20%">
                                            <asp:Label ID="Label2" runat="server" Text="Data Size" Font-Bold="True"></asp:Label>
                                        </td>

                                        <td style="width:20%">
                                            <asp:Label ID="Label3" runat="server" Text="Default Value" Font-Bold="True"></asp:Label>
                                        </td>

                                        <td style="width:20%">
                                            <asp:Label ID="Label4" runat="server" Text="Auto Increment" Font-Bold="True"></asp:Label>
                                        </td>

                                        <td style="width:20%">
                                            <asp:Label ID="Label5" runat="server" Text="Null/Not Null" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlDataType" runat="server" Font-Bold="True" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlDataType_SelectedIndexChanged">
                                                    <asp:ListItem>nchar</asp:ListItem>
                                                    <asp:ListItem>nvarchar</asp:ListItem>
                                                    <asp:ListItem>text</asp:ListItem>
                                                    <asp:ListItem>int</asp:ListItem>
                                                    <asp:ListItem>bit</asp:ListItem>
                                                    <asp:ListItem>datetime</asp:ListItem>
                                                    <asp:ListItem>numeric</asp:ListItem>
                                                </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtSize" runat="server" Width="100px"></asp:TextBox>
                                            <asp:Label ID="lblnotApplicable" runat="server" Text="Not Applicable" width="100px" Visible="False"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtDefault" runat="server" width="100px"></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlIncrement" runat="server" width="100px" 
                                                    AutoPostBack="True" Visible="False"  
                                                    onload="ddlIncrement_Load" >
                                                    <asp:ListItem Value="IDENTITY">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNotApplicable1" runat="server" Text="Not Applicable" width="100px"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlNull" runat="server" width="100px" 
                                                    AutoPostBack="True" >
                                                    <asp:ListItem>NULL</asp:ListItem>
                                                    <asp:ListItem Value="NOT NULL">NOT NULL</asp:ListItem>
                                                </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFirst" runat="server" Text="First Value" Font-Bold="True" Visible="False"></asp:Label>
                                        </td>

                                        <td colspan="4">
                                            <asp:Label ID="lblDecimal" runat="server" Text="Decimal Value" Font-Bold="True" Visible="False"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNumeric1" runat="server" Width="100px" Visible="False"></asp:TextBox>
                                        </td>

                                        <td colspan="4">
                                            <asp:TextBox ID="txtNumeric2" runat="server" Width="100px" Visible="False"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:Button ID="Button1" runat="server" Text="Add Column" width="130px" 
                                                ForeColor="#990033" Font-Bold="true" onclick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>


                        <td width="35%">
                            <asp:Panel ID="pnlDatabase" runat="server" GroupingText="Select Databases To Make Changes" 
                                Enabled="False" >
                                <table style="height:170px" width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:100%">
                                            <asp:CheckBox ID="chkDU" runat="server" Checked="True" Text="Dubai Database" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkKE" runat="server" Checked="True" Text="Kenya Database" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkEPZ" runat="server" Checked="True" Text="EPZ Database" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkTZ" runat="server" Checked="True" Text="Tanzania Database" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
        
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    


</asp:Content> 

