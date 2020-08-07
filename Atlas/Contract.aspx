<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.Contract" Codebehind="Contract.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
     <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ContractForm" runat="server" meta:resourcekey="ContractForm">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
        </Scripts>
    </asp:ScriptManager>
    <table style="width: 100%; font-family: Arial; font-size: small">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbContract" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                            DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                            DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                            DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                            <Items>
                                                <ComponentArt:ToolBarItem ID="tlbItemNew_TlbContract" runat="server" ClientSideCommand="tlbItemNew_TlbContract_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbContract"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbContract" runat="server" ClientSideCommand="tlbItemEdit_TlbContract_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbContract"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbContract" runat="server" DropDownImageHeight="16px"
                                                    ClientSideCommand="tlbItemDelete_TlbContract_onClick();" DropDownImageWidth="16px"
                                                    ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                                    meta:resourcekey="tlbItemDelete_TlbContract" TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbContract" runat="server" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbContract" TextImageSpacing="5"
                                                    ClientSideCommand="tlbItemHelp_TlbContract_onClick();" />
                                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbContract" runat="server" ClientSideCommand="tlbItemSave_TlbContract_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="save_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbContract"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbContract" runat="server" ClientSideCommand="tlbItemCancel_TlbContract_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="false" ImageHeight="16px"
                                                    ImageUrl="cancel_silver.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbContract"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbContract" runat="server"
                                                    ClientSideCommand="tlbItemFormReconstruction_TlbContract_onClick();" DropDownImageHeight="16px"
                                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                    ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbContract"
                                                    TextImageSpacing="5" />
                                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbContract" runat="server" ClientSideCommand="tlbItemExit_TlbContract_onClick();"
                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemExit_TlbContract"
                                                    TextImageSpacing="5" />
                                            </Items>
                                        </ComponentArt:ToolBar>
                                    </td>
                                    <td id="ActionMode_Contract" class="ToolbarMode">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:50%;" meta:resourcekey="AlighObj">
                                <tr>
                                    <td>
                                         <asp:Label ID="lblSearchTerm_Contract" runat="server" Text=": جستجوی قرارداد"
                                                            meta:resourcekey="lblSearchTerm_Contract" CssClass="WhiteLabel"></asp:Label>
                                    </td>
                                    <td>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                           <input id="txtSearchTerm_Contract" type="text" class="TextBoxes" onkeypress="txtSearchTerm_Contract_onKeyPess(event);"
                                                            style="width: 97%" />
                                    </td>
                                    <td>
                                          <ComponentArt:ToolBar ID="TlbContractSearch_Contract" runat="server"
                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                            <Items>
                                                                <ComponentArt:ToolBarItem ID="tlbItemContractSearch_TlbContractSearch_Contract"
                                                                    runat="server" ClientSideCommand="tlbItemContractSearch_TlbContractSearch_Contract_onClick();"
                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png"
                                                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemContractSearch_TlbContractSearch_Contract"
                                                                    TextImageSpacing="5" Enabled="true" />
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
                                    <td valign="top" style="width: 60%">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td id="header_Contracts_Contract" class="HeaderLabel" style="width: 50%">
                                                                Contract
                                                            </td>
                                                            <td id="loadingPanel_GridContracts_Contract" class="HeaderLabel" style="width: 45%">
                                                            </td>
                                                            <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                                <%--<ComponentArt:ToolBar ID="TlbRefresh_GridContracts_Contract" runat="server" CssClass="toolbar"
                                                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                    <Items>
                                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridContracts_Contract"
                                                                            runat="server" ClientSideCommand="Refresh_GridContracts_Contract();" DropDownImageHeight="16px"
                                                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                            ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridContracts_Contract"
                                                                            TextImageSpacing="5" />
                                                                    </Items>
                                                                </ComponentArt:ToolBar>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack ID="CallBack_GridContracts_Contract" OnCallback="CallBack_GridContracts_Contract_OnCallBack"
                                            runat="server">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridContracts_Contract" runat="server" AllowHorizontalScrolling="true"
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
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                       
                                                               
                                                                <ComponentArt:GridColumn Align="Center" DataField="Title" DefaultSortDirection="Descending"
                                                                    Width="110" HeadingText="عنوان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnTitle_GridContract_Contract" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Code" DefaultSortDirection="Descending"
                                                                    Width="110" HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCode_GridContract_Contract" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Contractor.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="پیمانکار" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnContractor_GridContracts_Contract"
                                                                    Width="140" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" DefaultSortDirection="Descending"
                                                                    HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridContracts_Contract"
                                                                    Width="100" TextWrap="true"/>
                                                                 <ComponentArt:GridColumn Align="Center" DataField="Contractor.ID" DefaultSortDirection="Descending"
                                                                    HeadingTextCssClass="HeadingText" meta:resourcekey="clmnContractor_GridContracts_Contract"
                                                                    Width="140" TextWrap="true" Visible="false"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="IsDefault" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                                    HeadingTextCssClass="HeadingText" meta:resourcekey="clmnIsDefault_GridContracts_Contract"
                                                                    Width="140" TextWrap="true" Visible="false"/>
                                                              
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <ItemSelect EventHandler="GridContracts_Contract_onItemSelect" />
                                                        <Load EventHandler="GridContracts_Contract_onLoad" />
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_Contract" />
                                                <asp:HiddenField runat="server" ID="hfContractsCount_Contract" />
                                                <asp:HiddenField runat="server" ID="hfContractsPageCount_Contract" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridContracts_Contract_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridContracts_Contract_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                             <tr>
                                    <td style="width: 100%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td runat="server" meta:resourcekey="AlignObj" style="width: 75%;">
                                                    <ComponentArt:ToolBar ID="TlbPaging_GridContracts_Contract" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                        Style="direction: ltr" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridContracts_Contract" runat="server"
                                                                ClientSideCommand="tlbItemRefresh_TlbPaging_GridContracts_Contract_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="refresh.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridContracts_Contract"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridContracts_Contract" runat="server"
                                                                ClientSideCommand="tlbItemFirst_TlbPaging_GridContracts_Contract_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="first.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridContracts_Contract"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridContracts_Contract" runat="server"
                                                                ClientSideCommand="tlbItemBefore_TlbPaging_GridContracts_Contract_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Before.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridContracts_Contract"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridContracts_Contract" runat="server"
                                                                ClientSideCommand="tlbItemNext_TlbPaging_GridContracts_Contract_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Next.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridContracts_Contract"
                                                                TextImageSpacing="5" />
                                                            <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridContracts_Contract" runat="server"
                                                                ClientSideCommand="tlbItemLast_TlbPaging_GridContracts_Contract_onClick();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="last.png"
                                                                ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridContracts_Contract"
                                                                TextImageSpacing="5" />
                                                        </Items>
                                                    </ComponentArt:ToolBar>
                                                </td>
                                                <td id="footer_GridContracts_Contract" class="WhiteLabel" style="width: 25%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                        </table>
                                    </td>
                                    <td valign="middle">
                                        <table style="width: 100%;" class="BoxStyle">
                                            <tr>
                                                <td class="DetailsBoxHeaderStyle">
                                                    <div id="header_tblContract_Contract" runat="server" style="color: White; width: 100%;
                                                        height: 100%" class="BoxContainerHeader">
                                                        Contract Details</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTitle_Contract" runat="server" CssClass="WhiteLabel" Text=": عنوان"
                                                        meta:resourcekey="lblTitle_Contract"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtTitle_Contract" type="text" style="width: 97%" class="TextBoxes" disabled="disabled"
                                                          />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCode_Contract" runat="server" CssClass="WhiteLabel" Text=": کد"
                                                        meta:resourcekey="lblCode_Contract"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtCode_Contract" type="text" style="width: 97%" class="TextBoxes"
                                                        disabled="disabled"   />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblContractor_Contract" runat="server" CssClass="WhiteLabel" Text=": پیمانکار"
                                                        meta:resourcekey="lblContractor_Contract"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ComponentArt:CallBack runat="server" ID="CallBcak_cmbContractor_Contract" OnCallback="CallBcak_cmbContractor_Contract_onCallback"
                                                        Height="26">
                                                        <Content>
                                                            <ComponentArt:ComboBox ID="cmbContractor_Contract" runat="server" AutoComplete="true"
                                                                DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                                CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                                DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                                FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                                ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                                TextBoxCssClass="comboTextBox" Enabled="false" TextBoxEnabled="true">
                                                                <ClientEvents>
                                                                    <Expand EventHandler="cmbContractor_Contract_onExpand" />
                                                                    <Collapse EventHandler="cmbContractor_Contract_onCollapse" />
                                                                </ClientEvents>
                                                            </ComponentArt:ComboBox>
                                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Contractor" />
                                                        </Content>
                                                        <ClientEvents>
                                                            <BeforeCallback EventHandler="CallBcak_cmbContractor_Contract_onBeforeCallback" />
                                                            <CallbackComplete EventHandler="CallBcak_cmbContractor_Contract_onCallbackComplete" />
                                                            <CallbackError EventHandler="CallBcak_cmbContractor_Contract_onCallbackError" />
                                                        </ClientEvents>
                                                    </ComponentArt:CallBack>
                                                </td>
                                            </tr>
                                          
                                            
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDescription_Contract" runat="server" CssClass="WhiteLabel" Text=": توضیحات"
                                                        meta:resourcekey="lblDescription_Contract"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea id="txtDescription_Contract" cols="20" name="S1" rows="7" style="width: 97%;
                                                        height: 35px" class="TextBoxes" disabled="disabled" 
                                                        ></textarea>
                                                </td>
                                            </tr>
                                            <tr meta:resourcekey="AlignObj">
                                                <td>
                                                    <table>
                                                        <tr >
                                                            <td style="width: 5%;">
                                                                <input disabled="disabled" id="chbContractIsDefault_Contract" type="checkbox" runat="server" />
                                                            </td>
                                                            <td style="width: 95%;">
                                                               <asp:Label ID="lblIsDefault_Contract" runat="server" CssClass="WhiteLabel" Text="قرارداد پیش فرض"
                                                                          meta:resourcekey="lblIsDefault_Contract"></asp:Label>
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
                        <img id="Img1" runat="server" alt="" src="~/Images/Dialog/Waiting.gif" />
                    </td>
                </tr>
            </table>
        </Content>
        <ClientEvents>
            <OnShow EventHandler="DialogWaiting_onShow" />
        </ClientEvents>
    </ComponentArt:Dialog>
    <asp:HiddenField runat="server" ID="hfContractsPageSize_Contract" />
    <asp:HiddenField runat="server" ID="hfheader_Contracts_Contract" meta:resourcekey="hfheader_Contracts_Contract" />
    <asp:HiddenField runat="server" ID="hfheader_tblContracts_Contract" meta:resourcekey="hfheader_tblContracts_Contract" />
    <asp:HiddenField runat="server" ID="hfView_Contract" meta:resourcekey="hfView_Contract" />
    <asp:HiddenField runat="server" ID="hfAdd_Contract" meta:resourcekey="hfAdd_Contract" />
    <asp:HiddenField runat="server" ID="hfEdit_Contract" meta:resourcekey="hfEdit_Contract" />
    <asp:HiddenField runat="server" ID="hfDelete_Contract" meta:resourcekey="hfDelete_Contract" />
    <asp:HiddenField runat="server" ID="hfDeleteMessage_Contract" meta:resourcekey="hfDeleteMessage_Contract" />
    <asp:HiddenField runat="server" ID="hfCloseMessage_Contract" meta:resourcekey="hfCloseMessage_Contract" />
    <asp:HiddenField runat="server" ID="hfloadingPanel_GridContracts_Contract" meta:resourcekey="hfloadingPanel_GridContracts_Contract" />
    <asp:HiddenField runat="server" ID="hfErrorType_Contract" meta:resourcekey="hfErrorType_Contract" />
    <asp:HiddenField runat="server" ID="hfConnectionError_Contract" meta:resourcekey="hfConnectionError_Contract" />
    <asp:HiddenField runat="server" ID="hffooter_GridContracts_Contract" meta:resourcekey="hffooter_GridContracts_Contract" />
    <asp:HiddenField runat="server" ID="hfcmbAlarm_Contract" meta:resourcekey="hfcmbAlarm_Contract" />
    </form>
</body>
</html>
