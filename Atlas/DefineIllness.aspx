<%@ Page Language="C#" AutoEventWireup="true" Inherits="DefineIllness" Codebehind="DefineIllness.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link id="Link1" href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link2" href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link3" href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link4" href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link5" href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link6" href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link7" href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link8" href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link9" href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link id="Link10" href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link11" href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link12" href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link13" href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link14" href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link15" href="css/persianDatePicker.css" runat="server" type="text/css"
        rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="DefineIllnessForm" runat="server" meta:resourcekey="DefineIllnessForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="font-size: small; font-family: Arial; width: 100%;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 20%;">
                                <ComponentArt:ToolBar ID="TlbDefineIllness" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDefineIllness" runat="server" ClientSideCommand="tlbItemSave_TlbDefineIllness_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDefineIllness"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDefineIllness" runat="server" ClientSideCommand="tlbItemCancel_TlbDefineIllness_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDefineIllness"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">

                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblIllnessName_DefineIllness" runat="server" class="WhiteLabel" Text=": نام"
                                                meta:resourcekey="lblIllnessName_DefineIllness"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 50%" valign="top">
                                            <input type="text" id="txtIllnessName_DefineIllness" class="TextBoxes" style="width: 99%;" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 50%" valign="top">
                                            <asp:Label ID="lblIllnessDescription_DefineIllness" runat="server" class="WhiteLabel" Text=": توضیحات "
                                                meta:resourcekey="lblIllnessDescription_DefineIllness"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 50%" valign="top">
                                            <textarea id="txtIllnessDescription_DefineIllness" cols="20" name="S1" rows="7" style="width: 99%; height: 74px"
                                                class="TextBoxes" 
                                                ></textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

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
        <asp:HiddenField runat="server" ID="hfTitle_DialogDefineIllness" meta:resourcekey="hfTitle_DialogDefineIllness" />
        <asp:HiddenField runat="server" ID="hfErrorType_DialogDefineIllness" meta:resourcekey="hfErrorType_DialogDefineIllness" />
        <asp:HiddenField runat="server" ID="hfConnectionError_DefineIllness" meta:resourcekey="hfConnectionError_DefineIllness" />
    </form>
</body>
</html>
