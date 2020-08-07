<%@ Page Language="C#" AutoEventWireup="true" Inherits="DefinePhysicians" Codebehind="DefinePhysicians.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link id="Link15" href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="DefinePhysiciansForm" runat="server" meta:resourcekey="DefinePhysiciansForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="font-size: small; font-family: Arial; width: 100%" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 20%">
                                <ComponentArt:ToolBar ID="TlbDefinePhysicians" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDefinePhysicians" runat="server" ClientSideCommand="tlbItemSave_TlbDefinePhysicians_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDefinePhysicians"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDefinePhysicians" runat="server" ClientSideCommand="tlbItemCancel_TlbDefinePhysicians_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDefinePhysicians"
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
                                            <asp:Label ID="lblPhysiciansCode_DefinePhysicians" runat="server" class="WhiteLabel" Text=": کد نظام پرشکی"
                                                meta:resourcekey="lblPhysiciansCode_DefinePhysicians"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPhysiciansProficiency_DefinePhysicians" runat="server" class="WhiteLabel" Text=": تخصص"
                                                meta:resourcekey="lblPhysiciansProficiency_DefinePhysicians"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%">
                                            <input type="text" id="txtPhysiciansCode_DefinePhysicians" class="TextBoxes" style="width: 99%;" /></td>
                                        <td>
                                            <input type="text" id="txtPhysiciansProficiency_DefinePhysicians" class="TextBoxes" style="width: 99%;" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblPhysiciansName_DefinePhysicians" runat="server" class="WhiteLabel" Text=": نام"
                                                meta:resourcekey="lblPhysiciansName_DefinePhysicians"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPhysiciansFamily_DefinePhysicians" runat="server" class="WhiteLabel" Text=": نام خانوادگی"
                                                meta:resourcekey="lblPhysiciansFamily_DefinePhysicians"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <input type="text" id="txtPhysiciansName_DefinePhysicians" class="TextBoxes" style="width: 99%;" /></td>
                                        <td class="auto-style2">
                                            <input type="text" id="txtPhysiciansFamily_DefinePhysicians" class="TextBoxes" style="width: 99%;" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblPhysiciansDescription_DefinePhysicians" runat="server" class="WhiteLabel" Text=": توضیحات "
                                                meta:resourcekey="lblPhysiciansDescription_DefinePhysicians"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <textarea id="txtPhysiciansDescription_DefinePhysicians" cols="20" name="S1" rows="7" style="width: 99%; height: 35px"
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
        <asp:HiddenField runat="server" ID="hfTitle_DialogDefinePhysicians" meta:resourcekey="hfTitle_DialogDefinePhysicians" />
        <asp:HiddenField runat="server" ID="hfErrorType_DialogDefinePhysicians" meta:resourcekey="hfErrorType_DialogDefinePhysicians" />
        <asp:HiddenField runat="server" ID="hfConnectionError_DefinePhysicians" meta:resourcekey="hfConnectionError_DefinePhysicians" />
    </form>
</body>
</html>
