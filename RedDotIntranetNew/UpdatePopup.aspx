<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePopup.aspx.cs" Inherits="UpdatePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         function openSmallWin() {
             
             var w = this;
             w.resizeTo(500, 500);
         }
     </script>
</head>
<body onload="openSmallWin();">
   <center>
    <form id="form1" runat="server">
      <div>
        <h1>Red Dot Distribution</h1>
        <br />
          <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
        <br />
     </div>
    </form>
    </center>
</body>
</html>
