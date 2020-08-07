<%@ Page Language="C#" AutoEventWireup="true" Inherits="Date_Time_ReportParameter" Codebehind="Date_Time_ReportParameter.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="Date_Time_ReportParameterForm" runat="server" meta:resourcekey="Date_Time_ReportParameterForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div style="height: 290px;" class="BoxStyle">
            <table id="tblMain_Date_Time_ReportParameter" style="width: 100%; font-family: Arial; font-size: small"
                meta:resourcekey="AlignObj">
                <tr>
                    <td style="width: 45%">
                        <asp:Label ID="lblDate_Date_Time_ReportParameter" runat="server" Text=" تاریخ :" CssClass="WhiteLabel"
                            meta:resourcekey="lblDate_Date_Time_ReportParameter"></asp:Label>
                    </td>
                    <td id="Container_DateCalendars_Date_Time_ReportParameter">
                        <table runat="server" id="Container_pdpDate_Date_Time_ReportParameter" visible="false" style="width: 100%">
                            <tr>
                                <td>
                                    <pcal:PersianDatePickup ID="pdpDate_Date_Time_ReportParameter" runat="server" CssClass="PersianDatePicker"
                                        ReadOnly="true"></pcal:PersianDatePickup>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="Container_gdpDate_Date_Time_ReportParameter" visible="false" style="width: 100%">
                            <tr>
                                <td>
                                    <table id="Container_gCalDate_Date_Time_ReportParameter" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td onmouseup="btn_gdpDate_Date_Time_ReportParameter_OnMouseUp(event)">
                                                <ComponentArt:Calendar ID="gdpDate_ReportParameter" runat="server" ControlType="Picker"
                                                    PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                    SelectedDate="2008-1-1" MaxDate="2122-1-1">
                                                    <ClientEvents>
                                                        <SelectionChanged EventHandler="gdpDate_Date_Time_ReportParameter_OnDateChange" />
                                                    </ClientEvents>
                                                </ComponentArt:Calendar>
                                            </td>
                                            <td style="font-size: 10px;">&nbsp;
                                            </td>
                                            <td>
                                                <img id="btn_gdpDate_Date_Time_ReportParameter" alt="" class="calendar_button" onclick="btn_gdpDate_Date_Time_ReportParameter_OnClick(event)"
                                                    onmouseup="btn_gdpDate_Date_Time_ReportParameter_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                    <ComponentArt:Calendar ID="gCalDate_Date_Time_ReportParameter" runat="server" AllowMonthSelection="false"
                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                        MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_ReportParameter"
                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                        <ClientEvents>
                                            <SelectionChanged EventHandler="gCalDate_Date_Time_ReportParameter_OnChange" />
                                            <Load EventHandler="gCalDate_Date_Time_ReportParameter_onLoad" />
                                        </ClientEvents>
                                    </ComponentArt:Calendar>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 10%" rowspan="2">
                        <ComponentArt:ToolBar ID="TlbRegister_Date_Time_ReportParameter" runat="server"
                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbRegister_Date_Time_ReportParameter"
                                    runat="server" ClientSideCommand="tlbItemRegister_TlbRegister_Date_Time_ReportParameter_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRegister_TlbRegister_Date_Time_ReportParameter"
                                    TextImageSpacing="5" Enabled="true" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTime_Date_Time_ReportParameter" runat="server" Text=" زمان :" CssClass="WhiteLabel"
                            meta:resourcekey="lblTime_Date_Time_ReportParameter"></asp:Label>
                    </td>
                    <td id="Container_Time_ReportParameter">
                        <table style="width: 50%" dir="ltr">
                            <tr>
                                <td align="center">
                                    <input type="text" id="TimeSelector_Date_Time_ReportParameter_txtHour" style="width: 70%; text-align: center"
                                        onchange="TimeSelector_Date_Time_ReportParameter_onChange('txtHour')" />
                                </td>
                                <td align="center">:
                                </td>
                                <td align="center">
                                    <input type="text" id="TimeSelector_Date_Time_ReportParameter_txtMinute" style="width: 70%; text-align: center"
                                        onchange="TimeSelector_Date_Time_ReportParameter_onChange('txtMinute')" />
                                </td>
                            </tr>
                        </table>

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
        <asp:HiddenField runat="server" ID="hfCurrentDate_Date_Time_ReportParameter" />
        <asp:HiddenField runat="server" ID="hfConnectionError_Date_Time_ReportParameter"
            meta:resourcekey="hfConnectionError_Date_Time_ReportParameter" />
        <asp:HiddenField runat="server" ID="hfErrorType_Date_Time_ReportParameter"
            meta:resourcekey="hfErrorType_Date_Time_ReportParameter" />
        <asp:HiddenField runat="server" ID="ReportParameterID" />
    </form>
</body>
</html>
