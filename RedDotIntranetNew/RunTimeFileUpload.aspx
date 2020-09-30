<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RunTimeFileUpload.aspx.cs"
    Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generating Runtime Controls</title>
    
    <script type="text/javascript" language="javascript">

    function AddNewRow() 
    {

                var rownum = 1;
                var div = document.createElement("div");
                var divid= "dv" + rownum;
                div.setAttribute("ID",divid);
                rownum++;
               
                //var lbl = document.createElement("label");
                //lbl.setAttribute("ID", "lbl" + rownum);
                //lbl.setAttribute("class", "label1");
                //lbl.innerHTML = "Images";
                //rownum++;
                
                var _upload = document.createElement("input");
                _upload.setAttribute("type", "file");
                _upload.setAttribute("ID", "upload" + rownum);
                _upload.setAttribute("runat", "server");
                _upload.setAttribute("name","uploads"+rownum);
                rownum++;
                
                var hyp = document.createElement("a");
                hyp.setAttribute("style", "cursor:Pointer");
                hyp.setAttribute("onclick", "return RemoveDv('" + divid + "');");
                hyp.innerHTML = "Remove";
                rownum++;
                
                //var br=document.createElement("br");

                var _pdiv = document.getElementById("divFileUploads");
                
                //div.appendChild(br);
                //div.appendChild(lbl);
                div.appendChild(_upload);
                div.appendChild(hyp);
                _pdiv.appendChild(div);
    }

    function RemoveDv(obj) 
                
    {
            var p = document.getElementById("Parent");
            var chld = document.getElementById(obj);
            p.removeChild(chld);
                
    }

</script>
   

</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr id="Tr1" runat="Server">
            <td>
             <label>
               Photo:</label><asp:FileUpload ID="uploadPhoto11" runat="server" CssClass="" /><br />

            <div id="divFileUploads">
            </div>
            <input type="button" onclick="AddNewRow(); return false;"  value="More" />&nbsp;
            <asp:Button ID="btnAddPhoto" Text="add photo" runat="server"  onclick="btnAddPhoto_Click1" />&nbsp;
            <asp:Button ID="btnCancel" Text="cancel" runat="server" />

            
        </td></tr>
      </table>
    </form>
</body>
</html>
