<%@ Page Language="C#" AutoEventWireup="true" CodeFile="orderImportPage.aspx.cs" Inherits="_orderImportPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= FileUpload1.ClientID %>');
            var FileUploadPath = fuData.value;

            if (FileUploadPath == '') {
                // There is no file selected
                args.IsValid = false;
            }
            else {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "xlsx") {
                    args.IsValid = true; // Valid file type
                }
                else {
                    args.IsValid = false; // Not valid file type
                }
            }
        }
 </script>  
</head>
<body>
    <form id="form1" runat="server">
    
            <div style="width:600px;height:250px;border:2px">
    
            <center>
             <table style="width:600px">
               <tr style="height:40">
                <td colspan="2" align="center">
                  <asp:Label ID="lblImportTitle" runat="server" Font-Size="20px" Font-Bold="True" ForeColor="Gray"></asp:Label></td>
               </tr> 
               <tr style="height:30">
                <td colspan="2" align="center">&nbsp;</td>
               </tr>
               
               <tr style="height:30">
                <td style="width:60%" valign="top"> 
                   <asp:FileUpload ID="FileUpload1" runat="server" Width="319px" /> 
                   <br /><br />
                   <asp:CustomValidator ID="CustomValidator1" runat="server"
                     ClientValidationFunction="ValidateFileUpload" ErrorMessage="Please select valid .xlsx (Excel 2007 is supported) file"></asp:CustomValidator>
                </td>
                <td style="width:40%" valign="top"> 
                   <asp:Button ID="btnImportRO" runat="server" Font-Bold="True" Text="Import Release Order" onclick="btnImportRO_Click" />
                   <asp:Button ID="btnImportPO" runat="server" Font-Bold="True" Text="Import Purchase Order" onclick="btnImportPO_Click" />         
                </td>
               </tr>
               <tr style="height:30">
                <td colspan="2" align="center">&nbsp;</td>
               </tr>
               <tr style="height:30">
                <td colspan="2" align="center">
                   <asp:Label ID="lbImportlMsg" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>   
                </td>
               </tr>
             </table>
            </center>
          </div>
    </form>
</body>
</html>
