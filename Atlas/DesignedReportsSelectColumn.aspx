<%@ Page Language="C#" AutoEventWireup="true" Inherits="DesignedReportsSelectColumn" Codebehind="DesignedReportsSelectColumn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <link id="Link15" href="css/persianDatePicker.css" runat="server" type="text/css"
        rel="Stylesheet" />
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="DesignedReportsSelectColumnForm" runat="server" meta:resourcekey="DesignedReportsSelectColumnForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="font-size: small; font-family: Arial; width: 100%" class="BoxStyle">
            <tr>
                <td>

                    <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbDesignedReportsSelectColumn" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemNew_TlbDesignedReportsSelectColumn" runat="server" ClientSideCommand="tlbItemNew_TlbDesignedReportsSelectColumn_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbDesignedReportsSelectColumn"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbDesignedReportsSelectColumn" runat="server" ClientSideCommand="tlbItemEdit_TlbDesignedReportsSelectColumn_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbDesignedReportsSelectColumn"
                                        TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbDesignedReportsSelectColumn" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbDesignedReportsSelectColumn_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbDesignedReportsSelectColumn" TextImageSpacing="5" />
                                     <ComponentArt:ToolBarItem ID="tlbItemSave_TlbDesignedReportsSelectColumn" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbDesignedReportsSelectColumn_onClick();" DropDownImageWidth="16px"
                                        Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbDesignedReportsSelectColumn" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbDesignedReportsSelectColumn" runat="server" ClientSideCommand="tlbItemCancel_TlbDesignedReportsSelectColumn_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbDesignedReportsSelectColumn"
                                        Enabled="false" TextImageSpacing="5" />                               
                                   <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbDesignedReportsSelectColumn" runat="server"
                                        ClientSideCommand="tlbItemFormReconstruction_TlbDesignedReportsSelectColumn_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbDesignedReportsSelectColumn" TextImageSpacing="5" />
                                     <ComponentArt:ToolBarItem ID="tlbItemExit_TlbDesignedReportsSelectColumn" runat="server" ClientSideCommand="tlbItemExit_TlbDesignedReportsSelectColumn_onClick();"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbDesignedReportsSelectColumn"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td id="ActionMode_DesignedReportsSelectColumn" class="ToolbarMode">
                        </td>
                    </tr>
                        <tr>
                            <td>

                            </td>

                        </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td height="55%">
                    <table style="width: 100%;">
                    <tr>
                        <td style="width: 65%">
                            <table style="width: 100%;" class="BoxStyle">
                                <tr>
                                    <td style="color: White; width: 100%">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="header_DesignedReportsSelectColumn_DesignedReportsSelectColumn" class="HeaderLabel" style="width: 50%">
                                                    Reports
                                                </td>
                                                <td id="loadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                    <ComponentArt:ToolBar ID="TlbRefresh_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" runat="server"
                                                                ClientSideCommand="Refresh_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn"
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
                                        <ComponentArt:CallBack runat="server" ID="CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" OnCallback="CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" runat="server" CssClass="Grid"
                                                    EnableViewState="false" FillContainer="true" FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/"
                                                    PagePaddingEnabled="true" PagerTextCssClass="GridFooterText" PageSize="14" RunningMode="Client"
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
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending" AllowSorting="False"
                                                                    HeadingText="نام ستون" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnColumnConceptName_DesignedReportsSelectColumn" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending" AllowSorting="False"
                                                                    HeadingText="عنوان نمایش" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnColumnTitle_DesignedReportsSelectColumn" TextWrap="true"/>
                                                                
                                                                <ComponentArt:GridColumn Align="Center" ColumnType="CheckBox" DataField="Active" DefaultSortDirection="Descending"
                                                                    HeadingText="فعال" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnColumnActive_DesignedReportsSelectColumn" TextWrap="true" AllowSorting="False"/>
                                                                
                                                                <ComponentArt:GridColumn DataField="ConceptID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                
                                                                
                                                                <ComponentArt:GridColumn DataField="ReportID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn DataField="Order" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="PersonInfoID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn DataField="IsConcept" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="ColumnType" Visible="false" />
                                                                 <ComponentArt:GridColumn DataField="ColumnName" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="TrafficColumnCount" Visible="false" />
                                                                <ComponentArt:GridColumn DataField="TrafficID" Visible="false" />
                                                                
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onLoad" />
                                                        <ItemSelect EventHandler="GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onItemSelect" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_DesignedReportsSelectColumn" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbInterAction_DesignedReportsSelectColumn" runat="server" CssClass="verticaltoolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemUp_TlbInterAction_DesignedReportsSelectColumn" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="arrow-up.png" ImageWidth="16px" ItemType="Command" ClientSideCommand="tlbItemUp_TlbInterAction_DesignedReportsSelectColumn_onClick();"
                                        meta:resourcekey="tlbItemUp_TlbInterAction_DesignedReportsSelectColumn" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDown_TlbInterAction_DesignedReportsSelectColumn" runat="server"
                                        ClientSideCommand="tlbItemDown_TlbInterAction_DesignedReportsSelectColumn_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="arrow-down.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDown_TlbInterAction_DesignedReportsSelectColumn"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>

                                    </td>
                                </tr>
                            </table>
                            </td>
                        <td style="width: 35%">
                            <table style="width: 90%;" class="BoxStyle" id="tblDesignedReportsSelectColumn_DesignedReportsSelectColumn">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle" >
                                        <div id="header_DesignedReportsSelectColumnDetails_DesignedReportsSelectColumn" runat="server" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                            Column Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" meta:resourcekey="AlignObj">
                                    <td >
                                        <asp:Label ID="lblColumnName_DesignedReportsSelectColumn" runat="server" meta:resourcekey="lblColumnName_DesignedReportsSelectColumn"
                                            Text=": ستون ها" CssClass="WhiteLabel"></asp:Label></td>
                                </tr>
                                <tr id="Tr3" runat="server" meta:resourcekey="AlignObj" >
                                    <td  >
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbColumnName_DesignedReportsSelectColumn" OnCallback="CallBack_cmbColumnName_DesignedReportsSelectColumn_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbColumnName_DesignedReportsSelectColumn" runat="server" AutoComplete="true"
                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbColumnName_DesignedReportsSelectColumn_onExpand" />
                                                        <Collapse EventHandler="cmbColumnName_DesignedReportsSelectColumn_onCollapse" />
                                                        <Change EventHandler="cmbColumnName_DesignedReportsSelectColumn_onChange" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_ColumnName" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbColumnName_DesignedReportsSelectColumn_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbColumnName_DesignedReportsSelectColumn_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbColumnName_DesignedReportsSelectColumn_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                    <td colspan="2">
                                        <asp:Label ID="lblPersonInfoColumnName_DesignedReportsSelectColumn" runat="server" meta:resourcekey="lblPersonInfoColumnName_DesignedReportsSelectColumn"
                                            Text=":  ستون های پرسنلی" CssClass="WhiteLabel"></asp:Label></td>
                                </tr>
                                <tr id="Tr8" runat="server" meta:resourcekey="AlignObj" >
                                    <td >
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn" OnCallback="CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbPersonInfoColumnName_DesignedReportsSelectColumn" runat="server" AutoComplete="true"
                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbPersonInfoColumnName_DesignedReportsSelectColumn_onExpand" />
                                                        <Collapse EventHandler="cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCollapse" />
                                                        <Change EventHandler="cmbPersonInfoColumnName_DesignedReportsSelectColumn_onChange" />
                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_PersonInfoColumnName" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbPersonInfoColumnName_DesignedReportsSelectColumn_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server" meta:resourcekey="AlignObj">
                                    <td >
                                        <asp:Label ID="lblColumnTitle_DesignedReportsSelectColumn" runat="server" meta:resourcekey="lblColumntitle_DesignedReportsSelectColumn"
                                            Text=": عنوان نمایش" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server" meta:resourcekey="AlignObj" >
                                    <td >
                                        <input type="text" runat="server" style="width: 99%;" class="TextBoxes" id="txtColumnTitle_DesignedReportsSelectColumn"
                                             disabled="disabled"   />
                                    </td>
                                </tr>
                             
                                <tr id="Tr6" runat="server" meta:resourcekey="AlignObj">
                                    <td >
                                        <table>
                                            <tr>
                                                <td style="width:5%;">
                                         <input  disabled="disabled" id="chbActiveTitle_DesignedReportsSelectColumn" type="checkbox"   runat="server"/>
                                    </td>
                                    <td style="width:95%;">
                                        <asp:Label ID="lblColumnActive_DesignedReportsSelectColumn" runat="server" meta:resourcekey="lblColumnActive_DesignedReportsSelectColumn"
                                            Text="فعال" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                    
                                </tr>
                               <tr>
                                  <td>
                                      <table width="100%" id="tblTrafficColumnCount_DesignedReportsSelectColumn" style="visibility:hidden;" >
                                          <tr meta:resourcekey="AlignObj">
                                              <td>
                                                   <asp:Label ID="lblTrafficColumnCount_DesignedReportsSelectColumn" runat="server" meta:resourcekey="lblTrafficCount_DesignedReportsSelectColumn"
                                            Text=": تعداد ستون های تردد" CssClass="WhiteLabel"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr meta:resourcekey="AlignObj">
                                              <td>
                                                   <input type="text" runat="server" style="width: 99%;" class="TextBoxes" onchange="txtTrafficColumnCount_DesignedReportsSelectColumn_onChange();" id="txtTrafficColumnCount_DesignedReportsSelectColumn"
                                             />
                                              </td>
                                          </tr>
                                      </table>
                                  </td>
                               </tr>
                               <%-- <tr id="Tr7" runat="server" meta:resourcekey="AlignObj">
                                    <td>
                                       
                                        
                                    </td>
                                </tr>--%>
                                
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

    <asp:HiddenField runat="server" ID="hfheader_DesignedReportsSelectColumn_DesignedReportsSelectColumn" meta:resourcekey="hfheader_DesignedReportsSelectColumn_DesignedReportsSelectColumn" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" meta:resourcekey="hfloadingPanel_GridDesignedReportsSelectColumn_DesignedReportsSelectColumn" />
        <asp:HiddenField runat="server" ID="hfheader_DesignedReportsSelectColumnDetails_DesignedReportsSelectColumn" meta:resourcekey="hfheader_DesignedReportsSelectColumnDetails_DesignedReportsSelectColumn" />
         <asp:HiddenField runat="server" ID="hfcmbAlarm_DesignedReportsSelectColumn" meta:resourcekey="hfcmbAlarm_DesignedReportsSelectColumn" />
                <asp:HiddenField runat="server" ID="hfView_DesignedReportsSelectColumn" meta:resourcekey="hfView_DesignedReportsSelectColumn" />
    <asp:HiddenField runat="server" ID="hfAdd_DesignedReportsSelectColumn" meta:resourcekey="hfAdd_DesignedReportsSelectColumn" />
    <asp:HiddenField runat="server" ID="hfEdit_DesignedReportsSelectColumn" meta:resourcekey="hfEdit_DesignedReportsSelectColumn" />
    <asp:HiddenField runat="server" ID="hfDelete_DesignedReportsSelectColumn" meta:resourcekey="hfDelete_DesignedReportsSelectColumn" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogDesignedReportsSelectColumn" meta:resourcekey="hfTitle_DialogDesignedReportsSelectColumn" />
          <asp:HiddenField runat="server" ID="hfDeleteMessage_DesignedReportsSelectColumn" meta:resourcekey="hfDeleteMessage_DesignedReportsSelectColumn" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_DesignedReportsSelectColumn" meta:resourcekey="hfCloseMessage_DesignedReportsSelectColumn" />
    </form>
</body>
</html>
