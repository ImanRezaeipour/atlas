<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.Contractors" Codebehind="Contractors.aspx.cs" %>

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
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ContractorsForm" runat="server" meta:resourcekey="ContractorsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="~/JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%; height: 400px; font-family: Arial; font-size: small">
            <%--start toolbar--%>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:ToolBar ID="TlbContractors" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                    DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                    DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                    DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemNew_TlbContractors" runat="server" ClientSideCommand="tlbItemNew_TlbContractors_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="add.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNew_TlbContractors"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemEdit_TlbContractors" runat="server" ClientSideCommand="tlbItemEdit_TlbContractors_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="edit.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemEdit_TlbContractors"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbContractors" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemDelete_TlbContractors_onClick();" DropDownImageWidth="16px"
                                            ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                            meta:resourcekey="tlbItemDelete_TlbContractors" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbContractors" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemHelp_TlbContractors" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemHelp_TlbContractors_onClick();" />
                                        <ComponentArt:ToolBarItem ID="tlbItemSave_TlbContractors" runat="server" DropDownImageHeight="16px"
                                            ClientSideCommand="tlbItemSave_TlbContractors_onClick();" DropDownImageWidth="16px"
                                            Enabled="false" ImageHeight="16px" ImageUrl="save_silver.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemSave_TlbContractors" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemCancel_TlbContractors" runat="server" ClientSideCommand="tlbItemCancel_TlbContractors_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="cancel_silver.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemCancel_TlbContractors"
                                            Enabled="false" TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbContractors" runat="server"
                                            ClientSideCommand="tlbItemFormReconstruction_TlbContractors_onClick();" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbContractors"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemExit_TlbContractors" runat="server" DropDownImageHeight="16px"
                                            DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                            ItemType="Command" meta:resourcekey="tlbItemExit_TlbContractors" TextImageSpacing="5"
                                            ClientSideCommand="tlbItemExit_TlbContractors_onClick();" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="ActionMode_Contractors" class="ToolbarMode"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%-- end toolbar--%>
            <%-- start Contractors grid & Search--%>
            <tr>
                <td valign="top">
                    <table style="width: 98%; height: 200px;" class="BoxStyle">
                        <tr>
                            <td style="height: 5%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td id="header_Contractors_Contractors" class="HeaderLabel" style="width: 15%;">Contractors
                                        </td>
                                        <td id="loadingPanel_GridContractors_Contractors" class="HeaderLabel" style="width: 15%"></td>
                                        <td class="HeaderLabel" style="width: 45%">
                                            <table style="width: 100%;" class="BoxStyle">
                                                <tr>
                                                    <td style="width: 80%">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 20%; font-size: small; font-weight: normal;">
                                                                    <asp:Label ID="lblSearchTerm_Contractors" runat="server" meta:resourcekey="lblSearchTerm_Contractors"
                                                                        Text="عبارت جستجو" CssClass="WhiteLabel"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <input type="text" runat="server" style="width: 99%;" class="TextBoxes"
                                                                        onkeypress="txtSearchTerm_Contractors_onKeyPess(event);" id="txtSearchTerm_Contractors" />
                                                                </td>
                                                                <td style="width: 5%">
                                                                    <ComponentArt:ToolBar ID="TlbContractorsSearch" runat="server" CssClass="toolbar"
                                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                        UseFadeEffect="false">
                                                                        <Items>
                                                                            <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbContractorsSearch" runat="server"
                                                                                ClientSideCommand="tlbItemSearch_TlbContractorsSearch_onClick();" DropDownImageHeight="16px"
                                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                                ItemType="Command" meta:resourcekey="tlbItemSearch_TlbContractorsSearch" TextImageSpacing="5" />
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
                            <td valign="top">
                                <div id="Container_GridContractors_Contractors" style="width: 100%">
                                    <ComponentArt:CallBack ID="CallBack_GridContractors_Contractors" runat="server" OnCallback="CallBack_GridContractors_Contractors_onCallBack">
                                        <Content>
                                            <ComponentArt:DataGrid ID="GridContractors_Contractors" runat="server" AllowHorizontalScrolling="true"
                                                CssClass="Grid" EnableViewState="false" ShowFooter="false" FillContainer="true"
                                                FooterCssClass="GridFooter" ImagesBaseUrl="images/Grid/" PagePaddingEnabled="true"
                                                PageSize="4" RunningMode="Client" AllowMultipleSelect="false" AllowColumnResizing="false"
                                                ScrollBar="Off" ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageHeight="2"
                                                ScrollTopBottomImageWidth="16" Scr0ollImagesFolderUrl="images/Grid/scroller/"
                                                ScrollButtonWidth="16" ScrollButtonHeight="17" ScrollBarCssClass="ScrollBar"
                                                ScrollGripCssClass="ScrollGrip" ScrollBarWidth="16" Width="1200">
                                                <Levels>
                                                    <ComponentArt:GridLevel AlternatingRowCssClass="AlternatingRow" DataCellCssClass="DataCell"
                                                        HeadingCellCssClass="HeadingCell" HeadingTextCssClass="HeadingCellText" RowCssClass="Row"
                                                        SelectedRowCssClass="SelectedRow" SelectorCellCssClass="SelectorCell" SortAscendingImageUrl="asc.gif"
                                                        SortDescendingImageUrl="desc.gif" SortImageHeight="5" SortImageWidth="9" DataKeyField="ID">
                                                        <Columns>
                                                            <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="Name" DefaultSortDirection="Descending"
                                                                HeadingText="نام" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnName_GridContractors_Contractors" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="Organization" DefaultSortDirection="Descending"
                                                                HeadingText="سازمان" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnOrganization_GridContractors_Contractors" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="Code" DefaultSortDirection="Descending"
                                                                HeadingText="کد" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnCode_GridContractors_Contractors" />
                                                            <ComponentArt:GridColumn Align="Center" DataField="EconomicCode" DefaultSortDirection="Descending"
                                                                HeadingText="کد اقتصادی" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnEconomicCode_GridContractors_Contractors" />
                                                            <ComponentArt:GridColumn Align="Center" DefaultSortDirection="Descending" DataField="Description"
                                                                HeadingText="توضیحات" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnDescription_GridContractors_Contractors" />                                                           
                                                            <ComponentArt:GridColumn DataField="Tel" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="Fax" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="Email" Visible="false" />
                                                            <ComponentArt:GridColumn DataField="Address" Visible="false" /> 
                                                            <ComponentArt:GridColumn DataField="IsDefault" Visible="false" ColumnType="CheckBox" />                                                           
                                                        </Columns>
                                                    </ComponentArt:GridLevel>
                                                </Levels>                                               
                                                <ClientEvents>
                                                    <Load EventHandler="GridContractors_Contractors_onLoad" />
                                                    <ItemCheckChange EventHandler="GridContractors_Contractors_onItemCheckChange" />
                                                    <ItemSelect EventHandler="GridContractors_Contractors_OnItemSelect" />
                                                </ClientEvents>
                                            </ComponentArt:DataGrid>
                                            <asp:HiddenField runat="server" ID="ErrorHiddenField_Contractors" />
                                            <asp:HiddenField runat="server" ID="hfContractorsCount_Contractors" />
                                            <asp:HiddenField runat="server" ID="hfContractorsPageCount_Contractors" />
                                        </Content>
                                        <ClientEvents>
                                            <CallbackComplete EventHandler="CallBack_GridContractors_Contractors_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_GridContractors_Contractors_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td runat="server" meta:resourcekey="AlignObj" style="width: 10%;">
                                            <ComponentArt:ToolBar ID="TlbPaging_GridContractors_Contractors" runat="server" CssClass="toolbar"
                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                Style="direction: ltr" UseFadeEffect="false">
                                                <Items>
                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_GridContractors_Contractors" runat="server"
                                                        ClientSideCommand="tlbItemRefresh_TlbPaging_GridContractors_Contractors_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="refresh.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_GridContractors_Contractors"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_GridContractors_Contractors" runat="server"
                                                        ClientSideCommand="tlbItemFirst_TlbPaging_GridContractors_Contractors_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="first.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_GridContractors_Contractors"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_GridContractors_Contractors" runat="server"
                                                        ClientSideCommand="tlbItemBefore_TlbPaging_GridContractors_Contractors_onClick();"
                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                                        ImageUrl="Before.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_GridContractors_Contractors"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_GridContractors_Contractors" runat="server"
                                                        ClientSideCommand="tlbItemNext_TlbPaging_GridContractors_Contractors_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="Next.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_GridContractors_Contractors"
                                                        TextImageSpacing="5" />
                                                    <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_GridContractors_Contractors" runat="server"
                                                        ClientSideCommand="tlbItemLast_TlbPaging_GridContractors_Contractors_onClick();" DropDownImageHeight="16px"
                                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="last.png"
                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_GridContractors_Contractors"
                                                        TextImageSpacing="5" />
                                                </Items>
                                            </ComponentArt:ToolBar>
                                        </td>
                                        <td id="beginfooter_GridContractors_Contractors" class="WhiteLabel" style="width: 45%"></td>
                                        <td class="whitelabel" style="width: 35%"></td>
                                        <td id="endfooter_gridcontractors_contractors" class="whitelabel" style="width: 20%"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--  end Contractor grid & Search--%>
            <%--  strat panel new --%>
            <tr>
                <td>
                    <table style="width: 98%;" class="BoxStyle">
                        <tr>
                            <td class="DetailsBoxHeaderStyle">
                                <div id="header_ContractorDetails_Contractors" runat="server" style="color: White; width: 100%; height: 100%"
                                    class="BoxContainerHeader">
                                    Contractors Details
                                </div>
                            </td>
                        </tr>
                        <%-- 11--%>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>      
                                        <%--<td>
                                            <input id="chbDefaultContractor_Contractors" type="checkbox" style="width:7%" />
                                        </td>                                  
                                        <td id="tdlblDefaultContractor_Contractors" style="width:93%">
                                            <asp:Label ID="lblDefaultContractor_Contractors" runat="server" Text=" پیمانکار پیش فرض" CssClass="WhiteLabel"
                                                meta:resourcekey="lblDefaultContractor_Contractors"></asp:Label>
                                        </td>
                                        
                                        <td></td>--%>
                                         <td colspan="3">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input id="chbDefaultContractor_Contractors" type="checkbox"   disabled="disabled"  />
                                                    </td> 
                                                     <td id="tdlblDefaultContractor_Contractors" >
                                                        <asp:Label ID="lblDefaultContractor_Contractors" runat="server" Text="پیمانکار پیش فرض" CssClass="WhiteLabel"
                                                            meta:resourcekey="lblDefaultContractor_Contractors"></asp:Label>
                                                    </td>                                                                                                                                                          
                                                </tr>
                                            </table>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblContractorName_Contractors" runat="server" CssClass="WhiteLabel"
                                                Text=": نام پیمانکار" meta:resourcekey="lblContractorName_Contractors"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganization_Contractors" runat="server" CssClass="WhiteLabel" Text=":سازمان"
                                                meta:resourcekey="lblOrganization_Contractors"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblContractorCode_Contractors" runat="server" CssClass="WhiteLabel"
                                                Text=": کد پیمانکار" meta:resourcekey="lblContractorCode_Contractors"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="txtContractorName_Contractors" type="text" style="width: 97%" class="TextBoxes"
                                                disabled="disabled" onclick="this.select();" />
                                        </td>
                                        <td>
                                            <input id="txtOrganization_Contractors" type="text" style="width: 97%" class="TextBoxes"
                                                disabled="disabled" />
                                        </td>
                                        <td>
                                            <input id="txtContractorCode_Contractors" type="text" style="width: 97%" class="TextBoxes"
                                                disabled="disabled" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEconomicCode_Contractors" runat="server" CssClass="WhiteLabel"
                                                Text=": کد اقتصادی" meta:resourcekey="lblEconomicCode_Contractors"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTelNumber_Contractors" runat="server" CssClass="WhiteLabel" Text=": تلفن"
                                                meta:resourcekey="lblTelNumber_Contractors"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFax_Contractors" runat="server" CssClass="WhiteLabel" Text=": فکس"
                                                meta:resourcekey="lblFax_Contractors"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <input id="txtEconomicCode_Contractors" type="text" style="width: 97%" class="TextBoxes"
                                                disabled="disabled" />
                                        </td>
                                        <td>
                                            <input id="txtTelNumber_Contractors" type="text" style="width: 97%" class="TextBoxes"
                                                disabled="disabled" />
                                        </td>
                                        <td>
                                            <input id="txtFax_Contractors" type="text" style="width: 97%" class="TextBoxes"
                                                disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEmail_Contractors" runat="server" CssClass="WhiteLabel" Text=": ایمیل"
                                                meta:resourcekey="lblEmail_Contractors"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAddress_Contractors" runat="server" CssClass="WhiteLabel" Text=": آدرس"
                                                meta:resourcekey="lblAddress_Contractors"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescription_Contractors" runat="server" CssClass="WhiteLabel"
                                                Text=": توضیحات" meta:resourcekey="lblDescription_Contractors"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <textarea id="txtEmail_Contractors" rows="7" cols="20" style="width: 97%; height: 25px"
                                                class="TextBoxes" disabled="disabled"></textarea>
                                        </td>
                                        <td>
                                            <textarea id="txtAddress_Contractors" rows="7" cols="20" style="width: 97%; height: 25px"
                                                class="TextBoxes" disabled="disabled"></textarea>
                                        </td>
                                        <td>
                                            <textarea id="txtDescription_Contractors" cols="20" rows="7" style="width: 97%; height: 25px"
                                                class="TextBoxes" disabled="disabled"></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%-- end panel new --%>
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
        <asp:HiddenField runat="server" ID="hfheader_ContractorDetails_Contractors" meta:resourcekey="hfheader_ContractorDetails_Contractors" />
        <asp:HiddenField runat="server" ID="hfheader_Contractors_Contractors" meta:resourcekey="hfheader_Contractors_Contractors" />
        <asp:HiddenField runat="server" ID="hfView_Contractors" meta:resourcekey="hfView_Contractors" />
        <asp:HiddenField runat="server" ID="hfAdd_Contractors" meta:resourcekey="hfAdd_Contractors" />
        <asp:HiddenField runat="server" ID="hfEdit_Contractors" meta:resourcekey="hfEdit_Contractors" />
        <asp:HiddenField runat="server" ID="hfDelete_Contractors" meta:resourcekey="hfDelete_Contractors" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_Contractors" meta:resourcekey="hfDeleteMessage_Contractors" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_Contractors" meta:resourcekey="hfCloseMessage_Contractors" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridContractors_Contractors" meta:resourcekey="hfloadingPanel_GridContractors_Contractors" />
        <asp:HiddenField runat="server" ID="hfContractorsPageSize_Contractors" />
        <%--  <asp:HiddenField runat="server" ID="hfbeginfooter_GridContractors_Contractors" meta:resourcekey="hfbeginfooter_GridContractors_Contractors" />--%>
        <asp:HiddenField runat="server" ID="hffooter_GridContractors_Contractors" meta:resourcekey="hffooter_GridContractors_Contractors" />
        <asp:HiddenField runat="server" ID="hfContractorsCount_GridContractors_Contractors" meta:resourcekey="hfContractorsCount_GridContractors_Contractors" />
    </form>
</body>
</html>
