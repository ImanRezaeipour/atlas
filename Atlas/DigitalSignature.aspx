<%@ Page Language="C#" AutoEventWireup="true" Inherits="DigitalSignature" Codebehind="DigitalSignature.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/JSignature/jSignature.min.js"></script>
    <script type="text/javascript" src="JS/JSignature/flashcanvas.js"></script>
    <script type="text/javascript" src="JS/JSignature/jSignature.min.noconflict.js"></script>
    <script type="text/javascript" src="JS/API/DigitalSignature.js"></script>
    <form id="DigitalSignatureForm" runat="server">
        <div id="signature" style="color:blue;">
        </div>
        <span><input type="button" value="GetData" onclick="GetData();"/></span>
        <span><input type="button" value="Clear" onclick="Clear();"/></span>
        <span><input type="button" value="SetData" onclick="SetData();"/></span>
        <span><input type="button" value="SaveImage" onclick="SaveImage();"/></span>

    </form>
</body>
</html>
