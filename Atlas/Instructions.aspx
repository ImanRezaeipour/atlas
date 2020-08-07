<%@ Page Language="C#" AutoEventWireup="true" Inherits="Instructions" CodeBehind="Instructions.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="InstructionsForm" runat="server" meta:resourcekey="InstructionsForm">
        <table style="width: 100%; font-family: tahoma; font-size: small; font-weight: bold">
            <tbody>
                <tr>
                    <td valign="middle" dir="rtl">
                        <img id="Image1" src="/DesktopModules/Atlas/Images/other/pdf.png" style="height: 21px; width: 22px; border-width: 0px;">
                        <a href="Files/taradod.pdf" style="color: #FFFFFF">دستورالعمل سیستم تردد کارکنان شهرداری تهران</a>
                    </td>
                </tr>
                 <%--<tr>
                    <td valign="middle" dir="rtl">
                        <img id="Image1" src="/DesktopModules/Atlas/Images/other/pdf.png" style="height: 21px; width: 22px; border-width: 0px;">
                        <a href="Files/dastorolamal.pdf" style="color: #FFFFFF">دستورالعمل مرخصی ساعتی و شیفت شیردهی پرسنل موسسه رایانه شهر</a>
                    </td>
                </tr>--%>
        </table>
        <asp:HiddenField runat="server" ID="hfheader_Instructions" meta:resourcekey="hfheader_Instructions" />
    </form>
</body>
</html>
