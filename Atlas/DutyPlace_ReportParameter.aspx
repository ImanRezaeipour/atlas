<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.DutyPlace_ReportParameter" Codebehind="DutyPlace_ReportParameter.aspx.cs" %>


<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <form id="DutyPlace_ReportParameterForm" runat="server" meta:resourcekey="DutyPlace_ReportParameterForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div style="height: 290px;" class="BoxStyle">
            <table id="tblMain_DutyPlace_ReportParameter" style="width: 100%; font-family: Arial; font-size: small"
                meta:resourcekey="AlignObj">

                <tr>
                    <td>
                        <asp:Label ID="lblDutyPlace_ReportParameter" runat="server" Text="محل ماموریت :" CssClass="WhiteLabel"
                            meta:resourcekey="lblDutyPlace_ReportParameter"></asp:Label>
                    </td>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbDutyPlace_ReportParameter"
                                        OnCallback="CallBack_cmbDutyPlace_ReportParameter_onCallBack" Height="26" Width="100%">
                                        <Content>

                                            <ComponentArt:ComboBox ID="cmbDutyPlace_ReportParameter" runat="server"
                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                DropDownCssClass="comboDropDown" DropDownHeight="190" DropDownResizingMode="Corner"
                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                Width="100%" ExpandDirection="Down" TextBoxEnabled="true">
                                                <DropDownContent>
                                                    <ComponentArt:TreeView ID="trvDutyPlace_ReportParameter" runat="server"
                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="98%"
                                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                        LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                                        NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                        ShowLines="true" Width="100%" meta:resourcekey="trvDutyPlace_ReportParameter">
                                                        <ClientEvents>
                                                            <NodeCheckChange EventHandler="trvDutyPlace_ReportParameter_onNodeCheckChange" />
                                                            <NodeExpand EventHandler="trvDutyPlace_ReportParameter_onNodeExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:TreeView>
                                                </DropDownContent>
                                                <ClientEvents>
                                                    <Expand EventHandler="cmbDutyPlace_ReportParameter_onExpand" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_DutyPlace_ReportParameter" />
                                        </Content>
                                        <ClientEvents>
                                            <BeforeCallback EventHandler="CallBack_cmbDutyPlace_ReportParameter_onBeforeCallback" />
                                            <CallbackComplete EventHandler="CallBack_cmbDutyPlace_ReportParameter_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_cmbDutyPlace_ReportParameter_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                                <td style="width: 5%">
                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbDutyPlace_ReportParameter"
                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbDutyPlace_ReportParameter"
                                                runat="server" ClientSideCommand="Refresh_cmbDutyPlace_ReportParameter();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbDutyPlace_ReportParameter"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbDutyPlace_ReportParameter"
                                                runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbDutyPlace_ReportParameter_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbDutyPlace_ReportParameter"
                                                TextImageSpacing="5" />
                                            
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                   <td  valign="middle" style="width: 5%">
                                <ComponentArt:ToolBar ID="TlbRegister_DutyPlace_ReportParameter" runat="server"
                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbRegister_DutyPlace_ReportParameter"
                                            runat="server" ClientSideCommand="tlbItemRegister_TlbRegister_DutyPlace_ReportParameter_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRegister_TlbRegister_DutyPlace_ReportParameter"
                                            TextImageSpacing="5" Enabled="true" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                </tr>
                <tr>
                     <td colspan="1">
                        <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <input id="chbSubDutyPlace_ReportParameter" type="checkbox" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSubDutyPlace_ReportParameter" class="WhiteLabel" runat="server"
                                                            Text="محل تابع" meta:resourcekey="lblSubDutyPlace_ReportParameter"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                    </td>
                    <td colspan="2"></td>
                   
                </tr>
            </table>

        </div>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="DialogWaiting">
            <Content>
                <table>
                    <tr>
                        <td>
                            <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfCurrentfromDate_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfCurrenttoDate_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfCurrentFromDate_Station_Clock_Date_ReportParameter" />
        <asp:HiddenField runat="server" ID="hfCurrentToDate_Station_Clock_Date_ReportParameter" />
        <asp:HiddenField runat="server" ID="hfConnectionError_Station_Clock_Date_ReportParameter"
            meta:resourcekey="hfConnectionError_Station_Clock_Date_ReportParameter" />
        <asp:HiddenField runat="server" ID="hfErrorType_Station_Clock_Date_ReportParameter"
            meta:resourcekey="hfErrorType_Station_Clock_Date_ReportParameter" />
        <asp:HiddenField runat="server" ID="ReportParameterID" />
      <asp:HiddenField runat="server" ID="hfCountSelectedNodeHiddenField_TrvDutyPlace_ReportParameter" meta:resourcekey="hfCountSelectedNodeHiddenField_TrvDutyPlace_ReportParameter" />
    </form>
</body>
</html>
