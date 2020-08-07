<%@ Page Language="C#" AutoEventWireup="true" Inherits="MasterReportViewer" Codebehind="MasterReportViewer.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="MasterReportViewerForm" runat="server">
    <div>
         <iframe runat="server" id="MasterReportViewerFrame_MasterReportViewer"></iframe>
         <asp:HiddenField runat="server" ID="hfMasterReportViewerFrame_MasterReportViewer"/>
         <asp:HiddenField runat="server" ID="hfReportViewerTitle_MasterReportViewer" />
    </div>
    </form>
</body>
</html>
