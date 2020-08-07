<%@ Page Language="C#" AutoEventWireup="true" Inherits="PublicNews" Codebehind="PublicNews.aspx.cs" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/bulletedListStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/rotator.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="Css/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/alert_box.css" runat="server" type="text/css" rel="Stylesheet" />
    <title></title>
</head>
<body>
    <script type="text/javascript" src="JS/jquery.js"></script>

    <form id="PublicNewsForm" runat="server" meta:resourcekey="PublicNewsForm">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
            <Scripts>
                <asp:ScriptReference Path="JS/MicrosoftAjax/MicrosoftAjax.debug.js" />
            </Scripts>
        </asp:ScriptManager>
        <table style="width: 100%;" class="HeaderLabel">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>

                            <td id="Td2" runat="server" style="width:75%;" meta:resourcekey="AlignObj">
                                <ComponentArt:ToolBar ID="TlbPaging_PublicNews" runat="server" CssClass="toolbar"
                                    DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                                    DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                                    DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageOnly"
                                    DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                                    UseFadeEffect="false" Style="direction: ltr">
                                    <Items>
                                        <ComponentArt:ToolBarItem ID="tlbItemRefresh_TlbPaging_PublicNews"
                                            runat="server" ClientSideCommand="tlbItemRefresh_TlbPaging_PublicNews_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="refresh.png"
                                            ImageWidth="16px" ItemType="Command" meta:resourcekey="tlbItemRefresh_TlbPaging_PublicNews"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemFirst_TlbPaging_PublicNews" runat="server"
                                            ClientSideCommand="tlbItemFirst_TlbPaging_PublicNews_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                            ImageUrl="first.png" ItemType="Command" meta:resourcekey="tlbItemFirst_TlbPaging_PublicNews"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemBefore_TlbPaging_PublicNews"
                                            runat="server" ClientSideCommand="tlbItemBefore_TlbPaging_PublicNews_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                            ImageUrl="Before.png" ItemType="Command" meta:resourcekey="tlbItemBefore_TlbPaging_PublicNews"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemNext_TlbPaging_PublicNews" runat="server"
                                            ClientSideCommand="tlbItemNext_TlbPaging_PublicNews_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                            ImageUrl="Next.png" ItemType="Command" meta:resourcekey="tlbItemNext_TlbPaging_PublicNews"
                                            TextImageSpacing="5" />
                                        <ComponentArt:ToolBarItem ID="tlbItemLast_TlbPaging_PublicNews" runat="server"
                                            ClientSideCommand="tlbItemLast_TlbPaging_PublicNews_onClick();"
                                            DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageWidth="16px"
                                            ImageUrl="last.png" ItemType="Command" meta:resourcekey="tlbItemLast_TlbPaging_PublicNews"
                                            TextImageSpacing="5" />
                                    </Items>
                                </ComponentArt:ToolBar>
                            </td>
                            <td id="PageCount_bulletedListPublicNews_PublicNews"  style="width: 25%" class="WhiteLabel"></td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <ComponentArt:CallBack ID="CallBack_bulletedListPublicNews_PublicNews" runat="server" OnCallback="CallBack_bulletedListPublicNews_PublicNews_onCallBack"
                        Height="26">
                        <Content>
                            <asp:BulletedList ID="bulletedListPublicNews_PublicNews" CssClass="bulletedList" DataTextField="Message" DataValueField="ID" runat="server">
                            </asp:BulletedList>
                            <asp:HiddenField runat="server" ID="ErrorHiddenField_PublicNews" />
                            <asp:HiddenField runat="server" ID="hfPublicNewsPageCount_PublicNews" />
                        </Content>
                        <ClientEvents>
                            <BeforeCallback EventHandler="CallBack_bulletedListPublicNews_PublicNews_onBeforeCallback" />
                            <CallbackComplete EventHandler="CallBack_bulletedListPublicNews_PublicNews_onCallbackComplete" />
                            <CallbackError EventHandler="CallBack_bulletedListPublicNews_PublicNews_onCallbackError" />
                        </ClientEvents>
                    </ComponentArt:CallBack>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hfheader_PublicNews" meta:resourcekey="hfheader_PublicNews" />
            <asp:HiddenField runat="server" ID="hfErrorType_PublicNews" meta:resourcekey="hfErrorType_PublicNews" />
           <asp:HiddenField runat="server" ID="hfConnectionError_PublicNews" meta:resourcekey="hfConnectionError_PublicNews" />
         <asp:HiddenField runat="server" ID="hfPublicNewsPageSize_PublicNews" />
        <asp:HiddenField runat="server" ID="hfPageCount_bulletedListPublicNews_PublicNews" meta:resourcekey="hfPageCount_bulletedListPublicNews_PublicNews" />
        
       
    </form>
</body>
</html>
