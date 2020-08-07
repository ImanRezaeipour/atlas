<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.WorkFlowDetail" Codebehind="WorkFlowDetail.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="WorkFlowDetailForm" runat="server" meta:resourcekey="WorkFlowDetailForm" class="BoxStyle">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table id="Mastertbl_WorkFlowDetailForm" style="width: 99%; height: 100%; font-family: Arial; font-size: small;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbWorkFlowDetail" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbWorkFlowDetail" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbWorkFlowDetail" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbWorkFlowDetail_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbWorkFlowDetail" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbWorkFlowDetail_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbWorkFlowDetail"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbWorkFlowDetail" runat="server" ClientSideCommand="tlbItemExit_TlbWorkFlowDetail_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbWorkFlowDetail"
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
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblSearchField_WorkFlowDetail" runat="server" Text=": جستجو بر اساس"
                                    CssClass="WhiteLabel" meta:resourcekey="lblSearchField_WorkFlowDetail"></asp:Label>
                            </td>
                            <td id="lblSearchTerm_WorkFlowDetail" style="visibility: visible">
                                <asp:Label ID="lblSearchTerm_WorkFlowDetail1" runat="server" Text="عبارت جستجو" CssClass="WhiteLabel"
                                    meta:resourcekey="lblSearchTerm_WorkFlowDetail"></asp:Label>
                            </td>
                            <%-- <td>
                                <asp:Label ID="lblSearchResult_WorkFlowDetail" runat="server" Text="نتایج جستجو" CssClass="WhiteLabel"
                                    meta:resourcekey="lblSearchResult_WorkFlowDetail" Visible="false"></asp:Label>
                            </td>--%>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbSearchField_WorkFlowDetail"
                                    OnCallback="CallBack_cmbSearchField_WorkFlowDetail_onCallBack" Height="26">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbSearchField_WorkFlowDetail" runat="server" AutoComplete="true"
                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                            DropDownHeight="110" DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover"
                                            HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover"
                                            SelectedItemCssClass="comboItemHover" TextBoxCssClass="comboTextBox" Width="150"
                                            TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Expand EventHandler="cmbSearchField_WorkFlowDetail_onExpand" />
                                                <Collapse EventHandler="cmbSearchField_WorkFlowDetail_onCollapse" />
                                                <Change EventHandler="cmbSearchField_WorkFlowDetail_OnChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlowDetailSearchField" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBack_cmbSearchField_WorkFlowDetail_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBack_cmbSearchField_WorkFlowDetail_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_cmbSearchField_WorkFlowDetail_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                            <td style="width: 17%">
                                <input id="txtSearchTerm_WorkFlowDetail" type="text" style="width: 95%; visibility: visible" class="TextBoxes"
                                    onkeypress="txtSearchTerm_WorkFlowDetail_onKeyPress(event)" />
                            </td>
                            <td id="tlbView_WorkFlowDetail" style="width: 5%; visibility: visible">
                                <ComponentArt:ToolBar ID="TlbView_WorkFlowDetail" runat="server"
                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_WorkFlowDetail"
                                            runat="server" ClientSideCommand="View_WorkFlowDetail();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbView_WorkFlowDetail"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td style="width: 2%">
                                <input id="chbMatchWholWord_WorkFlowDetail" type="checkbox" style="visibility: visible" />
                            </td>
                            <td id="tdlblMatchWholWord_WorkFlowDetail" style="width: 18%; visibility: visible">
                                <asp:Label ID="lblMatchWholWord_WorkFlowDetail" runat="server" Text="انطباق کل عبارت" CssClass="WhiteLabel"
                                    meta:resourcekey="lblMatchWholWord_WorkFlowDetail"></asp:Label>
                            </td>
                            <td style="width: 48%">
                                <div id="divPersonnelSearch_WorkFlowDetail" runat="server" meta:resourcekey="AlignObj" style="width: 100%; visibility: hidden" class="DropDownHeader">
                                    <img alt="" runat="server" id="imgbox_SearchByPersonnel_WorkFlowDetail" src="Images/Ghadir/arrowDown_silver.jpg"
                                        onclick="imgbox_SearchByPersonnel_WorkFlowDetail_onClick();" />
                                    <span id="header_SearchByPersonnelBox_WorkFlowDetail">select personnel</span>
                                </div>
                                <div class="dhtmlgoodies_contentBox" id="box_SearchByPersonnel_WorkFlowDetail" style="width: 30%;">
                                    <div class="dhtmlgoodies_content" id="subbox_SearchByPersonnel_WorkFlowDetail">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblPersonnel_WorkFlowDetail" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblPersonnel_WorkFlowDetail"
                                                                                Text=": پرسنل"></asp:Label>
                                                                        </td>
                                                                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                            <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_WorkFlowDetail" runat="server" CssClass="toolbar"
                                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                UseFadeEffect="false" Style="direction: ltr;">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_WorkFlowDetail" runat="server"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_WorkFlowDetail"
                                                                                        TextImageSpacing="5" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick();" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_WorkFlowDetail" runat="server"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                        ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_WorkFlowDetail"
                                                                                        ImageUrl="first.png" TextImageSpacing="5" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick();" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_WorkFlowDetail" runat="server"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                        ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_WorkFlowDetail"
                                                                                        ImageUrl="Before.png" TextImageSpacing="5" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick();" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_WorkFlowDetail" runat="server"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                        ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_WorkFlowDetail"
                                                                                        ImageUrl="Next.png" TextImageSpacing="5" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick();" />
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_WorkFlowDetail" runat="server"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                        ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_WorkFlowDetail"
                                                                                        ImageUrl="last.png" TextImageSpacing="5" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_WorkFlowDetail_onClick();" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 10%">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <ComponentArt:CallBack ID="CallBack_cmbPersonnel_WorkFlowDetail" runat="server" OnCallback="CallBack_cmbPersonnel_WorkFlowDetail_onCallBack"
                                                                    Height="26">
                                                                    <Content>
                                                                        <ComponentArt:ComboBox ID="cmbPersonnel_WorkFlowDetail" runat="server" AutoComplete="true"
                                                                            AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                            DropDownWidth="400" DropDownCssClass="comboDropDown" DropDownHeight="210" DropDownPageSize="7"
                                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_WorkFlowDetail"
                                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                            <ClientTemplates>
                                                                                <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_WorkFlowDetail">
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
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="400">
                                                                                    <tr class="headingRow">
                                                                                        <td id="clmnName_cmbPersonnel_WorkFlowDetail" class="headingCell" style="width: 40%; text-align: center">Name And Family
                                                                                        </td>
                                                                                        <td id="clmnBarCode_cmbPersonnel_WorkFlowDetail" class="headingCell" style="width: 30%; text-align: center">BarCode
                                                                                        </td>
                                                                                        <td id="clmnCardNum_cmbPersonnel_WorkFlowDetail" class="headingCell" style="width: 30%; text-align: center">CardNum
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </DropDownHeader>
                                                                            <ClientEvents>
                                                                                <Expand EventHandler="cmbPersonnel_WorkFlowDetail_onExpand" />
                                                                                <Change EventHandler="cmbPersonnel_WorkFlowDetail_OnChange" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:ComboBox>
                                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Personnel_WorkFlowDetail" />
                                                                        <asp:HiddenField runat="server" ID="hfPersonnelPageCount_WorkFlowDetail" />
                                                                    </Content>
                                                                    <ClientEvents>
                                                                        <BeforeCallback EventHandler="CallBack_cmbPersonnel_WorkFlowDetail_onBeforeCallback" />
                                                                        <CallbackComplete EventHandler="CallBack_cmbPersonnel_WorkFlowDetail_onCallBackComplete" />
                                                                        <CallbackError EventHandler="CallBack_cmbPersonnel_WorkFlowDetail_onCallbackError" />
                                                                    </ClientEvents>
                                                                </ComponentArt:CallBack>
                                                            </td>
                                                            <td style="width: 10%">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <input runat="server" id="txtPersonnelSearch_WorkFlowDetail" class="TextBoxes" type="text"
                                                                    onkeypress="txtPersonnelSearch_WorkFlowDetail_onKeyPess(event);" style="width: 95%" />
                                                            </td>
                                                            <td style="width: 10%">
                                                                <ComponentArt:ToolBar ID="TlbSearchPersonnel_WorkFlowDetail" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_WorkFlowDetail" runat="server"
                                                                            ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_WorkFlowDetail_onClick();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_WorkFlowDetail"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 90%">
                                                                <ComponentArt:ToolBar ID="TlbViewWorkFlow_WorkFlowDetail" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemView_TlbViewWorkFlow_WorkFlowDetail"
                                                                            runat="server" ClientSideCommand="ViewWorkFlow_WorkFlowDetail();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemView_TlbViewWorkFlow_WorkFlowDetail"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <ComponentArt:ToolBar ID="TlbAdvancedSearch_WorkFlowDetail" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_WorkFlowDetail" runat="server"
                                                                            ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_WorkFlowDetail_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_WorkFlowDetail"
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
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 35%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_Managers_WorkFlowDetail" class="HeaderLabel" style="width: 50%">Managers
                                                                </td>
                                                                <td id="loadingPanel_GridManagers_WorkFlowDetail" class="HeaderLabel" style="width: 45%"></td>
                                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridManagers_WorkFlowDetail" runat="server"
                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridManagers_WorkFlowDetail"
                                                                                runat="server" ClientSideCommand="Refresh_GridManagers_WorkFlowDetail();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridManagers_WorkFlowDetail"
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
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridManagers_WorkFlowDetail"
                                                            OnCallback="CallBack_GridManagers_WorkFlowDetail_onCallBack">
                                                            <Content>
                                                                <ComponentArt:DataGrid ID="GridManagers_WorkFlowDetail" runat="server" CssClass="Grid"
                                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="6" RunningMode="Client"
                                                                    SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                                                    AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                                    Height="150" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                                    <Levels>
                                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow"
                                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                                            <Columns>
                                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn DataField="ThePerson.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="ThePerson.Name" DefaultSortDirection="Descending"
                                                                                    HeadingText="نام مدیر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnManagersName_GridManagers_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Visible="false" DataField="TheOrganizationUnit.ID" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="TheOrganizationUnit.Name" DefaultSortDirection="Descending"
                                                                                    HeadingText="نام پست سازمانی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOrganizationName_GridManagers_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="ThePerson.BarCode" DefaultSortDirection="Descending"
                                                                                    HeadingText="شماره پرسنلی مدیر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnManagersCode_GridManagers_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                    </Levels>
                                                                    <ClientEvents>
                                                                        <Load EventHandler="GridManagers_WorkFlowDetail_onLoad" />
                                                                        <ItemSelect EventHandler="GridManagers_WorkFlowDetail_onItemSelect" />
                                                                    </ClientEvents>
                                                                </ComponentArt:DataGrid>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Managers_WorkFlowDetail" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_GridManagers_WorkFlowDetail_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_GridManagers_WorkFlowDetail_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 35%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_WorkFlows_WorkFlowDetail" class="HeaderLabel" style="width: 50%">WorkFlows
                                                                </td>
                                                                <td id="loadingPanel_GridWorkFlows_WorkFlowDetail" class="HeaderLabel" style="width: 45%"></td>
                                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbGridWorkFlows_WorkFlowDetail" runat="server"
                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemUnderManagmentPersonnelsRetrieve_TlbGridWorkFlows"
                                                                                runat="server" ClientSideCommand="tlbItemUnderManagmentPersonnelsRetrieve_TlbWorkFlowDetail_onClick();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="retrieveUndermanagements_Silver.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemUnderManagmentPersonnelsRetrieve_TlbGridWorkFlows"
                                                                                TextImageSpacing="5" Enabled="false" />
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridWorkFlows_WorkFlowDetail"
                                                                                runat="server" ClientSideCommand="Refresh_GridWorkFlows_WorkFlowDetail();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridWorkFlows_WorkFlowDetail"
                                                                                TextImageSpacing="5" />
                                                                            <ComponentArt:ToolBarItem ID="tlbItemAccessLevelView_TlbWorkFlowDetail" runat="server" DropDownImageHeight="16px"
                                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="access.png" ImageWidth="16px"
                                                                                ItemType="Command" meta:resourcekey="tlbItemAccessLevelView_TlbWorkFlowDetail" TextImageSpacing="5"
                                                                                ClientSideCommand="tlbItemAccessLevelView_TlbWorkFlowDetail_onClick();" />
                                                                        </Items>
                                                                    </ComponentArt:ToolBar>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridWorkFlows_WorkFlowDetail"
                                                            OnCallback="CallBack_GridWorkFlows_WorkFlowDetail_onCallBack">
                                                            <Content>
                                                                <ComponentArt:DataGrid ID="GridWorkFlows_WorkFlowDetail" runat="server" CssClass="Grid"
                                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="6" RunningMode="Client"
                                                                    SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                                                    AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                                    Height="150" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                                    <Levels>
                                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow"
                                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                                            <Columns>
                                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn DataField="AccessGroup.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="FlowName" DefaultSortDirection="Descending"
                                                                                    HeadingText="جریان های کاری" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkFlows_GridWorkFlows_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="AccessGroup.Name" DefaultSortDirection="Descending"
                                                                                    HeadingText="گروه دسترسی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnAccessGroup_GridWorkFlows_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="ActiveFlow" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                                                    HeadingText="جریان فعال" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnActiveFlow_GridWorkFlows_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="MainFlow" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                                                    HeadingText="جریان اصلی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnMainFlow_GridWorkFlows_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                    </Levels>
                                                                    <ClientEvents>
                                                                        <Load EventHandler="GridWorkFlows_WorkFlowDetail_onLoad" />
                                                                        <ItemSelect EventHandler="GridWorkFlows_WorkFlowDetail_onItemSelect" />
                                                                    </ClientEvents>
                                                                </ComponentArt:DataGrid>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_WorkFlows_WorkFlowDetail" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_GridWorkFlows_WorkFlowDetail_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_GridWorkFlows_WorkFlowDetail_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 29%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_Operator_WorkFlowDetail" class="HeaderLabel" style="width: 50%">Operator
                                                                </td>
                                                                <td id="loadingPanel_GridOperator_WorkFlowDetail" class="HeaderLabel" style="width: 45%"></td>
                                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridOperator_WorkFlowDetail" runat="server"
                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridOperator_WorkFlowDetail"
                                                                                runat="server" ClientSideCommand="Refresh_GridOperator_WorkFlowDetail();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridOperator_WorkFlowDetail"
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
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridOperator_WorkFlowDetail"
                                                            OnCallback="CallBack_GridOperator_WorkFlowDetail_onCallBack">
                                                            <Content>
                                                                <ComponentArt:DataGrid ID="GridOperator_WorkFlowDetail" runat="server" CssClass="Grid"
                                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="6" RunningMode="Client"
                                                                    SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                                                    AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                                    Height="150" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                                    <Levels>
                                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow"
                                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                                            <Columns>
                                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn DataField="Person.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Person.Name" DefaultSortDirection="Descending"
                                                                                    HeadingText="اپراتور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOperatorName_GridOperator_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Person.PersonCode" DefaultSortDirection="Descending"
                                                                                    HeadingText="شماره پرسنلی اپراتور" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOperatorCode_GridOperator_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                    </Levels>
                                                                    <ClientEvents>
                                                                        <Load EventHandler="GridOperator_WorkFlowDetail_onLoad" />
                                                                        <ItemSelect EventHandler="GridOperator_WorkFlowDetail_onItemSelect" />
                                                                    </ClientEvents>
                                                                </ComponentArt:DataGrid>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Operator_WorkFlowDetail" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_GridOperator_WorkFlowDetail_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_GridOperator_WorkFlowDetail_onCallbackError" />
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
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_Substitute_WorkFlowDetail" class="HeaderLabel" style="width: 50%">Substitute
                                                                </td>
                                                                <td id="loadingPanel_GridSubstitute_WorkFlowDetail" class="HeaderLabel" style="width: 45%"></td>
                                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridSubstitute_WorkFlowDetail" runat="server"
                                                                        CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridSubstitute_WorkFlowDetail"
                                                                                runat="server" ClientSideCommand="Refresh_GridSubstitute_WorkFlowDetail();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridSubstitute_WorkFlowDetail"
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
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridSubstitute_WorkFlowDetail"
                                                            OnCallback="CallBack_GridSubstitute_WorkFlowDetail_onCallBack">
                                                            <Content>
                                                                <ComponentArt:DataGrid ID="GridSubstitute_WorkFlowDetail" runat="server" CssClass="Grid"
                                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="12" RunningMode="Client"
                                                                    SearchTextCssClass="GridHeaderText" AllowMultipleSelect="false" ShowFooter="false"
                                                                    AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
                                                                    Height="290" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                                    <Levels>
                                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow"
                                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false">
                                                                            <Columns>
                                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn DataField="Manager.ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Person.Name" DefaultSortDirection="Descending"
                                                                                    HeadingText="نام جانشین" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSubstituteName_GridSubstitute_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="Person.PersonCode" DefaultSortDirection="Descending"
                                                                                    HeadingText="شماره پرسنلی جانشین" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSubstituteCode_GridSubstitute_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="TheFromDate" DefaultSortDirection="Descending"
                                                                                    HeadingText="از تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSubstituteFromDate_GridSubstitute_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                                <ComponentArt:GridColumn Align="Center" DataField="TheToDate" DefaultSortDirection="Descending"
                                                                                    HeadingText="تا تاریخ" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnSubstituteToDate_GridSubstitute_WorkFlowDetail"
                                                                                    TextWrap="true" />
                                                                            </Columns>
                                                                        </ComponentArt:GridLevel>
                                                                    </Levels>
                                                                    <ClientEvents>
                                                                        <Load EventHandler="GridSubstitute_WorkFlowDetail_onLoad" />
                                                                        <ItemSelect EventHandler="GridSubstitute_WorkFlowDetail_onItemSelect" />
                                                                    </ClientEvents>
                                                                </ComponentArt:DataGrid>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Substitute_WorkFlowDetail" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_GridSubstitute_WorkFlowDetail_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_GridSubstitute_WorkFlowDetail_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 50%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td id="header_UnderManagementPersonnel_WorkFlowDetail" class="HeaderLabel"
                                                                    style="width: 55%">UnderManagement Personnel
                                                                </td>
                                                                <td id="loadingPanel_trvUnderManagementPersonnel_WorkFlowDetail" class="HeaderLabel"
                                                                    style="width: 40%"></td>
                                                                <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                    <ComponentArt:ToolBar ID="TlbRefresh_trvUnderManagementPersonnel_WorkFlowDetail"
                                                                        runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvUnderManagementPersonnel_WorkFlowDetail"
                                                                                runat="server" ClientSideCommand="Refresh_trvUnderManagementPersonnel_WorkFlowDetail();"
                                                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvUnderManagementPersonnel_WorkFlowDetail"
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
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_trvUnderManagementPersonnel_WorkFlowDetail"
                                                            OnCallback="CallBack_trvUnderManagementPersonnel_WorkFlowDetail_onCallBack">
                                                            <Content>
                                                                <ComponentArt:TreeView ID="trvUnderManagementPersonnel_WorkFlowDetail" runat="server"
                                                                    CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView" DefaultImageHeight="16"
                                                                    ExpandNodeOnSelect="false" DefaultImageWidth="16" DragAndDropEnabled="false" EnableViewState="false"
                                                                    ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif"
                                                                    FillContainer="false" Height="290" HoverNodeCssClass="HoverTreeNode" ItemSpacing="2"
                                                                    KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="Images/TreeView/LeftLines"
                                                                    NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3"
                                                                    SelectedNodeCssClass="SelectedTreeNode" ShowLines="true" BorderColor="Black"
                                                                    meta:resourcekey="trvUnderManagementPersonnel_WorkFlowDetail">
                                                                    <ClientEvents>
                                                                        <Load EventHandler="trvUnderManagementPersonnel_WorkFlowDetail_onLoad" />
                                                                        <CallbackComplete EventHandler="trvUnderManagementPersonnel_WorkFlowDetail_onCallbackComplete" />
                                                                        <NodeBeforeExpand EventHandler="trvUnderManagementPersonnel_WorkFlowDetail_onNodeBeforeExpand" />
                                                                        <NodeExpand EventHandler="trvUnderManagementPersonnel_WorkFlowDetail_onNodeExpand" />
                                                                    </ClientEvents>
                                                                </ComponentArt:TreeView>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_UnderManagementPersonnel_WorkFlowDetail" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <CallbackComplete EventHandler="CallBack_trvUnderManagementPersonnel_WorkFlowDetail_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_trvUnderManagementPersonnel_WorkFlowDetail_onCallbackError" />
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
        <%--start dialogAccessLevel--%>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogAccessLevel"
            runat="server" Width="300px" HeaderClientTemplateId="DialogAccessLevelheader"
            FooterClientTemplateId="DialogAccessLevelfooter">
            <ClientTemplates>
                <ComponentArt:ClientTemplate ID="DialogAccessLevelheader">
                    <table style="width: 301px" cellpadding="0" cellspacing="0" border="0" onmousedown="DialogAccessLevel.StartDrag(event);">
                        <tr>
                            <td width="6">
                                <img id="DialogAccessLevel_topLeftImage" style="display: block;" src="Images/Dialog/top_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/top.gif); padding: 3px">
                                <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="Title_DialogAccessLevel" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bold"></td>
                                        <td id="CloseButton_DialogAccessLevel" valign="middle">
                                            <img alt="" src="Images/Dialog/close-down.png" onclick="DialogAccessLevel.Close('cancelled')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="6">
                                <img id="DialogAccessLevel_topRightImage" style="display: block;" src="Images/Dialog/top_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
                <ComponentArt:ClientTemplate ID="DialogAccessLevelfooter">
                    <table id="tbl_DialogAccessLevelfooter" style="width: 301px" cellpadding="0" cellspacing="0"
                        border="0">
                        <tr>
                            <td width="6">
                                <img id="DialogAccessLevel_downLeftImage" style="display: block;" src="Images/Dialog/down_left.gif"
                                    alt="" />
                            </td>
                            <td style="background-image: url(Images/Dialog/down.gif); background-repeat: repeat; padding: 3px"></td>
                            <td width="6">
                                <img id="DialogAccessLevel_downRightImage" style="display: block;" src="Images/Dialog/down_right.gif"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                </ComponentArt:ClientTemplate>
            </ClientTemplates>
            <Content>
                <table id="Table1" runat="server" class="BodyStyle" style="width: 100%; font-family: Arial; font-size: small">
                    <%--<tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbAccessLevel" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbAccessLevel" runat="server" ClientSideCommand="tlbItemSave_TlbAccessLevel_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbAccessLevel"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbAccessLevel" runat="server" ClientSideCommand="tlbItemExit_TlbAccessLevel_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_AccessLevel"
                                                TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td id="ActionMode_AccessLevel" class="ToolbarMode">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                    <%--<tr>
                    <td>
                        <asp:Label ID="lblAccessGroupName_AccessLevel_AccessGroups" runat="server" CssClass="WhiteLabel"
                            meta:resourcekey="lblAccessGroupName_AccessLevel_AccessGroups" Text=": نام گروه دسترسی"></asp:Label>
                    </td>
                </tr>--%>
                    <%-- <tr>
                    <td>
                        <input id="txtAccessGroupName_AccessLevel_AccessGroups" class="TextBoxes" readonly="readonly"
                            type="text" style="width: 99%" />
                    </td>
                </tr>--%>
                    <tr>
                        <td>
                            <table class="BoxStyle" style="width: 100%;">
                                <tr>
                                    <td id="" style="color: White; font-weight: bold; font-family: Arial; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_AccessLevelBox_AccessGroups" class="HeaderLabel" style="width: 50%">Access Levels
                                                </td>
                                                <td id="loadingPanel_trvAccessLevel_AccessGroups" class="HeaderLabel" style="width: 45%"></td>
                                                <%--<td id="Td2" runat="server" meta:resourcekey="InverseAlignObj" style="width: 5%">
                                                <ComponentArt:ToolBar ID="TlbRefresh_trvAccessLevel_AccessGroups" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_trvAccessLevel_AccessGroups"
                                                            runat="server" ClientSideCommand="Refresh_trvAccessLevel_AccessGroups();" DropDownImageHeight="16px"
                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_trvAccessLevel_AccessGroups"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>--%>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:CallBack ID="CallBack_trvAccessLevel_AccessGroups" runat="server" OnCallback="CallBack_trvAccessLevel_AccessGroups_onCallBack">
                                            <Content>
                                                <ComponentArt:TreeView ID="trvAccessLevel_AccessGroups" runat="server" ExpandNodeOnSelect="true"
                                                    CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                    DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                    EnableViewState="false" ExpandCollapseImageHeight="15" LoadingFeedbackText="loading......."
                                                    ExpandCollapseImageWidth="17" ExpandImageUrl="images/TreeView/col.gif" FillContainer="false"
                                                    ForceHighlightedNodeID="true" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2"
                                                    KeyboardEnabled="true" LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode"
                                                    NodeEditCssClass="NodeEdit" NodeIndent="17" NodeLabelPadding="3" SelectedNodeCssClass="SelectedTreeNode"
                                                    ShowLines="true" BorderColor="Black" Height="290" meta:resourcekey="trvAccessLevel_AccessGroups">
                                                    <ClientEvents>
                                                        <NodeCheckChange EventHandler="trvAccessLevel_AccessGroups_onNodeCheckChange" />
                                                        <Load EventHandler="trvAccessLevel_AccessGroups_onLoad" />
                                                        <NodeExpand EventHandler="trvAccessLevel_AccessGroups_onNodeExpand" />
                                                    </ClientEvents>
                                                </ComponentArt:TreeView>
                                                <asp:HiddenField ID="hfAccessLevelsList_AccessGroups" runat="server" Value="null" />
                                                <asp:HiddenField ID="ErrorHiddenField_AccessLevel" runat="server" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_trvAccessLevel_AccessGroups_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_trvAccessLevel_AccessGroups_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogAccessLevel_OnShow" />
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
        <%-- end dialogAccessLevel--%>
        <asp:HiddenField runat="server" ID="hfheader_Managers_WorkFlowDetail" meta:resourcekey="hfheader_Managers_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfheader_WorkFlows_WorkFlowDetail" meta:resourcekey="hfheader_WorkFlows_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfheader_Operator_WorkFlowDetail" meta:resourcekey="hfheader_Operator_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfheader_Substitute_WorkFlowDetail" meta:resourcekey="hfheader_Substitute_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfheader_UnderManagementPersonnel_WorkFlowDetail" meta:resourcekey="hfheader_UnderManagementPersonnel_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfCloseMessage__WorkFlowDetail" meta:resourcekey="hfCloseMessage__WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridOperator_WorkFlowDetail" meta:resourcekey="hfloadingPanel_GridOperator_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridWorkFlows_WorkFlowDetail" meta:resourcekey="hfloadingPanel_GridWorkFlows_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridManagers_WorkFlowDetail" meta:resourcekey="hfloadingPanel_GridManagers_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_trvUnderManagementPersonnel_WorkFlowDetail" meta:resourcekey="hfloadingPanel_trvUnderManagementPersonnel_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridSubstitute_WorkFlowDetail" meta:resourcekey="hfloadingPanel_GridSubstitute_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_WorkFlowDetail" meta:resourcekey="hfcmbAlarm_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfheader_AccessLevelBox_AccessGroups" meta:resourcekey="hfheader_AccessLevelBox_AccessGroups" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_trvAccessLevel_AccessGroups" meta:resourcekey="hfloadingPanel_trvAccessLevel_AccessGroups" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogAccessLevel" meta:resourcekey="hfTitle_DialogAccessLevel" />
        <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_WorkFlowDetail" meta:resourcekey="hfheader_SearchByPersonnelBox_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfErrorType_WorkFlowDetail" meta:resourcekey="hfErrorType_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfConnectionError_WorkFlowDetail" meta:resourcekey="hfConnectionError_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_WorkFlowDetail" meta:resourcekey="hfclmnBarCode_cmbPersonnel_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_WorkFlowDetail" meta:resourcekey="hfclmnName_cmbPersonnel_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_WorkFlowDetail" meta:resourcekey="hfclmnCardNum_cmbPersonnel_WorkFlowDetail" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogWorkFlowDetail" meta:resourcekey="hfTitle_DialogWorkFlowDetail" />
    </form>
</body>
</html>
