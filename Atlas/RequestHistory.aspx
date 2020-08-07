<%@ Page Language="C#" AutoEventWireup="true" Inherits="RequestHistory" Codebehind="RequestHistory.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="FUA" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RequestHistoryForm" runat="server" meta:resourcekey="RequestHistoryForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbRequest_RequestHistory" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRequest_RequestHistory" runat="server" ClientSideCommand="tlbItemSave_TlbRequest_RequestHistory_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbRequest_RequestHistory"
                                TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRequest_RequestHistory" runat="server"
                                ClientSideCommand="tlbItemFormReconstruction_TlbRequest_RequestHistory_onClick();" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" Visible="false"
                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRequest_RequestHistory" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRequest_RequestHistory" runat="server" ClientSideCommand="tlbItemExit_TlbRequest_RequestHistory_onClick();"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbRequest_RequestHistory"
                                TextImageSpacing="5" />
                        </Items>
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" runat="server" id="tblEditRequest_RequestHistory" class="BoxStyle">
                        <tr>
                            <td>
                                <table id="tblRequestHourly_RequestHistory" runat="server" style="width: 60%">
                                    <tr>
                                        <td style="width: 50%" valign="top">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%; border: 1px outset black; height: 45px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblFromHour_tbHourly_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                        meta:resourcekey="lblFromHour_tbHourly_RequestHistory" Text=": از ساعت"></asp:Label>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" meta:resourcekey="AlignObj" style="width: 50%">
                                                                    <MKB:TimeSelector ID="TimeSelector_FromHour_tbHourly_RequestHistory" runat="server"
                                                                        DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                        Visible="true">
                                                                    </MKB:TimeSelector>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%; border: 1px outset black">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblToHour_tbHourly_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                        meta:resourcekey="lblToHour_tbHourly_RequestHistory" Text=": تا ساعت"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td runat="server" meta:resourcekey="AlignObj" style="width: 50%">
                                                                    <MKB:TimeSelector ID="TimeSelector_ToHour_tbHourly_RequestHistory" runat="server"
                                                                        DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                        Visible="true">
                                                                    </MKB:TimeSelector>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table runat="server" id="tblToHourInNextDay_tbHourly_RequestHistory" style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 5%">
                                                                    <input id="chbToHourInNextDay_tbHourly_RequestHistory" type="checkbox" onclick="chbToHourInNextDay_tbHourly_RequestHistory_onClick();" /></td>
                                                                <td>
                                                                    <asp:Label ID="lblToHourInNextDay_tbHourly_RequestHistory" runat="server" Text="زمان انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_tbHourly_RequestHistory"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table runat="server" id="tblFromAndToHourInNextDay_tbHourly_RequestHistory" style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 5%">
                                                                    <input id="chbFromAndToHourInNextDay_tbHourly_RequestHistory" type="checkbox" onclick="chbFromAndToHourInNextDay_tbHourly_RequestHistory_onclick();" /></td>
                                                                <td>
                                                                    <asp:Label ID="lblFromAndToHourInNextDay_tbHourly_RequestHistory" runat="server" Text="زمان ابتدا و انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblFromAndToHourInNextDay_tbHourly_RequestHistory"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblRequestDate_tbHourly_RequestHistory"
                                                            runat="server" CssClass="WhiteLabel" meta:resourcekey="lblRequestDate_tbHourly_RequestHistory"
                                                            Text=": تاریخ درخواست"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Tr2" runat="server" meta:resourcekey="InverseAlignObj">
                                                    <td id="Container_RequestDateCalendars_tbHourly_RequestHistory">
                                                        <table runat="server" id="Container_pdpRequestDate_tbHourly_RequestHistory" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpRequestDate_tbHourly_RequestHistory" runat="server"
                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table runat="server" id="Container_gdpRequestDate_tbHourly_RequestHistory" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table id="Container_gCalRequestDate_tbHourly_RequestHistory" border="0" cellpadding="0"
                                                                        cellspacing="0">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpRequestDate_tbHourly_RequestHistory_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpRequestDate_tbHourly_RequestHistory" runat="server"
                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                    PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpRequestDate_tbHourly_RequestHistory_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpRequestDate_tbHourly_RequestHistory" alt="" class="calendar_button"
                                                                                    onclick="btn_gdpRequestDate_tbHourly_RequestHistory_OnClick(event)" onmouseup="btn_gdpRequestDate_tbHourly_RequestHistory_OnMouseUp(event)"
                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalRequestDate_tbHourly_RequestHistory" runat="server"
                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                        ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                        PopUpExpandControlId="btn_gdpRequestDate_tbHourly_RequestHistory" PrevImageUrl="cal_prevMonth.gif"
                                                                        SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                        SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalRequestDate_tbHourly_RequestHistory_OnChange" />
                                                                            <Load EventHandler="gCalRequestDate_tbHourly_RequestHistory_OnLoad" />
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
                                </table>
                                <table id="tblRequestDaily_RequestHistory" runat="server">
                                    <tr id="tr1" runat="server" meta:resourcekey="AlignObj">
                                        <td style="width: 50%">
                                            <asp:Label ID="lblFromDate_tbDaily_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                meta:resourcekey="lblFromDate_tbDaily_RequestHistory" Text=": از تاریخ"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblToDate_tbDaily_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                meta:resourcekey="lblToDate_tbDaily_RequestHistory" Text=": تا تاریخ"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr17" runat="server" meta:resourcekey="AlignObj">
                                        <td id="Container_FromDateCalendars_tbDaily_RequestHistory">
                                            <table runat="server" id="Container_pdpFromDate_tbDaily_RequestHistory" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpFromDate_tbDaily_RequestHistory" runat="server" CssClass="PersianDatePicker"
                                                            Style="margin: 0 40 0 0" ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpFromDate_tbDaily_RequestHistory" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalFromDate_tbDaily_RequestHistory" border="0" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpFromDate_tbDaily_RequestHistory_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpFromDate_tbDaily_RequestHistory" runat="server" ControlType="Picker"
                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                        SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpFromDate_tbDaily_RequestHistory_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpFromDate_tbDaily_RequestHistory" alt="" class="calendar_button"
                                                                        onclick="btn_gdpFromDate_tbDaily_RequestHistory_OnClick(event)" onmouseup="btn_gdpFromDate_tbDaily_RequestHistory_OnMouseUp(event)"
                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalFromDate_tbDaily_RequestHistory" runat="server" AllowMonthSelection="false"
                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_tbDaily_RequestHistory"
                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalFromDate_tbDaily_RequestHistory_OnChange" />
                                                                <Load EventHandler="gCalFromDate_tbDaily_RequestHistory_OnLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="Container_ToDateCalendars_tbDaily_RequestHistory">
                                            <table runat="server" id="Container_pdpToDate_tbDaily_RequestHistory" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpToDate_tbDaily_RequestHistory" runat="server" CssClass="PersianDatePicker"
                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="Container_gdpToDate_tbDaily_RequestHistory" visible="false"
                                                style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalToDate_tbDaily_RequestHistory" border="0" cellpadding="0"
                                                            cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpToDate_tbDaily_RequestHistory_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpToDate_tbDaily_RequestHistory" runat="server" ControlType="Picker"
                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                        SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpToDate_tbDaily_RequestHistory_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp;
                                                                </td>
                                                                <td>
                                                                    <img id="btn_gdpToDate_tbDaily_RequestHistory" alt="" class="calendar_button" onclick="btn_gdpToDate_tbDaily_RequestHistory_OnClick(event)"
                                                                        onmouseup="btn_gdpToDate_tbDaily_RequestHistory_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalToDate_tbDaily_RequestHistory" runat="server" AllowMonthSelection="false"
                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                            MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_tbDaily_RequestHistory"
                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalToDate_tbDaily_RequestHistory_OnChange" />
                                                                <Load EventHandler="gCalToDate_tbDaily_RequestHistory_OnLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblRequestOverTime_RequestHistory" runat="server">
                                    <tr>
                                        <td valign="top" style="width: 30%">
                                            <div id="Container_NormalTimeParts_tbOverTime_RequestHistory">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFromHour_tbOverTime_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblFromHour_tbOverTime_RequestHistory" Text=": از ساعت"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" meta:resourcekey="InverseAlignObj">
                                                            <MKB:TimeSelector ID="TimeSelector_FromHour_tbOverTime_RequestHistory" runat="server"
                                                                DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                Visible="true">
                                                            </MKB:TimeSelector>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblToHour_tbOverTime_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblToHour_tbOverTime_RequestHistory" Text=": تا ساعت"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" meta:resourcekey="InverseAlignObj">
                                                            <MKB:TimeSelector ID="TimeSelector_ToHour_tbOverTime_RequestHistory" runat="server"
                                                                DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                Visible="true">
                                                            </MKB:TimeSelector>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDuration_tbOverTime_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblDuration_tbOverTime_RequestHistory" Text=": مدت زمان"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" meta:resourcekey="InverseAlignObj">
                                                            <MKB:TimeSelector ID="TimeSelector_Duration_tbOverTime_RequestHistory" runat="server"
                                                                DisplaySeconds="true" MinuteIncrement="1" SelectedTimeFormat="TwentyFour" Style="direction: ltr;"
                                                                Visible="true">
                                                            </MKB:TimeSelector>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" meta:resourcekey="InverseAlignObj">
                                                            <table runat="server" id="tblToHourInNextDay_tbOvertime_RequestHistory" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="chbToHourInNextDay_tbOvertime_RequestHistory" type="checkbox" onclick="chbToHourInNextDay_tbOvertime_RequestHistory_onClick();" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblToHourInNextDay_tbOvertime_RequestHistory" runat="server" Text="زمان انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblToHourInNextDay_tbOvertime_RequestHistory"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" meta:resourcekey="InverseAlignObj">
                                                            <table runat="server" id="tblFromAndToHourInNextDay_tbOvertime_RequestHistory" style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 5%">
                                                                        <input id="chbFromAndToHourInNextDay_tbOvertime_RequestHistory" type="checkbox" onclick="chbFromAndToHourInNextDay_tbOvertime_RequestHistory_onclick();" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblFromAndToHourInNextDay_tbOvertime_RequestHistory" runat="server" Text="زمان ابتدا و انتها در روز بعد" CssClass="WhiteLabel" meta:resourcekey="lblFromAndToHourInNextDay_tbOvertime_RequestHistory"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td style="width: 35%" valign="top">
                                            <div id="Container_FromDateCalendars_tbOverTime_RequestHistory">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFromDate_tbOverTime_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblFromDate_tbOverTime_RequestHistory" Text=": از تاریخ"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="Container_pdpFromDate_tbOverTime_RequestHistory" runat="server" style="width: 100%"
                                                                visible="false">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpFromDate_tbOverTime_RequestHistory" runat="server"
                                                                            CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="Container_gdpFromDate_tbOverTime_RequestHistory" runat="server" style="width: 100%"
                                                                visible="false">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalFromDate_tbOverTime_RequestHistory" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpFromDate_tbOverTime_RequestHistory_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpFromDate_tbOverTime_RequestHistory" runat="server"
                                                                                        ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                        PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpFromDate_tbOverTime_RequestHistory_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpFromDate_tbOverTime_RequestHistory" alt="" class="calendar_button"
                                                                                        onclick="btn_gdpFromDate_tbOverTime_RequestHistory_OnClick(event)" onmouseup="btn_gdpFromDate_tbOverTime_RequestHistory_OnMouseUp(event)"
                                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalFromDate_tbOverTime_RequestHistory" runat="server"
                                                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                            ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                            PopUpExpandControlId="btn_gdpFromDate_tbOverTime_RequestHistory" PrevImageUrl="cal_prevMonth.gif"
                                                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                            SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalFromDate_tbOverTime_RequestHistory_OnChange" />
                                                                                <Load EventHandler="gCalFromDate_tbOverTime_RequestHistory_OnLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td style="width: 35%" valign="top">
                                            <div id="Container_ToDateCalendars_tbOverTime_RequestHistory">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblToDate_tbOverTime_RequestHistory" runat="server" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblToDate_tbOverTime_RequestHistory" Text=": تا تاریخ"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table id="Container_pdpToDate_tbOverTime_RequestHistory" runat="server" style="width: 100%"
                                                                visible="false">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpToDate_tbOverTime_RequestHistory" runat="server"
                                                                            CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table id="Container_gdpToDate_tbOverTime_RequestHistory" runat="server" style="width: 100%"
                                                                visible="false">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalToDate_tbOverTime_RequestHistory" border="0" cellpadding="0"
                                                                            cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpToDate_tbOverTime_RequestHistory_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpToDate_tbOverTime_RequestHistory" runat="server" ControlType="Picker"
                                                                                        MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpToDate_tbOverTime_RequestHistory_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpToDate_tbOverTime_RequestHistory" alt="" class="calendar_button"
                                                                                        onclick="btn_gdpToDate_tbOverTime_RequestHistory_OnClick(event)" onmouseup="btn_gdpToDate_tbOverTime_RequestHistory_OnMouseUp(event)"
                                                                                        src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalToDate_tbOverTime_RequestHistory" runat="server"
                                                                            AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                            CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                            DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                            ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                            NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                            PopUpExpandControlId="btn_gdpToDate_tbOverTime_RequestHistory" PrevImageUrl="cal_prevMonth.gif"
                                                                            SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                            SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalToDate_tbOverTime_RequestHistory_OnChange" />
                                                                                <Load EventHandler="gCalToDate_tbOverTime_RequestHistory_OnLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="BoxStyle" style="width: 100%; border-top: gray 1px double; border-right: gray 1px double; font-size: small; border-left: gray 1px double; border-bottom: gray 1px double;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblAttachment_RequestHistory" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAttachment_RequestHistory" Text="ضمیمه :"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 56%">
                                                                            <ComponentArt:CallBack ID="Callback_AttachmentUploader_RequestHistory" runat="server" OnCallback="Callback_AttachmentUploader_RequestHistory_onCallBack">
                                                                                <Content>
                                                                                    <cc1:FileUploaderAJAX ID="AttachmentUploader_RequestHistory" runat="server" MaxFiles="3" meta:resourcekey="AttachmentUploader_RequestHistory" showDeletedFilesOnPostBack="false" text_Add="" text_Delete="" text_X="" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <CallbackComplete EventHandler="Callback_AttachmentUploader_RequestHistory_onCallBackComplete" />
                                                                                    <CallbackError EventHandler="Callback_AttachmentUploader_RequestHistory_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbDeleteAttachment_RequestHistory" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemDeleteAttachment_TlbDeleteAttachment_RequestHistory" runat="server" ClientSideCommand="tlbItemDeleteAttachment_TlbDeleteAttachment_RequestHistory_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDeleteAttachment_TlbDeleteAttachment_RequestHistory" TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                        <td id="tdAttachmentName_RequestHistory"></td>
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
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td id="header_RequestHistory_RequestHistory" class="HeaderLabel" style="width: 65%">Request History
                            </td>
                            <td id="loadingPanel_GridRequestHistory_RequestHistory" class="HeaderLabel"
                                style="width: 30%"></td>
                            <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                <ComponentArt:ToolBar ID="TlbRefresh_GridRequestHistory_RequestHistory"
                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRequestHistory_RequestHistory"
                                            runat="server" ClientSideCommand="Refresh_GridRequestHistory_RequestHistory();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRequestHistory_RequestHistory"
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
                    <table id="Container_GridRequestHistory_RequestHistory" style="width: 100%">
                        <tr>
                            <td>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridRequestHistory_RequestHistory"
                                    OnCallback="CallBack_GridRequestHistory_RequestHistory_onCallBack">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridRequestHistory_RequestHistory" runat="server"
                                            AllowHorizontalScrolling="false" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                            FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                            PagePaddingEnabled="true" PageSize="1" RunningMode="Client" AllowMultipleSelect="false"
                                            AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                            ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                    SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                    SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="PrecardName" DefaultSortDirection="Descending"
                                                            HeadingText="نوع درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestTopic_GridRequestHistory_RequestHistory"
                                                            Width="150" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                            HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridRequestHistory_RequestHistory"
                                                            Width="100" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                            HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridRequestHistory_RequestHistory"
                                                            Width="100" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                            HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromTime_GridRequestHistory_RequestHistory"
                                                            Width="100" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                            HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToTime_GridRequestHistory_RequestHistory"
                                                            Width="100" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="TheDuration" DefaultSortDirection="Descending"
                                                            HeadingText="مقدار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDuration_GridRequestHistory_RequestHistory"
                                                            Width="100" TextWrap="true" />
                                                          <ComponentArt:GridColumn Align="Center" DataField="AttachmentFile" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnAttachmentFile_RequestHistory"
                                                                HeadingText="آپلود فایل" HeadingTextCssClass="HeadingText" Width="60" TextWrap="true" />
                                                    </Columns>
                                                    
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestStatus_GridEndorsementFlowState_EndorsementFlowState">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td align="center" style="font-family: Verdana; font-size: 10px;" title="##SetCellTitle_GridEndorsementFlowState_EndorsementFlowState(DataItem.GetMember('RequestStatus').Value)##">
                                                                <img src="##SetClmnImage_GridEndorsementFlowState_EndorsementFlowState(DataItem.GetMember('RequestStatus').Value)##"
                                                                    alt="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnAttachmentFile_RequestHistory">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px; cursor: pointer"
                                                                    ondblclick="ShowAttachmentFile_GridRequestHistory_RequestHistory('AttachmentFile');">##SetAttachmentFileImage_GridRequestHistroy_RequestHistory(DataItem.GetMember('AttachmentFile').Value)##
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                            <ClientEvents>
                                                <Load EventHandler="GridRequestHistory_RequestHistory_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_History_RequestHistory" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridRequestHistory_RequestHistory_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridRequestHistory_RequestHistory_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
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
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="320px">
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
            <ClientEvents>
                <OnShow EventHandler="DialogConfirm_OnShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogRequestHistory" meta:resourcekey="hfTitle_DialogRequestHistory" />
        <asp:HiddenField runat="server" ID="hfheader_RequestHistory_RequestHistory"
            meta:resourcekey="hfheader_RequestHistory_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRequestHistory_RequestHistory"
            meta:resourcekey="hfloadingPanel_GridRequestHistory_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfRequestHistory_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfErrorType_RequestHistory" meta:resourcekey="hfErrorType_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RequestHistory" meta:resourcekey="hfConnectionError_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_RequestHistory" />
         <asp:HiddenField runat="server" ID="hfOldAttachmentFile_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RequestHistory" meta:resourcekey="hfCloseMessage_RequestHistory" />
        <asp:HiddenField runat="server" ID="hfRequestMaxLength_RequestHistory" meta:resourcekey="hfRequestMaxLength_RequestHistory"/>
    </form>
</body>
</html>
