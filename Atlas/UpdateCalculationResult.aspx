<%@ Page Language="C#" AutoEventWireup="true" Inherits="UpdateCalculationResult" Codebehind="UpdateCalculationResult.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UpdateCalculationResultForm" runat="server" meta:resourcekey="UpdateCalculationResultForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table id="Mastertbl_UpdateCalculationResult" style="width: 99%; height: 100%; font-family: Arial; font-size: small;"
                class="BoxStyle">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbUpdateCalculationResult" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" class="BoxStyle">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemCalculationResultArchive_TlbUpdateCalculationResult" runat="server" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="archive.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemCalculationResultArchive_TlbUpdateCalculationResult" TextImageSpacing="5"
                                    ClientSideCommand="tlbItemCalculationResultArchive_TlbUpdateCalculationResult_onClick();" />
                                <ComponentArt:ToolBarItem ID="tlbItemGridSettings_TlbUpdateCalculationResult" runat="server"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="package_settings.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemGridSettings_TlbUpdateCalculationResult"
                                    TextImageSpacing="5" ClientSideCommand="tlbItemGridSettings_TlbUpdateCalculationResult_onClick();" />
                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUpdateCalculationResult" runat="server" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUpdateCalculationResult" TextImageSpacing="5"
                                    ClientSideCommand="tlbItemHelp_TlbUpdateCalculationResult_onClick();" />
                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUpdateCalculationResult" runat="server"
                                    ClientSideCommand="tlbItemFormReconstruction_TlbUpdateCalculationResult_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUpdateCalculationResult" TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUpdateCalculationResult" runat="server" DropDownImageHeight="16px"
                                    ClientSideCommand="tlbItemExit_TlbUpdateCalculationResult_onClick();" DropDownImageWidth="16px"
                                    ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbUpdateCalculationResult"
                                    TextImageSpacing="5" />

                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%">
                                                <div runat="server" class="DropDownHeader" meta:resourcekey="AlignObj" style="width: 100%">
                                                    <img id="imgbox_SearchByPersonnel_UpdateCalculationResult" runat="server" alt="" onclick="imgbox_SearchByPersonnel_UpdateCalculationResult_onClick();"
                                                        src="Images/Ghadir/arrowDown.jpg" />
                                                    <span id="header_SearchByPersonnelBox_UpdateCalculationResult" class="WhiteLabel">انتخاب پرسنل</span>
                                                </div>
                                                <div id="box_SearchByPersonnel_UpdateCalculationResult" class="dhtmlgoodies_contentBox"
                                                    style="width: 40%;">
                                                    <div id="subbox_SearchByPersonnel_UpdateCalculationResult" class="dhtmlgoodies_content">
                                                        <table style="width: 95%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 90%">
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPersonnel_UpdateCalculationResult" runat="server" CssClass="ContentLabel"
                                                                                                meta:resourcekey="lblPersonnel_UpdateCalculationResult" Text=": پرسنل"></asp:Label>
                                                                                        </td>
                                                                                        <td id="Td2" runat="server" meta:resourcekey="InverseAlignObj">
                                                                                            <ComponentArt:ToolBar ID="TlbPaging_PersonnelSearch_UpdateCalculationResult" runat="server"
                                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                                UseFadeEffect="false" Style="direction: ltr">
                                                                                                <Items>
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        TextImageSpacing="5" />
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                        ImageUrl="first.png" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        TextImageSpacing="5" />
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                        ImageUrl="Before.png" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        TextImageSpacing="5" />
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        runat="server" ClientSideCommand="tlbItemNext_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                        ImageUrl="Next.png" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        TextImageSpacing="5" />
                                                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        runat="server" ClientSideCommand="tlbItemLast_TlbPaging_PersonnelSearch_UpdateCalculationResult_onClick();"
                                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                                                                                        ImageUrl="last.png" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PersonnelSearch_UpdateCalculationResult"
                                                                                                        TextImageSpacing="5" />
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
                                                                                <ComponentArt:CallBack ID="CallBack_cmbPersonnel_UpdateCalculationResult" runat="server"
                                                                                    Height="26" OnCallback="CallBack_cmbPersonnel_UpdateCalculationResult_onCallBack">
                                                                                    <Content>
                                                                                        <ComponentArt:ComboBox ID="cmbPersonnel_UpdateCalculationResult" runat="server" AutoComplete="true"
                                                                                            AutoHighlight="false" CssClass="comboBox" DataFields="BarCode" DataTextField="Name"
                                                                                            DropDownCssClass="comboDropDown" DropDownWidth="390" DropDownHeight="250" DropDownPageSize="8"
                                                                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemClientTemplateId="ItemTemplate_cmbPersonnel_UpdateCalculationResult"
                                                                                            ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" RunningMode="Client" TextBoxEnabled="true"
                                                                                            SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox">
                                                                                            <ClientTemplates>
                                                                                                <ComponentArt:ClientTemplate ID="ItemTemplate_cmbPersonnel_UpdateCalculationResult">
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
                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="390">
                                                                                                    <tr class="headingRow">
                                                                                                        <td id="clmnName_cmbPersonnel_UpdateCalculationResult" class="headingCell" style="width: 40%; text-align: center">Name And Family
                                                                                                        </td>
                                                                                                        <td id="clmnBarCode_cmbPersonnel_UpdateCalculationResult" class="headingCell" style="width: 30%; text-align: center">BarCode
                                                                                                        </td>
                                                                                                        <td id="clmnCardNum_cmbPersonnel_UpdateCalculationResult" class="headingCell" style="width: 30%; text-align: center">CardNum
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </DropDownHeader>
                                                                                            <ClientEvents>
                                                                                                <Expand EventHandler="cmbPersonnel_UpdateCalculationResult_onExpand" />
                                                                                                <Change EventHandler="cmbPersonnel_UpdateCalculationResult_onChange" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:ComboBox>
                                                                                        <asp:HiddenField ID="ErrorHiddenField_Personnel_UpdateCalculationResult" runat="server" />
                                                                                        <asp:HiddenField ID="hfPersonnelPageCount_UpdateCalculationResult" runat="server" />
                                                                                        <asp:HiddenField ID="hfPersonnelCount_UpdateCalculationResult" runat="server" />
                                                                                    </Content>
                                                                                    <ClientEvents>
                                                                                        <BeforeCallback EventHandler="CallBack_cmbPersonnel_UpdateCalculationResult_onBeforeCallback" />
                                                                                        <CallbackComplete EventHandler="CallBack_cmbPersonnel_UpdateCalculationResult_onCallBackComplete" />
                                                                                        <CallbackError EventHandler="CallBack_cmbPersonnel_UpdateCalculationResult_onCallbackError" />
                                                                                    </ClientEvents>
                                                                                </ComponentArt:CallBack>
                                                                            </td>
                                                                            <td style="width: 10%">&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 90%">
                                                                                <input id="txtPersonnelSearch_UpdateCalculationResult" runat="server" class="TextBoxes"
                                                                                 onkeypress="txtPersonnelSearch_UpdateCalculationResult_onKeyPess(event);"    style="width: 95%" type="text" />
                                                                            </td>
                                                                            <td style="width: 10%">
                                                                                <ComponentArt:ToolBar ID="TlbSearchPersonnel_UpdateCalculationResult" runat="server"
                                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbSearchPersonnel_UpdateCalculationResult"
                                                                                            runat="server" ClientSideCommand="tlbItemSearch_TlbSearchPersonnel_UpdateCalculationResult_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSearch_TlbSearchPersonnel_UpdateCalculationResult"
                                                                                            TextImageSpacing="5" />
                                                                                    </Items>
                                                                                </ComponentArt:ToolBar>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 90%">&nbsp;
                                                                            </td>
                                                                            <td style="width: 10%">
                                                                                <ComponentArt:ToolBar ID="TlbAdvancedSearch_UpdateCalculationResult" runat="server"
                                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemAdvancedSearch_TlbAdvancedSearch_UpdateCalculationResult"
                                                                                            runat="server" ClientSideCommand="tlbItemAdvancedSearch_TlbAdvancedSearch_UpdateCalculationResult_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png"
                                                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemAdvancedSearch_TlbAdvancedSearch_UpdateCalculationResult"
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
                                            <td id="tdPersonnelCount_UpdateCalculationResult" style="width: 20%">&nbsp;</td>
                                            <td valign="top">
                                                <table style="width: 30%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblYear_UpdateCalculationResult" runat="server" Text=": سال" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblYear_UpdateCalculationResult"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMonth_UpdateCalculationResult" runat="server" Text=": ماه" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblMonth_UpdateCalculationResult"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:ComboBox ID="cmbYear_UpdateCalculationResult" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbYear_UpdateCalculationResult_onChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                        </td>
                                                        <td>
                                                            <ComponentArt:ComboBox ID="cmbMonth_UpdateCalculationResult" runat="server" AutoComplete="true"
                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                TextBoxCssClass="comboTextBox" TextBoxEnabled="true" Width="100" DropDownHeight="280">
                                                                <ClientEvents>
                                                                    <Change EventHandler="cmbMonth_UpdateCalculationResult_onChange" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                        </td>
                                                        <td>
                                                            <ComponentArt:ToolBar ID="TlbView_UpdateCalculationResult" runat="server" CssClass="toolbar"
                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemView_TlbView_UpdateCalculationResult" runat="server"
                                                                        ClientSideCommand="tlbItemView_TlbView_UpdateCalculationResult_onClick();" DropDownImageHeight="16px"
                                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px"
                                                                        ItemType="Command" meta:resourcekey="tlbItemView_TlbView_UpdateCalculationResult"
                                                                        TextImageSpacing="5" Enabled="true" />
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
                    <td style="vertical-align: top">
                        <table style="width: 100%; height: 300px; border: outset 1px black;" class="BoxStyle">
                            <tr>
                                <td style="height: 5%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td id="header_UpdateCalculationResult_UpdateCalculationResult" class="HeaderLabel" style="width: 50%;">Calculation Results
                                            </td>
                                            <td id="loadingPanel_GridUpdateCalculationResult_UpdateCalculationResult" class="HeaderLabel"
                                                style="width: 45%"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    <div id="Container_GridUpdateCalculationResult_UpdateCalculationResult" style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridUpdateCalculationResult_UpdateCalculationResult"
                                            OnCallback="CallBack_GridUpdateCalculationResult_UpdateCalculationResult_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridUpdateCalculationResult_UpdateCalculationResult" runat="server" AllowEditing="true"
                                                    AllowHorizontalScrolling="true" CssClass="Grid" EnableViewState="false" ShowFooter="false"
                                                    FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" EditOnClickSelectedItem="false"
                                                    PagePaddingEnabled="true" PageSize="16" RunningMode="Client" AllowMultipleSelect="false"
                                                    AllowColumnResizing="false" ScrollBar="Off" ScrollTopBottomImagesEnabled="true"
                                                    ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="960">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell" HeadingRowCssClass="HL0HeadingRowClass"
                                                            DataKeyField="ID" HeadingCellCssClass="HHeadingCellClass" HeadingTextCssClass="HHeadingTextClass"
                                                            RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9" EditCommandClientTemplateId="EditCommandTemplate">
                                                            <Columns>
                                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="EditTemplate" EditControlType="EditCommand" Width="50" Align="Center" />
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn DataField="PersonId" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="PersonName" DefaultSortDirection="Descending" AllowEditing="False"
                                                                    HeadingText="نام و نام خانوادگی" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnPersonName_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="140" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="PersonCode" DefaultSortDirection="Descending" AllowEditing="False"
                                                                    HeadingText="شماره پرسنلی" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnPersonCode_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="110" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P1" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 1" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP1_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P2" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 2" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP2_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P3" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 3" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP3_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P4" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 4" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP4_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P5" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 5" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP5_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P6" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 6" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP6_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P7" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 7" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP7_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P8" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 8" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP8_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P9" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 9" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP9_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P10" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 10" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP10_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P11" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 11" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP11_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P12" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 12" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP12_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P13" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 13" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP13_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P14" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 14" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP14_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P15" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 15" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP15_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P16" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 16" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP16_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P17" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 17" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP17_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P18" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 18" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP18_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P19" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 19" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP19_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P20" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 20" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP20_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P21" DefaultSortDirection="Descending"
                                                                    HeadingText="2فیلد 1" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP21_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P22" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 22" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP22_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P23" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 23" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP23_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P24" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 24" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP24_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P25" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 25" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP25_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P26" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 26" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP26_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P27" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 27" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP27_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P28" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 28" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP28_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P29" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 29" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP29_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="P30" DefaultSortDirection="Descending"
                                                                    HeadingText="فیلد 30" HeadingTextCssClass="HHeadingTextClass" meta:resourcekey="clmnP30_GridUpdateCalculationResult_UpdateCalculationResult"
                                                                    Width="40" TextWrap="true" />
                                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="EditTemplate" EditControlType="EditCommand" Width="50" Align="Center" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="EditTemplate">
                                                            <a>
                                                                <img alt="" src="Images/ToolBar/edit.png" onclick="javascript:EditGridUpdateCalculationResult_UpdateCalculationResult('## DataItem.ClientId ##');" title="##SetCellTitle_GridUpdateCalculationResult_UpdateCalculationResult('Edit')##" /></a>
                                                        </ComponentArt:ClientTemplate>
                                                        <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                                                            <a>
                                                                <img alt="" src="Images/ToolBar/save.png" onclick="javascript:UpdateGridUpdateCalculationResult_UpdateCalculationResult();" title="##SetCellTitle_GridUpdateCalculationResult_UpdateCalculationResult('Save')##" /></a>&nbsp;<a>
                                                                    <img alt="" src="Images/ToolBar/cancel.png" onclick="javascript:GridUpdateCalculationResult_UpdateCalculationResult.EditCancel();" title="##SetCellTitle_GridUpdateCalculationResult_UpdateCalculationResult('Cancel')##" /></a>
                                                        </ComponentArt:ClientTemplate>
                                                    </ClientTemplates>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridUpdateCalculationResult_UpdateCalculationResult_onLoad" />
                                                        <ItemUpdate EventHandler="GridUpdateCalculationResult_UpdateCalculationResult_onItemUpdate"/>
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_UpdateCalculationResult" />
                                                <asp:HiddenField runat="server" ID="hfCalculationResultCount_UpdateCalculationResult" />
                                                <asp:HiddenField runat="server" ID="hfCalculationResultPageCount_UpdateCalculationResult" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridUpdateCalculationResult_UpdateCalculationResult_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridUpdateCalculationResult_UpdateCalculationResult_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                                <ComponentArt:ToolBar ID="TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                    Style="direction: ltr" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                            ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            TextImageSpacing="5" />
                                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            runat="server" ClientSideCommand="tlbItemFirst_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                            ImageUrl="first.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            TextImageSpacing="5" />
                                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                            ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            TextImageSpacing="5" />
                                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            runat="server" ClientSideCommand="tlbItemNext_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                            ImageUrl="Next.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            TextImageSpacing="5" />
                                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            runat="server" ClientSideCommand="tlbItemLast_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult_onClick();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                            ImageUrl="last.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridUpdateCalculationResult_UpdateCalculationResult"
                                                            TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                            <td id="footer_GridUpdateCalculationResult_UpdateCalculationResult" runat="server" class="WhiteLabel"
                                                meta:resourcekey="InverseAlignObj" style="width: 45%"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogGridSettings"
            runat="server" Width="200px">
            <Content>
                <table id="Table1" runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
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
                                    <td id="header_GridSettings_UpdateCalculationResult" style="color: White; font-weight: bold; font-family: Arial; width: 100%">Grid Settings
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 60%">
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_GridSettings_UpdateCalculationResult" type="checkbox"
                                                        onclick="chbSelectAll_GridSettings_UpdateCalculationResult_onClick();" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblSelectAll_GridSettings_UpdateCalculationResult" meta:resourcekey="lblSelectAll_GridSettings_UpdateCalculationResult"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridSettings_UpdateCalculationResult"
                                            OnCallback="CallBack_GridSettings_UpdateCalculationResult_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridSettings_UpdateCalculationResult" runat="server" AllowMultipleSelect="false"
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
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn DataField="PId" Visible="false" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                                    HeadingText="ستون گرید" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGridColumn_GridSettings_UpdateCalculationResult" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Visible" DefaultSortDirection="Descending"
                                                                    HeadingText="نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnView_GridSettings_UpdateCalculationResult"
                                                                    ColumnType="CheckBox" AllowEditing="True" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GridSettings_UpdateCalculationResult" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridSettings_UpdateCalculationResult_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridSettings_UpdateCalculationResult_onCallbackError" />
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogOverrideCalculationResult"
            runat="server" Width="400px">
            <Content>
                <table id="Mastertbl_DialogOverrideCalculationResult" style="width: 100%" class="BodyStyle">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 5%">
                                        <input id="chbOverrideCalculationResult_UpdateCalculationResult" type="checkbox" /></td>
                                    <td id="Td3" style="width: 95%">
                                        <asp:Label ID="lblOverrideCalculationResult_UpdateCalculationResult" runat="server" Text="جایگزینی آرشیو نتایج محاسبات پرسنل" meta:resourcekey="lblOverrideCalculationResult_UpdateCalculationResult"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOverrideCalculationResultDescription_UpdateCalculationResult" runat="server" CssClass="WhiteLabel" Style="text-wrap: normal" Text=" " meta:resourcekey="lblOverrideCalculationResultDescription_UpdateCalculationResult"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <ComponentArt:ToolBar ID="TlbOverrideCalculationResult_UpdateCalculationResult" runat="server" CssClass="toolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemOverrideCalculationResult_TlbOverrideCalculationResult_UpdateCalculationResult" runat="server"
                                        ClientSideCommand="tlbItemOverrideCalculationResult_TlbOverrideCalculationResult_UpdateCalculationResult_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemOverrideCalculationResult_TlbOverrideCalculationResult_UpdateCalculationResult"
                                        TextImageSpacing="5" Enabled="true" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="MiddleCentre" ID="DialogConfirm"
            runat="server" Width="420px">
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
                            <img id="Img1" runat="server" alt="" src="~/DesktopModules/Atlas/Images/Dialog/Waiting.gif" />
                        </td>
                    </tr>
                </table>
            </Content>
            <ClientEvents>
                <OnShow EventHandler="DialogWaiting_onShow" />
            </ClientEvents>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfTitle_DialogUpdateCalculationResult" meta:resourcekey="hfTitle_DialogUpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfheader_UpdateCalculationResult_UpdateCalculationResult" meta:resourcekey="hfheader_UpdateCalculationResult_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_UpdateCalculationResult" meta:resourcekey="hfDeleteMessage_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_UpdateCalculationResult" meta:resourcekey="hfCloseMessage_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfCurrentYear_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfCurrentMonth_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfCalculationResultPageSize_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hffooter_GridUpdateCalculationResult_UpdateCalculationResult" meta:resourcekey="hffooter_GridUpdateCalculationResult_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfErrorType_UpdateCalculationResult" meta:resourcekey="hfErrorType_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfConnectionError_UpdateCalculationResult" meta:resourcekey="hfConnectionError_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridUpdateCalculationResult_UpdateCalculationResult" meta:resourcekey="hfloadingPanel_GridUpdateCalculationResult_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfEdit_UpdateCalculationResult" meta:resourcekey="hfEdit_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfDelete_UpdateCalculationResult" meta:resourcekey="hfDelete_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfSave_UpdateCalculationResult" meta:resourcekey="hfSave_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfCancel_UpdateCalculationResult" meta:resourcekey="hfCancel_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfheader_SearchByPersonnelBox_UpdateCalculationResult"
            meta:resourcekey="hfheader_SearchByPersonnelBox_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfclmnName_cmbPersonnel_UpdateCalculationResult"
            meta:resourcekey="hfclmnName_cmbPersonnel_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfclmnBarCode_cmbPersonnel_UpdateCalculationResult"
            meta:resourcekey="hfclmnBarCode_cmbPersonnel_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfclmnCardNum_cmbPersonnel_UpdateCalculationResult"
            meta:resourcekey="hfclmnCardNum_cmbPersonnel_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfPersonnelPageSize_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfheader_GridSettings_UpdateCalculationResult" meta:resourcekey="hfheader_GridSettings_UpdateCalculationResult" />
        <asp:HiddenField runat="server" ID="hfPersonnelCountTitle_UpdateCalculationResult" meta:resourcekey="hfPersonnelCountTitle_UpdateCalculationResult" />
    </form>
</body>
</html>
