<%@ Page Language="C#" AutoEventWireup="true" Inherits="GTS.Clock.Presentaion.WebForms.RuleGenerator" Codebehind="RuleGenerator.aspx.cs" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="CSS/toolbar.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/label.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="CSS/tabStyle.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="CSS/style.css" runat="server" type="text/css" rel="stylesheet" />
    <link href="css/tableStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/inputStyle.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/dialog.css" runat="server" type="text/css" rel="Stylesheet" />
    <link href="css/iframe.css" runat="server" type="text/css" rel="Stylesheet" />
    <%--<link href="css/bootstrap.min.css" runat="server" type="text/css" rel="Stylesheet" />--%>
    <link href="css/bootstrap.css" runat="server" type="text/css" rel="Stylesheet" />
</head>
<body>

    <script type="text/javascript" src="JS/jquery-1.9.1.js"></script>
    <script src="JS/bootstrap.js" type="text/javascript"></script>
    <script src="JS/bootstrap-select.js" type="text/javascript"></script>

    <form id="RuleGeneratorForm" runat="server" meta:resourcekey="RuleGeneratorForm" class="BoxStyle">

        <table style="width: 100%; height: 5%;" class="BoxStyle">
            <tr style="height: 1%">
                <td style="height: 1%" dir="rtl">
                    <ComponentArt:ToolBar ID="TlbPersonnelMainInformation" runat="server" CssClass="toolbar"
                        DefaultItemActiveCssClass="itemActive" DefaultItemCheckedCssClass="itemChecked"
                        DefaultItemCheckedHoverCssClass="itemActive" DefaultItemCssClass="item" DefaultItemHoverCssClass="itemHover"
                        DefaultItemImageHeight="16px" DefaultItemImageWidth="16px" DefaultItemTextImageRelation="ImageBeforeText"
                        DefaultItemTextImageSpacing="0" ImagesBaseUrl="images/ToolBar/" ItemSpacing="1px"
                        UseFadeEffect="false" Width="100px">
                        <Items>
                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbRuleGenerator" runat="server" ClientSideCommand="tlbItemSave_TlbRuleGenerator_onClick()" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save.png" ImageWidth="16px" ItemType="Command"
                                Text="ذخیره" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemSave_TlbGenerateScripts" ClientSideCommand="tlbItemSave_TlbGenerateScripts_onClick()" runat="server" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="save_red.png" ImageWidth="16px"
                                ItemType="Command" Text=" تولید اسکریپت فارسی"
                                TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemHelp_TlbRuleGenerator" ClientSideCommand="tlbItemHelp_TlbRuleGenerator_onClick()" runat="server" DropDownImageHeight="16px"
                                DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="help.gif" ImageWidth="16px"
                                ItemType="Command" Text="راهنما"
                                TextImageSpacing="5" />
                            <%-- <ComponentArt:ToolBarItem ID="tlbItemRulesParameters_TlbPersonnelMainInformation" Text="اضافه کردن دستورات سطح اول"
                                runat="server" DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="regulation.png"
                                ImageWidth="16px" ItemType="Command" TextImageSpacing="5" />
                            <ComponentArt:ToolBarItem ID="tlbItemExtraInformation_TlbPersonnelMainInformation" Text="اضافه کردن دستورات سطح دوم"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="view_detailed.png"
                                ImageWidth="16px" ItemType="Command" TextImageSpacing="5" />--%>

                            <ComponentArt:ToolBarItem ID="tlbItemExit_TlbRuleGenerator" runat="server" ClientSideCommand="tlbItemExit_TlbRuleGenerator_onClick()"
                                DropDownImageHeight="16px" DropDownImageWidth="16px" ImageHeight="16px" ImageUrl="exit.png"
                                ImageWidth="16px" ItemType="Command" TextImageSpacing="5" Text="خروج" />
                        </Items>
                    </ComponentArt:ToolBar>

                </td>
            </tr>

        </table>
        <table align="center">
            <tr>
                <td align="center">
                    <asp:Label runat="server" ID="tlbRuleName_TlbRuleGenerator" CssClass="HeaderLabel"></asp:Label>
                </td>
            </tr>
        </table>

        <div class="container BoxStyle" style="width: 100%; height: 100%">

            <table class="BoxStyle" style="width: 100%">
                <tr>
                    <td style="border: double; width: 20%" class="panel-body">
                        <div class='btn-HistoryLess'>
                            <div class='dropdown pull-right'>
                                <button type="button" class='btn btn-primary dropdown-toggle' data-toggle='dropdown' style="width: 210px" id='WorkDaysFirstCombo_RuleGenerator'>انتخاب کنید<span class='caret'></span></button>
                                <ul class='dropdown-menu scrollable-menu' onclick="Firstcmb_RuleGenerator_OnClick()" id="Firstcmb">
                                </ul>

                            </div>
                        </div>

                    </td>
                    <td style="border: double; width: 25%" class="panel-body">
                        <div class="btn-HistoryLess">
                            <div class='dropdown pull-right'>
                                <button type="button" style="width: 50px; text-align: right" class='btn btn-primary dropdown-toggle' data-toggle='dropdown' id='FirstAndOR_RuleGenerator'>و <span class='caret'></span></button>
                                <ul class='dropdown-menu scrollable-menu' id="Firstandor" onclick='ShowSelectedFillLettersComboInLabel(this)'>
                                </ul>
                            </div>
                        </div>
                        <div class='btn-group'>
                            <div class='dropdown pull-right'>
                                <button type="button" style="width: 210px" class='btn btn-primary dropdown-toggle' data-toggle='dropdown' id='WorkDaysSecondCombo_RuleGenerator'>انتخاب کنید<span class='caret'></span></button>
                                <ul class='dropdown-menu scrollable-menu' id="Secondcmb" onclick="Firstcmb_RuleGenerator_OnClick()">
                                </ul>

                            </div>
                        </div>
                    </td>

                    <td style="border: double; width: 25%" class="panel-body">
                        <div class="btn-HistoryLess">
                            <div class='dropdown pull-right'>
                                <button type="button" style="width: 50px" class='btn btn-primary dropdown-toggle' data-toggle='dropdown' id='SecondAndOr_RuleGenerator'>و <span class='caret'></span></button>
                                <ul class='dropdown-menu scrollable-menu' id="secondandor" onclick='ShowSelectedFillLettersComboInLabel(this)'>
                                </ul>
                            </div>
                        </div>
                        <div class='btn-group'>
                            <div class='dropdown pull-right'>
                                <button type="button" style="width: 210px" class='btn btn-primary dropdown-toggle' data-toggle='dropdown' id='WorkDaysThirdCombo_RuleGenerator'>انتخاب کنید<span class='caret'></span></button>
                                <ul class='dropdown-menu scrollable-menu' id="Thirdcmb" onclick="Firstcmb_RuleGenerator_OnClick()">
                                </ul>

                            </div>
                        </div>
                    </td>
                    <td style="border: double; width: 15%" class="panel-body">
                        <button id="AddRuleParameter_RuleGenerator" type="button" class="ClearItems btn btn-primary" data-toggle="modal" data-target="#DeclareParameterModal">تعریف پارامتر قانون</button>

                    </td>
                    <td style="border: double; width: 15%" class="panel-body">
                        <button id="AddRuleGeneralVariables_RuleGenerator" type="button" class="ClearItems btn btn-primary" data-toggle="modal" data-target="#GeneralVariableAssignmentModal">تعریف متغیر سراسری</button>
                        <div class="modal fade " id="GeneralVariableAssignmentModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content BoxStyle">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h6 class="modal-title" id="GeneralVariableModalLabel">انتساب متغیر سراسری</h6>
                                    </div>
                                    <div class="modal-body">
                                        <button type="button" data-target="#DeclareVariableModal" id="DeclareGeneralVariablebtn" class="ClearItems btn btn-primary" data-toggle="modal">تعریف متغیر</button>
                                        <table class="date-add-General-Variable  table table-striped" id="AddGeneralVariable">
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <input type="button" id="AddGeneralVariablebtn" value="انتساب مقدار به متغیر" style="width: auto" class="btn btn-primary" />

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>
                                        <asp:Label ID="GeneralVarResualtlbl"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <asp:HiddenField ID="hfDaysAndOr" runat="server" />
                    </td>
                </tr>

            </table>
            <table class="BoxStyle" style="width: 100%">
                <%-- <tr>
       </tr>--%>
                <%--<tr class="panel panel-primary BoxStyle">--%>
                <tr>
                    <%--<td class="panel-body" style="width: 90%; border: double">--%>
                    <td class="panel-body" style="border: double; width: 100%">

                        <table>
                            <tr>

                                <td>


                                    <table class="data-Add-Condition table table-striped" id="AddContbl">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <input type="button" id="AddCon" value="اضافه کردن شرط" class="btn btn-primary" />
                                    <button type="button" id="ShowConditionModal" class="ClearItems btn btn-primary" data-toggle="modal"
                                        data-target="#VariableAssignmentModal">
                                        اضافه کردن متغیر شرط                     
                                        
                                    </button>
                                    <%--</div>--%>
                                </td>
                            </tr>
                        </table>
                        <%--</td>--%>

                        <%--<td id="tdconditionVariable" class="panel-body" style="border: double; width: 20%">--%>

                        <%--  <button type="button" id="ShowConditionModal" class="ClearItems btn btn-primary" data-toggle="modal"
                               data-target="#VariableAssignmentModal">
                               اضافه کردن متغیر شرط 
                           </button>--%>

                        <div class="modal fade " id="VariableAssignmentModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content BoxStyle">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="icon-search icon-white"></i><span aria-hidden="true">&times;</span></button>
                                        <h6 class="modal-title" id="myModalLabel">انتساب متغیر شرط</h6>
                                    </div>
                                    <div class="modal-body">
                                        <button type="button" data-target="#DeclareVariableModal" id="DeclareVariablebtn" class="ClearItems btn btn-primary" data-toggle="modal">تعریف متغیر</button>
                                        <table class="date-add-Condition-Variable  table table-striped" id="AddConVariable">
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <input type="button" id="AddConditionVariable" value="انتساب مقدار به متغیر" style="width: auto" class="btn btn-primary" />

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>
                                        <asp:Label ID="VarResualtlbl"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </td>



                </tr>
                <tr>
                    <td style="width: 100%; border: double">

                        <table>
                            <tr>
                                <td class="panel-body">
                                    <table class="data-Add-Order table table-striped" id="AddOrdertbl">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <input type="button" id="AddOrders" value="اضافه کردن دستورات آنگاه" class="btn btn-primary" />
                                    <button type="button" id="ShowFirstOrderModal" class="ClearItems btn btn-primary" data-toggle="modal" data-target="#FirstOrderVariableAssignmentModal">
                                        اضافه کردن متغیر دستورات
                                    </button>
                                </td>

                            </tr>
                        </table>
                        <%--</td>--%>
                        <%--<td style="width: 20%; border: double" class="panel-body">--%>
                        <%--  <button type="button" id="ShowFirstOrderModal" class="ClearItems btn btn-primary" data-toggle="modal" data-target="#FirstOrderVariableAssignmentModal">
                    اضافه کردن متغیر دستورات
                </button>--%>
                        <div class="modal fade " id="FirstOrderVariableAssignmentModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content BoxStyle">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h6 class="modal-title" id="myFModalModalLabel">انتساب متغیر دستورات آنگاه</h6>
                                    </div>
                                    <div class="modal-body">
                                        <button type="button" data-target="#DeclareVariableModal" id="DeclareFirstOrderVariablebtn" class="ClearItems btn btn-primary" data-toggle="modal">تعریف متغیر</button>
                                        <table class="date-add-ThenOrders-Variable table table-striped" id="AddThenOrderVariable" style="width: 100%">
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <input type="button" id="AddThenOrderVariablebtn" value="انتساب مقدار به متغیر" style="width: auto" class="btn btn-primary" />

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>
                                        <asp:Label runat="server" ID="VarResualtFirOrderlbl"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <%--<tr class="panel panel-primary BoxStyle">--%>
                <tr>
                    <%--<td style="border: double" class="panel-body">--%>
                    <td style="border: double; width: 100%">
                        <table>
                            <tr>
                                <td class="panel-body">
                                    <table class="data-Add-OrderElse table table-striped" id="AddElsetbl">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <input type="button" id="AddElseOrder" value="اضافه کردن دستورات درغیراینصورت" class="btn btn-primary" />
                                    <button type="button" id="ShowSecondtOrderModal" class="ClearItems btn btn-primary" data-toggle="modal"
                                        data-target="#SecondOrderVariableAssignmentModal">
                                        اضافه کردن متغیر دستورات 
                                    </button>
                                </td>

                            </tr>
                        </table>
                        <%--</td>--%>
                        <%--<td style="width: 20%; border: double" class="panel-body">--%>
                        <%--   <button type="button" id="ShowSecondtOrderModal" class="ClearItems btn btn-primary" data-toggle="modal"
                    data-target="#SecondOrderVariableAssignmentModal">
                    اضافه کردن متغیر دستورات 
                </button>--%>

                        <div class="modal fade " id="SecondOrderVariableAssignmentModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content BoxStyle">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h6 class="modal-title" id="mySecondOrderModalLabel">انتساب متغیر دستورات درغیراینصورت</h6>
                                    </div>
                                    <div class="modal-body">
                                        <button type="button" data-target="#DeclareVariableModal" id="SecondOrderDeclareVariablebtn" class="ClearItems btn btn-primary" data-toggle="modal">تعریف متغیر</button>
                                        <table class="date-add-ElseOrders-Variable table table-striped" id="AddElseOrderVariable">
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <input type="button" id="AddElseOrderVariablebtn" value="انتساب مقدار به متغیر" style="width: auto" class="btn btn-primary" />

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>
                                        <asp:Label runat="server" ID="VarResualtSecOrderlbl"></asp:Label>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="modal fade " id="DeclareParameterModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="Innermodal-dialog" role="document">
                                <div class="modal-content BoxStyle">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h6 class="modal-title">تعریف پارامتر</h6>
                                    </div>
                                    <div class="modal-body">

                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblParameterName" runat="server" CssClass="WhiteLabel"
                                                        Text="نام پارامتر" meta:resourcekey="lblParameterName_RuleGenerator"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td style="width: 100%">
                                                    <input type="text" class="form-control input-sm" id="txtDeclareParameter" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblParameterType" runat="server" CssClass="WhiteLabel"
                                                        Text="نوع پارامتر" meta:resourcekey="lblParameterType_RuleGenerator"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <div class='btn-HistoryLess'>
                                                        <div class='dropdown pull-right'>
                                                            <button type="button" class='btn btn-primary dropdown-toggle' data-toggle='dropdown' id="ParameterType_RuleGenerator">عددی<span class='caret'></span></button>
                                                            <ul class='dropdown-menu scrollable-menu' onclick="ParameterType_RuleGenerator_OnClick()">
                                                                <li onclick="SetValueOgParametype(this)" value="1"><a href="#">عددی</a></li>
                                                                <li onclick="SetValueOgParametype(this)" value="2"><a href="#">زمانی</a></li>
                                                                <li onclick="SetValueOgParametype(this)" value="3"><a href="#">تاریخ</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>

                                    <div class="modal-footer">
                                        <input type="button" id="RegParameter" value="ثبت" class="btn btn-success" onclick="RegParameter_RuleGenerator_onClick()" />

                                        <label id="Resultlbl" runat="server" style="width: auto"></label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade " id="DeclareVariableModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="Innermodal-dialog" role="document">
                                <div class="modal-content BoxStyle">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h6 class="modal-title">تعریف متغیر</h6>

                                    </div>
                                    <div class="modal-body">
                                        <ul class="nav nav-tabs" style="position: center">
                                            <li class="active"><a data-toggle="tab" href="#1" id="Variable">متغیر  </a></li>
                                            <li><a data-toggle="tab" href="#2" id="Const">ثابت عددی</a></li>
                                        </ul>
                                        <div class="tab-content" style="direction: rtl">
                                            <div id="1" class="tab-pane fade in active">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <p>نام متغیر</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%-- <td>&nbsp &nbsp   &nbsp &nbsp   &nbsp &nbsp 
                                                        </td>--%>
                                                        <td style="width: 100%">
                                                            <input type="text" id="VariableTextID" class="form-control input-sm" />
                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <p>
                                                                جنس متغیر
                                                            </p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--<td>&nbsp &nbsp   &nbsp &nbsp   &nbsp &nbsp 
                                                        </td>--%>
                                                        <td>
                                                            <div class='btn-HistoryLess'>
                                                                <div class='dropdown pull-right'>

                                                                    <button type="button" id="VariableTypeID" class="btn btn-primary dropdown-toggle" data-toggle='dropdown'>عددی <span class='caret'></span></button>
                                                                    <ul class='dropdown-menu' onclick="VariableTypeID_RuleGenerator_OnClick()">
                                                                        <li><a href="#" id="V1" value="1">عددی</a></li>

                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <table id="LocalVariabletableID" style="visibility: hidden">
                                                                <tr>

                                                                    <td>
                                                                        <p>
                                                                            حوزه عمل متغیر
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <%-- <td>&nbsp &nbsp   &nbsp &nbsp   &nbsp &nbsp 
                                                                    </td>--%>
                                                                    <td>

                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <input type="radio" id="PublicRadio" name="VariableType" value="Public" />
                                                                                    عمومی<br />

                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <input type="radio" id="LocalRadio" name="VariableType" value="Private" />
                                                                                    محلی
                                                                            <br />
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
                                                            <input type="button" id="RegVariableID" value="ثبت" class="btn btn-success" onclick="RegVariableID_RuleGenerator_onClick()" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>

                                                    <tr>
                                                        <td>
                                                            <label id="SaveVarMessagelbl" runat="server" style="font-size: small"></label>

                                                        </td>

                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="2" class="tab-pane fade in">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <p>مقدار ثابت عددی</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%--  <td>&nbsp &nbsp   &nbsp &nbsp   &nbsp &nbsp 
                                                        </td>--%>
                                                        <td>
                                                            <input type="text" id="ConstTextID" class="form-control input-sm" />
                                                        </td>


                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <input type="button" id="RegConstID" value="ثبت" class="btn btn-success" onclick="RegConstID_RuleGenerator_onClick()" />

                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label id="SaveConstMessagelbl" runat="server" style="font-size: small; width: 100%"></label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="PersianScriptlblID"></asp:Label>
                        <br />
                        <asp:Label ID="testlbl2" runat="server"></asp:Label>

                    </td>
                </tr>
            </table>
        </div>


        <div class="modal fade " id="SaveRuleModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content BoxStyle">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h6 class="modal-title" id="myVariableModalLabel">
                            <asp:Label runat="server" ID="SaveResultHeaderlbl"></asp:Label></h6>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="SaveResultllbl"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>

                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade " id="RuleWarning" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content BoxStyle">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h6 class="modal-title" id="myWarningModalLabel">
                            <asp:Label runat="server" ID="Warninglbl"></asp:Label></h6>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="ResualtWarninglbl" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن</button>

                    </div>
                </div>
            </div>
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
        <asp:HiddenField runat="server" ID="hfResources_Resources" />
        <asp:HiddenField runat="server" ID="hfDayResource_Resource" />
        <asp:HiddenField runat="server" ID="hfConcept_Concept" />
        <asp:HiddenField runat="server" ID="hfRGSetting_Setting" />
        <asp:HiddenField runat="server" ID="hfpreposition_preposition" />
        <asp:HiddenField runat="server" ID="hfOperationSuccessResult" />
        <asp:HiddenField ID="hfCloseMessage_Rules" runat="server" meta:resourcekey="hfCloseMessage_Rules" />
        <asp:HiddenField ID="hfConnectionError_Concepts" runat="server" meta:resourcekey="hfConnectionError_Concepts" />
        <asp:HiddenField ID="hfDeleteMessage_Rules" runat="server" meta:resourcekey="hfDeleteMessage_Concepts" />
        <asp:HiddenField runat="server" ID="hfErrorType_RuleGenerator" meta:resourcekey="hfErrorType_RuleGenerator" />
        <asp:HiddenField runat="server" ID="hfConnectionError_RuleGenerator" meta:resourcekey="hfConnectionError_RuleGenerator" />
        <asp:HiddenField runat="server" ID="hfOperationError" />
        <asp:HiddenField runat="server" ID="hfReplicatedName" />
    </form>
</body>
</html>
