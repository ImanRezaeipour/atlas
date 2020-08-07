<%@ Page Language="C#" AutoEventWireup="true" Inherits="FlowConditions" Codebehind="FlowConditions.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/hierarchicalGridStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="FlowConditionsForm" runat="server" meta:resourcekey="FlowConditionsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table id="Mastertbl_FlowConditions" style="width: 100%; font-family: Arial; font-size: small" class="BodyStyle" meta:resourcekey="Mastertbl_FlowConditions">
                <tr>
                    <td>
                        <ComponentArt:ToolBar ID="TlbFlowConditions" runat="server" CssClass="toolbar"
                            DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                            DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                            DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                            DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                            UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemSave_TlbFlowConditions" runat="server"
                                    ClientSideCommand="tlbItemSave_TlbFlowConditions_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemSave_TlbFlowConditions" TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbFlowConditions" runat="server"
                                    ClientSideCommand="tlbItemHelp_TlbFlowConditions_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemHelp_TlbFlowConditions" TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemFormReconstruction_TlbFlowConditions"
                                    runat="server" ClientSideCommand="tlbItemFormReconstruction_TlbFlowConditions_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemFormReconstruction_TlbFlowConditions"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbFlowConditions" runat="server"
                                    ClientSideCommand="tlbItemExit_TlbFlowConditions_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px"
                                    ItemType="Command" meta:resourcekey="tlbItemExit_TlbFlowConditions" TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="BoxStyle">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td id="header_FlowConditions_FlowConditions" class="HeaderLabel" style="width: 30%">شروط جریان
                                            </td>
                                            <td id="loadingPanel_GridFlowConditions_FlowConditions" class="HeaderLabel" style="width: 30%">&nbsp;</td>
                                            <td style="width: 30%">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td style="width:10%">
                                                            <input type="checkbox" id="chbAllPrecardsEndFlow_FlowConditions" onclick="chbAllPrecardsEndFlow_FlowConditions_onclick();"/>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblAllPrecardsEndFlow_FlowConditions" CssClass="WhiteLabel" meta:resourcekey="lblAllPrecardsEndFlow_FlowConditions"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                <ComponentArt:ToolBar ID="TlbRefresh_GridFlowConditions_FlowConditions" runat="server"
                                                    CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                    ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridFlowConditions_FlowConditions"
                                                            runat="server" ClientSideCommand="Refresh_GridFlowConditions_FlowConditions();"
                                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridFlowConditions_FlowConditions"
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
                                    <div style="overflow: auto">
                                        <ComponentArt:CallBack ID="CallBack_GridFlowConditions_FlowConditions" runat="server" OnCallback="CallBack_GridFlowConditions_FlowConditions_onCallBack">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridFlowConditions_FlowConditions" runat="server" AllowColumnResizing="false"
                                                    AllowHorizontalScrolling="false" CssClass="HGridClass" Height="100%" ImagesBaseUrl="images/Grid/"
                                                    IndentCellCssClass="HIndentCell" IndentCellWidth="19" meta:resourcekey="GridFlowConditions_FlowConditions"
                                                    AllowMultipleSelect="false" PagerStyle="Numbered" PagerTextCssClass="GridFooterText" EditOnClickSelectedItem="false"
                                                    PageSize="10" PreloadLevels="false" RunningMode="Client" ScrollBarCssClass="ScrollBar" PreBindServerTemplates="true"
                                                    ScrollBarWidth="16" ScrollButtonHeight="17" ScrollButtonWidth="16" ScrollGripCssClass="HScrollGrip"
                                                    ScrollImagesFolderUrl="images/Grid/Scroller/" ScrollTopBottomImageHeight="2"
                                                    ScrollTopBottomImagesEnabled="true" ScrollTopBottomImageWidth="16" ShowFooter="false"
                                                    TreeLineImageHeight="20" TreeLineImageWidth="19" Width="850">
                                                    <Levels>
                                                        <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL0AlternatingRowClass"
                                                            DataCellCssClass="HDataCell" DataKeyField="ID"
                                                            GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                                            HeadingRowCssClass="HL0HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                                            RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL0SelectorCell"
                                                            SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                                            SortAscendingImageUrl="asc.gif" SortDescendingImageUrl="desc.gif" SortImageHeight="5"
                                                            SortImageWidth="9" TableHeadingCssClass="HTableHeading">
                                                            <Columns>
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" AllowEditing="False" DataType="System.Decimal" FormatString="###" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Name" HeadingText="نام گروه پیشکارت"
                                                                    meta:resourcekey="clmnPrecardGroupName_GridFlowConditions_FlowConditions" Width="850" AllowEditing="False" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                        <ComponentArt:GridLevel AllowReordering="false" AllowSorting="false" AlternatingRowCssClass="HL1AlternatingRowClass"
                                                            DataCellCssClass="HDataCell" DataKeyField="PrecardID" 
                                                            GroupHeadingCssClass="HTableHeading" HeadingCellCssClass="HHeadingCellClass"
                                                            HeadingRowCssClass="HL1HeadingRowClass" HeadingTextCssClass="HHeadingTextClass"
                                                            RowCssClass="HRowClass" SelectedRowCssClass="HSelectedRowClass" SelectorCellCssClass="HL1SelectorCell"
                                                            SelectorCellWidth="19" SelectorImageUrl="selector.gif" ShowSelectorCells="true"
                                                            TableHeadingCssClass="HTableHeading" EditCommandClientTemplateId="EditCommandTemplate">
                                                            <Columns>
                                                                <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="ID" DataType="System.Decimal" FormatString="###" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="State" HeadingText=" " AllowEditing="false" Width="20" DataCellClientTemplateId="DataCellClientTemplateId_State_GridFlowConditions_FlowConditions"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="PrecardName" HeadingText="نام پیشکارت"
                                                                    meta:resourcekey="clmnPrecardName_GridFlowConditions_FlowConditions" Width="200" AllowEditing="False" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="PrecardCode" HeadingText="کد پیشکارت"
                                                                    meta:resourcekey="clmnPreCardCode_GridFlowConditions_FlowConditions" Width="100" TextWrap="true" AllowEditing="False" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="EndFlow" HeadingText="پایان جریان" ColumnType="CheckBox" AllowEditing="True" 
                                                                    meta:resourcekey="clmnEndFlow_GridFlowConditions_FlowConditions" Width="90" TextWrap="true" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="OperatorTitle" HeadingText="عملگر" AllowEditing="True"
                                                                    meta:resourcekey="clmnOperator_GridFlowConditions_FlowConditions" Width="100" TextWrap="true" EditControlType="Custom" EditCellServerTemplateId="GridServerTemplate_ConditionOperator_GridFlowConditions_FlowConditions" CustomEditGetExpression="GetConditionOperator_GridFlowConditions_FlowConditions(DataItem);" CustomEditSetExpression="SetConditionOperator_GridFlowConditions_FlowConditions(DataItem);" />
                                                                <ComponentArt:GridColumn Align="Center" DataField="Value" HeadingText="مقدار" AllowEditing="True" 
                                                                    meta:resourcekey="clmnValue_GridFlowConditions_FlowConditions" Width="80" TextWrap="true" EditControlType="Custom" EditCellServerTemplateId="GridServerTemplate_ConditionValue_GridFlowConditions_FlowConditions" CustomEditGetExpression="GetConditionValue_GridFlowConditions_FlowConditions(DataItem)" CustomEditSetExpression="SetConditionValue_GridFlowConditions_FlowConditions(DataItem)"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Description" HeadingText="توضیحات" AllowEditing="True" EditControlType="Custom" EditCellServerTemplateId="GridServerTemplate_Description_GridFlowConditions_FlowConditions" CustomEditGetExpression="GetDescription_GridFlowConditions_FlowConditions(DataItem)"  CustomEditSetExpression="SetDescription_GridFlowConditions_FlowConditions(DataItem)"
                                                                    meta:resourcekey="clmnDescription_GridFlowConditions_FlowConditions" Width="80" TextWrap="true"/>                                                                
                                                                <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="PrecardAccessGroupDetailID" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="PrecardID"/>
                                                                <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="OperatorKey" />
                                                                <ComponentArt:GridColumn Visible="false" AllowEditing="False" DataField="ValueType"/>
                                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="EditTemplate" EditControlType="EditCommand" Width="50" Align="Center" />
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientTemplates>
                                                        <ComponentArt:ClientTemplate ID="EditTemplate">
                                                                <img src="Images/ToolBar/edit.png" alt="" onclick="javascript:EditGridFlowConditions_FlowConditions('## DataItem.ClientId ##');" title="##SetCellTitle_GridFlowConditions_FlowConditions('Edit')##" /></a>&nbsp;<a>
                                                                    <img src="Images/ToolBar/remove.png" alt="" onclick="javascript:DeleteGridFlowConditions_FlowConditions('## DataItem.ClientId ##');" title="##SetCellTitle_GridFlowConditions_FlowConditions('Delete')##" /></a>
                                                        </ComponentArt:ClientTemplate>
                                                        <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                                                            <a>
                                                                <img src="Images/ToolBar/save.png" alt="" onclick="javascript:UpdateGridFlowConditions_FlowConditions();" title="##SetCellTitle_GridFlowConditions_FlowConditions('Save')##" /></a>&nbsp;<a>
                                                                    <img src="Images/ToolBar/cancel.png" alt="" onclick="javascript:CancelEditGridFlowConditions_FlowConditions('## DataItem.ClientId ##');" title="##SetCellTitle_GridFlowConditions_FlowConditions('Cancel')##" /></a>
                                                        </ComponentArt:ClientTemplate>
                                                        <ComponentArt:ClientTemplate ID="DataCellClientTemplateId_State_GridFlowConditions_FlowConditions">
                                                            <table>
                                                                <tr>
                                                                    <td title="##SetCellTitle_GridFlowConditions_FlowConditions(DataItem.GetMember('State').Value)##">
                                                                        <img src="##SetState_GridKartable_Kartable(DataItem.GetMember('State').Value)##" alt="" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ComponentArt:ClientTemplate>
                                                    </ClientTemplates>
                                                    <ServerTemplates>
                                                        <ComponentArt:GridServerTemplate ID="GridServerTemplate_ConditionOperator_GridFlowConditions_FlowConditions">
                                                            <Template>
                                                                <asp:DropDownList runat="server" ID="cmbConditionOperator_FlowConditions" style="font-family:Tahoma; font-size:11px" onchange="cmbConditionOperator_FlowConditions_onChange();">
                                                                </asp:DropDownList>
                                                            </Template>
                                                        </ComponentArt:GridServerTemplate>
                                                        <ComponentArt:GridServerTemplate ID="GridServerTemplate_ConditionValue_GridFlowConditions_FlowConditions">
                                                            <Template>
                                                                <asp:TextBox runat="server" ID="txtConditionValue_FlowConditions" style="font-family:Tahoma; font-size:11px">
                                                                </asp:TextBox>
                                                            </Template>
                                                        </ComponentArt:GridServerTemplate>
                                                        <ComponentArt:GridServerTemplate ID="GridServerTemplate_Description_GridFlowConditions_FlowConditions">
                                                            <Template>
                                                                <asp:TextBox runat="server" ID="txtDescription_FlowConditions" style="font-family:Tahoma; font-size:11px">
                                                                </asp:TextBox>
                                                            </Template>
                                                        </ComponentArt:GridServerTemplate>
                                                    </ServerTemplates>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridFlowConditions_FlowConditions_onLoad"/>
                                                        <ItemBeforeCheckChange EventHandler="GridFlowConditions_FlowConditions_onItemBeforeCheckChange"/>
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_FlowConditions_FlowConditions" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridFlowConditions_FlowConditions_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridFlowConditions_FlowConditions_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </div>
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
        <asp:HiddenField runat="server" ID="hfTitle_DialogFlowConditions" meta:resourcekey="hfTitle_DialogFlowConditions" />
        <asp:HiddenField runat="server" ID="hfheader_FlowConditions_FlowConditions" meta:resourcekey="hfheader_FlowConditions_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridFlowConditions_FlowConditions" meta:resourcekey="hfloadingPanel_GridFlowConditions_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfDeleteMessage_FlowConditions" meta:resourcekey="hfDeleteMessage_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfCloseMessage_FlowConditions" meta:resourcekey="hfCloseMessage_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfErrorType_FlowConditions" meta:resourcekey="hfErrorType_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfConnectionError_FlowConditions" meta:resourcekey="hfConnectionError_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfView_FlowConditions" meta:resourcekey="hfView_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfEdit_FlowConditions" meta:resourcekey="hfEdit_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfDelete_FlowConditions" meta:resourcekey="hfDelete_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfSave_FlowConditions" meta:resourcekey="hfSave_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfCancel_FlowConditions" meta:resourcekey="hfCancel_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfMinute_FlowConditions" meta:resourcekey="hfMinute_FlowConditions" />
        <asp:HiddenField runat="server" ID="hfDay_FlowConditions" meta:resourcekey="hfDay_FlowConditions" />
    </form>
</body>
</html>
