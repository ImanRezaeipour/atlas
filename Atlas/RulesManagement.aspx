<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.RulesManagement" Codebehind="RulesManagement.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="css/colorPickerStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RulesManagementForm" runat="server" meta:resourcekey="RulesManagementForm">
        <table style="width: 100%; height: 400px; font-family: Arial; font-size: small;" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 85%;">
                                            <ComponentArt:ToolBar ID="TlbRules" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbRules" runat="server" ClientSideCommand="tlbItemNew_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemNew_TlbRules"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbRules" runat="server" ClientSideCommand="tlbItemEdit_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemEdit_TlbRules"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbRules" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemDelete_TlbRules_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" RuleType="Command"
                                                        meta:resourcekey="tlbItemDelete_TlbRules" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRules" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemSave_TlbRules_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" RuleType="Command"
                                                        meta:resourcekey="tlbItemSave_TlbRules" TextImageSpacing="5" Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbRules" runat="server" ClientSideCommand="tlbItemCancel_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemCancel_TlbRules" TextImageSpacing="5"
                                                        Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemDefine_TlbRules" runat="server" ClientSideCommand="tlbItemDefine_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed_silver.png"
                                                        ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemDefine_TlbRules" TextImageSpacing="5"
                                                        Enabled="false" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemCompileConceptAndRule_TlbRules"
                                                        runat="server" ClientSideCommand="tlbItemCompileConceptAndRule_TlbRules_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCompileConceptAndRule_TlbRules"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRule" runat="server"
                                                        ClientSideCommand="tlbItemFormReconstruction_TlbRule_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRule" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRulesManagement" runat="server"
                                                        ClientSideCommand="tlbItemHelp_TlbRulesManagement_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                        ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRule" TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRules" runat="server" DropDownImageHeight="16px"
                                                        ClientSideCommand="tlbItemExit_TlbRules_onClick();" DropDownImageWidth="16px"
                                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" RuleType="Command"
                                                        meta:resourcekey="tlbItemExit_TlbRules" TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td style="width: 15%;" class="ToolbarMode">
                                            <span id="ActionMode_Rules"></span>
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
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table style="width: 99%;" class="BoxStyle">
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <asp:Label ID="lblQuickSerch_Rule" runat="server" meta:resourcekey="lblQuickSearch_Rules" Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                                                </td>
                                                                <td style="width: 80%">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSearchTerm_Rules" />
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <ComponentArt:ToolBar ID="tlbItemQuickSearch" runat="server" CssClass="toolbar"
                                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                    UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbRuleQuickSearch" runat="server"
                                                                                            ClientSideCommand="tlbItemSearch_TlbRuleQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                            ItemType="Command" meta:resourcekey="tlbItemSearch_TlbRuleQuickSearch" TextImageSpacing="5" />
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
                                                    <td style="width: 60%">
                                                        <table style="width: 99%;" class="BoxStyle">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td id="" class="HeaderLabel" style="width: 95%">
                                                                                <table style="width: 100%;">
                                                                                    <tr>
                                                                                        <td id="header_RulesBox_Rules" class="HeaderLabel" style="width: 50%;">Rules
                                                                                        </td>
                                                                                        <td id="loadingPanel_GridRules_Rules" class="HeaderLabel" style="width: 45%"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <ComponentArt:CallBack ID="CallBack_GridRules_Rules" OnCallback="CallBack_GridRules_Rules_OnCallBack"
                                                                        runat="server">
                                                                        <Content>
                                                                            <ComponentArt:DataGrid ID="GridRules_Rules" runat="server" AllowHorizontalScrolling="true"
                                                                                CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                                                                FooterCssClass="GridFooter" Height="150" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                                                PageSize="10" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                                                ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                                                ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="200">
                                                                                <Levels>
                                                                                    <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                                        AllowSorting="false" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                                        RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                                        SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                                        DataKeyField="ID" SortImageWidth="9" HoverRowCssClass="HoverRow">
                                                                                        <Columns>
                                                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                            <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending" meta:resourcekey="clmnRuleName_GridRules_Rules" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                                            <ComponentArt:GridColumn Align="Center" DataField="IdentifierCode" DefaultSortDirection="Descending" meta:resourcekey="clmnRuleIdentifierCode_GridRules_Rules" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                                            <ComponentArt:GridColumn DataField="TypeId" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                                            <ComponentArt:GridColumn Align="Center" DataField="Type" DefaultSortDirection="Descending" meta:resourcekey="clmnRuleType_GridRules_Rules" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                                            <ComponentArt:GridColumn DataField="UserDefined" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="Script" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="CSharpCode" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="JsonObject" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="CustomCategoryCode" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="RuleStateObject" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="Order" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="RuleParametersObject" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="RuleVariablesObject" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="DesignedRuleID" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="operationalArea" Visible="false" />
                                                                                            <ComponentArt:GridColumn DataField="operationalAreaName" Align="Center" DefaultSortDirection="Descending" meta:resourcekey="clmnRuleType_GridScope_Rules" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                                        </Columns>
                                                                                    </ComponentArt:GridLevel>
                                                                                </Levels>
                                                                                <ClientEvents>
                                                                                    <ItemSelect EventHandler="GridRules_Rules_onItemSelect" />
                                                                                    <Load EventHandler="GridRules_Rules_onLoad" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:DataGrid>
                                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Rules" />
                                                                            <asp:HiddenField runat="server" ID="hfRulesCount_Rules" />
                                                                            <asp:HiddenField runat="server" ID="hfRulesPageCount_Rules" />
                                                                        </Content>
                                                                        <ClientEvents>
                                                                            <CallbackComplete EventHandler="CallBack_GridRules_Rules_OnCallbackComplete" />
                                                                            <CallbackError EventHandler="CallBack_GridRules_Rules_onCallbackError" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:CallBack>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td id="Td1" runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                                                <ComponentArt:ToolBar runat="server" ID="TlbPaging_GridRules_Rules" CssClass="toolbar"
                                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                                    Style="direction: ltr" UseFadeEffect="false">
                                                                                    <Items>
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridRules_Rules"
                                                                                            runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="refresh.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbItemFirst_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="first.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbItemBefore_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="Before.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbItemNext_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="Next.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridRules_Rules" runat="server"
                                                                                            ClientSideCommand="tlbItemLast_TlbPaging_GridRules_Rules_onClick();"
                                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                                            ImageUrl="last.png" ImageWidth="16px" RuleType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridRules_Rules"
                                                                                            TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                                    </Items>
                                                                                </ComponentArt:ToolBar>
                                                                            </td>
                                                                            <td id="footer_GridRules_Rules" class="WhiteLabel" style="width: 25%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 40%">
                                                        <table style="width: 99%;" class="BoxStyle">
                                                            <tr>
                                                                <td class="DetailsBoxHeaderStyle" colspan="3">
                                                                    <div id="header_RuleDetails_Rules" runat="server" meta:resourcekey="AlignObj"
                                                                        class="BoxContainerHeader" style="color: White; width: 100%; height: 100%">
                                                                        جزئیات قانون
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleName_Rules" runat="server" meta:resourcekey="lblRuleName_Rules"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="txtRuleName_Rules" type="text" runat="server" style="width: 99%;" class="TextBoxes" disabled="disabled" />
                                                                </td>
                                                            </tr>
                                                            <%--    
                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleCode_Rules" runat="server" meta:resourcekey="lblRuleCode_Rules"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="txtRuleCode_Rules" type="text" runat="server" style="width: 99%;" class="TextBoxes" disabled="disabled"
                                                                        onchange="txtRuleCode_Rules_onChange()" />
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRulePriority" runat="server">الویت قانون</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="txtRulePriority" type="text" runat="server" style="width: 99%;" class="TextBoxes" disabled="disabled"
                                                                        onchange="" />
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleScope_RulesManagement" runat="server">حوزه عمل قانون</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleScope_Rules"
                                                                        Height="26">
                                                                        <Content>
                                                                            <ComponentArt:ComboBox ID="cmbRuleScope_Rules" runat="server" AutoComplete="true"
                                                                                AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                                DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                ItemCssClass="comboitem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover"
                                                                                Style="width: 99%" TextBoxCssClass="comboTextBox" Enabled="false">
                                                                                <ClientEvents>
                                                                                   <Expand EventHandler="cmbRuleScope_Rules_onExpand" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:ComboBox>
                                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_ScopeFields" />
                                                                        </Content>
                                                                        <ClientEvents>
                                                                            <BeforeCallback EventHandler="cmbRuleScope_Rules_onBeforeCallback" />
                                                                            <CallbackComplete EventHandler="cmbRuleScope_Rules_onCallbackComplete" />
                                                                            <CallbackError EventHandler="cmbRuleScope_Rules_onCallbackError" />
                                                                        </ClientEvents>
                                                                    </ComponentArt:CallBack>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <td class="WhiteLabel">
                                                                    <asp:Label ID="lblRuleType_Rules" runat="server" meta:resourcekey="lblRuleType_Rules"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleType_Rules" OnCallback="CallBack_cmbRuleType_Rules_onCallBack"
                                                                                    Height="26">
                                                                                    <Content>
                                                                                        <ComponentArt:ComboBox ID="cmbRuleType_Rules" runat="server" AutoComplete="true"
                                                                                            AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown"
                                                                                            DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png"
                                                                                            DropImageUrl="Images/ComboBox/ddn.png" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover"
                                                                                            ItemCssClass="comboitem" ItemHoverCssClass="comboItemHover" Enabled="false" SelectedItemCssClass="comboItemHover"
                                                                                            Style="width: 99%" TextBoxCssClass="comboTextBox">
                                                                                            <ClientEvents>
                                                                                                <Expand EventHandler="cmbRuleType_Rules_onExpand" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:ComboBox>
                                                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_TypeFields" />
                                                                                    </Content>
                                                                                    <ClientEvents>
                                                                                        <BeforeCallback EventHandler="cmbRuleType_Rules_onBeforeCallback" />
                                                                                        <CallbackComplete EventHandler="cmbRuleType_Rules_onCallbackComplete" />
                                                                                        <CallbackError EventHandler="cmbRuleType_Rules_onCallbackError" />
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
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink runat="server" NavigateUrl="~/RuleGenerator/GTS.Clock.Business.UserCalculator.DLL" Text="دانلود کتابخانه قوانین طراحی شده" ID="DownloadDLL_Rulesmanagement"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <iframe runat="server" id="DLLIframe_RulesManagement" style="display: none"></iframe>

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
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbCancel"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <iframe id="hidden_IFrame_Rules" runat="server" style="width: 0px; height: 0px"></iframe>
        <asp:HiddenField runat="server" ID="hfTitle_DialogRulesManagement" meta:resourcekey="hfTitle_DialogRulesManagement" />
        <asp:HiddenField runat="server" ID="hfheader_RulesBox_Rules" meta:resourcekey="hfheader_RulesBox_Rules" />
        <asp:HiddenField runat="server" ID="hfHeaderRule_Rules" meta:resourcekey="hfRule_Rules" />
        <asp:HiddenField runat="server" ID="hfHeaderRuleParameter_Rules" meta:resourcekey="hfRuleParameter_Rules" />
        <asp:HiddenField runat="server" ID="hfStateView_Rules" meta:resourcekey="hfView_Rules" />
        <asp:HiddenField runat="server" ID="hfStateAdd_Rules" meta:resourcekey="hfAdd_Rules" />
        <asp:HiddenField runat="server" ID="hfStateEdit_Rules" meta:resourcekey="hfEdit_Rules" />
        <asp:HiddenField runat="server" ID="hfStateDelete_Rules" meta:resourcekey="hfDelete_Rules" />
        <asp:HiddenField runat="server" ID="hfStateErrorType_Rules" meta:resourcekey="hfErrorType_Rules" />
        <asp:HiddenField runat="server" ID="hfStateConnectionError_Rules" meta:resourcekey="hfConnectionError_Rules" />
        <asp:HiddenField runat="server" ID="hfDeleteMessageRule_Rules" meta:resourcekey="hfDeleteMessage_Rules" />
        <asp:HiddenField runat="server" ID="hfDeleteMessageRuleParam_Rules" meta:resourcekey="hfDeleteMessageRuleParam_Rules" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Rules" meta:resourcekey="hfCloseMessage_Rules" />
        <asp:HiddenField runat="server" ID="hfRulesPageSize_Rules" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRules_Rules" meta:resourcekey="hfloadingPanel_GridRules_Rules" />
        <asp:HiddenField runat="server" ID="hfJsonEnum_Type" />
        <asp:HiddenField runat="server" ID="hfJsonRuleParameterTypeEnum" />
        <asp:HiddenField runat="server" ID="hfheader_RuleDetails_Rules" meta:resourcekey="hfheader_RuleDetails_Rules" />
        <asp:HiddenField runat="server" ID="hfheader_RuleParametersDetails_Rules" meta:resourcekey="hfheader_RuleParametersDetails_Rules" />
        <asp:HiddenField runat="server" ID="hfCallBackDataSaveRules_Rules" />
        <asp:HiddenField runat="server" ID="hfRuleTypeEnum" />
    </form>
</body>
</html>

