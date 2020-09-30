<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="RewardPointValue.aspx.cs" Inherits="IntranetNew_Partners_RewardPointValue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Reward Point Value 
         In USD</Lable></td>
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
    
<asp:Panel ID="pnlRewardpointValue" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="40%">
           &nbsp;
        </td>
        <td width="30%">
        &nbsp;
        </td>
        <td width="30%">
       <asp:Label ID="lblRewardValueID" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>
    
 
      <tr>
        
        <td width="40%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Date  &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:TextBox ID="txtFromDt" runat="server" placeholder="MM/DD/YYYY" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
             <cc1:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFromDt" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
        <td width="30%">
            <asp:TextBox ID="txtToDt" runat="server"  placeholder="MM/DD/YYYY" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
            <cc1:CalendarExtender ID="_txtToDtCalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtToDt" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Country   &nbsp;  </label>
        </td>
        <td width="30%" >
              <asp:DropDownList ID="ddlCountry" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
            </asp:DropDownList>
        </td>
         <td width="30%" >
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> One Reward point value in USD   &nbsp;  </label>
        </td>
        <td width="30%" >
           <asp:TextBox ID="txtRewardValue" runat="server"  placeholder="0.00" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
          
        </td>
         <td width="30%" >
        </td>
    </tr>

     <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Remark   &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" >
          <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" 
                style="width:82%; padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        
        </td>
    </tr>

         <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> IsActive   &nbsp;  </label>
        </td>
        <td width="30%" align="left" >
          <asp:CheckBox ID="chkIsActive" runat="server"  style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:CheckBox> 
        </td>
         <td width="30%" >
        </td>
    </tr>



     <tr>
        <td width="80%" align="left" colspan="2">
          &nbsp;&nbsp;  
            <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;" 
                onclick="BtnSave_Click"  />  &nbsp;&nbsp; 
            
                       <asp:Button ID="BtnSearch" runat="server" Text="Search"  
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnSearch_Click" /> &nbsp;&nbsp; 

           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnCancel_Click" />
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

    <tr>
        
        <td align="left" colspan="2">
           &nbsp; 
        </td>
    </tr>

</table>

</asp:Panel>

  </td>
</tr>

<tr>
    <td> &nbsp;</td>
</tr>

<tr>
    <td width="100%" align="center">
        <asp:Panel ID="pnlRewardValueList" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                ForeColor="#333333"  Width="100%" AllowPaging="True" PageSize="20" 
                onselectedindexchanged="Gridview1_SelectedIndexChanged" 
                onpageindexchanging="Gridview1_PageIndexChanging"     >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID">

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server"
                                  Text='<%#Eval("RewardValueId")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenterID" />
                            <HeaderStyle CssClass="gvHeaderCenterID" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="25%" HeaderText="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("Country")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="CountryCode" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCountryCode" runat="server" Text='<%#Eval("CountryCode")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="FromDate">
                            <ItemTemplate>
                                <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="15%" HeaderText="ToDate">
                            <ItemTemplate >
                                <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="20%" HeaderText="Reward Value" >
                            <ItemTemplate>
                                <asp:Label ID="lblRewardValue" runat="server" Text='<%#Eval("RewardValue")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>
                          
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Remark" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>' ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="gvItemCenter" />
                                <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="10%" HeaderText="IsActive">
                            <ItemTemplate>
                                <asp:CheckBox align="center" ID="chkIsActive" runat="server" Checked='<%#Eval("IsActive")%>' Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>
                   
                        <asp:CommandField ShowSelectButton="True" SelectText="Edit" ControlStyle-ForeColor="Blue" ButtonType="Link" ItemStyle-Width="8%" />

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

        </asp:Panel>
    </td>
</tr>



</table>

<style type="text/css">

.gvItemCenter { text-align: left; }
.gvHeaderCenter {  text-align: left; }
.gvSelectButton { ForeColor: Blue}
.gvHeaderCenterID { text-align: center; }
.gvItemCenterID { text-align: center; }

</style>



</asp:Content>

