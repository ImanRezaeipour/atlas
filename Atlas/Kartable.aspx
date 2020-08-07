﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kartable" Codebehind="Kartable.aspx.cs" %>

<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>    
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="KartableForm" runat="server" meta:resourcekey="KartableForm" onsubmit="return false;">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_KartableForm" style="width: 99%; height: 100%; font-family: Arial; font-size: small;"
            class="BoxStyle">
            <tr>
                <td>
                    <ComponentArt:ToolBar ID="TlbKartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                        ItemSpacing="1px" UseFadeEffect="false">
                    </ComponentArt:ToolBar>
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbKartableFilter_Kartable" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                </ComponentArt:ToolBar>
                            </td>
                            <td style="width: 70%">
                                <table style="width: 100%;">
                                    <tr>
                                        <%--ستون اول--%>
                                        <td style="width:40%">
                                            <table id="tblViewCordinateYearAndMonth_Kartable" class="BoxStyle" runat="server" visible="false">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblYear_Kartable" runat="server" Text=": سال" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblYear_Kartable" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMonth_Kartable" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblMonth_Kartable"></asp:Label>
                                                    </td>
                                                    <td>&nbsp</td>
                                                </tr>
                                                <tr>
                                                    <td id="tblcmbYearAndMonth_Kartable1">
                                                        <ComponentArt:ComboBox ID="cmbYear_Kartable" runat="server" AutoComplete="true" AutoFilter="true"
                                                            AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                            ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                            TextBoxEnabled="true" Width="70">
                                                            <ClientEvents>
                                                                <Change EventHandler="cmbYear_Kartable_onChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                    </td>
                                                    <td id="tblcmbYearAndMonth_Kartable2">
                                                        <ComponentArt:ComboBox ID="cmbMonth_Kartable" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" DropDownHeight="280">
                                                            <ClientEvents>
                                                                <Change EventHandler="cmbMonth_Kartable_onChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbViewCordinateYearAndMonth_Kartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                            ItemSpacing="1px" UseFadeEffect="false" >
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbViewCordinateYearAndMonth_Kartable" runat="server" ClientSideCommand="tlbItemView_TlbViewCordinateYearAndMonth_Kartable_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbViewCordinateYearAndMonth_Kartable"
                                                                    TextImageSpacing="5" Enabled="true"  />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                        <%-- ستون دوم--%>
                                        <td style="width:40%">
                                            <table id="tblDate_Kartable" runat="server"  visible="false" >
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDate_Kartable" runat="server" CssClass="WhiteLabel" Text=": تاریخ"
                                                            meta:resourcekey="lblDate_Kartable"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Container_DateCalendars_RequestRegister">
                                                        <table id="Container_pdpDate_Kartable" runat="server" visible="false" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpDate_Kartable" runat="server" CssClass="PersianDatePicker" ReadOnly="true">
                                                                    </pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="Container_gdpDate_Kartable" runat="server" visible="false" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table id="Container_gCalDate_Kartable" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpDate_Kartable_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpDate_Kartable" runat="server" ControlType="Picker"
                                                                                    MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                    SelectedDate="2008-1-1" Visible="false">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpDate_Kartable_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpDate_Kartable" alt="" class="calendar_button" runat="server" onclick="btn_gdpDate_Kartable_OnClick(event)"
                                                                                    onmouseup="btn_gdpDate_Kartable_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif"
                                                                                    visible="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalDate_Kartable" runat="server" AllowMonthSelection="false"
                                                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                        MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_Kartable"
                                                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" Visible="false">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalDate_Kartable_OnChange" />
                                                                            <Load EventHandler="gCalDate_Kartable_OnLoad" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" class="BoxStyle" id="tblViewCordinateDate_Kartable" visible="false">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFromDate_Kartable" runat="server" Text=": از تاریخ"
                                                            CssClass="WhiteLabel" meta:resourcekey="lblFromDate_Kartable" ></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblToDate_Kartable" runat="server" Text=": تا تاریخ" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblToDate_Kartable" ></asp:Label>
                                                    </td>
                                                    <td>&nbsp</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table runat="server" id="Container_pdpFromDate_Kartable" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpFromDate_Kartable" runat="server" CssClass="PersianDatePicker" Width="90px"
                                                                        ReadOnly="true"></pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table runat="server" id="Container_gdpFromDate_Kartable" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table id="Container_gCalFromDate_Kartable" border="0" cellpadding="0"
                                                                        cellspacing="0">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpFromDate_Kartable_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpFromDate_Kartable" runat="server" ControlType="Picker"
                                                                                    MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                    SelectedDate="2008-1-1">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpFromDate_Kartable_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpFromDate_Kartable" alt="" class="calendar_button" onclick="btn_gdpFromDate_Kartable_OnClick(event)"
                                                                                    onmouseup="btn_gdpFromDate_Kartable_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalFromDate_Kartable" runat="server" AllowMonthSelection="false"
                                                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                        MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_Kartable"
                                                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalFromDate_Kartable_OnChange" />
                                                                            <Load EventHandler="gCalFromDate_Kartable_onLoad" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table runat="server" id="Container_pdpToDate_Kartable" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpToDate_Kartable" runat="server" CssClass="PersianDatePicker" Width="90px"
                                                                        ReadOnly="true"></pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table runat="server" id="Container_gdpToDate_Kartable" visible="false"
                                                            style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table id="Container_gCalToDate_Kartable" border="0" cellpadding="0"
                                                                        cellspacing="0">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpToDate_Kartable_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpToDate_Kartable" runat="server" ControlType="Picker"
                                                                                    MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                    SelectedDate="2008-1-1">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpToDate_Kartable_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpToDate_Kartable" alt="" class="calendar_button" onclick="btn_gdpToDate_Kartable_OnClick(event)"
                                                                                    onmouseup="btn_gdpToDate_Kartable_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalToDate_Kartable" runat="server" AllowMonthSelection="false"
                                                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                        MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_Kartable"
                                                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalToDate_Kartable_OnChange" />
                                                                            <Load EventHandler="gCalToDate_Kartable_onLoad" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbViewCordinateDate_Kartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                            ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbViewCordinateDate_Kartable" runat="server" ClientSideCommand="tlbItemView_TlbViewCordinateDate_Kartable_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbViewCordinateDate_Kartable"
                                                                    TextImageSpacing="5" Enabled="true" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <%-- ستون سوم--%>
                                        <td style="width:20%">
                                            <table class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSortBy_Kartable" runat="server" Text=": مرتب سازی بر اساس" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblSortBy_Kartable"></asp:Label>
                                                    </td>
                                                    <td>&nbsp</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ComponentArt:ComboBox ID="cmbSortBy_Kartable" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Style="width: 140px;">
                                                            <ClientEvents>
                                                                <Change EventHandler="cmbSortBy_Kartable_onChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbView_Kartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                            ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_Kartable" runat="server" ClientSideCommand="tlbItemView_TlbView_Kartable_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_Kartable"
                                                                    TextImageSpacing="5" Enabled="true" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--     ابتدای قبلی--%>
                                        <%--      <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <table id="tbllblYearAndMonth">
                                                            <tr>
                                                                <td style="width: 15%">
                                                                    <asp:Label ID="lblYear_Kartable" runat="server" Text=": سال" CssClass="WhiteLabel"
                                                                        meta:resourcekey="lblYear_Kartable" Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <asp:Label ID="lblMonth_Kartable" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                                                        meta:resourcekey="lblMonth_Kartable" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 40%">
                                                        <asp:Label ID="lblDate_Kartable" runat="server" CssClass="WhiteLabel" Text=": تاریخ"
                                                            meta:resourcekey="lblDate_Kartable" Visible="false"></asp:Label>
                                                    </td>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="lblSortBy_Kartable" runat="server" Text=": مرتب سازی بر اساس" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblSortBy_Kartable"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="tblcmbYearAndMonth_Kartable">
                                                            <tr>
                                                                <td id="tblcmbYearAndMonth_Kartable1">
                                                                    <ComponentArt:ComboBox ID="cmbYear_Kartable" runat="server" AutoComplete="true" AutoFilter="true"
                                                                        AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                        TextBoxEnabled="true" Width="100" Visible="false">
                                                                        <ClientEvents>
                                                                            <Change EventHandler="cmbYear_Kartable_onChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:ComboBox>
                                                                </td>
                                                                <td id="tblcmbYearAndMonth_Kartable2">
                                                                    <ComponentArt:ComboBox ID="cmbMonth_Kartable" runat="server" AutoComplete="true"
                                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                        TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" Visible="false" DropDownHeight="280">
                                                                        <ClientEvents>
                                                                            <Change EventHandler="cmbMonth_Kartable_onChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:ComboBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td id="Container_DateCalendars_RequestRegister">
                                                        <table id="Container_pdpDate_Kartable" runat="server" visible="false" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <pcal:PersianDatePickup ID="pdpDate_Kartable" runat="server" CssClass="PersianDatePicker" ReadOnly="true">
                                                                    </pcal:PersianDatePickup>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table id="Container_gdpDate_Kartable" runat="server" visible="false" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <table id="Container_gCalDate_Kartable" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td onmouseup="btn_gdpDate_Kartable_OnMouseUp(event)">
                                                                                <ComponentArt:Calendar ID="gdpDate_Kartable" runat="server" ControlType="Picker"
                                                                                    MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                    SelectedDate="2008-1-1" Visible="false">
                                                                                    <ClientEvents>
                                                                                        <SelectionChanged EventHandler="gdpDate_Kartable_OnDateChange" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:Calendar>
                                                                            </td>
                                                                            <td style="font-size: 10px;">&nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img id="btn_gdpDate_Kartable" alt="" class="calendar_button" runat="server" onclick="btn_gdpDate_Kartable_OnClick(event)"
                                                                                    onmouseup="btn_gdpDate_Kartable_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif"
                                                                                    visible="false" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <ComponentArt:Calendar ID="gCalDate_Kartable" runat="server" AllowMonthSelection="false"
                                                                        AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                        CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                        DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                        MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                        OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_Kartable"
                                                                        PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                        SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1" Visible="false">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gCalDate_Kartable_OnChange" />
                                                                            <Load EventHandler="gCalDate_Kartable_OnLoad" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ComboBox ID="cmbSortBy_Kartable" runat="server" AutoComplete="true"
                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                            TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Style="width: 200px;">
                                                            <ClientEvents>
                                                                <Change EventHandler="cmbSortBy_Kartable_onChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:ComboBox>
                                                    </td>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbView_Kartable" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/"
                                                            ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_Kartable" runat="server" ClientSideCommand="tlbItemView_TlbView_Kartable_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_Kartable"
                                                                    TextImageSpacing="5" Enabled="true" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>    --%>
                                        <%--  انتهای قبلی  --%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table style="width: 100%; height: 300px;" class="BoxStyle">
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td id="header_Kartable_Kartable" class="HeaderLabel" style="width: 15%;">Kartable
                                        </td>
                                        <td id="loadingPanel_GridKartable_Kartable" class="HeaderLabel" style="width: 15%"></td>
                                        <td id="Container_SelectAllinthisPage_Kartable" style="width: 30%">
                                            <table runat="server" id="SelectAllinthisPageBox_Kartable" style="width: 100%;" visible="false">
                                                <tr>
                                                    <td style="width: 5%">
                                                        <input id="chbSelectAllinthisPage_Kartable" type="checkbox" onclick="chbSelectAllinthisPage_Kartable_onClick();" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSelectAllinthisPage_Kartable" runat="server" Text="انتخاب همه در این صفحه"
                                                            meta:resourcekey="lblSelectAllinthisPage_Kartable" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table runat="server" id="EndFlowRequestsViewBox_Kartable" style="width: 100%;" visible="false">
                                                <tr>
                                                    <td style="width: 5%">
                                                        <input id="chbEndFlowRequestsView_Kartable" type="checkbox" onclick="chbEndFlowRequestsView_Kartable_onClick();" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEndFlowRequestsView_Kartable" runat="server" Text="نمایش درخواست های تایید نهایی شده"
                                                            meta:resourcekey="lblEndFlowRequestsView_Kartable" CssClass="WhiteLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="HeaderLabel" style="width: 40%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td style="width: 80%">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 30%; font-size: small; font-weight: normal;">
                                                                    <asp:Label ID="lblQuickSearch_Kartable" runat="server" meta:resourcekey="lblQuickSearch_Kartable"
                                                                        Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <input type="text" runat="server" style="width: 99%;" class="TextBoxes"
                                                                        onkeypress="txtSearchTerm_Kartable_onKeyPess(event);" id="txtSearchTerm_Kartable" />
                                                                </td>
                                                                <td style="width: 5%">
                                                                    <ComponentArt:ToolBar ID="TlbKartableQuickSearch" runat="server" CssClass="toolbar"
                                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                        UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbKartableQuickSearch" runat="server"
                                                                                ClientSideCommand="tlbItemSearch_TlbKartableQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                ItemType="Command" meta:resourcekey="tlbItemSearch_TlbKartableQuickSearch" TextImageSpacing="5" />
                                                                        </Items>
                                                                    </ComponentArt:ToolBar>
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
                            <td valign="top">
                                <div id="Container_GridKartable_Kartable" style="width: 100%">
                                    <ComponentArt:CallBack ID="CallBack_GridKartable_Kartable" runat="server" OnCallback="CallBack_GridKartable_Kartable_onCallBack">
                                        <Content>
                                            <ComponentArt:DataGrid ID="GridKartable_Kartable" runat="server" AllowHorizontalScrolling="true"
                                                CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                                FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                PageSize="25" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                ScrollTopBottomImageWidth="16" Scr0ollImagesFolderUrl="images/Grid/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="1200">
                                                <Levels>
                                                    <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                        HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                        SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                        SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9">
                                                        <Columns>
                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn DataField="RequestID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn DataField="ParentID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="FlowStatus" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnFlowStatus_GridKartable_Kartable"
                                                                HeadingText="وضعیت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnState_GridKartable_Kartable"
                                                                Width="40" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="FlowLevels" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnFlowLevels_GridKartable_Kartable"
                                                                HeadingText="مراحل" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnLevels_GridKartable_Kartable"
                                                                Width="40" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="RequestType" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestType_GridKartable_Kartable"
                                                                HeadingText="نوع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnType_GridKartable_Kartable"
                                                                Width="30" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="RequestSource" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestSource_GridKartable_Kartable"
                                                                HeadingText="منبع" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRefrence_GridKartable_Kartable"
                                                                Width="60" />
                                                            <ComponentArt:GridColumn Align="Center" DefaultSortDirection="Descending" ColumnType="CheckBox" DataField="Select"
                                                                HeadingText="انتخاب" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSelect_GridKartable_Kartable"
                                                                Width="50" AllowEditing="True" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="Row" DefaultSortDirection="Descending"
                                                                HeadingText="ردیف" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRow_GridKartable_Kartable"
                                                                Width="30" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="BarCode" DefaultSortDirection="Descending"
                                                                HeadingText="کد پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelBarCode_GridKartable_Kartable"
                                                                Width="120" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="Applicant" DefaultSortDirection="Descending"
                                                                HeadingText="درخواست کننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnApplicant_GridKartable_Kartable"
                                                                Width="150" DataCellClientTemplateId="DataCellClientTemplateId_clmnApplicant_GridKartable_Kartable" TextWrap="true" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="RequestTitle" DefaultSortDirection="Descending"
                                                                HeadingText="عنوان درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestTopic_GridKartable_Kartable"
                                                                Width="150" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                                HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridKartable_Kartable"
                                                                Width="120" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                                HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridKartable_Kartable"
                                                                Width="120" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="TheFromTime" DefaultSortDirection="Descending"
                                                                HeadingText="از ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromHour_GridKartable_Kartable"
                                                                Width="70" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="TheToTime" DefaultSortDirection="Descending"
                                                                HeadingText="تا ساعت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToHour_GridKartable_Kartable"
                                                                Width="70" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="TheDuration" DefaultSortDirection="Descending"
                                                                HeadingText="مقدار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDuration_GridKartable_Kartable"
                                                                Width="70" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="RegistrationDate" DefaultSortDirection="Descending"
                                                                HeadingText="تاریخ صدور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExportDate_GridKartable_Kartable"
                                                                Width="100" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="OperatorUser" DefaultSortDirection="Descending"
                                                                HeadingText="صادرکننده" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnExporter_GridKartable_Kartable"
                                                                Width="150" TextWrap="true" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="DepartmentName" DefaultSortDirection="Descending"
                                                                HeadingText="بخش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDepartment_GridKartable_Kartable"
                                                                Width="150" TextWrap="true" DataCellClientTemplateId="DataCellClientTemplateId_clmnDepartment_GridKartable_Kartable" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                HeadingText="شرح درخواست" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnRequestDescription_GridKartable_Kartable"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnDescription_GridKartable_Kartable"
                                                                Width="200" TextWrap="true" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="AttachmentFile" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnAttachmentFile_GridKartable_Kartable"
                                                                HeadingText="آپلود فایل" HeadingTextCssClass="HeadingText" Width="60" TextWrap="true" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="RequestHistory" DefaultSortDirection="Descending"
                                                                DataCellClientTemplateId="DataCellClientTemplateId_clmnRequestHistory_GridKartable_Kartable"
                                                                HeadingText="ویرایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnHistory_GridKartable_Kartable"
                                                                Width="60" />
                                                            <ComponentArt:GridColumn DataField="ManagerFlowID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn DataField="PersonId" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn DataField="PersonImage" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ThePureFromDate" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="ThePureToDate" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="IsEdited" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="RequestSubstituteID" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="RequestSubstituteConfirm" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="DepartmentId" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>
                                                <ClientTemplates>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnFlowStatus_GridKartable_Kartable">
                                                        <table style="width: 100%; cursor: crosshair;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px;" title="##SetCellTitle_GridKartable_Kartable('FlowStatus', DataItem.GetMember('FlowStatus').Value)##">
                                                                    <img src="##SetClmnImage_GridKartable_Kartable('FlowStatus', DataItem.GetMember('FlowStatus').Value,DataItem.GetMember('ParentID').Value)##"
                                                                        alt="" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnFlowLevels_GridKartable_Kartable">
                                                        <table style="width: 100%; cursor: crosshair;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px; cursor: pointer;"
                                                                    ondblclick="GetRequestFlowLevel_GridKartable_Kartable();">
                                                                    <img style="cursor: crosshair;" src="##SetClmnImage_GridKartable_Kartable('FlowLevels', DataItem.GetMember('FlowLevels').Value,DataItem.GetMember('ParentID').Value)##"
                                                                        alt="" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestType_GridKartable_Kartable">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px;" title="##SetCellTitle_GridKartable_Kartable('RequestType', DataItem.GetMember('RequestType').Value);##">
                                                                    <img src="##SetClmnImage_GridKartable_Kartable('RequestType', DataItem.GetMember('RequestType').Value,DataItem.GetMember('ParentID').Value)##"
                                                                        alt="" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestSource_GridKartable_Kartable">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px;" title="##SetCellTitle_GridKartable_Kartable('RequestSource', DataItem.GetMember('RequestSource').Value);##">
                                                                    <img src="##SetClmnImage_GridKartable_Kartable('RequestSource', DataItem.GetMember('RequestSource').Value,DataItem.GetMember('ParentID').Value)##"
                                                                        alt="" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnRequestHistory_GridKartable_Kartable">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                ##SetRequestHistoryImage_GridKartable_Kartable(DataItem.GetMember('IsEdited').Value,DataItem.GetMember('ParentID').Value,DataItem.GetMember('RequestSubstituteID').Value)##
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnDescription_GridKartable_Kartable">
                                                        <table style="width: 100%; cursor: crosshair;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px; cursor: crosshair"
                                                                    ondblclick="ShowRequestDescription_GridKartable_Kartable();">##DataItem.GetMember('Description').Value##
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnApplicant_GridKartable_Kartable">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; cursor: pointer"
                                                                    ondblclick="ShowApplicantImage_GridKartable_Kartable();">##DataItem.GetMember('Applicant').Value##
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnAttachmentFile_GridKartable_Kartable">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px; cursor: pointer"
                                                                    ondblclick="ShowAttachmentFile_GridKartable_Kartable('AttachmentFile');">##SetAttachmentFileImage_GridKartable_Kartable(DataItem.GetMember('AttachmentFile').Value)##
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                    <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_clmnDepartment_GridKartable_Kartable">
                                                        <table style="width: 100%; cursor: crosshair;" ondblclick="ShowDialogParentDepartments();">
                                                            <tr>
                                                                <td align="center" style="font-family: Tahoma; font-size: 10px; cursor: pointer;">##DataItem.GetMember('DepartmentName').Value##
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ComponentArt:ClientTemplate>
                                                </ClientTemplates>
                                                <ClientEvents>
                                                    <Load EventHandler="GridKartable_Kartable_onLoad" />
                                                    <ItemCheckChange EventHandler="GridKartable_Kartable_onItemCheckChange" />
                                                </ClientEvents>
                                            </ComponentArt:DataGrid>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Kartable" />
                                            <asp:HiddenField runat="server" ID="hfKartableCount_Kartable" />
                                            <asp:HiddenField runat="server" ID="hfKartablePageCount_Kartable" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_GridKartable_Kartable_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_GridKartable_Kartable_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                            <ComponentArt:ToolBar ID="TlbPaging_GridKartable_Kartable" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                Style="direction: ltr" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridKartable_Kartable" runat="server"
                                                        ClientSideCommand="tlbItemRefresh_TlbPaging_GridKartable_Kartable_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridKartable_Kartable"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridKartable_Kartable" runat="server"
                                                        ClientSideCommand="tlbItemFirst_TlbPaging_GridKartable_Kartable_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="first.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridKartable_Kartable"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridKartable_Kartable" runat="server"
                                                        ClientSideCommand="tlbItemBefore_TlbPaging_GridKartable_Kartable_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridKartable_Kartable"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridKartable_Kartable" runat="server"
                                                        ClientSideCommand="tlbItemNext_TlbPaging_GridKartable_Kartable_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Next.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridKartable_Kartable"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridKartable_Kartable" runat="server"
                                                        ClientSideCommand="tlbItemLast_TlbPaging_GridKartable_Kartable_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="last.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridKartable_Kartable"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="beginfooter_GridKartable_Kartable" class="WhiteLabel" style="width: 45%">&nbsp;
                                        </td>
                                        <td runat="server" id="endfooter_GridKartable_Kartable" class="WhiteLabel" style="width: 45%"
                                            meta:resourcekey="InverseAlignObj"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogKartableFilterheader" FooterClientTemplateId="DialogKartableFilterfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogKartableFilter"
            runat="server" PreloadContentUrl="false" ContentUrl="KartableFilter.aspx" IFrameCssClass="KartableFilter_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogKartableFilterheader">
                    <table id="tbl_DialogKartableFilterheader" style="width: 603px" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogKartableFilter.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogKartableFilter_topLeftImage" style="display: block;" src="/DesktopModules/Atlas/Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogKartableFilter" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogKartableFilter" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="DialogKartableFilter.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogKartableFilter_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogKartableFilterfooter">
                    <table id="tbl_DialogKartableFilterfooter" style="width: 603px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogKartableFilter_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogKartableFilter_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogKartableFilter_onShow" />
                <OnClose EventHandler="DialogKartableFilter_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogHistoryheader" FooterClientTemplateId="DialogHistoryfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogHistory"
            runat="server" PreloadContentUrl="false" ContentUrl="History.aspx" IFrameCssClass="History_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogHistoryheader">
                    <table id="tbl_DialogHistoryheader" style="width: 603px" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogHistory.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogHistory_topLeftImage" style="display: block;" src="/DesktopModules/Atlas/Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogHistory" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogHistory" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogHistory_IFrame').src = 'WhitePage.aspx'; DialogHistory.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogHistory_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogHistoryfooter">
                    <table id="tbl_DialogHistoryfooter" style="width: 603px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogHistory_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogHistory_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogHistory_onShow" />
                <OnClose EventHandler="DialogHistory_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogRequestRejectDescription"
            runat="server" Width="350px">
            <Content>
                <table id="tbl_RequestRejectDescription_Kartable" runat="server" style="width: 100%; font-family: Arial; font-size: small"
                    class="BodyStyle">
                    <tr>
                        <td style="width: 98%">
                            <ComponentArt:ToolBar ID="TlbRequestReject_Kartable" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemEndorsement_TlbRequestReject_Kartable" runat="server"
                                        ClientSideCommand="tlbItemEndorsement_TlbRequestReject_Kartable_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemEndorsement_TlbRequestReject_Kartable"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRequestReject_Kartable" runat="server"
                                        ClientSideCommand="tlbItemCancel_TlbRequestReject_Kartable_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbRequestReject_Kartable"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                            <ComponentArt:ToolBar ID="tlbExit_RequestReject_Kartable" runat="server" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RequestReject_Kartable" runat="server"
                                        ClientSideCommand="tlbItemExit_tlbExit_RequestReject_Kartable_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RequestReject_Kartable"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td id="hfDescription_RequestReject_Kartable" style="width: 98%"></td>
                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtDescription_RequestReject_Kartable" cols="5" name="S1" rows="12"
                                style="width: 99%; height: 100px;" class="TextBoxes"></textarea>
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogRequestRejectDescription_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogRequestDescription"
            runat="server" Width="400px">
            <Content>
                <table id="tbl_DialogRequestDescription_Kartable" runat="server" class="BodyStyle"
                    style="width: 100%; font-family: Arial; font-size: small">
                    <tr>
                        <td style="width: 98%">&nbsp;
                        </td>
                        <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">
                            <ComponentArt:ToolBar ID="tlbExit_RequestDescription_Kartable" runat="server" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_RequestDescription_Kartable" runat="server"
                                        ClientSideCommand="tlbItemExit_tlbExit_RequestDescription_Kartable_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_RequestDescription_Kartable"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 98%">
                            <asp:Label ID="lblDescription_RequestDescription_Kartable" runat="server" CssClass="WhiteLabel"
                                meta:resourcekey="lblDescription_RequestDescription_Kartable" Text=": توضیحات درخواست"></asp:Label>
                        </td>
                        <td id="Td3" runat="server" meta:resourcekey="InverseAlignObj" style="width: 2%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtDescription_RequestDescription_Kartable" cols="5" name="S1" rows="12"
                                style="width: 99%; height: 100px" class="TextBoxes" readonly="readonly"></textarea>
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogRequestDescription_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogRequestsStateheader" FooterClientTemplateId="DialogRequestsStatefooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogRequestsState"
            runat="server" PreloadContentUrl="false" ContentUrl="RequestsState.aspx" IFrameCssClass="RequestsState_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogRequestsStateheader">
                    <table id="tbl_DialogRequestsStateheader" style="width: 603px" cellpadding="0" cellspacing="0"
                        border="0" onmousedown="DialogRequestsState.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogRequestsState_topLeftImage" style="display: block;" src="/DesktopModules/Atlas/Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogRequestsState" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogRequestsState" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="DialogRequestsState.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogRequestsState_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogRequestsStatefooter">
                    <table id="tbl_DialogRequestsStatefooter" style="width: 603px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogRequestsState_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogRequestsState_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogRequestsState_onShow" />
                <OnClose EventHandler="DialogRequestsState_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogApplicantImage"
            runat="server" Width="200px">
            <Content>
                <table id="Mastertbl_DialogApplicantImage" style="width: 100%" class="BodyStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 95%" id="tdCurrentApplicant_DialogApplicantImage">&nbsp;
                                    </td>
                                    <td id="Td4" style="width: 5%" runat="server" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbApplicantPicture" runat="server"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemClosePicture_TlbApplicantPicture" runat="server"
                                                    ClientSideCommand="tlbItemClosePicture_TlbApplicantPicture_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemClosePicture_TlbApplicantPicture"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <iframe id="ApplicantImage_DialogApplicantImage" scrolling="yes"
                                style="width: 70%; height: 170px; overflow: scroll" allowtransparency="true" frameborder="0"></iframe>
                        </td>
                    </tr>
                </table>
            </Content>
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogGridSettings"
            runat="server" Width="200px">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
                    meta:resourcekey="tblGridSettings_DialogGridSettings"
                    class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbGridSettings" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGridSettings" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbGridSettings_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbGridSettings"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbGridSettings" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbGridSettings_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbGridSettings"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td id="header_GridSettings_Kartabl" style="color: White; font-weight: bold; font-family: Arial; width: 100%">Grid Settings
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 60%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_GridSettings_Kartable" type="checkbox"
                                                        onclick="chbSelectAll_GridSettings_Kartable_onClick();" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblSelectAll_GridSettings_Kartabl" meta:resourcekey="lblSelectAll_GridSettings_Kartabl"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridSettings_Kartable"
                                            OnCallback="CallBack_GridSettings_Kartable_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridSettings_Kartable" runat="server" AllowMultipleSelect="false"
                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" Height="495"
                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                                    ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="17" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" TabIndex="0" Width="180" AllowColumnResizing="false"
                                                    ScrollBar="Auto" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
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
                                                                <ComponentArt:GridColumn DataField="CurrentUserSettingID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                <ComponentArt:GridColumn DataField="Exist" Visible="false" ColumnType="CheckBox" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="GridColumn" DefaultSortDirection="Descending"
                                                                    HeadingText="ستون گرید" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGridColumn_GridSettings_Kartabl" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ViewState" DefaultSortDirection="Descending"
                                                                    HeadingText="نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnView_GridSettings_Kartabl"
                                                                    ColumnType="CheckBox" AllowEditing="True" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSettings_Kartable" />
                                                <asp:HiddenField runat="server" ID="hfExist_GridSettings_Kartable" />
                                                <asp:HiddenField runat="server" ID="hfCurrentId_GridSettings_Kartable" />
                                                <asp:HiddenField runat="server" ID="hfCurrentUserSettingId_GridSettings_Kartable" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridSettings_Kartabl_CallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridSettings_Kartabl_onCallbackError" />
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogParentDepartments"
            runat="server" Width="400px">
            <Content>
                <table id="tbl_DialogParentDepartments_Kartable" runat="server" class="BodyStyle"
                    style="width: 100%; font-family: Arial; font-size: small">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 98%">&nbsp;
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="tlbExit_ParentDepartments_Kartable" runat="server"
                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                            UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_tlbExit_ParentDepartments_Kartable"
                                                    runat="server" ClientSideCommand="tlbItemExit_tlbExit_ParentDepartments_Kartable_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="close-down.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_tlbExit_ParentDepartments_Kartable"
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
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="lblParentDepartments_ParentDepartments_Kartable" runat="server"
                                            CssClass="WhiteLabel" meta:resourcekey="lblParentDepartments_ParentDepartments_Kartable"
                                            Text=": بخش های والد"></asp:Label>
                                    </td>
                                    <td id="loadingPanel_trvParentDepartments_Kartable" class="HeaderLabel"
                                        style="width: 45%"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ComponentArt:CallBack runat="server" ID="CallBack_trvParentDepartments_Kartable"
                                OnCallback="CallBack_trvParentDepartments_Kartable_onCallBack">
                                <Content>
                                    <ComponentArt:TreeView ID="trvParentDepartments_Kartable" runat="server"
                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="200"
                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                        LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                        NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                        ShowLines="true" Width="100%" meta:resourcekey="trvLineImages">
                                        <ClientEvents>
                                            <Load EventHandler="trvParentDepartments_Kartable_onLoad" />
                                            <NodeExpand EventHandler="trvParentDepartments_Kartable_onNodeExpand" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_ParentDepartments_Kartable" />
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="CallBack_trvParentDepartments_Kartable_onCallbackComplete" />
                                    <CallbackError EventHandler="CallBack_trvParentDepartments_Kartable_onCallbackError" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogParentDepartments_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogKartable" meta:resourcekey="hfTitle_DialogKartable" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogSurveyedRequests" meta:resourcekey="hfTitle_DialogSurveyedRequests" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogSentry" meta:resourcekey="hfTitle_DialogSentry" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogSpecialKartable" meta:resourcekey="hfTitle_DialogSpecialKartable" />
        <asp:HiddenField runat="server" ID="hfheader_Kartable_Kartable" meta:resourcekey="hfheader_Kartable_Kartable" />
        <asp:HiddenField runat="server" ID="hfheader_SurveyedRequests_Kartable" meta:resourcekey="hfheader_SurveyedRequests_Kartable" />
        <asp:HiddenField runat="server" ID="hfheader_SentryKartable_Kartable" meta:resourcekey="hfheader_SentryKartable_Kartable" />
        <asp:HiddenField runat="server" ID="hfheader_SpecialKartable_Kartable" meta:resourcekey="hfheader_SpecialKartable_Kartable" />
        <asp:HiddenField runat="server" ID="hfKartablePageSize_Kartable" />
        <asp:HiddenField runat="server" ID="hffooter_GridKartable_Kartable" meta:resourcekey="hffooter_GridKartable_Kartable" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridKartable_Kartable" meta:resourcekey="hfloadingPanel_GridKartable_Kartable" />
        <asp:HiddenField runat="server" ID="hfRejectMessage_Kartable" meta:resourcekey="hfRejectMessage_Kartable" />
        <asp:HiddenField runat="server" ID="hfKartableCloseMessage_Kartable" meta:resourcekey="hfKartableCloseMessage_Kartable" />
        <asp:HiddenField runat="server" ID="hfSpecialKartableCloseMessage_Kartable" meta:resourcekey="hfSpecialKartableCloseMessage_Kartable" />
        <asp:HiddenField runat="server" ID="hfSurveyCloseMessage_Kartable" meta:resourcekey="hfSurveyCloseMessage_Kartable" />
        <asp:HiddenField runat="server" ID="hfSentryCloseMessage_Kartable" meta:resourcekey="hfSentryCloseMessage_Kartable" />
        <asp:HiddenField runat="server" ID="hfPageChange_Kartable" meta:resourcekey="hfPageChange_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestTypes_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestSources_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestStates_Kartable" />
        <asp:HiddenField runat="server" ID="hfCurrentYear_Kartable" />
        <asp:HiddenField runat="server" ID="hfCurrentMonth_Kartable" />
        <asp:HiddenField runat="server" ID="hfCurrentSortBy_Kartable" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_Kartable" />
        <asp:HiddenField runat="server" ID="hfErrorType_Kartable" meta:resourcekey="hfErrorType_Kartable" />
        <asp:HiddenField runat="server" ID="hfConnectionError_Kartable" meta:resourcekey="hfConnectionError_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestRejectDescription_Kartable" meta:resourcekey="hfRequestRejectDescription_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestDeleteDescription_Kartable" meta:resourcekey="hfRequestDeleteDescription_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestCount_GridKartabl_Kartabl" meta:resourcekey="hfRequestCount_GridKartabl_Kartabl" />
        <asp:HiddenField runat="server" ID="hfheader_GridSettings_Kartabl" meta:resourcekey="hfheader_GridSettings_Kartabl" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogRequestSubstituteKartable" meta:resourcekey="hfTitle_DialogRequestSubstituteKartable" />
        <asp:HiddenField runat="server" ID="hfheader_SpecialRequestSubstituteKartable_Kartable" meta:resourcekey="hfheader_SpecialRequestSubstituteKartable_Kartable" />
        <asp:HiddenField runat="server" ID="hfRequestSubstituteKartableCloseMessage_Kartable" meta:resourcekey="hfRequestSubstituteKartableCloseMessage_Kartable" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_trvParentDepartments_Kartable" meta:resourcekey="hfloadingPanel_trvParentDepartments_Kartable" />
        <%-- <asp:HiddenField runat="server" ID="hftblCordinateView_Kartable" meta:resourcekey="hftblCordinateView_Kartable" />
        <asp:HiddenField runat="server" ID="hftblSortBy_Kartable" meta:resourcekey="hftblSortBy_Kartable" />--%>


    </form>
</body>
</html>


