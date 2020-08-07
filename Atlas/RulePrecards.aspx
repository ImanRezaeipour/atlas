<%@ Page Language="C#" AutoEventWireup="true" Inherits="RulePrecards" Codebehind="RulePrecards.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Assembly="AspNetPersianDatePickup" Namespace="AspNetPersianDatePickup"
    TagPrefix="pcal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/customHierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/multiPage.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="Css/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/dropdowndive.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="RulePrecardsForm" runat="server" meta:resourcekey="RulePrecardsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table id="Mastertbl_RulePrecards" style="width: 100%; font-family: Arial; font-size: small" class="BodyStyle" meta:resourcekey="Mastertbl_RulePrecards">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbRulePrecards" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRulePrecards" runat="server"
                                    ClientSideCommand="tlbItemSave_TlbRulePrecards_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemSave_TlbRulePrecards" TextImageSpacing="5" />

                                <ComponentArt:ToolBarItem ID="tlbItemSetParameter_TlbRulePrecards" runat="server"
                                    ClientSideCommand="tlbItemSetParameter_TlbRulePrecards_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemSetParameter_TlbRulePrecards" TextImageSpacing="5" />

                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRulePrecards" runat="server"
                                    ClientSideCommand="tlbItemHelp_TlbRulePrecards_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbRulePrecards" TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbRulePrecards"
                                    runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbRulePrecards_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbRulePrecards"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRulePrecards" runat="server"
                                    ClientSideCommand="tlbItemExit_TlbRulePrecards_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemExit_TlbRulePrecards" TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td id="header_RuleText_RulePrecards" class="BoxStyle" style="width: 80%"></td>
                </tr>

                <tr>
                    <td>
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="3">
                                                <div id="divRuleDetails_RulePrecards" runat="server" meta:resourcekey="AlignObj" style="width: 50%; visibility: hidden" class="DropDownHeader">
                                                    <img alt="" runat="server" id="imgbox_RuleDetails_RulePrecards" src="~/DesktopModules/Atlas/Images/Ghadir/arrowDown_silver.jpg"
                                                        onclick="imgbox_RuleDetails_RulePrecards_onClick();" />
                                                    <span id="header_RuleDetails_RulePrecards">Rule Details</span>
                                                </div>
                                                <div class="dhtmlgoodies_contentBox" id="box_R33TagDiv_RulePrecards" style="width: 30%;">
                                                    <div class="dhtmlgoodies_content" id="subbox_R33TagDiv_RulePrecards">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 2%">
                                                                    <input id="chbSubstituteConfirmation_RulePrecards" type="checkbox" />
                                                                </td>
                                                                <td id="tdlblSubstituteConfirmation_RulePrecards" style="width: 98%">
                                                                    <asp:Label ID="lblSubstituteConfirmation_RulePrecards" runat="server" Text="لزوم تایید جانشین" CssClass="WhiteLabel"
                                                                        meta:resourcekey="lblSubstituteConfirmation_RulePrecards"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="header_RulePrecards_RulePrecards" class="HeaderLabel" style="width: 25%">پیشکارت های قانون
                                            </td>
                                            <td id="loadingPanel_GridRulePrecards_RulePrecards" class="HeaderLabel" style="width: 15%"></td>
                                            <td style="width: 25%">
                                                <table style="width: 100%; visibility: visible" id="tblOperator_RulePrecards">
                                                    <tr>
                                                        <td style="width: 2%">
                                                            <input id="chbActiveOperator_RulePrecards" type="checkbox" onchange="chbActiveOperator_RulePrecards_OnChange();" />
                                                        </td>
                                                        <td style="width: 98%">
                                                            <asp:Label ID="lblActiveOperator_RulePrecards" runat="server" Text="اعمال اپراتور" CssClass="WhiteLabel"
                                                                meta:resourcekey="lblActiveOperator_RulePrecards"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 35%">
                                                <table style="width: 100%; visibility: visible" id="tblPecardSearch_RulePrecards">
                                                    <tr>
                                                        <td>
                                                            <input type="text" runat="server" style="width: 95%;" class="TextBoxes" id="txtSerchTerm_Precard"
                                                                onkeypress="txtSerchTerm_Precard_onKeyPess(event);" />
                                                        </td>
                                                        <td style="width: 5%">
                                                            <ComponentArt:ToolBar ID="TlbPrecardQuickSearch" runat="server" CssClass="toolbar"
                                                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                                                UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemSearch_TlbPrecardQuickSearch" runat="server"
                                                                        ClientSideCommand="tlbItemSearch_TlbPrecardQuickSearch_onClick();" DropDownImageHeight="16px"
                                                                        DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="search.png" ImageWidth="16px"
                                                                        ItemType="Command" meta:resourcekey="tlbItemSearch_TlbPrecardQuickSearch" TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                <table id="tbRefresh_RulePrecards">
                                                    <tr>
                                                        <td>
                                                            <ComponentArt:ToolBar ID="TlbRefresh_GridRulePrecards_RulePrecards" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridRulePrecards_RulePrecards"
                                                                        runat="server" ClientSideCommand="Refresh_GridRulePrecards_RulePrecards();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridRulePrecards_RulePrecards"
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
                                <td>
                                    <%--<div style="overflow: auto" id ="divGridRulePrecard_RulePrecards">--%>
                                    <table id="tbGridRulePrecard_RulePrecards">
                                        <tr>
                                            <td>
                                                <ComponentArt:CallBack ID="CallBack_GridRulePrecards_RulePrecards" runat="server" OnCallback="CallBack_GridRulePrecards_RulePrecards_onCallBack">
                                                    <Content>
                                                        <ComponentArt:DataGrid ID="GridRulePrecards_RulePrecards" runat="server" AllowColumnResizing="false"
                                                            AllowHorizontalScrolling="false" CssClass="HGridClass" Height="100%" ImagesBaseUrl="images/Grid/"
                                                            IndentCellCssClass="HIndentCell" IndentCellWidth="19" meta:resourcekey="GridRulePrecards_RulePrecards"
                                                            AllowMultipleSelect="false" PagerStyle="Numbered" PagerTextCssClass="GridFooterText" EditOnClickSelectedItem="false"
                                                            PageSize="1" PreloadLevels="false" RunningMode="Client" ScrollBarCssClass="ScrollBar" PreBindServerTemplates="true"
                                                            ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16" ScrollGripCssClass="HScrollGrip"
                                                            ScrollImagesFolderUrl="images/Grid/Scroller/" ScrollTopBottomImageHeight="2"
                                                            ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" ShowFooter="false"
                                                            TreeLineImageHeight="20" TreeLineImageWidth="19" Width="850">
                                                            <Levels>                                                              
                                                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false"
                                                                    DataCellCssClass="HDataCell" DataKeyField="ID"
                                                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass" RowCssClass="H0RowClass"
                                                                    HeadingRowCssClass="HL0HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                                                     SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL0SelectorCell"
                                                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                                                    SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                                    SortImageWidth="9" TableHeadingCssClass="HTableHeading">
                                                                    <Columns>
                                                                        <ComponentArt:GridColumn DataField="ID" Visible="false" AllowEditing="False" DataType="System.Decimal" FormatString="###" />
                                                                        <ComponentArt:GridColumn DataField="ExistPrecard" Visible="false" AllowEditing="False" DataType="System.Decimal" FormatString="###" />
                                                                        <ComponentArt:GridColumn DataField="PrecardID" Visible="false" AllowEditing="False" DataType="System.Decimal" FormatString="###" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="PrecardName" HeadingText="نام پیشکارت"
                                                                            meta:resourcekey="clmnPrecardName_GridRulePrecards_RulePrecards" Width="700" AllowEditing="False" DataCellClientTemplateId="DataCellClientTemplate_PrecardName_GridRulePrecards_RulePrecards" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="Active" HeadingText="فعال" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                                            meta:resourcekey="clmnPrecardActive_GridRulePrecards_RulePrecards" Width="50" AllowEditing="True" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="Operator" HeadingText="اپراتور" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                                            meta:resourcekey="clmnPrecardOperator_GridRulePrecards_RulePrecards" Width="60" AllowEditing="True" />
                                                                        <ComponentArt:GridColumn DataField="PrecardColor" Visible="false" />
                                                                    </Columns>
                                                                </ComponentArt:GridLevel>                                                               
                                                                <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL1AlternatingRowClass"
                                                                    DataCellCssClass="HDataCell" DataKeyField="ID" ShowHeadingCells="false"
                                                                    GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass" RowCssClass="H0RowClass"
                                                                    HeadingRowCssClass="HL1HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                                                    SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL1SelectorCell"
                                                                    SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                                                    TableHeadingCssClass="HTableHeading" EditCommandClientTemplateId="EditCommandTemplate">
                                                                    <Columns>
                                                                        <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="ID" />
                                                                        <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="PrecardID" DataType="System.Decimal" FormatString="###" />
                                                                        <ComponentArt:GridColumn Visible="false" DataField="ParamType" />
                                                                        <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="ParamID" DataType="System.Decimal" FormatString="###" />
                                                                        <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="KeyName" FormatString="###" />
                                                                        <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="ExistParam" DataType="System.Decimal" FormatString="###" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="PrameterName" HeadingText="نام پارامتر"
                                                                            meta:resourcekey="clmnParameterName_GridRulePrecards_RulePrecards" Width="600" AllowEditing="False" DataCellClientTemplateId="DataCellClientTemplate_PrameterName_GridRulePrecards_RulePrecards" />
                                                                        <ComponentArt:GridColumn Align="Center" DataField="ParameterValue" HeadingText="مقدار" AllowEditing="True"
                                                                            meta:resourcekey="clmnValue_GridRulePrecards_RulePrecards" Width="200" TextWrap="true" DataCellClientTemplateId="DataCellClientTemplate_ParameterValue_GridRulePrecards_RulePrecards" />
                                                                        <ComponentArt:GridColumn DataField="ContinueOnTomorrow" ColumnType="CheckBox" Visible="false" />
                                                                        <ComponentArt:GridColumn DataField="ParameterColor" Visible="false" />                                                                      
                                                                    </Columns>
                                                                </ComponentArt:GridLevel>
                                                            </Levels>
                                                            <ClientTemplates>
                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_PrecardName_GridRulePrecards_RulePrecards">
                                                                    <table style="width: 100%; height:17px ; background-color: ##DataItem.GetMember('PrecardColor').Value##">
                                                                        <tr>
                                                                            <td align="center">##DataItem.GetMember('PrecardName').Value##
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ComponentArt:ClientTemplate>
                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_Active_GridRulePrecards_RulePrecards">
                                                                    <table style="width: 100%; height:17px ;background-color: ##DataItem.GetMember('PrecardColor').Value##">
                                                                        <tr>
                                                                            <td >##DataItem.GetMember('Active').Checked##
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ComponentArt:ClientTemplate>
                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_Operator_GridRulePrecards_RulePrecards">
                                                                    <table style="width: 100%; height:17px ; background-color: ##DataItem.GetMember('PrecardColor').Value##">
                                                                        <tr>
                                                                             <td >##DataItem.GetMember('Operator').Checked##   
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ComponentArt:ClientTemplate>
                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_PrameterName_GridRulePrecards_RulePrecards">
                                                                    <table style="width: 100%;  height:17px ;background-color: ##DataItem.GetMember('ParameterColor').Value##">
                                                                        <tr>
                                                                            <td >##DataItem.GetMember('PrameterName').Value##
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ComponentArt:ClientTemplate>
                                                                <ComponentArt:ClientTemplate ID="DataCellClientTemplate_ParameterValue_GridRulePrecards_RulePrecards">
                                                                    <table style="width: 100% ;">
                                                                        <tr>
                                                                            <td style="background-color: ##DataItem.GetMember('ParameterColor').Value##">
                                                                                ##DataItem.GetMember('ParameterValue').Value##
                                                                            </td>
                                                                            <td style="width:5%">
                                                                                <img src="Images/ToolBar/edit.png" alt="" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ComponentArt:ClientTemplate>
                                                            </ClientTemplates>
                                                            <ClientEvents>
                                                                <Load EventHandler="GridRulePrecards_RulePrecards_onLoad" />
                                                                <ItemDoubleClick EventHandler="GridRulePrecards_RulePrecards_onItemDoubleClick" />
                                                                <ItemCheckChange EventHandler="GridRulePrecards_RulePrecards_onItemCheckChange" />
                                                            </ClientEvents>
                                                        </ComponentArt:DataGrid>
                                                        <asp:HiddenField runat="server" ID="ErrorHiddenField_RulePrecards_RulePrecards" />
                                                        <asp:HiddenField runat="server" ID="RulePrecardParamObjHiddenField_RulePrecards" />
                                                        <asp:HiddenField runat="server" ID="RuleDetailObjHiddenField_RulePrecards" />
                                                    </Content>
                                                    <ClientEvents>
                                                        <CallbackComplete EventHandler="CallBack_GridRulePrecards_RulePrecards_onCallbackComplete" />
                                                        <CallbackError EventHandler="CallBack_GridRulePrecards_RulePrecards_onCallbackError" />
                                                    </ClientEvents>
                                                </ComponentArt:CallBack>
                                            </td>
                                        </tr>
                                    </table>

                                    <%--</div>--%>
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
            Modal="true" AllowResize="false" AllowDrag="false" Alignment="TopCentre" ID="DialogParameterValue"
            runat="server" Width="200px">
            <Content>
                <table runat="server" style="font-family: Arial; border-top: gray 1px double; border-right: black 1px double; font-size: small; border-left: black 1px double; border-bottom: gray 1px double; width: 100%;"
                    meta:resourcekey="tblParameterValue_DialogParameterValue"
                    class="BodyStyle">
                    <tr>
                        <td id="Title_DialogParameterValue" valign="bottom" style="color: White; font-size: 13px; font-family: Arial; font-weight: bolder"></td>
                    </tr>
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
                                    <td>
                                        <%-- id="header_parametervalue_uivalidationintroduction" style="color: white; font-weight: bold; font-family: arial; width: 100%">parameter value
                                        <asp:label id="lblparametervalue_RulePrecards" runat="server" cssclass="whitelabel" meta:resourcekey="lblparametervalue_RulePrecards"
                                                                    text=": عددی"></asp:label>>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 100%" class="TabStripContainer">
                                            <ComponentArt:TabStrip ID="TabStripRulePrecardsTerms" runat="server" DefaultGroupTabSpacing="1"
                                                DefaultItemLookId="DefaultTabLook" DefaultSelectedItemLookId="SelectedTabLook"
                                                ImagesBaseUrl="images/TabStrip" MultiPageId="MultiPageRulePrecardsTerms" ScrollLeftLookId="ScrollItem"
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
                                                    <ComponentArt:TabStripTab ID="tbNumeric_TabStripRulePrecardsTerms" meta:resourcekey="tbNumeric_TabStripRulePrecardsTerms"
                                                        Text="عددی" Value="Numeric" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbTime_TabStripRulePrecardsTerms" meta:resourcekey="tbTime_TabStripRulePrecardsTerms"
                                                        Text="زمان" Value="Time" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                    <ComponentArt:TabStripTab ID="tbDate_TabStripRulePrecardsTerms" meta:resourcekey="tbDate_TabStripRulePrecardsTerms"
                                                        Text="تاریخ" Value="Date" Enabled="false">
                                                    </ComponentArt:TabStripTab>
                                                </Tabs>
                                            </ComponentArt:TabStrip>
                                        </div>
                                        <ComponentArt:MultiPage ID="MultiPageRulePrecardsTerms" runat="server" CssClass="MultiPage"
                                            Width="300">
                                            <ComponentArt:PageView ID="pgvNumeric_MultiPageRulePrecardsTerms" runat="server"
                                                Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblnumeric_RulePrecards" runat="server" CssClass="whitelabel" meta:resourcekey="lblnumeric_RulePrecards"
                                                                Text=": عددی"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <input id="txtNumeric_RulePrecards" runat="server" type="text" class="TextBoxes" onkeypress="txtNumeric_RulePrecards_onkeypress(event)"
                                                                            style="width: 85px;" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <ComponentArt:ToolBar ID="TlbConfirm_pgvNumeric_MultiPageRulePrecardsTerms" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRulePrecardsTerms"
                                                                                    runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRulePrecardsTerms_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                                                    Enabled="True" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvNumeric_MultiPageRulePrecardsTerms"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                    <td>
                                                                        <ComponentArt:ToolBar ID="TlbClean_pgvNumeric_MultiPageRulePrecardsTerms" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_pgvNumeric_MultiPageRulePrecardsTerms"
                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_pgvNumeric_MultiPageRulePrecardsTerms_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                    Enabled="True" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_pgvNumeric_MultiPageRulePrecardsTerms"
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
                                            <ComponentArt:PageView ID="pgvTime_MultiPageRulePrecardsTerms" runat="server" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTime_RulePrecards" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblTime_RulePrecards"
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
                                                                                                <input type="text" id="TimeSelector_Hour_RulePrecards_txtHour" style="width: 70%; text-align: center"
                                                                                                    onchange="TimeSelector_Hour_RulePrecards_onChange('txtHour')" onkeypress="TimeSelector_Hour_RulePrecards_txtHour_onKeyPress(event)" />
                                                                                            </td>
                                                                                            <td align="center">:
                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <input type="text" id="TimeSelector_Hour_RulePrecards_txtMinute" style="width: 70%; text-align: center" onkeypress="TimeSelector_Hour_RulePrecards_txtMinute_onKeyPress(event)"
                                                                                                    onchange="TimeSelector_Hour_RulePrecards_onChange('txtMinute')" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td>
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 10%">
                                                                                                <input id="chbNextDay_RulePrecards" type="checkbox" style="display: none" /></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblNextDay_RulePrecards" runat="server" CssClass="WhiteLabel" Text="روز بعد" meta:resourcekey="lblNextDay_RulePrecards" Visible="false"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="center">
                                                                        <ComponentArt:ToolBar ID="TlbConfirm_pgvTime_MultiPageRulePrecardsTerms" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRulePrecardsTerms"
                                                                                    runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRulePrecardsTerms_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                                                    Enabled="True" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvTime_MultiPageRulePrecardsTerms"
                                                                                    TextImageSpacing="5" />
                                                                            </Items>
                                                                        </ComponentArt:ToolBar>
                                                                    </td>
                                                                    <td>
                                                                        <ComponentArt:ToolBar ID="TlbClean_pgvTime_MultiPageRulePrecardsTerms" runat="server"
                                                                            CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                            ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                            <Items>
                                                                                <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_pgvTime_MultiPageRulePrecardsTerms"
                                                                                    runat="server" ClientSideCommand="tlbItemClean_TlbClean_pgvTime_MultiPageRulePrecardsTerms_onClick();"
                                                                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                                    Enabled="True" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_pgvTime_MultiPageRulePrecardsTerms"
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
                                            <ComponentArt:PageView ID="pgvDate_MultiPageRulePrecardsTerms" runat="server" Width="100%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDate_RulePrecards" runat="server" CssClass="WhiteLabel" meta:resourcekey="lblDate_RulePrecards"
                                                                Text=": تاریخ"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="Container_DateCalendars_RulePrecards">
                                                            <table runat="server" id="Container_pdpDate_RulePrecards" visible="false" style="width: 100%" onkeypress="Container_pdpDate_RulePrecards_onKeyPress(event)">
                                                                <tr>
                                                                    <td>
                                                                        <pcal:PersianDatePickup ID="pdpDate_RulePrecards" runat="server" CssClass="PersianDatePicker"
                                                                            ReadOnly="true"></pcal:PersianDatePickup>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table runat="server" id="Container_gdpDate_RulePrecards" visible="false" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <table id="Container_gCalDate_RulePrecards" border="0" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td onmouseup="btn_gdpDate_RulePrecards_OnMouseUp(event)">
                                                                                    <ComponentArt:Calendar ID="gdpDate_RulePrecards" runat="server" ControlType="Picker"
                                                                                        PickerCssClass="picker" PickerCustomFormat="yyyy/MM/dd" PickerFormat="Custom"
                                                                                        SelectedDate="2008-1-1">
                                                                                        <ClientEvents>
                                                                                            <SelectionChanged EventHandler="gdpDate_RulePrecards_OnDateChange" />
                                                                                        </ClientEvents>
                                                                                    </ComponentArt:Calendar>
                                                                                </td>
                                                                                <td style="font-size: 10px;">&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    <img id="btn_gdpDate_RulePrecards" alt="" class="calendar_button" onclick="btn_gdpDate_RulePrecards_OnClick(event)" onkeypress="btn_gdpDate_RulePrecards_onkeyPress(event)"
                                                                                        onmouseup="btn_gdpDate_RulePrecards_OnMouseUp(event)" src="Images/Calendar/btn_calendar.gif" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <ComponentArt:Calendar ID="gCalDate_RulePrecards" runat="server" AllowMonthSelection="false"
                                                                            AllowMultipleSelection="false" AllowWeekSelection="false" CalendarCssClass="calendar"
                                                                            CalendarTitleCssClass="title" ControlType="Calendar" DayCssClass="day" DayHeaderCssClass="dayheader"
                                                                            DayHoverCssClass="dayhover" DayNameFormat="FirstTwoLetters" ImagesBaseUrl="Images/Calendar"
                                                                            MonthCssClass="month" NextImageUrl="cal_nextMonth.gif" NextPrevCssClass="nextprev"
                                                                            OtherMonthDayCssClass="othermonthday" PopUp="Custom" PopUpExpandControlId="btn_gdpDate_RulePrecards"
                                                                            PrevImageUrl="cal_prevMonth.gif" SelectedDate="2008-1-1" SelectedDayCssClass="selectedday"
                                                                            SwapDuration="300" SwapSlide="Linear" VisibleDate="2008-1-1">
                                                                            <ClientEvents>
                                                                                <SelectionChanged EventHandler="gCalDate_RulePrecards_OnChange" />
                                                                                <Load EventHandler="gCalDate_RulePrecards_onLoad" />
                                                                            </ClientEvents>
                                                                        </ComponentArt:Calendar>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <ComponentArt:ToolBar ID="TlbConfirm_pgvDate_MultiPageRulePrecardsTerms" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRulePrecardsTerms"
                                                                        runat="server" ClientSideCommand="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRulePrecardsTerms_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png"
                                                                        Enabled="True" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemConfirm_TlbConfirm_pgvDate_MultiPageRulePrecardsTerms"
                                                                        TextImageSpacing="5" />
                                                                </Items>
                                                            </ComponentArt:ToolBar>
                                                        </td>
                                                        <td>
                                                            <ComponentArt:ToolBar ID="TlbClean_pgvDate_MultiPageRulePrecardsTerms" runat="server"
                                                                CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                                <Items>
                                                                    <ComponentArt:ToolBarItem ID="tlbItemClean_TlbClean_pgvDate_MultiPageRulePrecardsTerms"
                                                                        runat="server" ClientSideCommand="tlbItemClean_TlbClean_pgvDate_MultiPageRulePrecardsTerms_onClick();"
                                                                        DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                                                        Enabled="True" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClean_TlbClean_pgvDate_MultiPageRulePrecardsTerms"
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
            </Content>
        </ComponentArt:Dialog>
        <asp:HiddenField runat="server" ID="hfheader_RulePrecards_RulePrecards" meta:resourcekey="hfheader_RulePrecards_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfTitle_RuleParameters_DialogRulePrecards" meta:resourcekey="hfTitle_RuleParameters_DialogRulePrecards" />
        <asp:HiddenField runat="server" ID="hfTitle_RulePrecardsParameters_DialogRulePrecards" meta:resourcekey="hfTitle_RulePrecardsParameters_DialogRulePrecards" />
        <asp:HiddenField runat="server" ID="hfTitle_RulePrecards_DialogRulePrecards" meta:resourcekey="hfTitle_RulePrecards_DialogRulePrecards" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridRulePrecards_RulePrecards" meta:resourcekey="hfloadingPanel_GridRulePrecards_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfheader_RuleParameter_RulePrecards" meta:resourcekey="hfheader_RuleParameter_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfErrorType_RulePrecards" meta:resourcekey="hfErrorType_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RulePrecards" meta:resourcekey="hfConnectionError_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_RulePrecards" meta:resourcekey="hfDeleteMessage_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RulePrecardsParameters_RulePrecards" meta:resourcekey="hfCloseMessage_RulePrecardsParameters_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RulePrecards_RulePrecards" meta:resourcekey="hfCloseMessage_RulePrecards_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RuleParameters_RulePrecards" meta:resourcekey="hfCloseMessage_RuleParameters_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfheader_RulePrecardsParameters_RulePrecards" meta:resourcekey="hfheader_RulePrecardsParameters_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfheader_RuleText_RulePrecards" meta:resourcekey="hfheader_RuleText_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfRulePrecardParamObjHiddenField_RulePrecards" meta:resourcekey="hfRulePrecardParamObjHiddenField_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfTitle_DialogParameterValue" meta:resourcekey="hfTitle_DialogParameterValue" />
        <asp:HiddenField runat="server" ID="hfParameterValueNotEmpty_RulePrecards" meta:resourcekey="hfParameterValueNotEmpty_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfWarningType_RulePrecards" meta:resourcekey="hfWarningType_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfheader_RuleDetails_RulePrecards" meta:resourcekey="hfheader_RuleDetails_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfRuleDetailObjHiddenField_RulePrecards" meta:resourcekey="hfRuleDetailObjHiddenField_RulePrecards" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_RulePrecards" meta:resourcekey="hfCloseMessage_RulePrecards" />



    </form>
</body>
</html>
