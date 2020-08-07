<%@ Page Language="C#" AutoEventWireup="true" Inherits="PersonnelDynamicExtraInformation" Codebehind="PersonnelDynamicExtraInformation.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/calendarStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="PersonnelDynamicExtraInformationForm" runat="server" meta:resourcekey="PersonnelDynamicExtraInformationForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 98%; height: 400px; font-family: Arial; font-size: small" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 90%">
                                            <ComponentArt:ToolBar ID="TlbDynamicParameters" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbDynamicParameters" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemNew_TlbDynamicParameters_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbDynamicParameters"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbDynamicParameters" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemEdit_TlbDynamicParameters_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbDynamicParameters"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbDynamicParameters" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemDelete_TlbDynamicParameters_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                                        meta:resourcekey="tlbItemDelete_TlbDynamicParameters" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDynamicParameters" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemSave_TlbDynamicParameters_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command"
                                                        meta:resourcekey="tlbItemSave_TlbDynamicParameters" TextImageSpacing="5" Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDynamicParameters" runat="server" Enabled="false"
                                                        ClientSideCommand="tlbItemCancel_TlbDynamicParameters_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDynamicParameters" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbDynamicParameters" runat="server"
                                                        ClientSideCommand="tlbItemFormReconstruction_TlbDynamicParameters_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbDynamicParameters" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDynamicParameters" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemExit_TlbDynamicParameters_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbDynamicParameters"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="ActionMode_DynamicParameters" class="ToolbarMode"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td valign="top" style="width: 70%">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td style="color: White; width: 100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_DynamicParameters_PersonnelDynamicExtraInformation" class="HeaderLabel" style="width: 50%">Rule Parameters
                                                    </td>
                                                    <td id="loadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation" class="HeaderLabel" style="width: 45%"></td>
                                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_GridDynamicParameters_PersonnelDynamicExtraInformation" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDynamicParameters_PersonnelDynamicExtraInformation" runat="server"
                                                                    ClientSideCommand="Refresh_GridDynamicParameters_PersonnelDynamicExtraInformation();" DropDownImageHeight="16px" DropDownImageWidth="16px"
                                                                    ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command"
                                                                    meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDynamicParameters_PersonnelDynamicExtraInformation" TextImageSpacing="5" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <ComponentArt:CallBack ID="CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation" runat="server" OnCallback="CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation_onCallBack">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridDynamicParameters_PersonnelDynamicExtraInformation" runat="server" AllowHorizontalScrolling="true"
                                                        CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                                        FooterCssClass="GridFooter" Height="100%" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                        PageSize="5" RunningMode="Client" Width="744px" AllowMultipleSelect="false"
                                                        AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                        ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                        <Levels>
                                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                SortImageWidth="9">
                                                                <Columns>
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Key" DefaultSortDirection="Descending"
                                                                        HeadingText="کد پارامتر پویا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDynamicParameterCustomCode_GridDynamicParameters_PersonnelDynamicExtraInformation" TextWrap="true"/>
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                                        HeadingText="عنوان پارامتر پویا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDynamicParameterTitle_GridDynamicParameters_PersonnelDynamicExtraInformation"
                                                                        TextWrap="true" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientEvents>
                                                            <ItemSelect EventHandler="GridDynamicParameters_PersonnelDynamicExtraInformation_onItemSelect" />
                                                            <Load EventHandler="GridDynamicParameters_PersonnelDynamicExtraInformation_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField ID="ErrorHiddenField_DynamicParameters_PersonnelDynamicExtraInformation" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridDynamicParameters_PersonnelDynamicExtraInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width: 100%;" class="BoxStyle" id="tblDynamicParametersDetails_PersonnelDynamicExtraInformation">
                                    <tr>
                                        <td class="DetailsBoxHeaderStyle">
                                            <div id="header_DynamicParameterDetails_PersonnelDynamicExtraInformation" runat="server" meta:resourcekey="AlignObj" style="width: 100%; height: 100%"
                                                class="BoxContainerHeader">
                                                Rule Parameter Details
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDynamicParameterCustomCode_PersonnelDynamicExtraInformation" runat="server" meta:resourcekey="lblDynamicParameterCustomCode_PersonnelDynamicExtraInformation"
                                                Text=": کد پارامتر پویا" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtDynamicParameterCustomCode_PersonnelDynamicExtraInformation"
                                                disabled="disabled"   />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDynamicParameterTitle_PersonnelDynamicExtraInformation" runat="server" meta:resourcekey="lblDynamicParameterTitle_PersonnelDynamicExtraInformation"
                                                Text=": عنوان پارامتر پویا" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtDynamicParameterTitle_PersonnelDynamicExtraInformation"
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
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 90%">
                                <ComponentArt:ToolBar ID="TlbDynamicParametersPairs" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbDynamicParametersPairs" runat="server" ClientSideCommand="tlbItemNew_TlbDynamicParametersPairs_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbDynamicParametersPairs"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbDynamicParametersPairs" runat="server" ClientSideCommand="tlbItemEdit_TlbDynamicParametersPairs_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbDynamicParametersPairs"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbDynamicParametersPairs" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemDelete_TlbDynamicParametersPairs_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbDynamicParametersPairs" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDynamicParametersPairs" runat="server" ClientSideCommand="tlbItemSave_TlbDynamicParametersPairs_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDynamicParametersPairs"
                                            TextImageSpacing="5" Enabled="false" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDynamicParametersPairs" runat="server" Enabled="false"
                                            ClientSideCommand="tlbItemCancel_TlbDynamicParametersPairs_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDynamicParametersPairs" TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_DynamicParametersPairs" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">                        
                        <tr>
                            <td style="width: 60%">
                                <asp:Label ID="lblDynamicParameterValue_PersonnelDynamicExtraInformation" runat="server" meta:resourcekey="lblDynamicParameterValue_PersonnelDynamicExtraInformation"
                                    Text=": مقدار پارامتر پویا" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblFromDate_PersonnelDynamicExtraInformation" runat="server" meta:resourcekey="lblFromDate_PersonnelDynamicExtraInformation"
                                    Text=": از تاریخ" CssClass="WhiteLabel"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:Label ID="lblToDate_PersonnelDynamicExtraInformation" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblToDate_PersonnelDynamicExtraInformation" Text=": تا تاریخ"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="txtDynamicParameterValue_PersonnelDynamicExtraInformation" runat="server" class="TextBoxes" disabled="disabled"   style="width: 70%;" type="text" /></td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Container_FromDateCalendars_PersonnelDynamicExtraInformation">
                                            <table id="Container_pdpFromDate_PersonnelDynamicExtraInformation" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpFromDate_PersonnelDynamicExtraInformation" runat="server" CssClass="PersianDatePicker" ReadOnly="true" Wrap="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="Container_gdpFromDate_PersonnelDynamicExtraInformation" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalFromDate_PersonnelDynamicExtraInformation" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpFromDate_PersonnelDynamicExtraInformation_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpFromDate_PersonnelDynamicExtraInformation" runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpFromDate_PersonnelDynamicExtraInformation_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp; </td>
                                                                <td>
                                                                    <img id="btn_gdpFromDate_PersonnelDynamicExtraInformation" alt="" class="calendar_button" onclick="btn_gdpFromDate_PersonnelDynamicExtraInformation_OnClick(event)" onmouseup="btn_gdpFromDate_PersonnelDynamicExtraInformation_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalFromDate_PersonnelDynamicExtraInformation" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpFromDate_PersonnelDynamicExtraInformation" PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalFromDate_PersonnelDynamicExtraInformation_OnChange" />
                                                                <Load EventHandler="gCalFromDate_PersonnelDynamicExtraInformation_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:Calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="Container_ToDateCalendars_PersonnelDynamicExtraInformation">
                                            <table id="Container_pdpToDate_PersonnelDynamicExtraInformation" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <pcal:PersianDatePickup ID="pdpToDate_PersonnelDynamicExtraInformation" runat="server" CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="Container_gdpToDate_PersonnelDynamicExtraInformation" runat="server" style="width: 100%" visible="false">
                                                <tr>
                                                    <td>
                                                        <table id="Container_gCalToDate_PersonnelDynamicExtraInformation" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td onmouseup="btn_gdpToDate_PersonnelDynamicExtraInformation_OnMouseUp(event)">
                                                                    <ComponentArt:Calendar ID="gdpToDate_PersonnelDynamicExtraInformation" runat="server" ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom" SelectedDate="2008-1-1">
                                                                        <ClientEvents>
                                                                            <SelectionChanged EventHandler="gdpToDate_PersonnelDynamicExtraInformation_OnDateChange" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:Calendar>
                                                                </td>
                                                                <td style="font-size: 10px;">&nbsp; </td>
                                                                <td>
                                                                    <img id="btn_gdpToDate_PersonnelDynamicExtraInformation" alt="" class="calendar_button" onclick="btn_gdpToDate_PersonnelDynamicExtraInformation_OnClick(event)" onmouseup="btn_gdpToDate_PersonnelDynamicExtraInformation_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <ComponentArt:Calendar ID="gCalToDate_PersonnelDynamicExtraInformation" runat="server" AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpToDate_PersonnelDynamicExtraInformation" PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                            <ClientEvents>
                                                                <SelectionChanged EventHandler="gCalToDate_PersonnelDynamicExtraInformation_OnChange" />
                                                                <Load EventHandler="gCalToDate_PersonnelDynamicExtraInformation_onLoad" />
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
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td valign="top" style="width: 100%">
                                <table style="width: 100%;" class="BoxStyle">
                                    <tr>
                                        <td style="color: White; width: 100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_DynamicParameterPairs_PersonnelDynamicExtraInformation" class="HeaderLabel" style="width: 60%">Rule Parameter Pairs
                                                    </td>
                                                    <td id="loadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" class="HeaderLabel" style="width: 35%"></td>
                                                    <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" runat="server"
                                                                    ClientSideCommand="Refresh_GridDynamicParameterPairs_PersonnelDynamicExtraInformation();" DropDownImageHeight="16px"
                                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDynamicParameterPairs_PersonnelDynamicExtraInformation"
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
                                            <ComponentArt:CallBack runat="server" ID="CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" OnCallback="CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onCallBack">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridDynamicParameterPairs_PersonnelDynamicExtraInformation" runat="server" CssClass="Grid" EnableViewState="false"
                                                        FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                        PagePaddingEnabled="true" PageSize="4" RunningMode="Client" Width="100%" AllowMultipleSelect="false"
                                                        ScrollBar="On" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                        ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                        ShowFooter="false" ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                        ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                        <Levels>
                                                            <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                SortImageWidth="9">
                                                                <Columns>
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                    <ComponentArt:GridColumn DataField="Person.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                    <ComponentArt:GridColumn DataField="ParamField.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Value" DefaultSortDirection="Descending"
                                                                        HeadingText="مقدار پارامتر پویا" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDynamicParameterValue_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                                        HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnFromDate_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                                        HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnToDate_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientEvents>
                                                            <Load EventHandler="GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onLoad" />
                                                            <ItemSelect EventHandler="GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onItemSelect" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField ID="ErrorHiddenField_DynamicParameterPairs_PersonnelDynamicExtraInformation" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridDynamicParameterPairs_PersonnelDynamicExtraInformation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
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
        <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelDynamicExtraInformation" meta:resourcekey="hfTitle_DialogPersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfheader_DynamicParameters_PersonnelDynamicExtraInformation" meta:resourcekey="hfheader_DynamicParameters_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfheader_DynamicParameterDetails_PersonnelDynamicExtraInformation" meta:resourcekey="hfheader_DynamicParameterDetails_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfheader_DynamicParameterPairs_PersonnelDynamicExtraInformation" meta:resourcekey="hfheader_DynamicParameterPairs_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfheader_DynamicParameterPairDetails_PersonnelDynamicExtraInformation" meta:resourcekey="hfheader_DynamicParameterPairDetails_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfView_DynamicParameter" meta:resourcekey="hfView_DynamicParameter" />
        <asp:HiddenField runat="server" ID="hfAdd_DynamicParameter" meta:resourcekey="hfAdd_DynamicParameter" />
        <asp:HiddenField runat="server" ID="hfEdit_DynamicParameter" meta:resourcekey="hfEdit_DynamicParameter" />
        <asp:HiddenField runat="server" ID="hfDelete_DynamicParameter" meta:resourcekey="hfDelete_DynamicParameter" />
        <asp:HiddenField runat="server" ID="hfView_DynamicParameterPair" meta:resourcekey="hfView_DynamicParameterPair" />
        <asp:HiddenField runat="server" ID="hfAdd_DynamicParameterPair" meta:resourcekey="hfAdd_DynamicParameterPair" />
        <asp:HiddenField runat="server" ID="hfDelete_DynamicParameterPair" meta:resourcekey="hfDelete_DynamicParameterPair" />
        <asp:HiddenField runat="server" ID="hfEdit_DynamicParameterPair" meta:resourcekey="hfEdit_DynamicParameterPair" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_DynamicParameter" meta:resourcekey="hfDeleteMessage_DynamicParameter" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_DynamicParameterPair" meta:resourcekey="hfDeleteMessage_DynamicParameterPair" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelDynamicExtraInformation" meta:resourcekey="hfCloseMessage_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation" meta:resourcekey="hfloadingPanel_GridDynamicParameters_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" meta:resourcekey="hfloadingPanel_GridDynamicParameterPairs_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfErrorType_PersonnelDynamicExtraInformation" meta:resourcekey="hfErrorType_PersonnelDynamicExtraInformation" />
        <asp:HiddenField runat="server" ID="hfConnectionError_PersonnelDynamicExtraInformation" meta:resourcekey="hfConnectionError_RPersonnelDynamicExtraInformation" />
    </form>
</body>
</html>
