<%@ Page Language="C#" AutoEventWireup="true" Inherits="FromTime_ToTime_ReportParameter" Codebehind="FromTime_ToTime_ReportParameter.aspx.cs" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link id="Link1" href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link2" href="css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link4" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link5" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="FromTime_ToTime_ReportParameterForm" runat="server" meta:resourcekey="FromTime_ToTime_ReportParameterForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
     <div style="height: 290px;" class="BoxStyle">
        <table id="tblMain_FromDate_ToDate_ReportParameter" style="width: 60%; font-family: Arial;
            font-size: small" meta:resourcekey="AlignObj">
            <tr>
                <td style="width: 45%">
                    <asp:Label ID="lblFrom_FromTime_ToTime_ReportParameter" runat="server" Text="از ساعت :" CssClass="WhiteLabel"
                        meta:resourcekey="lblFrom_FromTime_ToTime_ReportParameter"></asp:Label>
                </td>
                <td id="Container_fromTime_RuleParameters">
                   <MKB:TimeSelector ID="TimeSelector_FromTime_ReportParameter" runat="server"
                                                                                DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                                Visible="true">
                                                                            </MKB:TimeSelector>
                </td>
                <td style="width: 10%" rowspan="2">
                    <ComponentArt:ToolBar ID="TlbRegister_FromTime_ToTime_ReportParameter" runat="server"
                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbRegister_FromTime_ToTime_ReportParameter"
                                runat="server" ClientSideCommand="tlbItemRegister_TlbRegister_FromTime_ToTime_ReportParameter_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRegister_TlbRegister_FromTime_ToTime_ReportParameter"
                                TextImageSpacing="5" Enabled="true" />
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTo_FromTime_ToTime_ReportParameter" runat="server" Text=" تا ساعت :" CssClass="WhiteLabel"
                        meta:resourcekey="lblTo_FromTime_ToTime_ReportParameter"></asp:Label>
                </td>
                <td id="Container_toTime_RuleParameters">
                    <MKB:TimeSelector ID="TimeSelector_ToTime_ReportParameter" runat="server"
                                                                                DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                                Visible="true">
                                                                            </MKB:TimeSelector>
                    
                </td>
            </tr>
        </table>
    </div>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
        runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogWaiting">
        <Content>
            <table>
                <tr>
                    <td>
                        <img id="Img1" runat="server" alt="" src="~/DesktopModules/Atlas/Images/Dialog/Waiting.gif" />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>

    <asp:HiddenField runat="server" ID="hfConnectionError_FromTime_ToTime_ReportParameter"
        meta:resourcekey="hfConnectionError_FromTime_ToTime_ReportParameter" />
    <asp:HiddenField runat="server" ID="hfErrorType_FromTime_ToTime_ReportParameter"
        meta:resourcekey="hfErrorType_FromTime_ToTime_ReportParameter" />
    <asp:HiddenField runat="server" ID="ReportParameterID" />
    </form>
</body>
</html>
