<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="TrafficsTransfer" Codebehind="TrafficsTransfer.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="~/css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/persianDatePicker.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="TrafficsTransferForm" runat="server" meta:resourcekey="TrafficsTransferForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small" class="BodyStyle">
        <tr>
            <td colspan="2">
                <ComponentArt:ToolBar ID="TlbTrafficsTransfer" runat="server" CssClass="toolbar"
                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                    UseFadeEffect="false">
                    <Items>
                        <ComponentArt:ToolBarItem ID="tlbItemTrafficsTransfer_TlbTrafficsTransfer" runat="server" ClientSideCommand="tlbItemTrafficsTransfer_TlbTrafficsTransfer_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Transfer.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemTrafficsTransfer_TlbTrafficsTransfer"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbTrafficsTransfer" runat="server" DropDownImageHeight="16px"
                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbTrafficsTransfer" TextImageSpacing="5"
                            ClientSideCommand="tlbItemHelp_TlbTrafficsTransfer_onClick();" />
                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbTrafficsTransfer" runat="server"
                            ClientSideCommand="tlbItemFormReconstruction_TlbTrafficsTransfer_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbTrafficsTransfer"
                            TextImageSpacing="5" />
                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbTrafficsTransfer" runat="server" ClientSideCommand="tlbItemExit_TlbTrafficsTransfer_onClick();"
                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbTrafficsTransfer"
                            TextImageSpacing="5" />
                    </Items>
                </ComponentArt:ToolBar>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMachines_TrafficsTransfer" CssClass="WhiteLabel" runat="server"
                    Text=": دستگاه ها" meta:resourcekey="lblMachines_TrafficsTransfer"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <ComponentArt:CallBack ID="CallBack_cmbMachines_TrafficsTransfer" runat="server"
                    Height="26" OnCallback="CallBack_cmbMachines_TrafficsTransfer_onCallBack">
                    <Content>
                        <ComponentArt:ComboBox ID="cmbMachines_TrafficsTransfer" runat="server" AutoComplete="true"
                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                            TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                            <ClientEvents>
                                <Expand EventHandler="cmbMachines_TrafficsTransfer_onExpand" />
                                <Collapse EventHandler="cmbMachines_TrafficsTransfer_onCollapse" />
                            </ClientEvents>
                        </ComponentArt:ComboBox>
                        <asp:HiddenField ID="ErrorHiddenField_Machines_TrafficsTransfer" runat="server" />
                    </Content>
                    <ClientEvents>
                        <BeforeCallback EventHandler="CallBack_cmbMachines_TrafficsTransfer_onBeforeCallback" />
                        <CallbackComplete EventHandler="CallBack_cmbMachines_TrafficsTransfer_onCallbackComplete" />
                        <CallbackError EventHandler="CallBack_cmbMachines_TrafficsTransfer_onCallbackError" />
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:Label ID="lblFromHour_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                    Text=": از ساعت" meta:resourcekey="lblFromHour_TrafficsTransfer"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFromDate_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                    Text=": از تاریخ" meta:resourcekey="lblFromDate_TrafficsTransfer"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Container_FromDateCalendars_TrafficsTransfer">
                <MKB:TimeSelector ID="TimeSelector_FromHour_TrafficsTransfer" runat="server" DisplaySeconds="true"
                    MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                </MKB:TimeSelector>
            </td>
            <td>
                <table id="Container_pdpFromDate_TrafficsTransfer" runat="server" style="width: 100%"
                    visible="false">
                    <tr>
                        <td>
                            <pcal:PersianDatePickup ID="pdpFromDate_TrafficsTransfer" runat="server" CssClass="PersianDatePicker"
                                ReadOnly="true"></pcal:PersianDatePickup>
                        </td>
                    </tr>
                </table>
                <table id="Container_gdpFromDate_TrafficsTransfer" runat="server" style="width: 100%"
                    visible="false">
                    <tr>
                        <td>
                            <table id="Container_gCalFromDate_TrafficsTransfer" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td onmouseup="btn_gdpFromDate_TrafficsTransfer_OnMouseUp(event)">
                                        <ComponentArt:Calendar ID="gdpFromDate_TrafficsTransfer" runat="server" ControlType="Picker"
                                            MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                            SelectedDate="2008-1-1">
                                            <ClientEvents>
                                                <SelectionChanged EventHandler="gdpFromDate_TrafficsTransfer_OnDateChange" />
                                            </ClientEvents>
                                        </ComponentArt:Calendar>
                                    </td>
                                    <td style="font-size: 10px;">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <img id="btn_gdpFromDate_TrafficsTransfer" alt="" class="calendar_button" onclick="btn_gdpFromDate_TrafficsTransfer_OnClick(event)"
                                            onmouseup="btn_gdpFromDate_TrafficsTransfer_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                    </td>
                                </tr>
                            </table>
                            <ComponentArt:Calendar ID="gCalFromDate_TrafficsTransfer" runat="server" AllowMonthSelection="false"
                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_TrafficsTransfer"
                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                <ClientEvents>
                                    <SelectionChanged EventHandler="gCalFromDate_TrafficsTransfer_OnChange" />
                                    <Load EventHandler="gCalFromDate_TrafficsTransfer_onLoad" />
                                </ClientEvents>
                            </ComponentArt:Calendar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblToHour_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text=": تا ساعت"
                    meta:resourcekey="lblToHour_TrafficsTransfer"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblToDate_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text=": تا تاریخ"
                    meta:resourcekey="lblToDate_TrafficsTransfer"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <MKB:TimeSelector ID="TimeSelector_ToHour_TrafficsTransfer" runat="server" DisplaySeconds="true"
                    MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;" Visible="true">
                </MKB:TimeSelector>
            </td>
            <td>
                <table style="width: 100%; font-family: Arial; font-size: small">
                    <tr>
                        <td>
                            <table id="Container_pdpToDate_TrafficsTransfer" runat="server" style="width: 100%"
                                visible="false">
                                <tr>
                                    <td>
                                        <pcal:PersianDatePickup ID="pdpToDate_TrafficsTransfer" runat="server" CssClass="PersianDatePicker"
                                            ReadOnly="true"></pcal:PersianDatePickup>
                                    </td>
                                </tr>
                            </table>
                            <table id="Container_gdpToDate_TrafficsTransfer" runat="server" style="width: 100%"
                                visible="false">
                                <tr>
                                    <td>
                                        <table id="Container_gCalToDate_TrafficsTransfer" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td onmouseup="btn_gdpToDate_TrafficsTransfer_OnMouseUp(event)">
                                                    <ComponentArt:Calendar ID="gdpToDate_TrafficsTransfer" runat="server" ControlType="Picker"
                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                        SelectedDate="2008-1-1">
                                                        <ClientEvents>
                                                            <SelectionChanged EventHandler="gdpToDate_TrafficsTransfer_OnDateChange" />
                                                        </ClientEvents>
                                                    </ComponentArt:Calendar>
                                                </td>
                                                <td style="font-size: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <img id="btn_gdpToDate_TrafficsTransfer" alt="" class="calendar_button" onclick="btn_gdpToDate_TrafficsTransfer_OnClick(event)"
                                                        onmouseup="btn_gdpToDate_TrafficsTransfer_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                </td>
                                            </tr>
                                        </table>
                                        <ComponentArt:Calendar ID="gCalToDate_TrafficsTransfer" runat="server" AllowMonthSelection="false"
                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_TrafficsTransfer"
                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                            <ClientEvents>
                                                <SelectionChanged EventHandler="gCalToDate_TrafficsTransfer_OnChange" />
                                                <Load EventHandler="gCalToDate_TrafficsTransfer_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:Calendar>
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
                <table style="width: 100%;" class="BoxStyle">
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbNormalTransfer_TrafficsTransfer" type="radio" onclick="rdbNormalTransfer_TrafficsTransfer_onClick();"
                                            name="TrafficTransferMode" checked="checked"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNormalTransfer_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                            Text="عادی" meta:resourcekey="lblNormalTransfer_TrafficsTransfer"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbRecordeBaseTransfer_TrafficsTransfer" type="radio" onclick="rdbRecordeBaseTransfer_TrafficsTransfer_onClick();"
                                            name="TrafficTransferMode" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblRecordBaseTransfer_TrafficsTransfer" runat="server" Text="براساس سطر"
                                            CssClass="WhiteLabel" meta:resourcekey="lblRecordeBaseTransfer_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 20%" align="center">
                                        &nbsp;
                                    </td>
                                    <td style="width: 12%" align="center">
                                        &nbsp;
                                    </td>
                                    <td style="width: 20%" align="center">
                                        &nbsp;
                                    </td>
                                    <td style="width: 12%" align="center">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                        <input id="chbIntegratedConditionsOverRecordBase_TrafficsTransfer" type="checkbox"
                                            disabled="disabled" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblIntegratedConditionsOverRecordBase_TrafficsTransfer" runat="server"
                                            Text="ترکیب با شروط بالا" CssClass="WhiteLabel" meta:resourcekey="lblIntegratedConditionsOverRecordBase_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 20%" align="center">
                                        <asp:Label ID="lblFromRecord_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                            Text=": از سطر" meta:resourcekey="lblFromRecord_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 12%" align="center">
                                        <input id="txtFromRecord_TrafficsTransfer" class="TextBoxes" type="text" style="width: 90%;
                                            text-align: center" onchange="txtRecordControl_TrafficsTransfer_onChange('From');"
                                            disabled="disabled"   />
                                    </td>
                                    <td style="width: 20%" align="center">
                                        <asp:Label ID="lblToRecord_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                            Text=": تا سطر" meta:resourcekey="lblToRecord_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 12%" align="center">
                                        <input id="txtToRecord_TrafficsTransfer" class="TextBoxes" type="text" style="width: 90%;
                                            text-align: center" onchange="txtRecordControl_TrafficsTransfer_onChange('To');"
                                            disabled="disabled"   />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="rdbIdentifierBaseTransfer_TrafficsTransfer" type="radio" onclick="rdbIdentifierBaseTransfer_TrafficsTransfer_onClick();"
                                            name="TrafficTransferMode" value="1" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblIdentifierBaseTransfer_TrafficsTransfer" runat="server" Text="براساس شناسه"
                                            CssClass="WhiteLabel" meta:resourcekey="lblIdentifierBaseTransfer_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 20%" align="center">
                                        &nbsp;
                                    </td>
                                    <td style="width: 12%" align="center">
                                        &nbsp;
                                    </td>
                                    <td style="width: 20%" align="center">
                                        &nbsp;
                                    </td>
                                    <td style="width: 12%" align="center">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                        <input id="chbIntegratedConditionsOverIdentifierBase_TrafficsTransfer" type="checkbox"
                                            disabled="disabled" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblIntegratedConditionsOverIdentifierBase_TrafficsTransfer" runat="server"
                                            Text="ترکیب با شروط بالا" CssClass="WhiteLabel" meta:resourcekey="lblIntegratedConditionsOverIdentifierBase_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 20%" align="center">
                                        <asp:Label ID="lblFromIdentifier_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                            Text=": از شناسه" meta:resourcekey="lblFromIdentifier_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 12%" align="center">
                                        <input id="txtFromIdentifier_TrafficsTransfer" class="TextBoxes" type="text" style="width: 90%;
                                            text-align: center" onchange="txtIdentifierControl_TrafficsTransfer_onChange('From');"
                                            disabled="disabled"   />
                                    </td>
                                    <td style="width: 20%" align="center">
                                        <asp:Label ID="lblToIdentifier_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                            Text=": تا شناسه" meta:resourcekey="lblToIdentifier_TrafficsTransfer"></asp:Label>
                                    </td>
                                    <td style="width: 12%" align="center">
                                        <input id="txtToIdentifier_TrafficsTransfer" class="TextBoxes" type="text" style="width: 90%;
                                            text-align: center" onchange="txtIdentifierControl_TrafficsTransfer_onChange('To');"
                                            disabled="disabled"   />
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
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 9%" align="center">
                            <input id="txtDay_TrafficsTransfer" class="TextBoxes" type="text" style="width: 95%;
                                text-align: center" onchange="TransferFeaturesControl_TrafficsTransfer_onChange('Day')"
                                  />
                        </td>
                        <td style="width: 15%" align="center">
                            <asp:Label ID="lblDayAnd_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text="روز و"
                                meta:resourcekey="lblDayAnd_TrafficsTransfer"></asp:Label>
                        </td>
                        <td style="width: 9%" align="center">
                            <input id="txtHour_TrafficsTransfer" class="TextBoxes" type="text" style="width: 95%;
                                text-align: center" onchange="TransferFeaturesControl_TrafficsTransfer_onChange('Hour')"
                                  />
                        </td>
                        <td style="width: 15%" align="center">
                            <asp:Label ID="lblHourAnd_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                Text="ساعت و" meta:resourcekey="lblHourAnd_TrafficsTransfer"></asp:Label>
                        </td>
                        <td style="width: 9%" align="center">
                            <input id="txtMinute_TrafficsTransfer" class="TextBoxes" type="text" style="width: 95%;
                                text-align: center" onchange="TransferFeaturesControl_TrafficsTransfer_onChange('Minute')"
                                  />
                        </td>
                        <td style="width: 15%" align="center">
                            <asp:Label ID="lblMinuteTo_TrafficsTransfer" runat="server" CssClass="WhiteLabel"
                                Text="دقیقه به" meta:resourcekey="lblMinuteTo_TrafficsTransfer"></asp:Label>
                        </td>
                        <td style="width: 18%">
                            <ComponentArt:CallBack ID="CallBack_cmbTrafficTransferType_TrafficsTransfer" runat="server"
                                Height="26" OnCallback="CallBack_cmbTrafficTransferType_TrafficsTransfer_onCallBack">
                                <Content>
                                    <ComponentArt:ComboBox ID="cmbTrafficTransferType_TrafficsTransfer" runat="server"
                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                        DropDownHeight="50" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                        ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                        Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                        <ClientEvents>
                                            <Expand EventHandler="cmbTrafficTransferType_TrafficsTransfer_onExpand" />
                                            <Collapse EventHandler="cmbTrafficTransferType_TrafficsTransfer_onCollapse" />
                                        </ClientEvents>
                                    </ComponentArt:ComboBox>
                                    <asp:HiddenField ID="ErrorHiddenField_TrafficTransferType_TrafficsTransfer" runat="server" />
                                </Content>
                                <ClientEvents>
                                    <BeforeCallback EventHandler="CallBack_cmbTrafficTransferType_TrafficsTransfer_onBeforeCallback" />
                                    <CallbackComplete EventHandler="CallBack_cmbTrafficTransferType_TrafficsTransfer_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_cmbTrafficTransferType_TrafficsTransfer_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <ComponentArt:CallBack ID="CallBack_Container_TrafficsTransferProgressFeatures" runat="server" OnCallback="CallBack_Container_TrafficsTransferProgressFeatures_onCallBack">
                    <Content>
                        <table id="Container_TrafficsTransferProgressFeatures" runat="server" align="center" style="width: 99%" visible="false">
                            <tr>
                                <td>
                                    <div id="Progressbar_TrafficsTransfer" runat="server" style="font-size: 8pt; padding: 2px; border: solid black 1px; width: 100%">
                                        <table id="tblProgressbar_TrafficsTransfer" runat="server" style="width: 100%">
                                            <tr>
                                                <td id="p1_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p2_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p3_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p4_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p5_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p6_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p7_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p8_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p9_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p10_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p11_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p12_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p13_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p14_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p15_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p16_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p17_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p18_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p19_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p20_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p21_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p22_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p23_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p24_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p25_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p26_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p27_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p28_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p29_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p30_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p31_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p32_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p33_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p34_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p35_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p36_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p37_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p38_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p39_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p40_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p41_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p42_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p43_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p44_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p45_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p46_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p47_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p48_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p49_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p50_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p51_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p52_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p53_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p54_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p55_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p56_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p57_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p58_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p59_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p60_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p61_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p62_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p63_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p64_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p65_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p66_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p67_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p68_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p69_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p70_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p71_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p72_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p73_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p74_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p75_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p76_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p77_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p78_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p79_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p80_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p81_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p82_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p83_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p84_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p85_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p86_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p87_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p88_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p89_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p90_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p91_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p92_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p93_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p94_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p95_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p96_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p97_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p98_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p99_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                                <td id="p100_Progressbar_TrafficsTransfer" runat="server" style="width: 1%">&nbsp; </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td id="tdCompletedTrafficsTransferCount_TrafficsTransfer" style="width: 25%">
                                                <asp:Label ID="lblCompletedTrafficsTransferCount_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text=""></asp:Label>
                                            </td>
                                            <td id="tdErrorOccuredTrafficsTransferCount_TrafficsTransfer" style="width: 25%">
                                                <asp:Label ID="lblErrorOccuredTrafficsTransferCount_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text=""></asp:Label>
                                            </td>
                                            <td id="tdAllTrafficsCount_TrafficsTransfer" style="width: 25%">
                                                <asp:Label ID="lblAllTrafficsCount_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text=""></asp:Label>
                                            </td>
                                            <td id="tdProgressPercentage_TrafficsTransfer" style="width: 25%">
                                                <asp:Label ID="lblProgressPercentage_TrafficsTransfer" runat="server" CssClass="WhiteLabel" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfTrafficsTransferProgress_TrafficsTransfer" runat="server" />
                        <asp:HiddenField ID="ErrorHiddenField_TrafficsTransfer" runat="server" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="CallBack_Container_TrafficsTransferProgressFeatures_onCallbackComplete"/>
                        <CallbackError EventHandler="CallBack_Container_TrafficsTransferProgressFeatures_onCallbackError"/>
                    </ClientEvents>
                </ComponentArt:CallBack>
            </td>
        </tr>
    </table>
    <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
        Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
        runat="server" Width="280px">
        <Content>
            <table id="tblConfirm_DialogConfirm" style="width: 100%;" class="ConfirmStyle">
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
                        <img id="Img1" runat="server" alt="" src="~/DesktopModules/Atlas/Images/Dialog/Waiting.gif"  />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>    
    <asp:HiddenField runat="server" ID="hfTitle_DialogTrafficsTransfer" meta:resourcekey="hfTitle_DialogTrafficsTransfer" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_TrafficsTransfer" meta:resourcekey="hfCloseMessage_TrafficsTransfer" />
    <asp:HiddenField runat="server" ID="hfErrorType_TrafficsTransfer" meta:resourcekey="hfErrorType_TrafficsTransfer" />
    <asp:HiddenField runat="server" ID="hfConnectionError_TrafficsTransfer" meta:resourcekey="hfConnectionError_TrafficsTransfer" />
    <asp:HiddenField runat="server" ID="hfRetSuccessType_TrafficsTransfer" meta:resourcekey="hfRetSuccessType_TrafficsTransfer"/>
    <asp:HiddenField runat="server" ID="hfTrafficsTransferCompletedSuccessfully_TrafficsTransfer" meta:resourcekey="hfTrafficsTransferCompletedSuccessfully_TrafficsTransfer"/>
    <asp:HiddenField runat="server" ID="hfCurrentDate_TrafficsTransfer" />
    </form>
</body>
</html>
