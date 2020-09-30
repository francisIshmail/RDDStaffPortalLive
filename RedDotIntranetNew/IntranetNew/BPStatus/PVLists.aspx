<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="PVLists.aspx.cs" Inherits="IntranetNew_BPStatus_PVLists" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>

<script type="text/javascript">


    $(function () {

        $("[id$=txtVendorName]").autocomplete({

            source: function (request, response) {


                $.ajax({
                    url: "PVLists.aspx/GetVendors",
                    data: "{ 'prefix': '" + request.term + "', 'country': '" + $("[id$=ddlCountry]").val() + "' } ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('#')[0]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);

                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=txtVendorName]").val(i.item.label);
            },
            minLength: 1
        });
    });


    $(function () {

        $("[id$=txtBankName]").autocomplete({

            source: function (request, response) {

                $.ajax({
                    url: "PVLists.aspx/GetBanks",
                    data: "{ 'prefix': '" + request.term + "', 'country': '" + $("[id$=ddlCountry]").val() + "' } ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('#')[0]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);

                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=txtBankName]").val(i.item.label);
            },
            minLength: 1
        });
    });



</script>


<style type="text/css">
    td.column_style
    {
        border-left: 1px solid black;
        border-top: 1px solid black;
        padding-left: 4px;
         padding-right: 4px;
    }    
    
     th.column_style
    {
        border-left: 1px solid black;
        border-top: 1px solid black;
    }
      
</style>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Payment Voucher List </Lable>
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
    
<asp:Panel ID="pnlsetup" runat="server" Width="100%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="95%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="15%">
           &nbsp;
        </td>
        <td width="18%">
        &nbsp;
        </td>
         <td width="15%">
           &nbsp;
        </td>
        <td width="18%">
        &nbsp;
        </td>
         <td width="15%">
           &nbsp;
        </td>
        <td width="18%">
        &nbsp;
        </td>
    </tr>
    
      <tr>
         <td width="15%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Country  &nbsp; </label>  
        </td>
        <td width="18%">
            <asp:DropDownList ID="ddlCountry" runat="server"  class="form-control"  
                Width="170px" style="font-size:medium;" TabIndex="0"  > </asp:DropDownList>
        </td>
        <td width="15%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">PV From Date  &nbsp; </label>  
        </td>
        <td width="18%">
            <asp:TextBox id="txtPVDate" runat="server" MaxLength="10"  placeholder="Enter PV date" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
                 <cc1:CalendarExtender ID="txtPVDate_CalendarExtender1" runat="server" 
                                        Enabled="True" TargetControlID="txtPVDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
        </td>
        <td width="15%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">PV To Date  &nbsp; </label>  
        </td>
        <td width="18%">
                <asp:TextBox id="txtToPVDate" runat="server" MaxLength="10"  placeholder="Enter PV date" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
                 <cc1:CalendarExtender ID="txtToPVDate_CalendarExtender1" runat="server" 
                                        Enabled="True" TargetControlID="txtToPVDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
        </td>
    </tr>
    
       <tr>
            
<%--         <td width="15%">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Database  &nbsp; </label>  
        </td>
        <td width="18%">
             <asp:DropDownList ID="ddlDatabase" runat="server"  class="form-control"  
                Width="180px" style="font-size:medium;" TabIndex="0"> 
                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                    <asp:ListItem Value="SAPAE" Text="SAPAE"></asp:ListItem> 
                    <asp:ListItem Value="SAPKE" Text="SAPKE"></asp:ListItem> 
                    <asp:ListItem Value="SAPTZ" Text="SAPTZ"></asp:ListItem>
                    <asp:ListItem Value="SAPUG" Text="SAPUG"></asp:ListItem>
                    <asp:ListItem Value="SAPZM" Text="SAPZM"></asp:ListItem>
                    <asp:ListItem Value="SAPTRI" Text="SAPTRI"></asp:ListItem>
             </asp:DropDownList>
        </td>--%>

         <td width="15%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Vendor  &nbsp; </label>  
        </td>
        <td width="18%">
            <asp:TextBox id="txtVendorName" runat="server" MaxLength="255"  placeholder="Enter Vendor" class="form-control" Width="220px" style="font-size:medium;" >  </asp:TextBox>
        </td>

        <td width="15%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Bank Name  &nbsp; </label>  
        </td>
        <td width="18%">
            <asp:TextBox id="txtBankName" runat="server" MaxLength="50"  placeholder="Enter BankName" class="form-control" Width="220px" style="font-size:medium;" >  </asp:TextBox>
        </td>
        
        <td width="15%">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Paymt Ref No  &nbsp; </label>  
        </td>
        <td width="18%">
            <asp:TextBox id="txtPayRefNo" runat="server" MaxLength="50"  placeholder="Enter cheque/TT/Cash No" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
        </td>
       
    </tr>



    <tr>
        <td colspan="6" align="left"> 
            
            <asp:Button ID="BtnSearch" Text="Search" runat="server" class="btn btn-primary" 
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="19" 
                OnClientClick="return Validate();" onclick="BtnSearch_Click"  />

            &nbsp;&nbsp;  &nbsp;&nbsp;   
        
        <asp:Button ID="BtnExportToExcel" Text="Export To Excel" runat="server" class="btn btn-success" 
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="20" 
                onclick="BtnExportToExcel_Click" />
        
        </td>
    </tr>

    <tr>
        <td colspan="6"> &nbsp; </td>
    </tr>

  </table>

</asp:Panel>

  </td>
</tr>
   
<tr>
    <td width="100%" > &nbsp; </ td>
</tr>
   
<tr>
    <td width="100%">

    <asp:GridView ID="grvPVLists" runat="server" AutoGenerateColumns="False" 
                    Width="100%" CellPadding="4" BackColor="White" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px" 
            onselectedindexchanged="grvPVLists_SelectedIndexChanged" >
                        <Columns>
                            
                            <asp:CommandField ButtonType="Link" ControlStyle-CssClass="column_style" 
                                ControlStyle-ForeColor="Blue" ItemStyle-Width="3%" SelectText="Edit" 
                                ShowSelectButton="True">
                            <ControlStyle ForeColor="Blue" />
                            <ItemStyle CssClass="column_style" Width="3%" />
                            </asp:CommandField>

                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="1%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPVId" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("PVId")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Country" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountry" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("Country")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="RefNo" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNo" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("RefNo")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Approval Status" ItemStyle-Width="9%">
                              <ItemTemplate>
                                    <asp:Label ID="lblApprovalStatus" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("ApprovalStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="VType" 
                                ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label ID="lblVType" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("VType")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Vendor/Employee" 
                                ItemStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorEmployee" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("VendorEmployee")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Currency" 
                                ItemStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("Currency")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="RequestedAmt" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedAmt" runat="server" Font-Size="Smaller" 
                                        Text= '<%#Eval("RequestedAmt","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="ApprovedAmt" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblApprovedAmt" runat="server" Font-Size="Smaller" 
                                        Text= '<%#Eval("ApprovedAmt","{0:N2}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="Benificiary" 
                                ItemStyle-Width="12%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBenificiary" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("Benificiary")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Left" />
                            </asp:TemplateField>
                               
                            <asp:TemplateField HeaderStyle-Font-Size="Small"  HeaderText="BeingPayOf" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBU" runat="server" Font-Size="Smaller" Text='<%#Eval("BeingPayOf")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>
                              

                             <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="PV Status" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocStatus" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("DocStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderText="CreatedOn" ItemStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" runat="server" Font-Size="Smaller" 
                                        Text='<%#Eval("CreatedOn")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" HorizontalAlign="Center" />
                                <ItemStyle CssClass="column_style" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            
                        </Columns>
                     
                        <FooterStyle BackColor="#f91d1d" ForeColor="#4e465b" />
                        <HeaderStyle BackColor="#f91d1d" Font-Bold="True" ForeColor="#FFFFCC" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#4e465b" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#4e465b" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    </asp:GridView>

    </td>
</tr>


</table>

</asp:Content>

