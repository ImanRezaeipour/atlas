<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="GTS.Clock.Presentaion.WebForms.HourlyRequestOnAbsence" CodeBehind="HourlyRequestOnAbsence.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body onkeydown="HourlyRequestOnAbsenceForm_onKeyDown(event);">
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="HourlyRequestOnAbsenceForm" runat="server" meta:resourcekey="HourlyRequestOnAbsenceForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_HourlyRequestOnAbsence" style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td style="height: 10%">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbHourlyRequestOnAbsence" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbHourlyRequestOnAbsence" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbHourlyRequestOnAbsence_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbHourlyRequestOnAbsence_onClick();" />
                                        <%--DNN Note--%>
                                        <%--<ComponentArt:ToolBarItem ID="tlbItemSave_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbHourlyRequestOnAbsence" TextImageSpacing="5"
                                            Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" Enabled="false" />--%>
                                        <%--END of DNN Note--%>
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbHourlyRequestOnAbsence"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbHourlyRequestOnAbsence"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbHourlyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbHourlyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbHourlyRequestOnAbsence" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="tdSelectedDate_HourlyRequestOnAbsence" class="HeaderLabel" style="width: 30%"></td>
                            <td runat="server" id="ActionMode_HourlyRequestOnAbsence" class="ToolbarMode" style="width: 10%"
                                meta:resourcekey="InverseAlignObj"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 25%">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_AbsenceDetails_HourlyRequestOnAbsence" class="HeaderLabel" style="width: 50%">Absence Details
                                        </td>
                                        <td id="loadingPanel_GridAbsencePairs_RequestOnAbsence" class="HeaderLabel" style="width: 45%"></td>
                                        <td id="Td5" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridAbsencePairs_RequestOnAbsence" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridAbsencePairs_RequestOnAbsence"
                                                        runat="server" ClientSideCommand="Refresh_GridAbsencePairs_RequestOnAbsence();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridAbsencePairs_RequestOnAbsence"
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
                                <ComponentArt:CallBack ID="CallBack_GridAbsencePairs_RequestOnAbsence" runat="server"
                                    OnCallback="CallBack_GridAbsencePairs_RequestOnAbsence_onCallBack" Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridAbsencePairs_RequestOnAbsence" runat="server"
                                            CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                            FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                            PageSize="4" RunningMode="Client" Width="590" AllowMultipleSelect="false" AllowColumnResizing="false"
                                            ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                            ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" AllowHorizontalScrolling="true">
                                            <Levels>
                                                <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                    DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                    HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                            HeadingText="نوع غیبت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnAbsenceType_GridAbsencePairs_RequestOnAbsence" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="From" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridAbsencePairs_RequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="To" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridAbsencePairs_RequestOnAbsence" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <Load EventHandler="GridAbsencePairs_RequestOnAbsence_onLoad" />
                                                <ItemSelect EventHandler="GridAbsencePairs_RequestOnAbsence_onItemSelect" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_AbsencePairs" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridAbsencePairs_RequestOnAbsence_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 15%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5%">
                                <input type="radio" runat="server" id="rdbLeaveRequest_HourlyRequestOnAbsence" name="HourlyRequestOnAbsence"
                                    onclick="rdbLeaveRequest_HourlyRequestOnAbsence_onClick();" />
                            </td>
                            <td style="width: 95%">
                                <asp:Label runat="server" ID="lblLeaveRequest_HourlyRequestOnAbsence" Text="درخواست مرخصی"
                                    meta:resourcekey="lblLeaveRequest_HourlyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                                <div class="dhtmlgoodies_contentBox" id="box_LeaveRequest_HourlyRequestOnAbsence"
                                    style="width: 70%;">
                                    <div class="dhtmlgoodies_content" id="subbox_LeaveRequest_HourlyRequestOnAbsence">
                                        <table class="BoxStyle" style="width: 99%; height: 95%;">
                                            <tr>
                                                <td colspan="2">
                                                    <componentart:toolbar id="TlbHourlyRequestOnAbsence_LeaveRequest" runat="server" cssclass="toolbar"
                                                        defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                                        defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                                        defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                                        defaultitemtextimagespacing="0" imagesbaseurl="images/ToolBar/" itemspacing="1px"
                                                        usefadeeffect="false">
                                                        <Items> 
                                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbHourlyRequestOnAbsence_LeaveRequest" runat="server"
                                                                DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbHourlyRequestOnAbsence_onClick();"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemSave_TlbHourlyRequestOnAbsence" TextImageSpacing="5"
                                                                Enabled="false" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbHourlyRequestOnAbsence_LeaveRequest" runat="server"
                                                                DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbHourlyRequestOnAbsence_onClick();"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemCancel_TlbHourlyRequestOnAbsence"
                                                                TextImageSpacing="5" Enabled="false" />
                                                        </Items>
                                                    </componentart:toolbar>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px; width: 20px;">
                                                    <img alt="CloseDialog" src="Images/Dialog/close-down.png" style="cursor: pointer;" onclick="HourlyRequestOnAbsence_onCancel();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblLeaveType_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblLeaveType_HourlyRequestOnAbsence" Text=": نوع مرخصی"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbLeaveType_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbLeaveType_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="100" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbLeaveType_HourlyRequestOnAbsence_onChange" />
                                                                    <Expand EventHandler="cmbLeaveType_HourlyRequestOnAbsence_onExpand" />
                                                                    <Collapse EventHandler="cmbLeaveType_HourlyRequestOnAbsence_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_LeaveTypes" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbLeaveType_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <%-- strat toolbar--%>

                                                <td id="Substitute_LeaveRequest_TlbHourlyRequestOnAbsence" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbSubstitute_LeaveRequest_TlbHourlyRequestOnAbsence" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSubstitute_LeaveRequest_TlbSubstitute"
                                                                runat="server" ClientSideCommand="tlbSubstitute_HourlyRequestOnAbsence_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exceptions.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSubstitute_TlbHourlyRequestOnAbsence"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <%--   end toolbar--%>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table id="tblPairsContainer_Leave_HourlyRequestOnAbsence" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 50%" valign="top">
                                                                <table style="width: 100%; border: 1px outset black">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblFromHour_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFromHour_Leave_HourlyRequestOnAbsence" Text=": از ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <MKB:TimeSelector ID="TimeSelector_FromHour_Leave_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table style="width: 100%; border: 1px outset black">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblToHour_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToHour_Leave_HourlyRequestOnAbsence" Text=": تا ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <MKB:TimeSelector ID="TimeSelector_ToHour_Leave_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%" valign="top">
                                                                <table runat="server" id="tblToHourInNextDay_Leave_HourlyRequestOnAbsence" style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 5%">
                                                                            <input id="chbToHourInNextDay_Leave_HourlyRequestOnAbsence" type="checkbox" onclick="chbToHourInNextDay_Leave_HourlyRequestOnAbsence_onclick();"/></td>
                                                                        <td>
                                                                            <asp:Label ID="lblToHourInNextDay_Leave_HourlyRequestOnAbsence" runat="server" Text="زمان انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_Leave_HourlyRequestOnAbsence"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table runat="server" id="tblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence" style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 5%">
                                                                            <input id="chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence" type="checkbox" onclick="chbFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence_onclick();"/></td>
                                                                        <td>
                                                                            <asp:Label ID="lblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence" runat="server" Text="زمان ابتدا و انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblFromAndToHourInNextDay_Leave_HourlyRequestOnAbsence"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblDescription_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDescription_Leave_HourlyRequestOnAbsence" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <textarea id="txtDescription_Leave_HourlyRequestOnAbsence" class="TextBoxes" cols="20" name="S1" rows="2" style="width: 98%"></textarea>
                                                </td>
                                            </tr>
                                            <tr style="visibility: hidden">
                                                <td colspan="2">
                                                    <table style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAttachment_Leave_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_Leave_HourlyRequestOnAbsence" Text="ضمیمه :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 58%">
                                                                            <ComponentArt:CallBack ID="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence" runat="server" OnCallback="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBack">
                                                                                <Content>
                                                                                    <cc1:FileUploaderAJAX ID="AttachmentUploader_Leave_HourlyRequestOnAbsence" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_Leave_HourlyRequestOnAbsence" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <CallbackComplete EventHandler="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="Callback_AttachmentUploader_Leave_HourlyRequestOnAbsence_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbDeleteAttachment_Leave_HourlyRequestOnAbsence" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_HourlyRequestOnAbsence" TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                        <td id="tdAttachmentName_Leave_HourlyRequestOnAbsence"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="40%">
                                                                <asp:Label ID="lblDoctorName_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDoctorName_HourlyRequestOnAbsence" Text=": نام دکتر"></asp:Label>
                                                            </td>
                                                            <td width="10%"></td>
                                                            <td width="40%">
                                                                <asp:Label ID="lblIllnessName_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblIllnessName_HourlyRequestOnAbsence" Text=": نام بیماری"></asp:Label>
                                                            </td>
                                                            <td width="10%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ComponentArt:CallBack ID="CallBack_cmbDoctorName_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbDoctorName_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="99%" DropDownWidth="300">
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbDoctorName_HourlyRequestOnAbsence_onExpand" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField ID="ErrorHiddenField_Doctors" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbDoctorName_HourlyRequestOnAbsence_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:ToolBar ID="TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    Style="direction: ltr;" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence"
                                                                            runat="server" ClientSideCommand="tlbItemAdd_TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbDefineDoctor_tbHourly_HourlyRequestOnAbsence"
                                                                            TextImageSpacing="5" />

                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:CallBack ID="CallBack_cmbIllnessName_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbIllnessName_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="99%">
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbIllnessName_HourlyRequestOnAbsence_onExpand" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField ID="ErrorHiddenField_Illnesses" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbIllnessName_HourlyRequestOnAbsence_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:ToolBar ID="TlbDefineIllness_tbHourly_HourlyRequestOnAbsence" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    Style="direction: ltr;" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbDefineIllness_tbHourly_HourlyRequestOnAbsence"
                                                                            runat="server" ClientSideCommand="tlbItemAdd_TlbDefineIllness_tbHourly_HourlyRequestOnAbsence_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbDefineIllness_tbHourly_HourlyRequestOnAbsence"
                                                                            TextImageSpacing="5" />

                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="dhtmlgoodies_contentBox" id="box_MissionRequest_HourlyRequestOnAbsence"
                                    style="width: 70%;">
                                    <div class="dhtmlgoodies_content" id="subbox_MissionRequest_HourlyRequestOnAbsence">
                                        <table class="BoxStyle" style="width: 99%;">
                                            <tr>
                                                <td colspan="2">
                                                    <%--DNN Note--%>
                                                    <componentart:toolbar id="TlbHourlyRequestOnAbsence_MissionRequest" runat="server" cssclass="toolbar"
                                                        defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                                        defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                                        defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                                        defaultitemtextimagespacing="0" imagesbaseurl="images/ToolBar/" itemspacing="1px"
                                                        usefadeeffect="false">
                                                        <Items> 
                                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbHourlyRequestOnAbsence_MissionRequest" runat="server"
                                                                DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbHourlyRequestOnAbsence_onClick();"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemSave_TlbHourlyRequestOnAbsence" TextImageSpacing="5"
                                                                Enabled="false" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbHourlyRequestOnAbsence_MissionRequest" runat="server"
                                                                DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbHourlyRequestOnAbsence_onClick();"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemCancel_TlbHourlyRequestOnAbsence"
                                                                TextImageSpacing="5" Enabled="false" />
                                                        </Items>
                                                    </componentart:toolbar>
                                                    <%--END DNN Note--%>
                                                </td>
                                                <td style="padding-right: 5px; padding-left: 5px; width: 20px;">
                                                    <img alt="CloseDialog" src="Images/Dialog/close-down.png" style="cursor: pointer;" onclick="HourlyRequestOnAbsence_onCancel();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMissionType_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblMissionType_HourlyRequestOnAbsence" Text=": نوع ماموریت"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbMissionType_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMissionType_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DataTextField="Name" 
                                                                DataValueField="ID" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" 
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbMissionType_HourlyRequestOnAbsence_onExpand" />
                                                                    <Collapse EventHandler="cmbMissionType_HourlyRequestOnAbsence_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_MissionTypes" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbMissionType_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMissionType_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td id="Substitute_MissionRequest_TlbHourlyRequestOnAbsence" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbSubstitute_MissionRequest_TlbHourlyRequestOnAbsence" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSubstitute_MissionRequest_TlbSubstitute"
                                                                runat="server" ClientSideCommand="tlbSubstitute_HourlyRequestOnAbsence_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exceptions.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSubstitute_TlbHourlyRequestOnAbsence"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table id="tblPairsContainer_Mission_HourlyRequestOnAbsence" style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 50%" valign="top">
                                                                <table style="width: 100%; border: 1px outset black">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblFromHour_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFromHour_Mission_HourlyRequestOnAbsence" Text=": از ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <MKB:TimeSelector ID="TimeSelector_FromHour_Mission_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table style="width: 100%; border: 1px outset black">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblToHour_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToHour_Mission_HourlyRequestOnAbsence" Text=": تا ساعت"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <MKB:TimeSelector ID="TimeSelector_ToHour_Mission_HourlyRequestOnAbsence" runat="server" DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                                                                            </MKB:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%" valign="top">
                                                                <table runat="server" id="tblToHourInNextDay_Mission_HourlyRequestOnAbsence" style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 5%">
                                                                            <input id="chbToHourInNextDay_Mission_HourlyRequestOnAbsence" type="checkbox" onclick="chbToHourInNextDay_Mission_HourlyRequestOnAbsence_onClick();"/></td>
                                                                        <td>
                                                                            <asp:Label ID="lblToHourInNextDay_Mission_HourlyRequestOnAbsence" runat="server" Text="زمان انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_Mission_HourlyRequestOnAbsence"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <table runat="server" id="tblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence" style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 5%">
                                                                            <input id="chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence" type="checkbox" onclick="chbFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence_onclick();"/></td>
                                                                        <td>
                                                                            <asp:Label ID="lblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence" runat="server" Text="زمان ابتدا و انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblFromAndToHourInNextDay_Mission_HourlyRequestOnAbsence"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblDescription_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDescription_Mission_HourlyRequestOnAbsence" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <textarea id="txtDescription_Mission_HourlyRequestOnAbsence" class="TextBoxes" cols="20" name="S2" rows="2" style="width: 98%"></textarea>
                                                </td>
                                            </tr>
                                            <%--DNN Note: جابجایی فایل ضمیمه با محل ماموریت--%>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMissionLocation_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblMissionLocation_HourlyRequestOnAbsence" Text=": محل ماموریت"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack ID="CallBack_cmbMissionLocation_HourlyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMissionLocation_HourlyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="175" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100%">
                                                                <DropDownContent>
                                                                    <ComponentArt:TreeView ID="trvMissionLocation_HourlyRequestOnAbsence" runat="server" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="95%" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" meta:resourcekey="trvMissionLocation_HourlyRequestOnAbsence" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" Width="100%">
                                                                        <ClientEvents>
                                                                            <NodeSelect EventHandler="trvMissionLocation_HourlyRequestOnAbsence_onNodeSelect" />
                                                                            <NodeExpand EventHandler="trvMissionLocation_HourlyRequestOnAbsence_onNodeExpand" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:TreeView>
                                                                </DropDownContent>
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbMissionLocation_HourlyRequestOnAbsence_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_MissionLocations" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMissionLocation_HourlyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbMissionSearch_HourlyRequestOnAbsence" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemMissionSearch_TlbMissionSearch_HourlyRequestOnAbsence"
                                                                runat="server" ClientSideCommand="tlbItemMissionSearch_TlbMissionSearch_HourlyRequestOnAbsence_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemMissionSearch_TlbMissionSearch_HourlyRequestOnAbsence"
                                                                TextImageSpacing="5" Enabled="true" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                            <%--End DNN Note--%>
                                            <tr style="visibility: hidden">
                                                <td colspan="2">
                                                    <table style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAttachment_Mission_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_Mission_HourlyRequestOnAbsence" Text="ضمیمه :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 58%">
                                                                            <componentart:callback id="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence" runat="server" oncallback="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBack">
                                                                                <Content>
                                                                                    <cc1:FileUploaderAJAX ID="AttachmentUploader_Mission_HourlyRequestOnAbsence" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_Mission_HourlyRequestOnAbsence" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <CallbackComplete EventHandler="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="Callback_AttachmentUploader_Mission_HourlyRequestOnAbsence_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </componentart:callback>
                                                                        </td>
                                                                        <td style="width: 5%">

                                                                            <componentart:toolbar id="TlbDeleteAttachment_Mission_HourlyRequestOnAbsence" runat="server" cssclass="toolbar" defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked" defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover" defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText" imagesbaseurl="images/ToolBar/" itemspacing="1px" usefadeeffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_HourlyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_HourlyRequestOnAbsence_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_HourlyRequestOnAbsence" TextImageSpacing="5" />
                                                                                </Items>
                                                                            </componentart:toolbar>

                                                                        </td>
                                                                        <td id="tdAttachmentName_Mission_HourlyRequestOnAbsence"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>


                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="radio" runat="server" id="rdbMissionRequest_HourlyRequestOnAbsence"
                                    name="HourlyRequestOnAbsence" onclick="rdbMissionRequest_HourlyRequestOnAbsence_onClick();" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblMissionRequest_HourlyRequestOnAbsence" Text="درخواست ماموریت"
                                    meta:resourcekey="lblMissionRequest_HourlyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 50%">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td id="" style="color: White; font-weight: bold; font-family: Arial; width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_RegisteredRequests_HourlyRequestOnAbsence" class="HeaderLabel" style="width: 50%">Registered Requests
                                        </td>
                                        <td id="loadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                        runat="server" ClientSideCommand="Refresh_GridRegisteredRequests_HourlyRequestOnAbsence();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence"
                                    OnCallback="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallBack"
                                    Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRegisteredRequests_HourlyRequestOnAbsence" runat="server"
                                            CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PageSize="5" RunningMode="Client" Width="590" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" AllowHorizontalScrolling="true">
                                            <Levels>
                                                <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                    DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                    HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                            HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestType_GridRegisteredRequests_HourlyRequestOnAbsence" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ ثبت درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDate_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="StatusTitle" DefaultSortDirection="Descending"
                                                            DataCellClientTemplateId="DataCellClientTemplate_clmnState_GridRegisteredRequests_HourlyRequestOnAbsence"
                                                            HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_HourlyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn DataField="Status" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnState_GridRegisteredRequests_HourlyRequestOnAbsence">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center">##GetRequestStateTitle_HourlyRequestOnAbsence(DataItem.GetMember('Status').Value)##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRegisteredRequests_HourlyRequestOnAbsence_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRegisteredRequests_HourlyRequestOnAbsence_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="300px">
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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancelConfirm"
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
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogMissionLocationSearch"
            runat="server" Width="350px">
            <Content>
                <table id="Mastertbl_DialogMissionLocationSearch" style="width: 100%;" class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbMissionLocationSearch_HourlyRequestOnAbsence" runat="server"
                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbMissionLocationSearch_HourlyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemSave_TlbMissionLocationSearch_HourlyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbMissionLocationSearch_HourlyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbMissionLocationSearch_HourlyRequestOnAbsence"
                                        runat="server" ClientSideCommand="tlbItemSearch_TlbMissionLocationSearch_HourlyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbMissionLocationSearch_HourlyRequestOnAbsence"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMissionLocationSearch_HourlyRequestOnAbsence" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbMissionLocationSearch_HourlyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMissionLocationSearch_HourlyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchTermMissionLocation_HourlyRequestOnAbsence" runat="server" Text=": جستجوي محل ماموریت"
                                meta:resourcekey="lblSearchTermMissionLocation_HourlyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>

                                        <input id="txtSearchTermMissionLocation_HourlyRequestOnAbsence" type="text" class="TextBoxes"
                                            style="width: 87%" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMissionLocationSearchResult_HourlyRequestOnAbsence" runat="server" Text=": نتايج جستجوي محل ماموریت"
                                            meta:resourcekey="lblMissionLocationSearchResult_HourlyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence"
                                            OnCallback="CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbMissionLocationSearchResult_HourlyRequestOnAbsence" runat="server"
                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                    ExpandDirection="Up" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150"
                                                    DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                    Style="width: 90%" TextBoxCssClass="comboTextBox">
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MissionLocationSearchResult_HourlyRequestOnAbsence" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbMissionLocationSearchResult_HourlyRequestOnAbsence_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="Default" ID="DialogSubstitute"
            runat="server" Width="500px ">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 60%;" id="Container_PersonnelSelect_HourlyRequestOnAbsence" class="BodyStyle">
                    <tr>
                        <td style="width: 55%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:ToolBar ID="TlbSubstitute" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbSubstitute" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemSave_TlbSubstitute_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbSubstitute"
                                                    TextImageSpacing="5" Enabled="true" />
                                                <ComponentArt:ToolBarItem ID="tlbItemRefuse_TlbSubstitute" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemRefuse_TlbSubstitute_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefuse_TlbSubstitute"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPersonnel_HourlyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                            meta:resourcekey="lblSubstitute_HourlyRequestOnAbsence" Text=": جانشین"></asp:Label>
                                    </td>
                                    <td id="Td4" runat="server" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_HourlyRequestOnAbsence" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr;" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_HourlyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 12%"></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel_HourlyRequestOnAbsence" runat="server"
                                OnCallback="CallBack_cmbPersonnel_HourlyRequestOnAbsence_onCallBack" Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbPersonnel_HourlyRequestOnAbsence" runat="server" AutoComplete="true"
                                        AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                        DropDownCssClass="comboDropDown" DropDownHeight="150" DropDownPageSize="5" DropDownWidth="300"
                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_HourlyRequestOnAbsence"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_HourlyRequestOnAbsence">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr class="dataRow">
                                                        <td class="dataCell" style="width: 40%">## DataItem.getProperty('Text') ##
                                                        </td>
                                                        <td class="dataCell" style="width: 30%">## DataItem.getProperty('BarCode') ##
                                                        </td>
                                                        <td class="dataCell" style="width: 30%">## DataItem.getProperty('CardNum') ##
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ComponentArt:ClientTemplate>
                                        </ClientTemplates>
                                        <DropDownHeader>
                                            <table border="0" cellpadding="0" cellspacing="0" width="300">
                                                <tr class="headingRow">
                                                    <td id="clmnName_cmbPersonnel_HourlyRequestOnAbsence" class="headingCell" style="width: 40%; text-align: center">Name And Family
                                                    </td>
                                                    <td id="clmnBarCode_cmbPersonnel_HourlyRequestOnAbsence" class="headingCell" style="width: 30%; text-align: center">BarCode
                                                    </td>
                                                    <td id="clmnCardNum_cmbPersonnel_HourlyRequestOnAbsence" class="headingCell" style="width: 30%; text-align: center">CardNum
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownHeader>
                                        <ClientEvents>
                                            <Change EventHandler="cmbPersonnel_HourlyRequestOnAbsence_onChange" />
                                            <Expand EventHandler="cmbPersonnel_HourlyRequestOnAbsence_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField ID="ErrorHiddenField_Personnel_HourlyRequestOnAbsence" runat="server" />
                                    <asp:HiddenField ID="hfPersonnelPageCount_HourlyRequestOnAbsence" runat="server" />
                                    <asp:HiddenField ID="hfPersonnelCount_HourlyRequestOnAbsence" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel_HourlyRequestOnAbsence_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel_HourlyRequestOnAbsence_onCallBackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbPersonnel_HourlyRequestOnAbsence_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <input id="txtPersonnelSearch_HourlyRequestOnAbsence" runat="server" class="TextBoxes"
                                onkeypress="txtPersonnelSearch_HourlyRequestOnAbsence_onKeyPess(event);" style="width: 99%" type="text" />
                        </td>
                        <td>
                            <ComponentArt:ToolBar ID="TlbSearchPersonnel_HourlyRequestOnAbsence" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_HourlyRequestOnAbsence" runat="server"
                                        ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_HourlyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_HourlyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <%--<td>
                            <ComponentArt:ToolBar ID="TlbAdvancedSearch_HourlyRequestOnAbsence" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_HourlyRequestOnAbsence"
                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_HourlyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_HourlyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>--%>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogSubstitute_OnShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" Modal="true" AllowResize="false"
            runat="server" AllowDrag="false" Alignment="MiddleCentre" ID="Dialog1">
            <Content>
                <table>
                    <tr>
                        <td>
                            <img id="Img2" runat="server" alt="" src="~/DesktopModules/Atlas/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_HourlyRequestOnAbsence" meta:resourcekey="hfclmnName_cmbPersonnel_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_HourlyRequestOnAbsence" meta:resourcekey="hfclmnBarCode_cmbPersonnel_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_HourlyRequestOnAbsence" meta:resourcekey="hfclmnCardNum_cmbPersonnel_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogHourlyRequestOnAbsence" meta:resourcekey="hfTitle_DialogHourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfheader_AbsenceDetails_HourlyRequestOnAbsence"
            meta:resourcekey="hfheader_AbsenceDetails_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridAbsencePairs_RequestOnAbsence"
            meta:resourcekey="hfloadingPanel_GridAbsencePairs_RequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_HourlyRequestOnAbsence"
            meta:resourcekey="hfheader_RegisteredRequests_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence"
            meta:resourcekey="hfloadingPanel_GridRegisteredRequests_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_HourlyRequestOnAbsence" meta:resourcekey="hfDeleteMessage_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_HourlyRequestOnAbsence" meta:resourcekey="hfCloseMessage_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_HourlyRequestOnAbsence" meta:resourcekey="hfcmbAlarm_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfRequestStates_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfAdd_HourlyRequestOnAbsence" meta:resourcekey="hfAdd_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfDelete_HourlyRequestOnAbsence" meta:resourcekey="hfDelete_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfView_HourlyRequestOnAbsence" meta:resourcekey="hfView_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfErrorType_HourlyRequestOnAbsence" meta:resourcekey="hfErrorType_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfConnectionError_HourlyRequestOnAbsence" meta:resourcekey="hfConnectionError_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfRequestMaxLength_HourlyRequestOnAbsence" meta:resourcekey="hfRequestMaxLength_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfCloseWarningMessage_HourlyRequestOnAbsence" meta:resourcekey="hfCloseWarningMessage_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfMissionType_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfLeaveType_HourlyRequestOnAbsence" />

    </form>
</body>
</html>
