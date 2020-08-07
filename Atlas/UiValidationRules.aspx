<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="UiValidationRules" Codebehind="UiValidationRules.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/toolbar.css" type="text/css" rel="stylesheet" />
    <link href="css/gridStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/tabStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/calendarStyle.css" type="text/css" rel="stylesheet" />
    <link href="css/multiPage.css" type="text/css" rel="stylesheet" />
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="UiValidationRulesForm" runat="server" meta:resourcekey="UiValidationRulesForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; font-family: Arial; font-size: small" class="BoxStyle">
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbUiValidationRules" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbUiValidationRules" runat="server" ClientSideCommand="tlbItemSave_TlbUiValidationRules_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbUiValidationRules"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbUiValidationRules" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemHelp_TlbUiValidationRules_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbUiValidationRules"
                                            TextImageSpacing="5" Visible="true" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbUiValidationRules" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbUiValidationRules_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbUiValidationRules"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbUiValidationRules" runat="server" ClientSideCommand="tlbItemExit_TlbUiValidationRules_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbUiValidationRules"
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
                           <td style="width: 50%;">
                               <table style="width: 100%;">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblNameUiValidationRules_UiValidationRules" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblNameUiValidationRules_UiValidationRules" Text=": نام واسط کاربری"></asp:Label>
                            </td>
                            <td style="width: 70%">
                                <input id="txtNameUiValidationRules_UiValidationRules" runat="server" class="TextBoxes"
                                    disabled="disabled" style="width: 95%;" type="text" 
                                     />
                            </td>
                        </tr>
                    </table>
                           </td>
                           <td style="width: 50%;">
                                 <table style="width: 100%;">
                        <tr>
                            <td style="width: 30%">
                                <asp:Label ID="lblRuleGroupUiValidationRules_UiValidationRules" runat="server" CssClass="WhiteLabel"
                                    meta:resourcekey="lblRuleGroupUiValidationRules_UiValidationRules" Text=": گروه قوانین"></asp:Label>
                            </td>
                            <td style="width: 70%">
                               <ComponentArt:CallBack runat="server" ID="CallBack_cmbRuleGroup_UiValidationRules" OnCallback="CallBack_cmbRuleGroup_UiValidationRules_onCallback"
                                    Height="26">
                                    <Content>
                                        <ComponentArt:ComboBox ID="cmbRuleGroup_UiValidationRules" runat="server" AutoComplete="true"
                                            DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                            CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                            DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                            FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                            ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width:100%"
                                            TextBoxCssClass="comboTextBox" Enabled="true" TextBoxEnabled="true">
                                            <ClientEvents>
                                                <Expand EventHandler="cmbRuleGroup_UiValidationRules_onExpand" />
                                                <Collapse EventHandler="cmbRuleGroup_UiValidationRules_onCollapse" />
                                                <Change EventHandler="cmbRuleGroup_UiValidationRules_onChange" />
                                            </ClientEvents>
                                        </ComponentArt:ComboBox>
                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RuleGroup" />
                                    </Content>
                                    <ClientEvents>
                                        <BeforeCallback EventHandler="CallBack_cmbRuleGroup_UiValidationRules_onBeforeCallback" />
                                        <CallbackComplete EventHandler="CallBack_cmbRuleGroup_UiValidationRules_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_cmbRuleGroup_UiValidationRules_onCallbackError" />
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
                <td style="width: 100%">
                    <table style="width: 90%" class="BoxStyle">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td id="header_UiValidationRules_UiValidationRules" class="HeaderLabel" style="width: 50%">UiValidation Rules
                                        </td>
                                        <td id="loadingPanel_GridUiValidationRules_UiValidationRules" class="HeaderLabel"
                                            style="width: 45%"></td>
                                        <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                            <ComponentArt:ToolBar ID="TlbRefresh_GridUiValidationRules_UiValidationRules" runat="server"
                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridUiValidationRules_UiValidationRules"
                                                        runat="server" ClientSideCommand="Refresh_GridUiValidationRules_UiValidationRules();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridUiValidationRules_UiValidationRules"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                    </tr>
                                </table>
                                <ComponentArt:CallBack runat="server" ID="CallBack_GridUiValidationRules_UiValidationRules"
                                    OnCallback="CallBack_GridUiValidationRules_UiValidationRules_Callback">
                                    <Content>
                                        <ComponentArt:DataGrid ID="GridUiValidationRules_UiValidationRules" runat="server"
                                            CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                            ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                            PageSize="5" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
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
                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                        <ComponentArt:GridColumn DataField="RuleID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                        <ComponentArt:GridColumn Align="Center" DataField="Active" DefaultSortDirection="Descending"
                                                            HeadingText="فعال" ColumnType="CheckBox" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationRulesActive_GridUiValidationRules"
                                                            AllowEditing="True" />
                                                        <ComponentArt:GridColumn Align="Center" DataField="RuleName" DefaultSortDirection="Descending"
                                                            HeadingText="متن قانون" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnUiValidationRulesContent_GridUiValidationRules" TextWrap="true"/>
                                                        <ComponentArt:GridColumn Align="Center" DataField="OpratorRestriction" DefaultSortDirection="Descending"
                                                            HeadingText="اعمال اپراتور" ColumnType="CheckBox" HeadingTextCssClass="HeadingText"
                                                            meta:resourcekey="clmnUiValidationRulesAction_GridUiValidationRules" AllowEditing="True" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientEvents>
                                                <ItemSelect EventHandler="GridUiValidationRules_UiValidationRules_onItemSelect" />
                                                <Load EventHandler="GridUiValidationRules_UiValidationRules_onLoad" />
                                            </ClientEvents>
                                        </ComponentArt:DataGrid>
                                        <asp:HiddenField ID="ErrorHiddenField_UiValidationRules" runat="server" />
                                    </Content>
                                    <ClientEvents>
                                        <CallbackComplete EventHandler="CallBack_GridUiValidationRules_UiValidationRules_onCallbackComplete" />
                                        <CallbackError EventHandler="CallBack_GridUiValidationRules_UiValidationRules_onCallbackError" />
                                    </ClientEvents>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbParameterUiValidationRules" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbParameterUiValidationRules" runat="server"
                                            ClientSideCommand="tlbItemSave_TlbParameterUiValidationRules_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbParameterUiValidationRules"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbParameterUiValidationRules" runat="server"
                                            DropDownImageHeight="16px" ClientSideCommand="tlbItemCancel_TlbUiValidationRules_onClick();"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemCancel_TlbParameterUiValidationRules"
                                            TextImageSpacing="5" Visible="true" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 50%">
                                <table style="width: 100%" class="BoxStyle">
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td id="header_ParameterUiValidationRules_UiValidationRules" class="HeaderLabel"
                                                        style="width: 50%">parameter UiValidation
                                                    </td>
                                                    <td id="loadingPanel_GridParameterUiValidationRules_UiValidationRules" class="HeaderLabel"
                                                        style="width: 45%"></td>
                                                    <td id="Td4" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                            <ComponentArt:CallBack runat="server" ID="CallBack_GridParameterUiValidationRules_UiValidationRules"
                                                OnCallback="CallBack_GridParameterUiValidationRules_UiValidationRules_Callback">
                                                <Content>
                                                    <ComponentArt:DataGrid ID="GridParameterUiValidationRules_UiValidationRules" runat="server"
                                                        CssClass="Grid" EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter"
                                                        ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerTextCssClass="GridFooterText"
                                                        PageSize="5" RunningMode="Client" SearchTextCssClass="GridHeaderText" Width="100%"
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
                                                                    <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                    <ComponentArt:GridColumn DataField="Type" Visible="false" />
                                                                    <ComponentArt:GridColumn DataField="KeyName" Visible="false" />
                                                                    <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                        HeadingText="پارامتر" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterUiValidationRulesParameter_GridUiValidationRules"
                                                                        Width="150" TextWrap="true"/>
                                                                    <ComponentArt:GridColumn Align="Center" DataField="TheValue" DefaultSortDirection="Descending"
                                                                        HeadingText="مقدار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnParameterUiValidationRulesValue_GridUiValidationRules"
                                                                        Width="200" />
                                                                    <ComponentArt:GridColumn DataField="ContinueOnTomorrow" ColumnType="CheckBox" Visible="false"/>
                                                                </Columns>
                                                            </ComponentArt:GridLevel>
                                                        </Levels>
                                                        <ClientEvents>
                                                            <ItemSelect EventHandler="GridParameterUiValidationRules_UiValidationRules_onItemSelect" />
                                                            <Load EventHandler="GridParameterUiValidationRules_UiValidationRules_onLoad" />
                                                        </ClientEvents>
                                                    </ComponentArt:DataGrid>
                                                    <asp:HiddenField ID="ErrorHiddenField_ParameterUiValidationRules" runat="server" />
                                                </Content>
                                                <ClientEvents>
                                                    <CallbackComplete EventHandler="CallBack_GridParameterUiValidationRules_UiValidationRules_onCallbackComplete" />
                                                    <CallbackError EventHandler="CallBack_GridParameterUiValidationRules_UiValidationRules_onCallbackError" />
                                                </ClientEvents>
                                            </ComponentArt:CallBack>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table style="width: 50%">
                                    <tr>
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
                                                                            <pcal:PersianDatePickup ID="pdpDate_RuleParameters" runat="server" CssClass="PersianDatePicker"
                                                                                ReadOnly="true"></pcal:PersianDatePickup>
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
        <%--  Start  DialogPrecardIntroduction--%>
        <%--<ComponentArt:Dialog ModalMaskImage="Images/Dialog/alpha.png" HeaderCssClass="headerCss"
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogPrecardIntroduction"
            runat="server" Width="500px "   >
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 80%;"
                      meta:resourcekey="tblGridPrecardIntroduction_DialogPrecardIntroduction"
                    class="BodyStyle">
                      
                    <tr>
                        <td style="width: 100%">
                            <ComponentArt:ToolBar ID="TlbGridPrecardIntroduction" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGridPrecardIntroduction" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbGridPrecardIntroduction_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbGridPrecardIntroduction"
                                        TextImageSpacing="5" Enabled="true" />
                                    <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbGridPrecardIntroduction" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemHelp_TlbGridPrecardIntroduction_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbGridPrecardIntroduction"
                                        TextImageSpacing="5" />
                                       <ComponentArt:ToolBarItem ID="tlbItemExit_TlbGridPrecardIntroduction" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemExit_TlbGridPrecardIntroduction_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbGridPrecardIntroduction"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td id="header_GridPrecardIntroduction_UiValidationRules" style="color: White; font-weight: bold; font-family: Arial; width: 100%"   meta:resourcekey="header_GridPrecardIntroduction_UiValidationRules">PrecardIntroduction
                                    </td>--%>
                                    <%-- refresh PrecardIntroduction--%>
                                  <%--  <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                        <ComponentArt:ToolBar ID="TlbRefresh_GridPrecardIntroduction_UiValidationRules" runat="server"
                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridPrecardIntroduction_UiValidationRules"
                                                    runat="server" ClientSideCommand="Refresh_GridPrecardIntroduction_UiValidationRules();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridPrecardIntroduction_UiValidationRules"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                </tr>
                                <tr>
                                    <td  style="width: 100%">
                                        <table style="width: 100%" >
                                            <tr>
                                                <td style="width: 5%">
                                                    <input id="chbSelectAll_GridPrecardIntroduction_UiValidationRules" type="checkbox"
                                                        onclick="chbSelectAll_GridPrecardIntroduction_UiValidationRules_onClick();" />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblSelectAll_GridPrecardIntroduction_UiValidationRules" meta:resourcekey="lblSelectAll_GridPrecardIntroduction_UiValidationRules"
                                                        CssClass="WhiteLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2">
                                        
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridPrecardIntroduction_UiValidationRules"
                                            OnCallback="CallBack_GridPrecardIntroduction_UiValidationRules_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridPrecardIntroduction_UiValidationRules" runat="server" AllowMultipleSelect="false"
                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" Height="100"
                                                    ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true" PagerStyle="Numbered"
                                                    ShowFooter="false" PagerTextCssClass="GridFooterText" PageSize="12" RunningMode="Client"
                                                    SearchTextCssClass="GridHeaderText" TabIndex="0" Width="100" AllowColumnResizing="false"
                                                    ScrollBar="Auto" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                    ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                    ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" >

                                                    <Levels>
                                                        <ComponentArt:GridLevel AllowSorting="false" AlternatingRowCssClass="AlternatingRow"
                                                            DataCellCssClass="DataCell" DataKeyField="ID" HeadingCellCssClass="HeadingCell"
                                                            HeadingTextCssClass="HeadingCellText" HoverRowCssClass="HoverRow" RowCssClass="Row"
                                                            SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                            SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" AllowReordering="false" >
                                                           
                                                            <Columns>--%>
                                                              <%-- <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>--%>
                                                              <%-- <ComponentArt:GridColumn Align="Center" DataField="" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox"  HeadingTextCssClass="HeadingText"  HeadingText=" "
                                                                     AllowEditing="False"
                                                                    />
                                                                <ComponentArt:GridColumn Align="Center" DataField="ID" DefaultSortDirection="Descending"
                                                                    HeadingText="کد پیشکارت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkGroupCode_GridPrecardIntroduction_UiValidationRules"
                                                                 FormatString="###" TextWrap="true"     />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                    HeadingText=" نام پیشکارت" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnWorkGroupName_GridPrecardIntroduction_UiValidationRules" TextWrap="true"   />
                                                                <ComponentArt:GridColumn Align="Center" DataField="IsUsedByUiValidationRule" DefaultSortDirection="Descending"
                                                                    ColumnType="CheckBox"  HeadingTextCssClass="HeadingText"  HeadingText=" "
                                                                     AllowEditing="True"
                                                                    />
                                                            </Columns>
                                                           
                                                        </ComponentArt:GridLevel>
                                                    </Levels>

                                                    <ClientEvents>
                                                        <Load EventHandler="GridPrecardIntroduction_UiValidationRules_onLoad" />
                                                        <ItemCheckChange EventHandler="GridPrecardIntroduction_UiValidationRules_onItemCheckChange" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GridPrecardIntroduction_UiValidationRules" />
                                                <asp:HiddenField runat="server" ID="CheckListHiddenField_UiValidationRules" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridPrecardIntroduction_UiValidationRules_CallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridPrecardIntroduction_UiValidationRules_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                          
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </Content>
        </ComponentArt:Dialog>--%>

        <%--end dialog Assined Work Group--%>
        <asp:HiddenField runat="server" ID="hfheader_tblUiValidationRulesDetails_UiValidationRulesIntroduction"
            meta:resourcekey="hfheader_tblUiValidationRulesDetails_UiValidationRulesIntroduction" />
        <asp:HiddenField runat="server" ID="hfCurrentDate_RuleParameters" />
        <asp:HiddenField runat="server" ID="hfheader_ParameterUiValidationRules_UiValidationRules"
            meta:resourcekey="hfheader_ParameterUiValidationRules_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfheader_UiValidationRules_UiValidationRules"
            meta:resourcekey="hfheader_UiValidationRules_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfView_UiValidationRules" meta:resourcekey="hfView_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfAdd_UiValidationRules" meta:resourcekey="hfAdd_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfEdit_UiValidationRules" meta:resourcekey="hfEdit_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfDelete_UiValidationRules" meta:resourcekey="hfDelete_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_UiValidationRules" meta:resourcekey="hfDeleteMessage_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_UiValidationRules" meta:resourcekey="hfCloseMessage_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridUiValidationRules_UiValidationRules"
            meta:resourcekey="hfloadingPanel_GridUiValidationRules_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridParameterUiValidationRules_UiValidationRules"
            meta:resourcekey="hfloadingPanel_GridParameterUiValidationRules_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfErrorType_UiValidationRules" meta:resourcekey="hfErrorType_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfConnectionError_UiValidationRules" meta:resourcekey="hfConnectionError_UiValidationRules" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogUiValidationRules" meta:resourcekey="hfTitle_DialogUiValidationRules" />
        <asp:HiddenField runat="server" ID="hfcmbAlarm_UiValidationRules" meta:resourcekey="hfcmbAlarm_UiValidationRules" />
    </form>
</body>
</html>
