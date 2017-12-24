<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="RdlcReportForMvc5.Views.Students.WebForm" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="color: #FF0000">
       
        Department: <asp:TextBox ID="DeptTextBox" runat="server" Width="150px"></asp:TextBox> 
        &nbsp;
        <asp:Button ID="SrcBtn" runat="server" BorderColor="#009900" OnClick="SrcBtn_Click" Text="Button" />
&nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:reportviewer ID="ReportViewer1" runat="server" Width="600">
        </rsweb:reportviewer>
    
    </div>
    </form>
</body>
</html>
