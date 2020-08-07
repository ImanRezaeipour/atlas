<%@ Page Language="C#" AutoEventWireup="true" Inherits="ReportParametersConditions" Codebehind="ReportParametersConditions.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/combobox.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/mainpage.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <link id="Link2" href="Css/gridStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <title></title>   
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <form id="ReportParametersConditionsForm" runat="server" meta:resourcekey="ReportParametersConditionsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table style="font-size: small; font-family: Arial; width: 100%" class="BoxStyle">
                <tr>
                    <td colspan="2">
                        <ComponentArt:ToolBar ID="TlbReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>
                                <ComponentArt:ToolBarItem ID="tlbItemRegister_TlbReportParametersConditions" runat="server"
                                    ClientSideCommand="tlbItemRegister_TlbReportParametersConditions_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px"
                                    ImageUrl="save.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemSave_TlbReportParametersConditions"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbReportParametersConditions"
                                    runat="server" ClientSideCommand="tlbItemClear_TlbReportParametersConditions_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbReportParametersConditions"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbReportParametersConditions"
                                    runat="server" ClientSideCommand="tlbItemHelp_TlbReportParametersConditions_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemHelp_TlbReportParametersConditions"
                                    TextImageSpacing="5" />
                                <ComponentArt:ToolBarItem ID="tlbItemExit_TlbReportParametersConditions" runat="server"
                                    ClientSideCommand="tlbItemExit_TlbReportParametersConditions_onClick();" DropDownImageHeight="16px"
                                    DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png" ImageWidth="16px" ItemType="Command"
                                    meta:resourcekey="tlbItemExit_TlbReportParametersConditions" TextImageSpacing="5" />
                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table style="width: 90%;">
                            <tr>
                                <td style="width: 45%">
                                    <asp:Label ID="lblField_ReportParametersConditions" runat="server" CssClass="WhiteLabel" Text=": فیلد" meta:resourcekey="lblField_ReportParametersConditions"></asp:Label>
                                </td>
                                <td style="width: 45%">&nbsp;</td>
                                <td style="width: 45%">
                                    <asp:Label ID="lblFieldValue_ReportParametersConditions" runat="server" Text=": مقدار فیلد" CssClass="WhiteLabel" meta:resourcekey="lblFieldValue_ReportParametersConditions"></asp:Label>
                                </td>
                                <td style="width: 10%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:CallBack ID="CallBack_cmbField_ReportParametersConditions" runat="server" Height="26" OnCallback="CallBack_cmbField_ReportParametersConditions_onCallBack">
                                        <Content>
                                            <ComponentArt:ComboBox ID="cmbField_ReportParametersConditions" runat="server" AutoComplete="true" AutoFilter="true" AutoHighlight="false" CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner" DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png" Enabled="true" ExpandDirection="Down" FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem" ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%" TextBoxCssClass="comboTextBox" TextBoxEnabled="true">
                                                <ClientEvents>
                                                    <Expand EventHandler="cmbField_ReportParametersConditions_onExpand" />
                                                </ClientEvents>
                                            </ComponentArt:ComboBox>
                                            <asp:HiddenField ID="ErrorHiddenField_ReportParametersConditions" runat="server" />
                                        </Content>
                                        <ClientEvents>
                                            <BeforeCallback EventHandler="CallBack_cmbField_ReportParametersConditions_onBeforeCallback" />
                                            <CallbackComplete EventHandler="CallBack_cmbField_ReportParametersConditions_onCallbackComplete" />
                                            <CallbackError EventHandler="CallBack_cmbField_ReportParametersConditions_onCallbackError" />
                                        </ClientEvents>
                                    </ComponentArt:CallBack>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <ComponentArt:ToolBar ID="TlbInsertFieldToConditions_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemInsertFiledToConditions_TlbInsertFieldToConditions_ReportParametersConditions" runat="server" ClientSideCommand="tlbItemInsertFieldToConditions_TlbInsertFieldToConditions_ReportParametersConditions_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemInsertFieldToConditions_TlbInsertFieldToConditions_ReportParametersConditions" TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                            <td style="display:none;">
                                                <ComponentArt:ToolBar ID="TlbInsertFieldToOrders_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                    <Items>
                                                        <ComponentArt:ToolBarItem ID="tlbItemInsertFiledToOrders_TlbInsertFieldToOrders_ReportParametersConditions" runat="server" ClientSideCommand="tlbItemInsertFieldToOrders_TlbInsertFieldToOrders_ReportParametersConditions_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemInsertFieldToOrders_TlbInsertFieldToOrders_ReportParametersConditions" TextImageSpacing="5" />
                                                    </Items>
                                                </ComponentArt:ToolBar>
                                            </td>
                                            
                                        </tr>
                                    </table>

                                </td>
                                <td>
                                    <input id="txtFieldValue_ReportParametersConditions" type="text" class="TextBoxes" style="width: 150px" />
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbInsertValueToConditions_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemInsertValueToConditions_TlbInsertToConditions_ReportParametersConditions" runat="server" ClientSideCommand="tlbItemInsertValueToConditions_TlbInsertValueToConditions_ReportParametersConditions_onClick();" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="eyeglasses.png" ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemInsertValueToConditions_TlbInsertValueToConditions_ReportParametersConditions" TextImageSpacing="5" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 95%" valign="top" dir="ltr">
                        <ComponentArt:CallBack ID="CallBack_txtConditions_ReportParametersConditions" runat="server" Height="162" OnCallback="CallBack_txtConditions_ReportParametersConditions_onCallBack">
                            <Content>
                                <textarea id="txtConditions_ReportParametersConditions" meta:resourcekey="txtConditions_ReportParametersConditions" class="TextBoxes" rows="20" runat="server" cols="20" name="S1" style="width: 100%; height: 160px;" disabled="disabled"></textarea>
                                <asp:HiddenField ID="ErrorHiddenField_txtConditions_ReportParametersConditions" runat="server" />
                                <asp:HiddenField runat="server" ID="hfConditionValue_ReportParametersConditions" Value="" />
                                <asp:HiddenField runat="server" ID="hfTrafficConditionValue_ReportParametersConditions" Value="" />
                                <asp:HiddenField runat="server" ID="hfConditionID_ReportParametersConditions" Value="" />
                            </Content>
                            <ClientEvents>
                                <BeforeCallback EventHandler="CallBack_txtConditions_ReportParametersConditions_onBeforeCallback" />
                                <CallbackComplete EventHandler="CallBack_txtConditions_ReportParametersConditions_onCallbackComplete" />
                                <CallbackError EventHandler="CallBack_txtConditions_ReportParametersConditions_onCallbackError" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </td>
                    <td align="center">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%">
                                    <ComponentArt:ToolBar ID="TlbSum_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemSum_TlbSum_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbSum_ReportParametersConditions','tlbItemSum_TlbSum_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="+" Value="+" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>

                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbSubtraction_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemSubtraction_TlbSubtraction_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbSubtraction_ReportParametersConditions','tlbItemSubtraction_TlbSubtraction_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="-" Value="-" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbDivision_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemDivision_TlbDivision_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbDivision_ReportParametersConditions','tlbItemDivision_TlbDivision_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="/" Value="/" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbMultiplication_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemMultiplication_TlbMultiplication_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbMultiplication_ReportParametersConditions','tlbItemMultiplication_TlbMultiplication_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="*" Value="*" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbOpenBracket_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemOpenBracket_TlbOpenBracket_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbOpenBracket_ReportParametersConditions','tlbItemOpenBracket_TlbOpenBracket_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="(" Value="(" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbCloseBracket_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemCloseBracket_TlbOpenBracket_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbCloseBracket_ReportParametersConditions','tlbItemCloseBracket_TlbOpenBracket_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text=")" Value=")" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbLessThan_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbLessThan_TlbLessThan_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbLessThan_ReportParametersConditions','tlbLessThan_TlbLessThan_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="<" Value="<" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbGreaterThan_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbGreaterThan_TlbLessThan_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbGreaterThan_ReportParametersConditions','tlbGreaterThan_TlbLessThan_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text=">" Value=">" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbLessEqualThan_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemLessEqualThan_TlbLessEqualThan_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbLessEqualThan_ReportParametersConditions','tlbItemLessEqualThan_TlbLessEqualThan_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="<=" Value="<=" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbGreaterEqualThan_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemGreaterEqualThan_TlbGreaterEqualThan_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbGreaterEqualThan_ReportParametersConditions','tlbItemGreaterEqualThan_TlbGreaterEqualThan_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text=">=" Value=">=" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbEqual_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemEqual_TlbEqual_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbEqual_ReportParametersConditions','tlbItemEqual_TlbEqual_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="=" Value="=" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbNotEqual_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemNotEqual_TlbNotEqual_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbNotEqual_ReportParametersConditions','tlbItemNotEqual_TlbNotEqual_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="<>" Value="<>" TextImageSpacing="5" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbOr_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemOr_TlbOr_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbOr_ReportParametersConditions','tlbItemOr_TlbOr_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="یا" Value="Or" TextImageSpacing="5" meta:resourcekey="tlbItemOr_TlbOr_ReportParametersConditions" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbAnd_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemAnd_TlbAnd_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInConditions_ReportParametersConditions('TlbAnd_ReportParametersConditions','tlbItemAnd_TlbAnd_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="و" Value="And" TextImageSpacing="5" meta:resourcekey="tlbItemAnd_TlbAnd_ReportParametersConditions" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td colspan="2">
                        <ComponentArt:ToolBar ID="TlbOrderReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                            <Items>

                                <ComponentArt:ToolBarItem ID="tlbItemClear_TlbOrderReportParametersConditions"
                                    runat="server" ClientSideCommand="tlbItemClear_TlbOrderReportParametersConditions_onClick();"
                                    DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="clean.png"
                                    ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemClear_TlbOrderReportParametersConditions"
                                    TextImageSpacing="5" />

                            </Items>
                        </ComponentArt:ToolBar>
                    </td>
                </tr>
                <tr style="display:none;">

                    <td style="width: 95%" valign="top" dir="ltr">
                        <ComponentArt:CallBack ID="CallBack_txtOrders_ReportParametersConditions" runat="server" Height="162" OnCallback="CallBack_txtOrders_ReportParametersConditions_onCallBack">
                            <Content>
                                <textarea id="txtOrders_ReportParametersConditions" meta:resourcekey="txtOrders_ReportParametersConditions" class="TextBoxes" rows="20" runat="server" cols="20" name="S1" style="width: 100%; height: 155px;" disabled="disabled"></textarea>
                                <asp:HiddenField ID="ErrorHiddenField_txtOrders_ReportParametersConditions" runat="server" />
                                <asp:HiddenField runat="server" ID="hfOrderValue_ReportParametersConditions" Value="" />
                                <asp:HiddenField runat="server" ID="hfOrderID_ReportParametersConditions" Value="" />
                            </Content>
                            <ClientEvents>
                                <BeforeCallback EventHandler="CallBack_txtOrders_ReportParametersConditions_onBeforeCallback" />
                                <CallbackComplete EventHandler="CallBack_txtOrders_ReportParametersConditions_onCallbackComplete" />
                                <CallbackError EventHandler="CallBack_txtOrders_ReportParametersConditions_onCallbackError" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </td>
                    <td align="center">

                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%">
                                    <ComponentArt:ToolBar ID="TlbAscending_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemAscending_TlbAscending_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInOrders_ReportParametersConditions('TlbAscending_ReportParametersConditions','tlbItemAscending_TlbAscending_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="ص" Value="(ASC)" TextImageSpacing="5" meta:resourcekey="tlbItemAscending_TlbAscending_ReportParametersConditions" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td>
                                    <ComponentArt:ToolBar ID="TlbDescending_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemDescending_TlbDescending_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInOrders_ReportParametersConditions('TlbDescending_ReportParametersConditions','tlbItemDescending_TlbDescending_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="ن" Value="(DESC)" TextImageSpacing="5" meta:resourcekey="tlbItemDescending_TlbDescending_ReportParametersConditions" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%">
                                    <ComponentArt:ToolBar ID="TlbAndOrder_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false" Width="100%">
                                        <Items>
                                            <ComponentArt:ToolBarItem ID="tlbItemAndOrder_TlbAndOrder_ReportParametersConditions" runat="server" ClientSideCommand="InsertOperatorInOrders_ReportParametersConditions('TlbAndOrder_ReportParametersConditions','tlbItemAndOrder_TlbAndOrder_ReportParametersConditions');" DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageWidth="16px" ItemType="Command" Text="و" Value="," TextImageSpacing="5" meta:resourcekey="tlbItemAndOrder_TlbAnd_ReportParametersConditions" TextAlign="Center" />
                                        </Items>
                                    </ComponentArt:ToolBar>
                                </td>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
              <tr>
                  <td style="width: 95%" valign="top" >
                      <table style="font-size: small; font-family: Arial; width: 100%" class="BoxStyle">
            <tr>
                <td>

                    <table style="width: 100%;">
                    <tr>
                        <td>
                            <ComponentArt:ToolBar ID="TlbGroupColumn_ReportParametersConditions" runat="server" CssClass="toolbar" DefaultItemActiveCssClass="itemActive"
                                DefaultItemCheckedCssClass="itemChecked" DefaultItemCheckedHoverCssClass="itemActive"
                                DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover" DefaultItemImageHeight="16px"
                                DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText" DefaultItemTextImageSpacing="0"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                <Items>
                                     <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGroupColumn_ReportParametersConditions" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemSave_TlbGroupColumn_ReportParametersConditions_onClick();" DropDownImageWidth="16px"
                                        Enabled="true" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px"
                                        ItemType="Command" meta:resourcekey="tlbItemSave_TlbGroupColumn_ReportParametersConditions" TextImageSpacing="5" />
                                     <ComponentArt:ToolBarItem ID="tlbItemDelete_TlbGroupColumn_ReportParametersConditionsn" runat="server" DropDownImageHeight="16px"
                                        ClientSideCommand="tlbItemDelete_TlbGroupColumn_ReportParametersConditions_onClick();" DropDownImageWidth="16px"
                                        ImageHeight="16px" ImageUrl="remove.png" ImageWidth="16px" ItemType="Command"
                                        meta:resourcekey="tlbItemDelete_TlbGroupColumn_ReportParametersConditions" TextImageSpacing="5" />
                                    
                                   
                                </Items>
                            </ComponentArt:ToolBar>
                        </td>
                        <td >
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
                                                <td id="header_GroupColumn_ReportParametersConditions" class="HeaderLabel" style="width: 50%">
                                                    Reports
                                                </td>
                                                <td id="loadingPanel_GridGroupColumn_ReportParametersConditions" class="HeaderLabel" style="width: 45%">
                                                </td>
                                                <td id="Td1" runat="server" style="width: 5%" meta:resourcekey="InverseAlignObj">
                                                     <ComponentArt:ToolBar ID="TlbRefresh_GridGroupColumn_ReportParametersConditions" runat="server" CssClass="toolbar"
                                                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                                        ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" UseFadeEffect="false">
                                                        <Items>
                                                            <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbRefresh_GridGroupColumn_ReportParametersConditions" runat="server"
                                                                ClientSideCommand="Refresh_GridGroupColumn_ReportParametersConditions();" DropDownImageHeight="16px"
                                                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png" ImageWidth="16px"
                                                                ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbRefresh_GridGroupColumn_ReportParametersConditions"
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
                                          <ComponentArt:CallBack runat="server" ID="CallBack_GridGroupColumns_ReportParametersConditions" OnCallback="CallBack_GridGroupColumns_ReportParametersConditions_onCallback">
                                            <Content>
                                                <ComponentArt:DataGrid ID="GridGroupColumns_ReportParametersConditions" runat="server" CssClass="Grid"
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
                                                                <ComponentArt:GridColumn DataField="ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="Column.Name" DefaultSortDirection="Descending"
                                                                    HeadingText="نام ستون" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnColumnName_ReportParametersConditions" TextWrap="true"/>
                                                                <ComponentArt:GridColumn Align="Center" DataField="IsGroupingNewPage" DefaultSortDirection="Descending" ColumnType="CheckBox"
                                                                    HeadingText="نمایش در صفحه جدید" HeadingTextCssClass="HeadingText" meta:resourcekey="clmnGroupNewPage_ReportParametersConditions" TextWrap="true"/>
                                                                
                                                                <ComponentArt:GridColumn DataField="Column.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                               
                                                                <ComponentArt:GridColumn DataField="Report.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                <ComponentArt:GridColumn DataField="Person.ID" Visible="false" DataType="System.Decimal" FormatString="###"/>
                                                                 <ComponentArt:GridColumn DataField="Order" Visible="false" />

                                                                
                                                            </Columns>
                                                        </ComponentArt:GridLevel>
                                                    </Levels>
                                                    <ClientEvents>
                                                        <Load EventHandler="GridGroupColumns_ReportParametersConditions_onLoad" />
                                                    
                                                    </ClientEvents>
                                                </ComponentArt:DataGrid>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_GridGroupColumn_ReportParametersConditions" />
                                            </Content>
                                            <ClientEvents>
                                                <CallbackComplete EventHandler="CallBack_GridGroupColumns_ReportParametersConditions_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_GridGroupColumns_ReportParametersConditions_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                    <td>
                                        <ComponentArt:ToolBar ID="TlbInterAction_ReportParametersConditions" runat="server" CssClass="verticaltoolbar"
                                DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                                ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px" Orientation="Vertical" UseFadeEffect="false">
                                <Items>
                                    <ComponentArt:ToolBarItem ID="tlbItemUp_TlbInterAction_ReportParametersConditions" runat="server"
                                        DropDownImageHeight="16px" DropDownImageWidth="16px" Enabled="true" ImageHeight="16px"
                                        ImageUrl="arrow-up.png" ImageWidth="16px" ItemType="Command" ClientSideCommand="tlbItemUp_TlbInterAction_ReportParametersConditions_onClick();"
                                        meta:resourcekey="tlbItemUp_TlbInterAction_ReportParametersConditionsn" TextImageSpacing="5" />
                                    <ComponentArt:ToolBarItem ID="tlbItemDown_TlbInterAction_ReportParametersConditions" runat="server"
                                        ClientSideCommand="tlbItemDown_TlbInterAction_ReportParametersConditions_onClick();" DropDownImageHeight="16px"
                                        DropDownImageWidth="16px" Enabled="true" ImageHeight="16px" ImageUrl="arrow-down.png"
                                        ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemDown_TlbInterAction_ReportParametersConditions"
                                        TextImageSpacing="5" />
                                </Items>
                            </ComponentArt:ToolBar>

                                    </td>
                                </tr>
                            </table>
                            </td>
                        <td style="width: 35%">
                            <table style="width: 90%;" class="BoxStyle" id="tblGroupColumn_ReportParametersConditions">
                                <tr id="Tr1" runat="server" meta:resourcekey="AlignObj">
                                    <td class="DetailsBoxHeaderStyle" colspan="2">
                                        <div id="header_GroupColumnDetails_ReportParametersConditions" runat="server" meta:resourcekey="AlignObj"
                                            style="color: White; width: 100%; height: 100%" class="BoxContainerHeader">
                                          Group Column Details</div>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" meta:resourcekey="AlignObj">
                                    <td colspan="2">
                                        <asp:Label ID="lblGroupColumnName_ReportParametersConditions" runat="server" meta:resourcekey="lblGroupColumnName_ReportParametersConditions"
                                            Text=": ستون ها" CssClass="WhiteLabel"></asp:Label></td>
                                </tr>
                                <tr id="Tr3" runat="server" meta:resourcekey="AlignObj" >
                                    <td colspan ="2" >
                                        <ComponentArt:CallBack runat="server" ID="CallBack_cmbGroupColumnField_ReportParametersConditions" OnCallback="CallBack_cmbGroupColumnField_ReportParametersConditions_onCallback"
                                            Height="26">
                                            <Content>
                                                <ComponentArt:ComboBox ID="cmbGroupColumnField_ReportParametersConditions" runat="server" AutoComplete="true"
                                                    DataTextField="Name" DataValueField="ID" AutoFilter="true" AutoHighlight="false"
                                                    CssClass="comboBox" DropDownCssClass="comboDropDown" DropDownResizingMode="Corner"
                                                    DropHoverImageUrl="Images/ComboBox/ddn-hover.png" DropImageUrl="Images/ComboBox/ddn.png"
                                                    FocusedCssClass="comboBoxHover" HoverCssClass="comboBoxHover" ItemCssClass="comboItem"
                                                    ItemHoverCssClass="comboItemHover" SelectedItemCssClass="comboItemHover" Style="width: 100%"
                                                    TextBoxCssClass="comboTextBox" Enabled="true" TextBoxEnabled="true">
                                                    <ClientEvents>
                                                        <Expand EventHandler="cmbGroupColumnField_ReportParametersConditions_onExpand" />
                                                        <Collapse EventHandler="cmbGroupColumnField_ReportParametersConditions_onCollapse" />

                                                    </ClientEvents>
                                                </ComponentArt:ComboBox>
                                                <asp:HiddenField runat="server" ID="ErrorHiddenField_cmbGroupColumn_ReportParametersConditions" />
                                            </Content>
                                            <ClientEvents>
                                                <BeforeCallback EventHandler="CallBack_cmbGroupColumnField_ReportParametersConditions_onBeforeCallback" />
                                                <CallbackComplete EventHandler="CallBack_cmbGroupColumnField_ReportParametersConditions_onCallbackComplete" />
                                                <CallbackError EventHandler="CallBack_cmbGroupColumnField_ReportParametersConditions_onCallbackError" />
                                            </ClientEvents>
                                        </ComponentArt:CallBack>
                                    </td>
                                </tr>
                                
                               <tr>
                                    <td style="width: 60%">
                                                                            <asp:Label ID="lblGroupingReportByNewPage_ReportParametersConditions" runat="server" meta:resourcekey="lblGroupingReportByNewPage_ReportParametersConditions"
                                                                                Text="نمایش در صفحه جدید :" CssClass="WhiteLabel"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <input id="chbGroupingByNewPage_ReportParametersConditions" type="checkbox" />
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
                      <td align="center">
                          </td>
              </tr>
            </table>
        </div>
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

        <asp:HiddenField runat="server" ID="hfTitle_DialogInternalReportParametersConditions" meta:resourcekey="hfTitle_DialogInternalReportParametersConditions" />
        <asp:HiddenField runat="server" ID="hfErrorType_ReportParametersConditions" meta:resourcekey="hfErrorType_ReportParametersConditions" />
        <asp:HiddenField runat="server" ID="hfConnectionError_ReportParametersConditions" meta:resourcekey="hfConnectionError_ReportParametersConditions" />
        <asp:HiddenField runat="server" ID="hfheader_GroupColumn_ReportParametersConditions" meta:resourcekey="hfheader_GroupColumn_ReportParametersConditions" />
        <asp:HiddenField runat="server" ID="hfloadingPanel_GridGroupColumn_ReportParametersConditions" meta:resourcekey="hfloadingPanel_GridGroupColumn_ReportParametersConditions" />
        <asp:HiddenField runat="server" ID="hfheader_GroupColumnDetails_ReportParametersConditions" meta:resourcekey="hfheader_GroupColumnDetails_ReportParametersConditions" />
         <asp:HiddenField runat="server" ID="hfcmbAlarm_ReportParametersConditions" meta:resourcekey="hfcmbAlarm_ReportParametersConditions" />
    </form>
</body>
</html>
