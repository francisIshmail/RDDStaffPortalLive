<%@ Page Language="VB" explicit="true" strict="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Sub Page_Load(ByVal Sender As Object, ByVal E As EventArgs)
        Dim strRequest As String = Request.QueryString("file") '-- if something was passed to the file querystring  
        Dim setfileName As String = Request.QueryString("setfileName")
        
        If strRequest <> "" Then 'get absolute path of the file  
            Dim path As String = Server.MapPath(strRequest) 'get file object as FileInfo  
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server  
            If file.Exists Then 'set appropriate headers  
                Response.Clear()
                'Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name.Replace(" ", "-"))
                Response.AddHeader("Content-Disposition", "attachment; filename=" & setfileName)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End() 'if file does not exist  
            Else
                Response.Write("This file does not exist.")
            End If 'nothing in the URL as HTTP GET  
        Else
            Response.Write("Please provide a file to download.")
        End If
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
