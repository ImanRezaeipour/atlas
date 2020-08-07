<%@ Page Language="C#" AutoEventWireup="true" Inherits="ReportViewer" Codebehind="ReportViewer.aspx.cs" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="Sti" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body style="overflow:hidden">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ReportViewerForm" runat="server">
        <%--<div id="Container_StiReportViewer" style="display:none;">--%>
                <Sti:StiWebViewer ID="StiReportViewer" runat="server" Width="100%" RenderMode="AjaxWithCache" 
                    ButtonsImagesPath="Images" ToolBarBackColor="WhiteSmoke" OnGetReportData="StiReportViewer_GetReportData" ButtonImagesPath="Images/ReportViewer/" ShowExportDialog="false">                    
                </Sti:StiWebViewer>
        <%--</div>--%>
        <asp:HiddenField runat="server" ID="hfReportViewerTitle_ReportViewer" />
        <asp:HiddenField runat="server" ID="hfReportAttributes_ReportViewer"/>
    </form>
</body>
</html>
