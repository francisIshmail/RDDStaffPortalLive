<%@ Page Title="" Language="VB" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="false" CodeFile="sapAddOnceItems.aspx.vb" Inherits="Intranet_sapBase_sapAddOnceItems" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
    <style type="text/css">
        .style1
        {
            width: 40%;
            height: 30px;
        }
        .style2
        {
            width: 60%;
            height: 30px;
        }
        .style3
        {
            height: 22px;
        }
    </style>
     
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="SAP - Addonce Stock Item"></asp:Label>
                        &nbsp;Ver 15-May-2019</div>
                </td>
              </tr>
            </table>   
    <table width="100%" style="background-color:White">
        <tr style="height:25px">
             <td width="25%" align="left">
               <asp:Button ID="cmdConnectDb" runat="server" 
                    Text="Click! Connect To SAP" width="90%" BackColor="#00CC99" 
                    BorderColor="#FF9966" Font-Bold="True"/>
            </td> 
            <td width="30%" align="center">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Text="Not Connected" ForeColor="#993399"></asp:Label>
            </td>
             <td width="55%" align="left">
                <asp:CheckBox ID="chkAE" runat="server" Text="AE" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkUG" runat="server" Text="UG" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkTZ" runat="server" Text="TZ" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkKE" runat="server" Text="KE" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkZM" runat="server" Text="ZM" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkMA" runat="server" Text="MA" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkTRI" runat="server" Text="TRI" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />

           </td>
        </tr>
        <tr> 
            <td colspan="4" align="left" style="height:25px">
               <asp:Label ID="lblError" Text="" runat="server" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
    </table>

 <asp:Panel ID="Panel1" runat="server" >

    <table width="100%" style="background-color:White">
        <tr>
            <td valign="top" style="width:70%">

                <asp:Panel ID="pnlItem" runat="server" GroupingText="Item" CssClass="addOncePanel" Enabled="False">
                       <table style="width:100%;">
                           
                            <%--<tr>
                             <td style="width:30%">
                                    Category (<asp:Label ID="lblLstCntCat" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)</td>
                               <td style="width:70%">
                                    <asp:DropDownList ID="ddlCat" runat="server" Width="150px" BackColor="#FFCC66">
                                    </asp:DropDownList>    
                                        &nbsp;<asp:Label ID="lblCat" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>--%>
                            <tr>
                              <td>
                                    Manufacturer (<asp:Label ID="lblLstCntMfr" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)</td>
                                <td>
                                    <asp:DropDownList ID="ddlMfr" runat="server" Width="150px" BackColor="#FFCC66" AutoPostBack="true">
                                    </asp:DropDownList>  
                                     &nbsp;<asp:Label ID="lblMfr" runat="server" Text=""></asp:Label>  
                                </td>
                            </tr>
                             <tr>
                              <td>
                                    BU (<asp:Label ID="lblLstCnt" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)</td>
                               <td>
                                    <asp:DropDownList ID="ddl1" runat="server" Width="150px" BackColor="#FFCC66" AutoPostBack="True">
                                    </asp:DropDownList>     
                                    &nbsp;Code :<asp:Label ID="lblGrp" runat="server" Text=""></asp:Label>
                                    &nbsp;, BU :<asp:Label ID="lblBU" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                             
                             
                             <%--<tr>
                              <td>
                                    Model (<asp:Label ID="lblLstCntMdl" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)</td>
                                <td>
                                    <asp:DropDownList ID="ddlMdl" runat="server" Width="150px" BackColor="#FFCC66" AutoPostBack="true"> 
                                    </asp:DropDownList>    
                                     &nbsp;<asp:RadioButton ID="radNewMdl" Text="Add New ?" runat="server" GroupName="newgrp"
                                        AutoPostBack="True" Font-Bold="True" ForeColor="#CC3300" />
                                    &nbsp;<asp:Label ID="lblMdl" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>--%>

                            <tr>
                              <td>
                                    Product Category (<asp:Label ID="lblLstCntProductCategory" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)&nbsp;[Based on BU]</td>
                                <td>
                                    <asp:DropDownList ID="ddlProductCategory" runat="server" Width="150px" BackColor="#FFCC66" AutoPostBack="true" >
                                    </asp:DropDownList>    
                                     &nbsp;<asp:RadioButton ID="radNewProductCategory" Text="Add New ?" runat="server" GroupName="newgrp"
                                        AutoPostBack="True" Font-Bold="True" ForeColor="#CC3300" />
                                    &nbsp;<asp:Label ID="lblProductCategory" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                              <td>
                                    Product Line (<asp:Label ID="lblLstCntPL" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)&nbsp;[Based on Product Category]</td>
                                <td>
                                    <asp:DropDownList ID="ddlPL" runat="server" Width="150px" BackColor="#FFCC66">
                                    </asp:DropDownList>    
                                     &nbsp;<asp:RadioButton ID="radNewPL" Text="Add New ?" runat="server" GroupName="newgrp"
                                        AutoPostBack="True" Font-Bold="True" ForeColor="#CC3300" />
                                        &nbsp;<asp:Label ID="lblPL" runat="server" Text=""></asp:Label>
                                        &nbsp;<asp:Label ID="lblPLDesc" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>

                            <tr>
                              <td colspan="2">&nbsp;</td>
                              </tr>
                            <tr>
                              <td>
                                    Product Group (<asp:Label ID="lblLstCntProductGroup" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)&nbsp;[Based on BU]</td>
                                <td>
                                    <asp:DropDownList ID="ddlProductGroup" runat="server" Width="150px" BackColor="#FFCC66"> 
                                    </asp:DropDownList>    
                                     &nbsp;<asp:RadioButton ID="radNewProductGroup" Text="Add New ?" runat="server" GroupName="newgrp"
                                        AutoPostBack="True" Font-Bold="True" ForeColor="#CC3300" />
                                    &nbsp;<asp:Label ID="lblProductGroup" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>

                            <%--<tr>
                              <td>
                                    DashBoard Category (<asp:Label ID="lblLstCntDcat" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)</td>
                                <td>
                                    <asp:DropDownList ID="ddlDcat" runat="server" Width="150px" BackColor="#FFCC66">
                                    </asp:DropDownList>   
                                     &nbsp;<asp:RadioButton ID="radNewDcat" Text="Add New ?" runat="server" GroupName="newgrp"
                                        AutoPostBack="True" Font-Bold="True" ForeColor="#CC3300" />
                                        &nbsp;<asp:Label ID="lblDcat" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>--%>
                           
                            <tr>
                                <td > Item Code</td>
                                <td >
                                    <asp:TextBox ID="txtSimpleCode" runat="server" Width="50%" MaxLength="20"></asp:TextBox>
                                    &nbsp;Max 20 Chrs</td>
                            </tr>
                            
                            <tr>
                                <td>
                                    Item Desc. </td>
                                <td>
                                    <asp:TextBox ID="txtDesc1" runat="server" Width="85%" MaxLength="250"></asp:TextBox>
                                    &nbsp;Max 250 Chrs</td>
                            </tr>

                            <tr>
                                <td colspan="2" width="100%">
                                   <table style="width:90%;">
                                    <tr>
                                        <td width="8%">Length ( in cm )</td>
                                        <td width="12%">   <asp:TextBox ID="txtLength" runat="server" Width="85%" MaxLength="15"></asp:TextBox> </td>

                                         <td width="8%">Width ( in cm )</td>
                                        <td width="12%">   <asp:TextBox ID="txtWidth" runat="server" Width="85%" MaxLength="15"></asp:TextBox> </td>

                                        <td width="8%">Height ( in cm )</td>
                                        <td width="12%">   <asp:TextBox ID="txtHeight" runat="server" Width="85%" MaxLength="15"></asp:TextBox> </td>
                                        
                                          <td width="8%">Weight ( in KG )</td>
                                        <td width="12%">   <asp:TextBox ID="txtWeight" runat="server" Width="85%" MaxLength="15"></asp:TextBox> </td>
                                    </tr>
                                   
                                   </table>
                                </td>
                            </tr>


                        </table>
                </asp:Panel>    
            </td>

            <td valign="top" style="width:30%">
                 <asp:Panel ID="PanelNewCreation" runat="server" Visible="false" >  
                     <table style="width:100%;">
                      <tr>
                        <td style="width:30%"></td>
                        <td style="width:70%"></td>
                      </tr>
                      <tr>
                          <td colspan="2" align="center">
                          ADD NEW &nbsp;
                            <asp:Label ID="lblAddNewFor" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                         </td>
                      </tr>
                     <tr>
                        <td>Value</td>
                        <td><asp:TextBox ID="txtNewCode" runat="server" Width="85%" MaxLength="250"></asp:TextBox>&nbsp;Max 250 Chrs</td>
                     </tr>
                     <tr>
                     <td>Description</td>
                      <td>
                        <asp:TextBox ID="txtnewDesc" runat="server" Width="85%" MaxLength="250"></asp:TextBox>
                        &nbsp;Max 250 Chrs
                      </td>
                     </tr>
                     <tr>
                     <td>
                      <asp:Button ID="btnSaveNew" runat="server" Text="Save" width="50px" 
                                        BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                        ForeColor="Black" ToolTip="Save new value to database" />
                     </td>
                      <td>
                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" width="50px" 
                                        BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                        ForeColor="Black" ToolTip="Save new value to database" />
                      </td>
                     </tr>
                   </table>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td colspan="2" class="style3">
            </td>
        </tr>

       <tr>
                <td valign="top" colspan="2">
                    <asp:Panel ID="pnlBtn1" runat="server" CssClass="addOncePanel" GroupingText="Insert Values">
                        <table width="100%">
                            <tr>
                                <td align="left" class="style1" valign="top">
                                    <asp:CheckBox ID="chkAEdo" runat="server" Text="AE" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkUGdo" runat="server" Text="UG" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkTZdo" runat="server" Text="TZ" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkKEdo" runat="server" Text="KE" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkZMdo" runat="server" Text="ZM" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkMAdo" runat="server" Text="MA" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkTRIdo" runat="server" Text="TRI" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />

                                    &nbsp;
                                     <asp:Button ID="cmdAdd" runat="server" Text="Add Item to selected DBs" width="200px" 
                                        Enabled="False" BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                        ForeColor="Black" ToolTip="Please Check/Uncheck the Databases on left you want to add to.." />
                                  </td>
                                <td align="center" class="style2">
                                        <asp:Label ID="lblInfo" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                            
            </tr>
        </table>
    </asp:Panel>
    
    </ContentTemplate>
        
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:35%;left:50%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
     </asp:UpdateProgress>

</asp:Content>

