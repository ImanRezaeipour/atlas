<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.Departments" CodeBehind="Departments.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="DepartmentForm" runat="server" meta:resourcekey="DepartmentsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_Departments" style="width: 90%; height: 400px; font-family: Arial; font-size: small">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbDepartmentsIntroduction" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbDepartmentsIntroduction" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ClientSideCommand="tlbItemNew_TlbDepartmentsIntroduction_onClick();"
                                            ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbDepartmentsIntroduction" runat="server"
                                            ClientSideCommand="tlbItemEdit_TlbDepartmentsIntroduction_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemEdit_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbDepartmentsIntroduction" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png"
                                            ClientSideCommand="tlbItemDelete_TlbDepartmentsIntroduction_onClick();" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbDepartmentsIntroduction" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbDepartmentsIntroduction_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDepartmentsIntroduction" runat="server"
                                            ClientSideCommand="tlbItemSave_TlbDepartmentsIntroduction_onClick();" Enabled="false"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDepartmentsIntroduction" runat="server"
                                            Enabled="false" ClientSideCommand="tlbItemCancel_TlbDepartmentsIntroduction_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbDepartmentsIntroduction"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbDepartmentsIntroduction_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDepartmentsIntroduction" runat="server"
                                            ClientSideCommand="tlbItemExit_TlbDepartmentsIntroduction_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbDepartmentsIntroduction"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_DepartmentForm" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">

                        <tr>
                            <td style="width: 60%;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 40%">
                                                        <asp:Label ID="lblSearchTerm_Departments" runat="server" Text=": جستجوی بخش"
                                                            meta:resourcekey="lblSearchTerm_Departments" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%">&nbsp;</td>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="lblDepartmentSearchResult_Departments" runat="server" Text=": نتایج جستجوی بخش"
                                                            meta:resourcekey="lblDepartmentsSearchResult_Departments" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input id="txtSearchTerm_Departments" type="text" class="TextBoxes"
                                                            onkeypress="txtSerchTerm_Departments_onKeyPess(event);" style="width: 97%" />
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbDepartmentSearch_Departments" runat="server"
                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemDepartmentSearch_TlbDepartmentSearch_Departments"
                                                                    runat="server" ClientSideCommand="tlbItemDepartmentSearch_TlbDepartmentSearch_Departments_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDepartmentSearch_TlbDepartmentSearch_Departments"
                                                                    TextImageSpacing="5" Enabled="true" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbDepartmentSearchResult_Departments"
                                                            OnCallback="CallBack_cmbDepartmentSearchResult_Departments_onCallBack"
                                                            Height="26">
                                                            <Content>
                                                                <ComponentArt:ComboBox ID="cmbDepartmentSearchResult_Departments" runat="server"
                                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                                    ExpandDirection="Down" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150"
                                                                    DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                                    Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                    <ClientEvents>
                                                                        <Change EventHandler="cmbDepartmentSearchResult_Departments_onChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:ComboBox>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_DepartmentSearchResult_Departments" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <BeforeCallback EventHandler="CallBack_cmbDepartmentSearchResult_Departments_onBeforeCallback" />
                                                                <CallbackComplete EventHandler="CallBack_cmbDepartmentSearchResult_Departments_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_cmbDepartmentSearchResult_Departments_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                            <td></td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 60%" valign="top">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_Departments_DepartmentIntroduction" class="HeaderLabel" style="width: 50%">Departments
                                                    </td>
                                                    <td id="loadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction" class="HeaderLabel"
                                                        style="width: 45%"></td>
                                                    <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_trvDepartmentsIntroduction_DepartmentIntroduction"
                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvDepartmentsIntroduction_DepartmentIntroduction"
                                                                    runat="server" ClientSideCommand="Refresh_trvDepartmentsIntroduction_DepartmentIntroduction();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvDepartmentsIntroduction_DepartmentIntroduction"
                                                                    TextImageSpacing="5" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <ComponentArt:CallBack runat="server" ID="CallBack_trvDepartmentsIntroduction_DepartmentIntroduction"
                                                OnCallback="CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallBack">
                                                <Content>
                                                    <ComponentArt:TreeView ID="trvDepartmentsIntroduction_DepartmentIntroduction" runat="server"
                                                        ExpandNodeOnSelect="true" CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif"
                                                        CssClass="TreeView" DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16"
                                                        DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                        LoadingFeedbackText="loading......." ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                                        FillContainer="false" ForceHighlightedNodeID="true" Height="320" HoverNodeCssClass="HoverNestingTreeNode"
                                                        ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19"
                                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" meta:resourcekey="trvDepartmentsIntroduction_DepartmentIntroduction"
                                                        BorderColor="Black">
                                                        <ClientEvents>
                                                            <NodeSelect EventHandler="trvDepartmentsIntroduction_DepartmentIntroduction_onNodeSelect" />
                                                            <Load EventHandler="trvDepartmentsIntroduction_DepartmentIntroduction_onLoad" />
                                                            <NodeExpand EventHandler="trvDepartmentsIntroduction_DepartmentIntroduction_onNodeExpand" />
                                                        </ClientEvents>
                                                    </ComponentArt:TreeView>
                                                    <asp:HiddenField ID="ErrorHiddenField_Departments" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_trvDepartmentsIntroduction_DepartmentIntroduction_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="middle" align="center">
                                <table style="width: 100%;" class="BoxStyle" id="tblDepartment_DepartmentIntroduction">
                                    <tr runat="server" meta:resourcekey="AlignObj">
                                        <td colspan="2" class="DetailsBoxHeaderStyle">
                                            <div id="header_DepartmentDetails_DepartmentIntroduction" runat="server" class="BoxContainerHeader"
                                                meta:resourcekey="AlignObj" style="color: White; width: 100%; height: 100%">
                                                Department Details
                                            </div>
                                        </td>
                                    </tr>
                                    <tr runat="server" meta:resourcekey="AlignObj">
                                        <td style="width: 5%">
                                            <input id="chbDepartmentCodeView_DepartmentIntroduction" type="checkbox" onclick="chbDepartmentCodeView_DepartmentIntroduction_onclick();" />
                                        </td>
                                        <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 95%">
                                            <asp:Label ID="lblDepartmentCodeView_DepartmentIntroduction" runat="server" meta:resourcekey="lblDepartmentCodeView_DepartmentIntroduction"
                                                Text="نمایش کد بخش" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" meta:resourcekey="AlignObj">
                                        <td runat="server" colspan="2">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDepartmentCode_DepartmentIntroduction" runat="server" meta:resourcekey="lblDepartmentCode_DepartmentIntroduction"
                                                            Text=": کد بخش" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtDepartmentCode_DepartmentIntroduction"
                                                            disabled="disabled" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDepartmentName_DepartmentIntroduction" runat="server" meta:resourcekey="lblDepartmentName_DepartmentIntroduction"
                                                            Text=": نام بخش" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtDepartmentName_DepartmentIntroduction"
                                                            disabled="disabled" />
                                                    </td>
                                                </tr>
                                                <tr id="Tr5" runat="server" meta:resourcekey="AlignObj">
                                                    <td>
                                                        <asp:Label ID="lblDepartmentTypeDepartmentIntroduction" runat="server" meta:resourcekey="lblDepartmentType_DepartmentIntroduction"
                                                            Text=": نوع بخش" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                                    <td>
                                                        <componentart:callback id="CallBack_cmbDepartmentType_DepartmentIntroduction" runat="server" oncallback="CallBack_cmbDepartmentType_DepartmentIntroduction_onCallBack"
                                                            height="26">
                                                            <Content>
                                                                <ComponentArt:ComboBox ID="cmbDepartmentType_DepartmentIntroduction" runat="server" AutoComplete="true"
                                                                    AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                    DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                    DataTextField="Name" DataValueField="ID" DropImageUrl="Images/ComboBox/ddn.png"
                                                                    Enabled="false" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                    Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                                    <ClientEvents>
                                                                        <Expand EventHandler="cmbDepartmentType_DepartmentIntroduction_onExpand" />
                                                                        <Collapse EventHandler="cmbDepartmentType_DepartmentIntroduction_onCollapse" />
                                                                    </ClientEvents>
                                                                </ComponentArt:ComboBox>
                                                                <asp:HiddenField ID="ErrorHiddenField_DepartmentType" runat="server" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <BeforeCallback EventHandler="CallBack_cmbDepartmentType_DepartmentIntroduction_onBeforeCallback" />
                                                                <CallbackComplete EventHandler="CallBack_cmbDepartmentType_DepartmentIntroduction_onCallbackComplete" />
                                                            </ClientEvents>
                                                        </componentart:callback>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="280px">
            <Content>
                <table style="width: 100%;" class="ConfirmStyle">
                    <tr align="center">
                        <td colspan="2">
                            <asp:Label ID="lblConfirm" runat="server" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td style="width: 50%">
                            <ComponentArt:ToolBar ID="TlbOkConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemOk_TlbOkConfirm" runat="server" ClientSideCommand="tlbItemOk_TlbOkConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOk_TlbOkConfirm"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>
                            <ComponentArt:ToolBar ID="TlbCancelConfirm" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbCancelConfirm" runat="server" ClientSideCommand="tlbItemCancel_TlbCancelConfirm_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
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
        <asp:HiddenField runat="server" ID="hfheader_Departments_DepartmentIntroduction" meta:resourcekey="hfheader_Departments_DepartmentIntroduction" />
        <asp:HiddenField runat="server" ID="hfheader_DepartmentDetails_DepartmentIntroduction" meta:resourcekey="hfheader_DepartmentDetails_DepartmentIntroduction" />
        <asp:HiddenField runat="server" ID="hfView_Departments" meta:resourcekey="hfView_Departments" />
        <asp:HiddenField runat="server" ID="hfAdd_Departments" meta:resourcekey="hfAdd_Departments" />
        <asp:HiddenField runat="server" ID="hfEdit_Departments" meta:resourcekey="hfEdit_Departments" />
        <asp:HiddenField runat="server" ID="hfDelete_Departments" meta:resourcekey="hfDelete_Departments" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_Departments" meta:resourcekey="hfDeleteMessage_Departments" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Departments" meta:resourcekey="hfCloseMessage_Departments" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction"
            meta:resourcekey="hfloadingPanel_trvDepartmentsIntroduction_DepartmentIntroduction" />
        <asp:HiddenField runat="server" ID="hfErrorType_Departments" meta:resourcekey="hfErrorType_Departments" />
        <asp:HiddenField runat="server" ID="hfConnectionError_Departments" meta:resourcekey="hfConnectionError_Departments" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_Department" meta:resourcekey="hfcmbAlarm_Department" />

    </form>
</body>
</html>
