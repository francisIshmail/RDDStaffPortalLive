<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="FunnelSetup.aspx.cs" Inherits="IntranetNew_Funnel_FunnelSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Funnel Setup </Lable>
    </td>
</tr>

<tr>
        <td width="50%">
           &nbsp;
        </td>
        <td width="50%">
        &nbsp;
        </td>
</tr>

<tr>
        <td width="100%" align="center">
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlsetup" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="90%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="35%">
           &nbsp;
        </td>
        <td width="45%">
        &nbsp;
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>
    
 
      <tr>
        
        <td width="35%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Sales Person  &nbsp; </label>  
        </td>
        <td width="45%">
            <asp:DropDownList ID="ddlSalesperson" runat="server" AutoPostBack="true"
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onselectedindexchanged="ddlSalesperson_SelectedIndexChanged">
            </asp:DropDownList>
            
        </td>
        <td width="20%">
            <asp:Label ID="lblFunnelSetupId" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>

      <tr>
        <td width="35%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Designation   &nbsp;  </label>
        </td>
        <td width="65%" colspan="2" >
        <asp:Label ID="lblDesignation" runat="server" style="color:Red;font-weight:bold;font-family: Raleway;font-size:12px" ></asp:Label>

          <asp:Label ID="lblMembershipUserName" runat="server" style="color:Red;font-weight:bold;font-family: Raleway;font-size:12px" Visible="false" ></asp:Label>

         <%--   <asp:RadioButtonList ID="rddListDesign" runat="server" Enabled="false"
                                   RepeatDirection="Horizontal"  RepeatColumns="3"   Font-Size="Small" ></asp:RadioButtonList>--%>
        </td>
    </tr>

    <tr >
       
        <td width="35%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Country   &nbsp;  </label>
        </td>
        <td width="65%" colspan="2">
            <asp:RadioButtonList ID="rddlistcountries" runat="server" AutoPostBack="true"
                                   RepeatDirection="Horizontal"  RepeatColumns="4"   Font-Size="Small" 
                onselectedindexchanged="rddlistcountries_SelectedIndexChanged" ></asp:RadioButtonList>
        </td>
    </tr>
 
     <tr>
        <td width="80%" align="left" colspan="2">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnSave_Click" />  &nbsp;&nbsp; 
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel"  style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " />
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

    <tr>
        <td width="100%" colspan="3" >
             <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="False" GridLines="None" 
                ForeColor="#333333"  Width="100%"  onrowdatabound="GridView1_RowDataBound" 
                 CellPadding="4" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="BU"  
                            ItemStyle-Width="50%">
                            <ItemTemplate>
                                <asp:Label ID="lblBU" runat="server" Font-Size="Small" Text='<%#Eval("BU")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="Small" CssClass="gvHeaderBUCenter" />
                            <ItemStyle CssClass="gvBUCenter" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Access" 
                            ItemStyle-Width="50%">
                            <ItemTemplate>
                                <asp:Label ID="lblAccess" runat="server" Text='<%# Eval("Access") %>' 
                                    Visible="false" />
                                <asp:DropDownList ID="ddlAccess" runat="server" Font-Size="Small" Width="150px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="Small" />
                            <ItemStyle Width="50%" />
                        </asp:TemplateField>
                    </Columns>

                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />

                </asp:GridView>
        </td>    
    </tr>

    <tr>
        
        <td align="left" colspan="3">
           &nbsp; 
        </td>
    </tr>

</table>

</asp:Panel>

  </td>
</tr>

</table>


<style type="text/css">
    .gvBUCenter { text-align: center; }
    .gvHeaderBUCenter { text-align: center; }
</style>

</asp:Content>

