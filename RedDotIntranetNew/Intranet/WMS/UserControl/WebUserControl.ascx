<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl.ascx.cs" Inherits="Intranet_WMS_UserControl_WebUserControl" %>
<table > 
<tr> <td align ="center">   <h4>   System Administration  </h4> </td></tr>
<tr><td>                            <asp:Button ID="btcustomer" runat="server" 
        Text="Customer" height="26px" 
                                width="172px" onclick="btcustomer_Click"/>
                         </td></tr>
<tr><td>                            <asp:Button ID="btFrForwarder" runat="server"  Text="Freight Forwarders " 
                                height="26px" width="172px" 
        onclick="btFrForwarder_Click"  />
                         </td></tr>
<tr><td> 
                            <asp:Button ID="btSuppliers" runat="server" Text="Suppliers" height="26px" 
                                width="172px" onclick="btSuppliers_Click"  />
    </td></tr>
<tr><td> 
                            <asp:Button ID="btproduct" runat="server" Text="Product" height="26px" 
                                width="172px" onclick="btproduct_Click"  />
    </td></tr>
<tr><td> 
                            <asp:Button ID="btWarehouses" runat="server" Text="Warehouses" height="26px" 
                                width="172px" onclick="btWarehouses_Click"  />
    </td></tr>

    <tr> <td> 
                            <asp:Button ID="btcountry" runat="server" Text="Countries of Origin" 
                                height="26px" width="172px" onclick="btcountry_Click" />
        </td></tr>
    
</table>