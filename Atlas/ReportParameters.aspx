<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="GTS.Clock.Presentaion.WebForms.ReportParameters" CodeBehind="ReportParameters.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ReportParametersForm" runat="server" meta:resourcekey="ReportParametersForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="font-size: small; background-color: White; font-family: Arial; width: 960px">
            <tr>
                <td id="MultiPageContainerParent_ReportParameters">
                    <table id="MultiPageContainer_ReportParameters">
                        <tr>
                            <td>
                                <div class="TabStripContainer">
                                    <ComponentArt:TabStrip ID="TabStripReportParameters" runat="server" DefaultGroupTabSpacing="1"
                                        DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                                        ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageReportParameters" ScrollLeftLookId="ScrollItem"
                                        ScrollRightLookId="ScrollItem">
                                        <ItemLooks>
                                            <ComponentArt:ItemLook CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingBottom="4"
                                                LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="25"
                                                LeftIconUrl="tab_left_icon.gif" LeftIconWidth="13" LookId="DefaultTabLook" meta:resourcekey="DefaultTabLook"
                                                RightIconHeight="25" RightIconUrl="tab_right_icon.gif" RightIconWidth="13" />
                                            <ComponentArt:ItemLook CssClass="SelectedTab" LabelPaddingBottom="4" LabelPaddingLeft="15"
                                                LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="25" LeftIconUrl="selected_tab_left_icon.gif"
                                                LeftIconWidth="13" LookId="SelectedTabLook" meta:resourcekey="SelectedTabLook"
                                                RightIconHeight="25" RightIconUrl="selected_tab_right_icon.gif" RightIconWidth="13" />
                                            <ComponentArt:ItemLook CssClass="ScrollItem" HoverCssClass="ScrollItemHover" LabelPaddingBottom="0"
                                                LabelPaddingLeft="5" LabelPaddingRight="5" LabelPaddingTop="0" LookId="ScrollItem" />
                                        </ItemLooks>
                                        <Tabs>
                                            <ComponentArt:TabStripTab ID="tbPersoanlFilter_TabStripReportParameters" meta:resourcekey="tbPersoanlFilter_TabStripReportParameters"
                                                Text="فیلتر فردی">
                                            </ComponentArt:TabStripTab>
                                            <ComponentArt:TabStripTab ID="tbGroupFilter_TabStripReportParameters" meta:resourcekey="tbGroupFilter_TabStripReportParameters"
                                                Text="فیلتر گروهی">
                                            </ComponentArt:TabStripTab>
                                        </Tabs>
                                        <ClientEvents>
                                            <TabSelect EventHandler="TabStripReportParameters_onTabSelect" />
                                        </ClientEvents>
                                    </ComponentArt:TabStrip>
                                </div>
                                <ComponentArt:MultiPage ID="MultiPageReportParameters" runat="server" CssClass="MultiPage"
                                    Width="960">
                                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvPersoanlFilter_TabStripReportParameters"
                                        Visible="true">
                                        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <ComponentArt:ToolBar ID="TlbPersonalFilter_PersonalFilter_ReportParameters" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemReportView_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemReportView_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Report.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportView_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemReportViewExcel_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemReportViewExcel_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Excel.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportViewExcel_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemDynamicConditions_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemDynamicConditions_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="verde.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDynamicConditions_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                                ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick();" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                TextImageSpacing="5" /> 
                                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemExit_TlbPersoanlFilter_PersonalFilter_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersoanlFilter_PersonalFilter_ReportParameters"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;" class="BoxStyle">
                                                        <tr>
                                                            <td id="headerPersonnelFilter_PersonalFilter_ReportParameters" class="HeaderLabel">فیلتر پرسنل
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <table class="BoxStyle" style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 90%">
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td style="width: 50%">
                                                                                                                <asp:Label ID="lblPersonnel_PersonalFilter_ReportParameters" runat="server" Text=": پرسنل"
                                                                                                                    CssClass="WhiteLabel" meta:resourcekey="lblPersonnel_PersonalFilter_ReportParameters"></asp:Label>
                                                                                                            </td>
                                                                                                            <td id="Td1" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                                                <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                    runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                                    Style="direction: ltr" UseFadeEffect="false">
                                                                                                                    <Items>
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="first.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Before.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Next.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            TextImageSpacing="5" />
                                                                                                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters_onClick();"
                                                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="last.png"
                                                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_PersonalFilter_ReportParameters"
                                                                                                                            TextImageSpacing="5" />
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
                                                                                <tr>
                                                                                    <td style="width: 90%">
                                                                                        <ComponentArt:CallBack ID="CallBack_cmbPersonnel_PersonalFilter_ReportParameters"
                                                                                            runat="server" OnCallback="CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onCallBack"
                                                                                            Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbPersonnel_PersonalFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode,CardNum"
                                                                                                    DataTextField="FirstNameAndLastName" DropDownCssClass="comboDropDown" DropDownHeight="210"
                                                                                                    DropDownPageSize="7" DropDownWidth="460" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                    ItemClientTemplateId="ItemTemplate_cmbPersonnel_PersonalFilter_ReportParameters"
                                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client"
                                                                                                    SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                                    <ClientTemplates>
                                                                                                        <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_PersonalFilter_ReportParameters">
                                                                                                            <table border="0" cellpadding="0" cellspacing="0" width="460">
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
                                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="460">
                                                                                                            <tr class="headingRow">
                                                                                                                <td id="clmnName_cmbPersonnel_PersonalFilter_ReportParameters" class="headingCell"
                                                                                                                    style="width: 40%; text-align: center">Name And Family
                                                                                                                </td>
                                                                                                                <td id="clmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters" class="headingCell"
                                                                                                                    style="width: 30%; text-align: center">BarCode
                                                                                                                </td>
                                                                                                                <td id="clmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters" class="headingCell"
                                                                                                                    style="width: 30%; text-align: center">CardNum
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </DropDownHeader>
                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbPersonnel_PersonalFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField ID="ErrorHiddenField_Personnel_PersonalFilter_ReportParameters"
                                                                                                    runat="server" />
                                                                                                <asp:HiddenField ID="hfPersonnelPageCount_PersonalFilter_ReportParameters" runat="server" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbPersonnel_PersonalFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <table style="width: 50%">
                                                                                <tr>
                                                                                    <td style="width: 2%">
                                                                                        <input id="chbOnlyForCurrentUser_ReportParameters" type="checkbox" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblOnlyForCurrentUser_ReportParameters" runat="server" Text="گزارش کاربر جاری" CssClass="WhiteLabel"
                                                                                            meta:resourcekey="lblOnlyForCurrentUser_ReportParameters"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 2%">
                                                                                        <input id="chbDeactivePersonSearch_ReportParameters" type="checkbox" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblDeactivePersonSearch_ReportParameters" runat="server" Text="فیلتر شامل پرسنل غیر فعال" CssClass="WhiteLabel"
                                                                                            meta:resourcekey="chbDeactivePersonSearch_ReportParameters"></asp:Label>
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
                                                                <table style="width: 100%;" class="BoxStyle">
                                                                    <tr>
                                                                        <td>
                                                                            <ComponentArt:ToolBar ID="TlbFilter_PersonalFilter_ReportParameters" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemFilter_TlbFilter_PersonalFilter_ReportParameters"
                                                                                        runat="server" ClientSideCommand="tlbItemFilter_TlbFilter_PersonalFilter_ReportParameters_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="filter.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFilter_TlbFilter_PersonalFilter_ReportParameters"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 15%">
                                                                                        <asp:Label ID="lblName_PersonalFilter_ReportParameters" runat="server" CssClass="WhiteLabel"
                                                                                            meta:resourcekey="lblName_PersonalFilter_ReportParameters" Text="نام :"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 35%">
                                                                                        <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtFirstName_PersonalFilter_ReportParameters" onkeypress="txtFirstName_PersonalFilter_ReportParameters_OnkeyPress(event);" />
                                                                                    </td>
                                                                                    <td style="width: 15%">
                                                                                        <asp:Label ID="lblFamily_PersonalFilter_ReportParameters" runat="server" meta:resourcekey="lblFamily_PersonalFilter_ReportParameters"
                                                                                            Text="نام خانوادگی :" CssClass="WhiteLabel"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 35%">
                                                                                        <input type="text" style="width: 97%;" class="TextBoxes" id="txtLastName_PersonalFilter_ReportParameters" onkeypress="txtLastName_PersonalFilter_ReportParameters_OnkeyPress(event);" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblFatherName_PersonalFilter_ReportParameters" runat="server" meta:resourcekey="lblFatherName_PersonalFilter_ReportParameters"
                                                                                            Text="نام پدر :" CssClass="WhiteLabel"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtFatherName_PersonalFilter_ReportParameters" onkeypress="txtFatherName_PersonalFilter_ReportParameters_OnKeyPress(event); " />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblPersonnelNumber_PersonalFilter_ReportParameters" runat="server"
                                                                                            meta:resourcekey="lblPersonnelNumber_PersonalFilter_ReportParameters" Text="شماره پرسنلی :"
                                                                                            CssClass="WhiteLabel"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtPersonnelNumber_PersonalFilter_ReportParameters" onkeypress="txtPersonnelNumber_PersonalFilter_ReportParameters_OnKeyPress(event);" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCardNumber_PersonalFilter_ReportParameters" runat="server" meta:resourcekey="lblCardID_PersonalFilter_ReportParameters"
                                                                                            Text="شماره کارت :" CssClass="WhiteLabel"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtCardNumber_PersonalFilter_ReportParameters" onkeypress="txtCardNumber_PersonalFilter_ReportParameters_OnKeyPress(event);" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblOrganizationPost_PersonalFilter_ReportParameters" runat="server"
                                                                                            meta:resourcekey="lblOrganizationPost_PersonalFilter_ReportParameters" Text="پست سازمانی :"
                                                                                            CssClass="WhiteLabel"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <ComponentArt:CallBack ID="CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                        runat="server" OnCallback="CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters_onCallBack"
                                                                                                        Height="26">
                                                                                                        <Content>
                                                                                                            <ComponentArt:ComboBox ID="cmbOrganizationPost_PersonalFilter_ReportParameters" runat="server"
                                                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                                DropDownCssClass="comboDropDown" DropDownHeight="190" DropDownResizingMode="Corner"
                                                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                                                TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                                                                                <DropDownContent>
                                                                                                                    <ComponentArt:TreeView ID="trvOrganizationPost_PersonalFilter_ReportParameters" runat="server"
                                                                                                                        CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                                                                        DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                                                                        ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="98%"
                                                                                                                        HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                                                                                        LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                                                                                                        NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                                                                                        ShowLines="true" Width="100%" meta:resourcekey="trvOrganizationPost_PersonalFilter_ReportParameters">
                                                                                                                        <ClientEvents>
                                                                                                                            <NodeSelect EventHandler="trvOrganizationPost_PersonalFilter_ReportParameters_onNodeSelect" />
                                                                                                                            <CallbackComplete EventHandler="trvOrganizationPost_PersonalFilter_ReportParameters_onCallbackComplete" />
                                                                                                                            <NodeBeforeExpand EventHandler="trvOrganizationPost_PersonalFilter_ReportParameters_onNodeBeforeExpand" />
                                                                                                                            <NodeExpand EventHandler="trvOrganizationPost_PersonalFilter_ReportParameters_onNodeExpand" />
                                                                                                                        </ClientEvents>
                                                                                                                    </ComponentArt:TreeView>
                                                                                                                </DropDownContent>
                                                                                                                <ClientEvents>
                                                                                                                    <Expand EventHandler="cmbOrganizationPost_PersonalFilter_ReportParameters_onExpand" />
                                                                                                                </ClientEvents>
                                                                                                            </ComponentArt:ComboBox>
                                                                                                            <asp:HiddenField ID="ErrorHiddenField_OrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                runat="server" />
                                                                                                        </Content>
                                                                                                        <ClientEvents>
                                                                                                            <CallbackComplete EventHandler="CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters_onCallbackComplete" />
                                                                                                            <CallbackError EventHandler="CallBack_cmbOrganizationPost_PersonalFilter_ReportParameters_onCallbackError" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:CallBack>
                                                                                                </td>
                                                                                                <td style="width: 5%">
                                                                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="Refresh_cmbOrganizationPost_PersonalFilter_ReportParameters();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemSearch_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbRefresh_cmbOrganizationPost_PersonalFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
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
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView CssClass="PageContent" runat="server" ID="pgvGroupFilter_TabStripReportParameters"
                                        Visible="true">
                                        <table style="width: 100%; font-family: Arial; font-size: small;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 50%">
                                                                <ComponentArt:ToolBar ID="TlbPersonalFilter_GroupFilter_ReportParameters" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                    UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemReportView_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            runat="server" ClientSideCommand="tlbItemReportView_TlbPersonalFilter_GroupFilter_ReportParameters_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Report.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportView_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemReportViewExcel_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemReportViewExcel_TlbPersonalFilter_GroupFilter_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Excel.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportViewExcel_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                TextImageSpacing="5" />
                                                                         <ComponentArt:ToolBarItem ID="tlbItemDynamicConditions_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            runat="server" ClientSideCommand="tlbItemDynamicConditions_TlbPersonalFilter_GroupFilter_ReportParameters_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="verde.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDynamicConditions_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                                            ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbPersonalFilter_GroupFilter_ReportParameters_onClick();" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbPersonalFilter_GroupFilter_ReportParameters_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            TextImageSpacing="5" />
                                                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            runat="server" ClientSideCommand="tlbItemExit_TlbPersonalFilter_GroupFilter_ReportParameters_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonalFilter_GroupFilter_ReportParameters"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 5%">
                                                                            <input id="chbAllPersonWithThisConditions_ReportParameters" type="checkbox" checked="checked" /></td>
                                                                        <td>
                                                                            <asp:Label ID="lblAllPersonWithThisConditions_ReportParameters" CssClass="WhiteLabel" runat="server" Text="کلیه پرسنل با این شرایط" meta:resourcekey="lblAllPersonWithThisConditions_ReportParameters"></asp:Label>
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
                                                            <td id="headerPersonnelFilter_GroupFilter_ReportParameters" colspan="4" class="HeaderLabel">فیلتر پرسنل
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td meta:resourcekey="InverseAlignObj" colspan="2">
                                                                <table style="width: 100%;" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 3%">
                                                                            <input id="rdbAllPersonnel_GroupFilter_ReportParameters" type="radio"
                                                                                name="PersonnelActiveState" value="1" />
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblAllPersonnel_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblAllPersonnel_GroupFilter_ReportParameters"
                                                                                Text="کل پرسنل" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 3%">
                                                                            <input id="rdbActive_GroupFilter_ReportParameters" type="radio" name="PersonnelActiveState" checked="checked"
                                                                                value="2" />
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblActive_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblActive_GroupFilter_ReportParameters"
                                                                                Text="فعال" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 3%">
                                                                            <input id="rdbDeactive_GroupFilter_ReportParameters" type="radio" name="PersonnelActiveState"
                                                                                value="3" />
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblDeactive_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblDeactive_GroupFilter_ReportParameters"
                                                                                Text="غیر فعال" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 15%" meta:resourcekey="InverseAlignObj">&nbsp;</td>
                                                            <td style="width: 35%">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 15%" meta:resourcekey="InverseAlignObj">
                                                                <asp:Label ID="lblSex_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblSex_GroupFilter_ReportParameters"
                                                                    Text="جنسیت :" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td style="width: 35%">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbSex_GroupFilter_ReportParameters"
                                                                                OnCallback="CallBack_cmbSex_GroupFilter_ReportParameters_onCallBack" Height="26">
                                                                                <Content>
                                                                                    <ComponentArt:ComboBox ID="cmbSex_GroupFilter_ReportParameters" runat="server" AutoComplete="true"
                                                                                        AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                                        DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                        TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                                                        <ClientEvents>
                                                                                            <Expand EventHandler="cmbSex_GroupFilter_ReportParameters_onExpand" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:ComboBox>
                                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Sex_GroupFilter_ReportParameters" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <BeforeCallback EventHandler="CallBack_cmbSex_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                    <CallbackComplete EventHandler="CallBack_cmbSex_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                    <CallbackError EventHandler="CallBack_cmbSex_GroupFilter_ReportParameters_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbClean_cmbSex_GroupFilter_ReportParameters" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbSex_GroupFilter_ReportParameters"
                                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbSex_GroupFilter_ReportParameters_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbSex_GroupFilter_ReportParameters"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 15%" meta:resourcekey="InverseAlignObj">
                                                                <asp:Label ID="lblEducation_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblEducation_GroupFilter_ReportParameters"
                                                                    Text="تحصیلات :" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td style="width: 35%">
                                                                <input type="text" runat="server" style="width: 97%;" class="TextBoxes" id="txtEducation_GroupFilter_ReportParameters" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td meta:resourcekey="InverseAlignObj">
                                                                <asp:Label ID="lblMilitaryState_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblMilitaryState_GroupFilter_ReportParameters"
                                                                    Text="وضعیت نظام وظیفه :" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbMilitaryState_GroupFilter_ReportParameters"
                                                                                OnCallback="CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onCallBack"
                                                                                Height="26">
                                                                                <Content>
                                                                                    <ComponentArt:ComboBox ID="cmbMilitaryState_GroupFilter_ReportParameters" runat="server"
                                                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                        EnableViewState="false" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                        DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                        FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                        ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                                        Style="width: 100%" DropDownHeight="170" TextBoxEnabled="true">
                                                                                        <ClientEvents>
                                                                                            <Expand EventHandler="cmbMilitaryState_GroupFilter_ReportParameters_onExpand" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:ComboBox>
                                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_MilitaryState_GroupFilter_ReportParameters" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <BeforeCallback EventHandler="CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                    <CallbackComplete EventHandler="CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                    <CallbackError EventHandler="CallBack_cmbMilitaryState_GroupFilter_ReportParameters_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbClean_cmbMilitaryState_GroupFilter_ReportParameters"
                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbMilitaryState_GroupFilter_ReportParameters"
                                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbMilitaryState_GroupFilter_ReportParameters_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbMilitaryState_GroupFilter_ReportParameters"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td meta:resourcekey="InverseAlignObj">
                                                                <asp:Label ID="lblMarriageState_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblMarriageState_GroupFilter_ReportParameters"
                                                                    Text="وضعیت تاهل :" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbMarriageState_GroupFilter_ReportParameters"
                                                                                OnCallback="CallBack_cmbMarriageState_GroupFilter_ReportParameters_onCallBack"
                                                                                Height="26">
                                                                                <Content>
                                                                                    <ComponentArt:ComboBox ID="cmbMarriageState_GroupFilter_ReportParameters" runat="server"
                                                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                        TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                                                        <ClientEvents>
                                                                                            <Expand EventHandler="cmbMarriageState_GroupFilter_ReportParameters_onExpand" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:ComboBox>
                                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_MarriageState_GroupFilter_ReportParameters" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <BeforeCallback EventHandler="CallBack_cmbMarriageState_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                    <CallbackComplete EventHandler="CallBack_cmbMarriageState_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                    <CallbackError EventHandler="CallBack_cmbMarriageState_GroupFilter_ReportParameters_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbClean_cmbMarriageState_GroupFilter_ReportParameters"
                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>

                                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbMarriageState_GroupFilter_ReportParameters0"
                                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbMarriageState_GroupFilter_ReportParameters_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbMarriageState_GroupFilter_ReportParameters"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td meta:resourcekey="InverseAlignObj">
                                                                <asp:Label ID="lblGrade_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblGrade_GroupFilter_ReportParameters"
                                                                    Text="رتبه :" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbGrade_GroupFilter_ReportParameters"
                                                                                OnCallback="CallBack_cmbGrade_GroupFilter_ReportParameters_onCallBack"
                                                                                Height="26">
                                                                                <Content>
                                                                                    <ComponentArt:ComboBox ID="cmbGrade_GroupFilter_ReportParameters" runat="server"
                                                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                        TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true" DataTextField="Name" DataValueField="ID">
                                                                                        <ClientEvents>
                                                                                            <Expand EventHandler="cmbGrade_GroupFilter_ReportParameters_onExpand" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:ComboBox>
                                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_Grade_GroupFilter_ReportParameters" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <BeforeCallback EventHandler="CallBack_cmbGrade_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                    <CallbackComplete EventHandler="CallBack_cmbGrade_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                    <CallbackError EventHandler="CallBack_cmbGrade_GroupFilter_ReportParameters_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbClean_cmbGrade_GroupFilter_ReportParameters"
                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>

                                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbGrade_GroupFilter_ReportParameters"
                                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbGrade_GroupFilter_ReportParameters_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbGrade_GroupFilter_ReportParameters"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td meta:resourcekey="InverseAlignObj">
                                                                <asp:Label ID="lblCostCenter_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblCostCenter_GroupFilter_ReportParameters"
                                                                    Text="مرکز هزینه :" CssClass="WhiteLabel"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <ComponentArt:CallBack runat="server" ID="CallBack_cmbCostCenter_GroupFilter_ReportParameters"
                                                                                OnCallback="CallBack_cmbCostCenter_GroupFilter_ReportParameters_onCallBack"
                                                                                Height="26">
                                                                                <Content>
                                                                                    <ComponentArt:ComboBox ID="cmbCostCenter_GroupFilter_ReportParameters" runat="server"
                                                                                        AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                        DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                        DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                        ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                        TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true" DataTextField="Name" DataValueField="ID">
                                                                                        <ClientEvents>
                                                                                            <Expand EventHandler="cmbCostCenter_GroupFilter_ReportParameters_onExpand" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:ComboBox>
                                                                                    <asp:HiddenField runat="server" ID="ErrorHiddenField_CostCenter_GroupFilter_ReportParameters" />
                                                                                </Content>
                                                                                <ClientEvents>
                                                                                    <BeforeCallback EventHandler="CallBack_cmbCostCenter_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                    <CallbackComplete EventHandler="CallBack_cmbCostCenter_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                    <CallbackError EventHandler="CallBack_cmbCostCenter_GroupFilter_ReportParameters_onCallbackError" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:CallBack>
                                                                        </td>
                                                                        <td style="width: 5%">
                                                                            <ComponentArt:ToolBar ID="TlbClean_cmbCostCenter_GroupFilter_ReportParameters"
                                                                                runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>

                                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_cmbCostCenter_GroupFilter_ReportParameters"
                                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_cmbCostCenter_GroupFilter_ReportParameters_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_cmbCostCenter_GroupFilter_ReportParameters"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" valign="top">
                                                                <table style="width: 100%;" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 30%" meta:resourcekey="InverseAlignObj">
                                                                            <asp:Label ID="lblDepartment_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblDepartment_GroupFilter_ReportParameters"
                                                                                Text="بخش :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 100%">
                                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbDepartment_GroupFilter_ReportParameters"
                                                                                            OnCallback="CallBack_cmbDepartment_GroupFilter_ReportParameters_onCallBack" Height="26" Width="100%">
                                                                                            <Content>

                                                                                                <ComponentArt:ComboBox ID="cmbDepartment_GroupFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                    DropDownCssClass="comboDropDown" DropDownHeight="190" DropDownResizingMode="Corner"
                                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                                                    Width="100%" ExpandDirection="Down" TextBoxEnabled="true">
                                                                                                    <DropDownContent>
                                                                                                        <ComponentArt:TreeView ID="trvDepartment_GroupFilter_ReportParameters" runat="server"
                                                                                                            CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                                                            DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false" ExpandCollapseImageHeight="15"
                                                                                                            ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" Height="98%"
                                                                                                            HoverNodeCssClass="HoverTreeNode" ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20"
                                                                                                            LineImagesFolderUrl="Images/TreeView/LeftLines" LineImageWidth="19" NodeCssClass="TreeNode"
                                                                                                            NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                                                                            ShowLines="true" Width="100%" meta:resourcekey="trvDepartment_GroupFilter_ReportParameters">
                                                                                                            <ClientEvents>
                                                                                                                <NodeCheckChange EventHandler="trvDepartment_GroupFilter_ReportParameters_onNodeCheckChange" />
                                                                                                                <NodeSelect EventHandler="trvDepartment_GroupFilter_ReportParameters_onNodeSelect" />
                                                                                                                <NodeExpand EventHandler="trvDepartment_GroupFilter_ReportParameters_onNodeExpand" />
                                                                                                            </ClientEvents>
                                                                                                        </ComponentArt:TreeView>
                                                                                                    </DropDownContent>
                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbDepartment_GroupFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Department_GroupFilter_ReportParameters" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbDepartment_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbDepartment_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbDepartment_GroupFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 50%">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 5%"> 
                                                                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="Refresh_cmbDepartment_GroupFilter_ReportParameters();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemSearch_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbRefresh_cmbDepartment_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                        </Items>
                                                                                                    </ComponentArt:ToolBar>
                                                                                                </td>
                                                                                                <td style="width: 85%">
                                                                                                     <table style="width: 100%;">
                                                                                                        <tr>
                                                                                                           <td>
                                                                                                                <input id="chbSubDepartment_GroupFilter_ReportParameters" type="checkbox" />
                                                                                                            </td>
                                                                                                             <td meta:resourcekey="InverseAlignObj">
                                                                                                                <asp:Label ID="lblUnderDepartment_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblUnderDepartment_GroupFilter_ReportParameters"
                                                                                                                    Text="زیر بخش" CssClass="WhiteLabel"></asp:Label>
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
                                                                </table>
                                                            </td>
                                                            <td colspan="2">
                                                                <table style="width: 100%;" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 30%" meta:resourcekey="InverseAlignObj">
                                                                            <asp:Label ID="lblWorkGroup_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblWorkGroup_GroupFilter_ReportParameters"
                                                                                Text="گروه کاری :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbWorkGroup_GroupFilter_ReportParameters"
                                                                                            OnCallback="CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onCallBack" Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbWorkGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                                    TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" DataTextField="Name"
                                                                                                    DataValueField="ID" TextBoxEnabled="true">
                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbWorkGroup_GroupFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkGroup_GroupFilter_ReportParameters" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbWorkGroup_GroupFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters" runat="server"
                                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="Refresh_cmbWorkGroup_GroupFilter_ReportParameters();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbWorkGroup_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td meta:resourcekey="InverseAlignObj">
                                                                            <asp:Label ID="lblFromDate_WorkGroup_GroupFilter_ReportParameters" runat="server"
                                                                                meta:resourcekey="lblFromDate_WorkGroup_GroupFilter_ReportParameters" Text="از تاریخ :"
                                                                                CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td id="Container_WorkGroupCalendars_GroupFilter_ReportParameters" style="width: 60%">
                                                                                        <table runat="server" id="Container_pdpWorkGroup_GroupFilter_ReportParameters" visible="false"
                                                                                            style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <pcal:PersianDatePickup ID="pdpWorkGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table runat="server" id="Container_gdpWorkGroup_GroupFilter_ReportParameters" visible="false"
                                                                                            style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table id="Container_gCalWorkGroup_GroupFilter_ReportParameters" border="0" cellpadding="0"
                                                                                                        cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td onmouseup="btn_gdpWorkGroup_GroupFilter_ReportParameters_OnMouseUp(event)">
                                                                                                                <ComponentArt:Calendar ID="gdpWorkGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                                                    PickerFormat="Custom">
                                                                                                                    <ClientEvents>
                                                                                                                        <SelectionChanged EventHandler="gdpWorkGroup_GroupFilter_ReportParameters_OnDateChange" />
                                                                                                                    </ClientEvents>
                                                                                                                </ComponentArt:Calendar>
                                                                                                            </td>
                                                                                                            <td style="font-size: 10px;">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img id="btn_gdpWorkGroup_GroupFilter_ReportParameters" alt="" class="calendar_button"
                                                                                                                    onclick="btn_gdpWorkGroup_GroupFilter_ReportParameters_OnClick(event)" onmouseup="btn_gdpWorkGroup_GroupFilter_ReportParameters_OnMouseUp(event)"
                                                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    <ComponentArt:Calendar ID="gCalWorkGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                                        ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                                        PopUpExpandControlId="btn_gdpWorkGroup_GroupFilter_ReportParameters" PrevImageUrl="cal_prevMonth.gif"
                                                                                                        SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                                                        <ClientEvents>
                                                                                                            <SelectionChanged EventHandler="gCalWorkGroup_GroupFilter_ReportParameters_OnChange" />
                                                                                                            <Load EventHandler="gCalWorkGroup_GroupFilter_ReportParameters_onLoad" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:Calendar>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <ComponentArt:ToolBar ID="TlbClean_WorkGroupCalendars_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_WorkGroupCalendars_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="TlbItemClean_TlbClean_WorkGroupCalendars_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="TlbItemClean_TlbClean_WorkGroupCalendars_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
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
                                                        <tr>
                                                            <td colspan="2">
                                                                <table class="BoxStyle" style="width: 100%;">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td meta:resourcekey="InverseAlignObj" style="width: 30%">
                                                                                        <asp:Label ID="lblRuleGroup_GroupFilter_ReportParameters" runat="server" CssClass="WhiteLabel"
                                                                                            meta:resourcekey="lblRuleGroup_GroupFilter_ReportParameters" Text="گروه قانون :"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <ComponentArt:CallBack ID="CallBack_cmbRuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        OnCallback="CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onCallBack" Height="26">
                                                                                                        <Content>
                                                                                                            <ComponentArt:ComboBox ID="cmbRuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                                DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                                                TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                                                                                <ClientEvents>
                                                                                                                    <Expand EventHandler="cmbRuleGroup_GroupFilter_ReportParameters_onExpand" />
                                                                                                                </ClientEvents>
                                                                                                            </ComponentArt:ComboBox>
                                                                                                            <asp:HiddenField ID="ErrorHiddenField_RuleGroup_GroupFilter_ReportParameters" runat="server" />
                                                                                                        </Content>
                                                                                                        <ClientEvents>
                                                                                                            <BeforeCallback EventHandler="CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                            <CallbackComplete EventHandler="CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                            <CallbackError EventHandler="CallBack_cmbRuleGroup_GroupFilter_ReportParameters_onCallbackError" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:CallBack>
                                                                                                </td>
                                                                                                <td style="width: 5%">
                                                                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="Refresh_cmbRuleGroup_GroupFilter_ReportParameters();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbRuleGroup_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
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
                                                                    <tr>
                                                                        <td meta:resourcekey="InverseAlignObj" style="width: 30%">
                                                                            <asp:Label ID="lblFromDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                CssClass="WhiteLabel" meta:resourcekey="lblFromDate_RuleGroup_GroupFilter_ReportParameters"
                                                                                Text="از تاریخ :"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td id="Container_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters" style="width: 60%">
                                                                                        <table runat="server" id="Container_pdpFromDate_RuleGroup_GroupFilter_ReportParameters"
                                                                                            visible="false" style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <pcal:PersianDatePickup ID="pdpFromDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table runat="server" id="Container_gdpFromDate_RuleGroup_GroupFilter_ReportParameters"
                                                                                            visible="false" style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table id="Container_gCalFromDate_RuleGroup_GroupFilter_ReportParameters" border="0"
                                                                                                        cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td onmouseup="btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnMouseUp(event)">
                                                                                                                <ComponentArt:Calendar ID="gdpFromDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                                    ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                                                    MaxDate="2122-1-1" PickerFormat="Custom">
                                                                                                                    <ClientEvents>
                                                                                                                        <SelectionChanged EventHandler="gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnDateChange" />
                                                                                                                    </ClientEvents>
                                                                                                                </ComponentArt:Calendar>
                                                                                                            </td>
                                                                                                            <td style="font-size: 10px;">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img id="btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters" alt="" class="calendar_button"
                                                                                                                    onclick="btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnClick(event)"
                                                                                                                    onmouseup="btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters_OnMouseUp(event)"
                                                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    <ComponentArt:Calendar ID="gCalFromDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                                        ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                                        PopUpExpandControlId="btn_gdpFromDate_RuleGroup_GroupFilter_ReportParameters"
                                                                                                        PrevImageUrl="cal_prevMonth.gif" SelectedDayCssClass="selectedday" SwapDuration="300"
                                                                                                        SwapSlide="Linear" VisibleDate="2008-1-1" MaxDate="2122-1-1">
                                                                                                        <ClientEvents>
                                                                                                            <SelectionChanged EventHandler="gCalFromDate_RuleGroup_GroupFilter_ReportParameters_OnChange" />
                                                                                                            <Load EventHandler="gCalFromDate_RuleGroup_GroupFilter_ReportParameters_onLoad" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:Calendar>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <ComponentArt:ToolBar ID="TlbClean_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_FromDateCalendars_RuleGroup_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td id="Td2" runat="server" meta:resourcekey="AlignObj">
                                                                            <asp:Label ID="lblToDate_RuleGroup_GroupFilter_ReportParameters" runat="server" CssClass="WhiteLabel"
                                                                                meta:resourcekey="lblToDate_RuleGroup_GroupFilter_ReportParameters" Text="تا تاریخ :"></asp:Label>
                                                                        </td>
                                                                        <td id="Td3" runat="server" meta:resourcekey="AlignObj">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td id="Container_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters" style="width: 60%">
                                                                                        <table runat="server" id="Container_pdpToDate_RuleGroup_GroupFilter_ReportParameters"
                                                                                            visible="false" style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <pcal:PersianDatePickup ID="pdpToDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table runat="server" id="Container_gdpToDate_RuleGroup_GroupFilter_ReportParameters"
                                                                                            visible="false" style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table id="Container_gCalToDate_RuleGroup_GroupFilter_ReportParameters" border="0"
                                                                                                        cellpadding="0" cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td onmouseup="btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnMouseUp(event)">
                                                                                                                <ComponentArt:Calendar ID="gdpToDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                                    ControlType="Picker" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                                                    MaxDate="2122-1-1" PickerFormat="Custom">
                                                                                                                    <ClientEvents>
                                                                                                                        <SelectionChanged EventHandler="gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnDateChange" />
                                                                                                                    </ClientEvents>
                                                                                                                </ComponentArt:Calendar>
                                                                                                            </td>
                                                                                                            <td style="font-size: 10px;">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img id="btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters" alt="" class="calendar_button"
                                                                                                                    onclick="btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnClick(event)"
                                                                                                                    onmouseup="btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters_OnMouseUp(event)"
                                                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    <ComponentArt:Calendar ID="gCalToDate_RuleGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                                        ImagesBaseUrl="Images/Calendar" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                                        PopUpExpandControlId="btn_gdpToDate_RuleGroup_GroupFilter_ReportParameters" PrevImageUrl="cal_prevMonth.gif"
                                                                                                        SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1"
                                                                                                        MaxDate="2122-1-1">
                                                                                                        <ClientEvents>
                                                                                                            <SelectionChanged EventHandler="gCalToDate_RuleGroup_GroupFilter_ReportParameters_OnChange" />
                                                                                                            <Load EventHandler="gCalToDate_RuleGroup_GroupFilter_ReportParameters_onLoad" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:Calendar>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <ComponentArt:ToolBar ID="TlbClean_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_ToDateCalendars_RuleGroup_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td colspan="2" valign="top">
                                                                <table style="width: 100%;" class="BoxStyle">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td id="Td4" style="width: 30%" runat="server">
                                                                                        <asp:Label ID="lblCalculationRange_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblCalculationRange_GroupFilter_ReportParameters"
                                                                                            Text="محدوده محاسبات :" CssClass="WhiteLabel"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbCalculationRange_GroupFilter_ReportParameters"
                                                                                                        OnCallback="CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onCallBack"
                                                                                                        Height="26">
                                                                                                        <Content>
                                                                                                            <ComponentArt:ComboBox ID="cmbCalculationRange_GroupFilter_ReportParameters" runat="server"
                                                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                                DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                                                TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Down" DataTextField="Name"
                                                                                                                DropDownHeight="100" DataValueField="ID" TextBoxEnabled="true">
                                                                                                                <ClientEvents>
                                                                                                                    <Expand EventHandler="cmbCalculationRange_GroupFilter_ReportParameters_onExpand" />
                                                                                                                </ClientEvents>
                                                                                                            </ComponentArt:ComboBox>
                                                                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_CalculationRange_GroupFilter_ReportParameters" />
                                                                                                        </Content>
                                                                                                        <ClientEvents>
                                                                                                            <BeforeCallback EventHandler="CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                            <CallbackComplete EventHandler="CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                            <CallbackError EventHandler="CallBack_cmbCalculationRange_GroupFilter_ReportParameters_onCallbackError" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:CallBack>
                                                                                                </td>
                                                                                                <td style="width: 5%">
                                                                                                    <ComponentArt:ToolBar ID="TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters"
                                                                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="Refresh_cmbCalculationRange_GroupFilter_ReportParameters();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbCalculationRange_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
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
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblRunfromDate_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblRunfromDate_GroupFilter_ReportParameters"
                                                                                Text="اجرا از تاریخ :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td id="Container_RunFromDateCalndars_GroupFilter_ReportParameters" style="width: 61%">
                                                                                        <table runat="server" id="Container_pdpRunFromDate_GroupFilter_ReportParameters"
                                                                                            visible="false" style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <pcal:PersianDatePickup ID="pdpRunFromDate_GroupFilter_ReportParameters" runat="server"
                                                                                                        CssClass="PersianDatePicker" ReadOnly="true"></pcal:PersianDatePickup>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <table runat="server" id="Container_gdpRunFromDate_GroupFilter_ReportParameters"
                                                                                            visible="false" style="width: 100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table id="Container_gCalRunFromDate_GroupFilter_ReportParameters" border="0" cellpadding="0"
                                                                                                        cellspacing="0">
                                                                                                        <tr>
                                                                                                            <td onmouseup="btn_gdpRunFromDate_GroupFilter_ReportParameters_OnMouseUp(event)">
                                                                                                                <ComponentArt:Calendar ID="gdpRunFromDate_GroupFilter_ReportParameters" runat="server"
                                                                                                                    ControlType="Picker" MaxDate="2122-1-1" PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd"
                                                                                                                    PickerFormat="Custom">
                                                                                                                    <ClientEvents>
                                                                                                                        <SelectionChanged EventHandler="gdpRunFromDate_GroupFilter_ReportParameters_OnDateChange" />
                                                                                                                    </ClientEvents>
                                                                                                                </ComponentArt:Calendar>
                                                                                                            </td>
                                                                                                            <td style="font-size: 10px;">&nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <img id="btn_gdpRunFromDate_GroupFilter_ReportParameters" alt="" class="calendar_button"
                                                                                                                    onclick="btn_gdpRunFromDate_GroupFilter_ReportParameters_OnClick(event)" onmouseup="btn_gdpRunFromDate_GroupFilter_ReportParameters_OnMouseUp(event)"
                                                                                                                    src="Images/Calendar/btn_calendar.gif" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                    <ComponentArt:Calendar ID="gCalRunFromDate_GroupFilter_ReportParameters" runat="server"
                                                                                                        AllowMonthSelection="false" AllowMultipleSelection="false" AllowWeekSelection="false"
                                                                                                        CalendarCssClass="calendar" CalendarTitleCssClass="title" ControlType="Calendar"
                                                                                                        DayCssClass="day" DayHeaderCssClass="dayheader" DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters"
                                                                                                        ImagesBaseUrl="Images/Calendar" MaxDate="2122-1-1" MonthCssClass="month" NextImageUrl="cal_nextMonth.gif"
                                                                                                        NextPrevCssClass="nextprev" OtherMonthDayCssClass="othermonthday" PopUp="Custom"
                                                                                                        PopUpExpandControlId="btn_gdpRunFromDate_GroupFilter_ReportParameters" PrevImageUrl="cal_prevMonth.gif"
                                                                                                        SelectedDayCssClass="selectedday" SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                                                        <ClientEvents>
                                                                                                            <SelectionChanged EventHandler="gCalRunFromDate_GroupFilter_ReportParameters_OnChange" />
                                                                                                            <Load EventHandler="gCalRunFromDate_GroupFilter_ReportParameters_onLoad" />
                                                                                                        </ClientEvents>
                                                                                                    </ComponentArt:Calendar>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td valign="top">
                                                                                        <ComponentArt:ToolBar ID="TlbClean_CalculationRangeCalendars_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_CalculationRangeCalendars_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_CalculationRangeCalendars_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_CalculationRangeCalendars_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
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
                                                        <tr>
                                                            <td colspan="2">
                                                                <table style="width: 100%" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblControlStation_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblControlStation_GroupFilter_ReportParameters"
                                                                                Text="ایستگاه کنترل :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbControlStation_GroupFilter_ReportParameters"
                                                                                            OnCallback="CallBack_cmbControlStation_GroupFilter_ReportParameters_onCallBack"
                                                                                            Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbControlStation_GroupFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                    DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                                    DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                    ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                                    TextBoxCssClass="comboTextBox" Width="100%" ExpandDirection="Up" DataTextField="Name"
                                                                                                    DataValueField="ID" TextBoxEnabled="true">
                                                                                                    <DropDownContent>
                                                                                                        <ComponentArt:TreeView ID="trvControlStation_GroupFilter_ReportParameters" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                                                                            CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                                                            EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                                                            ExpandImageUrl="images/TreeView/col.gif" Height="98%" HoverNodeCssClass="HoverTreeNode"
                                                                                                            ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" meta:resourcekey="trvControlStation_GroupFilter_ReportParameters"
                                                                                                            LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                                                                            NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true"
                                                                                                            Width="100%">
                                                                                                            <ClientEvents>
                                                                                                                <NodeCheckChange EventHandler="trvControlStation_GroupFilter_ReportParameters_onNodeCheckChange" />
                                                                                                                <NodeExpand EventHandler="trvControlStation_GroupFilter_ReportParameters_onNodeExpand" />
                                                                                                            </ClientEvents>
                                                                                                        </ComponentArt:TreeView>
                                                                                                    </DropDownContent>
                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbControlStation_GroupFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ControlStation_GroupFilter_ReportParameters" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbControlStation_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbControlStation_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbControlStation_GroupFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbControlStation_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbControlStation_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="Refresh_cmbControlStation_GroupFilter_ReportParameters();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbControlStation_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbControlStation_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbControlStation_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbControlStation_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td colspan="2">
                                                                <table style="width: 100%" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblEmployType_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblEmployType_GroupFilter_ReportParameters"
                                                                                Text="نوع استخدام :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbEmployType_GroupFilter_ReportParameters"
                                                                                            OnCallback="CallBack_cmbEmployType_GroupFilter_ReportParameters_onCallBack" Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbEmployType_GroupFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                    DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                                                    Width="100%" ExpandDirection="Up" TextBoxEnabled="true" DropDownHeight="150">
                                                                                                    <DropDownContent>
                                                                                                        <ComponentArt:TreeView ID="trvEmployType_GroupFilter_ReportParameters" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                                                                            CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                                                            EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                                                            ExpandImageUrl="images/TreeView/col.gif" HoverNodeCssClass="HoverTreeNode"
                                                                                                            ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" meta:resourcekey="trvEmployType_GroupFilter_ReportParameters"
                                                                                                            LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                                                                            NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" AutoScroll="false"
                                                                                                            Width="100%">
                                                                                                            <ClientEvents>
                                                                                                                <NodeCheckChange EventHandler="trvEmployType_GroupFilter_ReportParameters_onNodeCheckChange" />
                                                                                                                <NodeExpand EventHandler="trvEmployType_GroupFilter_ReportParameters_onNodeExpand" />
                                                                                                            </ClientEvents>
                                                                                                        </ComponentArt:TreeView>
                                                                                                    </DropDownContent>
                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbEmployType_GroupFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_EmployType_GroupFilter_ReportParameters" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbEmployType_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbEmployType_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbEmployType_GroupFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbEmployType_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbEmployType_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="Refresh_cmbEmployType_GroupFilter_ReportParameters();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbEmployType_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbEmployType_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbEmployType_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbEmployType_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
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
                                                        <tr>
                                                             <td colspan="2">
                                                                <table style="width: 100%" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblUIValidationGroup_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblUIValidationGroup_GroupFilter_ReportParameters"
                                                                                Text="گروه واسط کاربری :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbUIValidationGroup_GroupFilter_ReportParameters"
                                                                                            OnCallback="CallBack_cmbUIValidationGroup_GroupFilter_ReportParameters_onCallBack" Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbUIValidationGroup_GroupFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                    DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                                                    Width="100%" ExpandDirection="Up" TextBoxEnabled="true" DropDownHeight="150">
                                                                                                    <DropDownContent>
                                                                                                        <ComponentArt:TreeView ID="trvUIValidationGroup_GroupFilter_ReportParameters" runat="server" CollapseImageUrl="images/TreeView/exp.gif"
                                                                                                            CssClass="TreeView" DefaultImageHeight="16" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                                                                            EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                                                                            ExpandImageUrl="images/TreeView/col.gif" HoverNodeCssClass="HoverTreeNode"
                                                                                                            ItemSpacing="2" KeyboardEnabled="true" LineImageHeight="20" meta:resourcekey="trvUIValidationGroup_GroupFilter_ReportParameters"
                                                                                                            LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17"
                                                                                                            NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" AutoScroll="false"
                                                                                                            Width="100%">
                                                                                                            <ClientEvents>
                                                                                                                <NodeCheckChange EventHandler="trvUIValidationGroup_GroupFilter_ReportParameters_onNodeCheckChange" />
                                                                                                                <NodeExpand EventHandler="trvUIValidationGroup_GroupFilter_ReportParameters_onNodeExpand" />
                                                                                                            </ClientEvents>
                                                                                                        </ComponentArt:TreeView>
                                                                                                    </DropDownContent>
                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbUIValidationGroup_GroupFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_UIValidationGroup_GroupFilter_ReportParameters" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbUIValidationGroup_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbUIValidationGroup_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbUIValidationGroup_GroupFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbUIValidationGroup_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td colspan="2">
                                                                <table style="width: 100%" class="BoxStyle">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblGroupingReport_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblGroupingReport_GroupFilter_ReportParameters"
                                                                                Text="گروه بندی :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbGroupingReport_GroupFilter_ReportParameters"
                                                                                            OnCallback="CallBack_cmbGroupingReport_GroupFilter_ReportParameters_onCallBack" Height="26">
                                                                                            <Content>
                                                                                                <ComponentArt:ComboBox ID="cmbGroupingReport_GroupFilter_ReportParameters" runat="server"
                                                                                                    AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                    DataTextField="Name" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox"
                                                                                                    Width="100%" ExpandDirection="Up" TextBoxEnabled="true">

                                                                                                    <ClientEvents>
                                                                                                        <Expand EventHandler="cmbGroupingReport_GroupFilter_ReportParameters_onExpand" />
                                                                                                    </ClientEvents>
                                                                                                </ComponentArt:ComboBox>
                                                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GroupingReport_GroupFilter_ReportParameters" />
                                                                                            </Content>
                                                                                            <ClientEvents>
                                                                                                <BeforeCallback EventHandler="CallBack_cmbGroupingReport_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                <CallbackComplete EventHandler="CallBack_cmbGroupingReport_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                <CallbackError EventHandler="CallBack_cmbGroupingReport_GroupFilter_ReportParameters_onCallbackError" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:CallBack>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        <ComponentArt:ToolBar ID="TlbRefresh_cmbGroupingReport_GroupFilter_ReportParameters"
                                                                                            runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbGroupingReport_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="Refresh_cmbGroupingReport_GroupFilter_ReportParameters();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbGroupingReport_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbGroupingReport_GroupFilter_ReportParameterss"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbGroupingReport_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbGroupingReport_GroupFilter_ReportParameterss"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </ComponentArt:ToolBar>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="visibility: visible;">
                                                                        <td style="width: 30%">
                                                                            <asp:Label ID="lblGroupingReportByNewPage_GroupFilter_ReportParameters" runat="server" meta:resourcekey="lblGroupingReportByNewPage_GroupFilter_ReportParameters"
                                                                                Text="نمایش در صفحه جدید :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <input id="chbGroupingByNewPage_GroupFilter_ReportParameters" type="checkbox" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td colspan="2">
                                                                 <table class="BoxStyle" style="width: 100%;">
            <tr>
                <td colspan="2">
                    <table style="width: 100%;">
                        <tr>
                            <td meta:resourcekey="InverseAlignObj" style="width: 30%">
                                <asp:Label ID="lblContract_GroupFilter_ReportParameters" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblContract_GroupFilter_ReportParameters" Text="قرارداد :"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <componentart:callback id="CallBack_cmbContract_GroupFilter_ReportParameters" runat="server"
                                                oncallback="CallBack_cmbContract_GroupFilter_ReportParameters_onCallBack" height="26">
                                                                                                        <Content>
                                                                                                            <ComponentArt:ComboBox ID="cmbContract_GroupFilter_ReportParameters" runat="server"
                                                                                                                AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox"
                                                                                                                DataTextField="Title" DataValueField="ID" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                                                ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                                                TextBoxCssClass="comboTextBox" Width="100%" TextBoxEnabled="true">
                                                                                                                <ClientEvents>
                                                                                                                    <Expand EventHandler="cmbContract_GroupFilter_ReportParameters_onExpand" />
                                                                                                                </ClientEvents>
                                                                                                            </ComponentArt:ComboBox>
                                                                                                            <asp:HiddenField ID="ErrorHiddenField_Contract_GroupFilter_ReportParameters" runat="server" />
                                                                                                        </Content>
                                                                                                        <ClientEvents>
                                                                                                            <BeforeCallback EventHandler="CallBack_cmbContract_GroupFilter_ReportParameters_onBeforeCallback" />
                                                                                                            <CallbackComplete EventHandler="CallBack_cmbContract_GroupFilter_ReportParameters_onCallbackComplete" />
                                                                                                            <CallbackError EventHandler="CallBack_cmbContract_GroupFilter_ReportParameters_onCallbackError" />
                                                                                                        </ClientEvents>
                                                                                                    </componentart:callback>
                                        </td>
                                        <td style="width: 5%">
                                            <componentart:toolbar id="TlbRefresh_cmbContract_GroupFilter_ReportParameters" runat="server"
                                                cssclass="toolbar" defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                                defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                                defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                                imagesbaseurl="images/ToolBar/" itemspacing="1px" usefadeeffect="false">
                                                                                                        <Items>
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_cmbContract_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="Refresh_cmbContract_GroupFilter_ReportParameters();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_cmbContract_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                            <ComponentArt:ToolBarItem ID="tlbItemClean_TlbRefresh_cmbContract_GroupFilter_ReportParameters"
                                                                                                                runat="server" ClientSideCommand="tlbItemClean_TlbRefresh_cmbContract_GroupFilter_ReportParameters_onClick();"
                                                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbRefresh_cmbContract_GroupFilter_ReportParameters"
                                                                                                                TextImageSpacing="5" />
                                                                                                        </Items>
                                                                                                    </componentart:toolbar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td meta:resourcekey="InverseAlignObj" style="width: 30%">
                    <asp:Label ID="lblFromDate_Contract_GroupFilter_ReportParameters" runat="server"
                        CssClass="WhiteLabel" meta:resourcekey="lblFromDate_Contract_GroupFilter_ReportParameters"
                        Text="از تاریخ :"></asp:Label>
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td id="Container_FromDateCalendars_Contract_GroupFilter_ReportParameters" style="width: 60%">
                                <table runat="server" id="Container_pdpFromDate_Contract_GroupFilter_ReportParameters"
                                    visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <pcal:persiandatepickup id="pdpFromDate_Contract_GroupFilter_ReportParameters" runat="server"
                                                cssclass="PersianDatePicker" readonly="true"></pcal:persiandatepickup>
                                        </td>
                                    </tr>
                                </table>
                                <table runat="server" id="Container_gdpFromDate_Contract_GroupFilter_ReportParameters"
                                    visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <table id="Container_gCalFromDate_Contract_GroupFilter_ReportParameters" border="0"
                                                cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td onmouseup="btn_gdpFromDate_Contract_GroupFilter_ReportParameters_OnMouseUp(event)">
                                                        <componentart:calendar id="gdpFromDate_Contract_GroupFilter_ReportParameters" runat="server"
                                                            controltype="Picker" pickercssclass="picker" pickercustomformat="yyyy/MM/dd"
                                                            maxdate="2122-1-1" pickerformat="Custom">
                                                                                                                    <ClientEvents>
                                                                                                                        <SelectionChanged EventHandler="gdpFromDate_Contract_GroupFilter_ReportParameters_OnDateChange" />
                                                                                                                    </ClientEvents>
                                                                                                                </componentart:calendar>
                                                    </td>
                                                    <td style="font-size: 10px;">&nbsp;
                                                    </td>
                                                    <td>
                                                        <img id="btn_gdpFromDate_Contract_GroupFilter_ReportParameters" alt="" class="calendar_button"
                                                            onclick="btn_gdpFromDate_Contract_GroupFilter_ReportParameters_OnClick(event)"
                                                            onmouseup="btn_gdpFromDate_Contract_GroupFilter_ReportParameters_OnMouseUp(event)"
                                                            src="Images/Calendar/btn_calendar.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <componentart:calendar id="gCalFromDate_Contract_GroupFilter_ReportParameters" runat="server"
                                                allowmonthselection="false" allowmultipleselection="false" allowweekselection="false"
                                                calendarcssclass="calendar" calendartitlecssclass="title" controltype="Calendar"
                                                daycssclass="day" dayheadercssclass="dayheader" dayhovercssclass="dayhover" daynameformat="FirstTwoLetters"
                                                imagesbaseurl="Images/Calendar" monthcssclass="month" nextimageurl="cal_nextMonth.gif"
                                                nextprevcssclass="nextprev" othermonthdaycssclass="othermonthday" popup="Custom"
                                                popupexpandcontrolid="btn_gdpFromDate_Contract_GroupFilter_ReportParameters"
                                                previmageurl="cal_prevMonth.gif" selecteddaycssclass="selectedday" swapduration="300"
                                                swapslide="Linear" visibledate="2008-1-1" maxdate="2122-1-1">
                                                                                                        <ClientEvents>
                                                                                                            <SelectionChanged EventHandler="gCalFromDate_Contract_GroupFilter_ReportParameters_OnChange" />
                                                                                                            <Load EventHandler="gCalFromDate_Contract_GroupFilter_ReportParameters_onLoad" />
                                                                                                        </ClientEvents>
                                                                                                    </componentart:calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <componentart:toolbar id="TlbClean_FromDateCalendars_Contract_GroupFilter_ReportParameters"
                                    runat="server" cssclass="toolbar" defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                    defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                    defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                    imagesbaseurl="images/ToolBar/" itemspacing="1px" usefadeeffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_FromDateCalendars_Contract_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_FromDateCalendars_Contract_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_FromDateCalendars_Contract_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </componentart:toolbar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="Td6" runat="server" meta:resourcekey="AlignObj">
                    <asp:Label ID="lblToDate_Contract_GroupFilter_ReportParameters" runat="server" CssClass="WhiteLabel"
                        meta:resourcekey="lblToDate_Contract_GroupFilter_ReportParameters" Text="تا تاریخ :"></asp:Label>
                </td>
                <td id="Td7" runat="server" meta:resourcekey="AlignObj">
                    <table style="width: 100%;">
                        <tr>
                            <td id="Container_ToDateCalendars_Contract_GroupFilter_ReportParameters" style="width: 60%">
                                <table runat="server" id="Container_pdpToDate_Contract_GroupFilter_ReportParameters"
                                    visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <pcal:persiandatepickup id="pdpToDate_Contract_GroupFilter_ReportParameters" runat="server"
                                                cssclass="PersianDatePicker" readonly="true"></pcal:persiandatepickup>
                                        </td>
                                    </tr>
                                </table>
                                <table runat="server" id="Container_gdpToDate_Contract_GroupFilter_ReportParameters"
                                    visible="false" style="width: 100%">
                                    <tr>
                                        <td>
                                            <table id="Container_gCalToDate_Contract_GroupFilter_ReportParameters" border="0"
                                                cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td onmouseup="btn_gdpToDate_Contract_GroupFilter_ReportParameters_OnMouseUp(event)">
                                                        <componentart:calendar id="gdpToDate_Contract_GroupFilter_ReportParameters" runat="server"
                                                            controltype="Picker" pickercssclass="picker" pickercustomformat="yyyy/MM/dd"
                                                            maxdate="2122-1-1" pickerformat="Custom">
                                                                                                                    <ClientEvents>
                                                                                                                        <SelectionChanged EventHandler="gdpToDate_Contract_GroupFilter_ReportParameters_OnDateChange" />
                                                                                                                    </ClientEvents>
                                                                                                                </componentart:calendar>
                                                    </td>
                                                    <td style="font-size: 10px;">&nbsp;
                                                    </td>
                                                    <td>
                                                        <img id="btn_gdpToDate_Contract_GroupFilter_ReportParameters" alt="" class="calendar_button"
                                                            onclick="btn_gdpToDate_Contract_GroupFilter_ReportParameters_OnClick(event)"
                                                            onmouseup="btn_gdpToDate_Contract_GroupFilter_ReportParameters_OnMouseUp(event)"
                                                            src="Images/Calendar/btn_calendar.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <componentart:calendar id="gCalToDate_Contract_GroupFilter_ReportParameters" runat="server"
                                                allowmonthselection="false" allowmultipleselection="false" allowweekselection="false"
                                                calendarcssclass="calendar" calendartitlecssclass="title" controltype="Calendar"
                                                daycssclass="day" dayheadercssclass="dayheader" dayhovercssclass="dayhover" daynameformat="FirstTwoLetters"
                                                imagesbaseurl="Images/Calendar" monthcssclass="month" nextimageurl="cal_nextMonth.gif"
                                                nextprevcssclass="nextprev" othermonthdaycssclass="othermonthday" popup="Custom"
                                                popupexpandcontrolid="btn_gdpToDate_Contract_GroupFilter_ReportParameters" previmageurl="cal_prevMonth.gif"
                                                selecteddaycssclass="selectedday" swapduration="300" swapslide="Linear" visibledate="2008-1-1"
                                                maxdate="2122-1-1">
                                                                                                        <ClientEvents>
                                                                                                            <SelectionChanged EventHandler="gCalToDate_Contract_GroupFilter_ReportParameters_OnChange" />
                                                                                                            <Load EventHandler="gCalToDate_Contract_GroupFilter_ReportParameters_onLoad" />
                                                                                                        </ClientEvents>
                                                                                                    </componentart:calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <componentart:toolbar id="TlbClean_ToDateCalendars_Contract_GroupFilter_ReportParameters"
                                    runat="server" cssclass="toolbar" defaultitemactivecssclass="itemActive" defaultitemcheckedcssclass="itemChecked"
                                    defaultitemcheckedhovercssclass="itemActive" defaultitemcssclass="item" defaultitemhovercssclass="itemHover"
                                    defaultitemimageheight="16px" defaultitemimagewidth="16px" defaultitemtextimagerelation="ImageBeforeText"
                                    imagesbaseurl="images/ToolBar/" itemspacing="1px" usefadeeffect="false">
                                                                                            <Items>
                                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_ToDateCalendars_Contract_GroupFilter_ReportParameters"
                                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_ToDateCalendars_Contract_GroupFilter_ReportParameters_onClick();"
                                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_ToDateCalendars_Contract_GroupFilter_ReportParameters"
                                                                                                    TextImageSpacing="5" />
                                                                                            </Items>
                                                                                        </componentart:toolbar>
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
                                        </table>
                                    </ComponentArt:PageView>
                                </ComponentArt:MultiPage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="ReportBox"  class="BoxStyle" style="width: 960px; height: 100%">
                        <tr>
                            <td id="headerReportParameters_ReportParameters" class="HeaderLabel" colspan="2">پارامترهای گزارش
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%" valign="top">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <ComponentArt:ToolBar ID="TlbReportParameters_ReportParameters" runat="server" CssClass="toolbar"
                                                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                            UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbReportParameters_ReportParameters" runat="server"
                                                                    ClientSideCommand="tlbItemEdit_TlbReportParameters_ReportParameters_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbReportParameters_ReportParameters"
                                                                    TextImageSpacing="5" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbReportParameters_ReportParameters"
                                                                    runat="server" ClientSideCommand="tlbItemRefresh_TlbReportParameters_ReportParameters_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbReportParameters_ReportParameters"
                                                                    TextImageSpacing="5" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemReportView_TlbReportParameters_ReportParameters"
                                                                    runat="server" ClientSideCommand="tlbItemReportView_TlbReportParameters_ReportParameters_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Report.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportView_TlbReportParameters_ReportParameters"
                                                                    TextImageSpacing="5" />
                                                                <ComponentArt:ToolBarItem ID="tlbItemReportViewExcel_TlbReportParameters_ReportParameters"
                                                                runat="server" ClientSideCommand="tlbItemReportViewExcel_TlbReportParameters_ReportParameters_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="Excel.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemReportViewExcel_TlbReportParameters_ReportParameters"
                                                                TextImageSpacing="5" />

                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                    <td id="loadingPanel_GridReportParameters_ReportParameters" class="WhiteLabel">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ComponentArt:CallBack ID="CallBack_GridReportParameters_ReportParameters" runat="server"
                                                OnCallback="CallBack_GridReportParameters_ReportParameters_onCallBack">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridReportParameters_ReportParameters" runat="server"
                                                        AllowColumnResizing="false" AllowMultipleSelect="false" CssClass="Grid" EnableViewState="false"
                                                        FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="11" RunningMode="Client"
                                                        ScrollBar="On" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                                                        ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                        ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" 
                                                        SearchTextCssClass="GridHeaderText" ShowFooter="false">
                                                        <Levels>
                                                            <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                                DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                                HeadingTextCssClass="HeadingCellText" RowCssClass="Row" SelectedRowCssClass="SelectedRow"
                                                                SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif"
                                                                SortImageHeight="5" SortImageWidth="9">
                                                                <Columns>
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                    <ComponentArt:GridColumn Align="Center" AllowEditing="False" DataField="ParameterTitle"
                                                                        DefaultSortDirection="Descending" HeadingText="نام پارامتر" HeadingTextCssClass="HeadingText"
                                                                        meta:resourcekey="clmnParameterName_GridReportParameters_ReportParameters" Width="100" TextWrap="true" />
                                                                    <ComponentArt:GridColumn Align="Center" AllowEditing="False" DataField="Value" DefaultSortDirection="Descending"
                                                                        HeadingText="مقدار پارامتر" HeadingTextCssClass="HeadingText" Width="250"  meta:resourcekey="clmnParameterValue_GridReportParameters_ReportParameters" />
                                                                    <ComponentArt:GridColumn DataField="Key" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="ActionId" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                    <ComponentArt:GridColumn DataField="ShowInDialog" Visible="false" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientEvents>
                                                            <Load EventHandler="GridReportParameters_ReportParameters_onLoad" />
                                                            <ItemDoubleClick EventHandler="GridReportParameters_ReportParameters_onItemDoubleClick" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField ID="ErroHiddenField_ReportParameters_ReportParameters" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridReportParameters_ReportParameters_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridReportParameters_ReportParameters_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <iframe id="IFrameReportParameters_ReportParameters" style="width: 99%; height: 300px"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPersonnelSelect"
            runat="server" Width="780px" HeaderClientTemplateId="DialogPersonnelSelectheader"
            FooterClientTemplateId="DialogPersonnelSelectfooter" Style="top: 611px; left: 0px">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogPersonnelSelectheader">
                    <table id="tbl_DialogPersonnelSelectheader" style="width: 783px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogPersonnelSelect.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelSelect_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogPersonnelSelect" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogPersonnelSelect" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="CloseDialogPersonnelSelect();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogPersonnelSelect_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogPersonnelSelectfooter">
                    <table id="tbl_DialogPersonnelSelectfooter" style="width: 783px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogPersonnelSelect_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogPersonnelSelect_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <Content>
                <table id="Mastertbl_PersonnelSelect" class="BoxStyle" style="width: 100%; background-color: White">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbPersonnelSelect" runat="server" class="BoxStyle" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbPersonnelSelect" runat="server" ClientSideCommand="tlbItemSave_TlbPersonnelSelect_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbPersonnelSelect"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbPersonnelSelect" runat="server" ClientSideCommand="tlbItemExit_TlbPersonnelSelect_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbPersonnelSelect"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="BoxStyle" style="width: 100%;">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_Personnel_PersonnelSelect" class="HeaderLabel" style="width: 20%;">Personnel
                                                </td>
                                                <td id="loadingPanel_GridPersonnel_PersonnelSelect" class="HeaderLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 20%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 5%">
                                                                <input id="chbAllInThisPage_PersonnelSelect" onclick="chbAllInThisPage_PersonnelSelect_onClick();" type="checkbox" /></td>
                                                            <td>
                                                                <asp:Label ID="lblAllInThisPage_PersonnelSelect" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAllInThisPage_PersonnelSelect" Text="همه در این صفحه"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 45%">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 15%; font-size: small; font-weight: normal;">
                                                                <asp:Label ID="lblQuickSearch_PersonnelSelect" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblQuickSearch_PersonnelSelect" Text=": جستجو"></asp:Label>
                                                            </td>
                                                            <td style="width: 80%">
                                                                <input id="txtSerchTerm_PersonnelSelect" runat="server" class="TextBoxes" onkeypress="txtSerchTerm_PersonnelSelect_onKeyPess(event);" style="width: 99%;" type="text" />
                                                            </td>
                                                            <td>
                                                                <ComponentArt:ToolBar ID="TlbPersonnelSelectQuickSearch" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbPersonnelSelectQuickSearch" runat="server" ClientSideCommand="tlbItemSearch_TlbPersonnelSelectQuickSearch_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbPersonnelSelectQuickSearch" TextImageSpacing="5" />
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
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack ID="CallBack_GridPersonnel_PersonnelSelect" runat="server"
                                            OnCallback="CallBack_GridPersonnel_PersonnelSelect_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridPersonnel_PersonnelSelect" runat="server" AllowColumnResizing="false"
                                                    AllowHorizontalScrolling="true" AllowMultipleSelect="false" CssClass="Grid" EnableViewState="false"
                                                    FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="10" RunningMode="Client"
                                                    ScrollBar="Off" ScrollBarCssClass="ScrollBar" ScrollBarWidth="16" ScrollButtonHeight="17"
                                                    ScrollButtonWidth="16" ScrollGripCssClass="ScrollGrip" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16"
                                                    SearchTextCssClass="GridHeaderText" ShowFooter="false" Width="600px">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                            DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                <ComponentArt:GridColumn Align="Center" ColumnType="CheckBox" DataField="Select"
                                                                    HeadingText="انتخاب" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSelect_GridPersonnel_PersonnelSelect"
                                                                    Width="50" AllowEditing="True" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending"
                                                                    HeadingText="شماره پرسنلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnPersonnelNumber_GridPersonnel_PersonnelSelect"
                                                                    Width="125" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridPersonnel_PersonnelSelect"
                                                                    Width="175" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Department.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="بخش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDepartment_GridPersonnel_PersonnelSelect"
                                                                    Width="175" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="OrganizationUnit.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="پست سازمانی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOrganizationPost_GridPersonnel_PersonnelSelect"
                                                                    Width="175" TextWrap="true" />
                                                                <ComponentArt:GridColumn DataField="Department.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                <ComponentArt:GridColumn DataField="OrganizationUnit.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridPersonnel_PersonnelSelect_onLoad" />
                                                        <ItemCheckChange EventHandler="GridPersonnel_PersonnelSelect_onItemCheckChange" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField ID="ErrorHiddenField_Personnel_PersonnelSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelCount_Personnel_PersonnelSelect" runat="server" />
                                                <asp:HiddenField ID="hfPersonnelPageCount_Personnel_PersonnelSelect" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridPersonnel_PersonnelSelect_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridPersonnel_PersonnelSelect_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td id="Td5" runat="server" meta:resourcekey="AlignObj" style="width: 50%;">
                                                    <ComponentArt:ToolBar ID="TlbPaging_GridPersonnel_PersonnelSelect" runat="server"
                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        Style="direction: ltr" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect_onClick();"
                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridPersonnel_PersonnelSelect"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <td id="PersonnelCount_GridPersonnel_PersonnelSelect" class="WhiteLabel" style="width: 25%"></td>
                                                <td id="footer_GridPersonnel_PersonnelSelect" class="WhiteLabel" style="width: 25%"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogPersonnelSelect_OnShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            HeaderClientTemplateId="DialogInternalReportParametersheader" FooterClientTemplateId="DialogInternalReportParametersfooter"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogInternalReportParameters"
            runat="server" PreloadContentUrl="false" ContentUrl="about:blank" IFrameCssClass="InternalReportParameters_iFrame">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogInternalReportParametersheader">
                    <table id="tbl_DialogInternalReportParametersheader" style="width: 703px" cellpadding="0"
                        cellspacing="0" border="0" onmousedown="DialogInternalReportParameters.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogInternalReportParameters_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogInternalReportParameters" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold;"></td>
                                        <td id="CloseButton_DialogInternalReportParameters" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="document.getElementById('DialogInternalReportParameters_IFrame').src = 'WhitePage.aspx'; DialogInternalReportParameters.Close('cancelled');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogInternalReportParameters_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogInternalReportParametersfooter">
                    <table id="tbl_DialogInternalReportParametersfooter" style="width: 703px" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogInternalReportParameters_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogInternalReportParameters_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <ClientEvents>
                <OnShow EventHandler="DialogInternalReportParameters_onShow" />
                <OnClose EventHandler="DialogInternalReportParameters_onClose" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="320px">
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

        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogDepartmentSearch"
            runat="server" Width="350px">
            <Content>
                <table style="width: 100%;" class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbDepartmentSearch_ReportParameters" runat="server"
                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDepartmentSearch_ReportParameters" runat="server" ClientSideCommand="tlbItemSave_TlbDepartmentSearch_ReportParameters_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbDepartmentSearch_ReportParameters"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDepartmentSearch_TlbDepartmentSearch_ReportParameters"
                                        runat="server" ClientSideCommand="tlbItemDepartmentSearch_TlbDepartmentSearch_ReportParameters_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDepartmentSearch_TlbDepartmentSearch_ReportParameters"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDepartmentSearch_ReportParameters" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbDepartmentSearch_ReportParameters_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbDepartmentSearch_ReportParameters"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchTermDepartment_ReportParameters" runat="server" Text=": جستجوي بخش"
                                meta:resourcekey="lblSearchTermDepartment_ReportParameters" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>

                                        <input id="txtSearchTermDepartment_ReportParameters" type="text" class="TextBoxes"
                                            onkeypress="txtSearchTermDepartment_ReportParameters_onKeyPess(event);" style="width: 87%" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDepartmentSearchResult_ReportParameters" runat="server" Text=": نتايج جستجوي بخش"
                                            meta:resourcekey="lblDepartmentsSearchResult_ReportParameters" CssClass="WhiteLabel"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbDepartmentSearchResult_ReportParameters"
                                            OnCallback="CallBack_cmbDepartmentSearchResult_ReportParameters_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbDepartmentSearchResult_ReportParameters" runat="server"
                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                    ExpandDirection="Up" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150"
                                                    DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                    Style="width: 90%" TextBoxCssClass="comboTextBox">
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_DepartmentSearchResult_ReportParameters" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbDepartmentSearchResult_ReportParameters_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbDepartmentSearchResult_ReportParameters_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbDepartmentSearchResult_ReportParameters_onCallbackError" />
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogOrganizationPostSearch"
            runat="server" Width="350px">
            <Content>
                <table style="width: 100%;" class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbOrganizationPostSearch_ReportParameters" runat="server"
                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbOrganizationPostSearch_ReportParameters" runat="server" ClientSideCommand="tlbItemSave_TlbOrganizationPostSearch_ReportParameters_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbOrganizationPostSearch_ReportParameters"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemOrganizationPostSearch_TlbOrganizationPostSearch_ReportParameters"
                                        runat="server" ClientSideCommand="tlbItemOrganizationPostSearch_TlbOrganizationPostSearch_ReportParameters_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemOrganizationPostSearch_TlbOrganizationPostSearch_ReportParameters"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbOrganizationPostSearch_ReportParameters" runat="server"
                                        ClientSideCommand="tlbItemExit_TlbOrganizationPostSearch_ReportParameters_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbOrganizationPostSearch_ReportParameters"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSearchTermOrganizationPost_ReportParameters" runat="server" Text=": جستجوي پست سازمانی"
                                meta:resourcekey="lblSearchTermOrganizationPost_ReportParameters" CssClass="WhiteLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>

                                        <input id="txtSearchTermOrganizationPost_ReportParameters" type="text" class="TextBoxes"
                                            onkeypress="txtSearchTermOrganizationPost_ReportParameters_onKeyPess(event);" style="width: 87%" />

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblOrganizationPostSearchResult_ReportParameters" runat="server" Text=": نتايج جستجوي پست سازمانی"
                                            meta:resourcekey="lblOrganizationPostSearchResult_ReportParameters" CssClass="WhiteLabel"></asp:Label>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbOrganizationPostSearchResult_ReportParameters"
                                            OnCallback="CallBack_cmbOrganizationPostSearchResult_ReportParameters_onCallBack"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbOrganizationPostSearchResult_ReportParameters" runat="server"
                                                    AutoComplete="true" AutoHighlight="false" CssClass="comboBox" DataFields="BarCode"
                                                    ExpandDirection="Up" DataTextField="Name" DropDownCssClass="comboDropDown" DropDownHeight="150"
                                                    DropDownPageSize="10" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" RunningMode="Client" SelectedItemCssClass="comboItemHover"
                                                    Style="width: 90%" TextBoxCssClass="comboTextBox">
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_OrganizationPostSearchResult_ReportParameters" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbOrganizationPostSearchResult_ReportParameters_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbOrganizationPostSearchResult_ReportParameters_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbOrganizationPostSearchResult_ReportParameters_onCallbackError" />
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
        <asp:HiddenField runat="server" ID="hfTitle_DialogReportParameters" meta:resourcekey="hfTitle_DialogReportParameters" />
        <asp:HiddenField runat="server" ID="hfheaderPersonnelFilter_PersonalFilter_ReportParameters"
            meta:resourcekey="hfheaderPersonnelFilter_PersonalFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfheaderReportParameters_PersonalFilter_ReportParameters"
            meta:resourcekey="hfheaderReportParameters_PersonalFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfheaderPersonnelFilter_GroupFilter_ReportParameters"
            meta:resourcekey="hfheaderPersonnelFilter_GroupFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfheaderReportParameters_ReportParameters" meta:resourcekey="hfheaderReportParameters_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_PersonalFilter_ReportParameters"
            meta:resourcekey="hfclmnName_cmbPersonnel_PersonalFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters"
            meta:resourcekey="hfclmnBarCode_cmbPersonnel_PersonalFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters"
            meta:resourcekey="hfclmnCardNum_cmbPersonnel_PersonalFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridReportParameters_ReportParameters"
            meta:resourcekey="hfloadingPanel_GridReportParameters_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfheaderSort_GroupFilter_ReportParameters" meta:resourcekey="hfheaderSort_GroupFilter_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_ReportParameters" meta:resourcekey="hfCurrentDate_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfErrorType_ReportParameters" meta:resourcekey="hfErrorType_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfConnectionError_ReportParameters" meta:resourcekey="hfConnectionError_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_ReportParameters" meta:resourcekey="hfCloseMessage_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfPersonnelCountTitle_GridPersonnel_PersonnelSelect" meta:resourcekey="hfPersonnelCountTitle_GridPersonnel_PersonnelSelect" />
        <asp:HiddenField runat="server" ID="hffooter_GridPersonnel_PersonnelSelect" meta:resourcekey="hffooter_GridPersonnel_PersonnelSelect" />
        <asp:HiddenField runat="server" ID="hfSelectedPersonnelCount_ReportParameters" meta:resourcekey="hfSelectedPersonnelCount_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_PersonnelSelect" meta:resourcekey="hfCloseMessage_PersonnelSelect" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridPersonnel_PersonnelSelect" meta:resourcekey="hfloadingPanel_GridPersonnel_PersonnelSelect" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogPersonnelSelect" meta:resourcekey="hfTitle_DialogPersonnelSelect" />
        <asp:HiddenField runat="server" ID="hfheader_Personnel_PersonnelSelect" meta:resourcekey="hfheader_Personnel_PersonnelSelect" />
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_Personnel_PersonnelSelect" />
        <asp:HiddenField runat="server" ID="hfCountSelectedNodeHiddenField_TrvDepartment_ReportParameters" meta:resourcekey="hfCountSelectedNodeHiddenField_TrvDepartment_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfCountSelectedNodeHiddenField_TrvEmployType_ReportParameters" meta:resourcekey="hfCountSelectedNodeHiddenField_TrvEmployType_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfCountSelectedNodeHiddenField_TrvUIValidationGroup_ReportParameters" meta:resourcekey="hfCountSelectedNodeHiddenField_TrvUIValidationGroup_ReportParameters" />
        <asp:HiddenField runat="server" ID="hfCountSelectedNodeHiddenField_TrvControlStation_ReportParameters" meta:resourcekey="hfCountSelectedNodeHiddenField_TrvControlStation_ReportParameters" />
    </form>
</body>
</html>
