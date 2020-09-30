<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MsgBoxControl.ascx.cs" Inherits="MsgBoxControl" %>


<div ID="pnlMessage" Runat="server" Visible="false" style="height:100%;width:100%;
border: thin solid #808080;position: fixed;z-index: 100;background-color: Black;opacity:0.9;top:-1px;left:-1px;">

<div style="width:50%;margin:auto;margin-top:20%;background-color:white;overflow:auto">
    <table style="height:100%;width:100%">
        <tr style="background-color:Gray;height:20px">
            <td align="center" valign="top" style="width:100%">
                <asp:Label ID="lblHeader" runat="server" Text="Message" Font-Bold="true" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="width:100%">
                <asp:Label id="lblMsg" runat="server" ForeColor="Black"></asp:Label> 
            </td>
        </tr>
        <tr>
            <td align="left" valign="bottom" style="padding-left:10px;padding-bottom:10px">
                <asp:Button ID="lnkBtnOK" runat="server" Text="OK" Font-Bold="true" 
                    Width="50px" Height="30px" ForeColor="White" BackColor="Black" onclick="lnkBtnOK_Click" />
            </td>
        </tr>
    </table>
</div>

</div>
