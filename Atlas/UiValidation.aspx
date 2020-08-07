<%@ Page Language="C#" AutoEventWireup="true" Inherits="UiValidation" Codebehind="UiValidation.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/combobox.css"  runat="server" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UiValidationForm" runat="server" meta:resourcekey="UiValidationForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 60%">
                                <ComponentArt:ToolBar ID="TlbUiValidationIntroduction" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbUiValidationIntroduction" runat="server"
                                            ClientSideCommand="tlbItemNew_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemNew_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbUiValidationIntroduction" runat="server"
                                            ClientSideCommand="tlbItemEdit_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemEdit_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbUiValidationIntroduction" runat="server"
                                            ClientSideCommand="tlbItemDelete_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemDelete_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSetLawParameter_TlbUiValidationIntroduction" runat="server"
                                            ClientSideCommand="tlbItemSetLawParameter_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSetLawParameter_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUiValidationIntroduction" runat="server"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" ClientSideCommand="tlbItemHelp_TlbUiValidationIntroduction_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbUiValidationIntroduction" runat="server" Enabled="false"
                                            ClientSideCommand="tlbItemSave_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbUiValidationIntroduction" runat="server" Enabled="false"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbUiValidation_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" Visible="true" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUiValidationIntroduction"
                                            runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbUiValidationIntroduction_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUiValidationIntroduction" runat="server"
                                            ClientSideCommand="tlbItemExit_TlbUiValidation_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbUiValidationIntroduction"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_UiValidation" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 60%">
                                <table style="width: 100%" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_UiValidation_UiValidation" class="HeaderLabel" style="width: 50%">UiValidation
                                                    </td>
                                                    <td id="loadingPanel_GridUiValidation_UiValidation" class="HeaderLabel" style="width: 45%"></td>
                                                    <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                        <ComponentArt:ToolBar ID="TlbRefresh_GridUiValidation_UiValidation" runat="server"
                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridUiValidation_UiValidation"
                                                                    runat="server" ClientSideCommand="Refresh_GridUiValidation_UiValidation();" DropDownImageHeight="16px"
                                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridUiValidation_UiValidation"
                                                                    TextImageSpacing="5" />
                                                            </Items>
                                                        </ComponentArt:ToolBar>
                                                    </td>
                                                </tr>
                                            </table>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_GridUiValidation_UiValidation"
                                                OnCallback="CallBack_GridUiValidation_UiValidation_Callback">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridUiValidation_UiValidation" runat="server" CssClass="Grid"
                                                        EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                        PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="5" RunningMode="Client"
                                                        SearchTextCssClass="GridHeaderText" Width="100%" AllowMultipleSelect="false"
                                                        ShowFooter="false" AllowColumnResizing="false" ScrollBar="On" ScrollTopBottomImagesEnabled="true"
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
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="CustomCode" DefaultSortDirection="Descending"
                                                                        HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationCode_GridUiValidation"
                                                                        Width="30" TextWrap="true" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                        HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationName_GridUiValidation"
                                                                        Width="200" TextWrap="true" />
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientEvents>
                                                            <ItemSelect EventHandler="GridUiValidation_UiValidation_onItemSelect" />
                                                            <Load EventHandler="GridUiValidation_UiValidation_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField ID="ErrorHiddenField_UiValidation" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridUiValidation_UiValidation_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridUiValidation_UiValidation_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width: 100%;" id="tblUiValidation_UiValidationIntroduction" class="BoxStyle">
                                    <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                        <td class="DetailsBoxHeaderStyle">
                                            <div id="header_tblUiValidation_UiValidationIntroduction" runat="server" class="BoxContainerHeader"
                                                meta:resourcekey="AlignObj" style="width: 100%; height: 100%">
                                                UiValidation Details
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <asp:Label ID="lblUiValidationCode_UiValidationIntroduction" runat="server" meta:resourcekey="lblUiValidationCode_UiValidationIntroduction"
                                                Text="کد :" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr5" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtUiValidationCode_UiValidationIntroduction"
                                                disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <asp:Label ID="lblUiValidationName_UiValidationIntroduction" runat="server" meta:resourcekey="lblUiValidationName_UiValidationIntroduction"
                                                Text="نام واسط کاربری :" CssClass="WhiteLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                        <td>
                                            <input type="text" runat="server" style="width: 98%;" class="TextBoxes" id="txtUiValidationName_UiValidationIntroduction"
                                                disabled="disabled" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--  new--%>
            <tr>
                <td>
                    <table style="width: 98%" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_UiValidationRules_UiValidationIntroduction" class="HeaderLabel" style="width: 35%">UiValidation Rules
                                        </td>
                                        <td id="loadingPanel_GridUiValidationRules_UiValidationIntroduction" class="HeaderLabel"
                                            style="width: 25%"></td>
                                        <%-- group--%>
                                        <td style="width:35%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width:20%">
                                                        <asp:Label ID="lblRuleGroupUiValidationRules_UiValidationIntroduction" runat="server" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblRuleGroupUiValidationRules_UiValidationIntroduction" Text=": گروه قوانین"></asp:Label>
                                                    </td>
                                                    <td style="width: 80%">
                                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleGroup_UiValidationIntroduction" OnCallback="CallBack_cmbRuleGroup_UiValidationIntroduction_onCallback"
                                                            Height="26">
                                                            <Content>
                                                                <ComponentArt:ComboBox ID="cmbRuleGroup_UiValidationIntroduction" runat="server" AutoComplete="true"
                                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                                    <ClientEvents>
                                                                        <Expand EventHandler="cmbRuleGroup_UiValidationIntroduction_onExpand" />
                                                                        <Collapse EventHandler="cmbRuleGroup_UiValidationIntroduction_onCollapse" />
                                                                        <Change EventHandler="cmbRuleGroup_UiValidationIntroduction_onChange" />
                                                                    </ClientEvents>
                                                                </ComponentArt:ComboBox>
                                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleGroup" />
                                                            </Content>
                                                            <ClientEvents>
                                                                <BeforeCallback EventHandler="CallBack_cmbRuleGroup_UiValidationIntroduction_onBeforeCallback" />
                                                                <CallbackComplete EventHandler="CallBack_cmbRuleGroup_UiValidationIntroduction_onCallbackComplete" />
                                                                <CallbackError EventHandler="CallBack_cmbRuleGroup_UiValidationIntroduction_onCallbackError" />
                                                            </ClientEvents>
                                                        </ComponentArt:CallBack>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%-- endgroup--%>
                                        <td id="Td2" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridUiValidationRules" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridUiValidationRules_UiValidationIntroduction"
                                                        runat="server" ClientSideCommand="tlbItemRefresh_GridUiValidationRules_UiValidationIntroduction();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridUiValidationRules_UiValidationIntroduction"
                                                        TextImageSpacing="5"  Enabled="false"/>
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridUiValidationRules_UiValidationIntroduction"
                                    OnCallback="CallBack_GridUiValidationRules_UiValidationIntroduction_Callback">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridUiValidationRules_UiValidationIntroduction" runat="server"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="8" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
                                            AllowMultipleSelect="false" ShowFooter="false" AllowColumnResizing="false" ScrollBar="On"
                                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2" ScrollTopBottomImageWidth="16"
                                            ScrollImagesFolderUrl="images/Grid/scroller/" ScrollButtonWidth="16" ScrollButtonHeight="17"
                                            ScrollBarCssClass="ScrollBar" ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                            <Levels>
                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                    DataKeyField="ID" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                    SortImageWidth="9">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn DataField="RuleColor" HeadingText=" " DataCellClientTemplateId="DataCellClientTemplate_clmnRuleColor_GridUiValidationRules_UiValidationIntroduction"/>                                                        
                                                         <ComponentArt:GridColumn DataField="CustomCode" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn DataField="IsWarningAllowed" Visible="false" ColumnType="CheckBox" />
                                                        <ComponentArt:GridColumn DataField="RuleID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                         <ComponentArt:GridColumn DataField="GroupID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                         <ComponentArt:GridColumn DataField="RuleGroupID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn DataField="RuleType" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                         <ComponentArt:GridColumn DataField="RuleGroupType" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Active" DefaultSortDirection="Descending"
                                                            HeadingText="فعال" ColumnType="CheckBox" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationRulesActive_GridUiValidationRules"
                                                            AllowEditing="True" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RuleName" DefaultSortDirection="Descending"
                                                            HeadingText="متن قانون" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationRulesContent_GridUiValidationRules" TextWrap="true" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="Warning" DefaultSortDirection="Descending"
                                                            HeadingText="هشدار" ColumnType="CheckBox" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationRulesWarning_GridUiValidationRules"
                                                            AllowEditing="True" />
                                                         <ComponentArt:GridColumn  DataField="ExistRuleTag"  Visible ="false" ColumnType="CheckBox" />                                                                                                                                                                                
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID ="DataCellClientTemplate_clmnRuleColor_GridUiValidationRules_UiValidationIntroduction">
                                                    <table style ="width: 100% ; height:17px; background-color : ##DataItem.GetMember('RuleColor').Value##">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                             <ClientEvents>
                                                <Load EventHandler="GridUiValidationRules_UiValidationIntroduction_onLoad" />
                                                <ItemDoubleClick  EventHandler = "GridUiValidationRules_UiValidationIntroduction_onItemDoubleClick"/>
                                                <ItemCheckChange  EventHandler = "GridUiValidationRules_UiValidationIntroduction_onItemCheckChange" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField ID="ErrorHiddenField_GridUiValidationRules_UiValidationIntroduction" runat="server" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridUiValidationRules_UiValidationIntroduction_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridUiValidationRules_UiValidationIntroduction_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>

            </tr>
            <%--end new--%>
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
       <%-- start dialog--%>
         <ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogParameterValue"
            runat="server" Width="200px">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
                    meta:resourcekey="tblParameterValue_DialogParameterValue"
                    class="BodyStyle">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbParameterValue" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemExit_TlbParameterValue" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbParameterValue_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbParameterValue"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td
                                         <%--id="header_ParameterValue_UiValidationIntroduction" style="color: White; font-weight: bold; font-family: Arial; width: 100%">parameter Value--%>
                                       <%-- <asp:Label ID="lblParameterValue_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblParameterValue_RuleParameters"
                                                                    Text=": عددی"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                   <%--start paramvalue--%>
                                    <td>
                                            <div style="width: 100%" class="TabStripContainer">
                                                <ComponentArt:TabStrip ID="TabStripRuleParametersTerms" runat="server" DefaultGroupTabSpacing="1"
                                                    DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                                                    ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageRuleParametersTerms" ScrollLeftLookId="ScrollItem"
                                                    ScrollRightLookId="ScrollItem" Width="100%">
                                                    <ItemLooks>
                                                        <ComponentArt:ItemLook CssClass="DefaultTab" HoverCssClass="DefaultTabHover" LabelPaddingBottom="4"
                                                            LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="22"
                                                            LeftIconUrl="tab_left_icon.gif" LeftIconWidth="13" LookId="DefaultTabLook" meta:resourcekey="DefaultTabLook"
                                                            RightIconHeight="22" RightIconUrl="tab_right_icon.gif" RightIconWidth="13" />
                                                        <ComponentArt:ItemLook CssClass="SelectedTab" LabelPaddingBottom="4" LabelPaddingLeft="15"
                                                            LabelPaddingRight="15" LabelPaddingTop="4" LeftIconHeight="22" LeftIconUrl="selected_tab_left_icon.gif"
                                                            LeftIconWidth="13" LookId="SelectedTabLook" meta:resourcekey="SelectedTabLook"
                                                            RightIconHeight="22" RightIconUrl="selected_tab_right_icon.gif" RightIconWidth="13" />
                                                        <ComponentArt:ItemLook CssClass="ScrollItem" HoverCssClass="ScrollItemHover" LabelPaddingBottom="0"
                                                            LabelPaddingLeft="5" LabelPaddingRight="5" LabelPaddingTop="0" LookId="ScrollItem" />
                                                    </ItemLooks>
                                                    <Tabs>
                                                        <ComponentArt:TabStripTab ID="tbNumeric_TabStripRuleParametersTerms" meta:resourcekey="tbNumeric_TabStripRuleParametersTerms"
                                                            Text="عددی" Value="Numeric" Enabled="false">
                                                        </ComponentArt:TabStripTab>
                                                        <ComponentArt:TabStripTab ID="tbTime_TabStripRuleParametersTerms" meta:resourcekey="tbTime_TabStripRuleParametersTerms"
                                                            Text="زمان" Value="Time" Enabled="false">
                                                        </ComponentArt:TabStripTab>
                                                        <ComponentArt:TabStripTab ID="tbDate_TabStripRuleParametersTerms" meta:resourcekey="tbDate_TabStripRuleParametersTerms"
                                                            Text="تاریخ" Value="Date" Enabled="false">
                                                        </ComponentArt:TabStripTab>
                                                    </Tabs>
                                                </ComponentArt:TabStrip>
                                            </div>
                                            <ComponentArt:MultiPage ID="MultiPageRuleParametersTerms" runat="server" CssClass="MultiPage"
                                                Width="300">
                                                <ComponentArt:PageView ID="pgvNumeric_MultiPageRuleParametersTerms" runat="server"
                                                    Width="100%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblNumeric_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblNumeric_RuleParameters"
                                                                    Text=": عددی"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <input id="txtNumeric_RuleParameters" runat="server" type="text" class="TextBoxes"
                                                                                style="width: 85px;" disabled="disabled"   />
                                                                        </td>
                                                                        <td align="center">
                                                                            <ComponentArt:ToolBar ID="TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms"
                                                                                        runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                                        Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRuleParametersTerms"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:PageView>
                                                <ComponentArt:PageView ID="pgvTime_MultiPageRuleParametersTerms" runat="server" Width="100%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTime_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTime_RuleParameters"
                                                                    Text=": زمان"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 90%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 60%">
                                                                                        <table style="width: 100%" dir="ltr">
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <input type="text" id="TimeSelector_Hour_RuleParameters_txtHour" style="width:70%; text-align: center"
                                                                                                        onchange="TimeSelector_Hour_RuleParameters_onChange('txtHour')"
                                                                                                          />
                                                                                                </td>
                                                                                                <td align="center">:
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <input type="text" id="TimeSelector_Hour_RuleParameters_txtMinute" style="width:70%; text-align: center"
                                                                                                        onchange="TimeSelector_Hour_RuleParameters_onChange('txtMinute')"
                                                                                                          />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td style="width: 10%">
                                                                                                    <input id="chbNextDay_RuleParameters" type="checkbox" /></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblNextDay_RuleParameters" runat="server" CssClass="WhiteLabel" Text="روز بعد" meta:resourcekey="lblNextDay_RuleParameters"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="center">
                                                                            <ComponentArt:ToolBar ID="TlbConfirm_pgvTime_MultiPageRuleParametersTerms" runat="server"
                                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                                <Items>
                                                                                    <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms"
                                                                                        runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms_onClick();"
                                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                                        Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRuleParametersTerms"
                                                                                        TextImageSpacing="5" />
                                                                                </Items>
                                                                            </ComponentArt:ToolBar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:PageView>
                                                <ComponentArt:PageView ID="pgvDate_MultiPageRuleParametersTerms" runat="server" Width="100%">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblDate_RuleParameters" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDate_RuleParameters"
                                                                    Text=": تاریخ"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Container_DateCalendars_RuleParameters">
                                                                <table runat="server" id="Container_pdpDate_RuleParameters" visible="false" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <%--<pcal:persiandatepickup id="pdpdate_ruleparameters" runat="server" cssclass="persiandatepicker"
                                                                                readonly="true"></pcal:persiandatepickup>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table runat="server" id="Container_gdpDate_RuleParameters" visible="false" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <table id="Container_gCalDate_RuleParameters" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td onmouseup="btn_gdpDate_RuleParameters_OnMouseUp(event)">
                                                                                        <ComponentArt:Calendar ID="gdpDate_RuleParameters" runat="server" ControlType="Picker"
                                                                                            PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                            SelectedDate="2008-1-1">
                                                                                            <ClientEvents>
                                                                                                <SelectionChanged EventHandler="gdpDate_RuleParameters_OnDateChange" />
                                                                                            </ClientEvents>
                                                                                        </ComponentArt:Calendar>
                                                                                    </td>
                                                                                    <td style="font-size: 10px;">&nbsp;
                                                                                    </td>
                                                                                    <td>
                                                                                        <img id="btn_gdpDate_RuleParameters" alt="" class="calendar_button" onclick="btn_gdpDate_RuleParameters_OnClick(event)"
                                                                                            onmouseup="btn_gdpDate_RuleParameters_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <ComponentArt:Calendar ID="gCalDate_RuleParameters" runat="server" AllowMonthSelection="false"
                                                                                AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                                CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                                DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                                MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                                OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_RuleParameters"
                                                                                PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                                SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                                <ClientEvents>
                                                                                    <SelectionChanged EventHandler="gCalDate_RuleParameters_OnChange" />
                                                                                    <Load EventHandler="gCalDate_RuleParameters_onLoad" />
                                                                                </ClientEvents>
                                                                            </ComponentArt:Calendar>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <ComponentArt:ToolBar ID="TlbConfirm_pgvDate_MultiPageRuleParametersTerms" runat="server"
                                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms"
                                                                            runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms_onClick();"
                                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png"
                                                                            Enabled="false" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRuleParametersTerms"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ComponentArt:PageView>
                                            </ComponentArt:MultiPage>
                                        </td>
                                   <%-- end paramvalue--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>
        <%--end dialog--%> 
        <asp:HiddenField runat="server" ID="hfheader_tblUiValidationDetails_UiValidationIntroduction"
            meta:resourcekey="hfheader_tblUiValidationDetails_UiValidationIntroduction" />
        <asp:HiddenField runat="server" ID="hfheader_UiValidation_UiValidation" meta:resourcekey="hfheader_UiValidation_UiValidation" />
        <asp:HiddenField runat="server" ID="hfView_UiValidation" meta:resourcekey="hfView_UiValidation" />
        <asp:HiddenField runat="server" ID="hfAdd_UiValidation" meta:resourcekey="hfAdd_UiValidation" />
        <asp:HiddenField runat="server" ID="hfEdit_UiValidation" meta:resourcekey="hfEdit_UiValidation" />
        <asp:HiddenField runat="server" ID="hfDelete_UiValidation" meta:resourcekey="hfDelete_UiValidation" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_UiValidation" meta:resourcekey="hfDeleteMessage_UiValidation" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_UiValidation" meta:resourcekey="hfCloseMessage_UiValidation" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridUiValidation_UiValidation"
            meta:resourcekey="hfloadingPanel_GridUiValidation_UiValidation" />
        <asp:HiddenField runat="server" ID="hfErrorType_UiValidation" meta:resourcekey="hfErrorType_UiValidation" />
        <asp:HiddenField runat="server" ID="hfConnectionError_UiValidation" meta:resourcekey="hfConnectionError_UiValidation" />
        <asp:HiddenField runat="server" ID="hfheader_UiValidationRules_UiValidationIntroduction" meta:resourcekey="hfheader_UiValidationRules_UiValidationIntroduction" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridUiValidationRules_UiValidationIntroduction"
            meta:resourcekey="hfloadingPanel_GridUiValidationRules_UiValidationIntroduction" />
        <asp:HiddenField runat="server" ID="hfheader_ParameterValue_UiValidationIntroduction"
            meta:resourcekey="hfheader_ParameterValue_UiValidationIntroduction" />
    </form>
</body>
</html>
