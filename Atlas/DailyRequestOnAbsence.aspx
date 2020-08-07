<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="GTS.Clock.Presentaion.WebForms.DailyRequestOnAbsence" CodeBehind="DailyRequestOnAbsence.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="DailyRequestOnAbsenceForm" runat="server" meta:resourcekey="DailyRequestOnAbsenceForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_DailyRequestOnAbsence" style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td style="height: 10%">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <componentart:toolbar id="TlbDailyRequestOnAbsence" runat="server" cssclass="toolbar"
                                    defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                    defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                    defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                    defaultitemtextimagespacing="0" imagesbaseurl="images/ToolBar/" itemspacing="1px"
                                    usefadeeffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbDailyRequestOnAbsence" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbDailyRequestOnAbsence_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbDailyRequestOnAbsence"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbDailyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbDailyRequestOnAbsence"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbDailyRequestOnAbsence_onClick();" />
                                          <%--DNN Note--%>
                                      <%--  <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDailyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbDailyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbDailyRequestOnAbsence" TextImageSpacing="5"
                                            Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDailyRequestOnAbsence" runat="server"
                                            ClientSideCommand="tlbItemCancel_TlbDailyRequestOnAbsence_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDailyRequestOnAbsence"
                                            TextImageSpacing="5" Enabled="false" />--%>
                                         <%--END of DNN Note--%>
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbDailyRequestOnAbsence"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbDailyRequestOnAbsence_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbDailyRequestOnAbsence"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDailyRequestOnAbsence" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemExit_TlbDailyRequestOnAbsence_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbDailyRequestOnAbsence" TextImageSpacing="5" />
                                    </Items>
                                </componentart:toolbar>
                            </td>
                            <td id="tdSelectedDate_DailyRequestOnAbsence" class="HeaderLabel" style="width: 30%"></td>
                            <td runat="server" id="ActionMode_DailyRequestOnAbsence" class="ToolbarMode" style="width: 10%"
                                meta:resourcekey="InverseAlignObj"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" style="height: 15%">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5%">
                                <input type="radio" runat="server" id="rdbLeaveRequest_DailyRequestOnAbsence" name="DailyRequestOnAbsence"
                                    onclick="rdbLeaveRequest_DailyRequestOnAbsence_onClick();" />
                            </td>
                            <td style="width: 95%">
                                <asp:Label runat="server" ID="lblLeaveRequest_DailyRequestOnAbsence" Text="درخواست مرخصی"
                                    meta:resourcekey="lblLeaveRequest_DailyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                                <div class="dhtmlgoodies_contentBox" id="box_LeaveRequest_DailyRequestOnAbsence"
                                    style="width: 70%;">
                                    <div class="dhtmlgoodies_content" id="subbox_LeaveRequest_DailyRequestOnAbsence">
                                        <table class="BoxStyle" style="width: 100%; height: 100%;">

                                            <tr>
                                                <td colspan="2">
                                                    <componentart:toolbar id="TlbDailyRequestOnAbsence_LeaveRequest" runat="server" cssclass="toolbar"
                                                        defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                                        defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                                        defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                                        defaultitemtextimagespacing="0" imagesbaseurl="images/ToolBar/" itemspacing="1px"
                                                        usefadeeffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDailyRequestOnAbsence_LeaveRequest" runat="server"
                                                                DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbDailyRequestOnAbsence_onClick();"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemSave_TlbDailyRequestOnAbsence" TextImageSpacing="5"
                                                                Enabled="false" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDailyRequestOnAbsence_LeaveRequest" runat="server"
                                                                ClientSideCommand="tlbItemCancel_TlbDailyRequestOnAbsence_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDailyRequestOnAbsence"
                                                                TextImageSpacing="5" Enabled="false" />
                                                             </Items>
                                                    </componentart:toolbar>
                                                </td>

                                                <td style="padding-right: 5px; padding-left: 5px; width: 15px;">
                                                    <img alt="CloseDialog" src="Images/Dialog/close-down.png" style="cursor: pointer;" onclick="DailyRequestOnAbsence_onCancel();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblLeaveType_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblLeaveType_DailyRequestOnAbsence" Text=": نوع مرخصی"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbLeaveType_DailyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbLeaveType_DailyRequestOnAbsence_onCallBack">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbLeaveType_DailyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="130" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="95%">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbLeaveType_DailyRequestOnAbsence_onChange" />
                                                                    <Expand EventHandler="cmbLeaveType_DailyRequestOnAbsence_onExpand" />
                                                                    <Collapse EventHandler="cmbLeaveType_DailyRequestOnAbsence_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_LeaveTypes" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbLeaveType_DailyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbLeaveType_DailyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbLeaveType_DailyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <%-- strat toolbar--%>

                                                <td id="Substitute_LeaveRequest_TlbDailyRequestOnAbsence" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbSubstitute_LeaveRequest_TlbDailyRequestOnAbsence" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSubstitute_LeaveRequest_TlbSubstitute"
                                                                runat="server" ClientSideCommand="tlbSubstitute_DailyRequestOnAbsence_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exceptions.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSubstitute_TlbDailyRequestOnAbsence"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <%--   end toolbar--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate_Leave_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblFromDate_Leave_DailyRequestOnAbsence" Text=": از تاریخ"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblToDate_Leave_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToDate_Leave_DailyRequestOnAbsence" Text=": تا تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_LeaveFromDateCalendars_DailyRequestOnAbsence" runat="server" meta:resourcekey="AlignObj">
                                                    <table id="Container_pdpFromDate_Leave_DailyRequestOnAbsence" runat="server" style="width: 100%" visible="false">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpFromDate_Leave_DailyRequestOnAbsence" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Container_gdpFromDate_Leave_DailyRequestOnAbsence" runat="server" style="width: 100%" visible="false">
                                                        <tr>
                                                            <td>
                                                                <table id="Container_gCalFromDate_Leave_DailyRequestOnAbsence" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpFromDate_Leave_DailyRequestOnAbsence_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpFromDate_Leave_DailyRequestOnAbsence" runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpFromDate_Leave_DailyRequestOnAbsence_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">&nbsp; </td>
                                                                        <td>
                                                                            <img id="btn_gdpFromDate_Leave_DailyRequestOnAbsence" alt="" class="calendar_button" onclick="btn_gdpFromDate_Leave_DailyRequestOnAbsence_OnClick(event)" onmouseup="btn_gdpFromDate_Leave_DailyRequestOnAbsence_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalFromDate_Leave_DailyRequestOnAbsence" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_Leave_DailyRequestOnAbsence" PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalFromDate_Leave_DailyRequestOnAbsence_OnChange" />
                                                                        <Load EventHandler="gCalFromDate_Leave_DailyRequestOnAbsence_onLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td id="Container_ToDateLeaveCalendars_DailyRequestOnAbsence" runat="server" meta:resourcekey="AlignObj">
                                                    <table id="Container_pdpToDate_Leave_DailyRequestOnAbsence" runat="server" style="width: 100%" visible="false">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpToDate_Leave_DailyRequestOnAbsence" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="Container_gdpToDate_Leave_DailyRequestOnAbsence" runat="server" style="width: 100%" visible="false">
                                                        <tr>
                                                            <td>
                                                                <table id="Container_gCalToDate_Leave_DailyRequestOnAbsence" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpToDate_Leave_DailyRequestOnAbsence_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpToDate_Leave_DailyRequestOnAbsence" runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpToDate_Leave_DailyRequestOnAbsence_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">&nbsp; </td>
                                                                        <td>
                                                                            <img id="btn_gdpToDate_Leave_DailyRequestOnAbsence" alt="" class="calendar_button" onclick="btn_gdpToDate_Leave_DailyRequestOnAbsence_OnClick(event)" onmouseup="btn_gdpToDate_Leave_DailyRequestOnAbsence_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalToDate_Leave_DailyRequestOnAbsence" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_Leave_DailyRequestOnAbsence" PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalToDate_Leave_DailyRequestOnAbsence_OnChange" />
                                                                        <Load EventHandler="gCalToDate_Leave_DailyRequestOnAbsence_OnLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblDescription_Leave_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDescription_Leave_DailyRequestOnAbsence" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <textarea id="txtDescription_Leave_DailyRequestOnAbsence" class="TextBoxes" cols="20" name="S1" rows="2" style="width: 98%"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAttachment_Leave_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_Leave_DailyRequestOnAbsence" Text="ضمیمه :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 56%">
                                                                            <ComponentArt:CallBack ID="Callback_AttachmentUploader_Leave_DailyRequestOnAbsence" runat="server" OnCallback="Callback_AttachmentUploader_Leave_DailyRequestOnAbsence_onCallBack">
                                                                                <Content>
                                                                                    <cc1:FileUploaderAJAX ID="AttachmentUploader_Leave_DailyRequestOnAbsence" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_Leave_DailyRequestOnAbsence" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <CallbackComplete EventHandler="Callback_AttachmentUploader_Leave_DailyRequestOnAbsence_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="Callback_AttachmentUploader_Leave_DailyRequestOnAbsence_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">

                                                                            <ComponentArt:ToolBar ID="TlbDeleteAttachment_Leave_DailyRequestOnAbsence" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_DailyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_DailyRequestOnAbsence_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_Leave_DailyRequestOnAbsence" TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>

                                                                        </td>
                                                                        <td id="tdAttachmentName_Leave_DailyRequestOnAbsence"></td>
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
                                                                <asp:Label ID="lblDoctorName_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDoctorName_DailyRequestOnAbsence" Text=": نام دکتر"></asp:Label>
                                                            </td>
                                                            <td width="10%"></td>
                                                            <td width="40%">
                                                                <asp:Label ID="lblIllnessName_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblIllnessName_DailyRequestOnAbsence" Text=": نام بیماری"></asp:Label>
                                                            </td>
                                                            <td width="10%"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <ComponentArt:CallBack ID="CallBack_cmbDoctorName_DailyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbDoctorName_DailyRequestOnAbsence_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbDoctorName_DailyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="99%" DropDownWidth="300">
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbDoctorName_DailyRequestOnAbsence_onExpand" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField ID="ErrorHiddenField_Doctors" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbDoctorName_DailyRequestOnAbsence_BeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbDoctorName_DailyRequestOnAbsence_CallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbDoctorName_DailyRequestOnAbsence_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:ToolBar ID="TlbDefineDoctor_tbDaily_DailyRequestOnAbsence" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    Style="direction: ltr;" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbDefineDoctor_tbDaily_DailyRequestOnAbsence"
                                                                            runat="server" ClientSideCommand="tlbItemAdd_TlbDefineDoctor_tbDaily_DailyRequestOnAbsence_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbDefineDoctor_tbDaily_DailyRequestOnAbsence"
                                                                            TextImageSpacing="5" />

                                                                    </Items>
                                                                </ComponentArt:ToolBar>

                                                            </td>
                                                            <td>
                                                                <ComponentArt:CallBack ID="CallBack_cmbIllnessName_DailyRequestOnAbsence" runat="server" Height="26" OnCallback="CallBack_cmbIllnessName_DailyRequestOnAbsence_onCallBack">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbIllnessName_DailyRequestOnAbsence" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownHeight="120" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="99%">
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbIllnessName_DailyRequestOnAbsence_onExpand" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField ID="ErrorHiddenField_Illnesses" runat="server" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbIllnessName_DailyRequestOnAbsence_BeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbIllnessName_DailyRequestOnAbsence_CallbackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbIllnessName_DailyRequestOnAbsence_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:ToolBar ID="TlbDefineIllness_tbDaily_DailyRequestOnAbsence" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    Style="direction: ltr;" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdd_TlbDefineIllness_tbDaily_DailyRequestOnAbsence"
                                                                            runat="server" ClientSideCommand="tlbItemAdd_TlbDefineIllness_tbDaily_DailyRequestOnAbsence_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdd_TlbDefineIllness_tbDaily_DailyRequestOnAbsence"
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="radio" runat="server" id="rdbMissionRequest_DailyRequestOnAbsence" name="DailyRequestOnAbsence"
                                    onclick="rdbMissionRequest_DailyRequestOnAbsence_onClick();" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblMissionRequest_DailyRequestOnAbsence" Text="درخواست ماموریت"
                                    meta:resourcekey="lblMissionRequest_DailyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                                <div class="dhtmlgoodies_contentBox" id="box_MissionRequest_DailyRequestOnAbsence"
                                    style="width: 70%;">
                                    <div class="dhtmlgoodies_content" id="subbox_MissionRequest_DailyRequestOnAbsence">
                                        <table class="BoxStyle" style="width: 100%;">
                                            <tr>
                                                <td colspan="2">
                                                    <componentart:toolbar id="TlbDailyRequestOnAbsence_MissionRequest" runat="server" cssclass="toolbar"
                                                        defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                                        defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                                        defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                                        defaultitemtextimagespacing="0" imagesbaseurl="images/ToolBar/" itemspacing="1px"
                                                        usefadeeffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDailyRequestOnAbsence_MissionRequest" runat="server"
                                                                DropDownImageHeight="16px" ClientSideCommand="tlbItemSave_TlbDailyRequestOnAbsence_onClick();"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemSave_TlbDailyRequestOnAbsence" TextImageSpacing="5"
                                                                Enabled="false" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDailyRequestOnAbsence_MissionRequest" runat="server"
                                                                ClientSideCommand="tlbItemCancel_TlbDailyRequestOnAbsence_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDailyRequestOnAbsence"
                                                                TextImageSpacing="5" Enabled="false" />
                                                             </Items>
                                                    </componentart:toolbar>

                                                </td>

                                                <td style="padding-right: 5px; padding-left: 5px; width: 15px;">
                                                    <img alt="CloseDialog" src="Images/Dialog/close-down.png" style="cursor: pointer;" onclick="DailyRequestOnAbsence_onCancel();" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblMissionType_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblMissionType_DailyRequestOnAbsence" Text=": نوع ماموریت"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <ComponentArt:CallBack ID="CallBack_cmbMissionType_DailyRequestOnAbsence" runat="server"
                                                        OnCallback="CallBack_cmbMissionType_DailyRequestOnAbsence_onCallBack" Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMissionType_DailyRequestOnAbsence" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DataTextField="Name" DataValueField="ID" DropDownHeight="120" DropDownResizingMode="Corner"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                TextBoxEnabled="true" Width="95%">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbMissionType_DailyRequestOnAbsence_onExpand" />
                                                                    <Collapse EventHandler="cmbMissionType_DailyRequestOnAbsence_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_MissionTypes" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbMissionType_DailyRequestOnAbsence_BeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbMissionType_DailyRequestOnAbsence_CallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMissionType_DailyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                 <%-- strat toolbar--%>
                           
                                                <td id="Substitute_MissionType_TlbDailyRequestOnAbsence" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbSubstitute_MissionType_TlbDailyRequestOnAbsence" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbSubstitute_MissionType_TlbSubstitute"
                                                                runat="server" ClientSideCommand="tlbSubstitute_DailyRequestOnAbsence_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exceptions.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSubstitute_TlbDailyRequestOnAbsence"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <%--   end toolbar--%>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate_Mission_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblFromDate_Mission_DailyRequestOnAbsence" Text=": از تاریخ"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblToDate_Mission_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblToDate_Mission_DailyRequestOnAbsence" Text=": تا تاریخ"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Container_FromDateMissionCalendars_DailyRequestOnAbsence" runat="server"
                                                    meta:resourcekey="AlignObj">
                                                    <table runat="server" id="Container_pdpFromDate_Mission_DailyRequestOnAbsence" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpFromDate_Mission_DailyRequestOnAbsence" runat="server"
                                                                    ReadOnly="true" CssClass="PersianDatePicker"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpFromDate_Mission_DailyRequestOnAbsence" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalFromDate_Mission_DailyRequestOnAbsence">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpFromDate_Mission_DailyRequestOnAbsence_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpFromDate_Mission_DailyRequestOnAbsence" runat="server"
                                                                                ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpFromDate_Mission_DailyRequestOnAbsence_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpFromDate_Mission_DailyRequestOnAbsence" alt="" class="calendar_button"
                                                                                onclick="btn_gdpFromDate_Mission_DailyRequestOnAbsence_OnClick(event)" onmouseup="btn_gdpFromDate_Mission_DailyRequestOnAbsence_OnMouseUp(event)"
                                                                                src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <ComponentArt:Calendar ID="gCalFromDate_Mission_DailyRequestOnAbsence" runat="server"
                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                        ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                        PopUpExpandControlId="btn_gdpFromDate_Mission_DailyRequestOnAbsence" PrevImageUrl="cal_prevMonth.gif"
                                                        SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                        SwapSlide="Linear" VisibleDate="2008-1-1">
                                                        <ClientEvents>
                                                            <SelectionChanged EventHandler="gCalFromDate_Mission_DailyRequestOnAbsence_OnChange" />
                                                            <Load EventHandler="gCalFromDate_Mission_DailyRequestOnAbsence_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:Calendar>
                                                </td>
                                                <td id="Container_ToDateMissionCalendars_DailyRequestOnAbsence" runat="server" meta:resourcekey="AlignObj">
                                                    <table runat="server" id="Container_pdpToDate_Mission_DailyRequestOnAbsence" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <pcal:PersianDatePickup ID="pdpToDate_Mission_DailyRequestOnAbsence" runat="server"
                                                                    ReadOnly="true" CssClass="PersianDatePicker"></pcal:PersianDatePickup>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table runat="server" id="Container_gdpToDate_Mission_DailyRequestOnAbsence" visible="false"
                                                        style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" id="Container_gCalToDate_Mission_DailyRequestOnAbsence">
                                                                    <tr>
                                                                        <td onmouseup="btn_gdpToDate_Mission_DailyRequestOnAbsence_OnMouseUp(event)">
                                                                            <ComponentArt:Calendar ID="gdpToDate_Mission_DailyRequestOnAbsence" runat="server"
                                                                                ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gdpToDate_Mission_DailyRequestOnAbsence_OnDateChange" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                        <td style="font-size: 10px;">&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <img id="btn_gdpToDate_Mission_DailyRequestOnAbsence" alt="" class="calendar_button"
                                                                                onclick="btn_gdpToDate_Mission_DailyRequestOnAbsence_OnClick(event)" onmouseup="btn_gdpToDate_Mission_DailyRequestOnAbsence_OnMouseUp(event)"
                                                                                src="Images/Calendar/btn_calendar.gif" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <ComponentArt:Calendar ID="gCalToDate_Mission_DailyRequestOnAbsence" runat="server"
                                                                    AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                    CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                    DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                    ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                    NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                    PopUpExpandControlId="btn_gdpToDate_Mission_DailyRequestOnAbsence" PrevImageUrl="cal_prevMonth.gif"
                                                                    SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                    SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                    <ClientEvents>
                                                                        <SelectionChanged EventHandler="gCalToDate_Mission_DailyRequestOnAbsence_OnChange" />
                                                                        <Load EventHandler="gCalToDate_Mission_DailyRequestOnAbsence_onLoad" />
                                                                    </ClientEvents>
                                                                </ComponentArt:Calendar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <%--DNN Note: جابجایی محل ماموریت با فایل ضمیمه در نمایش فرم--%>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblMissionLocation_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblMissionLocation_DailyRequestOnAbsence" Text=": محل ماموریت"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack ID="CallBack_cmbMissionLocation_DailyRequestOnAbsence" runat="server"
                                                        OnCallback="CallBack_cmbMissionLocation_DailyRequestOnAbsence_onCallBack" Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbMissionLocation_DailyRequestOnAbsence" runat="server"
                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                DropDownCssClass="comboDropDown" DropDownHeight="200" DropDownResizingMode="Corner"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                ExpandDirection="Up" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" Width="95%" TextBoxEnabled="true">
                                                                <DropDownContent>
                                                                    <ComponentArt:TreeView ID="trvMissionLocation_DailyRequestOnAbsence" runat="server"
                                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="95%"
                                                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                                        LineImageWidth="19" meta:resourcekey="trvMissionLocation_DailyRequestOnAbsence"
                                                                        NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                                        SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" Width="100%">
                                                                        <ClientEvents>
                                                                            <NodeSelect EventHandler="trvMissionLocation_DailyRequestOnAbsence_onNodeSelect" />
                                                                            <NodeExpand EventHandler="trvMissionLocation_DailyRequestOnAbsence_onNodeExpand" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:TreeView>
                                                                </DropDownContent>
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbMissionLocation_DailyRequestOnAbsence_onExpand" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField ID="ErrorHiddenField_MissionLocations" runat="server" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBack_cmbMissionLocation_DailyRequestOnAbsence_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBack_cmbMissionLocation_DailyRequestOnAbsence_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBack_cmbMissionLocation_DailyRequestOnAbsence_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbMissionSearch_DailyRequestOnAbsence" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemMissionSearch_TlbMissionSearch_DailyRequestOnAbsence"
                                                                runat="server" ClientSideCommand="tlbItemMissionSearch_TlbMissionSearch_DailyRequestOnAbsence_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemMissionSearch_TlbMissionSearch_DailyRequestOnAbsence"
                                                                TextImageSpacing="5" Enabled="true" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblAttachment_Mission_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_Mission_DailyRequestOnAbsence" Text="ضمیمه :"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 56%">
                                                                <componentart:callback id="Callback_AttachmentUploader_Mission_DailyRequestOnAbsence" runat="server" oncallback="Callback_AttachmentUploader_Mission_DailyRequestOnAbsence_onCallBack">
                                                                    <Content>
                                                                        <cc1:FileUploaderAJAX ID="AttachmentUploader_Mission_DailyRequestOnAbsence" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_Mission_DailyRequestOnAbsence" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <CallbackComplete EventHandler="Callback_AttachmentUploader_Mission_DailyRequestOnAbsence_onCallBackComplete" />
                                                                        <CallbackError EventHandler="Callback_AttachmentUploader_Mission_DailyRequestOnAbsence_onCallbackError" />
                                                                    </ClientEvents>
                                                                </componentart:callback>
                                                            </td>
                                                            <td style="width: 5%">
                                                                <componentart:toolbar id="TlbDeleteAttachment_Mission_DailyRequestOnAbsence" runat="server" cssclass="toolbar" defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked" defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover" defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText" imagesbaseurl="images/ToolBar/" itemspacing="1px" usefadeeffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_DailyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_DailyRequestOnAbsence_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_Mission_DailyRequestOnAbsence" TextImageSpacing="5" />
                                                                    </Items>
                                                                </componentart:toolbar>
                                                            </td>
                                                            <td id="tdAttachmentName_Mission_DailyRequestOnAbsence"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblDescription_Mission_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                                        meta:resourcekey="lblDescription_Mission_DailyRequestOnAbsence" Text=": توضیحات"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <textarea id="txtDescription_Mission_DailyRequestOnAbsence" class="TextBoxes" cols="20"
                                                        name="S1" rows="2" style="width: 98%"></textarea>
                                                </td>
                                            </tr>


                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 50%" valign="top">
                    <table style="width: 100%;" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_RegisteredRequests_DailyRequestOnAbsence" class="HeaderLabel" style="width: 50%">Registered Requests
                                        </td>
                                        <td id="loadingPanel_GridRegisteredRequests_DailyRequestOnAbsence" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td6" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRegisteredRequests_DailyRequestOnAbsence"
                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_DailyRequestOnAbsence"
                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_DailyRequestOnAbsence_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRegisteredRequests_DailyRequestOnAbsence"
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
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRegisteredRequests_DailyRequestOnAbsence"
                                    OnCallback="CallBack_GridRegisteredRequests_DailyRequestOnAbsence_onCallBack"
                                    Width="590">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRegisteredRequests_DailyRequestOnAbsence" runat="server"
                                            AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PageSize="13" RunningMode="Client" Width="590" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                    DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                    HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                            HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestType_GridRegisteredRequests_DailyRequestOnAbsence" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                            HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRegisteredRequests_DailyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                            HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRegisteredRequests_DailyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                            HeadingText="تاریخ ثبت درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDate_GridRegisteredRequests_DailyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="StatusTitle" DefaultSortDirection="Descending"
                                                            DataCellClientTemplateId="DataCellClientTemplate_clmnState_GridRegisteredRequests_DailyRequestOnAbsence"
                                                            HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridRegisteredRequests_DailyRequestOnAbsence" />
                                                        <ComponentArt:GridColumn DataField="Status" Visible="false" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_clmnState_GridRegisteredRequests_DailyRequestOnAbsence">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center">##GetRequestStateTitle_DailyRequestOnAbsence(DataItem.GetMember('Status').Value)##
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRegisteredRequests_DailyRequestOnAbsence_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RegisteredRequests" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRegisteredRequests_DailyRequestOnAbsence_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRegisteredRequests_DailyRequestOnAbsence_onCallbackError" />
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
                            <ComponentArt:ToolBar ID="TlbMissionLocationSearch_DailyRequestOnAbsence" runat="server"
                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbMissionLocationSearch_DailyRequestOnAbsence" runat="server" ClientSideCommand="tlbItemSave_TlbMissionLocationSearch_DailyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbMissionLocationSearch_DailyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbMissionLocationSearch_DailyRequestOnAbsence"
                                        runat="server" ClientSideCommand="tlbItemSearch_TlbMissionLocationSearch_DailyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbMissionLocationSearch_DailyRequestOnAbsence"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbMissionLocationSearch_DailyRequestOnAbsence" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbMissionLocationSearch_DailyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbMissionLocationSearch_DailyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchTermMissionLocation_DailyRequestOnAbsence" runat="server" Text=": جستجوي محل ماموریت"
                                meta:resourcekey="lblSearchTermMissionLocation_DailyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>

                                        <input id="txtSearchTermMissionLocation_DailyRequestOnAbsence" type="text" class="TextBoxes"
                                            style="width: 87%" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMissionLocationSearchResult_DailyRequestOnAbsence" runat="server" Text=": نتايج جستجوي محل ماموریت"
                                            meta:resourcekey="lblMissionLocationSearchResult_DailyRequestOnAbsence" CssClass="WhiteLabel"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbMissionLocationSearchResult_DailyRequestOnAbsence"
                                            OnCallback="CallBack_cmbMissionLocationSearchResult_DailyRequestOnAbsence_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbMissionLocationSearchResult_DailyRequestOnAbsence" runat="server"
                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                    ExpandDirection="Up" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150"
                                                    DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                    Style="width: 90%" TextBoxCssClass="comboTextBox">
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_MissionLocationSearchResult_DailyRequestOnAbsence" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbMissionLocationSearchResult_DailyRequestOnAbsence_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbMissionLocationSearchResult_DailyRequestOnAbsence_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbMissionLocationSearchResult_DailyRequestOnAbsence_onCallbackError" />
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
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 60%;" id="Container_PersonnelSelect_DailyRequestOnAbsence" class="BodyStyle">
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
                                        <asp:Label ID="lblPersonnel_DailyRequestOnAbsence" runat="server" CssClass="WhiteLabel"
                                            meta:resourcekey="lblSubstitute_DailyRequestOnAbsence" Text=": جانشین"></asp:Label>
                                    </td>
                                    <td id="Td4" runat="server" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_DailyRequestOnAbsence" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            Style="direction: ltr;" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_DailyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_DailyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_DailyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_DailyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_DailyRequestOnAbsence_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_DailyRequestOnAbsence"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 12%"></td>
                        <td >&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack ID="CallBack_cmbPersonnel_DailyRequestOnAbsence" runat="server"
                                OnCallback="CallBack_cmbPersonnel_DailyRequestOnAbsence_onCallBack" Height="26">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbPersonnel_DailyRequestOnAbsence" runat="server" AutoComplete="true"
                                        AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                        DropDownCssClass="comboDropDown" DropDownHeight="150" DropDownPageSize="5" DropDownWidth="300"
                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_DailyRequestOnAbsence"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                        SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                        <ClientTemplates>
                                            <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_DailyRequestOnAbsence">
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
                                                    <td id="clmnName_cmbPersonnel_DailyRequestOnAbsence" class="headingCell" style="width: 40%; text-align: center">Name And Family
                                                    </td>
                                                    <td id="clmnBarCode_cmbPersonnel_DailyRequestOnAbsence" class="headingCell" style="width: 30%; text-align: center">BarCode
                                                    </td>
                                                    <td id="clmnCardNum_cmbPersonnel_DailyRequestOnAbsence" class="headingCell" style="width: 30%; text-align: center">CardNum
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownHeader>
                                        <ClientEvents>
                                            <Change EventHandler="cmbPersonnel_DailyRequestOnAbsence_onChange" />
                                            <Expand EventHandler="cmbPersonnel_DailyRequestOnAbsence_onExpand" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField ID="ErrorHiddenField_Personnel_DailyRequestOnAbsence" runat="server" />
                                    <asp:HiddenField ID="hfPersonnelPageCount_DailyRequestOnAbsence" runat="server" />
                                    <asp:HiddenField ID="hfPersonnelCount_DailyRequestOnAbsence" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbPersonnel_DailyRequestOnAbsence_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbPersonnel_DailyRequestOnAbsence_onCallBackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbPersonnel_DailyRequestOnAbsence_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <input id="txtPersonnelSearch_DailyRequestOnAbsence" runat="server" class="TextBoxes"
                                onkeypress="txtPersonnelSearch_DailyRequestOnAbsence_onKeyPess(event);" style="width: 99%" type="text" />
                        </td>
                        <td>
                            <ComponentArt:ToolBar ID="TlbSearchPersonnel_DailyRequestOnAbsence" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_DailyRequestOnAbsence" runat="server"
                                        ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_DailyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_DailyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                       <%-- <td>
                            <ComponentArt:ToolBar ID="TlbAdvancedSearch_DailyRequestOnAbsence" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_DailyRequestOnAbsence"
                                        runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_DailyRequestOnAbsence_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_DailyRequestOnAbsence"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>--%>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogSubstitute_OnShow"/>
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
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_DailyRequestOnAbsence" meta:resourcekey="hfclmnName_cmbPersonnel_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_DailyRequestOnAbsence" meta:resourcekey="hfclmnBarCode_cmbPersonnel_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_DailyRequestOnAbsence" meta:resourcekey="hfclmnCardNum_cmbPersonnel_HourlyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogDailyRequestOnAbsence" meta:resourcekey="hfTitle_DialogDailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfheader_RegisteredRequests_DailyRequestOnAbsence"
            meta:resourcekey="hfheader_RegisteredRequests_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRegisteredRequests_DailyRequestOnAbsence"
            meta:resourcekey="hfloadingPanel_GridRegisteredRequests_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_DailyRequestOnAbsence" meta:resourcekey="hfDeleteMessage_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_DailyRequestOnAbsence" meta:resourcekey="hfCloseMessage_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_DailyRequestOnAbsence" meta:resourcekey="hfcmbAlarm_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfRequestStates_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfAdd_DailyRequestOnAbsence" meta:resourcekey="hfAdd_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfDelete_DailyRequestOnAbsence" meta:resourcekey="hfDelete_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfView_DailyRequestOnAbsence" meta:resourcekey="hfView_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfErrorType_DailyRequestOnAbsence" meta:resourcekey="hfErrorType_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfConnectionError_DailyRequestOnAbsence" meta:resourcekey="hfConnectionError_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfRequestMaxLength_DailyRequestOnAbsence" meta:resourcekey="hfRequestMaxLength_DailyRequestOnAbsence"/>
        <asp:HiddenField runat="server" ID="hfCloseWarningMessage_DailyRequestOnAbsence" meta:resourcekey="hfCloseWarningMessage_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfMissionType_DailyRequestOnAbsence" />
        <asp:HiddenField runat="server" ID="hfLeaveType_DailyRequestOnAbsence" />
    </form>
</body>
</html>
