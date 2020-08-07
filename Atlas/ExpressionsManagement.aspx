<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.ExpressionsManagement" Codebehind="ExpressionsManagement.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/colorPickerStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/treeStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/GridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ExpressionsManagementForm" runat="server" meta:resourcekey="ExpressionsManagementForm">
        <div>
            <table id="tblExpressions_ExpressionsForm" style="width: 99%; height: 400px; font-family: Arial; font-size: small;" class="BoxStyle">
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbExpressions" runat="server" CssClass="toolbar"
                                        DefaultItemActiveCssClass="itemActive"
                                        DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                        DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                        DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemNew_TlbExpressions" runat="server" ClientSideCommand="tlbItemNew_TlbExpressions_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemNew_TlbExpressions"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbExpressions" runat="server" ClientSideCommand="tlbItemEdit_TlbExpressions_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemEdit_TlbExpressions"
                                                TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbExpressions" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemDelete_TlbExpressions_onClick();" DropDownImageWidth="16px"
                                                ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ExpressionType="Command"
                                                meta:resourcekey="tlbItemDelete_TlbExpressions" TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbExpressions" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemSave_TlbExpressions_onClick();" DropDownImageWidth="16px"
                                                ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px" ExpressionType="Command"
                                                meta:resourcekey="tlbItemSave_TlbExpressions" TextImageSpacing="5" Enabled="false" />
                                            <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbExpressions" runat="server" ClientSideCommand="tlbItemCancel_TlbTlbExpressions_onClick();"
                                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                                ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemCancel_TlbExpressions" TextImageSpacing="5"
                                                Enabled="false" />
                                            <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbExpression" runat="server"
                                                ClientSideCommand="tlbItemFormReconstruction_TlbExpressions_onClick();" DropDownImageHeight="16px"
                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbExpression" TextImageSpacing="5" />
                                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbExpressions" runat="server" DropDownImageHeight="16px"
                                                ClientSideCommand="tlbItemExit_TlbExpressions_onClick();" DropDownImageWidth="16px"
                                                ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ExpressionType="Command"
                                                meta:resourcekey="tlbItemExit_TlbExpressions" TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td id="ActionMode_Expressions" class="ToolbarMode"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td class="WhiteLabel" style="width: 10%;">
                                    <asp:Label ID="lblScriptFaBegin_Expressions" runat="server" meta:resourcekey="lblScriptFaBegin_Expressions"></asp:Label>
                                </td>
                                <td style="width: 15%;">
                                    <input id="txtScriptFaBegin_Expressions" type="text" runat="server" class="TextBoxes" disabled="disabled"
                                         style="width: 90%" />
                                </td>
                                <td colspan="2" rowspan="4" width="40%">
                                    <ComponentArt:CallBack runat="server" ID="CallBack_trvExpressions_Expressions" OnCallback="CallBack_trvExpressions_Expressions_onCallBack">
                                        <Content>
                                            <ComponentArt:TreeView ID="trvExpressions_Expressions" runat="server" ExpandNodeOnSelect="true"
                                                CollapseNodeOnSelect="false" CollapseImageUrl="images/TreeView/exp.gif" CssClass="TreeView"
                                                DefaultImageHeight="16" HighlightSelectedPath="true" DefaultImageWidth="16" DragAndDropEnabled="false"
                                                EnableViewState="false" ExpandCollapseImageHeight="15" ExpandCollapseImageWidth="17"
                                                ExpandImageUrl="images/TreeView/col.gif" FillContainer="false" ForceHighlightedNodeID="true"
                                                Height="150" HoverNodeCssClass="HoverNestingTreeNode" ItemSpacing="2" KeyboardEnabled="true"
                                                LineImageHeight="20" LineImageWidth="19" NodeCssClass="TreeNode" NodeEditCssClass="NodeEdit"
                                                LoadingFeedbackText="Loading" NodeIndent="17" NodeLabelPadding="3" meta:resourcekey="trvExpressions_Expressions"
                                                SelectedNodeCssClass="SelectedTreeNode" ShowLines="true">
                                                <ClientEvents>
                                                    <NodeSelect EventHandler="trvExpressions_Expressions_onNodeSelect" />
                                                    <NodeBeforeExpand EventHandler="trvExpressions_Expressions_onNodeBeforeExpand" />
                                                    <NodeExpand EventHandler="trvExpressions_Expressions_onNodeExpand"/>
                                                    <CallbackComplete EventHandler="trvExpressions_Expressions_onCallbackComplete" />
                                                </ClientEvents>
                                            </ComponentArt:TreeView>
                                            <asp:HiddenField ID="ErrorHiddenField_trvExpressions_Expressions" runat="server" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_trvExpressions_Expressions_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_trvExpressions_Expressions_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                            </tr>
                            <tr>
                                <td class="WhiteLabel" style="width: 10%;">
                                    <asp:Label ID="lblScriptFaEnd_Expressions" runat="server" meta:resourcekey="lblScriptFaEnd_Expressions"></asp:Label>
                                </td>
                                <td>
                                    <input id="txtScriptFaEnd_Expressions" type="text" runat="server" class="TextBoxes" disabled="disabled"
                                         style="width: 90%" />
                                </td>
                            </tr>
                            <tr>
                                <td class="WhiteLabel" style="width: 10%;">
                                    <asp:Label ID="lblScriptCSharpBegin_Expressions" runat="server" meta:resourcekey="lblScriptCSharpBegin_Expressions"></asp:Label>
                                </td>
                                <td>
                                    <textarea id="txtScriptCSharpBegin_Expressions" dir="ltr" cols="20" rows="2" style="width: 90%;" class="TextBoxes"  disabled="disabled"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="WhiteLabel" style="width: 10%;">
                                    <asp:Label ID="lblScriptCSharpEnd_Expressions" runat="server" meta:resourcekey="lblScriptCSharpEnd_Expressions"></asp:Label>
                                </td>
                                <td>
                                    <textarea id="txtScriptCSharpEnd_Expressions" cols="20" rows="2" dir="ltr" style="width: 90%;" class="TextBoxes" disabled="disabled"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <asp:Label ID="lblOrder_Expressions" runat="server" meta:resourcekey="lblOrderExpression_Expressions" CssClass="WhiteLabel"></asp:Label>
                                </td>
                                <td>
                                    <input id="txtOrder_Expressions" type="text" runat="server" style="width: 90%;" class="TextBoxes" disabled="disabled"  value="0" /></td>
                                <td colspan="4">
                                    <table style="width: 60%;">
                                        <tr>
                                            <td style="width: 1%;">
                                                <input id="ckbEditable_Expressions" type="checkbox" disabled="disabled" /></td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblEditableExpresion_Expresions" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblEditableExpresion_Expresions"
                                                    Text="قابل ویرایش"></asp:Label></td>
                                            <td style="width: 1%">
                                                <input id="ckbAddable_Expressions" type="checkbox" disabled="disabled" /></td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblAddable_Expressions" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAddable_Expressions" Text="قابل درج"></asp:Label>
                                            </td>
                                            <td style="width: 1%">
                                                <input id="ckbAddByParent_Expressions" type="checkbox" disabled="disabled" /></td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblAddByParent_Expressions" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblAddByParent_Expressions" Text="درج با والد"></asp:Label>
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
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <table style="width: 38%;" class="BoxStyle">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblQuickSearch_Expressions" runat="server" meta:resourcekey="lblQuickSearch_Expressions" Text=": جستجوی سریع" CssClass="WhiteLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtSearchTerm_Expressions" />
                                                        </td>
                                                        <td style="width: 5%">
                                                            <ComponentArt:ToolBar ID="tlbItemQuickSearch" runat="server" CssClass="toolbar"
                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbExpressionsQuickSearch" runat="server"
                                                                        ClientSideCommand="tlbItemSearch_TlbExpressionQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                        ItemType="Command" meta:resourcekey="tlbItemSearch_TlbExpressionsQuickSearch" TextImageSpacing="5" />
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
                                <td style="width: 60%;">
                                    <table style="width: 100%;" class="BoxStyle">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td id="" class="HeaderLabel" style="width: 95%">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td id="header_ExpressionsBox_Expressions" class="HeaderLabel" style="width: 50%;">Expressions
                                                                    </td>
                                                                    <td id="loadingPanel_GridExpressions_Expressions" class="HeaderLabel" style="width: 45%"></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <ComponentArt:CallBack ID="CallBack_GridExpressions_Expression" OnCallback="CallBack_GridExpressions_Expression_OnCallBack"
                                                    runat="server">
                                                    <Content>
                                                        <ComponentArt:DataGrid ID="GridExpressions_Expressions" runat="server" AllowHorizontalScrolling="true"
                                                            CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                                            FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                            PageSize="7" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                            ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                            ScrollTopBottomImageWidth="16" ScrollImagesFolderUrl="images/Grid/scroller/"
                                                            ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                            ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16">
                                                            <Levels>
                                                                <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                                    AllowSorting="false" HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText"
                                                                    RowCssClass="Row" SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell"
                                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                    DataKeyField="ID" SortImageWidth="9" HoverRowCssClass="HoverRow">
                                                                    <Columns>
                                                                        <ComponentArt:GridColumn Align="Center" DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                        <ComponentArt:GridColumn Align="Center" DataField="Parent_ID" Visible="false" meta:resourcekey="clmnExpressionParent_ID_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" DataType="System.Decimal" FormatString="###"/>
                                                                        <ComponentArt:GridColumn Align="Center" DataField="ScriptBeginFa" Visible="True" meta:resourcekey="clmnExpressionScriptBeginFa_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="ScriptEndFa" Visible="True" meta:resourcekey="clmnExpressionScriptEndFa_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="ScriptBeginEn" Visible="True" meta:resourcekey="clmnExpressionScriptBeginEn_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="ScriptEndEn" Visible="True" meta:resourcekey="clmnExpressionScriptEndEn_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="AddOnParentCreation" ColumnType="CheckBox" Visible="True" meta:resourcekey="clmnExpressionAddOnParentCreation_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="CanAddToFinal" ColumnType="CheckBox" Visible="True" meta:resourcekey="clmnExpressionCanAddToFinal_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="CanEditInFinal" ColumnType="CheckBox" Visible="True" meta:resourcekey="clmnExpressionCanEditInFinal_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="Visible" ColumnType="CheckBox" Visible="false" meta:resourcekey="clmnExpressionVisible_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="SortOrder" Visible="True" meta:resourcekey="clmnExpressionSortOrder_GridExpressions_Expressions" DefaultSortDirection="Descending" HeadingTextCssClass="HeadingText" TextWrap="true" />
                                                                    </Columns>
                                                                </ComponentArt:GridLevel>
                                                            </Levels>
                                                            <ClientEvents>
                                                                <ItemSelect EventHandler="GridExpressions_Expressions_onItemSelect" />
                                                                <Load EventHandler="GridExpressions_Expressions_onLoad" />
                                                            </ClientEvents>
                                                        </ComponentArt:DataGrid>
                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_Expressions" />
                                                        <asp:HiddenField runat="server" ID="hfExpressionsCount_Expressions" />
                                                        <asp:HiddenField runat="server" ID="hfExpressionsPageCount_Expressions" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <CallbackComplete EventHandler="CallBack_GridExpressions_Expression_OnCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_GridExpressions_Expression_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                            <ComponentArt:ToolBar runat="server" ID="TlbPaging_GridExpressions_Expressions" CssClass="toolbar"
                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                Style="direction: ltr" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridExpressions_Expressions"
                                                                        runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_GridExpressions_Expressions_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                        ImageUrl="refresh.png" ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridExpressions_Expressions"
                                                                        TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridExpressions_Expressions" runat="server"
                                                                        ClientSideCommand="tlbItemFirst_TlbPaging_GridExpressions_Expressions_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                        ImageUrl="first.png" ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridExpressions_Expressions"
                                                                        TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridExpressions_Expressions" runat="server"
                                                                        ClientSideCommand="tlbItemBefore_TlbPaging_GridExpressions_Expressions_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                        ImageUrl="Before.png" ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridExpressions_Expressions"
                                                                        TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridExpressions_Expressions" runat="server"
                                                                        ClientSideCommand="tlbItemNext_TlbPaging_GridExpressions_Expressions_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                        ImageUrl="Next.png" ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridExpressions_Expressions"
                                                                        TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridExpressions_Expressions" runat="server"
                                                                        ClientSideCommand="tlbItemLast_TlbPaging_GridExpressions_Expressions_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                                        ImageUrl="last.png" ImageWidth="16px" ExpressionType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridExpressions_Expressions"
                                                                        TextImageSpacing="5" TextImageRelation="ImageOnly" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                        <td id="footer_GridExpressions_Expressions" class="WhiteLabel" style="width: 25%"></td>
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
        </div>
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
        <iframe id="hidden_IFrame_Expressions" style="width: 0px; height: 0px"></iframe>
        <asp:HiddenField runat="server" ID="hfTitle_DialogExpressionsManagement" meta:resourcekey="hfTitle_DialogExpressionsManagement" />
        <asp:HiddenField runat="server" ID="hfheader_ExpressionsBox_Expressions" meta:resourcekey="hfheader_ExpressionsBox_Expressions" />
        <asp:HiddenField runat="server" ID="hfView_Expressions" meta:resourcekey="hfView_Expressions" />
        <asp:HiddenField runat="server" ID="hfAdd_Expressions" meta:resourcekey="hfAdd_Expressions" />
        <asp:HiddenField runat="server" ID="hfEdit_Expressions" meta:resourcekey="hfEdit_Expressions" />
        <asp:HiddenField runat="server" ID="hfDelete_Expressions" meta:resourcekey="hfDelete_Expressions" />
        <asp:HiddenField runat="server" ID="hfErrorType_Expressions" meta:resourcekey="hfErrorType_Expressions" />
        <asp:HiddenField runat="server" ID="hfConnectionError_Expressions" meta:resourcekey="hfConnectionError_Expressions" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_Expressions" meta:resourcekey="hfDeleteMessage_Expressions" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Expressions" meta:resourcekey="hfCloseMessage_Expressions" />
        <asp:HiddenField runat="server" ID="hfExpressionsPageSize_Expressions" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridExpressions_Expressions" meta:resourcekey="hfloadingPanel_GridExpressions_Expressions" />
        <asp:HiddenField runat="server" ID="hffooter_GridExpressions_Expressions" meta:resourcekey="hffooter_GridExpressions_Expressions" />
    </form>
</body>
</html>
