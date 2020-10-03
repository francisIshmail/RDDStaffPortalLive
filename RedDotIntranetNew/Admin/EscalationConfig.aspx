<%@ Page Title="" Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="EscalationConfig.aspx.cs" Inherits="Admin_EscalationConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
--%>        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title">WorkFlow Escalation Email Configurations</div>
                </td>
             </tr>
    </table>

    <div>
      <center>
        <asp:Panel ID="Panel1" runat="server" BorderColor="#006666" BorderStyle="Solid">
        
        
        <table width="80%">
            <tr>
                <td colspan="4">
                    &nbsp;
                    </td>
            </tr>

            <tr>
                <td colspan="4">
                   <h3> <asp:Label ID="lblAddEdit" runat="server" Font-Bold="true" BackColor="" ForeColor="#333399"></asp:Label></h3>
                   <br /><hr /><br />
                   <asp:Label ID="lblError" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                   <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            
            <tr style="height:30px">
                <td valign="top">
                    Process ID:-
                </td>
                <td colspan="3" valign="top">
                    <asp:DropDownList ID="ddlprocessID" runat="server" Width="150px" AutoPostBack="True" onselectedindexchanged="ddlprocessID_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    Process StatusID:-
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlProcessStatusID" runat="server" Width="150px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlProcessStatusID_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    Escalated 1 Days:-
                </td>
                <td>
                    <asp:TextBox ID="txt1stEsclate" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    E-mail ID:-
                </td>
                <td>
                    <asp:TextBox ID="txt1stEmail" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <%--<asp:Label ID="lblErr1stEsclate" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>--%>
                </td>
                <td>
                    
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter A Valid Email ID" ControlToValidate="txt1stEmail" Font-Bold="True" ForeColor="#3366CC" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                    <%--<asp:Label ID="lblErr1stEmail" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>--%>
                </td>
            </tr>

            <tr>
                <td>
                    Escalated 2 Days:-
                </td>
                <td>
                    <asp:TextBox ID="txt2ndEsclate" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    E-mail ID:-
                </td>
                <td>
                    <asp:TextBox ID="txt2ndemail" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <%--<asp:Label ID="lblErr2ndEsclate" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>--%>
                </td>
                <td>
                    
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter A Valid Email ID" ControlToValidate="txt2ndEmail" Font-Bold="True" ForeColor="#3366CC" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                    <%--<asp:Label ID="lblErr2ndEmail" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>--%>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <hr />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    
                    <asp:Button ID="btnAdd" runat="server" Text="Save Data" width="80px" 
                        onclick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" width="80px" 
                        onclick="btnDelete_Click" Enabled="False" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    
                </td>
            </tr>
        </table>
        <br />
        </asp:Panel>
        </center>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

