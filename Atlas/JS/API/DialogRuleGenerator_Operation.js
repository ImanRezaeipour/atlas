/// <reference path="../jquery-1.9.1.js" />

var num = 0;
var ThenOrderCounter = 0;
var ElseOrderCounter = 0;
var ConditionVariableCounter = 0;
var GeneralVariableCounter = 0;
var FirstLVariableCounter = 0;
var SecondLVariableCounter = 0;
var DeclaredVariableCounter = 0;
var LastFirstLevelVariableRow = 0;
var LastSecondLevelVariableRow = 0;
var ISVariableNameUniqe = true;
var ISParameterNameUniqe = true;
var ActiveSelectBoxID = 0;
var SearchWordInDropDown = '';
var JsonObj;
var SelectedItemtext = "";
var OperationalAreaID = 0;
var ActiveModal = null;
var SelectedModalTab = null;
var ResourceValues = null;
var VariableObjectArrey = [];
var ParameterObjectArrey = [];
var AllHiddenFieldValues = new Array();
var ObjRuleGenerator_RuleGenerator = null;
var CurrentPageState_RuleGenerator = 'View';
var FullObject = [];
var CheckedItemObj;
var operations = '';
$(document).ready(function () {
    var DialogRuleGeneratorValue = parent.DialogRuleGenerator.get_value();
    var RuleName = DialogRuleGeneratorValue.RuleName;
    OperationalAreaID = DialogRuleGeneratorValue.OperationalAreaId;
    $("#tlbRuleName_TlbRuleGenerator").text(RuleName);
    CheckedItemObj = JSON.stringify([{ ID: 'cmb_1', Type: 'Dayscmb', Value: '' }, { ID: 'cmb_2', Type: 'Andorcmb', Value: '' }, { ID: 'cmb_3', Type: 'Dayscmb', Value: '' }, { ID: 'cmb_4', Type: 'Andorcmb', Value: '' }, { ID: 'cmb_5', Type: 'Dayscmb', Value: '' }]);
    $("#hfDaysAndOr").val(CheckedItemObj);
    //اضافه کردن متغیر عمومی
    $("#AddGeneralVariablebtn").click(function () {
        GeneralVariableCounter += 1;
        //var json = JSON.stringify([{ ID: GeneralVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'Savebtn_td_1_tr_', Type: 'Button', Value: '' }, { ID: 'Canclebtn_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'td_3_tr_' + GeneralVariableCounter, Type: 'td', Value: '' }, { ID: 'FirstConceptConditionVariable_td_3_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptConditionVariableUL_td_3_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_3_tr_' + GeneralVariableCounter, Type: 'input', Value: '' }, { ID: 'td_4_tr_' + GeneralVariableCounter, Type: 'td', Value: '' }, { ID: 'OprConditionVariable_td_4_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'OprConditionVariableUL_td_4_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }, { ID: 'td_5_tr_' + GeneralVariableCounter, Type: 'td', Value: '' }, { ID: 'SecondConceptConditionVariable_td_5_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondConceptConditionVariableUL_td_5_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_5_tr_' + GeneralVariableCounter, Type: 'input', Value: '' }, { ID: 'ConditionVariableList_td_7_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'ConditionVariableCmbUL_td_7_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }]);
        var json = JSON.stringify([{ ID: GeneralVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'Savebtn_td_1_tr_', Type: 'Button', Value: '' }, { ID: 'Canclebtn_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptGeneralVariable_td_3_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'GeneralVariableUL_td_3_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_3_tr_' + GeneralVariableCounter, Type: 'input', Value: '' }, { ID: 'OprGeneralVariable_td_4_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'OprGeneralVariableUL_td_4_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }, { ID: 'SecondConceptGeneralVariable_td_5_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'GeneralVariableUL_td_5_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_5_tr_' + GeneralVariableCounter, Type: 'input', Value: '' }, { ID: 'GeneralVariableList_td_7_tr_' + GeneralVariableCounter, Type: 'Button', Value: '' }, { ID: 'GeneralVariableCmbUL_td_7_tr_' + GeneralVariableCounter, Type: 'ul', Value: '' }]);

        //var tr = $("<tr class='BoxStyle'><td><button id='Savebtn_td_1_tr_" + GeneralVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(1,this)'>ثبت</button></td><td><button id=Canclebtn_'" + GeneralVariableCounter + "' type='button' class='btn btn-danger' >انصراف</button></td><td id=td_3_tr_" + GeneralVariableCounter + "><div class='btn-HistoryLess'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='FirstConceptConditionVariable_td_3_tr_" + GeneralVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' style='width:100%' class='dropdown-menu scrollable-menu'  role='menu' id='FirstConceptConditionVariableUL_td_3_tr_" + GeneralVariableCounter + "'><li><input id='inputBox_td_3_tr_" + GeneralVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div> </td> <td id=td_4_tr_" + GeneralVariableCounter + "><div class='btn-HistoryLess'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='OprConditionVariable_td_4_tr_" + GeneralVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu' id='OprConditionVariableUL_td_4_tr_" + GeneralVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li><li><a href='#'>=!</a></li></ul></div></td> <td id=td_5_tr_" + GeneralVariableCounter + "><div class='btn-HistoryLess'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='SecondConceptConditionVariable_td_5_tr_" + GeneralVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu scrollable-menu' style='width:100%' id='SecondConceptConditionVariableUL_td_5_tr_" + GeneralVariableCounter + "'><li><input id='inputBox_td_5_tr_" + GeneralVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></ul></div></td> <td>=</td><td><div class='btn-HistoryLess'> <div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='ConditionVariableList_td_7_tr_" + GeneralVariableCounter + "'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInHistoryLessDropDowns()' class='dropdown-menu' id='ConditionVariableCmbUL_td_7_tr_" + GeneralVariableCounter + "'></ul></div></div></td><td><input type='hidden' class='HFClass' id=hf_" + GeneralVariableCounter + " /></td></tr>")
        //var tr = $("<tr class='BoxStyle'><td><button id='Savebtn_td_1_tr_" + GeneralVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(1,this)'>ثبت</button></td><td><button id=Canclebtn_'" + GeneralVariableCounter + "' type='button' class='btn btn-danger' >انصراف</button></td><td id=td_3_tr_" + GeneralVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='FirstConceptConditionVariable_td_3_tr_" + GeneralVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' style='width:100%' class='dropdown-menu scrollable-menu VariablesClass'  role='menu' id='FirstConceptConditionVariableUL_td_3_tr_" + GeneralVariableCounter + "'><li><input id='inputBox_td_3_tr_" + GeneralVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + GeneralVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='OprConditionVariable_td_4_tr_" + GeneralVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu VariablesClass' id='OprConditionVariableUL_td_4_tr_" + GeneralVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li><li><a href='#'>=!</a></li></ul></div></td> <td id=td_5_tr_" + GeneralVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='SecondConceptConditionVariable_td_5_tr_" + GeneralVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu scrollable-menu VariablesClass' style='width:100%' id='SecondConceptConditionVariableUL_td_5_tr_" + GeneralVariableCounter + "'><li><input id='inputBox_td_5_tr_" + GeneralVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></ul></div></td> <td>=</td><td><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='ConditionVariableList_td_7_tr_" + GeneralVariableCounter + "'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu VariablesClass' id='ConditionVariableCmbUL_td_7_tr_" + GeneralVariableCounter + "'></ul></div></td><td></div><input type='hidden' class='HFClass' id=hf1_" + GeneralVariableCounter + " /></td></tr>")
        var tr = $("<tr class='BoxStyle'><td><button id='Canclebtn_" + GeneralVariableCounter + "' type='button' class='btn btn-danger' onclick='Removetr(4,this)'>انصراف</button></td><td><button id='Savebtn_td_1_tr_" + GeneralVariableCounter + "' type='button' class='btn btn-success' onclick='ShowGeneralDeclareVariableInComboBoxes(this)'>ثبت</button></td><td id=td_3_tr_" + GeneralVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='FirstConceptGeneralVariable_td_3_tr_" + GeneralVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(4)' style='width:100%' class='dropdown-menu scrollable-menu VariablesClass'  role='menu' id='GeneralVariableUL_td_3_tr_" + GeneralVariableCounter + "'><li><input id='inputBox_td_3_tr_" + GeneralVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + GeneralVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='OprGeneralVariable_td_4_tr_" + GeneralVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(4)' class='dropdown-menu VariablesClass' id='OprGeneralVariableUL_td_4_tr_" + GeneralVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li><li><a href='#'>=!</a></li></ul></div></td> <td id=td_5_tr_" + GeneralVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='SecondConceptGeneralVariable_td_5_tr_" + GeneralVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(4)' class='dropdown-menu scrollable-menu VariablesClass' style='width:100%' id='GeneralVariableUL_td_5_tr_" + GeneralVariableCounter + "'><li><input id='inputBox_td_5_tr_" + GeneralVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></ul></div></td> <td>=</td><td><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='GeneralVariableList_td_7_tr_" + GeneralVariableCounter + "' style='width:80px'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(4)' class='dropdown-menu VariablesClass' id='GeneralVariableCmbUL_td_7_tr_" + GeneralVariableCounter + "'></ul></div></td><td></div><input type='hidden' class='HFClass' id=hf4_" + GeneralVariableCounter + " /></td></tr>")

        $("#GeneralVarResualtlbl").text("");
        $(".date-add-General-Variable").append(tr);

        document.getElementById('hf4_' + GeneralVariableCounter + '').value = json;
        FillCombo('hf4_' + GeneralVariableCounter, "#GeneralVariableUL_td_3_tr_" + GeneralVariableCounter);
        FillCombo('hf4_' + GeneralVariableCounter, "#GeneralVariableUL_td_5_tr_" + GeneralVariableCounter);
        AddGeneralVariableToNewRow();
        AddConstToNewRow(4);
        ShowDeclareParametersInNewDropDowns(9);
    })
    // اضافه کردن متغیر شرط
    $("#AddConditionVariable").click(function () {
        ConditionVariableCounter += 1;
        //var json = JSON.stringify([{ ID: ConditionVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'Savebtn_td_1_tr_', Type: 'Button', Value: '' }, { ID: 'Canclebtn_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'td_3_tr_' + ConditionVariableCounter, Type: 'td', Value: '' }, { ID: 'FirstConceptConditionVariable_td_3_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptConditionVariableUL_td_3_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_3_tr_' + ConditionVariableCounter, Type: 'input', Value: '' }, { ID: 'td_4_tr_' + ConditionVariableCounter, Type: 'td', Value: '' }, { ID: 'OprConditionVariable_td_4_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'OprConditionVariableUL_td_4_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }, { ID: 'td_5_tr_' + ConditionVariableCounter, Type: 'td', Value: '' }, { ID: 'SecondConceptConditionVariable_td_5_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondConceptConditionVariableUL_td_5_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_5_tr_' + ConditionVariableCounter, Type: 'input', Value: '' }, { ID: 'ConditionVariableList_td_7_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'ConditionVariableCmbUL_td_7_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }]);
        var json = JSON.stringify([{ ID: ConditionVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'Savebtn_td_1_tr_', Type: 'Button', Value: '' }, { ID: 'Canclebtn_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptConditionVariable_td_3_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptConditionVariableUL_td_3_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_3_tr_' + ConditionVariableCounter, Type: 'input', Value: '' }, { ID: 'OprConditionVariable_td_4_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'OprConditionVariableUL_td_4_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }, { ID: 'SecondConceptConditionVariable_td_5_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondConceptConditionVariableUL_td_5_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_5_tr_' + ConditionVariableCounter, Type: 'input', Value: '' }, { ID: 'ConditionVariableList_td_7_tr_' + ConditionVariableCounter, Type: 'Button', Value: '' }, { ID: 'ConditionVariableCmbUL_td_7_tr_' + ConditionVariableCounter, Type: 'ul', Value: '' }]);

        //var tr = $("<tr class='BoxStyle'><td><button id='Savebtn_td_1_tr_" + ConditionVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(1,this)'>ثبت</button></td><td><button id=Canclebtn_'" + ConditionVariableCounter + "' type='button' class='btn btn-danger' >انصراف</button></td><td id=td_3_tr_" + ConditionVariableCounter + "><div class='btn-HistoryLess'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='FirstConceptConditionVariable_td_3_tr_" + ConditionVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' style='width:100%' class='dropdown-menu scrollable-menu'  role='menu' id='FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter + "'><li><input id='inputBox_td_3_tr_" + ConditionVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div> </td> <td id=td_4_tr_" + ConditionVariableCounter + "><div class='btn-HistoryLess'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='OprConditionVariable_td_4_tr_" + ConditionVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu' id='OprConditionVariableUL_td_4_tr_" + ConditionVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li><li><a href='#'>=!</a></li></ul></div></td> <td id=td_5_tr_" + ConditionVariableCounter + "><div class='btn-HistoryLess'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='SecondConceptConditionVariable_td_5_tr_" + ConditionVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu scrollable-menu' style='width:100%' id='SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter + "'><li><input id='inputBox_td_5_tr_" + ConditionVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></ul></div></td> <td>=</td><td><div class='btn-HistoryLess'> <div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='ConditionVariableList_td_7_tr_" + ConditionVariableCounter + "'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInHistoryLessDropDowns()' class='dropdown-menu' id='ConditionVariableCmbUL_td_7_tr_" + ConditionVariableCounter + "'></ul></div></div></td><td><input type='hidden' class='HFClass' id=hf_" + ConditionVariableCounter + " /></td></tr>")
        //var tr = $("<tr class='BoxStyle'><td><button id='Savebtn_td_1_tr_" + ConditionVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(1,this)'>ثبت</button></td><td><button id=Canclebtn_'" + ConditionVariableCounter + "' type='button' class='btn btn-danger' >انصراف</button></td><td id=td_3_tr_" + ConditionVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='FirstConceptConditionVariable_td_3_tr_" + ConditionVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' style='width:100%' class='dropdown-menu scrollable-menu VariablesClass'  role='menu' id='FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter + "'><li><input id='inputBox_td_3_tr_" + ConditionVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + ConditionVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='OprConditionVariable_td_4_tr_" + ConditionVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu VariablesClass' id='OprConditionVariableUL_td_4_tr_" + ConditionVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li><li><a href='#'>=!</a></li></ul></div></td> <td id=td_5_tr_" + ConditionVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='SecondConceptConditionVariable_td_5_tr_" + ConditionVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu scrollable-menu VariablesClass' style='width:100%' id='SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter + "'><li><input id='inputBox_td_5_tr_" + ConditionVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></ul></div></td> <td>=</td><td><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='ConditionVariableList_td_7_tr_" + ConditionVariableCounter + "'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu VariablesClass' id='ConditionVariableCmbUL_td_7_tr_" + ConditionVariableCounter + "'></ul></div></td><td></div><input type='hidden' class='HFClass' id=hf1_" + ConditionVariableCounter + " /></td></tr>")
        var tr = $("<tr class='BoxStyle'><td><button id='Canclebtn_" + ConditionVariableCounter + "' type='button' class='btn btn-danger' onclick='Removetr(5,this)'>انصراف</button></td><td><button id='Savebtn_td_1_tr_" + ConditionVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(1,this)'>ثبت</button></td><td id=td_3_tr_" + ConditionVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='FirstConceptConditionVariable_td_3_tr_" + ConditionVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' style='width:100%' class='dropdown-menu scrollable-menu VariablesClass'  role='menu' id='FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter + "'><li><input id='inputBox_td_3_tr_" + ConditionVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + ConditionVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='OprConditionVariable_td_4_tr_" + ConditionVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu VariablesClass' id='OprConditionVariableUL_td_4_tr_" + ConditionVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li><li><a href='#'>=!</a></li></ul></div></td> <td id=td_5_tr_" + ConditionVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='SecondConceptConditionVariable_td_5_tr_" + ConditionVariableCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu scrollable-menu VariablesClass' style='width:100%' id='SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter + "'><li><input id='inputBox_td_5_tr_" + ConditionVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></ul></div></td> <td>=</td><td><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='ConditionVariableList_td_7_tr_" + ConditionVariableCounter + "'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(1)' class='dropdown-menu VariablesClass' id='ConditionVariableCmbUL_td_7_tr_" + ConditionVariableCounter + "'></ul></div></td><td></div><input type='hidden' class='HFClass' id=hf1_" + ConditionVariableCounter + " /></td></tr>")


        $(".date-add-Condition-Variable").append(tr);

        document.getElementById('hf1_' + ConditionVariableCounter + '').value = json;
        FillCombo('hf1_' + ConditionVariableCounter, "#FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter);
        FillCombo('hf1_' + ConditionVariableCounter, "#SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter);
        //FillOperationCombo("#OprConditionVariableUL_td_4_tr_" + ConditionVariableCounter);
        AddVariablesToNewRow(1);
        AddConstToNewRow(5);
        ShowDeclareParametersInNewDropDowns(9);
    })
    // اضافه کردن متغیر دستورات سطح اول
    $("#AddThenOrderVariablebtn").click(function () {
        FirstLVariableCounter += 1;
        //var json = JSON.stringify([{ ID: FirstLVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'SavebuttonFirstLVLVariable', Type: 'Button', Value: '' }, { ID: 'CanclebuttonFirstLVLVariable', Type: 'Button', Value: '' }, { ID: 'td_3_tr_' + FirstLVariableCounter, Type: 'td', Value: '' }, { ID: 'ConceptbtnFirstLevel_td_3_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptbtnFirstLevelUL_td_3_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxFirstlvlVariable_td_3_tr_' + FirstLVariableCounter, Type: 'input', Value: '' }, { ID: 'AndOrcmbFirstLevel_td_4_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'AndOrcmbFirstLevelUL_td_4_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }, { ID: 'SecondConceptbtnFirstLevel_td_5_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondConceptbtnFirstLevelUL_td_5_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxFirstlvlVariable_td_5_tr_' + FirstLVariableCounter, Type: 'input', Value: '' }, { ID: 'FirstLevelVariable_td_7_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstLevelVariableUL_td_7_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }]);
        var json = JSON.stringify([{ ID: FirstLVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'Savebtn_td_1_tr_', Type: 'Button', Value: '' }, { ID: 'CanclebuttonFirstLVLVariable', Type: 'Button', Value: '' }, { ID: 'ConceptbtnFirstLevel_td_3_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstConceptbtnFirstLevelUL_td_3_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxFirstlvlVariable_td_3_tr_' + FirstLVariableCounter, Type: 'input', Value: '' }, { ID: 'AndOrcmbFirstLevel_td_4_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'AndOrcmbFirstLevelUL_td_4_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }, { ID: 'SecondConceptbtnFirstLevel_td_5_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondConceptbtnFirstLevelUL_td_5_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxFirstlvlVariable_td_5_tr_' + FirstLVariableCounter, Type: 'input', Value: '' }, { ID: 'FirstLevelVariable_td_7_tr_' + FirstLVariableCounter, Type: 'Button', Value: '' }, { ID: 'FirstLevelVariableUL_td_7_tr_' + FirstLVariableCounter, Type: 'ul', Value: '' }]);
        //var tr = $("<tr class='BoxStyle'><td><button id='Savebtn_td_1_tr_" + FirstLVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(2,this)'>ثبت</button></td><td><button id='CanclebuttonFirstLVLVariable' type='button' class='btn btn-danger'>انصراف</button></td><td id=td_3_tr_" + FirstLVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' id='ConceptbtnFirstLevel_td_3_tr_" + FirstLVariableCounter + "' type='button' data-toggle='dropdown'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' style='width:100%' id='FirstConceptbtnFirstLevelUL_td_3_tr_" + FirstLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' ><li><input id='inputBoxFirstlvlVariable_td_3_tr_" + FirstLVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + FirstLVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmbFirstLevel_td_4_tr_" + FirstLVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' class='dropdown-menu VariablesClass' role='menu' id='AndOrcmbFirstLevelUL_td_4_tr_" + FirstLVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li></ul></div></td> <td id=td_5_tr_" + FirstLVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' id='SecondConceptbtnFirstLevel_td_5_tr_" + FirstLVariableCounter + "' type='button' data-toggle='dropdown'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' style='width:100%' id='SecondConceptbtnFirstLevelUL_td_5_tr_" + FirstLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' ><li><input id='inputBoxFirstlvlVariable_td_5_tr_" + FirstLVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></td> <td>=</td><td></div><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' id='FirstLevelVariable_td_7_tr_" + FirstLVariableCounter + "' type='button' data-toggle='dropdown'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' id='FirstLevelVariableUL_td_7_tr_" + FirstLVariableCounter + "' class='dropdown-menu VariablesClass'></ul></div></div> </td><td></div><input type='hidden' class='HFClass' id=hf2_" + FirstLVariableCounter + " /></td></tr>");
        var tr = $("<tr class='BoxStyle'><td><button id='CanclebuttonFirstLVLVariable" + FirstLVariableCounter + "' type='button' class='btn btn-danger' onclick='Removetr(6,this)'>انصراف</button></td><td><button id='Savebtn_td_1_tr_" + FirstLVariableCounter + "' type='button' class='btn btn-success' onclick='ShowDeclareVariableInComboBoxes(2,this)'>ثبت</button></td><td id=td_3_tr_" + FirstLVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' id='ConceptbtnFirstLevel_td_3_tr_" + FirstLVariableCounter + "' type='button' data-toggle='dropdown'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' style='width:100%' id='FirstConceptbtnFirstLevelUL_td_3_tr_" + FirstLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' ><li><input id='inputBoxFirstlvlVariable_td_3_tr_" + FirstLVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + FirstLVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmbFirstLevel_td_4_tr_" + FirstLVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' class='dropdown-menu VariablesClass' role='menu' id='AndOrcmbFirstLevelUL_td_4_tr_" + FirstLVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>=</a></li></ul></div></td> <td id=td_5_tr_" + FirstLVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' id='SecondConceptbtnFirstLevel_td_5_tr_" + FirstLVariableCounter + "' type='button' data-toggle='dropdown'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' style='width:100%' id='SecondConceptbtnFirstLevelUL_td_5_tr_" + FirstLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' ><li><input id='inputBoxFirstlvlVariable_td_5_tr_" + FirstLVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></td></div> <td>=</td><td><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' id='FirstLevelVariable_td_7_tr_" + FirstLVariableCounter + "' type='button' data-toggle='dropdown'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(2)' id='FirstLevelVariableUL_td_7_tr_" + FirstLVariableCounter + "' class='dropdown-menu VariablesClass'></ul></div></div> </td><td></div><input type='hidden' class='HFClass' id=hf2_" + FirstLVariableCounter + " /></td></tr>");
        $(".date-add-ThenOrders-Variable").append(tr);

        document.getElementById('hf2_' + FirstLVariableCounter + '').value = json;
        FillCombo('hf2_' + FirstLVariableCounter, "#FirstConceptbtnFirstLevelUL_td_3_tr_" + FirstLVariableCounter);
        FillCombo('hf2_' + FirstLVariableCounter, "#SecondConceptbtnFirstLevelUL_td_5_tr_" + FirstLVariableCounter);
        //FillOperationCombo("#AndOrcmbFirstLevelUL_td_4_tr_" + FirstLVariableCounter);
        AddVariablesToNewRow(2);
        AddConstToNewRow(6);
        ShowDeclareParametersInNewDropDowns(10);
    })
    // اضافه کردن متغیر دستورات سطح دوم
    $("#AddElseOrderVariablebtn").click(function () {
        SecondLVariableCounter += 1;
        var json = JSON.stringify([{ ID: SecondLVariableCounter, Type: 'RowNumber', Value: '' }, { ID: 'Savebtn_td_1_tr_', Type: 'Button', Value: '' }, { ID: 'CanclebuttonSecondLVLVariable', Type: 'Button', Value: '' }, { ID: 'ConceptbtnSecondLevel_td_3_tr_' + SecondLVariableCounter, Type: 'Button', Value: '' }, { ID: 'ConceptbtnSecondLevelUL_td_3_tr_' + SecondLVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxSecondlvlVariable_td_3_tr_' + SecondLVariableCounter, Type: 'input', Value: '' }, { ID: 'AndOrcmbSecondLevel_td_4_tr_' + SecondLVariableCounter, Type: 'Button', Value: '' }, { ID: 'AndOrcmbSecondLevelUL_td_4_tr_' + SecondLVariableCounter, type: 'ul', Value: '' }, { ID: 'SecondConceptbtnSecondLevel_td_5_tr_' + SecondLVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondConceptbtnSecondLevelUL_td_5_tr_' + SecondLVariableCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxSecondlvlVariable_td_5_tr_' + SecondLVariableCounter, Type: 'input', Value: '' }, { ID: 'SecondLevelVariable_td_7_tr_' + SecondLVariableCounter, Type: 'Button', Value: '' }, { ID: 'SecondLevelVariableUL_td_7_tr_' + SecondLVariableCounter, Type: 'ul', Value: '' }]);
        //var tr = $("<tr class='BoxStyle'><td><button type='button' class='btn btn-danger' onclick='ShowDeclareVariableInComboBoxes(3)'>انصراف</button></td><td><button type='button' class='btn btn-success'>ثبت</button></td><td> <select class='btn btn-success dropdown-toggle' id=SecondLevelVariable_" + SecondLVariableCounter + "><option onclick='ShowDeclareVariableDialog_OnClick(3)' id='DeclareGeneralVariable'>تعریف متغیر  </option><option>________</option></td><td>=</td><td> <select class='btn btn-info dropdown-toggle' id=Firstcnpcmb_" + SecondLVariableCounter + "></select> </td> <td><select class='btn btn-info dropdown-toggle' id=oprcmb_" + SecondLVariableCounter + "><option>+</option><option>*</option></select></td><td> <select class='btn btn-info dropdown-toggle' id=Secondcnpcmb_" + SecondLVariableCounter + "><option>ماموریت</option><option>مرخصی</option></select> </td><td><asp:HiddenField ID=hf_" + SecondLVariableCounter + "/></td></tr>");
        //  var tr = $("<tr class='BoxStyle'><td> <select class='btn btn-info dropdown-toggle' id=Secondcnpcmb_" + SecondLVariableCounter + "></select> </td> <td><select class='btn btn-info dropdown-toggle' id=oprcmb_" + SecondLVariableCounter + "><option>+</option><option>*</option></select></td> <td> <select class='btn btn-info dropdown-toggle' id=Firstcnpcmb_" + SecondLVariableCounter + "></select></td> <td>=</td><td> <select class='btn btn-success dropdown-toggle' id=SecondLevelVariable_" + SecondLVariableCounter + "><option onclick='ShowDeclareVariableDialog_OnClick(3)' id='DeclareGeneralVariable'>تعریف متغیر  </option><option>________</option></td></select> <td><button type='button' class='btn btn-success'  onclick='ShowDeclareVariableInComboBoxes(3)'>ثبت</button></td><td><button type='button' class='btn btn-danger'>انصراف</button></td><td><asp:HiddenField ID=hf_" + SecondLVariableCounter + "/></td></tr>")
        var tr = $("<tr class='BoxStyle'><td><button id='CanclebuttonSecondLVLVariable " + SecondLVariableCounter + "' type='button' class='btn btn-danger' onclick='Removetr(7,this)'>انصراف</button></td><td><button id='Savebtn_td_1_tr_" + SecondLVariableCounter + "' type='button' class='btn btn-success'  onclick='ShowDeclareVariableInComboBoxes(3,this)'>ثبت</button></td><td id=td_3_tr_" + SecondLVariableCounter + "><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' id='ConceptbtnSecondLevel_td_3_tr_" + SecondLVariableCounter + "' type='button' data-toggle='dropdown'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(3)' style='width:100%' id='ConceptbtnSecondLevelUL_td_3_tr_" + SecondLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' role='menu' ><li><input id='inputBoxSecondlvlVariable_td_3_tr_" + SecondLVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div> </td> <td id=td_4_tr_" + SecondLVariableCounter + "></div><div class='btn-group'> <div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmbSecondLevel_td_4_tr_" + SecondLVariableCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(3)' class='dropdown-menu VariablesClass' role='menu' id='AndOrcmbSecondLevelUL_td_4_tr_" + SecondLVariableCounter + "'><li><a href='#'>+</a></li><li><a href='#'>*</a></li><li><a href='#'>/</a></li><li><a href='#'>-</a></li><li><a href='#'>*</a></li></ul></div></td> <td id=td_5_tr_" + SecondLVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' id='SecondConceptbtnSecondLevel_td_5_tr_" + SecondLVariableCounter + "' type='button' data-toggle='dropdown'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(3)' style='width:100%' id='SecondConceptbtnSecondLevelUL_td_5_tr_" + SecondLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' role='menu' ><li><input id='inputBoxSecondlvlVariable_td_5_tr_" + SecondLVariableCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td>=</td><td id=td_7_tr_" + SecondLVariableCounter + "></div><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' id='SecondLevelVariable_td_7_tr_" + SecondLVariableCounter + "' type='button' data-toggle='dropdown'> متغیرها <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDownsInVariablesModal(3)' id='SecondLevelVariableUL_td_7_tr_" + SecondLVariableCounter + "' class='dropdown-menu scrollable-menu VariablesClass' role='menu' ></ul></div> <td></div><input type='hidden' class='HFClass' id=hf3_" + SecondLVariableCounter + " /></td></tr>")

        $(".date-add-ElseOrders-Variable").append(tr);
        document.getElementById('hf3_' + SecondLVariableCounter + '').value = json;
        FillCombo('hf3_' + SecondLVariableCounter, "#ConceptbtnSecondLevelUL_td_3_tr_" + SecondLVariableCounter);
        FillCombo('hf3_' + SecondLVariableCounter, "#SecondConceptbtnSecondLevelUL_td_5_tr_" + SecondLVariableCounter);
        //FillOperationCombo("#AndOrcmbSecondLevelUL_td_4_tr_" + SecondLVariableCounter);
        AddVariablesToNewRow(3);
        AddConstToNewRow(7);
        ShowDeclareParametersInNewDropDowns(11);
    })
    //  اضافه کردن شرط با دکمه اصلی
    $("#AddCon").click(function () {
        num += 1;
        var PrimaryContr;
        //var conditionjson = JSON.stringify([{ ID: num, Type: 'RowNumber', Value: '' }, { ID: 'Conditiontr_' + num, Type: 'tr', Value: '' }, { ID: 'Conditiontable_1_tr_' + num, Type: 'table', Value: '' }, { ID: 'Conditiontr_1_table_' + num, Type: 'tr', Value: '' }, { ID: 'td_1_tr_' + num, Type: 'td', Value: '' }, { ID: 'AndOrcmb_td_1_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConOrAnd_td_1_tr_' + num, Type: 'ul', Value: '' }, { ID: 'td_3_tr_' + num, Type: 'td', Value: '' }, { ID: 'td_4_tr_' + num, Type: 'td', Value: '' }, { ID: 'DropDownAddCondition_td_4_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_4_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_4_tr_' + num, Type: '', Value: '' }, { ID: 'td_5_tr_', Type: 'td', Value: '' }, { ID: 'AndOrcmb_td_5_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConOpr_td_5_tr_' + num, Type: 'ul', Value: '' }, { ID: 'td_6_tr_' + num, Type: 'td', Value: '' }, { ID: 'DropDownAddCondition_td_6_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_6_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_6_tr_' + num, Type: 'input', Value: '' }, { ID: 'td_7_tr_' + num, Type: 'td', Value: '' }, { ID: 'td_8_tr_' + num, Type: 'td', Value: '' }, { ID: 'ButtonAddCondition_td_8_tr_' + num, Type: 'Button', Value: '' }]);
        var conditionjson = JSON.stringify([{ ID: num, Type: 'Condition_1', Value: '' }, { ID: 'AndOrcmb_td_1_tr_' + num, Type: 'Button', Value: 'و' }, { ID: 'ConOrAnd_td_1_tr_' + num, Type: 'ul', Value: '' }, { ID: 'ParenthesisCmb_td_3_tr_' + num, Type: 'Button', Value: '(' }, { ID: 'ParenthesisUL_td_3_tr_' + num, Type: 'ul', Value: '' }, { ID: 'DropDownAddCondition_td_4_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_4_tr_' + num, Type: 'ul', Value: '' }, { ID: 'AndOrcmb_td_5_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConOpr_td_5_tr_' + num, Type: 'ul', Value: '' }, { ID: 'DropDownAddCondition_td_6_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_6_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_6_tr_' + num, Type: 'input', Value: '' }, { ID: 'ParenthesisCmb_td_7_tr_' + num, Type: 'button', Value: ')' }, { ID: 'ParenthesisUL_td_7_tr_' + num, Type: 'ul', Value: '' }, { ID: 'ButtonAddCondition_td_8_tr_' + num, Type: 'Button', Value: '' }, { ID: 'Conditionhf_' + num, Type: 'HiddenField', Value: '' }]);

        if (num === 1) {
            //PrimaryContr = $("<tr id= 'Conditiontr_" + num + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_2_tr_" + num + "'>اگر</td><td id='td_3_tr_" + num + "'> (</td> <td id='td_4_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'> ) </td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td> </tr></table></td></tr>")
            //PrimaryContr = $("<tr id= 'Conditiontr_" + num + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_2_tr_" + num + "'>اگر</td><td id='td_3_tr_" + num + "'><div class='btn-group'><div class='dropdown'><button class='btn btn-info' type='button' value='+' onclick='AddParenthesis(this)' id='Pulsbtn_td_3_tr_" + num + "'> + </button><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='ParenthesisCmb_td_3_tr_" + num + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li></ul></div></div></td> <td id='td_4_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='ParenthesisCmb_t7_7_tr_" + num + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li></ul></div></div></td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td> </tr></table></td></tr>")
            PrimaryContr = $("<tr id= 'Conditiontr_" + num + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_2_tr_" + num + "'>اگر</td><td id='td_3_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='ParenthesisCmb_td_3_tr_" + num + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td> <td id='td_4_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='ParenthesisCmb_td_7_tr_" + num + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td> </tr></table></td></tr>")

        }
        else {
            //PrimaryContr = $("<tr id= 'Conditiontr_" + num + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_1_tr_" + num + "'><div class='btn-group'><div class='dropdown'><button class='btn btn-info' type='button' value='+' onclick='AddParenthesis(this)' id='Pulsbtn_td_3_tr_" + num + "'> + </button><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmb_td_1_tr_" + num + "' > و <span class='caret'></span></button><ul class='dropdown-menu test' id='ConOrAnd_td_1_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td> <td id='td_2_tr_" + num + "'>اگر</td><td id='td_3_tr_" + num + "'> <td id='td_3_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='ParenthesisCmb_td_3_tr_" + num + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li></ul></div></div></td> <td id='td_4_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='ParenthesisCmb_t7_7_tr_" + num + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li></ul></div></div></td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td> </tr></table></td></tr>")
            PrimaryContr = $("<tr id= 'Conditiontr_" + num + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_1_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmb_td_1_tr_" + num + "' > و <span class='caret'></span></button><ul class='dropdown-menu test' id='ConOrAnd_td_1_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td> <td id='td_3_tr_" + num + "'> <td id='td_3_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' style='width:60px' type='button' data-toggle='dropdown' value='(' id='ParenthesisCmb_td_3_tr_" + num + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td> <td id='td_4_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='ParenthesisCmb_td_7_tr_" + num + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td> </tr></table></td></tr>")

        }

        $(".data-Add-Condition").append(PrimaryContr);
        // document.getElementById('Conditionhf_' + num + '').value = conditionjson;
        $("#Conditionhf_" + num).val(conditionjson);
        LocalVariableSetting();
        //FillCombo('Orderhf_' + num, "#DropDownAddCondition_td_4_tr_" + num);
        FillCombo('Conditionhf_' + num, "#ConceptULCondition_td_4_tr_" + num);
        FillCombo('Conditionhf_' + num, "#ConceptULCondition_td_6_tr_" + num);
        FillOperationCombo("#ConOpr_td_5_tr_" + num);
        AddVariablesToNewRow(4);
        AddConstToNewRow(1);
        ShowDeclareParametersInNewDropDowns(1);
    })

    // اضافه کردن شرط با دکمه های ایجاد شده
    $(document).on('click', '.AddbtnClass', function () {
        //var x = event.srcElement.id;
        num += 1;

        //var conditionjson = JSON.stringify([{ ID: num, Type: 'RowNumber', Value: '' }, { ID: 'Conditiontr_' + num, Type: 'tr', Value: '' }, { ID: 'Conditiontable_1_tr_' + num, Type: 'table', Value: '' }, { ID: 'Conditiontr_1_table_' + num, Type: 'tr', Value: '' }, { ID: 'td_1_tr_' + num, Type: 'td', Value: '' }, { ID: 'AndOrcmb_td_1_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConOrAnd_td_1_tr_' + num, Type: 'ul', Value: '' }, { ID: 'td_2_tr_' + num, Type: 'td', Value: '' }, { ID: 'td_3_tr_' + num, Type: 'td', Value: '' }, { ID: 'td_4_tr_' + num, Type: 'td', Value: '' }, { ID: 'DropDownAddCondition_td_4_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_4_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_4_tr_' + num, Type: 'input', Value: '' }, { ID: 'td_5_tr_' + num, Type: 'td', Value: '' }, { ID: 'AndOrcmb_td_5_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConOpr_td_5_tr_' + num, Type: 'ul', Value: '' }, { ID: 'DropDownAddCondition_td_6_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_6_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_6_tr_' + num, Type: 'input', Value: '' }, { ID: 'td_7_tr_' + num, Type: 'td', Value: '' }, { ID: 'ButtonAddCondition_td_8_tr_' + num, Type: 'Button', Value: '' }, { ID: 'td_9_tr_' + num, Type: 'td', Value: '' }]);

        var conditionjson = JSON.stringify([{ ID: num, Type: 'Condition_2', Value: '' }, { ID: 'AndOrcmb_td_1_tr_' + num, Type: 'Button', Value: 'و' }, { ID: 'ConOrAnd_td_1_tr_' + num, Type: 'ul', Value: '' }, { ID: 'ParenthesisCmb_td_3_tr_' + num, Type: 'Button', Value: '(' }, { ID: 'ParenthesisUL_td_3_tr_' + num, Type: '', Value: '' }, { ID: 'DropDownAddCondition_td_4_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_4_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_4_tr_' + num, Type: 'input', Value: '' }, { ID: 'AndOrcmb_td_5_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConOpr_td_5_tr_' + num, Type: 'ul', Value: '' }, { ID: 'DropDownAddCondition_td_6_tr_' + num, Type: 'Button', Value: '' }, { ID: 'ConceptULCondition_td_6_tr_' + num, Type: 'ul', Value: '' }, { ID: 'inputBox_td_6_tr_' + num, Type: 'input', Value: '' }, { ID: 'ParenthesisCmb_td_7_tr_' + num, Type: 'Button', Value: ')' }, { ID: 'ParenthesisUL_td_7_tr_' + num, Type: 'ul', Value: '' }, { ID: 'ButtonAddCondition_td_8_tr_' + num, Type: 'Button', Value: '' }, { ID: 'Conditionhf_' + num, Type: 'HiddenField', Value: '' }]);
        //var OrderFirstContr = $("<tr id= 'Conditiontr_" + num + "'  class='BoxStyle'><td><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td style=" + "width: 15%" + " id='td_1_tr_" + num + "'><input type=button id='ButtonAddCondition_td_1_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'>  </td><td style=" + "width: 5%" + " id='td_2_tr_" + num + "'>(</td><td style=" + "width: 30%" + " id='td_3_tr_" + num + "'> <select class='btn btn-info dropdown-toggle' id='DropDownAddCondition_td_3_tr_" + num + "'><option id='FirstDeclareLocalConditionVariable_" + num + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option> <option> _______ </option></select> </td> <td style=" + "width: 5%" + " id='td_4_tr_" + num + "'><select class='btn btn-info dropdown-toggle' id='Operationcmb_td_4_tr_" + num + "'><option><</option><option>></option><option>=</option><option><=</option><option>>=</option><option><><option></select></td><td style=" + "width: 30%" + " id='td_5_tr_" + num + "' > <select class='btn btn-info dropdown-toggle' id='DropDownAddCondition_td_5_tr_" + num + "'><option id='SecondDeclareLocalConditionVariable_" + num + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_6_tr_" + num + "'>)اگر</td> <td style=" + "width: 5%" + " id='td_7_tr_" + num + "'> <select class='btn btn-info dropdown-toggle' id='AndOrcmb_td_7_tr_" + num + "'><option>و</option><option>یا</option></select></td><td style=" + "width: 5%" + " id='td_8_tr_" + num + "'><asp:HiddenField ID=Orderhf_" + num + "/></td></tr></table></td></tr>");
        //  var OrderFirstContr = $("<tr id= 'Conditiontr_" + num + "'  class='BoxStyle'><td><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td style=" + "width: 5%" + " id='td_1_tr_" + num + "'> <select class='btn btn-info dropdown-toggle' id='AndOrcmb_td_1_tr_" + num + "'><option>و</option><option>یا</option></select></td> <td style=" + "width: 3%" + " id='td_2_tr_" + num + "'> اگر</td><td style=" + "width: 2%" + " id='td_3_tr_" + num + "'> (</td> <td style=" + "width: 30%" + " id='td_4_tr_" + num + "' > <select class='btn btn-info dropdown-toggle' id='DropDownAddCondition_td_4_tr_" + num + "'></select></td> <td style=" + "width: 5%" + " id='td_5_tr_" + num + "'><select class='btn btn-info dropdown-toggle' id='Operationcmb_td_5_tr_" + num + "'><option><</option><option>></option><option>=</option><option><=</option><option>>=</option><option><><option></select></td> <td style=" + "width: 30%" + " id='td_6_tr_" + num + "'> <select class='btn btn-info dropdown-toggle' id='DropDownAddCondition_td_6_tr_" + num + "'></select> </td> <td style=" + "width: 5%" + " id='td_7_tr_" + num + "'>)</td> <td style=" + "width: 15%" + " id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'>  </td><td style=" + "width: 5%" + " id='td_9_tr_" + num + "'><asp:HiddenField ID=Orderhf_" + num + "/></td></tr></table></td></tr>")
        //var OrderFirstContr = $("<tr id= 'Conditiontr_" + num + "'  class='BoxStyle'><td><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_1_tr_" + num + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:40px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_1_tr_" + num + "'>  و  <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu' role='menu' id='ConOrAnd_td_1_tr_" + num + "'><li><a href='#'>و</a></li><li><a href='#'>یا</a></li></ul></div></div></td> <td id='td_2_tr_" + num + "'> اگر</td><td id='td_3_tr_" + num + "'> (</td> <td id='td_4_tr_" + num + "' > <div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' style='width:100%' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' style='width:100%' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'>)</td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'>  </td><td id='td_9_tr_" + num + "'><td style='width: 20%' id='td_9_tr_" + num + "'><input style='width: 100%' type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td></tr></table></td></tr>")
        //var OrderFirstContr = $("<tr id= 'Conditiontr_" + num + "'  class='BoxStyle'><td><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_1_tr_" + num + "'><div class='btn-group'> <div class='dropdown'><button class='btn btn-info' type='button' value='+' onclick='AddParenthesis(this)' id='Pulsbtn_td_3_tr_" + num + "'> + </button><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmb_td_1_tr_" + num + "'>  و  <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOrAnd_td_1_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td> <td id='td_2_tr_" + num + "'> اگر</td><td id='td_3_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='ParenthesisCmb_td_3_tr_" + num + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li></ul></div></div></td> <td id='td_4_tr_" + num + "' > <div class='btn-group'><div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='ParenthesisCmb_t7_7_tr_" + num + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li></ul></div></div></td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'>  </td><td id='td_9_tr_" + num + "'><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td></tr></table></td></tr>")
        var OrderFirstContr = $("<tr id= 'Conditiontr_" + num + "'  class='BoxStyle'><td><table id='Conditiontable_1_tr_" + num + "'><tr id='Conditiontr_1_table_" + num + "'><td id='td_1_tr_" + num + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmb_td_1_tr_" + num + "'>  و  <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOrAnd_td_1_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td> <td id='td_3_tr_" + num + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='ParenthesisCmb_td_3_tr_" + num + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td> <td id='td_4_tr_" + num + "' > <div class='btn-group'><div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULCondition_td_4_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_4_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td> <td id='td_5_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + num + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOpr_td_5_tr_" + num + "'></ul></div></div></td> <td id='td_6_tr_" + num + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + num + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + num + "'><li><input style='width:100%' id='inputBox_td_6_tr_" + num + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='ParenthesisCmb_td_7_tr_" + num + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td> <td id='td_8_tr_" + num + "'><input type=button id='ButtonAddCondition_td_8_tr_" + num + "' value=اضافه class='AddbtnClass btn btn-info'>  </td><td id='td_9_tr_" + num + "'><td id='td_9_tr_" + num + "'><input type=button id='RemoveConbtn_td_9_tr_" + num + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + num + "'><input type='hidden' class='HFClass' id=Conditionhf_" + num + " /></td></tr></table></td></tr>")


        var trid = $(this).parent('td').closest('table').attr('id');

        $("#" + trid + "").after(OrderFirstContr);
        $("#Conditionhf_" + num).val(conditionjson);
        LocalVariableSetting();
        FillCombo('Orderhf_' + num, "#ConceptULCondition_td_4_tr_" + num);
        FillCombo('Orderhf_' + num, "#ConceptULCondition_td_6_tr_" + num);
        FillOperationCombo("#ConOpr_td_5_tr_" + num);
        AddVariablesToNewRow(4);
        AddConstToNewRow(1);
        ShowDeclareParametersInNewDropDowns(1);
    });

    // اضافه کردن دستورات و شرط سطح اول با دکمه ی اصلی
    $("#AddOrders").click(function () {
        var IsValid = CheckValidateInAddiogRulesItems("FirstlvlOrder_1");
        if (IsValid === "true") {
            ThenOrderCounter += 1;

            //var orderjson = JSON.stringify([{ ID: ThenOrderCounter, Type: 'RowNumber', Value: '' }, { ID: 'OrderThentr_' + ThenOrderCounter, Type: 'tr', Value: '' }, { ID: 'OrderThentable_1_tr_' + ThenOrderCounter, Type: 'table', Value: '' }, { ID: 'OrderThentr_1_table_' + ThenOrderCounter, Type: 'tr', Value: '' }, { ID: 'td_1_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'td_2_tr_', Type: 'td', Value: '' }, { ID: 'DropDownAddOrders_td_2_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrders_td_2_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxSecondlvlByPrimarybtn_td_2_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'td_4_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddOrders_td_4_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrders_td_4_tr_' + ThenOrderCounter, Type: '', Value: '' }, { ID: 'inputBoxSecondlvlByPrimarybtn_td_4_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'td_5_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'AddSubConditionbtn_td_5_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_6_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'AddOrdersbtn_td_6_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_7_tr_' + ThenOrderCounter, Type: 'td', Value: '' }]);
            var firstorderjson = JSON.stringify([{ ID: ThenOrderCounter, Type: 'FirstlvlOrder_1', Value: '' }, { ID: 'DropDownAddOrders_td_2_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrders_td_2_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxSecondlvlByPrimarybtn_td_2_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'DropDownAddOrders_td_4_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrders_td_4_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxSecondlvlByPrimarybtn_td_4_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'AddSubConditionbtn_td_5_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'AddOrdersbtn_td_6_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'FirstOrderhf_' + num, Type: 'HiddenField', Value: '' }]);
            //var PrimaryOrdertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td style=" + "width: 15%" + " id='td_1_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_1_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style=" + "width: 14%" + " id='td_2_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_2_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td><td style=" + "width: 30%" + " id='td_3_tr_" + ThenOrderCounter + "'><select  class='btn btn-info dropdown-toggle' id=DropDownAddOrders_td_3_tr_" + ThenOrderCounter + "><option id='FirstDeclareLocalFirstLvlOrderVariable_" + ThenOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_4_tr_" + ThenOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_5_tr_" + ThenOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddOrders_td_5_tr_" + ThenOrderCounter + "><option id='SecondDeclareLocalFirstLvlOrderVariable_" + ThenOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_6_tr_" + ThenOrderCounter + "'>آنگاه</td><td style=" + "width: 1%" + " id='td_7_tr_" + ThenOrderCounter + "'><asp:HiddenField ID=Conhf_" + ThenOrderCounter + "/></td></tr></table></td></tr>")
            //  var PrimaryOrdertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td style=" + "width: 5%" + " id='td_1_tr_" + ThenOrderCounter + "'>آنگاه</td> <td style=" + "width: 30%" + " id='td_2_tr_" + ThenOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddOrders_td_2_tr_" + ThenOrderCounter + "></select></td> <td style=" + "width: 5%" + " id='td_3_tr_" + ThenOrderCounter + "'>=</td> <td style=" + "width: 30%" + " id='td_4_tr_" + ThenOrderCounter + "'><select  class='btn btn-info dropdown-toggle' id=DropDownAddOrders_td_4_tr_" + ThenOrderCounter + "></select></td> <td style=" + "width: 14%" + " id='td_5_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td style=" + "width: 15%" + " id='td_6_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_6_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style=" + "width: 1%" + " id='td_7_tr_" + ThenOrderCounter + "'><asp:HiddenField ID=Conhf_" + ThenOrderCounter + "/></td></tr></table></td></tr>")
            //var PrimaryOrdertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'>آنگاه</td> <td id='td_2_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddOrders_td_2_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' style='width:100%' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrders_td_2_tr_" + ThenOrderCounter + "'><li><input id='inputBoxSecondlvlByPrimarybtn_td_2_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_3_tr_" + ThenOrderCounter + "'>=</td> <td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddOrders_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' style='width:100%' role='menu' id='ConceptULOrders_td_4_tr_" + ThenOrderCounter + "'><li><input id='inputBoxSecondlvlByPrimarybtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_5_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td id='td_6_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_6_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style='width: 20%' id='td_7_tr_" + ThenOrderCounter + "'><input style='width: 100%' type=button id='RemoveFirOrdersbtn_td_7_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")

            var PrimaryOrdertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'>آنگاه</td> <td id='td_2_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddOrders_td_2_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrders_td_2_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxSecondlvlByPrimarybtn_td_2_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td> <td id='td_3_tr_" + ThenOrderCounter + "'>=</td> <td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddOrders_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrders_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxSecondlvlByPrimarybtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td> <td id='td_5_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td id='td_6_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_6_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td id='td_7_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_7_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")
            $(".data-Add-Order").append(PrimaryOrdertr);
            $("#FirstOrderhf_" + ThenOrderCounter).val(firstorderjson);
            LocalVariableSetting();
            FillCombo('FirstOrderhf_' + ThenOrderCounter, "#ConceptULOrders_td_2_tr_" + ThenOrderCounter);
            FillCombo('FirstOrderhf_' + ThenOrderCounter, "#ConceptULOrders_td_4_tr_" + ThenOrderCounter);
            AddVariablesToNewRow(5);
            AddConstToNewRow(2);
            ShowDeclareParametersInNewDropDowns(3);
        }
        else {
            var Message = "ایجاد دستور بدون شرط مجاز نمی باشد";
            $("#ResualtWarninglbl").text(Message);
            $('#RuleWarning').modal('show');
        }
    })
    // اضافه کردن دستورات و زیرشرط سطح اول بادکمه ی دستورات ایجاد شده 
    $(document).on('click', '.AddOrderbtnClass', function () {
        // Ordernum += 1;
        var IsValid = CheckValidateInAddiogRulesItems("FirstlvlOrder_2");
        if (IsValid === "true") {
            ThenOrderCounter += 1;

            //var orderjson = JSON.stringify([{ ID: ThenOrderCounter, Type: 'RowNumber', Value: '' }, { ID: 'OrderThentr_' + ThenOrderCounter, Type: 'tr', Value: '' }, { ID: 'OrderThentable_1_tr_' + ThenOrderCounter, Type: 'table', Value: '' }, { ID: 'OrderThentr_1_table_' + ThenOrderCounter, Type: 'tr', Value: '' }, { ID: 'td_1_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'td_2_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrder_td_2_tr_' + ThenOrderCounter, Type: 'button', Value: '' }, { ID: 'ConceptULOrdersByOrderButton_td_2_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrdersByOrderButton_td_2_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxByOrderbtn_td_2_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'td_3_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'td_4_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrder_td_4_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrdersByOrderButton_td_4_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxByOrderbtn_td_4_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'td_5_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'AddSubConditionbtn_td_5_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_6_tr_' + ThenOrderCounter, Type: 'td', Value: '' }, { ID: 'AddOrdersbtn_td_6_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_7_tr_' + ThenOrderCounter, Type: 'td', Value: '' }]);
            var firstorderjson = JSON.stringify([{ ID: ThenOrderCounter, Type: 'FirstlvlOrder_2', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrder_td_2_tr_' + ThenOrderCounter, Type: 'button', Value: '' }, { ID: 'ConceptULOrdersByOrderButton_td_2_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxByOrderbtn_td_2_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrder_td_4_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrdersByOrderButton_td_4_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxByOrderbtn_td_4_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'AddSubConditionbtn_td_5_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'AddOrdersbtn_td_6_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'FirstOrderhf_' + num, Type: 'HiddenField', Value: '' }]);
            //var Ordertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td style=" + "width: 15%" + " id='td_1_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_1_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style=" + "width: 14%" + " id='td_2_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_2_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td><td style=" + "width: 30%" + " id='td_3_tr_" + ThenOrderCounter + "'><select class='btn btn-info dropdown-toggle' id='DropDownAddTempBySubButtonOrder_td_3_tr_" + ThenOrderCounter + "'><option id='FirstDeclareLocalFirstLvlOrderVariable_" + ThenOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_4_tr_" + ThenOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_5_tr_" + ThenOrderCounter + "'><select class='btn btn-info dropdown-toggle' id='DropDownAddTempBySubButtonOrder_td_5_tr_" + ThenOrderCounter + "'><option id='SecondDeclareLocalFirstLvlOrderVariable_" + ThenOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_6_tr_" + ThenOrderCounter + "'>آنگاه</td><td style=" + "width: 1%" + " id='td_7_tr_" + ThenOrderCounter + "'><asp:HiddenField ID=Conhf_" + ThenOrderCounter + "/></td></tr></table></td></tr>")
            //   var Ordertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td style=" + "width: 5%" + " id='td_1_tr_" + ThenOrderCounter + "'>آنگاه</td><td style=" + "width: 30%" + " id='td_2_tr_" + ThenOrderCounter + "'><select class='btn btn-info dropdown-toggle' id='DropDownAddTempBySubButtonOrder_td_2_tr_" + ThenOrderCounter + "'></select></td> <td style=" + "width: 5%" + " id='td_3_tr_" + ThenOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_4_tr_" + ThenOrderCounter + "'><select class='btn btn-info dropdown-toggle' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "'></select></td><td style=" + "width: 14%" + " id='td_5_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td style=" + "width: 15%" + " id='td_6_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_6_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style=" + "width: 1%" + " id='td_7_tr_" + ThenOrderCounter + "'><asp:HiddenField ID=Conhf_" + ThenOrderCounter + "/></td></tr></table></td></tr>")
            //var Ordertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'>آنگاه</td><td id='td_2_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_2_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' role='menu' style='width:100%' id='ConceptULOrdersByOrderButton_td_2_tr_" + ThenOrderCounter + "'><li><input id='inputBoxByOrderbtn_td_2_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_3_tr_" + ThenOrderCounter + "'>=</td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' style='width:100%' role='menu' id='ConceptULOrdersByOrderButton_td_4_tr_" + ThenOrderCounter + "'><li><input id='inputBoxByOrderbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_5_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td id='td_6_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_6_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style='width: 20%' id='td_7_tr_" + ThenOrderCounter + "'><input style='width: 100%' type=button id='RemoveFirOrdersbtn_td_7_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")

            var Ordertr = $("<tr id='OrderThentr_" + ThenOrderCounter + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "'><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'>آنگاه</td><td id='td_2_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_2_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByOrderButton_td_2_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByOrderbtn_td_2_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td> <td id='td_3_tr_" + ThenOrderCounter + "'>=</td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByOrderButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByOrderbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td id='td_6_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_6_tr_" + ThenOrderCounter + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td id='td_7_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_7_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")

            //var InnerOrdertrid = $(this).closest('tr').attr('id');
            var InnerOrdertrid = $(this).parent('td').closest('table').attr('id');
            $("#" + InnerOrdertrid + "").after(Ordertr);
            $("#FirstOrderhf_" + ThenOrderCounter).val(firstorderjson);
            LocalVariableSetting();
            FillCombo('FirstOrderhf_' + ThenOrderCounter, "#ConceptULOrdersByOrderButton_td_2_tr_" + ThenOrderCounter);
            FillCombo('FirstOrderhf_' + ThenOrderCounter, "#ConceptULOrdersByOrderButton_td_4_tr_" + ThenOrderCounter);
            AddVariablesToNewRow(5);
            AddConstToNewRow(2);
            ShowDeclareParametersInNewDropDowns(4);
        }
        else {
            var Message = "ایجاد دستور بدون شرط مجاز نمی باشد";
            $("#ResualtWarninglbl").text(Message);
            $('#RuleWarning').modal('show');
        }
    })
    //   اضافه کردن دستورات و زیرشرط سطح اول با دکمه ی زیرشرط ایجاد شده
    $(document).on('click', '.AddSubConbtnClass', function () {

        var thisitem = (this).id;
        var IsPreviouseCondition;
        var ID = thisitem.substring(0, 23);
        if (ID === "AddSubConditionbtn_td_8") {
            IsPreviouseCondition = true;
        }
        var IsValid = CheckValidateInAddiogRulesItems("FirstlvlOrder_3");
        if (IsValid === "true") {
            ThenOrderCounter += 1;
            // فیلد حافظه با پرانتز
            var subcon = JSON.stringify([{ ID: ThenOrderCounter, Type: 'FirstlvlOrder_3', Value: '' }, { ID: 'AndOrcmbFirOrder_td_1_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'AndOrcmbFirOrderUL_td_1_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'FirOrderParenthesisCmb_td_3_tr_' + ThenOrderCounter, Type: 'Button', Value: '(' }, { ID: 'FirOrderParenthesisUL_td_3_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrder_td_4_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrdersByConditionButton_td_4_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxByConditionbtn_td_4_tr_', Type: 'input', value: '' }, { ID: 'oprcmbFirstOrder_td_6_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConOprFirstOrder_td_6_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrder_td_6_tr_' + ThenOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULOrdersByConditionButton_td_6_tr_' + ThenOrderCounter, Type: 'ul', Type: '' }, { ID: 'inputBoxByConditionbtn_td_6_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'SecOrderParenthesisCmb_td_7_tr_' + ThenOrderCounter, Type: 'Button', Value: ')' }, { ID: 'SecOrderParenthesisUL_td_7_tr_' + ThenOrderCounter, Type: 'ul', Value: '' }, { ID: 'AddSubConditionbtn_td_8_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'AddOrdersbtn_td_9_tr_' + ThenOrderCounter, Type: 'input', Value: '' }, { ID: 'FirstOrderhf_' + num, Type: 'HiddenField', Value: '' }]);

            // اضافه کردن شرط بدون پرانتز
            //   var Contr = $("<tr id='OrderThentr_" + ThenOrderCounter + "' class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "' ><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_2_tr_" + ThenOrderCounter + "'>اگر</td><td id='td_3_tr_" + ThenOrderCounter + "'>(</td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirstOrder_td_6_tr_" + ThenOrderCounter + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprFirstOrder_td_6_tr_" + ThenOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_6_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_6_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'>)</td><td id='td_8_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_8_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'/></td><td id='td_9_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_9_tr_" + ThenOrderCounter + "' value=" + "دستورات" + " class='AddOrderbtnClass btn btn-info'/></td><td id='td_10_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_10_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")
            if (IsPreviouseCondition === true) {
                var Contr = $("<tr id='OrderThentr_" + ThenOrderCounter + "' class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "' ><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmbFirOrder_td_1_tr_" + ThenOrderCounter + "' > و <span class='caret'></span></button><ul class='dropdown-menu test' id='AndOrcmbFirOrderUL_td_1_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td><td id='td_3_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='FirOrderParenthesisCmb_td_3_tr_" + ThenOrderCounter + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirstOrder_td_6_tr_" + ThenOrderCounter + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprFirstOrder_td_6_tr_" + ThenOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_6_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_6_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='SecOrderParenthesisCmb_td_7_tr_" + ThenOrderCounter + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='SecOrderParenthesisUL_td_7_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_8_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'/></td><td id='td_9_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_9_tr_" + ThenOrderCounter + "' value=" + "دستورات" + " class='AddOrderbtnClass btn btn-info'/></td><td id='td_10_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_10_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")

            }
            else {
                //  اضافه کردن شرط با پرانتز
                var Contr = $("<tr id='OrderThentr_" + ThenOrderCounter + "' class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "' ><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_2_tr_" + ThenOrderCounter + "'>اگر</td><td id='td_3_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='FirOrderParenthesisCmb_td_3_tr_" + ThenOrderCounter + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirstOrder_td_6_tr_" + ThenOrderCounter + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprFirstOrder_td_6_tr_" + ThenOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_6_tr_" + ThenOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_6_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='SecOrderParenthesisCmb_td_7_tr_" + ThenOrderCounter + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='SecOrderParenthesisUL_td_7_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_8_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'/></td><td id='td_9_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_9_tr_" + ThenOrderCounter + "' value=" + "دستورات" + " class='AddOrderbtnClass btn btn-info'/></td><td id='td_10_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_10_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")
            }

            //var InnerContrid = $(this).closest('tr').attr('id');
            var InnerContrid = $(this).parent('td').closest('table').attr('id');
            $("#" + InnerContrid + "").after(Contr);
            $("#FirstOrderhf_" + ThenOrderCounter).val(subcon);
            LocalVariableSetting();
            FillCombo('FirstOrderhf_' + ThenOrderCounter, "#ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter);
            FillCombo('FirstOrderhf_' + ThenOrderCounter, "#ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter);
            FillOperationCombo("#ConOprFirstOrder_td_6_tr_" + ThenOrderCounter);

            AddVariablesToNewRow(5);
            AddConstToNewRow(2);
            ShowDeclareParametersInNewDropDowns(5);
        }
        else {
            var Message = "ایجاد دستور بدون شرط مجاز نمی باشد";
            $("#ResualtWarninglbl").text(Message);
            $('#RuleWarning').modal('show');
        }
    })


    // اضافه کردن دستورات و شرط سطح دوم با دکمه ی اصلی
    $("#AddElseOrder").click(function () {
        var IsValid = CheckValidateInAddiogRulesItems("SecondlvlOrder_1");
        if (IsValid === "true") {
            ElseOrderCounter += 1;

            //var orderjson = JSON.stringify([{ ID: ElseOrderCounter, Type: 'RowNumber', Value: '' }, { ID: 'OrderElsetr_' + ElseOrderCounter, Type: 'tr', Value: '' }, { ID: 'OrderElsetable_1_tr_' + ElseOrderCounter, Type: 'table', Value: '' }, { ID: 'OrderElsetr_1_table_' + ElseOrderCounter, Type: 'tr', Value: '' }, { ID: 'td_2_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddElseOrders_td_2_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevel_td_2_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_2_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'td_3_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'td_4_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddElseOrders_td_3_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevel_td_3_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_3_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'td_5_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'ButtonAddElseCondition_td_5_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_6_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'ButtonAddElseOrder_td_6_tr_' + ElseOrderCounter, Type: '', Value: '' }, { ID: 'td_7_tr_' + ElseOrderCounter, Type: 'td', Value: '' }]);
            var secondorderjson = JSON.stringify([{ ID: ElseOrderCounter, Type: 'SecondlvlOrder_1', Value: '' }, { ID: 'DropDownAddElseOrders_td_2_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevel_td_2_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_2_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'DropDownAddElseOrders_td_3_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevel_td_3_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBox_td_3_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'ButtonAddElseCondition_td_5_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ButtonAddElseOrder_td_6_tr_' + ElseOrderCounter, Type: '', Value: '' }, { ID: 'SecondOrderhf_' + num, Type: 'HiddenField', Value: '' }]);
            // var trElse = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td style=" + "width: 15%" + " id='td_1_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_1_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style=" + "width: 14%" + " id='td_2_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_2_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td style=" + "width: 30%" + " id='td_3_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id='DropDownAddElseOrders_td_3_tr_" + ElseOrderCounter + "'><option id='FisrtDeclareLocalSecondLvlVariable_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_4_tr_" + ElseOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_5_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddElseOrders_td_5_tr_" + ElseOrderCounter + "><option id='SecondDeclareLocalSecondLvlVariable_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 5%" + " id='td_6_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td style=" + "width: 1%" + " id='td_7_tr_" + ElseOrderCounter + "'><asp:HiddenField ID=ConhfElse_" + ElseOrderCounter + "/></td></tr></table></td></tr>")
            //     var trElse = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td style=" + "width: 5%" + " id='td_1_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td style=" + "width: 30%" + " id='td_2_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddElseOrders_td_2_tr_" + ElseOrderCounter + "></select></td><td style=" + "width: 5%" + " id='td_3_tr_" + ElseOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_4_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id='DropDownAddElseOrders_td_4_tr_" + ElseOrderCounter + "'></select></td><td style=" + "width: 14%" + " id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td style=" + "width: 15%" + " id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style=" + "width: 1%" + " id='td_7_tr_" + ElseOrderCounter + "'><asp:HiddenField ID=ConhfElse_" + ElseOrderCounter + "/></td></tr></table></td></tr>")
            //var trElse = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td id='td_2_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_2_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' style='width:100%' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter + "'><li><input id='inputBox_td_2_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'>=</td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:410px' class='btn btn-info dropdown-toggle ' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_3_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' role='menu' style='width:100%' id='ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter + "'><li><input id='inputBox_td_3_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style='width: 20%' id='td_7_tr_" + ElseOrderCounter + "'><input style='width: 100%' type=button id='RemoveSecOrdersbtn_td_7_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")

            //var trElse = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td id='td_2_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_2_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBox_td_2_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'>=</td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:390px' class='btn btn-info dropdown-toggle ' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_3_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBox_td_3_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_7_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_7_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")
            var trElse = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_2_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_2_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBox_td_2_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'>=</td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:390px' class='btn btn-info dropdown-toggle ' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_3_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBox_td_3_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_7_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_7_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")

            $(".data-Add-OrderElse").append(trElse);
            $("#SecondOrderhf_" + ElseOrderCounter).val(secondorderjson);
            LocalVariableSetting();
            FillCombo('SecondOrderhf_' + ElseOrderCounter, "#ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter);
            FillCombo('SecondOrderhf_' + ElseOrderCounter, "#ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter);
            AddVariablesToNewRow(6);
            AddConstToNewRow(3);
            ShowDeclareParametersInNewDropDowns(6);
        }
        else {
            var Message = "اضافه کردن شرط 'درغیراینصورت' بدون شرط 'آنگاه' امکان پذیر نمی باشد";
            $("#ResualtWarninglbl").text(Message);
            $('#RuleWarning').modal('show');
        }
    })
    // اضافه کردن دستورات و زیرشرط سطح دوم با دکمه ی دستورات ایجاد شده
    $(document).on('click', '.AddOrderElsebtnClass', function () {
        var IsValid = CheckValidateInAddiogRulesItems("SecondlvlOrder_2");
        if (IsValid === "true") {
            ElseOrderCounter += 1;

            //var orderjsonElse = JSON.stringify([{ ID: ElseOrderCounter, Type: 'RowNumber', Value: '' }, { ID: 'OrderElsetr_' + ElseOrderCounter, Type: 'tr', Value: '' }, { ID: 'OrderElsetable_1_tr_', Type: 'table', Value: '' }, { ID: 'OrderElsetr_1_table_' + ElseOrderCounter, Type: 'tr', Value: '' }, { ID: 'td_1_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'td_2_tr_', Type: 'td', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrderElse_td_2_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByOrderButton_td_2_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlOrderbtn_td_2_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'td_3_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'td_4_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrderElse_td_4_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByOrderButton_td_4_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlOrderbtn_td_4_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'td_5_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'ButtonAddElseCondition_td_5_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_6_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'ButtonAddElseOrder_td_6_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_7_tr_', Type: 'td', Value: '' }]);
            var orderjsonElse = JSON.stringify([{ ID: ElseOrderCounter, Type: 'SecondlvlOrder_2', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrderElse_td_2_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByOrderButton_td_2_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlOrderbtn_td_2_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'DropDownAddTempBySubButtonOrderElse_td_4_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByOrderButton_td_4_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlOrderbtn_td_4_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'ButtonAddElseCondition_td_5_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ButtonAddElseOrder_td_6_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'SecondOrderhf_' + num, Type: 'HiddenField', Value: '' }]);
            //var ElseOrdertr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td style=" + "width: 15%" + " id='td_1_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_1_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style=" + "width: 14%" + " id='td_2_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_2_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td style=" + "width: 30%" + " id='td_3_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddTempBySubButtonOrderElse_td_3_tr_" + ElseOrderCounter + "><option id='FisrtDeclareLocalSecondLvlVariable_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></selext></td><td style=" + "width: 30%" + " id='td_4_tr_" + ElseOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_5_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddTempBySubButtonOrderElse_td_5_tr_" + ElseOrderCounter + "><option id='SecondDeclareLocalSecondLvlVariable_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</option><option> _______ </option></select></td><td style=" + "width: 30%" + " id='td_6_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td style=" + "width: 1%" + " id='td_7_tr_" + ElseOrderCounter + "'><asp:HiddenField ID=ConhfElse_" + ElseOrderCounter + "/></td></tr></table></td></tr>")
            //  var ElseOrdertr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td style=" + "width: 30%" + " id='td_1_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td style=" + "width: 30%" + " id='td_2_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddTempBySubButtonOrderElse_td_2_tr_" + ElseOrderCounter + "></select></td><td style=" + "width: 30%" + " id='td_3_tr_" + ElseOrderCounter + "'>=</td><td style=" + "width: 30%" + " id='td_4_tr_" + ElseOrderCounter + "'><select class='btn btn-info dropdown-toggle' id=DropDownAddTempBySubButtonOrderElse_td_4_tr_" + ElseOrderCounter + "></select></td><td style=" + "width: 14%" + " id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td style=" + "width: 15%" + " id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style=" + "width: 1%" + " id='td_7_tr_" + ElseOrderCounter + "'><asp:HiddenField ID=ConhfElse_" + ElseOrderCounter + "/></td></tr></table></td></tr>")
            //var ElseOrdertr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td id='td_2_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrderElse_td_2_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' style='width:100%' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter + "'><li><input id='inputBoxBySecondlvlOrderbtn_td_2_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'>=</td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrderElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' style='width:100%' onclick='ShowDropDownContentInDropDowns()' role='menu' id='ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter + "'><li><input id='inputBoxBySecondlvlOrderbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style='width: 20%' id='td_7_tr_" + ElseOrderCounter + "'><input style='width: 100%' type=button id='RemoveSecOrdersbtn_td_7_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_8_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")

            //var ElseOrdertr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ElseOrderCounter + "'>درغیراینصورت</td><td id='td_2_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrderElse_td_2_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlOrderbtn_td_2_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'>=</td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:400px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrderElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlOrderbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_7_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_7_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_8_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")
            var ElseOrdertr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_2_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrderElse_td_2_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlOrderbtn_td_2_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'>=</td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:390px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrderElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlOrderbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + ElseOrderCounter + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_7_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_7_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_8_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")

            //var tridElse = $(this).closest('tr').attr('id');
            var tridElse = $(this).parent('td').closest('table').attr('id');
            $("#" + tridElse + "").after(ElseOrdertr);
            $("#SecondOrderhf_" + ElseOrderCounter).val(orderjsonElse);
            LocalVariableSetting();
            FillCombo('SecondOrderhf_' + ElseOrderCounter, "#ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter);
            FillCombo('SecondOrderhf_' + ElseOrderCounter, "#ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter);
            AddVariablesToNewRow(6);
            AddConstToNewRow(3);
            ShowDeclareParametersInNewDropDowns(7);
        }
        else {
            var Message = "اضافه کردن شرط 'درغیراینصورت' بدون شرط 'آنگاه' امکان پذیر نمی باشد";
            $("#ResualtWarninglbl").text(Message);
            $('#RuleWarning').modal('show');

        }
    })
    // اضافه کردن دستورات و زیرشرط سطح دوم با دکمه ی زیرشرط ایجاد شده
    $(document).on('click', '.AddSubElseConbtnClass', function () {
        var thisitem = (this).id;
        var IsPreviouseCondition;
        var ID = thisitem.substring(0, 27);
        if (ID === "ButtonAddElseCondition_td_8") {
            IsPreviouseCondition = true;
        }
        var IsValid = CheckValidateInAddiogRulesItems("SecondlvlOrder_3");
        if (IsValid === "true") {
            ElseOrderCounter += 1;

            //var subconElse = JSON.stringify([{ ID: ElseOrderCounter, Type: 'RowNumber', Value: '' }, { ID: 'OrderElsetr_' + ElseOrderCounter, Type: 'tr', Value: '' }, { ID: 'OrderElsetable_1_tr_' + ElseOrderCounter, Type: 'table', Value: '' }, { ID: 'OrderElsetr_1_table_' + ElseOrderCounter, Type: 'tr', Value: '' }, { ID: 'td_1_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'AndOrcmbSecOrder_td_1_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'AndOrcmbSecOrderUL_td_1_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'td_2_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'td_3_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'td_4_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddTempBySubButtonConditionElse_td_4_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByConditionButton_td_4_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlConditionbtn_td_4_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'td_5_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'oprcmbFirSecstOrder_td_5_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConOprSecstOrder_td_5_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'td_6_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'DropDownAddTempBySubButtonConditionElse_td_6_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByConditionButton_td_6_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlConditionbtn_td_6_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'td_7_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'td_8_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'ButtonAddElseCondition_td_8_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_9_tr_' + ElseOrderCounter, Type: 'td', Value: '' }, { ID: 'ButtonAddElseOrder_td_9_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'td_10_tr_' + ElseOrderCounter, Type: 'td', Value: '' }]);
            var subconElse = JSON.stringify([{ ID: ElseOrderCounter, Type: 'SecondlvlOrder_3', Value: '' }, { ID: 'AndOrcmbSecOrder_td_1_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'AndOrcmbSecOrderUL_td_1_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'FirOrderParenthesisCmb_td_3_tr_' + ElseOrderCounter, Type: 'Button', Value: '(' }, { ID: 'FirOrderParenthesisUL_td_3_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'DropDownAddTempBySubButtonConditionElse_td_4_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByConditionButton_td_4_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlConditionbtn_td_4_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'oprcmbFirSecstOrder_td_5_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConOprSecstOrder_td_5_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'DropDownAddTempBySubButtonConditionElse_td_6_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ConceptULSecondLevelByConditionButton_td_6_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'inputBoxBySecondlvlConditionbtn_td_6_tr_' + ElseOrderCounter, Type: 'input', Value: '' }, { ID: 'SecOrderParenthesisCmb_td_7_tr_' + ElseOrderCounter, Type: 'Button', Value: ')' }, { ID: 'SecOrderParenthesisUL_td_7_tr_' + ElseOrderCounter, Type: 'ul', Value: '' }, { ID: 'ButtonAddElseCondition_td_8_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'ButtonAddElseOrder_td_9_tr_' + ElseOrderCounter, Type: 'Button', Value: '' }, { ID: 'SecondOrderhf_' + num, Type: 'HiddenField', Value: '' }]);

            //var ElseContr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:40px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmbSecOrder_td_1_tr_" + ElseOrderCounter + "'> و <span class='caret'></span></button><ul class='dropdown-menu' onclick='ShowDropDownContentInDropDowns()' role='menu' id='AndOrcmbSecOrderUL_td_1_tr_" + ElseOrderCounter + "'><li><a href='#'> و </a></li><li><a href='#'> یا </a></li></ul></div></div></td><td id='td_2_tr_" + ElseOrderCounter + "'> اگر</td><td id='td_3_tr_" + ElseOrderCounter + "'>( </td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul style='width:100%' class='dropdown-menu scrollable-menu' onclick='ShowDropDownContentInDropDowns()' role='menu' id='ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter + "'><li><input id='inputBoxBySecondlvlConditionbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirSecstOrder_td_5_tr_" + ElseOrderCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu' role='menu' id='ConOprSecstOrder_td_5_tr_" + ElseOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_6_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul style='width:100%' onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter + "'><li><input id='inputBoxBySecondlvlConditionbtn_td_6_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_7_tr_" + ElseOrderCounter + "'>)</td><td id='td_8_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_8_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_9_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_9_tr_" + ElseOrderCounter + "' value=" + "دستور" + " class='AddOrderElsebtnClass btn btn-info'></td><td style='width: 20%' id='td_10_tr_" + ElseOrderCounter + "'><input style='width: 100%' type=button id='RemoveSecOrdersbtn_td_10_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_11_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")
            //var ElseContr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_2_tr_" + ElseOrderCounter + "'> اگر</td><td id='td_3_tr_" + ElseOrderCounter + "'>( </td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul style='width:100%' class='dropdown-menu scrollable-menu' onclick='ShowDropDownContentInDropDowns()' role='menu' id='ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter + "'><li><input id='inputBoxBySecondlvlConditionbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirSecstOrder_td_5_tr_" + ElseOrderCounter + "'> عملگر <span class='caret'></span></button><ul onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu' role='menu' id='ConOprSecstOrder_td_5_tr_" + ElseOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_6_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul style='width:100%' onclick='ShowDropDownContentInDropDowns()' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter + "'><li><input id='inputBoxBySecondlvlConditionbtn_td_6_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_7_tr_" + ElseOrderCounter + "'>)</td><td id='td_8_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_8_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_9_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_9_tr_" + ElseOrderCounter + "' value=" + "دستور" + " class='AddOrderElsebtnClass btn btn-info'></td><td style='width: 20%' id='td_10_tr_" + ElseOrderCounter + "'><input style='width: 100%' type=button id='RemoveSecOrdersbtn_td_10_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_11_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")
            if (IsPreviouseCondition === true) {
                var ElseContr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropup pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmbSecOrder_td_1_tr_" + ElseOrderCounter + "' > و <span class='caret'></span></button><ul class='dropdown-menu test' id='AndOrcmbSecOrderUL_td_1_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td><td id='td_3_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropup pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='FirOrderParenthesisCmb_td_3_tr_" + ElseOrderCounter + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirSecstOrder_td_5_tr_" + ElseOrderCounter + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprSecstOrder_td_5_tr_" + ElseOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_6_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_6_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_7_tr_" + ElseOrderCounter + "'> <div class='btn-group'><div class='dropup pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='SecOrderParenthesisCmb_td_7_tr_" + ElseOrderCounter + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='SecOrderParenthesisUL_td_7_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_8_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_9_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_9_tr_" + ElseOrderCounter + "' value=" + "دستور" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_10_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_10_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_11_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")
            }
            else {
                var ElseContr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_2_tr_" + ElseOrderCounter + "'> اگر</td><td id='td_3_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropup pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='(' id='FirOrderParenthesisCmb_td_3_tr_" + ElseOrderCounter + "' > ( <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_4_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirSecstOrder_td_5_tr_" + ElseOrderCounter + "'> عملگر <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprSecstOrder_td_5_tr_" + ElseOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_6_tr_" + ElseOrderCounter + "'>انتخاب مفاهیم <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_6_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_7_tr_" + ElseOrderCounter + "'> <div class='btn-group'><div class='dropup pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value=')' id='SecOrderParenthesisCmb_td_7_tr_" + ElseOrderCounter + "' > ) <span class='caret'></span></button><ul class='dropdown-menu test' id='SecOrderParenthesisUL_td_7_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_8_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_9_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_9_tr_" + ElseOrderCounter + "' value=" + "دستور" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_10_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_10_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_11_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + ElseOrderCounter + " /></td></tr></table></td></tr>")
            }


            //var tridConElse = $(this).closest('tr').attr('id');
            var tridConElse = $(this).parent('td').closest('table').attr('id');
            $("#" + tridConElse + "").after(ElseContr);
            $("#SecondOrderhf_" + ElseOrderCounter).val(subconElse);
            LocalVariableSetting();
            $("#FirstcnpcmbElse_" + ElseOrderCounter).append($("<option>متغیر</option>"));
            FillCombo('SecondOrderhf_' + ElseOrderCounter, "#ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter);
            FillCombo('SecondOrderhf_' + ElseOrderCounter, "#ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter);
            FillOperationCombo("#ConOprSecstOrder_td_5_tr_" + ElseOrderCounter);
            AddVariablesToNewRow(6);
            AddConstToNewRow(3);
            ShowDeclareParametersInNewDropDowns(8);
        }
        else {
            var Message = "اضافه کردن شرط 'درغیراینصورت' بدون شرط 'آنگاه' امکان پذیر نمی باشد";
            $("#ResualtWarninglbl").text(Message);
            $('#RuleWarning').modal('show');

        }
    })


    $("#VariableAssignmentModal").on('show.bs.modal', function (e) {
        ActiveModal = $(e.relatedTarget.id);
    });
    $("#FirstOrderVariableAssignmentModal").on('show.bs.modal', function (e) {
        ActiveModal = $(e.relatedTarget.id);
    });
    $("#SecondOrderVariableAssignmentModal").on('show.bs.modal', function (e) {
        ActiveModal = $(e.relatedTarget.id);
    });
    ResourceValues = $("#hfResources_Resources").val();
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        //show selected tab / active
        SelectedModalTab = e.target;
        $("#SaveVarMessagelbl").text("");
        $("#SaveConstMessagelbl").text("");
        $("#VarResualtlbl").text("");
        $("#VarResualtFirOrderlbl").text("");
        $("#VarResualtSecOrderlbl").text("");
        $("#ConstTextID").val('');
        $("#LocalVariabletableID").val('');
    });
    FillDaysCombo();
    FillLettersCombo();
    $(".a").on('click', function (e) {
        if ($(this).attr("checked")) {
            $(this).removeAttr('checked', 'checked');
            //CheckedItemObj

        }
        else {
            $(this).attr('checked', 'checked');
        }

    })
    $(".ClearItems").on('click', function () {

        $("#txtDeclareParameter").val('');
        $("#VariableTextID").val('');
        $("#Resultlbl").text("");
        $("#SaveVarMessagelbl").text("");
        $("#SaveConstMessagelbl").text("");
        $("#ConstTextID").val('');
        $("#LocalVariabletableID").val('');
        $("#VarResualtlbl").text("");
        $("#VarResualtFirOrderlbl").text("");
        $("#VarResualtSecOrderlbl").text("");
    })
    if (DialogRuleGeneratorValue.DesignedRuleID != "0")
        GetRulePropertyInEditMode();
    $(".CheckChecking").change(function () {
        if ($(this).attr("checked")) {
            $(this).removeAttr('checked', 'checked');
            //CheckedItemObj

        }
        else {
            $(this).attr('checked', 'checked');
        }
        FillDaysAndOr();
        var parentButton = $(this).closest('div')[0].childNodes[1].id;
        var thisID = $(this).closest('div')[0].childNodes[3].id;
        var SelectedItemCount = GetValueOFCheckedItemINCombos(thisID).length;
        if ($(this).is(':checked')) {
            SelectedItemtext = $("#" + parentButton).text();

            if (SelectedItemtext === 'انتخاب کنید') {
                SelectedItemtext = "";
            }
            //SelectedItemCount += 1;
            if (SelectedItemCount > 2) {
                if (parentButton === 'WorkDaysFirstCombo_RuleGenerator')
                    //$("#lblFirstWorkDays_RuleGenerator").append(SelectedItemtext + " " + $(this)[0].value);
                    $("#WorkDaysFirstCombo_RuleGenerator").text(SelectedItemCount + "  " + "آیتم انتخاب شدند");

                else if (parentButton === 'WorkDaysSecondCombo_RuleGenerator')
                    //$("#lblThirdWorkDays_RuleGenerator").append(SelectedItemtext + " " + $(this)[0].value);
                    $("#WorkDaysSecondCombo_RuleGenerator").text(SelectedItemCount + "  " + "آیتم انتخاب شدند");

                else if (parentButton === 'WorkDaysThirdCombo_RuleGenerator')
                    //$("#lblFifthWorkDays_RuleGenerator").append(SelectedItemtext + " " + $(this)[0].value);
                    $("#WorkDaysThirdCombo_RuleGenerator").text(SelectedItemCount + "  " + "آیتم انتخاب شدند");
            }
            else {

                if (parentButton === 'WorkDaysFirstCombo_RuleGenerator')
                    //$("#lblFirstWorkDays_RuleGenerator").append(SelectedItemtext + " " + $(this)[0].value);
                    $("#WorkDaysFirstCombo_RuleGenerator").text(SelectedItemtext + " " + $(this)[0].value);

                else if (parentButton === 'WorkDaysSecondCombo_RuleGenerator')
                    //$("#lblThirdWorkDays_RuleGenerator").append(SelectedItemtext + " " + $(this)[0].value);
                    $("#WorkDaysSecondCombo_RuleGenerator").text(SelectedItemtext + " " + $(this)[0].value);

                else if (parentButton === 'WorkDaysThirdCombo_RuleGenerator')
                    //$("#lblFifthWorkDays_RuleGenerator").append(SelectedItemtext + " " + $(this)[0].value);
                    $("#WorkDaysThirdCombo_RuleGenerator").text(SelectedItemtext + " " + $(this)[0].value);

            }
        }

        else {
            //SelectedItemCount -= 1;
            if (SelectedItemCount === 0) {
                if (parentButton === 'WorkDaysFirstCombo_RuleGenerator') {
                    $("#WorkDaysFirstCombo_RuleGenerator").text("انتخاب کنید");
                }
                else if (parentButton === 'WorkDaysSecondCombo_RuleGenerator') {
                    $("#WorkDaysSecondCombo_RuleGenerator").text("انتخاب کنید");
                }
                else if (parentButton === 'WorkDaysThirdCombo_RuleGenerator') {
                    $("#WorkDaysThirdCombo_RuleGenerator").text("انتخاب کنید");
                }
            }
            else {
                if (SelectedItemCount > 2) {
                    ////
                    if (parentButton === 'WorkDaysFirstCombo_RuleGenerator') {
                        $("#WorkDaysFirstCombo_RuleGenerator").text(SelectedItemCount + "  " + "آیتم انتخاب شدند");
                    }
                    else if (parentButton === 'WorkDaysSecondCombo_RuleGenerator') {
                        $("#WorkDaysSecondCombo_RuleGenerator").text(SelectedItemCount + "  " + "آیتم انتخاب شدند");
                    }
                    else if (parentButton === 'WorkDaysThirdCombo_RuleGenerator') {
                        $("#WorkDaysThirdCombo_RuleGenerator").text(SelectedItemCount + "  " + "آیتم انتخاب شدند");
                    }
                }
                else {
                    var SelectedItems;
                    var UnSelectedItem;
                    var FinalList = [];
                    if (parentButton === 'WorkDaysFirstCombo_RuleGenerator') {
                        SelectedItems = GetCheckedItemInFirstCombo();
                        UnSelectedItem = $(this)[0].value;

                        FinalList = [];
                        for (var i = 0; i < SelectedItems.length ; i++) {
                            var StreingSelectedItem = jQuery.trim("" + SelectedItems[i] + "");
                            if (StreingSelectedItem != jQuery.trim(UnSelectedItem)) {
                                FinalList.push(StreingSelectedItem);
                            }
                            $("#WorkDaysFirstCombo_RuleGenerator").text("");
                            $("#WorkDaysFirstCombo_RuleGenerator").text(FinalList);
                        }
                    }
                    else if (parentButton === 'WorkDaysSecondCombo_RuleGenerator') {
                        SelectedItems = GetCheckedItemInSecondCombo();
                        UnSelectedItem = $(this)[0].value;

                        FinalList = [];
                        for (var i = 0; i < SelectedItems.length ; i++) {
                            var StreingSelectedItem = jQuery.trim("" + SelectedItems[i] + "");
                            if (StreingSelectedItem != jQuery.trim(UnSelectedItem)) {
                                FinalList.push(StreingSelectedItem);
                            }

                            $("#WorkDaysSecondCombo_RuleGenerator").text("");
                            $("#WorkDaysSecondCombo_RuleGenerator").text(FinalList);
                        }
                    }
                    else if (parentButton === 'WorkDaysThirdCombo_RuleGenerator') {
                        SelectedItems = GetCheckedItemInThirdCombo();
                        UnSelectedItem = $(this)[0].value;

                        FinalList = [];
                        for (var i = 0; i < SelectedItems.length ; i++) {
                            var StreingSelectedItem = jQuery.trim("" + SelectedItems[i] + "");
                            if (StreingSelectedItem != jQuery.trim(UnSelectedItem)) {
                                FinalList.push(StreingSelectedItem);
                            }

                            $("#WorkDaysThirdCombo_RuleGenerator").text("");
                            $("#WorkDaysThirdCombo_RuleGenerator").text(FinalList);
                        }
                    }
                }
            }
        }
    })
}
    )

//  حذف سطرها
function Removetr(CallerID, thisitem) {

    var SelectedButtonID = $(thisitem).attr('id');
    var RowNumber = SelectedButtonID.substring(SelectedButtonID.lastIndexOf("_") + 1, SelectedButtonID.length);
    switch (CallerID) {
        case 1:
            $("#Conditionhf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            $("#Conditiontr_" + RowNumber).remove();
            num -= 1;
            break;
        case 2:
            $("#FirstOrderhf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            $("#OrderThentr_" + RowNumber).remove();
            ThenOrderCounter -= 1;
            break;
        case 3:
            $("#SecondOrderhf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            $("#OrderElsetr_" + RowNumber).remove();
            ElseOrderCounter -= 1;
            break;
        case 4:
            $("#hf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            GeneralVariableCounter -= 1;
            break;
        case 5:
            $("#hf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            ConditionVariableCounter -= 1;
            break;
        case 6:
            $("#hf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            FirstLVariableCounter -= 1;
            break;
        case 7:
            $("#hf_" + RowNumber).remove();
            $(thisitem).closest('tr').remove();
            SecondLVariableCounter -= 1;
            break;
    }


}
// پر کردن کمبوهای مفاهیم
function FillCombo(hfID, id) {
    var ConceptHFValue = $("#hfConcept_Concept").val();
    var JsonConceptObj = JSON.parse(ConceptHFValue);
    $.each(JsonConceptObj, function (i, Concept) {
        if ((Concept.ConceptType === 1 && OperationalAreaID === "2") || Concept.ConceptType === 0 && OperationalAreaID === "0") {
            //if (Concept.ConceptType === parseInt(OperationalAreaID)) {
            var DataValueJson = JSON.stringify(Concept.DataValueObj);
            $(id).append($("<li Value=" + DataValueJson + " onclick='ShowDropDownContentInDropDowns(this)' ><a href='#'> " + Concept.ConceptName + " </a></li>"));
        }

    })
    $(id).append($("<li onclick='ShowDropDownContentInDropDowns(this)' ><a href='#'> 0  </a></li>"));
    $(id).append($("<li onclick='ShowDropDownContentInDropDowns(this)' ><a href='#'> 1  </a></li>"));
    $(id).append($("<li onclick='ShowDropDownContentInDropDowns(this)' ><a href='#'> 60  </a></li>"));
    $(id).append($("<li onclick='ShowDropDownContentInDropDowns(this)' ><a href='#'>  1440  </a></li>"));
}
//  پر کردن کمبوهای عملگرها
function FillOperationCombo(id) {
    operations = [{ ID: 1, ConceptCode: 1, Value: ">" }, { ID: 2, ConceptCode: 2, Value: "<" }, { ID: 3, ConceptCode: 3, Value: ">=" }, { ID: 4, ConceptCode: 4, Value: "<=" }, { ID: 5, ConceptCode: 5, Value: "==" }, { ID: 6, ConceptCode: 6, Value: "<>" }];
    var operationstringify = JSON.stringify(operations);
    var JsonObj = JSON.parse(operationstringify);

    $.each(JsonObj, function (i, Concept) {
        var OperationJson = JSON.stringify(Concept)
        $(id).append($("<li onclick='ShowDropDownContentInDropDowns(this)' Value='" + OperationJson + "'><a  href='#'> " + Concept.Value + " </a></li>"));
    })
}
// پر کردن کمبوی روزهای هفته
function FillDaysCombo() {
    var hfID = $("#hfDayResource_Resource").val();
    var jsonDayObj = JSON.parse(hfID);
    $.each(jsonDayObj, function (i, item) {
        var DataValueJson = JSON.stringify(item.DataValueObj);
        $("#Firstcmb").append($("<li value=" + i + "><a  href='#' class='small'> <input class='CheckChecking' type='checkbox' value = '" + item + "'/> " + item + " </a></li>"));
        $("#Secondcmb").append($("<li value=" + i + "><a  href='#' class='small'> <input class='CheckChecking' type='checkbox' value = '" + item + "' /> " + item + " </a></li>"));
        $("#Thirdcmb").append($("<li value=" + i + "><a  href='#' class='small'> <input class='CheckChecking' type='checkbox' value = '" + item + "'/> " + item + " </a></li>"));
    })
}
//  بدست آوردن آخرین آیتم لیست پرانتزها و اضافه کردن به لیست پرانتزها
function AddParenthesis(thiss) {
    var SelectedButtonID = $(thiss).attr('id');
    var RowNumber = SelectedButtonID.substring(SelectedButtonID.lastIndexOf("_") + 1, SelectedButtonID.length);
    var ULButtonID = "ParenthesisUL_td_3_tr_" + RowNumber;
    //var LastItem = $("#" + ButtonClass + ":first-child").text();
    var LastItem = $("#" + ULButtonID)[0].childNodes.length;
    //var LastItemIndex = LastItem.substring(3);

    var AdditionalItem = "";
    for (var i = 0; i < LastItem ; i++) {
        var AdditionalItem = AdditionalItem + "(";
    }

    $("#" + ULButtonID).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + AdditionalItem + "(" + "</a></li>"));

}
//    پر کردن کمبوی و یا پیش از
function FillLettersCombo() {
    var hfID = $("#hfpreposition_preposition").val();
    var jsonDayObj = JSON.parse(hfID);
    $.each(jsonDayObj, function (i, item) {
        var DataValueJson = JSON.stringify(item.DataValueObj);
        $("#Firstandor").append($("<li onclick='ShowDropDownContentInAboveDropDowns(this)' value=" + i + "><a href='#'> " + item + " </a></li>"));
        $("#secondandor").append($("<li onclick='ShowDropDownContentInAboveDropDowns(this)' value=" + i + "><a href='#'>" + item + " </a></li>"));
    })
}
//  نمایش آیتم انتخاب شده از کمبوی 'و یا قبل' در لیبل بالای صفحه
function ShowSelectedFillLettersComboInLabel(thiss) {
    var SelectedButton = $(thiss).closest('div')[0].childNodes[1].id;
    if (SelectedButton === 'FirstAndOR_RuleGenerator')
        //$("#lblSecondWorkDays_RuleGenerator").append("   " + $(thiss).closest('div')[0].childNodes[1].textContent);
        $("#FirstAndOR_RuleGenerator").text("   " + $(thiss).closest('div')[0].childNodes[1].textContent);
    else
        $("#SecondAndOr_RuleGenerator").text("   " + $(thiss).closest('div')[0].childNodes[1].textContent);

}
function tlbItemSave_TlbGenerateScripts_onClick() {
    CreatePersianScript();
}
//  بدست اوردن آیتم های انتخاب شده در کمبوهای شرط بالای صفحه
function GetCheckedItemInFirstCombo() {
    //GetValueOFCheckedItemINFirstCombo()
    var FirstCheckedItemArrey = [];

    var ChildCount = $("#Firstcmb")[0].children.length;
    for (var i = 0; i < ChildCount; i++) {
        var inputNode = $("#Firstcmb")[0].children[i].children[0].children[0];
        if (inputNode.hasAttribute("checked")) {
            FirstCheckedItemArrey.push(inputNode.parentNode.innerText);
        }
    }
    return FirstCheckedItemArrey;
}
// بدست آوردن ایندکس آیتم های انتخاب شده در کمبوباکس های بالای صفحه
function GetValueOFCheckedItemINCombos(id) {
    var CheckedItemItemValueArray = [];
    var ChildCount = $("#" + id)[0].children.length;
    for (var i = 0; i < ChildCount; i++) {
        var inputNode = $("#" + id)[0].children[i].children[0].children[0];
        if (inputNode.hasAttribute("checked")) {
            CheckedItemItemValueArray.push(inputNode.parentNode.parentNode.value);
        }
    }
    return CheckedItemItemValueArray;
}

function GetValueOfSelectedItemInAndorCombo(id) {
    var ChildCount = $("#" + id)[0].children.length;
    for (var i = 0; i < ChildCount; i++) {
        var SelectedItemInAndorCombo = $("#" + id)[0].children[i].children[0].children[0];
    }
}
//  پر کردن فیلد حافظه کمبوهای بالای صفحه
function FillDaysAndOr() {
    var SelectedItemInFirstCombo = GetValueOFCheckedItemINCombos("Firstcmb");
    var SelectedItemInFirstAndorCombo = $("#FirstAndOR_RuleGenerator").val();
    var SelectedItemInSecondCombo = GetValueOFCheckedItemINCombos("Secondcmb");
    var SelectedItemInSecondAndorCombo = $("#SecondAndOr_RuleGenerator").val();
    var SelectedItemInThirdCombo = GetValueOFCheckedItemINCombos("Thirdcmb");
    var HFValueCon = $("#hfDaysAndOr").val();
    var JsonObj = JSON.parse(HFValueCon);
    $.each(JsonObj, function (i, key) {
        if (key.ID === "cmb_1") {
            key.Value = SelectedItemInFirstCombo;
            a = JsonObj;
            JsonStringObject = JSON.stringify(a);
        }
        if (key.ID === "cmb_2") {
            var selfirtext = SelectedItemInFirstAndorCombo;
            key.Value = selfirtext;
            a = JsonObj;
            JsonStringObject = JSON.stringify(a);
        }
        else if (key.ID === "cmb_3") {
            key.Value = SelectedItemInSecondCombo;
            a = JsonObj;
            JsonStringObject = JSON.stringify(a);
        }
        if (key.ID === "cmb_4") {
            var selsectext = SelectedItemInSecondAndorCombo;
            key.Value = selsectext;
            a = JsonObj;
            JsonStringObject = JSON.stringify(a);
        }
        else if (key.ID === "cmb_5") {
            key.Value = SelectedItemInThirdCombo;
            a = JsonObj;
            JsonStringObject = JSON.stringify(a);
        }
    })
    $("#hfDaysAndOr").val(JsonStringObject);
    //$("#testlbl2").text(JsonStringObject);
}
// دست اوردن آیتم های انتخاب شده در کمبوهای شرط بالای صفحه
function GetCheckedItemInSecondCombo() {
    var SecondCheckedItemArrey = [];
    var ChildCount = $("#Secondcmb")[0].children.length;
    for (var i = 0; i < ChildCount; i++) {
        var inputNode = $("#Secondcmb")[0].children[i].children[0].children[0];
        if (inputNode.hasAttribute("checked")) {
            SecondCheckedItemArrey.push(inputNode.parentNode.innerText);
        }
    }
    return SecondCheckedItemArrey;
}
//  بدست اوردن آیتم های انتخاب شده در کمبوهای شرط بالای صفحه
function GetCheckedItemInThirdCombo() {
    var ThirdCheckedItemArrey = [];
    var ChildCount = $("#Thirdcmb")[0].children.length;
    for (var i = 0; i < ChildCount; i++) {
        var inputNode = $("#Thirdcmb")[0].children[i].children[0].children[0];
        if (inputNode.hasAttribute("checked")) {
            ThirdCheckedItemArrey.push(inputNode.parentNode.innerText);
        }
    }
    return ThirdCheckedItemArrey;
}
// جستجو در combo
function searchdropdownitem(thisobj) {
    var ConceptHFValue = $("#hfConcept_Concept").val();
    var JsonConceptObj = JSON.parse(ConceptHFValue);
    var Inputid = thisobj.id;
    var ParameterArrey = ParameterObjectArrey;
    var VariableArrey = VariableObjectArrey;
    var ConstList = [0, 1, 60, 1440];
    for (var i = 0; i < VariableArrey.length; i++) {
        JsonConceptObj.push(VariableArrey[i].variablename);
    }
    for (var i = 0; i < ParameterArrey.length; i++) {
        JsonConceptObj.push(ParameterArrey[i].Parametername);
    }
    for (var i = 0; i < ConstList.length; i++) {
        JsonConceptObj.push(ConstList[i]);
    }
    var ulid = $("#" + Inputid).closest("ul").attr("id");
    $("#" + Inputid).keyup(function (event) {
        var SearchWordInDropDown = $("#" + Inputid).val();
        var key = event.char;
        var SearchResultArrey = new Array;
        $("#" + ulid).find('li:gt(0)').remove();
        $.each(JsonConceptObj, function (i, Concept) {
            var ConceptName = Concept.ConceptName;
            if (ConceptName !== undefined) {
                if (ConceptName.indexOf(SearchWordInDropDown) >= 0) {
                    SearchResultArrey.push(ConceptName);
                }
            }
            else {
                ConceptName = JsonConceptObj[i];
                ConceptName = "" + ConceptName + "";
                if (ConceptName.indexOf(SearchWordInDropDown) >= 0) {
                    SearchResultArrey.push(ConceptName);
                }
            }


        })


        for (var i = 0; i < SearchResultArrey.length; i++) {
            $("#" + ulid).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + SearchResultArrey[i] + "</a></li>"));
        }
    })


}

// نمایش ToolTip
function ChagnigliAttribute() {
    $('ul li.Tooltip').hover(function () {
        var SelectedText = jQuery.trim($(this).text());
        var TooltipText = null;
        for (var i = 0; i < VariableObjectArrey.length; i++) {
            var VariableName = VariableObjectArrey[i].variablename;
            var n = VariableName.localeCompare(SelectedText);
            if (VariableName == SelectedText) {
                TooltipText = VariableObjectArrey[i].variablevalue;
                $(this).attr('title', TooltipText);
            }

        }
    })
}
function ShowDropDownContentInDropDownsInVariablesModal(callerID) {
    var selText;
    var ConceptHFValue = $("#hfConcept_Concept").val();
    var JsonPrimaryConceptObj = JSON.parse(ConceptHFValue);
    $(".VariablesClass li a").click(function (e) {
        selText = $(this).text();
        var IsNotConcept = "";
        var a;
        var JsonStringObject;
        $(this).parents('.btn-group').find('.dropdown-toggle').html(selText + '<span class="caret"></span>');
        try {
            selCodeString = $(this)[0].parentNode.attributes[0].value;
        }
        catch (ex) {
            selCodeString = "NotFound";
        }
        if (selCodeString === 'Tooltip') {
            $(".VariablesClass").click(function (e) {

                try {
                    var selCodeObj = JSON.parse(selCodeString);
                    selCode = selCodeObj.ConceptCode;
                }
                catch (ex) {
                    selCode = selText;
                }
                var HasValue = "";
                for (var key in selCodeObj) {
                    if (key === "Value") {
                        HasValue = "True";
                    }
                }

                var SelectedButtonID = $(this).prev('button').attr('id');
                if (HasValue === "True") {
                    for (var i = 0; i < operations.length; i++) {
                        if (operations[i].ConceptCode === selCode) {
                            $("#" + SelectedButtonID).val(selCode);
                        }
                    }
                }
                else {
                    for (var i = 0; i < JsonPrimaryConceptObj.length; i++) {
                        if (JsonPrimaryConceptObj[i].DataValueObj.ConceptCode === selCode) {
                            $("#" + SelectedButtonID).val(selCode);
                            break;
                        }
                    }
                }
                var RowNumber = SelectedButtonID.substring(SelectedButtonID.lastIndexOf("_") + 1, SelectedButtonID.length);
                switch (callerID) {
                    case 1:
                        {
                            for (var i = 1; i <= ConditionVariableCounter; i++) {
                                var HFValueCon = $("#hf1_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf1_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }
                    case 2:
                        {
                            for (var i = 1; i <= FirstLVariableCounter; i++) {
                                var HFValueCon = $("#hf2_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf2_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }

                    case 3:
                        {
                            for (var i = 1; i <= SecondLVariableCounter; i++) {
                                var HFValueCon = $("#hf3_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf3_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }
                    case 4:
                        {
                            for (var i = 1; i <= GeneralVariableCounter; i++) {
                                var HFValueCon = $("#hf4_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf4_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }

                }
            })
        }
        else if (selCodeString === "NotFound") {
            ShowDropDownContentInHistoryLessDropDowns(this);
        }
        else {
            $(".VariablesClass").click(function (e) {

                try {
                    var selCodeObj = JSON.parse(selCodeString);
                    selCode = selCodeObj.ConceptCode;
                }
                catch (ex) {
                    selCode = selText;
                }
                var HasValue = "";
                for (var key in selCodeObj) {
                    if (key === "Value") {
                        HasValue = "True";
                    }
                }

                var SelectedButtonID = $(this).prev('button').attr('id');
                if (HasValue === "True") {
                    for (var i = 0; i < operations.length; i++) {
                        if (operations[i].ConceptCode === selCode) {
                            $("#" + SelectedButtonID).val(selCode);
                        }
                    }
                }
                else {
                    for (var i = 0; i < JsonPrimaryConceptObj.length; i++) {
                        if (JsonPrimaryConceptObj[i].DataValueObj.ConceptCode === selCode) {
                            $("#" + SelectedButtonID).val(selCode);
                            break;
                        }
                    }
                }
                var RowNumber = SelectedButtonID.substring(SelectedButtonID.lastIndexOf("_") + 1, SelectedButtonID.length);
                switch (callerID) {
                    case 1:
                        {
                            for (var i = 1; i <= ConditionVariableCounter; i++) {
                                var HFValueCon = $("#hf1_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf1_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }
                    case 2:
                        {
                            for (var i = 1; i <= FirstLVariableCounter; i++) {
                                var HFValueCon = $("#hf2_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf2_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }

                    case 3:
                        {
                            for (var i = 1; i <= SecondLVariableCounter; i++) {
                                var HFValueCon = $("#hf3_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf3_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }
                    case 4:
                        {
                            for (var i = 1; i <= GeneralVariableCounter; i++) {
                                var HFValueCon = $("#hf4_" + i).val();
                                var JsonConceptObj = JSON.parse(HFValueCon);
                                $.each(JsonConceptObj, function (i, key) {
                                    if (key.ID === SelectedButtonID) {
                                        key.Value = selCode;
                                        a = JsonConceptObj;
                                        JsonStringObject = JSON.stringify(a);
                                    }
                                })

                            }

                            var RowNumberInt = parseInt(RowNumber);

                            $("#hf4_" + RowNumberInt).val(JsonStringObject);
                            break;
                        }

                }
            })

        }
    })
}
// نمایش آیتم انتخاب شده در کمبوباکس ها و پر کردن فیلد حافظه
function ShowDropDownContentInDropDowns(thisliitem) {
    var selText;
    var ConceptHFValue = $("#hfConcept_Concept").val();
    var JsonPrimaryConceptObj = JSON.parse(ConceptHFValue);
    //$(".dropdown-menu li a").on('click', function () {
    //selText = $(this).text();
    selText = $(thisliitem).text();
    var thisaitem = $(thisliitem)[0].childNodes;
    var IsNotConcept = "";
    var a;
    var JsonStringObject;
    $(thisliitem).parents('.btn-group').find('.dropdown-toggle').html(selText + '<span class="caret"></span>');
    try {
        selCodeString = $(thisliitem)[0].attributes['value'].value;

    }
    catch (ex) {
        selCodeString = "NotFound";
    }
    if (selCodeString === 'Tooltip') {
        //selText = $("#txtDeclareParameter").val();
        $(".dropdown-menu").click(function (e) {
            var SelectedButtonID = $(this).prev('button').attr('id');
            $("#" + SelectedButtonID).val(selText);

            var RowNumber = SelectedButtonID.substring(SelectedButtonID.lastIndexOf("_") + 1, SelectedButtonID.length);

            for (var i = 1; i <= num; i++) {
                var HFValueCon = $("#Conditionhf_" + i).val();
                var JsonConceptObj = JSON.parse(HFValueCon);
                $.each(JsonConceptObj, function (i, key) {
                    if (key.ID === SelectedButtonID) {
                        key.Value = selText;
                        a = JsonConceptObj;
                        JsonStringObject = JSON.stringify(a);
                    }
                })

            }

            for (var i = 1; i <= ThenOrderCounter; i++) {
                var HfValueFistorder = $("#FirstOrderhf_" + i).val();
                var JsonObjectFistorder = JSON.parse(HfValueFistorder);
                $.each(JsonObjectFistorder, function (i, key) {
                    if (key.ID === SelectedButtonID) {
                        key.Value = selText;
                        a = JsonObjectFistorder;
                        JsonStringObject = JSON.stringify(a);
                    }
                })
            }
            for (var i = 1; i <= ElseOrderCounter; i++) {
                var HfValueSecondlvlOrder = $("#SecondOrderhf_" + i).val();
                var JsonObjectSecondOrder = JSON.parse(HfValueSecondlvlOrder);
                $.each(JsonObjectSecondOrder, function (i, key) {
                    if (key.ID === SelectedButtonID) {
                        key.Value = selText;
                        a = JsonObjectSecondOrder;
                        JsonStringObject = JSON.stringify(a);
                    }
                })
            }
            var ObjectType = a[0].Type;
            var RowNumberInt = parseInt(RowNumber);
            switch (ObjectType) {
                case "Condition_1":
                case "Condition_2":
                    {
                        $("#Conditionhf_" + RowNumberInt).val(JsonStringObject);
                        break;
                    }
                case "FirstlvlOrder_1":
                case "FirstlvlOrder_2":
                case "FirstlvlOrder_3":
                    {
                        $("#FirstOrderhf_" + RowNumberInt).val(JsonStringObject);
                        break;
                    }
                case "SecondlvlOrder_1":
                case "SecondlvlOrder_2":
                case "SecondlvlOrder_3":
                    {
                        $("#SecondOrderhf_" + RowNumberInt).val(JsonStringObject);
                        break;
                    }


            }
        })
    }
    else {
        $(".dropdown-menu").click(function (e) {

            try {
                if (selCodeString === "NotFound") {

                    //  برای سرچ در کمبوها
                    for (var i = 0; i < JsonPrimaryConceptObj.length; i++) {
                        if (JsonPrimaryConceptObj[i].ConceptName === selText) {
                            selCode = JsonPrimaryConceptObj[i].DataValueObj.ConceptCode;
                            break;
                        }
                        else {
                            selCode = selText;
                        }

                    }
                }
                else {
                    var selCodeObj = JSON.parse(selCodeString);
                    selCode = selCodeObj.ConceptCode;

                }
            }
            catch (ex) {
                selCode = selText;
            }
            var HasValue = "";
            for (var key in selCodeObj) {
                if (key === "Value") {
                    HasValue = "True";
                }
            }
            var SelectedButtonID = $(this).prev('button').attr('id');

            var SelCodeNum = jQuery.trim(selCode);
                SelCodeNum = parseInt(SelCodeNum);

            if (typeof selCode === "string" && $.isNumeric(SelCodeNum))
            {
                $("#" + SelectedButtonID).val("");
            }
           
      
            if (HasValue === "True") {
                for (var i = 0; i < operations.length; i++) {
                    if (operations[i].ConceptCode === selCode) {
                        $("#" + SelectedButtonID).val(selCode);
                    }
                }
            }
            else {
                for (var i = 0; i < JsonPrimaryConceptObj.length; i++) {
                    if (JsonPrimaryConceptObj[i].DataValueObj.ConceptCode === selCode) {
                        $("#" + SelectedButtonID).val(selCode);
                        break;
                    }
                }
            }
            
            var RowNumber = SelectedButtonID.substring(SelectedButtonID.lastIndexOf("_") + 1, SelectedButtonID.length);

            for (var i = 1; i <= num; i++) {
                var HFValueCon = $("#Conditionhf_" + i).val();
                if (HFValueCon != undefined) {
                    var JsonConceptObj = JSON.parse(HFValueCon);
                    $.each(JsonConceptObj, function (i, key) {
                        if (key.ID === SelectedButtonID) {
                            key.Value = selCode;
                            a = JsonConceptObj;
                            JsonStringObject = JSON.stringify(a);
                        }
                    })
                }
            }

            for (var i = 1; i <= ThenOrderCounter; i++) {
                var HfValueFistorder = $("#FirstOrderhf_" + i).val();
                var JsonObjectFistorder = JSON.parse(HfValueFistorder);
                $.each(JsonObjectFistorder, function (i, key) {
                    if (key.ID === SelectedButtonID) {
                        key.Value = selCode;
                        a = JsonObjectFistorder;
                        JsonStringObject = JSON.stringify(a);
                    }
                })
            }
            for (var i = 1; i <= ElseOrderCounter; i++) {
                var HfValueSecondlvlOrder = $("#SecondOrderhf_" + i).val();
                var JsonObjectSecondOrder = JSON.parse(HfValueSecondlvlOrder);
                $.each(JsonObjectSecondOrder, function (i, key) {
                    if (key.ID === SelectedButtonID) {
                        key.Value = selCode;
                        a = JsonObjectSecondOrder;
                        JsonStringObject = JSON.stringify(a);
                    }
                })
            }
            try {
                var ObjectType = a[0].Type;
            }
            catch (ex) {

            }
            RowNumberInt = parseInt(RowNumber);
            switch (ObjectType) {
                case "Condition_1":
                case "Condition_2":
                    {
                        $("#Conditionhf_" + RowNumberInt).val(JsonStringObject);
                        break;
                    }
                case "FirstlvlOrder_1":
                case "FirstlvlOrder_2":
                case "FirstlvlOrder_3":
                    {
                        $("#FirstOrderhf_" + RowNumberInt).val(JsonStringObject);
                        break;
                    }
                case "SecondlvlOrder_1":
                case "SecondlvlOrder_2":
                case "SecondlvlOrder_3":
                    {
                        $("#SecondOrderhf_" + RowNumberInt).val(JsonStringObject);
                        break;
                    }
            }

        })
    }
    //})

}

function ShowDropDownContentInAboveDropDowns(thisitem) {
    var selItem;
    //var ParameterName = $("#txtDeclareParameter").val();

    //$(".dropdown-menu li a").click(function (e) {
    selItem = $(thisitem).text();
    selItem = jQuery.trim(selItem);
    $(thisitem).parents('.btn-HistoryLess').find('.dropdown-toggle').html(selItem + '<span class="caret"></span>');
    $(".dropdown-menu").click(function (e) {
        var SelectedButtonID = $(this).prev('button').attr('id');
        if (selItem === "و") {
            $("#" + SelectedButtonID).val("0");
        }
        else if (selItem === "یا") {
            $("#" + SelectedButtonID).val("1");
        }
        else {
            $("#" + SelectedButtonID).val("2");
        }
        //$("#" + SelectedButtonID).val(ParameterName);

    })
    //})
}

function SetValueOfParametype(thisitem) {
    var selItem;
    //var ParameterName = $("#txtDeclareParameter").val();

    //$(".dropdown-menu li a").click(function (e) {
    selItem = $(thisitem).text();
    selItem = jQuery.trim(selItem);
    $(thisitem).parents('.btn-HistoryLess').find('.dropdown-toggle').html(selItem + '<span class="caret"></span>');
    $(".dropdown-menu").click(function (e) {
        var SelectedButtonID = $(this).prev('button').attr('id');
        if (selItem === "عددی") {
            $("#" + SelectedButtonID).val("0");
        }
        else if (selItem === "زمانی") {
            $("#" + SelectedButtonID).val("1");
        }
        else {
            $("#" + SelectedButtonID).val("2");
        }
        //$("#" + SelectedButtonID).val(ParameterName);

    })
    //})
}

//نمایش آیتم های انتخاب شده در کمبوباکس هایی که فیلد حافظه ندارند 
function ShowDropDownContentInHistoryLessDropDowns(thisitem) {
    var selItem;
    //var ParameterName = $("#txtDeclareParameter").val();

    //$(".dropdown-menu li a").click(function (e) {
    selItem = $(thisitem).text();

    $(thisitem).parents('.btn-HistoryLess').find('.dropdown-toggle').html(selItem + '<span class="caret"></span>');
    //$(".dropdown-menu li a").click(function (e) {
    //var SelectedButtonID = $(this).prev('button').attr('id');
    //$("#" + SelectedButtonID).val(ParameterName);

    //})
    //})
}

function ShowDropDownConditionInVariables() {
    var selText;
    $(".dropdown-menu li a").click(function (e) {
        selText = $(this).text();
        var a;
        var JsonStringObject;
        $(this).parents('.btn-group').find('.dropdown-toggle').html(selText + '<span class="caret"></span>');
    })
}
function FillHiddenFeildInEachRow() {
    $(".dropdown-menu li a").click(function (e) {
        var SelectedULid = e.currentTarget.parentNode.parentNode.id;
        var RowNumber = SelectedULid.substring(SelectedULid.lastIndexOf("_") + 1, SelectedULid.length);

    })
}
function DialogSetVisibilityOfControls() {
    var LocalTableControl = document.getElementById("LocalVariabletableID");
    LocalTableControl.style.visibility = "";

}
// نمایش پارامترهای تعریف شده در کمبوباکس ها
function ShowDeclaredParameterInComboBoxes() {
    var ParameterName = $("#txtDeclareParameter").val();
    var ParameterObjCount = ParameterObjectArrey.length;
    //  for (var i = 0; i < ParameterObjCount; i++) {
    //   var ParameterName = ParameterObjectArrey.parametername;
    for (var j = 1; j <= num; j++) {
        $("#ConceptULCondition_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'>" + ParameterName + "</a></li>"));
        $("#ConceptULCondition_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'>" + ParameterName + "</a></li>"));
    }
    for (var j = 1; j <= ThenOrderCounter; j++) {
        $("#ConceptULOrders_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULOrders_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULOrdersByOrderButton_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULOrdersByOrderButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULOrdersByConditionButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip'><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULOrdersByConditionButton_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
    }
    for (var j = 1; j <= ElseOrderCounter; j++) {
        $("#ConceptULSecondLevel_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULSecondLevel_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));
        $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' ><a href='#'> " + ParameterName + "</a></li>"));

    }
    //  }
}
//‌نمایش پارامترهای تعریف شده در سطرهای جدید
function ShowDeclareParametersInNewDropDowns(caller) {
    var ParameterObjectArreyCount = ParameterObjectArrey.length;
    switch (caller) {
        case 1:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULCondition_td_4_tr_" + num).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULCondition_td_6_tr_" + num).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
        case 3:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULOrders_td_2_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULOrders_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
            //case 4:
            //    for (var i = 0; i < ParameterObjectArreyCount; i++) {
            //        $("#ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter).append($("<li><a href='#'> " + ParameterObjectArrey[i].parametername + " </a></li>"));
            //        $("#ConceptULSecondLevel_td_4_tr_" + ElseOrderCounter).append($("<li><a href='#'> " + ParameterObjectArrey[i].parametername + "</a></li>"));
            //    }
        case 4:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULOrdersByOrderButton_td_2_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULOrdersByOrderButton_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
        case 5:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
        case 6:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
        case 7:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
        case 8:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + "</a></li>"));
                break;
            }
        case 9:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                break;
            }
        case 10:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#FirstConceptbtnFirstLevelUL_td_3_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#SecondConceptbtnFirstLevelUL_td_5_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                break;
            }
        case 11:
            for (var i = 0; i < ParameterObjectArreyCount; i++) {
                $("#ConceptbtnSecondLevelUL_td_3_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                $("#SecondConceptbtnSecondLevelUL_td_5_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ParameterObjectArrey[i].Parametername + " </a></li>"));
                break;
            }
    }

}
//  نمایش متغیرهای عمومی تعریف شده در کمبوباکس های موجود 
function ShowGeneralDeclareVariableInComboBoxes(CurrentControl) {
    var ObjectValue;
    var FirstParameter;
    var SecondPAram;
    var ThirdParam;
    var VariableName = $("#VariableTextID").val();
    var VariableType = $("#VariableTypeID").val();
    var SavebtnID = $(CurrentControl).attr('id');
    var fristcombotype = "";
    var secondcombotype = "";
    var RowNumber = SavebtnID.substring(SavebtnID.lastIndexOf("_") + 1, SavebtnID.length);
    var SuccessMessage = $("#hfOperationSuccessResult").val();
    var ErrorMessage = $("#hfOperationError").val();
    if (VariableName !== "") {
        if (ISVariableNameUniqe == true) {
            var FirstConceptComboText = "";
            var SecondtConceptComboText = "";
            //$("#FirstConceptGeneralVariable_td_3_tr_" + RowNumber).val("");
            //$("#SecondConceptGeneralVariable_td_5_tr_" + RowNumber).val("");
             FirstConceptComboText = $("#FirstConceptGeneralVariable_td_3_tr_" + RowNumber).val();
            if (FirstConceptComboText !== "")
            {
                fristcombotype = 2;
            }
            if (FirstConceptComboText === "") {

                FirstConceptComboText = $("#FirstConceptGeneralVariable_td_3_tr_" + RowNumber).text();
                fristcombotype = 1;
                for (var i = 0; i < ParameterObjectArrey.length; i++) {
                    if (ParameterObjectArrey[i].Parametername === jQuery.trim(FirstConceptComboText)) {
                        FirstConceptComboText = "MyRule[" + '"' + FirstConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                        fristcombotype = 3;
                    }
                }
            }
             SecondtConceptComboText = $("#SecondConceptGeneralVariable_td_5_tr_" + RowNumber).val();
            if (SecondtConceptComboText !== "")
            {
                secondcombotype = 2;
            }
            if (SecondtConceptComboText === "") {
                 SecondtConceptComboText = $("#SecondConceptGeneralVariable_td_5_tr_" + RowNumber).text();
                secondcombotype = 1;
                for (var i = 0; i < ParameterObjectArrey.length; i++) {
                    if (ParameterObjectArrey[i].Parametername === jQuery.trim(SecondtConceptComboText)) {
                        SecondtConceptComboText = "MyRule[" + '"' + SecondtConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                        secondcombotype = 3;
                    }
                }
            }
            var OperationComboText = $("#OprGeneralVariable_td_4_tr_" + RowNumber).text();
            var FinalVariable = $("#GeneralVariableList_td_7_tr_" + RowNumber).text();

            ObjectValue = FirstConceptComboText + OperationComboText + SecondtConceptComboText;

            for (var j = 1; j <= num; j++) {
                $("#ConceptULCondition_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'>" + FinalVariable + "</a></li>"));
                $("#ConceptULCondition_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'>" + FinalVariable + "</a></li>"));

            }
            for (var j = 1; j <= ThenOrderCounter; j++) {
                $("#ConceptULOrders_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULOrders_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#CoceptULOrderByOrderButton_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#CoceptULOrderByOrderButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULOrdersByConditionButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULOrdersByConditionButton_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
            }
            for (var j = 1; j <= ElseOrderCounter; j++) {
                $("#ConceptULSecondLevel_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULSecondLevel_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULSecondLeveByConditionButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                $("#ConceptULSecondLeveByConditionButton_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));

            }


            FinalVariable = jQuery.trim(FinalVariable);
            for (var i = 0; i < VariableObjectArrey.length; i++) {
                if (FinalVariable === VariableObjectArrey[i].variablename && VariableObjectArrey[i].type === 2) {
                    VariableObjectArrey[i].variablefirstparam = FirstConceptComboText;
                    VariableObjectArrey[i].variablesecondparam = OperationComboText;
                    VariableObjectArrey[i].variablethirdparam = SecondtConceptComboText;
                    VariableObjectArrey[i].variablefirstparamtype = fristcombotype;
                    VariableObjectArrey[i].variablesecondparamtype = secondcombotype;
                    VariableObjectArrey[i].variablevalue = ObjectValue;
                }
                $("#VarResualtlbl").text(SuccessMessage);
                $("#VarResualtlbl").css("color", 'green');
                $("#VarResualtFirOrderlbl").text(SuccessMessage);
                $("#VarResualtFirOrderlbl").css("color", 'green');
                $("#VarResualtSecOrderlbl").text(SuccessMessage);
                $("#VarResualtSecOrderlbl").css("color", 'green');
                $("#GeneralVarResualtlbl").text(SuccessMessage);
                $("#GeneralVarResualtlbl").css("color", 'green');

            }
        }
    }
    else
    {
        $("#GeneralVarResualtlbl").text("متغیری جهت انتساب انتخاب نشده است یا متغیر قبلا مقداردهی شده است");
        $("#GeneralVarResualtlbl").css("color", 'red');
    }
}
// نمایش متغیرهای تعریف شده در کمبوباکس ها
function ShowDeclareVariableInComboBoxes(caller, CurrentControl) {
    var ObjectValue;
    var FirstParameter;
    var SecondPAram;
    var ThirdParam;
    var VariableName = $("#VariableTextID").val();
    var VariableType = $("#VariableTypeID").val();
    var SavebtnID = $(CurrentControl).attr('id');
    var fristcombotype = "";
    var secondcombotype = "";
    var RowNumber = SavebtnID.substring(SavebtnID.lastIndexOf("_") + 1, SavebtnID.length);
    var SuccessMessage = $("#hfOperationSuccessResult").val();
    var ErrorMessage = $("#hfOperationError").val();
    try {
        if (document.getElementById('LocalRadio').checked == true) {
            $('#' + ActiveSelectBoxID).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + " </a></li>"));
        }
        else {
            if (ISVariableNameUniqe == true) {

                switch (caller) {
                    case 1:
                        var FirstConceptComboText = "";
                        var SecondtConceptComboText = "";
                         FirstConceptComboText = $("#FirstConceptConditionVariable_td_3_tr_" + RowNumber).val();
                        if (FirstConceptComboText !== "")
                        {
                            fristcombotype = 2;
                        }
                        if (FirstConceptComboText === "") {

                             FirstConceptComboText = $("#FirstConceptConditionVariable_td_3_tr_" + RowNumber).text();
                            fristcombotype = 1;
                            for (var i = 0; i < ParameterObjectArrey.length; i++) {
                                if (ParameterObjectArrey[i].Parametername === jQuery.trim(FirstConceptComboText)) {
                                    FirstConceptComboText = "MyRule[" + '"' + FirstConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                    fristcombotype = 3;
                                }
                            }
                        }
                         SecondtConceptComboText = $("#SecondConceptConditionVariable_td_5_tr_" + RowNumber).val();
                        if (SecondtConceptComboText !== "")
                        {
                            secondcombotype = 2;
                        }
                        if (SecondtConceptComboText === "") {
                            var SecondtConceptComboText = $("#SecondConceptConditionVariable_td_5_tr_" + RowNumber).text();
                            secondcombotype = 1;
                            for (var i = 0; i < ParameterObjectArrey.length; i++) {
                                if (ParameterObjectArrey[i].Parametername === jQuery.trim(SecondtConceptComboText)) {
                                    SecondtConceptComboText = "MyRule[" + '"' + SecondtConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                    secondcombotype = 3;
                                }
                            }
                        }
                        var OperationComboText = $("#OprConditionVariable_td_4_tr_" + RowNumber).text();
                        var FinalVariable = $("#ConditionVariableList_td_7_tr_" + RowNumber).text();

                        ObjectValue = FirstConceptComboText + OperationComboText + SecondtConceptComboText;




                        for (var j = 1; j <= num; j++) {
                            $("#ConceptULCondition_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'>" + FinalVariable + "</a></li>"));
                            $("#ConceptULCondition_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'>" + FinalVariable + "</a></li>"));

                        }

                        break;
                    case 2:
                        var FirstConceptComboText = "";
                        var SecondtConceptComboText = "";
                         FirstConceptComboText = $("#ConceptbtnFirstLevel_td_3_tr_" + RowNumber).val();
                        if (FirstConceptComboText !== "")
                        {
                            fristcombotype = 2;
                        }
                        if (FirstConceptComboText === "") {
                             FirstConceptComboText = $("#ConceptbtnFirstLevel_td_3_tr_" + RowNumber).text();
                            fristcombotype = 1;
                            for (var i = 0; i < ParameterObjectArrey.length; i++) {
                                if (ParameterObjectArrey[i].Parametername === jQuery.trim(FirstConceptComboText)) {
                                    FirstConceptComboText = "MyRule[" + '"' + FirstConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                    fristcombotype = 3;
                                }
                            }
                        }
                         SecondtConceptComboText = $("#SecondConceptbtnFirstLevel_td_5_tr_" + RowNumber).val();
                        if (SecondtConceptComboText !== "")
                        {
                            secondcombotype = 2;
                        }
                        if (SecondtConceptComboText === "") {
                             SecondtConceptComboText = $("#SecondConceptbtnFirstLevel_td_5_tr_" + RowNumber).text();
                            secondcombotype = 1;
                            for (var i = 0; i < ParameterObjectArrey.length; i++) {
                                if (ParameterObjectArrey[i].Parametername === jQuery.trim(SecondtConceptComboText)) {
                                    SecondtConceptComboText = "MyRule[" + '"' + SecondtConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                    secondcombotype = 3;
                                }
                            }
                        }
                        var OperationComboText = $("#AndOrcmbFirstLevel_td_4_tr_" + RowNumber).text();
                        var FinalVariable = $("#FirstLevelVariable_td_7_tr_" + RowNumber).text();
                        ObjectValue = FirstConceptComboText + OperationComboText + SecondtConceptComboText;
                        for (var j = 1; j <= ThenOrderCounter; j++) {
                            $("#ConceptULOrders_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#ConceptULOrders_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#CoceptULOrderByOrderButton_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#CoceptULOrderByOrderButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#ConceptULOrdersByConditionButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#ConceptULOrdersByConditionButton_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                        }
                        //ItemSave_RuleGenerator();

                        break;
                    case 3:
                        var FirstConceptComboText = "";
                        var SecondtConceptComboText = "";
                         FirstConceptComboText = $("#ConceptbtnSecondLevel_td_3_tr_" + RowNumber).val();
                        if (FirstConceptComboText != "")
                        {
                            fristcombotype = 2;
                        }
                        if (FirstConceptComboText === "") {
                             FirstConceptComboText = $("#ConceptbtnSecondLevel_td_3_tr_" + RowNumber).text();
                            fristcombotype = 1;
                            for (var i = 0; i < ParameterObjectArrey.length; i++) {
                                if (ParameterObjectArrey[i].Parametername === jQuery.trim(FirstConceptComboText))
                                    FirstConceptComboText = "MyRule[" + '"' + FirstConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                fristcombotype = 3;
                            }
                        }
                         SecondtConceptComboText = $("#SecondConceptbtnSecondLevel_td_5_tr_" + RowNumber).val();
                        if (SecondtConceptComboText !== "")
                        {
                            secondcombotype = 2;
                        }
                        if (SecondtConceptComboText === "") {
                             SecondtConceptComboText = $("#SecondConceptbtnSecondLevel_td_5_tr_" + RowNumber).text();
                            secondcombotype = 1;
                            for (var i = 0; i < ParameterObjectArrey.length; i++) {
                                if (ParameterObjectArrey[i].Parametername === jQuery.trim(SecondtConceptComboText))
                                    SecondtConceptComboText = "MyRule[" + '"' + SecondtConceptComboText + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                secondcombotype = 3;
                            }
                        }
                        var OperationComboText = $("#AndOrcmbSecondLevel_td_4_tr_" + RowNumber).text();
                        var FinalVariable = $("#SecondLevelVariable_td_7_tr_1" + RowNumber);

                        ObjectValue = FirstConceptComboText + OperationComboText + SecondtConceptComboText;
                        for (var j = 1; j <= ElseOrderCounter; j++) {
                            $("#ConceptULSecondLevel_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#ConceptULSecondLevel_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + FinalVariable + "</a></li>"));
                            $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + VariableName + "</a></li>"));
                            $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + VariableName + "</a></li>"));
                            $("#ConceptULSecondLeveByConditionButton_td_4_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + VariableName + "</a></li>"));
                            $("#ConceptULSecondLeveByConditionButton_td_6_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)' class='Tooltip' data-toggle='tooltip' onmouseover='ChagnigliAttribute()'><a href='#'> " + VariableName + "</a></li>"));

                        }
                        //ItemSave_RuleGenerator();

                        break;


                }
                FinalVariable = jQuery.trim(FinalVariable);
                for (var i = 0; i < VariableObjectArrey.length; i++) {
                    if (FinalVariable === VariableObjectArrey[i].variablename && VariableObjectArrey[i].type === 2) {
                        VariableObjectArrey[i].variablefirstparam = FirstConceptComboText;
                        VariableObjectArrey[i].variablesecondparam = OperationComboText;
                        VariableObjectArrey[i].variablethirdparam = SecondtConceptComboText;
                        VariableObjectArrey[i].variablefirstparamtype = fristcombotype;
                        VariableObjectArrey[i].variablesecondparamtype = secondcombotype;
                        VariableObjectArrey[i].variablevalue = ObjectValue;
                    }
                    $("#VarResualtlbl").text(SuccessMessage);
                    $("#VarResualtlbl").css("color", 'green');
                    $("#VarResualtFirOrderlbl").text(SuccessMessage);
                    $("#VarResualtFirOrderlbl").css("color", 'green');
                    $("#VarResualtSecOrderlbl").text(SuccessMessage);
                    $("#VarResualtSecOrderlbl").css("color", 'green');

                }
            }
        }
    }
    catch (ex) {
        $("#VarResualtlbl").text(ErrorMessage);
        $("#VarResualtlbl").css("color", 'red');
        $("#VarResualtFirOrderlbl").text(ErrorMessage);
        $("#VarResualtFirOrderlbl").css("color", 'red');
        $("#VarResualtSecOrderlbl").text(ErrorMessage);
        $("#VarResualtSecOrderlbl").css("color", 'red');
    }
}
//  ثبت پارامتر و اضافه کردن آن به آرایه   
function btnRegisterParameter_onClick() {
    var ParameterName = $("#txtDeclareParameter").val();
    var ParameterType = $("#ParameterType_RuleGenerator").text();
    var SuccessMessage = $("#hfOperationSuccessResult").val();
    var ErrorMessage = $("#hfOperationError").val();
    var ReplicatedName = $("#hfReplicatedName").val();
    var ArreyCount = ParameterObjectArrey.length;
    var SelectedTypeValue;
    var ParameterCount = 0;
    //if ($.isNumeric(ParameterName))
    //{
    //    $("#Resultlbl").text("");
    //    $("#Resultlbl").text(" نام پارامتر نباید عدد باشد");
    //    $("#Resultlbl").css("color", 'red');
    //    return;
    //}



    if (/[^A-Za-z]/g.test(ParameterName)) {
        $("#Resultlbl").text("");
        $("#Resultlbl").text(" نام پارامتر باید فقط شامل حروف انگلیسی باشد");
        $("#Resultlbl").css("color", 'red');
        return;
    }

    try {
        for (var i = 0; i < ArreyCount; i++) {
            if (ParameterObjectArrey[i].Parametername === ParameterName) {
                ISParameterNameUniqe = false;
                $("#Resultlbl").text("");
                $("#Resultlbl").text(ReplicatedName);
                $("#Resultlbl").css("color", 'red');
                return;
            }
        }
        ParameterCount += 1;
        SelectedTypeValue = jQuery.trim(ParameterType);
        switch (SelectedTypeValue) {
            case "عددی":
                SelectedTypeValue = 0;
                break;
            case "زمانی":
                SelectedTypeValue = 1;
                break;
            case "تاریخی":
                SelectedTypeValue = 2;
                break;
        }
        ParameterObjectArrey.push({ Parameterid: 'RuleParameter_' + ParameterCount, Parametername: ParameterName, Parametertype: SelectedTypeValue });
        ShowDeclaredParameterInComboBoxes();
        $("#Resultlbl").text(SuccessMessage);
        $("#Resultlbl").css("color", 'green');
    }
    catch (ex) {
        $("#Resultlbl").text(ErrorMessage);
        $("#Resultlbl").css("color", 'red');

    }
}
// ثبت متغیر عمومی و اضافه کردن آن به آرایه
function RegisterGeneralVariables() {
    var VariableName = $("#VariableTextID").val();
    var VariableType = $("#VariableTypeID").val();
    var ConstValue = $("#ConstTextID").val();
    var ArreyCount = VariableObjectArrey.length;
    var CallerModal;
    var SelectedTypeValue;
    var SuccessMessage = $("#hfOperationSuccessResult").val();
    var ErrorMessage = $("#hfOperationError").val();
    var ReplicatedName = $("#hfReplicatedName").val();
    try {
        var SelectedModalTabID = SelectedModalTab.id;
    }
    catch (ex) {
        var SelectedModalTabID = "Variable";
    }
    if (/[^A-Za-z]/g.test(VariableName)) {
        $("#SaveVarMessagelbl").text("");
        $("#SaveVarMessagelbl").text(" نام متغیر باید فقط شامل حروف انگلیسی باشد");
        $("#SaveVarMessagelbl").css("color", 'red');
        return;
    }
    if (ConstValue !== "") {
        if ($.isNumeric(ConstValue)) {
            ConstValue = jQuery.trim(ConstValue);
        }
        else {
            $("#SaveConstMessagelbl").text("");
            $("#SaveConstMessagelbl").text(" ثابت عددی نمیتواند شامل حروف باشد ");
            $("#SaveConstMessagelbl").css("color", 'red');
            return;
        }
    }
    try {

        if (SelectedModalTabID === "Const") {
            for (j = 1; j <= GeneralVariableCounter; j++) {
                $("#GeneralVariableCmbUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                $("#GeneralVariableUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#GeneralVariableUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));

            }
            for (var jj = 1; jj <= num; jj++) {
                $("#ConceptULCondition_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULCondition_td_6_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
            }
            for (var jj = 1; jj <= ThenOrderCounter; jj++) {
                $("#ConceptULOrders_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULOrders_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULOrdersByConditionButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULOrdersByConditionButton_td_6_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULOrdersByOrderButton_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULOrdersByOrderButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));

            }
            for (var jj = 1; jj <= ElseOrderCounter; jj++) {

                $("#ConceptULSecondLevel_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULSecondLevel_td_3_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));

            }
            DeclaredVariableCounter += 1;
            VariableObjectArrey.push({ variableid: 'ConstID_' + DeclaredVariableCounter, type: 1, variablename: ConstValue, variabletype: VariableType, variablevalue: '', variablefirstparam: '', variablesecondparam: '', variablethirdparam: '', variablefirstparamtype: '', variablesecondparamtype: '', variabletarget: CallerModal });
            $("#SaveConstMessagelbl").text(SuccessMessage);
            $("#SaveConstMessagelbl").css("color", 'green');

        }

        else {
            for (i = 0; i < ArreyCount; i++) {
                if (VariableObjectArrey[i].variablename === VariableName) {
                    ISVariableNameUniqe = false;
                    $("#SaveVarMessagelbl").text(ReplicatedName);
                    $("#SaveVarMessagelbl").css("color", 'red');
                    return;
                }
            }
            for (j = 1; j <= GeneralVariableCounter; j++) {
                $("#GeneralVariableCmbUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableName + " </a></li>"));
                $("#GeneralVariableUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + " </a></li>"));
                $("#GeneralVariableUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + " </a></li>"));

            }
            SelectedTypeValue = jQuery.trim(VariableType);
            switch (SelectedTypeValue) {
                case "عددی":
                    SelectedTypeValue = 0;
                    break;
                case "زمانی":
                    SelectedTypeValue = 1;
                    break;
                case "تاریخی":
                    SelectedTypeValue = 2;
                    break;
            }
            DeclaredVariableCounter += 1;
            VariableObjectArrey.push({ variableid: 'VariableID_' + DeclaredVariableCounter, type: 2, variablename: VariableName, variabletype: SelectedTypeValue, variablevalue: '', variablefirstparam: '', variablesecondparam: '', variablethirdparam: '', variablefirstparamtype: '', variablesecondparamtype: '', variabletarget: CallerModal });
            $("#SaveVarMessagelbl").text(SuccessMessage);
            $("#SaveVarMessagelbl").css("color", 'green');

        }

    }

    catch (ex) {
        $("#SaveVarMessagelbl").text(ErrorMessage);
        $("#SaveConstMessagelbl").text(ErrorMessage);
        $("#SaveVarMessagelbl").css("color", 'red');
        $("#SaveConstMessagelbl").css("color", 'red');
    }

}
//  اضافه کردن ثابت عددی به تمام کمبوها
function AddConstToNewRow(Caller) {
    for (var i = 0; i < DeclaredVariableCounter; i++) {
        if (VariableObjectArrey[i].type === 1) {
            switch (Caller) {
                case 1:
                    $("#ConceptULCondition_td_4_tr_" + num).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULCondition_td_6_tr_" + num).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    break;
                case 2:
                    $("#ConceptULOrders_td_2_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#ConceptULOrders_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByOrderButton_td_2_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByOrderButton_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));

                    break;
                case 3:
                    $("#ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    break;
                case 4:
                    $("#GeneralVariableCmbUL_td_7_tr_" + GeneralVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#GeneralVariableUL_td_3_tr_" + GeneralVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#GeneralVariableUL_td_5_tr_" + GeneralVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));


                    break;
                case 5:
                    $("#ConditionVariableCmbUL_td_7_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    break;
                case 6:
                    $("#FirstLevelVariableUL_td_7_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#FirstConceptbtnFirstLevelUL_td_3_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#SecondConceptbtnFirstLevelUL_td_5_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    break;
                case 7:
                    $("#ConceptbtnSecondLevelUL_td_3_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#SecondConceptbtnSecondLevelUL_td_5_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#SecondLevelVariableUL_td_7_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableObjectArrey[i].variablename + " </a></li>"));
                    break;
            }







        }
    }
}
// ثبت متغیر و اضافه کردن آن به آرایه
function RegisterLocalVariable() {
    //var callerID = DialogDeclareVariable.get_value();
    var VariableName = $("#VariableTextID").val();
    var VariableType = $("#VariableTypeID").val();
    var ConstValue = $("#ConstTextID").val();
    var ArreyCount = VariableObjectArrey.length;
    var CallerModal;
    var SelectedTypeValue;
    var SuccessMessage = $("#hfOperationSuccessResult").val();
    var ErrorMessage = $("#hfOperationError").val();
    var ReplicatedName = $("#hfReplicatedName").val();
    try {
        var SelectedModalTabID = SelectedModalTab.id;
    }
    catch (ex) {
        var SelectedModalTabID = "Variable";
    }

    if (/[^A-Za-z]/g.test(VariableName)) {
        $("#SaveVarMessagelbl").text("");
        $("#SaveVarMessagelbl").text(" نام متغیر باید فقط شامل حروف انگلیسی باشد");
        $("#SaveVarMessagelbl").css("color", 'red');
        return;
    }
    
    if (ConstValue !== "") {
        if ($.isNumeric(ConstValue)) {
            ConstValue = jQuery.trim(ConstValue);
        }
        else {
            $("#SaveConstMessagelbl").text("");
            $("#SaveConstMessagelbl").text(" ثابت عددی نمیتواند شامل حروف باشد ");
            $("#SaveConstMessagelbl").css("color", 'red');
            return;
        }
    }
    try {
        if (SelectedModalTabID === "Const") {
            switch (ActiveModal.selector) {
                case "ShowConditionModal":
                    CallerModal = 1;
                    for (j = 1; j <= ConditionVariableCounter; j++) {
                        $("#ConditionVariableCmbUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                        $("#FirstConceptConditionVariableUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#SecondConceptConditionVariableUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));

                    }
                    for (var jj = 1; jj <= num; jj++) {
                        $("#ConceptULCondition_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULCondition_td_6_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                    }
                    break;
                case "ShowFirstOrderModal":
                    CallerModal = 2;
                    for (j = 1; j <= FirstLVariableCounter; j++) {
                        $("#FirstLevelVariableUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                        $("#FirstLevelVariableUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                        $("#SecondConceptbtnFirstLevelUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                    }
                    for (var jj = 1; jj <= ThenOrderCounter; jj++) {
                        $("#ConceptULOrders_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULOrders_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULOrdersByConditionButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULOrdersByConditionButton_td_6_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULOrdersByOrderButton_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULOrdersByOrderButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));

                    }
                    break;
                case "ShowSecondtOrderModal":
                    CallerModal = 3;
                    for (var j = 1; j <= SecondLVariableCounter ; j++) {

                        $("#ConceptbtnSecondLevelUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                        $("#SecondConceptbtnSecondLevelUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                        $("#SecondLevelVariableUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + ConstValue + " </a></li>"));
                    }
                    for (var jj = 1; jj <= ElseOrderCounter; jj++) {

                        $("#ConceptULSecondLevel_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULSecondLevel_td_3_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));
                        $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + jj).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + ConstValue + " </a></li>"));

                    }
                    break;
            }

            DeclaredVariableCounter += 1;
            VariableObjectArrey.push({ variableid: 'ConstID_' + DeclaredVariableCounter, type: 1, variablename: ConstValue, variabletype: VariableType, variablevalue: '', variablefirstparam: '', variablesecondparam: '', variablethirdparam: '', variablefirstparamtype: '', variablesecondparamtype: '', variabletarget: CallerModal });
            $("#SaveConstMessagelbl").text(SuccessMessage);
            $("#SaveConstMessagelbl").css("color", 'green');
        }


        else {

            for (i = 0; i < ArreyCount; i++) {
                if (VariableObjectArrey[i].variablename === VariableName) {
                    ISVariableNameUniqe = false;
                    $("#SaveVarMessagelbl").text(ReplicatedName);
                    $("#SaveVarMessagelbl").css("color", 'red');
                    return;
                }
            }

            switch (ActiveModal.selector) {
                case "ShowConditionModal":
                    CallerModal = 1;
                    for (j = 1; j <= ConditionVariableCounter; j++) {
                        //$("#ConditionVariable_" + j).append($("<option value=" + VariableName + ">" + VariableName + " </option>"));
                        $("#ConditionVariableCmbUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableName + " </a></li>"));
                        $("#FirstConceptConditionVariableUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + " </a></li>"));
                        $("#SecondConceptConditionVariableUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + " </a></li>"));
                    }
                    break;
                case "ShowFirstOrderModal":
                    CallerModal = 2;
                    for (j = 1; j <= FirstLVariableCounter; j++) {
                        //$("#FirstLevelVariable_" + j).append($("<option value=" + VariableName + ">" + VariableName + " </option>"));
                        $("#FirstLevelVariableUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableName + " </a></li>"));
                        $("#FirstConceptbtnFirstLevelUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + "</a></li>"));
                        $("#SecondConceptbtnFirstLevelUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + "</a></li>"));
                    }
                    break;
                case "ShowSecondtOrderModal":
                    CallerModal = 3;
                    for (var j = 1; j <= SecondLVariableCounter ; j++) {
                        //$("#SecondLevelVariable_" + j).append($("<option value=" + VariableName + ">" + VariableName + " </option>"));
                        $("#ConceptbtnSecondLevelUL_td_3_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableName + " </a></li>"));
                        $("#SecondConceptbtnSecondLevelUL_td_5_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + " </a></li>"));
                        $("#SecondLevelVariableUL_td_7_tr_" + j).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableName + "</a></li>"));
                    }

                    break;
            }
            SelectedTypeValue = jQuery.trim(VariableType);
            switch (SelectedTypeValue) {
                case "عددی":
                    SelectedTypeValue = 0;
                    break;
                case "زمانی":
                    SelectedTypeValue = 1;
                    break;
                case "تاریخی":
                    SelectedTypeValue = 2;
                    break;
            }
            DeclaredVariableCounter += 1;
            VariableObjectArrey.push({ variableid: 'VariableID_' + DeclaredVariableCounter, type: 2, variablename: VariableName, variabletype: SelectedTypeValue, variablevalue: '', variablefirstparam: '', variablesecondparam: '', variablethirdparam: '', variablefirstparamtype: '', variablesecondparamtype: '', variabletarget: CallerModal });
            $("#SaveVarMessagelbl").text(SuccessMessage);
            $("#SaveVarMessagelbl").css("color", 'green');

        }

    }
    catch (ex) {
        $("#SaveVarMessagelbl").text(ErrorMessage);
        $("#SaveConstMessagelbl").text(ErrorMessage);
        $("#SaveVarMessagelbl").css("color", 'red');
        $("#SaveConstMessagelbl").css("color", 'red');
    }
}
// اضافه کردن متغیر عمومی تعریف شده به سطر جدید
function AddGeneralVariableToNewRow() {

    for (i = 0 ; i < VariableObjectArrey.length ; i++) {
        if (VariableObjectArrey[i].type === 2) {
            $("#GeneralVariableCmbUL_td_7_tr_" + GeneralVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
            $("#GeneralVariableUL_td_3_tr_" + GeneralVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
            $("#GeneralVariableUL_td_5_tr_" + GeneralVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
        }
    }
}
// اضافه کردن تمامی متغیرهای تعریف شده به سطر جدید
function AddVariablesToNewRow(Caller) {
    for (i = 0 ; i < VariableObjectArrey.length ; i++) {
        switch (Caller) {
            case 1:
                if (VariableObjectArrey[i].variablevalue != '') {
                    $("#ConditionVariableCmbUL_td_7_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#FirstConceptConditionVariableUL_td_3_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#SecondConceptConditionVariableUL_td_5_tr_" + ConditionVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                }
                break;
            case 2:
                if (VariableObjectArrey[i].variablevalue != '') {
                    $("#FirstLevelVariableUL_td_7_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#FirstConceptbtnFirstLevelUL_td_3_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#SecondConceptbtnFirstLevelUL_td_5_tr_" + FirstLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));

                }
                break;
            case 3:
                if (VariableObjectArrey[i].variablevalue != '') {
                    $("#ConceptbtnSecondLevelUL_td_3_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#SecondConceptbtnSecondLevelUL_td_5_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#SecondLevelVariableUL_td_7_tr_" + SecondLVariableCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>" + VariableObjectArrey[i].variablename + " </a></li>"));

                }
                break;
            case 4:
                if (VariableObjectArrey[i].variablevalue != '') {
                    $("#ConceptULCondition_td_4_tr_" + num).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULCondition_td_6_tr_" + num).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                }
                break;
            case 5:
                if (VariableObjectArrey[i].variablevalue != '') {
                    $("#ConceptULOrders_td_2_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#ConceptULOrders_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByOrderButton_td_2_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByOrderButton_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                }
            case 6:
                if (VariableObjectArrey[i].variablevalue != '') {
                    $("#ConceptULSecondLevel_td_2_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + " </a></li>"));
                    $("#ConceptULSecondLevel_td_3_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                    $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter).append($("<li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'> " + VariableObjectArrey[i].variablename + "</a></li>"));
                }
        }
    }
}
//  بدست اوردن شناسه کنترل ها
function GetControlID(CurrentControl) {
    ActiveSelectBoxID = $(CurrentControl).parent().parent().attr('id');
    $("#VariableAssignmentModal").modal();

}
// نمایش یا عدم نمایش گزینه متغیر محلی
function LocalVariableSetting() {
    var IsLocalVariabledInUse = CheckRGVariablSetting();
    if (IsLocalVariabledInUse.LocalVariable === true) {
        for (var i = 1; i <= num; i++) {
            //$("#DropDownAddCondition_td_4_tr_" + i).append($("<option id='FirstDeclareLocalConditionVariable_" + i + "' onclick='GetControlID(this)'>تعریف متغیر محلی</option><option> _______ </option>"));
            $("#ConceptULCondition_td_4_tr_" + i).append($("<li><a href='#VariableAssignmentModal' id='FirstDeclareLocalConditionVariable_" + i + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'>تعریف متغیر محلی</a></li>"));
            $("#ConceptULCondition_td_6_tr_" + i).append($("<li><a href='#VariableAssignmentModal' id='SecondDeclareLocalConditionVariable_" + i + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a> </li>"));
        }
        for (var j = 1; j <= ThenOrderCounter ; j++) {
            $("#ConceptULOrders_td_2_tr_" + j).append($("<li><a href='#' id='FirstDeclareLocalFirstLvlOrderVariableByPrimary_" + ThenOrderCounter + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULOrders_td_4_tr_" + j).append($("<li><a href='#' id='SecondDeclareLocalFirstLvlOrderVariableByPrimary_" + ThenOrderCounter + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULOrdersByOrderButton_td_2_tr_" + j).append($("<li><a href='#' id='FirstDeclareLocalFirstLvlOrderVariableByOrder_" + ThenOrderCounter + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULOrdersByOrderButton_td_4_tr_" + j).append($("<li><a href='#' id='SecondDeclareLocalFirstLvlOrderVariableByOrder_" + ThenOrderCounter + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULOrdersByConditionButton_td_4_tr_" + j).append($("<li><a href='#' id='FirstDeclareLocalFirstLvlOrderVariableByCondition_" + ThenOrderCounter + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULOrdersByConditionButton_td_6_tr_" + j).append($("<li><a href='#' id='SecondDeclareLocalFirstLvlOrderVariableByCondition_" + ThenOrderCounter + "' onclick='GetControlID(this)' data-target='#VariableAssignmentModal'> تعریف متغیر محلی</a></li>"));
        }
        for (var k = 1; k <= ElseOrderCounter; k++) {
            $("#ConceptULSecondLevel_td_2_tr_" + k).append($("<li><a href='#' id='SecondDeclareLocalSecondLvlVariableByPrimary_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULSecondLevel_td_3_tr_" + k).append($("<li><a href='#' id='FisrtDeclareLocalSecondLvlVariableByPrimary_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULSecondLevelByOrderButton_td_2_tr_" + k).append($("<li><a href='#' id='FisrtDeclareLocalSecondLvlVariableByOrder_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULSecondLevelByOrderButton_td_4_tr_" + k).append($("<li><a href='#' id='SecondDeclareLocalSecondLvlVariableByOrder_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULSecondLevelByConditionButton_td_4_tr_" + k).append($("<li><a href='#' id='FisrtDeclareLocalSecondLvlVariableByCondition_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</a></li>"));
            $("#ConceptULSecondLevelByConditionButton_td_6_tr_" + k).append($("<li><a href='#' id='SecondDeclareLocalSecondLvlVariableByCondition_" + ElseOrderCounter + "' onclick='GetControlID(this)'> تعریف متغیر محلی</a></li>"));
        }
    }
}
// بدست آودن مقدار فیلد حافظه ی تنظیمات متغیر 
function CheckRGVariablSetting() {
    var SettingHFValue = $("#hfRGSetting_Setting").val();
    var JsonSettingObj = JSON.parse(SettingHFValue);
    return JsonSettingObj;
}
// بدست آوردن مقدار فیلد حافظه ی تنظیمات متغیر عمومی
function CheckRGSettingForGeneralVariables() {
    var SettingHFValue = $("#hfRGSetting_Setting").val();
    var JsonSettingObj = JSON.parse(SettingHFValue);
    return JsonSettingObj;
}
function CharToKeyCode_RuleGenerator(str) {
    var OutStr = '';
    if (str != null && str != undefined) {
        for (var i = 0; i < str.length; i++) {
            var KeyCode = str.charCodeAt(i);
            var CharKeyCode = '//' + KeyCode;
            OutStr += CharKeyCode;
        }
    }
    return OutStr;
}
function ItemSave_RuleGenerator() {
    RuleGenerator_onSave();
}
function ItemNew_RuleGenerator_OnClick() {
    ChangePageState_RuleGenerator('Add');
}
function ChangePageState_RuleGenerator(state) {
    CurrentPageState_RuleGenerator = state;
}
function RuleGenerator_onSave() {
    if (CurrentPageState_RuleGenerator != 'Delete') {


        UpdateRuleGenerator_RuleGenerator();

    }
    else
        ShowDialogConfirm('Delete');
}
function UpdateRuleGenerator_RuleGenerator() {
    ItemNew_RuleGenerator_OnClick();
    var DialogRuleGeneratorValue = parent.DialogRuleGenerator.get_value();
    var RuleTemplateID = DialogRuleGeneratorValue.ID;
    var DesignedRuleID = DialogRuleGeneratorValue.DesignedRuleID;
    CreateJsonobjectOfHiddenFields();

    var CSharpCOde = CreateCSharpCodeOfRule();
    if (CSharpCOde !== 0) {
        var PersionScript = CreatePersianScript();
        ObjRuleGenerator_RuleGenerator = new Object();
        ObjRuleGenerator_RuleGenerator.VariableObjectArrey = null;
        ObjRuleGenerator_RuleGenerator.ParameterObjectArrey = null;
        if (CurrentPageState_RuleGenerator != 'Delete') {
            ObjRuleGenerator_RuleGenerator.VariableObjectArrey = VariableObjectArrey;
            ObjRuleGenerator_RuleGenerator.ParameterObjectArrey = ParameterObjectArrey;
            ObjRuleGenerator_RuleGenerator.RuleStateObjectArrey = AllHiddenFieldValues;
            ObjRuleGenerator_RuleGenerator.CSharpCodeObject = CSharpCOde;
            ObjRuleGenerator_RuleGenerator.PersionScript = PersionScript;
        }
        var ObjRuleGeneratorVariableArreyJson = JSON.stringify(ObjRuleGenerator_RuleGenerator.VariableObjectArrey);
        var ObjRuleGeneratorParameterArreyJson = JSON.stringify(ObjRuleGenerator_RuleGenerator.ParameterObjectArrey);
        var ObjRuleGeneratorRuleStateArreyJson = JSON.stringify(ObjRuleGenerator_RuleGenerator.RuleStateObjectArrey);
        UpdateRuleGenerator_RuleGeneratorPage(CharToKeyCode_RuleGenerator(CurrentPageState_RuleGenerator), 1, CharToKeyCode_RuleGenerator(ObjRuleGeneratorVariableArreyJson), CharToKeyCode_RuleGenerator(ObjRuleGeneratorParameterArreyJson), CharToKeyCode_RuleGenerator(ObjRuleGeneratorRuleStateArreyJson), CharToKeyCode_RuleGenerator(CSharpCOde), CharToKeyCode_RuleGenerator(PersionScript), RuleTemplateID, DesignedRuleID);
    }
}
function UpdateRuleGenerator_RuleGeneratorPage_OnCallBack(Response) {

    var RetMessage = Response;
    if (Response != null && Response.length > 0) {
        // DialogWaiting.Close();
        if (Response[1] == "ConnectionError") {
            Response[0] = document.getElementById('hfErrorType_RuleGenerator').value;
            Response[1] = document.getElementById('hfConnectionError_RuleGenerator').value;
        }
        //showDialog(RetMessage[0], Response[1], RetMessage[2]);
        $("#SaveResultHeaderlbl").text(RetMessage[0])
        $("#SaveResultllbl").text(RetMessage[1]);
        if (RetMessage[2] == 'success') {
            $("#SaveResultllbl").css("color", "green");
        }
        else {
            $("#SaveResultllbl").css("color", "red");
        }
        ChangePageState_RuleGenerator('View');
    }
    else {
        if (CurrentPageState_RuleGenerator == 'Delete')
            ChangePageState_RuleGenerator('View');
    }
    $('#SaveRuleModal').modal('show');
}
function FirstAndOR_RuleGenerator_OnClick() {
    //ShowDropDownContentInHistoryLessDropDowns();
}
function ParameterType_RuleGenerator_OnClick() {
    ShowDropDownContentInHistoryLessDropDowns();
}
function RegParameter_RuleGenerator_onClick() {
    btnRegisterParameter_onClick();
}
function VariableTypeID_RuleGenerator_OnClick() {
    ShowDropDownContentInHistoryLessDropDowns();
}
function RegConstID_RuleGenerator_onClick() {
    RegisterGeneralVariables();
}
function Firstcmb_RuleGenerator_OnClick() {
    //FillDaysAndOr();
}
function RegVariableID_RuleGenerator_onClick() {
    var IsVariableGeneral = CheckRGVariablSetting();
    if (IsVariableGeneral.GeneralVariable === true) {
        RegisterGeneralVariables();
    }
    else {
        RegisterLocalVariable();
    }
}
function VariableSetting() {
    var IsVariableGeneral = CheckRGVariablSetting();
    if (IsVariableGeneral.GeneralVariable === true) {
        $("#ShowConditionModal").css('display', 'none');
        $("#ShowFirstOrderModal").css('display', 'none');
        $("#ShowSecondtOrderModal").css('display', 'none');
    }
    else {
        $("#AddRuleGeneralVariables_RuleGenerator").css('display', 'none');
    }
}
//  ولیدیشن ایجاد کد سی شارپ
function CheckValidateInGeneratingCode(FirstCondition, SecondCondition, FirstOrder, SecondOrder) {
    var LocalResourceObject = JSON.parse(ResourceValues);
    if (FirstCondition === "" && SecondCondition === "") {
        var Message = LocalResourceObject.RuleWithoutCondition;
        $("#ResualtWarninglbl").text(Message);
        $('#RuleWarning').modal('show');
        return 0;
    }

    if (FirstOrder === "" && SecondOrder !== "") {
        var Message = LocalResourceObject.InvalidSecondOrder;
        $("#ResualtWarninglbl").text(Message);
        $('#RuleWarning').modal('show');
        return 0;
    }
    if ((FirstOrder === "" && SecondOrder === "") || FirstOrder === "") {

        var Message = LocalResourceObject.RuleWithoutOrder;
        $("#ResualtWarninglbl").text(Message);
        $('#RuleWarning').modal('show');
        return 0;
    }
}
// ولیدیشن اضافه کردن سطر
function CheckValidateInAddiogRulesItems(AddingItemType) {
    var IsValid = "true";
    var OrderedHF = GetHiddenFieldByOrder();
    var IndexOfAddingItemType = AddingItemType.indexOf("_");
    var AddingItemTypeWithoutNumber = AddingItemType.substring(0, IndexOfAddingItemType);
    var FirstConditionValue = $("#hfDaysAndOr")[0].value;
    var FirstConditionValueParse = JSON.parse(FirstConditionValue);
    var HasFirstConditionValue = 'true';
    var HasSecondConditionValue = 'true';
    var HasSecondOrderValue = 'true';
    if (FirstConditionValueParse[0].Value === "" || FirstConditionValueParse[0].Value.length === 0) {
        HasFirstConditionValue = 'false';
    }
    switch (AddingItemTypeWithoutNumber) {
        case "FirstlvlOrder":
            {

                if (OrderedHF.length > 0) {
                    for (var i = 0; i < OrderedHF.length; i++) {
                        var OrderHFValue = OrderedHF[i].value;
                        var OrderHFValueObject = JSON.parse(OrderHFValue);
                        var OrderHFValueObjectType = OrderHFValueObject[0].Type;
                        var IndexOfOrderedHF = OrderHFValueObjectType.indexOf("_");
                        var OrderHFWithoutNumber = OrderHFValueObjectType.substring(0, IndexOfOrderedHF);

                        if (OrderHFWithoutNumber === "Condition") {
                            HasSecondConditionValue = 'true';
                            break;
                        }
                        else {
                            HasSecondConditionValue = 'false';
                        }
                    }
                }
                else {
                    HasSecondConditionValue = 'false';
                }
                if (HasFirstConditionValue === 'false' && HasSecondConditionValue === 'false') {
                    IsValid = 'false';
                }
                break;
            }
        case "SecondlvlOrder":
            {
                if (OrderedHF.length > 0) {
                    for (var i = 0; i < OrderedHF.length; i++) {
                        var OrderedHFValue = OrderedHF[i].value;
                        var OrderedHFObject = JSON.parse(OrderedHFValue);
                        var OrderedHFObjectType = OrderedHFObject[0].Type;
                        var IndexOfOrderHFType = OrderedHFObjectType.indexOf("_");
                        var OrderHFWithoutNumber = OrderedHFObjectType.substring(0, IndexOfOrderHFType);

                        if (OrderHFWithoutNumber === "FirstlvlOrder") {
                            HasSecondOrderValue = 'true';
                            break;
                        }
                        else {
                            HasSecondOrderValue = 'false';
                        }
                    }
                }
                else {
                    HasSecondOrderValue = 'false';
                }
                if (HasSecondOrderValue === 'false') {
                    IsValid = 'false';
                }
                break;
            }

    }

    return IsValid;
}

//  ایجاد اسکریپت فارسی قانون
function CreatePersianScript() {
    //FillDaysAndOr();
    CreateCSharpCodeOfRule();
    var conditionexprition = "";
    var firstorderexprition = "";
    var secondorderexprition = "";
    var FirstOprValue = "";
    var SecondOprValue = "";
    var FirstComboConceptName = "";
    var SecondComboConceptName = "";
    var ThirdComboConceptName = "";
    var ForthComboConceptName = "";
    var FifthComboConceptName = "";
    var SixthComboConceptName = "";
    var SeventhComboConceptName = "";
    var EighthComboConceptName = "";
    var NinthComboConceptName = "";
    var TenthComboConceptName = "";
    var EleventhComboConceptName = "";
    var TwelfthComboConceptName = "";
    var FirstConditionComboConceptName = "";
    var SecondConditionComboConceptName = "";
    var LocalResourceObject = JSON.parse(ResourceValues);
    var FirstCheckedItemArrey = GetCheckedItemInFirstCombo();
    if (FirstCheckedItemArrey.length > 0) {
        FirstOprValue = $("#FirstAndOR_RuleGenerator").text();
    }
    var SecondCheckedItemArrey = GetCheckedItemInSecondCombo();
    if (SecondCheckedItemArrey.length > 0) {
        SecondOprValue = $("#SecondAndOr_RuleGenerator").text();
    }
    var ThirdCheckedItemArrey = GetCheckedItemInThirdCombo();
    var ObjectType;
    var OrderHF = GetHiddenFieldByOrder();
    var OrderHFCount = OrderHF.length;
    for (var i = 0; i < OrderHFCount; i++) {

        var hfvalue = OrderHF[i].value;
        var hfvalueObj = JSON.parse(hfvalue);
        var HFType = hfvalueObj[0].Type;
        var HFID = hfvalueObj[0].ID;
        var ConceptHFValue = $("#hfConcept_Concept").val();
        var JsonConceptObj = JSON.parse(ConceptHFValue);
        switch (HFType) {
            case "Condition_1":
            case "Condition_2":
                {
                    var FirstConditionCombo = "";
                    var SecondConditionCombo = "";
                    var OperatorComboValue = "";
                    var AndOrComboValue = "";

                    $.each(hfvalueObj, function (i, key) {

                        if (key.ID === "DropDownAddCondition_td_4_tr_" + HFID) {
                            FirstConditionCombo = key.Value;
                        }
                        if (key.ID === "DropDownAddCondition_td_6_tr_" + HFID) {
                            SecondConditionCombo = key.Value;
                        }
                        if (key.ID === "AndOrcmb_td_5_tr_" + HFID) {
                            OperatorComboValue = key.Value;
                        }
                        if (key.ID == "AndOrcmb_td_1_tr_" + HFID) {
                            AndOrComboValue = key.Value;
                        }
                    })
                    for (var l = 0; l < JsonConceptObj.length; l++) {
                        if (JsonConceptObj[l].DataValueObj.ConceptCode === FirstConditionCombo) {
                            FirstConditionComboConceptName = JsonConceptObj[l].ConceptName;
                            break;
                        }
                        else {
                            FirstConditionComboConceptName = FirstConditionCombo;
                        }
                    }
                    for (var k = 0; k < JsonConceptObj.length; k++) {
                        if (JsonConceptObj[k].DataValueObj.ConceptCode === SecondConditionCombo) {
                            SecondConditionComboConceptName = JsonConceptObj[k].ConceptName;
                            break;
                        }
                        else {
                            SecondConditionComboConceptName = SecondConditionCombo;
                        }
                    }
                    for (var j = 0; j < operations.length; j++) {
                        if (operations[j].ConceptCode === parseInt(OperatorComboValue)) {
                            var OperatorValue = operations[j].Value;
                        }
                    }
                    conditionexprition = conditionexprition + " " + AndOrComboValue + " " + FirstConditionComboConceptName + OperatorValue + SecondConditionComboConceptName;
                    break;
                }
            case "FirstlvlOrder_1":
                {
                    var FirstOrderFirstlvlComboValueByPrimaryKey = "";
                    var SecondOrderFirstlvlComboValueByPrimaryKey = "";
                    var FirstComboConceptName = "";
                    var SecondComboConceptName = "";
                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID === "DropDownAddOrders_td_2_tr_" + HFID) {
                            FirstOrderFirstlvlComboValueByPrimaryKey = key.Value;
                        }
                        if (key.ID === "DropDownAddOrders_td_4_tr_" + HFID) {
                            SecondOrderFirstlvlComboValueByPrimaryKey = key.Value;
                        }
                    })
                    for (var p = 0; p < JsonConceptObj.length; p++) {
                        if (JsonConceptObj[p].DataValueObj.ConceptCode === FirstOrderFirstlvlComboValueByPrimaryKey) {
                            FirstComboConceptName = JsonConceptObj[p].ConceptName;
                            break;
                        }
                        else {
                            FirstComboConceptName = FirstOrderFirstlvlComboValueByPrimaryKey;
                        }
                    }
                    for (var t = 0; t < JsonConceptObj.length; t++) {
                        if (JsonConceptObj[t].DataValueObj.ConceptCode === SecondOrderFirstlvlComboValueByPrimaryKey) {
                            SecondComboConceptName = JsonConceptObj[t].ConceptName;
                            break;
                        }
                        else {
                            SecondComboConceptName = SecondOrderFirstlvlComboValueByPrimaryKey;
                        }
                    }
                    if (firstorderexprition == '') {
                        firstorderexprition = FirstComboConceptName + "=" + SecondComboConceptName;
                    }
                    else {
                        //firstorderexprition = firstorderexprition + " و " + FirstComboConceptName + "=" + SecondComboConceptName;
                        firstorderexprition = firstorderexprition + " " + FirstComboConceptName + "=" + SecondComboConceptName;
                    }
                    break;
                }
            case "FirstlvlOrder_2":
                {
                    var FirstOrderFirstlvlComboValueByOrderKey = "";
                    var SecondOrderFirstlvlComboValueByOrderKey = "";
                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_2_tr_" + HFID) {
                            FirstOrderFirstlvlComboValueByOrderKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_4_tr_" + HFID) {
                            SecondOrderFirstlvlComboValueByOrderKey = key.Value;
                        }
                    })
                    for (var p = 0; p < JsonConceptObj.length; p++) {
                        if (JsonConceptObj[p].DataValueObj.ConceptCode === FirstOrderFirstlvlComboValueByOrderKey) {
                            ThirdComboConceptName = JsonConceptObj[p].ConceptName;
                            break;
                        }
                        else {
                            ThirdComboConceptName = FirstOrderFirstlvlComboValueByOrderKey
                        }
                    }
                    for (var t = 0; t < JsonConceptObj.length; t++) {
                        if (JsonConceptObj[t].DataValueObj.ConceptCode === SecondOrderFirstlvlComboValueByOrderKey) {
                            ForthComboConceptName = JsonConceptObj[t].ConceptName;
                            break;
                        }
                        else {
                            ForthComboConceptName = SecondOrderFirstlvlComboValueByOrderKey;
                        }
                    }

                    //firstorderexprition = firstorderexprition + " و " + ThirdComboConceptName + "=" + ForthComboConceptName;
                    firstorderexprition = firstorderexprition + " " + ThirdComboConceptName + "=" + ForthComboConceptName;

                    break;
                }
            case "FirstlvlOrder_3":
                {
                    var FirstOrderFirstlvlComboValueByConditionKey = "";
                    var SecondOrderFirstlvlComboValueByConditionKey = "";
                    var FirstOrderOperation = "";
                    var AndOrComboText = "";

                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_4_tr_" + HFID) {
                            FirstOrderFirstlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_6_tr_" + HFID) {
                            SecondOrderFirstlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID == "oprcmbFirstOrder_td_6_tr_" + HFID) {
                            FirstOrderOperation = key.Value;
                        }
                        if (key.ID == "AndOrcmbFirOrder_td_1_tr_" + HFID) {
                            AndOrComboText = key.Value;
                        }

                    })
                    for (var p = 0; p < JsonConceptObj.length; p++) {
                        if (JsonConceptObj[p].DataValueObj.ConceptCode === FirstOrderFirstlvlComboValueByConditionKey) {
                            FifthComboConceptName = JsonConceptObj[p].ConceptName;
                            break;
                        }
                        else {
                            FifthComboConceptName = FirstOrderFirstlvlComboValueByConditionKey;
                        }
                    }
                    for (var t = 0; t < JsonConceptObj.length; t++) {
                        if (JsonConceptObj[t].DataValueObj.ConceptCode === SecondOrderFirstlvlComboValueByConditionKey) {
                            SixthComboConceptName = JsonConceptObj[t].ConceptName;
                            break;
                        }
                        else {
                            SixthComboConceptName = SecondOrderFirstlvlComboValueByConditionKey;
                        }
                    }
                    for (var j = 0; j < operations.length; j++) {
                        if (operations[j].ConceptCode === parseInt(FirstOrderOperation)) {
                            var OperatorValue = operations[j].Value;
                        }
                    }
                    firstorderexprition = firstorderexprition + " " + AndOrComboText + " " + LocalResourceObject.ResourceIF + "(" + FifthComboConceptName + OperatorValue + SixthComboConceptName + ")";

                    break;
                }
            case "SecondlvlOrder_1":
                {
                    var FirstOrderSecondlvlComboValueByPrimaryKey = "";
                    var SecondOrderSecondlvlComboValueByPrimaryKey = "";
                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID == "DropDownAddElseOrders_td_2_tr_" + HFID) {
                            FirstOrderSecondlvlComboValueByPrimaryKey = key.Value;
                        }
                        if (key.ID == "DropDownAddElseOrders_td_3_tr_" + HFID) {
                            SecondOrderSecondlvlComboValueByPrimaryKey = key.Value;
                        }
                    })
                    for (var p = 0; p < JsonConceptObj.length; p++) {
                        if (JsonConceptObj[p].DataValueObj.ConceptCode === FirstOrderSecondlvlComboValueByPrimaryKey) {
                            SeventhComboConceptName = JsonConceptObj[p].ConceptName;
                            break;
                        }
                        else {
                            SeventhComboConceptName = FirstOrderSecondlvlComboValueByPrimaryKey;
                        }
                    }
                    for (var t = 0; t < JsonConceptObj.length; t++) {
                        if (JsonConceptObj[t].DataValueObj.ConceptCode === SecondOrderSecondlvlComboValueByPrimaryKey) {
                            EighthComboConceptName = JsonConceptObj[t].ConceptName;
                            break;
                        }
                        else {
                            EighthComboConceptName = SecondOrderSecondlvlComboValueByPrimaryKey;
                        }
                    }
                    if (secondorderexprition == '') {
                        secondorderexprition = SeventhComboConceptName + "=" + EighthComboConceptName;
                    }
                    else {
                        secondorderexprition = secondorderexprition + " و " + SeventhComboConceptName + "=" + EighthComboConceptName;
                    }
                    break;
                }
            case "SecondlvlOrder_2":
                {
                    var FirstOrderSecondlvlComboValueByOrderKey = "";
                    var SecondOrderSecondlvlComboValueByOrderKey = "";
                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID === "DropDownAddTempBySubButtonOrderElse_td_2_tr_" + HFID) {
                            FirstOrderSecondlvlComboValueByOrderKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonOrderElse_td_4_tr_" + HFID) {
                            SecondOrderSecondlvlComboValueByOrderKey = key.Value;
                        }
                    })
                    for (var p = 0; p < JsonConceptObj.length; p++) {
                        if (JsonConceptObj[p].DataValueObj.ConceptCode === FirstOrderSecondlvlComboValueByOrderKey) {
                            NinthComboConceptName = JsonConceptObj[p].ConceptName;
                            break;
                        }
                        else {
                            NinthComboConceptName = FirstOrderSecondlvlComboValueByOrderKey;
                        }
                    }
                    for (var t = 0; t < JsonConceptObj.length; t++) {
                        if (JsonConceptObj[t].DataValueObj.ConceptCode === FirstOrderSecondlvlComboValueByOrderKey) {
                            TenthComboConceptName = JsonConceptObj[t].ConceptName;
                            break;
                        }
                        else {
                            TenthComboConceptName = FirstOrderSecondlvlComboValueByOrderKey;
                        }
                    }

                    //secondorderexprition = secondorderexprition + " و " + NinthComboConceptName + "=" + TenthComboConceptName;
                    secondorderexprition = secondorderexprition + NinthComboConceptName + "=" + TenthComboConceptName;

                    break;
                }
            case "SecondlvlOrder_3":
                {
                    var FirstOrderSecondlvlComboValueByConditionKey = "";
                    var SecondOrderSecondlvlComboValueByConditionKey = "";
                    var OprationComboValue = "";
                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID === "DropDownAddTempBySubButtonConditionElse_td_4_tr_" + HFID) {
                            FirstOrderSecondlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonConditionElse_td_6_tr_" + HFID) {
                            SecondOrderSecondlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID === "oprcmbFirSecstOrder_td_5_tr_" + HFID) {
                            OprationComboValue = key.Value;
                        }
                    })
                    for (var p = 0; p < JsonConceptObj.length; p++) {
                        if (JsonConceptObj[p].DataValueObj.ConceptCode === FirstOrderSecondlvlComboValueByConditionKey) {
                            EleventhComboConceptName = JsonConceptObj[p].ConceptName;
                            break;
                        }
                        else {
                            EleventhComboConceptName = FirstOrderSecondlvlComboValueByConditionKey;
                        }
                    }
                    for (var t = 0; t < JsonConceptObj.length; t++) {
                        if (JsonConceptObj[t].DataValueObj.ConceptCode === SecondOrderSecondlvlComboValueByConditionKey) {
                            TwelfthComboConceptName = JsonConceptObj[t].ConceptName;
                            break;
                        }
                        else {
                            TwelfthComboConceptName = SecondOrderSecondlvlComboValueByConditionKey;
                        }
                    }
                    for (var j = 0; j < operations.length; j++) {
                        if (operations[j].ConceptCode === parseInt(OprationComboValue)) {
                            var OperatorValue = operations[j].Value;
                        }
                    }
                    secondorderexprition = secondorderexprition + " " + LocalResourceObject.ResourceIF + " ( " + EleventhComboConceptName + " " + OperatorValue + " " + TwelfthComboConceptName + " ) ";
                    break;
                }
        }

    }
    if (SecondCheckedItemArrey.length === 0) {
        FirstOprValue = "";
    }
    if (ThirdCheckedItemArrey.length === 0) {
        SecondOprValue = "";
    }
    if (conditionexprition === "") {
        if (secondorderexprition === "") {

            var FinalScript = LocalResourceObject.ResourceIF + " ( " + FirstCheckedItemArrey + " " + FirstOprValue + " " + SecondCheckedItemArrey + " " + SecondOprValue + " " + ThirdCheckedItemArrey + " ) " + LocalResourceObject.ResourceTHEN + " " + firstorderexprition;
        }
        else {
            var FinalScript = LocalResourceObject.ResourceIF + " ( " + FirstCheckedItemArrey + " " + FirstOprValue + " " + SecondCheckedItemArrey + " " + SecondOprValue + " " + ThirdCheckedItemArrey + " ) " + LocalResourceObject.ResourceTHEN + " " + firstorderexprition + " " + LocalResourceObject.ResourceELSE + " " + secondorderexprition;
        }
    }
    else {
        if (secondorderexprition === "") {
            var FinalScript = FirstCheckedItemArrey + " " + FirstOprValue + " " + SecondCheckedItemArrey + " " + SecondOprValue + " " + ThirdCheckedItemArrey + " " + LocalResourceObject.ResourceIF + " ( " + conditionexprition + " ) " + LocalResourceObject.ResourceTHEN + " " + firstorderexprition;
        }
        else {
            var FinalScript = FirstCheckedItemArrey + " " + FirstOprValue + " " + SecondCheckedItemArrey + " " + SecondOprValue + " " + ThirdCheckedItemArrey + " " + LocalResourceObject.ResourceIF + " ( " + conditionexprition + " ) " + LocalResourceObject.ResourceTHEN + " " + firstorderexprition + " " + LocalResourceObject.ResourceELSE + " " + secondorderexprition;
        }
    }
    $("#PersianScriptlblID").text(FinalScript);
    return FinalScript;


}
// جمع آوری فیلدهای حافظه به ترتیب نمایش
function GetHiddenFieldByOrder() {
    var HFList = [];
    $(".HFClass").each(function (i, x) {
        HFList.push(x);
    })
    return HFList;
}
//  ایجاد کد سی شارپ قسمت بالای صفحه
function CreateCSharpCodeOfPreCondition() {

    var PreConditionCsharpCode = "";
    var FirstPart = "";
    var FirstOprator = "";
    var SecondPart = "";
    var SecondOprator = "";
    var ThirthPart = "";
    var DaysAndorhf = $("#hfDaysAndOr").val();
    var JsonConceptObj = JSON.parse(DaysAndorhf);
    var FirstcmbValue = JsonConceptObj[0].Value;
    var SecondcmbValue = JsonConceptObj[1].Value;
    var ThirthcmbValue = JsonConceptObj[2].Value;
    var ForthcmbValue = JsonConceptObj[3].Value;
    var FifthcmbValue = JsonConceptObj[4].Value;
    for (var i = 0; i < FirstcmbValue.length; i++) {
        if (FirstcmbValue[i] === 0) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + " && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Saturday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Saturday";
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = "DayOfWeek.Saturday";
                }
                else {
                    FirstPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Saturday";
                }
            }
        }
        else if (FirstcmbValue[i] === 1) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Sunday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Sunday"
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = " DayOfWeek.Sunday";
                }
                else {
                    FirstPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Sunday"
                }
            }
        }
        else if (FirstcmbValue[i] === 2) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Monday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Monday";
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = "DayOfWeek.Monday";
                }
                else {
                    FirstPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Monday";
                }
            }
        }
        else if (FirstcmbValue[i] === 3) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Tuesday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Tuesday";
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = "DayOfWeek.Tuesday";
                }
                else {
                    FirstPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Tuesday";
                }
            }
        }
        else if (FirstcmbValue[i] === 4) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Wednesday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday";
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = "DayOfWeek.Wednesday";
                }
                else {
                    FirstPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday";
                }
            }
        }
        else if (FirstcmbValue[i] === 5) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Thursday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday";
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = "DayOfWeek.Thursday";
                }
                else {
                    FirstPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday";
                }
            }
        }
        else if (FirstcmbValue[i] === 6) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Friday";
                }
                else {
                    FirstPart = FirstPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday";
                }
            }
            else {
                if (SecondcmbValue == '2') {
                    FirstPart = "DayOfWeek.Friday";
                }
                else {
                    FirstPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday";
                }
            }
        }
        else if (FirstcmbValue[i] === 7) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {

                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";

                }
                else {
                    FirstPart = FirstPart + " || calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";
                }
            }
            else {
                FirstPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";

            }
        }
        else if (FirstcmbValue[i] === 8) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
                }
                else {
                    FirstPart = FirstPart + " || calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
                }
            }
            else {
                FirstPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
        }
        else if (FirstcmbValue[i] === 9) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + " && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.Person.HasCalendar(calculator.RuleCalculateDate, " + '"2"' +").Value < 0 ";
                }
                else {
                    FirstPart = FirstPart + " || calculator.Person.HasCalendar(calculator.RuleCalculateDate, "+ '"2"' + ").Value < 0 ";
                }
            }
            else {
                FirstPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
        }
        else if (FirstcmbValue[i] === 10) {
            if (FirstPart != '') {
                if (SecondcmbValue == '2') {
                    FirstPart = FirstPart + " && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate, " +'"1"' +") ";
                }
                else {
                    FirstPart = FirstPart + " || calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate, " + '"1"' + ") ";
                }
            }
            else {
                FirstPart = "  calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate, " + '"1"' + ") ";
            }
        }

    }
    FirstPart = FirstPart;
    switch (SecondcmbValue) {
        case '0':
            {
                FirstOprator = ' && ';
                break;
            }
        case '1':
            {
                FirstOprator = ' || ';
                break;
            }
        case '2':
            {
                FirstOprator = ' && (calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ';
                break;
            }
    }
    for (var i = 0; i < ThirthcmbValue.length; i++) {
        if (ThirthcmbValue[i] === 0) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Saturday";

                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Saturday";
                }
            }
            else {
                if (ForthcmbValue == '2') {
                    SecondPart = "DayOfWeek.Saturday";
                }
                else {
                    SecondPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Saturday";
                }
            }
        }
        else if (ThirthcmbValue[i] === 1) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Sunday";
                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Sunday"
                }
            }
            else {
                if (ForthcmbValue == '2') {
                    SecondPart = "DayOfWeek.Sunday";
                }
                else {
                    SecondPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Sunday"
                }
            }
        }
        else if (ThirthcmbValue[i] === 2) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Monday";
                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Monday";
                }
            }
            else {
                if (ForthcmbValue == '2') {
                    SecondPart = "DayOfWeek.Monday";
                }
                else {
                    SecondPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Monday";
                }
            }
        }
        else if (ThirthcmbValue[i] === 3) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {

                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Tuesday";
                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Tuesday";
                }
            }
            else {
                if (ForthcmbValue == '2') {
                    SecondPart = "DayOfWeek.Tuesday";
                }
                else {
                    SecondPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Tuesday";

                }
            }
        }
        else if (ThirthcmbValue[i] === 4) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Wednesday";
                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday";
                }
            }
            else {
                if (ForthcmbValue == '2') {
                    SecondPart = "DayOfWeek.Wednesday";
                }
                else {
                    SecondPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday";
                }
            }
        }
        else if (ThirthcmbValue[i] === 5) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {

                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Thursday";
                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday";
                }
            }
            else {
                if (ForthcmbValue == '2') {

                    SecondPart = "DayOfWeek.Thursday";
                }
                else {
                    SecondPart = " calculator.RuleCalculateDate.DayOfWeek = DayOfWeek.Thursday";
                }
            }
        }
        else if (ThirthcmbValue[i] === 6) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == DayOfWeek.Friday";
                }
                else {
                    SecondPart = SecondPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday";
                }
            }
            else {
                if (ForthcmbValue == '2') {
                    SecondPart = "DayOfWeek.Friday";
                }
                else {
                    SecondPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday";
                }
            }
        }
        else if (ThirthcmbValue[i] === 7) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek ==  calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";
                }
                else {
                    SecondPart = SecondPart + " || calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";
                }
            }
            else {
                SecondPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";

            }
        }
        else if (ThirthcmbValue[i] === 8) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
                }
                else {
                    SecondPart = SecondPart + " || calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
                }
            }
            else {
                SecondPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
        }
        else if (ThirthcmbValue[i] === 9) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + "&& calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.Person.HasCalendar(calculator.RuleCalculateDate, "+'"2"'+").Value < 0 ";
                }
                else {
                    SecondPart = SecondPart + "|| calculator.Person.HasCalendar(calculator.RuleCalculateDate, "+'"2"'+").Value < 0 ";
                }
            }
            else {
                SecondPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
        }
        else if (ThirthcmbValue[i] === 10) {
            if (SecondPart != '') {
                if (ForthcmbValue == '2') {
                    SecondPart = SecondPart + " && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate, "+'"1"'+") ";
                }
                else {
                    SecondPart = SecondPart + " || calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate," + "'1'" + ") ";
                }
            }
            else {
                SecondPart = "  calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate," + "'1'" + ") ";
            }
        }

    }
    if (SecondPart != "") {
        SecondPart = " ( " + SecondPart + " )";
    }
    switch (ForthcmbValue) {
        case '0':
            {
                SecondOprator = ' && ';
                break;
            }
        case '1':
            {
                SecondOprator = ' || ';
                break;
            }
        case '2':
            {
                SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ';
                break;
            }
    }
    for (var i = 0; i < FifthcmbValue.length; i++) {
        if (FifthcmbValue[i] === 0) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1) == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Saturday";
                }
                else {
                    ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Saturday";
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1) == ') {
                    ThirthPart = "DayOfWeek.Saturday";
                }
                else {
                    ThirthPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Saturday";
                }
            }
        }
        else if (FifthcmbValue[i] === 1) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1) == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Sunday"

                }
                else {
                    ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Sunday";
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = "DayOfWeek.Sunday";
                }
                else {
                    ThirthPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Sunday";
                }
            }
        }
        else if (FifthcmbValue[i] === 2) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1) == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Monday";
                }
                else {
                    if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                        ThirthPart = ThirthPart + "DayOfWeek.Monday";

                    }
                    else {
                        ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Monday";
                    }
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = "DayOfWeek.Monday";
                }
                else {
                    ThirthPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Monday";
                }
            }
        }
        else if (FifthcmbValue[i] === 3) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Tuesday";
                }
                else {
                    ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Tuesday";
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = "DayOfWeek.Tuesday";
                }
                else {
                    ThirthPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Tuesday";
                }
            }
        }
        else if (FifthcmbValue[i] === 4) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Wednesday";

                }
                else {
                    ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek = DayOfWeek.Wednesday";
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = "DayOfWeek.Wednesday";
                }
                else {
                    ThirthPart = "  calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Wednesday";
                }
            }
        }
        else if (FifthcmbValue[i] === 5) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Thursday";
                }
                else {
                    ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday";
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = "== DayOfWeek.Thursday";
                }
                else {
                    ThirthPart = " calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Thursday";
                }
            }
        }
        else if (FifthcmbValue[i] === 6) {
            if (ThirthPart != '') {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = ThirthPart + "DayOfWeek.Friday";
                }
                else {
                    ThirthPart = ThirthPart + " || calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday";
                }
            }
            else {
                if (SecondOprator = ' && calculator.RuleCalculateDate.AddDays(-1).DayOfWeek == ') {
                    ThirthPart = "DayOfWeek.Friday";
                }
                else {
                    ThirthPart = "calculator.RuleCalculateDate.DayOfWeek == DayOfWeek.Friday";
                }
            }
        }
        else if (FifthcmbValue[i] === 7) {
            if (ThirthPart != '') {

                ThirthPart = ThirthPart + " || calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";
            }
            else {
                ThirthPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value > 0 ";

            }
        }
        else if (FifthcmbValue[i] === 8) {
            if (ThirthPart != '') {
                ThirthPart = ThirthPart + " || calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
            else {
                ThirthPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
        }
        else if (FifthcmbValue[i] === 9) {
            if (ThirthPart != '') {
                ThirthPart = ThirthPart + "  || calculator.Person.HasCalendar(calculator.RuleCalculateDate, 2).Value < 0 ";
            }
            else {
                ThirthPart = " calculator.Person.GetShiftByDate(calculator.RuleCalculateDate).Value < 0 ";
            }
        }
        else if (FifthcmbValue[i] === 10) {
            if (ThirthPart != '') {
                ThirthPart = ThirthPart + " || calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate," + "'1'" + ") ";
            }
            else {
                ThirthPart = "  calculator.EngEnvironment.HasCalendar(calculator.RuleCalculateDate," + "'1'" + ") ";
            }
        }
    }
    if (ThirthPart != "") {
        ThirthPart = "(" + ThirthPart + ")";
    }
    if (ThirthPart == '()') {
        ThirthPart = "";
    }
    if (SecondPart == ' (  ) )') {
        SecondPart = "";
    }
    if (FirstPart == '()') {
        FirstPart = "";
    }
    if (SecondcmbValue == '2' && ForthcmbValue != '2') {
        //FirstPart = FirstPart.replace('calculator.RuleCalculateDate.', '');

        if (ThirthPart == '()') {
            PreConditionCsharpCode = SecondPart + FirstOprator + FirstPart + ")";
        }
        else {
            PreConditionCsharpCode = SecondPart + FirstOprator + FirstPart + ")" + SecondOprator + ThirthPart;
        }
        //var FirstPartArrey = FirstPart.split(',');
        //var FirstPartArreyCount = FirstPartArrey.length;
        //for (var i = 1; i <= FirstPartArreyCount; i++) {
        //    if (ThirthPart == '()') {
        //        //PreConditionCsharpCode = SecondPart + FirstOprator + FirstPart;
        //        PreConditionCsharpCode = PreConditionCsharpCode + SecondPart + FirstOprator + FirstPartArrey[i];
        //    }
        //    else {
        //        PreConditionCsharpCode = PreConditionCsharpCode + SecondPart + FirstOprator + FirstPartArrey[i];

        //        //PreConditionCsharpCode = SecondPart + FirstOprator + FirstPart + SecondOprator + ThirthPart;
        //    }
        //}
    }
    else if (SecondcmbValue != '2' && ForthcmbValue == '2') {
        SecondPart = SecondPart.replace('(calculator.RuleCalculateDate.', '');
        PreConditionCsharpCode = FirstPart + FirstOprator + ThirthPart + SecondOprator + SecondPart;
    }
    else {
        PreConditionCsharpCode = FirstPart + FirstOprator + SecondPart + SecondOprator + ThirthPart;
    }
    return PreConditionCsharpCode;
}
// ایجاد کد سی شارپ قانون
function CreateCSharpCodeOfRule() {

    var PreConditionCsharpCode = CreateCSharpCodeOfPreCondition();
    // ایجاد کد قسمت اصلی قانون

    var ConceptHFValue = $("#hfConcept_Concept").val();
    var JsonConceptObj = JSON.parse(ConceptHFValue);
    var FirstConditionComboConceptName = "";
    var SecondConditionComboConceptName = "";
    var FirstComboConceptName = "";
    var SecondComboConceptName = "";
    var ThirdComboConceptName = "";
    var ForthComboConceptName = "";
    var FifthComboConceptName = "";
    var SixthComboConceptName = "";
    var SeventhComboConceptName = "";
    var EighthComboConceptName = "";
    var NinthComboConceptName = "";
    var TenthComboConceptName = "";
    var EleventhComboConceptName = "";
    var TwelfthComboConceptName = "";
    var ConditionCode = "";
    var FirstOrderCode = "";
    var SecondorderCode = "";
    var OrderHF = GetHiddenFieldByOrder();
    var OrderHFCount = OrderHF.length;
    for (var i = 0; i < OrderHFCount; i++) {

        var hfvalue = OrderHF[i].value;
        var hfvalueObj = JSON.parse(hfvalue);
        var HFType = hfvalueObj[0].Type;
        var HFID = hfvalueObj[0].ID;
        switch (HFType) {
            case "Condition_1":
            case "Condition_2":
                {
                    var FirstConditionCombo = "";
                    var SecondConditionCombo = "";
                    var OperatorComboValue = "";
                    var AndOrComboValue = "";
                    var FirstParenthesisComboValue = "";
                    var SecondParenthesisComboValue = "";
                    var FirstConditionComboText = "";
                    var SecondConditionComboText = "";
                    $.each(hfvalueObj, function (i, key) {
                        FirstConditionComboText = $("#DropDownAddCondition_td_4_tr_" + HFID).text();
                        SecondConditionComboText = $("#DropDownAddCondition_td_6_tr_" + HFID).text();
                        FirstConditionComboText = jQuery.trim(FirstConditionComboText);
                        SecondConditionComboText = jQuery.trim(SecondConditionComboText);
                        FirstConditionComboText = parseInt(FirstConditionComboText);
                        SecondConditionComboText = parseInt(SecondConditionComboText);
                        if (key.ID === "DropDownAddCondition_td_4_tr_" + HFID) {
                            FirstConditionCombo = key.Value;
                        }
                        if (key.ID === "DropDownAddCondition_td_6_tr_" + HFID) {
                            SecondConditionCombo = key.Value;
                        }
                        if (key.ID === "AndOrcmb_td_5_tr_" + HFID) {
                            OperatorComboValue = key.Value;
                        }
                        if (key.ID === "AndOrcmb_td_1_tr_" + HFID) {
                            AndOrComboValue = key.Value;
                        }
                        if (key.ID === "ParenthesisCmb_td_3_tr_" + HFID) {
                            FirstParenthesisComboValue = key.Value;
                        }
                        if (key.ID === "ParenthesisCmb_td_7_tr_" + HFID) {
                            SecondParenthesisComboValue = key.Value;
                        }
                    })
                    FirstConditionCombo = jQuery.trim(FirstConditionCombo);
                    SecondConditionCombo = jQuery.trim(SecondConditionCombo);
                    if ($.isNumeric(FirstConditionComboText)) {
                        FirstConditionComboConceptName = FirstConditionComboText;
                    }
                    else {
                        for (var a = 0; a < JsonConceptObj.length; a++) {
                            if (JsonConceptObj[a].DataValueObj.ConceptCode === parseInt(FirstConditionCombo)) {
                                FirstConditionComboConceptName = " calculator.DoConcept(" + FirstConditionCombo + ").Value ";
                                break;
                            }
                            else if (VariableObjectArrey.length > 0) {
                                for (var aa = 0; aa < VariableObjectArrey.length; aa++) {
                                    if (VariableObjectArrey[aa].variablename === FirstConditionCombo) {
                                        if (VariableObjectArrey[aa].variablefirstparamtype === 2 && VariableObjectArrey[aa].variablesecondparamtype === 2) {
                                            FirstConditionComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[aa].variablefirstparam + ").Value) " + VariableObjectArrey[aa].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[aa].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 2 && VariableObjectArrey[aa].variablesecondparamtype === 1) {
                                            FirstConditionComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[aa].variablefirstparam + ").Value) " + VariableObjectArrey[aa].variablesecondparam + VariableObjectArrey[aa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 1 && VariableObjectArrey[aa].variablesecondparamtype === 2) {
                                            FirstConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[aa].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 1 && VariableObjectArrey[aa].variablesecondparamtype === 1) {
                                            FirstConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + VariableObjectArrey[aa].variablethirdparam;

                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 3 && VariableObjectArrey[aa].variablesecondparamtype === 3) {
                                            FirstConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + VariableObjectArrey[aa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 2 && VariableObjectArrey[aa].variablesecondparamtype === 3) {
                                            FirstConditionComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[aa].variablefirstparam + ").Value) " + VariableObjectArrey[aa].variablesecondparam + VariableObjectArrey[aa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 1 && VariableObjectArrey[aa].variablesecondparamtype === 3) {
                                            FirstConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + +VariableObjectArrey[aa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 3 && VariableObjectArrey[aa].variablesecondparamtype === 2) {
                                            FirstConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[aa].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[aa].variablefirstparamtype === 3 && VariableObjectArrey[aa].variablesecondparamtype === 1) {
                                            FirstConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + VariableObjectArrey[aa].variablethirdparam;
                                            break;
                                        }

                                    }
                                    else {
                                        FirstConditionComboConceptName = "MyRule[" + '"' + FirstConditionCombo + '"' + ", calculator.RuleCalculateDate].ToInt()";

                                    }
                                }
                            }
                            else {

                                FirstConditionComboConceptName = "MyRule[" + '"' + FirstConditionCombo + '"' + ", calculator.RuleCalculateDate].ToInt()";
                            }
                        }
                    }
                    if (FirstConditionCombo === "") {
                        FirstConditionComboConceptName = FirstConditionCombo;
                    }
                    if ($.isNumeric(SecondConditionComboText)) {
                        SecondConditionComboConceptName = SecondConditionComboText;

                    }
                    else {
                        for (var a1 = 0; a1 < JsonConceptObj.length; a1++) {
                            if (JsonConceptObj[a1].DataValueObj.ConceptCode === parseInt(SecondConditionCombo)) {
                                SecondConditionComboConceptName = " calculator.DoConcept(" + SecondConditionCombo + ").Value ";
                                break;
                            }
                            else if (VariableObjectArrey.length > 0) {
                                for (var aaa = 0; aaa < VariableObjectArrey.length; aaa++) {
                                    if (VariableObjectArrey[aaa].variablename === SecondConditionCombo) {
                                        if (VariableObjectArrey[aaa].variablefirstparamtype === 2 && VariableObjectArrey[aaa].variablesecondparamtype === 2) {
                                            SecondConditionComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[aaa].variablefirstparam + ").Value) " + VariableObjectArrey[aaa].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[aaa].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 2 && VariableObjectArrey[aaa].variablesecondparamtype === 1) {
                                            SecondConditionComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[aaa].variablefirstparam + ").Value) " + VariableObjectArrey[aaa].variablesecondparam + VariableObjectArrey[aaa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 1 && VariableObjectArrey[aaa].variablesecondparamtype === 2) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[aaa].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 1 && VariableObjectArrey[aaa].variablesecondparamtype === 1) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aa].variablefirstparam + VariableObjectArrey[aa].variablesecondparam + VariableObjectArrey[aa].variablethirdparam;
                                            break;
                                        }
                                            ////////////////////////////
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 1 && VariableObjectArrey[aaa].variablesecondparamtype === 1) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam + VariableObjectArrey[aaa].variablethirdparam;

                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 3 && VariableObjectArrey[aaa].variablesecondparamtype === 3) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam + VariableObjectArrey[aaa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 2 && VariableObjectArrey[aaa].variablesecondparamtype === 3) {
                                            SecondConditionComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[aaa].variablefirstparam + ").Value) " + VariableObjectArrey[aaa].variablesecondparam + VariableObjectArrey[aaa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 1 && VariableObjectArrey[aaa].variablesecondparamtype === 3) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam + VariableObjectArrey[aaa].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 3 && VariableObjectArrey[aaa].variablesecondparamtype === 2) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[aaa].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[aaa].variablefirstparamtype === 3 && VariableObjectArrey[aaa].variablesecondparamtype === 1) {
                                            SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam + VariableObjectArrey[aaa].variablethirdparam;
                                            break;
                                        }

                                        ///////////////////////////
                                        //else if ($.isNumeric(SecondConditionComboText)) {
                                        //    SecondConditionComboConceptName = SecondConditionComboText;
                                        //    break;
                                        //}
                                        //else
                                        //{
                                        //    SecondConditionComboConceptName = VariableObjectArrey[aaa].variablefirstparam + VariableObjectArrey[aaa].variablesecondparam  + VariableObjectArrey[aaa].variablethirdparam ;
                                        //    break;
                                        //}
                                    }
                                    else {
                                        SecondConditionComboConceptName = "MyRule[" + '"' + SecondConditionCombo + '"' + ", calculator.RuleCalculateDate].ToInt()";

                                    }
                                }
                            }

                            else {
                                SecondConditionComboConceptName = "MyRule[" + '"' + SecondConditionCombo + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }

                        }
                    }
                    if (SecondConditionCombo === "") {
                        SecondConditionComboConceptName = SecondConditionCombo;
                    }
                    var OperatorValue = "";
                    for (var j = 0; j < operations.length; j++) {
                        if (operations[j].ConceptCode === parseInt(OperatorComboValue)) {
                            OperatorValue = operations[j].Value;
                        }
                    }
                    AndOrComboValue = jQuery.trim(AndOrComboValue);
                    if (AndOrComboValue == "و") {
                        AndOrComboValue = " && ";
                    }
                    else if (AndOrComboValue == "یا") {
                        AndOrComboValue = " || ";
                    }
                    else if (AndOrComboValue == "درغیراینصورت") {
                        AndOrComboValue = "else "
                    }
                    else {
                        AndOrComboValue = "";
                    }
                    if (HFID > 1) {
                        //ConditionCode = ConditionCode + AndOrComboValue + FirstConditionComboConceptName + OperatorValue + SecondConditionComboConceptName;
                        ConditionCode = ConditionCode + AndOrComboValue + FirstParenthesisComboValue + FirstConditionComboConceptName + OperatorValue + SecondConditionComboConceptName + SecondParenthesisComboValue;
                    }
                    else {
                        //ConditionCode = ConditionCode + FirstConditionComboConceptName + OperatorValue + SecondConditionComboConceptName;
                        ConditionCode = ConditionCode + FirstParenthesisComboValue + FirstConditionComboConceptName + OperatorValue + SecondConditionComboConceptName + SecondParenthesisComboValue;
                    }
                    break;
                }

            case "FirstlvlOrder_1":
                {
                    var FirstOrderFirstlvlComboValueByPrimaryKey = "";
                    var SecondOrderFirstlvlComboValueByPrimaryKey = "";
                    var FirstOrderFirstlvlComboValueByPrimaryKeyText = "";
                    var SecondOrderFirstlvlComboValueByPrimaryKeyText = "";
                    $.each(hfvalueObj, function (i, key) {
                        FirstOrderFirstlvlComboValueByPrimaryKeyText = $("#DropDownAddOrders_td_2_tr_" + HFID).text();
                        SecondOrderFirstlvlComboValueByPrimaryKeyText = $("#DropDownAddOrders_td_4_tr_" + HFID).text();
                        FirstOrderFirstlvlComboValueByPrimaryKeyText = jQuery.trim(FirstOrderFirstlvlComboValueByPrimaryKeyText);
                        SecondOrderFirstlvlComboValueByPrimaryKeyText = jQuery.trim(SecondOrderFirstlvlComboValueByPrimaryKeyText);
                        FirstOrderFirstlvlComboValueByPrimaryKeyText = parseInt(FirstOrderFirstlvlComboValueByPrimaryKeyText);
                        SecondOrderFirstlvlComboValueByPrimaryKeyText = parseInt(SecondOrderFirstlvlComboValueByPrimaryKeyText);
                        if (key.ID === "DropDownAddOrders_td_2_tr_" + HFID) {
                            FirstOrderFirstlvlComboValueByPrimaryKey = key.Value;
                        }
                        if (key.ID === "DropDownAddOrders_td_4_tr_" + HFID) {
                            SecondOrderFirstlvlComboValueByPrimaryKey = key.Value;
                        }
                    })
                    FirstOrderFirstlvlComboValueByPrimaryKey = jQuery.trim(FirstOrderFirstlvlComboValueByPrimaryKey);
                    SecondOrderFirstlvlComboValueByPrimaryKey = jQuery.trim(SecondOrderFirstlvlComboValueByPrimaryKey);
                    if ($.isNumeric(FirstOrderFirstlvlComboValueByPrimaryKeyText)) {
                        FirstComboConceptName = FirstOrderFirstlvlComboValueByPrimaryKeyText;

                    }
                    else {
                        for (var b = 0; b < JsonConceptObj.length; b++) {
                            if (JsonConceptObj[b].DataValueObj.ConceptCode === parseInt(FirstOrderFirstlvlComboValueByPrimaryKey)) {
                                FirstComboConceptName = " calculator.DoConcept(" + FirstOrderFirstlvlComboValueByPrimaryKey + ").Value ";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var bb = 0; bb < VariableObjectArrey.length; bb++) {
                                    if (VariableObjectArrey[bb].variablename === FirstOrderFirstlvlComboValueByPrimaryKey) {
                                        if (VariableObjectArrey[bb].variablefirstparamtype === 2 && VariableObjectArrey[bb].variablesecondparamtype === 2) {
                                            FirstComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[bb].variablefirstparam + ").Value) " + VariableObjectArrey[bb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bb].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 2 && VariableObjectArrey[bb].variablesecondparamtype === 1) {
                                            FirstComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[bb].variablefirstparam + ").Value) " + VariableObjectArrey[bb].variablesecondparam + VariableObjectArrey[bb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 1 && VariableObjectArrey[bb].variablesecondparamtype === 2) {
                                            FirstComboConceptName = VariableObjectArrey[bb].variablefirstparam + VariableObjectArrey[bb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bb].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            ////////////////////////////////
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 3 && VariableObjectArrey[bb].variablesecondparamtype === 3) {
                                            FirstComboConceptName = VariableObjectArrey[bb].variablefirstparam + VariableObjectArrey[bb].variablesecondparam + VariableObjectArrey[bb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 2 && VariableObjectArrey[bb].variablesecondparamtype === 3) {
                                            FirstComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[bb].variablefirstparam + ").Value) " + VariableObjectArrey[bb].variablesecondparam + VariableObjectArrey[bb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 1 && VariableObjectArrey[bb].variablesecondparamtype === 3) {
                                            FirstComboConceptName = VariableObjectArrey[bb].variablefirstparam + VariableObjectArrey[bb].variablesecondparam + VariableObjectArrey[bb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 3 && VariableObjectArrey[bb].variablesecondparamtype === 2) {
                                            FirstComboConceptName = VariableObjectArrey[bb].variablefirstparam + VariableObjectArrey[bb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bb].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[bb].variablefirstparamtype === 3 && VariableObjectArrey[bb].variablesecondparamtype === 1) {
                                            FirstComboConceptName = VariableObjectArrey[bb].variablefirstparam + VariableObjectArrey[bb].variablesecondparam + VariableObjectArrey[bb].variablethirdparam;
                                            break;
                                        }
                                            ///////////////////////////
                                            //else if ($.isNumeric(FirstOrderFirstlvlComboValueByPrimaryKeyText)) {
                                            //    FirstComboConceptName = FirstOrderFirstlvlComboValueByPrimaryKeyText;
                                            //    break;
                                            //}
                                        else {
                                            FirstComboConceptName = VariableObjectArrey[bb].variablefirstparam + VariableObjectArrey[bb].variablesecondparam + VariableObjectArrey[bb].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {

                                        FirstComboConceptName = "MyRule[" + '"' + FirstOrderFirstlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                                    }
                                }
                            }

                            else {
                                FirstComboConceptName = "MyRule[" + '"' + FirstOrderFirstlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                            }
                        }
                    }
                    if (FirstOrderFirstlvlComboValueByPrimaryKey === "") {
                        FirstComboConceptName = FirstOrderFirstlvlComboValueByPrimaryKey;
                    }
                    if ($.isNumeric(SecondOrderFirstlvlComboValueByPrimaryKeyText)) {
                        SecondComboConceptName = SecondOrderFirstlvlComboValueByPrimaryKeyText;

                    }
                    else {
                        for (var b1 = 0; b1 < JsonConceptObj.length; b1++) {
                            if (JsonConceptObj[b1].DataValueObj.ConceptCode === parseInt(SecondOrderFirstlvlComboValueByPrimaryKey)) {
                                SecondComboConceptName = " calculator.DoConcept(" + SecondOrderFirstlvlComboValueByPrimaryKey + ").Value ";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var bbb = 0; bbb < VariableObjectArrey.length; bbb++) {
                                    if (VariableObjectArrey[bbb].variablename === SecondOrderFirstlvlComboValueByPrimaryKey) {
                                        if (VariableObjectArrey[bbb].variablefirstparamtype === 2 && VariableObjectArrey[bbb].variablesecondparamtype === 2) {
                                            SecondComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablefirstparam + ").Value) " + VariableObjectArrey[bbb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 2 && VariableObjectArrey[bbb].variablesecondparamtype === 1) {
                                            SecondComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablefirstparam + ").Value) " + VariableObjectArrey[bbb].variablesecondparam + VariableObjectArrey[bbb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 1 && VariableObjectArrey[bbb].variablesecondparamtype === 2) {
                                            SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(SecondOrderFirstlvlComboValueByPrimaryKeyText)) {
                                            //    SecondComboConceptName = SecondOrderFirstlvlComboValueByPrimaryKeyText;
                                            //    break;
                                            //}
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 1 && VariableObjectArrey[bbb].variablesecondparamtype === 1) {
                                            SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + VariableObjectArrey[bbb].variablethirdparam;

                                            break;
                                        }
                                            ////////////////
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 3 && VariableObjectArrey[bbb].variablesecondparamtype === 3) {
                                            //SecondComboConceptName = "MyRule[" + '"' + VariableObjectArrey[bbb].variablefirstparam + '"' + ", calculator.RuleCalculateDate].ToInt()" + VariableObjectArrey[bbb].variablesecondparam + "MyRule[" + '"' + VariableObjectArrey[bbb].variablethirdparam + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                            SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + VariableObjectArrey[bbb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 2 && VariableObjectArrey[bbb].variablesecondparamtype === 3) {
                                            SecondComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablefirstparam + ").Value) " + VariableObjectArrey[bbb].variablesecondparam + VariableObjectArrey[bbb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 1 && VariableObjectArrey[bbb].variablesecondparamtype === 3) {
                                            //SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + "MyRule[" + '"' + VariableObjectArrey[bbb].variablethirdparam + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                            SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + VariableObjectArrey[bbb].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 3 && VariableObjectArrey[bbb].variablesecondparamtype === 2) {
                                            //SecondComboConceptName = "MyRule[" + '"' + VariableObjectArrey[bbb].variablefirstparam + '"' + ", calculator.RuleCalculateDate].ToInt()" + VariableObjectArrey[bbb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablethirdparam + ").Value) ";
                                            SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[bbb].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[bbb].variablefirstparamtype === 3 && VariableObjectArrey[bbb].variablesecondparamtype === 1) {
                                            SecondComboConceptName = VariableObjectArrey[bbb].variablefirstparam + VariableObjectArrey[bbb].variablesecondparam + VariableObjectArrey[bbb].variablethirdparam;
                                            break;
                                        }

                                    }
                                    else {
                                        SecondComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }

                            }

                            else {
                                SecondComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }

                        }
                    }
                    if (SecondOrderFirstlvlComboValueByPrimaryKey === "") {
                        SecondComboConceptName = SecondOrderFirstlvlComboValueByPrimaryKey;
                    }
                    var HFType = hfvalueObj[0].Type;
                    var Previousehfvaluejson;
                    if (i === 0) {
                        Previousehfvaluejson = OrderHF[0].value;
                    }
                    else {
                        Previousehfvaluejson = OrderHF[i - 1].value;
                    }
                    var Previousehfvalue = JSON.parse(Previousehfvaluejson);
                    if (Previousehfvalue[0].Type === 'FirstlvlOrder_3') {
                        if (FirstOrderCode === '') {
                            if (FirstComboConceptName !== "" && SecondComboConceptName !== "") {
                                FirstOrderCode = FirstComboConceptName + "=" + SecondComboConceptName + ";}";
                            }

                        }
                        else {
                            if (FirstComboConceptName === "" && SecondComboConceptName === "") {
                                FirstOrderCode = FirstOrderCode;
                            }
                            else {
                                FirstOrderCode = FirstOrderCode + FirstComboConceptName + "=" + SecondComboConceptName + ";}";
                            }
                        }
                    }
                    else {
                        if (FirstOrderCode == '') {
                            if (FirstComboConceptName !== "" && SecondComboConceptName !== "") {
                                FirstOrderCode = FirstComboConceptName + "=" + SecondComboConceptName + ";";
                            }
                        }
                        else {
                            if (FirstComboConceptName === "" && SecondComboConceptName === "") {
                                FirstOrderCode = FirstOrderCode;
                            }
                            else {
                                FirstOrderCode = FirstOrderCode + FirstComboConceptName + "=" + SecondComboConceptName + ";";
                            }
                        }
                    }
                    break;
                }
            case "FirstlvlOrder_2":
                {
                    var FirstOrderFirstlvlComboValueByOrderKey = "";
                    var SecondOrderFirstlvlComboValueByOrderKey = "";
                    var FirstOrderFirstlvlComboValueByOrderKeyText = "";
                    var SecondOrderFirstlvlComboValueByOrderKeyText = "";
                    $.each(hfvalueObj, function (i, key) {
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_2_tr_" + HFID) {
                            FirstOrderFirstlvlComboValueByOrderKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_4_tr_" + HFID) {
                            SecondOrderFirstlvlComboValueByOrderKey = key.Value;
                        }
                    })
                    FirstOrderFirstlvlComboValueByOrderKey = jQuery.trim(FirstOrderFirstlvlComboValueByOrderKey);
                    SecondOrderFirstlvlComboValueByOrderKey = jQuery.trim(SecondOrderFirstlvlComboValueByOrderKey);
                    FirstOrderFirstlvlComboValueByOrderKeyText = $("#DropDownAddTempBySubButtonOrder_td_2_tr_" + HFID).text();
                    SecondOrderFirstlvlComboValueByOrderKeyText = $("#DropDownAddTempBySubButtonOrder_td_4_tr_" + HFID).text();
                    FirstOrderFirstlvlComboValueByOrderKeyText = jQuery.trim(FirstOrderFirstlvlComboValueByOrderKeyText);
                    SecondOrderFirstlvlComboValueByOrderKeyText = jQuery.trim(SecondOrderFirstlvlComboValueByOrderKeyText);
                    FirstOrderFirstlvlComboValueByOrderKeyText = parseInt(FirstOrderFirstlvlComboValueByOrderKeyText);
                    SecondOrderFirstlvlComboValueByOrderKeyText = parseInt(SecondOrderFirstlvlComboValueByOrderKeyText);
                    if ($.isNumeric(FirstOrderFirstlvlComboValueByOrderKeyText)) {
                        ThirdComboConceptName = FirstOrderFirstlvlComboValueByOrderKeyText;
                    }
                    else {
                        for (var c = 0; c < JsonConceptObj.length; c++) {
                            if (JsonConceptObj[c].DataValueObj.ConceptCode === parseInt(FirstOrderFirstlvlComboValueByOrderKey)) {
                                ThirdComboConceptName = " calculator.DoConcept(" + FirstOrderFirstlvlComboValueByOrderKey + ").Value ";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var cc = 0; cc < VariableObjectArrey.length; cc++) {
                                    if (VariableObjectArrey[cc].variablename === FirstOrderFirstlvlComboValueByOrderKey) {
                                        if (VariableObjectArrey[cc].variablefirstparamtype === 2 && VariableObjectArrey[cc].variablesecondparamtype === 2) {
                                            ThirdComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[cc].variablefirstparam + ").Value) " + VariableObjectArrey[cc].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[cc].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 2 && VariableObjectArrey[cc].variablesecondparamtype === 1) {
                                            ThirdComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[cc].variablefirstparam + ").Value) " + VariableObjectArrey[cc].variablesecondparam + VariableObjectArrey[cc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 1 && VariableObjectArrey[cc].variablesecondparamtype === 2) {
                                            ThirdComboConceptName = VariableObjectArrey[cc].variablefirstparam + VariableObjectArrey[cc].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[cc].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(FirstOrderFirstlvlComboValueByOrderKeyText)) {
                                            //    ThirdComboConceptName = FirstOrderFirstlvlComboValueByOrderKeyText;
                                            //}
                                            //////////////////////
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 3 && VariableObjectArrey[cc].variablesecondparamtype === 3) {
                                            ThirdComboConceptName = VariableObjectArrey[cc].variablefirstparam + VariableObjectArrey[cc].variablesecondparam + VariableObjectArrey[cc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 2 && VariableObjectArrey[cc].variablesecondparamtype === 3) {
                                            ThirdComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[cc].variablefirstparam + ").Value) " + VariableObjectArrey[cc].variablesecondparam + VariableObjectArrey[cc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 1 && VariableObjectArrey[cc].variablesecondparamtype === 3) {
                                            ThirdComboConceptName = VariableObjectArrey[cc].variablefirstparam + VariableObjectArrey[cc].variablesecondparam + VariableObjectArrey[cc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 3 && VariableObjectArrey[cc].variablesecondparamtype === 2) {
                                            ThirdComboConceptName = VariableObjectArrey[cc].variablefirstparam + VariableObjectArrey[cc].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[cc].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[cc].variablefirstparamtype === 3 && VariableObjectArrey[cc].variablesecondparamtype === 1) {
                                            ThirdComboConceptName = VariableObjectArrey[cc].variablefirstparam + VariableObjectArrey[cc].variablesecondparam + VariableObjectArrey[cc].variablethirdparam;
                                            break;
                                        }
                                            /////////////////////
                                        else {
                                            ThirdComboConceptName = VariableObjectArrey[cc].variablefirstparam + VariableObjectArrey[cc].variablesecondparam + VariableObjectArrey[cc].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {
                                        ThirdComboConceptName = "MyRule[" + '"' + FirstOrderFirstlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }

                            else {
                                ThirdComboConceptName = "MyRule[" + '"' + FirstOrderFirstlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }
                        }
                    }
                    if (FirstOrderFirstlvlComboValueByOrderKey === "") {
                        ThirdComboConceptName = FirstOrderFirstlvlComboValueByOrderKey;
                    }
                    if ($.isNumeric(SecondOrderFirstlvlComboValueByOrderKeyText)) {
                        ForthComboConceptName = SecondOrderFirstlvlComboValueByOrderKeyText;
                    }
                    else {
                        for (var c1 = 0; c1 < JsonConceptObj.length; c1++) {
                            if (JsonConceptObj[c1].DataValueObj.ConceptCode === parseInt(SecondOrderFirstlvlComboValueByOrderKey)) {
                                ForthComboConceptName = " calculator.DoConcept(" + SecondOrderFirstlvlComboValueByOrderKey + ").Value ";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var ccc = 0; ccc < VariableObjectArrey.length; ccc++) {
                                    if (VariableObjectArrey[ccc].variablename === SecondOrderFirstlvlComboValueByOrderKey) {
                                        if (VariableObjectArrey[ccc].variablefirstparamtype === 2 && VariableObjectArrey[ccc].variablesecondparamtype === 2) {
                                            ForthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ccc].variablefirstparam + ").Value) " + VariableObjectArrey[ccc].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ccc].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 2 && VariableObjectArrey[ccc].variablesecondparamtype === 1) {
                                            ForthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ccc].variablefirstparam + ").Value) " + VariableObjectArrey[ccc].variablesecondparam + VariableObjectArrey[ccc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 1 && VariableObjectArrey[ccc].variablesecondparamtype === 2) {
                                            ForthComboConceptName = VariableObjectArrey[ccc].variablefirstparam + VariableObjectArrey[ccc].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ccc].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(SecondOrderFirstlvlComboValueByOrderKeyText)) {
                                            //    ForthComboConceptName = SecondOrderFirstlvlComboValueByOrderKeyText;
                                            //}
                                            /////////////////////////////////
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 3 && VariableObjectArrey[ccc].variablesecondparamtype === 3) {
                                            ForthComboConceptName = VariableObjectArrey[ccc].variablefirstparam + VariableObjectArrey[ccc].variablesecondparam + VariableObjectArrey[ccc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 2 && VariableObjectArrey[ccc].variablesecondparamtype === 3) {
                                            ForthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ccc].variablefirstparam + ").Value) " + VariableObjectArrey[ccc].variablesecondparam + VariableObjectArrey[ccc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 1 && VariableObjectArrey[ccc].variablesecondparamtype === 3) {
                                            ForthComboConceptName = VariableObjectArrey[ccc].variablefirstparam + VariableObjectArrey[ccc].variablesecondparam + VariableObjectArrey[ccc].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 3 && VariableObjectArrey[ccc].variablesecondparamtype === 2) {
                                            ForthComboConceptName = VariableObjectArrey[ccc].variablefirstparam + VariableObjectArrey[ccc].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ccc].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ccc].variablefirstparamtype === 3 && VariableObjectArrey[ccc].variablesecondparamtype === 1) {
                                            ForthComboConceptName = VariableObjectArrey[ccc].variablefirstparam + VariableObjectArrey[ccc].variablesecondparam + VariableObjectArrey[ccc].variablethirdparam;
                                            break;
                                        }
                                            /////////////////////////////////
                                        else {
                                            ForthComboConceptName = VariableObjectArrey[ccc].variablefirstparam + VariableObjectArrey[ccc].variablesecondparam + VariableObjectArrey[ccc].variablethirdparam;
                                            break;
                                        }
                                    }
                                        //else if (VariableObjectArrey[ccc].variablefirstparamtype === 1 && VariableObjectArrey[ccc].variablesecondparamtype === 1)
                                        //{
                                        //    ForthComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";
                                        //    break;
                                        //}
                                        //else {
                                        //    ForthComboConceptName = SecondOrderFirstlvlComboValueByOrderKey;
                                        //    break;
                                        //}
                                    else {
                                        ForthComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }


                            else {
                                ForthComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }
                        }
                    }
                    if (SecondOrderFirstlvlComboValueByOrderKey === "") {
                        ForthComboConceptName = SecondOrderFirstlvlComboValueByOrderKey;
                    }
                    var HFType = hfvalueObj[0].Type;
                    var Previousehfvaluejson = OrderHF[i - 1].value;
                    var Previousehfvalue = JSON.parse(Previousehfvaluejson);
                    if (Previousehfvalue[0].Type === 'FirstlvlOrder_3') {
                        if (ThirdComboConceptName === "" && ForthComboConceptName === "") {
                            FirstOrderCode = FirstOrderCode;
                        }
                        else {
                            FirstOrderCode = FirstOrderCode + ThirdComboConceptName + "=" + ForthComboConceptName + ";}";

                        }
                    }
                    else {
                        if (ThirdComboConceptName === "" && ForthComboConceptName === "") {
                            FirstOrderCode = FirstOrderCode;
                        }
                        else {
                            FirstOrderCode = FirstOrderCode + ThirdComboConceptName + "=" + ForthComboConceptName + ";";
                        }
                    }
                    break;
                }
            case "FirstlvlOrder_3":
                {
                    var FirstOrderFirstlvlComboValueByConditionKey = "";
                    var SecondOrderFirstlvlComboValueByConditionKey = "";
                    var FirstOrderFirstlvlComboValueByConditionKeyText = "";
                    var SecondOrderFirstlvlComboValueByConditionKeyText = "";
                    var FirstOrderOperation = "";
                    var AndOrComboValue = "";
                    var FirstParenthesiscmb = "";
                    var SecondParenthesiscmb = "";
                    $.each(hfvalueObj, function (i, key) {
                        FirstOrderFirstlvlComboValueByConditionKeyText = $("#DropDownAddTempBySubButtonOrder_td_4_tr_" + HFID).text();
                        SecondOrderFirstlvlComboValueByConditionKeyText = $("#DropDownAddTempBySubButtonOrder_td_6_tr_" + HFID).text();
                        FirstOrderFirstlvlComboValueByConditionKeyText = jQuery.trim(FirstOrderFirstlvlComboValueByConditionKeyText);
                        SecondOrderFirstlvlComboValueByConditionKeyText = jQuery.trim(SecondOrderFirstlvlComboValueByConditionKeyText);
                        FirstOrderFirstlvlComboValueByConditionKeyText = parseInt(FirstOrderFirstlvlComboValueByConditionKeyText);
                        SecondOrderFirstlvlComboValueByConditionKeyText = parseInt(SecondOrderFirstlvlComboValueByConditionKeyText);
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_4_tr_" + HFID) {
                            FirstOrderFirstlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonOrder_td_6_tr_" + HFID) {
                            SecondOrderFirstlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID == "oprcmbFirstOrder_td_6_tr_" + HFID) {
                            FirstOrderOperation = key.Value;
                        }
                        if (key.ID == "AndOrcmbFirOrder_td_1_tr_" + HFID) {
                            AndOrComboValue = key.Value;
                        }
                        if (key.ID == "FirOrderParenthesisCmb_td_3_tr_" + HFID) {
                            FirstParenthesiscmb = key.Value;
                        }
                        if (key.ID === "SecOrderParenthesisCmb_td_7_tr_" + HFID) {
                            SecondParenthesiscmb = key.Value;
                        }
                        for (var j = 0; j < operations.length; j++) {
                            if (operations[j].ConceptCode === parseInt(FirstOrderOperation)) {
                                var OperatorValue = operations[j].Value;
                            }
                        }
                        //AndOrComboText = $("#AndOrcmbFirOrderUL_td_1_tr_" + i)[0].childNodes[1].attributes[0].value;

                    })
                    FirstOrderFirstlvlComboValueByConditionKey = jQuery.trim(FirstOrderFirstlvlComboValueByConditionKey);
                    SecondOrderFirstlvlComboValueByConditionKey = jQuery.trim(SecondOrderFirstlvlComboValueByConditionKey);

                    if ($.isNumeric(FirstOrderFirstlvlComboValueByConditionKeyText)) {
                        FifthComboConceptName = FirstOrderFirstlvlComboValueByConditionKeyText;

                    }
                    else {
                        for (var d = 0; d < JsonConceptObj.length; d++) {
                            if (JsonConceptObj[d].DataValueObj.ConceptCode === parseInt(FirstOrderFirstlvlComboValueByConditionKey)) {
                                FifthComboConceptName = " calculator.DoConcept(" + FirstOrderFirstlvlComboValueByConditionKey + ").Value ";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var dd = 0; dd < VariableObjectArrey.length; dd++) {
                                    if (VariableObjectArrey[dd].variablename === FirstOrderFirstlvlComboValueByConditionKey) {
                                        if (VariableObjectArrey[dd].variablefirstparamtype === 2 && VariableObjectArrey[dd].variablesecondparamtype === 2) {
                                            FifthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[dd].variablefirstparam + ").Value) " + VariableObjectArrey[dd].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[dd].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 2 && VariableObjectArrey[dd].variablesecondparamtype == 1) {
                                            FifthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[dd].variablefirstparam + ").Value) " + VariableObjectArrey[dd].variablesecondparam + VariableObjectArrey[dd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 1 && VariableObjectArrey[dd].variablesecondparamtype === 2) {
                                            FifthComboConceptName = VariableObjectArrey[dd].variablefirstparam + VariableObjectArrey[dd].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[dd].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(FirstOrderFirstlvlComboValueByConditionKeyText)) {
                                            //    FifthComboConceptName = FirstOrderFirstlvlComboValueByConditionKeyText;
                                            //    break;
                                            //}
                                            /////////////////////
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 3 && VariableObjectArrey[dd].variablesecondparamtype === 3) {
                                            FifthComboConceptName = VariableObjectArrey[dd].variablefirstparam + VariableObjectArrey[dd].variablesecondparam + VariableObjectArrey[dd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 2 && VariableObjectArrey[dd].variablesecondparamtype === 3) {
                                            FifthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[dd].variablefirstparam + ").Value) " + VariableObjectArrey[dd].variablesecondparam + VariableObjectArrey[dd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 1 && VariableObjectArrey[dd].variablesecondparamtype === 3) {
                                            FifthComboConceptName = VariableObjectArrey[dd].variablefirstparam + VariableObjectArrey[dd].variablesecondparam + VariableObjectArrey[dd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 3 && VariableObjectArrey[dd].variablesecondparamtype === 2) {
                                            FifthComboConceptName = VariableObjectArrey[dd].variablefirstparam + VariableObjectArrey[dd].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[dd].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[dd].variablefirstparamtype === 3 && VariableObjectArrey[dd].variablesecondparamtype === 1) {
                                            FifthComboConceptName = VariableObjectArrey[dd].variablefirstparam + VariableObjectArrey[dd].variablesecondparam + VariableObjectArrey[dd].variablethirdparam;
                                            break;
                                        }
                                            ////////////////////////
                                        else {
                                            FifthComboConceptName = VariableObjectArrey[dd].variablefirstparam + VariableObjectArrey[dd].variablesecondparam + VariableObjectArrey[dd].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {
                                        FifthComboConceptName = "MyRule[" + '"' + FirstOrderFirstlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }

                            else {
                                FifthComboConceptName = "MyRule[" + '"' + FirstOrderFirstlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }
                        }
                    }
                    if (FirstOrderFirstlvlComboValueByConditionKey === "") {
                        FifthComboConceptName = FirstOrderFirstlvlComboValueByConditionKey;
                    }
                    if ($.isNumeric(SecondOrderFirstlvlComboValueByConditionKeyText)) {
                        SixthComboConceptName = SecondOrderFirstlvlComboValueByConditionKeyText;
                    }
                    else {
                        for (var d1 = 0; d1 < JsonConceptObj.length; d1++) {
                            if (JsonConceptObj[d1].DataValueObj.ConceptCode === parseInt(SecondOrderFirstlvlComboValueByConditionKey)) {
                                SixthComboConceptName = " calculator.DoConcept(" + SecondOrderFirstlvlComboValueByConditionKey + ").Value ";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var ddd = 0; ddd < VariableObjectArrey.length; ddd++) {
                                    if (VariableObjectArrey[ddd].variablename === SecondOrderFirstlvlComboValueByConditionKey) {
                                        if (VariableObjectArrey[ddd].variablefirstparamtype === 2 && VariableObjectArrey[ddd].variablesecondparamtype === 2) {
                                            SixthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ddd].variablefirstparam + ").Value) " + VariableObjectArrey[ddd].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ddd].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 2 && VariableObjectArrey[ddd].variablesecondparamtype === 1) {
                                            SixthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ddd].variablefirstparam + ").Value) " + VariableObjectArrey[ddd].variablesecondparam + VariableObjectArrey[ddd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 1 && VariableObjectArrey[ddd].variablesecondparamtype === 2) {
                                            SixthComboConceptName = VariableObjectArrey[ddd].variablefirstparam + VariableObjectArrey[ddd].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ddd].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(SecondOrderFirstlvlComboValueByConditionKeyText)) {
                                            //    SixthComboConceptName = SecondOrderFirstlvlComboValueByConditionKeyText;
                                            //}
                                            ////////////////////////////
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 3 && VariableObjectArrey[ddd].variablesecondparamtype === 3) {
                                            SixthComboConceptName = VariableObjectArrey[ddd].variablefirstparam + VariableObjectArrey[ddd].variablesecondparam + VariableObjectArrey[ddd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 2 && VariableObjectArrey[ddd].variablesecondparamtype === 3) {
                                            SixthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ddd].variablefirstparam + ").Value) " + VariableObjectArrey[ddd].variablesecondparam + VariableObjectArrey[ddd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 1 && VariableObjectArrey[ddd].variablesecondparamtype === 3) {
                                            SixthComboConceptName = VariableObjectArrey[ddd].variablefirstparam + VariableObjectArrey[ddd].variablesecondparam + VariableObjectArrey[ddd].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 3 && VariableObjectArrey[ddd].variablesecondparamtype === 2) {
                                            SixthComboConceptName = VariableObjectArrey[ddd].variablefirstparam + VariableObjectArrey[ddd].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ddd].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ddd].variablefirstparamtype === 3 && VariableObjectArrey[ddd].variablesecondparamtype === 1) {
                                            SixthComboConceptName = VariableObjectArrey[ddd].variablefirstparam + VariableObjectArrey[ddd].variablesecondparam + VariableObjectArrey[ddd].variablethirdparam;
                                            break;
                                        }
                                            ///////////////////////////
                                        else {
                                            SixthComboConceptName = VariableObjectArrey[ddd].variablefirstparam + VariableObjectArrey[ddd].variablesecondparam + VariableObjectArrey[ddd].variablethirdparam;
                                            break;
                                        }

                                    }
                                    else {
                                        SixthComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }

                            else {
                                SixthComboConceptName = "MyRule[" + '"' + SecondOrderFirstlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }
                        }
                    }
                    if (SecondOrderFirstlvlComboValueByConditionKey === "") {
                        SixthComboConceptName = SecondOrderFirstlvlComboValueByConditionKey;
                    }
                    AndOrComboValue = jQuery.trim(AndOrComboValue);
                    if (AndOrComboValue == "و") {
                        AndOrComboValue = " && ";
                    }
                    else if (AndOrComboValue == "یا") {
                        AndOrComboValue = " || ";
                    }
                    else if (AndOrComboValue == "درغیراینصورت") {
                        AndOrComboValue = "else "
                    }
                    else {
                        AndOrComboValue = "";
                    }
                    var FirstOrderCondition = FirstParenthesiscmb + FifthComboConceptName + OperatorValue + SixthComboConceptName + SecondParenthesiscmb;
                    var Previousehfvaluejson = OrderHF[i - 1].value;
                    var Previousehfvalue = JSON.parse(Previousehfvaluejson);
                    if (Previousehfvalue[0].Type === 'FirstlvlOrder_3') {
                        var IndexOfBraces = FirstOrderCode.indexOf("{");
                        var EditedFirstOrderCode = FirstOrderCode.substring(0, IndexOfBraces);
                        FirstOrderCode = EditedFirstOrderCode + AndOrComboValue + FirstOrderCondition + "{";
                    }
                    else {
                        //FirstOrderCode = FirstOrderCode + " if (" + FifthComboConceptName + OperatorValue + SixthComboConceptName + ") {";
                        FirstOrderCode = FirstOrderCode + " if " + FirstParenthesiscmb + FifthComboConceptName + OperatorValue + SixthComboConceptName + SecondParenthesiscmb + " {";
                    }
                    break;
                }
            case "SecondlvlOrder_1":
                {
                    var FirstOrderSecondlvlComboValueByPrimaryKey = "";
                    var SecondOrderSecondlvlComboValueByPrimaryKey = "";
                    var FirstOrderSecondlvlComboValueByPrimaryKeyText = "";
                    var SecondOrderSecondlvlComboValueByPrimaryKeyText = "";
                    $.each(hfvalueObj, function (i, key) {
                        FirstOrderSecondlvlComboValueByPrimaryKeyText = $("#DropDownAddElseOrders_td_2_tr_" + HFID).text();
                        SecondOrderSecondlvlComboValueByPrimaryKeyText = $("#DropDownAddElseOrders_td_3_tr_" + HFID).text();
                        FirstOrderSecondlvlComboValueByPrimaryKeyText = jQuery.trim(FirstOrderSecondlvlComboValueByPrimaryKeyText);
                        SecondOrderSecondlvlComboValueByPrimaryKeyText = jQuery.trim(SecondOrderSecondlvlComboValueByPrimaryKeyText);
                        FirstOrderSecondlvlComboValueByPrimaryKeyText = parseInt(FirstOrderSecondlvlComboValueByPrimaryKeyText);
                        SecondOrderSecondlvlComboValueByPrimaryKeyText = parseInt(SecondOrderSecondlvlComboValueByPrimaryKeyText);
                        if (key.ID == "DropDownAddElseOrders_td_2_tr_" + HFID) {
                            FirstOrderSecondlvlComboValueByPrimaryKey = key.Value;
                        }
                        if (key.ID == "DropDownAddElseOrders_td_3_tr_" + HFID) {
                            SecondOrderSecondlvlComboValueByPrimaryKey = key.Value;
                        }
                    })
                    FirstOrderSecondlvlComboValueByPrimaryKey = jQuery.trim(FirstOrderSecondlvlComboValueByPrimaryKey);
                    SecondOrderSecondlvlComboValueByPrimaryKey = jQuery.trim(SecondOrderSecondlvlComboValueByPrimaryKey);
                    if ($.isNumeric(FirstOrderSecondlvlComboValueByPrimaryKeyText)) {
                        SeventhComboConceptName = FirstOrderSecondlvlComboValueByPrimaryKeyText;

                    }
                    else {
                        for (var e = 0; e < JsonConceptObj.length; e++) {
                            if (JsonConceptObj[e].DataValueObj.ConceptCode === parseInt(FirstOrderSecondlvlComboValueByPrimaryKey)) {
                                SeventhComboConceptName = " calculator.DoConcept(" + FirstOrderSecondlvlComboValueByPrimaryKey + ").Value";
                                break;
                            }

                            else if (b = VariableObjectArrey.length > 0) {
                                for (var ee = 0; ee < VariableObjectArrey.length; ee++) {
                                    if (VariableObjectArrey[ee].variablename === FirstOrderSecondlvlComboValueByPrimaryKey) {
                                        if (VariableObjectArrey[ee].variablefirstparamtype === 2 && VariableObjectArrey[ee].variablesecondparamtype === 2) {
                                            SeventhComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ee].variablefirstparam + ").Value) " + VariableObjectArrey[ee].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ee].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 2 && VariableObjectArrey[ee].variablesecondparamtype === 1) {
                                            SeventhComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ee].variablefirstparam + ").Value) " + VariableObjectArrey[ee].variablesecondparam + VariableObjectArrey[ee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 1 && VariableObjectArrey[ee].variablesecondparamtype === 2) {
                                            SeventhComboConceptName = VariableObjectArrey[ee].variablefirstparam + VariableObjectArrey[ee].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ee].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(FirstOrderSecondlvlComboValueByPrimaryKeyText)) {
                                            //    SeventhComboConceptName = FirstOrderSecondlvlComboValueByPrimaryKeyText;
                                            //    break;
                                            //}
                                            //////////////////////////

                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 3 && VariableObjectArrey[ee].variablesecondparamtype === 3) {
                                            SeventhComboConceptName = VariableObjectArrey[ee].variablefirstparam + VariableObjectArrey[ee].variablesecondparam + VariableObjectArrey[ee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 2 && VariableObjectArrey[ee].variablesecondparamtype === 3) {
                                            SeventhComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ee].variablefirstparam + ").Value) " + VariableObjectArrey[ee].variablesecondparam + VariableObjectArrey[ee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 1 && VariableObjectArrey[ee].variablesecondparamtype === 3) {
                                            SeventhComboConceptName = VariableObjectArrey[ee].variablefirstparam + VariableObjectArrey[ee].variablesecondparam + VariableObjectArrey[ee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 3 && VariableObjectArrey[ee].variablesecondparamtype === 2) {
                                            SeventhComboConceptName = VariableObjectArrey[ee].variablefirstparam + VariableObjectArrey[ee].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ee].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ee].variablefirstparamtype === 3 && VariableObjectArrey[ee].variablesecondparamtype === 1) {
                                            SeventhComboConceptName = VariableObjectArrey[ee].variablefirstparam + VariableObjectArrey[ee].variablesecondparam + VariableObjectArrey[ee].variablethirdparam;
                                            break;
                                        }
                                            /////////////////////////
                                        else {
                                            SeventhComboConceptName = VariableObjectArrey[ee].variablefirstparam + VariableObjectArrey[ee].variablesecondparam + VariableObjectArrey[ee].variablethirdparam;
                                            break;
                                        }

                                    }
                                    else {
                                        SeventhComboConceptName = "MyRule[" + '"' + FirstOrderSecondlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                                    }
                                }
                            }

                            else {
                                SeventhComboConceptName = "MyRule[" + '"' + FirstOrderSecondlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }
                        }
                    }
                    if (FirstOrderSecondlvlComboValueByPrimaryKey === "") {
                        SeventhComboConceptName = FirstOrderSecondlvlComboValueByPrimaryKey;
                    }
                    if ($.isNumeric(SecondOrderSecondlvlComboValueByPrimaryKeyText)) {
                        EighthComboConceptName = SecondOrderSecondlvlComboValueByPrimaryKeyText;

                    }
                    else {
                        for (var e1 = 0; e1 < JsonConceptObj.length; e1++) {
                            if (JsonConceptObj[e1].DataValueObj.ConceptCode === parseInt(SecondOrderSecondlvlComboValueByPrimaryKey)) {
                                EighthComboConceptName = " calculator.DoConcept(" + SecondOrderSecondlvlComboValueByPrimaryKey + ").Value";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var eee = 0; eee < VariableObjectArrey.length; eee++) {
                                    if (VariableObjectArrey[eee].variablename === SecondOrderSecondlvlComboValueByPrimaryKey) {
                                        if (VariableObjectArrey[eee].variablefirstparamtype === 2 && VariableObjectArrey[eee].variablesecondparamtype === 2) {
                                            EighthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[eee].variablefirstparam + ").Value) " + VariableObjectArrey[eee].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[eee].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 2 && VariableObjectArrey[eee].variablesecondparamtype === 1) {
                                            EighthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[eee].variablefirstparam + ").Value) " + VariableObjectArrey[eee].variablesecondparam + VariableObjectArrey[eee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 1 && VariableObjectArrey[eee].variablesecondparamtype === 2) {
                                            EighthComboConceptName = VariableObjectArrey[eee].variablefirstparam + VariableObjectArrey[eee].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[eee].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(SecondOrderSecondlvlComboValueByPrimaryKeyText)) {
                                            //    EighthComboConceptName = SecondOrderSecondlvlComboValueByPrimaryKeyText;
                                            //    break;
                                            //}
                                            ////////////////////////////
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 3 && VariableObjectArrey[eee].variablesecondparamtype === 3) {
                                            EighthComboConceptName = VariableObjectArrey[eee].variablefirstparam + VariableObjectArrey[eee].variablesecondparam + VariableObjectArrey[eee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 2 && VariableObjectArrey[eee].variablesecondparamtype === 3) {
                                            EighthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[eee].variablefirstparam + ").Value) " + VariableObjectArrey[eee].variablesecondparam + VariableObjectArrey[eee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 1 && VariableObjectArrey[eee].variablesecondparamtype === 3) {
                                            EighthComboConceptName = VariableObjectArrey[eee].variablefirstparam + VariableObjectArrey[eee].variablesecondparam + VariableObjectArrey[eee].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 3 && VariableObjectArrey[eee].variablesecondparamtype === 2) {
                                            EighthComboConceptName = VariableObjectArrey[eee].variablefirstparam + VariableObjectArrey[eee].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[eee].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[eee].variablefirstparamtype === 3 && VariableObjectArrey[eee].variablesecondparamtype === 1) {
                                            EighthComboConceptName = VariableObjectArrey[eee].variablefirstparam + VariableObjectArrey[eee].variablesecondparam + VariableObjectArrey[eee].variablethirdparam;
                                            break;
                                        }
                                            ////////////////////////////
                                        else {
                                            EighthComboConceptName = VariableObjectArrey[eee].variablefirstparam + VariableObjectArrey[eee].variablesecondparam + VariableObjectArrey[eee].variablethirdparam;
                                            break;
                                        }

                                    }
                                    else {
                                        EighthComboConceptName = "MyRule[" + '"' + SecondOrderSecondlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                                    }
                                }
                            }

                            else {
                                EighthComboConceptName = "MyRule[" + '"' + SecondOrderSecondlvlComboValueByPrimaryKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                            }
                        }
                    }
                    if (SecondOrderSecondlvlComboValueByPrimaryKey === "") {
                        EighthComboConceptName = SecondOrderSecondlvlComboValueByPrimaryKey;
                    }
                    var HFType = hfvalueObj[0].Type;
                    var Previousehfvaluejson = OrderHF[HFID - 1].value;
                    var Previousehfvalue = JSON.parse(Previousehfvaluejson);
                    if (Previousehfvalue[0].Type === 'SecondlvlOrder_3') {
                        if (SecondorderCode == '') {
                            if (SeventhComboConceptName !== "" && EighthComboConceptName !== "") {
                                SecondorderCode = SeventhComboConceptName + "=" + EighthComboConceptName + ";}";
                            }
                        }
                        else {
                            if (SeventhComboConceptName === "" && EighthComboConceptName === "") {
                                SecondorderCode = SecondorderCode;
                            }
                            else {
                                SecondorderCode = SecondorderCode + SeventhComboConceptName + "=" + EighthComboConceptName + ";}";
                            }
                        }

                    }
                    else {
                        if (SecondorderCode == '') {
                            if (SeventhComboConceptName !== "" && EighthComboConceptName !== "") {
                                SecondorderCode = SeventhComboConceptName + "=" + EighthComboConceptName + ";";
                            }
                        }
                        else {
                            if (SeventhComboConceptName === "" && EighthComboConceptName === "") {
                                SecondorderCode = SecondorderCode;
                            }
                            else {
                                SecondorderCode = SecondorderCode + SeventhComboConceptName + "=" + EighthComboConceptName + ";";
                            }
                        }
                    }
                    break;
                }
            case "SecondlvlOrder_2":
                {
                    var FirstOrderSecondlvlComboValueByOrderKey = "";
                    var SecondOrderSecondlvlComboValueByOrderKey = "";
                    var FirstOrderSecondlvlComboValueByOrderKeyText = "";
                    var SecondOrderSecondlvlComboValueByOrderKeyText = "";

                    $.each(hfvalueObj, function (i, key) {
                        FirstOrderSecondlvlComboValueByOrderKeyText = $("#DropDownAddTempBySubButtonOrderElse_td_2_tr_" + HFID).text();
                        SecondOrderSecondlvlComboValueByOrderKeyText = $("#DropDownAddTempBySubButtonOrderElse_td_4_tr_" + HFID).text();
                        FirstOrderSecondlvlComboValueByOrderKeyText = jQuery.trim(FirstOrderSecondlvlComboValueByOrderKeyText);
                        SecondOrderSecondlvlComboValueByOrderKeyText = jQuery.trim(SecondOrderSecondlvlComboValueByOrderKeyText);
                        FirstOrderSecondlvlComboValueByOrderKeyText = parseInt(FirstOrderSecondlvlComboValueByOrderKeyText);
                        SecondOrderSecondlvlComboValueByOrderKeyText = parseInt(SecondOrderSecondlvlComboValueByOrderKeyText);
                        if (key.ID === "DropDownAddTempBySubButtonOrderElse_td_2_tr_" + HFID) {
                            FirstOrderSecondlvlComboValueByOrderKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonOrderElse_td_4_tr_" + HFID) {
                            SecondOrderSecondlvlComboValueByOrderKey = key.Value;
                        }
                    })
                    FirstOrderSecondlvlComboValueByOrderKey = jQuery.trim(FirstOrderSecondlvlComboValueByOrderKey);
                    SecondOrderSecondlvlComboValueByOrderKey = jQuery.trim(SecondOrderSecondlvlComboValueByOrderKey);
                    if ($.isNumeric(FirstOrderSecondlvlComboValueByOrderKeyText)) {
                        NinthComboConceptName = FirstOrderSecondlvlComboValueByOrderKeyText;

                    }
                    else {
                        for (var f = 0; f < JsonConceptObj.length; f++) {
                            if (JsonConceptObj[f].DataValueObj.ConceptCode === parseInt(FirstOrderSecondlvlComboValueByOrderKey)) {
                                NinthComboConceptName = " calculator.DoConcept(" + FirstOrderSecondlvlComboValueByOrderKey + ").Value";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var ff = 0; ff < VariableObjectArrey.length; ff++) {
                                    if (VariableObjectArrey[ff].variablename === FirstOrderSecondlvlComboValueByOrderKey) {
                                        if (VariableObjectArrey[ff].variablefirstparamtype === 2 && VariableObjectArrey[ff].variablesecondparamtype === 2) {
                                            NinthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ff].variablefirstparam + ").Value) " + VariableObjectArrey[ff].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ff].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 2 && VariableObjectArrey[ff].variablesecondparamtype === 1) {
                                            NinthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ff].variablefirstparam + ").Value) " + VariableObjectArrey[ff].variablesecondparam + VariableObjectArrey[ff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 1 && VariableObjectArrey[ff].variablesecondparamtype === 2) {
                                            NinthComboConceptName = VariableObjectArrey[ff].variablefirstparam + VariableObjectArrey[ff].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ff].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(FirstOrderSecondlvlComboValueByOrderKeyText)) {
                                            //    NinthComboConceptName = FirstOrderSecondlvlComboValueByOrderKeyText;
                                            //    break;
                                            //}
                                            ///////////////////////////
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 3 && VariableObjectArrey[ff].variablesecondparamtype === 3) {
                                            NinthComboConceptName = VariableObjectArrey[ff].variablefirstparam + VariableObjectArrey[ff].variablesecondparam + VariableObjectArrey[ff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 2 && VariableObjectArrey[ff].variablesecondparamtype === 3) {
                                            NinthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ff].variablefirstparam + ").Value) " + VariableObjectArrey[ff].variablesecondparam + VariableObjectArrey[ff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 1 && VariableObjectArrey[ff].variablesecondparamtype === 3) {
                                            NinthComboConceptName = VariableObjectArrey[ff].variablefirstparam + VariableObjectArrey[ff].variablesecondparam + VariableObjectArrey[ff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 3 && VariableObjectArrey[ff].variablesecondparamtype === 2) {
                                            NinthComboConceptName = VariableObjectArrey[ff].variablefirstparam + VariableObjectArrey[ff].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ff].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ff].variablefirstparamtype === 3 && VariableObjectArrey[ff].variablesecondparamtype === 1) {
                                            NinthComboConceptName = VariableObjectArrey[ff].variablefirstparam + VariableObjectArrey[ff].variablesecondparam + VariableObjectArrey[ff].variablethirdparam;
                                            break;
                                        }
                                            ///////////////////////////
                                        else {
                                            NinthComboConceptName = VariableObjectArrey[ff].variablefirstparam + VariableObjectArrey[ff].variablesecondparam + VariableObjectArrey[ff].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {
                                        NinthComboConceptName = "MyRule[" + '"' + FirstOrderSecondlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }

                            else {
                                NinthComboConceptName = "MyRule[" + '"' + FirstOrderSecondlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                            }
                        }
                    }
                    if (FirstOrderSecondlvlComboValueByOrderKey === "") {
                        NinthComboConceptName = FirstOrderSecondlvlComboValueByOrderKey;
                    }
                    if ($.isNumeric(SecondOrderSecondlvlComboValueByOrderKeyText)) {
                        TenthComboConceptName = SecondOrderSecondlvlComboValueByOrderKeyText;
                    }
                    else {
                        for (var f1 = 0; f1 < JsonConceptObj.length; f1++) {
                            if (JsonConceptObj[f1].DataValueObj.ConceptCode === parseInt(SecondOrderSecondlvlComboValueByOrderKey)) {
                                TenthComboConceptName = "calculator.DoConcept(" + SecondOrderSecondlvlComboValueByOrderKey + ").Value";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var fff = 0; fff < VariableObjectArrey.length; fff++) {
                                    if (VariableObjectArrey[fff].variablename === SecondOrderSecondlvlComboValueByOrderKey) {
                                        if (VariableObjectArrey[fff].variablefirstparamtype === 2 && VariableObjectArrey[fff].variablesecondparamtype === 2) {
                                            TenthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[fff].variablefirstparam + ").Value) " + VariableObjectArrey[fff].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[fff].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 2 && VariableObjectArrey[fff].variablesecondparamtype === 1) {
                                            TenthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[fff].variablefirstparam + ").Value) " + VariableObjectArrey[fff].variablesecondparam + VariableObjectArrey[fff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 1 && VariableObjectArrey[fff].variablesecondparamtype === 2) {
                                            TenthComboConceptName = VariableObjectArrey[fff].variablefirstparam + VariableObjectArrey[fff].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[fff].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //else if ($.isNumeric(SecondOrderSecondlvlComboValueByOrderKeyText)) {
                                            //    TenthComboConceptName = SecondOrderSecondlvlComboValueByOrderKeyText;
                                            //    break;
                                            //}
                                            ////////////////////////
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 3 && VariableObjectArrey[fff].variablesecondparamtype === 3) {
                                            TenthComboConceptName = VariableObjectArrey[fff].variablefirstparam + VariableObjectArrey[fff].variablesecondparam + VariableObjectArrey[fff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 2 && VariableObjectArrey[fff].variablesecondparamtype === 3) {
                                            TenthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[fff].variablefirstparam + ").Value) " + VariableObjectArrey[fff].variablesecondparam + VariableObjectArrey[fff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 1 && VariableObjectArrey[fff].variablesecondparamtype === 3) {
                                            TenthComboConceptName = VariableObjectArrey[fff].variablefirstparam + VariableObjectArrey[fff].variablesecondparam + VariableObjectArrey[fff].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 3 && VariableObjectArrey[fff].variablesecondparamtype === 2) {
                                            TenthComboConceptName = VariableObjectArrey[fff].variablefirstparam + VariableObjectArrey[fff].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[fff].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[fff].variablefirstparamtype === 3 && VariableObjectArrey[fff].variablesecondparamtype === 1) {
                                            TenthComboConceptName = VariableObjectArrey[fff].variablefirstparam + VariableObjectArrey[fff].variablesecondparam + VariableObjectArrey[fff].variablethirdparam;
                                            break;
                                        }
                                            ////////////////////////
                                        else {
                                            TenthComboConceptName = VariableObjectArrey[fff].variablefirstparam + VariableObjectArrey[fff].variablesecondparam + VariableObjectArrey[fff].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {
                                        TenthComboConceptName = "MyRule[" + '"' + SecondOrderSecondlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }

                            else {
                                TenthComboConceptName = "MyRule[" + '"' + SecondOrderSecondlvlComboValueByOrderKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                            }
                        }
                    }
                    if (SecondOrderSecondlvlComboValueByOrderKey === "") {
                        TenthComboConceptName = SecondOrderSecondlvlComboValueByOrderKey;
                    }
                    var HFType = hfvalueObj[0].Type;
                    var Previousehfvaluejson = OrderHF[HFID - 1].value;
                    var Previousehfvalue = JSON.parse(Previousehfvaluejson);
                    if (Previousehfvalue[0].Type === 'SecondlvlOrder_3') {
                        if (NinthComboConceptName === "" && TenthComboConceptName === "") {
                            SecondorderCode = SecondorderCode;
                        }
                        else {
                            SecondorderCode = SecondorderCode + NinthComboConceptName + "=" + TenthComboConceptName + ";}";
                        }

                    }
                    else {
                        if (NinthComboConceptName === "" && TenthComboConceptName === "") {
                            SecondorderCode = SecondorderCode;
                        }
                        else {
                            SecondorderCode = SecondorderCode + NinthComboConceptName + "=" + TenthComboConceptName + ";";
                        }

                    }
                    break;
                }
            case "SecondlvlOrder_3":
                {
                    var FirstOrderSecondlvlComboValueByConditionKey = "";
                    var SecondOrderSecondlvlComboValueByConditionKey = "";
                    var FirstOrderSecondlvlComboValueByConditionKeyText = "";
                    var SecondOrderSecondlvlComboValueByConditionKeyText = "";

                    var OprationComboValue = "";
                    var AndOrComboValue = "";
                    $.each(hfvalueObj, function (i, key) {
                        FirstOrderSecondlvlComboValueByConditionKeyText = $("#DropDownAddTempBySubButtonConditionElse_td_4_tr_" + HFID).text();
                        SecondOrderSecondlvlComboValueByConditionKeyText = $("#DropDownAddTempBySubButtonConditionElse_td_6_tr_" + HFID).text();
                        FirstOrderSecondlvlComboValueByConditionKeyText = jQuery.trim(FirstOrderSecondlvlComboValueByConditionKeyText);
                        SecondOrderSecondlvlComboValueByConditionKeyText = jQuery.trim(SecondOrderSecondlvlComboValueByConditionKeyText);
                        FirstOrderSecondlvlComboValueByConditionKeyText = parseInt(FirstOrderSecondlvlComboValueByConditionKeyText);
                        SecondOrderSecondlvlComboValueByConditionKeyText = parseInt(SecondOrderSecondlvlComboValueByConditionKeyText);
                        if (key.ID === "DropDownAddTempBySubButtonConditionElse_td_4_tr_" + HFID) {
                            FirstOrderSecondlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID === "DropDownAddTempBySubButtonConditionElse_td_6_tr_" + HFID) {
                            SecondOrderSecondlvlComboValueByConditionKey = key.Value;
                        }
                        if (key.ID === "oprcmbFirSecstOrder_td_5_tr_" + HFID) {
                            OprationComboValue = key.Value;
                        }
                        if (key.ID == "AndOrcmbSecOrder_td_1_tr_" + HFID) {
                            AndOrComboValue = key.Value;
                        }
                    })
                    FirstOrderSecondlvlComboValueByConditionKey = jQuery.trim(FirstOrderSecondlvlComboValueByConditionKey);
                    SecondOrderSecondlvlComboValueByConditionKey = jQuery.trim(SecondOrderSecondlvlComboValueByConditionKey);
                    if ($.isNumeric(FirstOrderSecondlvlComboValueByConditionKeyText)) {
                        EleventhComboConceptName = FirstOrderSecondlvlComboValueByConditionKeyText;
                    }
                    else {
                        for (var g = 0; g < JsonConceptObj.length; g++) {
                            if (JsonConceptObj[g].DataValueObj.ConceptCode === parseInt(FirstOrderSecondlvlComboValueByConditionKey)) {
                                EleventhComboConceptName = " calculator.DoConcept(" + FirstOrderSecondlvlComboValueByConditionKey + ").Value";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var gg = 0; gg < VariableObjectArrey.length; gg++) {
                                    if (VariableObjectArrey[gg].variablename === parseInt(FirstOrderSecondlvlComboValueByConditionKey)) {
                                        if (VariableObjectArrey[gg].variablefirstparamtype === 2 && VariableObjectArrey[gg].variablesecondparamtype === 2) {
                                            EleventhComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[gg].variablefirstparam + ").Value) " + VariableObjectArrey[gg].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[gg].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 2 && VariableObjectArrey[gg].variablesecondparamtype === 1) {
                                            EleventhComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[gg].variablefirstparam + ").Value) " + VariableObjectArrey[gg].variablesecondparam + VariableObjectArrey[gg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 1 && VariableObjectArrey[gg].variablesecondparamtype === 2) {
                                            EleventhComboConceptName = VariableObjectArrey[gg].variablefirstparam + VariableObjectArrey[gg].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[gg].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            /////////////////////
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 3 && VariableObjectArrey[gg].variablesecondparamtype === 3) {
                                            EleventhComboConceptName = VariableObjectArrey[gg].variablefirstparam + VariableObjectArrey[gg].variablesecondparam + VariableObjectArrey[gg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 2 && VariableObjectArrey[gg].variablesecondparamtype === 3) {
                                            EleventhComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[gg].variablefirstparam + ").Value) " + VariableObjectArrey[gg].variablesecondparam + VariableObjectArrey[gg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 1 && VariableObjectArrey[gg].variablesecondparamtype === 3) {
                                            EleventhComboConceptName = VariableObjectArrey[gg].variablefirstparam + VariableObjectArrey[gg].variablesecondparam + VariableObjectArrey[gg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 3 && VariableObjectArrey[gg].variablesecondparamtype === 2) {
                                            EleventhComboConceptName = VariableObjectArrey[gg].variablefirstparam + VariableObjectArrey[gg].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[gg].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[gg].variablefirstparamtype === 3 && VariableObjectArrey[gg].variablesecondparamtype === 1) {
                                            EleventhComboConceptName = VariableObjectArrey[gg].variablefirstparam + VariableObjectArrey[gg].variablesecondparam + VariableObjectArrey[gg].variablethirdparam;
                                            break;
                                        }
                                            /////////////////////
                                        else {
                                            EleventhComboConceptName = VariableObjectArrey[gg].variablefirstparam + VariableObjectArrey[gg].variablesecondparam + VariableObjectArrey[gg].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {
                                        EleventhComboConceptName = "MyRule[" + '"' + FirstOrderSecondlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }
                                //else if ($.isNumeric(FirstOrderSecondlvlComboValueByConditionKeyText))
                                //{
                                //    EleventhComboConceptName = FirstOrderSecondlvlComboValueByConditionKeyText;
                                //    break;
                                //}
                            else {
                                EleventhComboConceptName = "MyRule[" + '"' + FirstOrderSecondlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                            }
                        }
                    }
                    if (FirstOrderSecondlvlComboValueByConditionKey === "") {
                        EleventhComboConceptName = FirstOrderSecondlvlComboValueByConditionKey;
                    }
                    if ($.isNumeric(SecondOrderSecondlvlComboValueByConditionKeyText)) {
                        TwelfthComboConceptName = SecondOrderSecondlvlComboValueByConditionKeyText;

                    }
                    else {
                        for (var g1 = 0; g1 < JsonConceptObj.length; g1++) {
                            if (JsonConceptObj[g1].DataValueObj.ConceptCode === parseInt(SecondOrderSecondlvlComboValueByConditionKey)) {
                                TwelfthComboConceptName = " calculator.DoConcept(" + SecondOrderSecondlvlComboValueByConditionKey + ").Value";
                                break;
                            }

                            else if (VariableObjectArrey.length > 0) {
                                for (var ggg = 0; ggg < VariableObjectArrey.length; ggg++) {
                                    if (VariableObjectArrey[ggg].variablename === parseInt(SecondOrderSecondlvlComboValueByConditionKey)) {
                                        if (VariableObjectArrey[ggg].variablefirstparamtype === 2 && VariableObjectArrey[ggg].variablesecondparamtype === 2) {
                                            TwelfthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ggg].variablefirstparam + ").Value) " + VariableObjectArrey[ggg].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ggg].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 2 && VariableObjectArrey[ggg].variablesecondparamtype === 1) {
                                            TwelfthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ggg].variablefirstparam + ").Value) " + VariableObjectArrey[ggg].variablesecondparam + VariableObjectArrey[ggg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 1 && VariableObjectArrey[ggg].variablesecondparamtype === 2) {
                                            TwelfthComboConceptName = VariableObjectArrey[ggg].variablefirstparam + VariableObjectArrey[ggg].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ggg].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                            //////////////////////
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 3 && VariableObjectArrey[ggg].variablesecondparamtype === 3) {
                                            TwelfthComboConceptName = VariableObjectArrey[ggg].variablefirstparam + VariableObjectArrey[ggg].variablesecondparam + VariableObjectArrey[ggg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 2 && VariableObjectArrey[ggg].variablesecondparamtype === 3) {
                                            TwelfthComboConceptName = " (calculator.DoConcept(" + VariableObjectArrey[ggg].variablefirstparam + ").Value) " + VariableObjectArrey[ggg].variablesecondparam + VariableObjectArrey[ggg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 1 && VariableObjectArrey[ggg].variablesecondparamtype === 3) {
                                            TwelfthComboConceptName = VariableObjectArrey[ggg].variablefirstparam + VariableObjectArrey[ggg].variablesecondparam + VariableObjectArrey[ggg].variablethirdparam;
                                            break;
                                        }
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 3 && VariableObjectArrey[ggg].variablesecondparamtype === 2) {
                                            TwelfthComboConceptName = VariableObjectArrey[ggg].variablefirstparam + VariableObjectArrey[ggg].variablesecondparam + " (calculator.DoConcept(" + VariableObjectArrey[ggg].variablethirdparam + ").Value) ";
                                            break;
                                        }
                                        else if (VariableObjectArrey[ggg].variablefirstparamtype === 3 && VariableObjectArrey[ggg].variablesecondparamtype === 1) {
                                            TwelfthComboConceptName = VariableObjectArrey[ggg].variablefirstparam + VariableObjectArrey[ggg].variablesecondparam + VariableObjectArrey[ggg].variablethirdparam;
                                            break;
                                        }
                                            //////////////////////
                                            //else if ($.isNumeric(SecondOrderSecondlvlComboValueByConditionKeyText)) {
                                            //    TwelfthComboConceptName = SecondOrderSecondlvlComboValueByConditionKeyText;
                                            //    break;
                                            //}
                                        else {
                                            TwelfthComboConceptName = VariableObjectArrey[ggg].variablefirstparam + VariableObjectArrey[ggg].variablesecondparam + VariableObjectArrey[ggg].variablethirdparam;
                                            break;
                                        }
                                    }
                                    else {
                                        TwelfthComboConceptName = "MyRule[" + '"' + SecondOrderSecondlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";


                                    }
                                }
                            }

                            else {
                                TwelfthComboConceptName = "MyRule[" + '"' + SecondOrderSecondlvlComboValueByConditionKey + '"' + ", calculator.RuleCalculateDate].ToInt()";

                            }
                        }
                    }
                    if (SecondOrderSecondlvlComboValueByConditionKey === "") {
                        TwelfthComboConceptName = SecondOrderSecondlvlComboValueByConditionKey;
                    }
                    AndOrComboValue = jQuery.trim(AndOrComboValue);
                    if (AndOrComboValue == "و") {
                        AndOrComboValue = " && ";
                    }
                    else if (AndOrComboValue == "یا") {
                        AndOrComboValue = " || ";
                    }
                    else if (AndOrComboValue == "درغیراینصورت") {
                        AndOrComboValue = "else "
                    }
                    else {
                        AndOrComboValue = "";
                    }
                    SecondorderCode = SecondorderCode + "if (" + EleventhComboConceptName + OprationComboValue + TwelfthComboConceptName + ") {";
                    break;
                }
        }

    }
    var finalCShrapCode;
   
    if (PreConditionCsharpCode == "") {
        if (SecondorderCode == "") {

            //finalCShrapCode = "if (" + ConditionCode + "){" + FirstOrderCode + "}";
            finalCShrapCode = "if " + ConditionCode + "{" + FirstOrderCode + "}";

        }
        else {

            //finalCShrapCode = "if (" + ConditionCode + "){" + FirstOrderCode + "}" + "else {" + SecondorderCode + "}";
            finalCShrapCode = "if " + ConditionCode + "{" + FirstOrderCode + "}" + "else {" + SecondorderCode + "}";

        }

    }
    else {
        if (SecondorderCode == "") {
            if (ConditionCode == "") {
                finalCShrapCode = "if (" + PreConditionCsharpCode + ")" + "{" + FirstOrderCode + "}";
            }
            else {
                finalCShrapCode = "if (" + PreConditionCsharpCode + ") {" + "if " + ConditionCode + "{" + FirstOrderCode + "} }";
            }
        }
        else {
            if (ConditionCode == "") {
                //finalCShrapCode = "if (" + PreConditionCsharpCode + ") {" + "{" + FirstOrderCode + "}" + "else {" + SecondorderCode + "} }";
                finalCShrapCode = "if (" + PreConditionCsharpCode + ") {"  + FirstOrderCode + "}" + "else {" + SecondorderCode + "}";
            }
            else {
                //finalCShrapCode = "if (" + PreConditionCsharpCode + ") {" + "if (" + ConditionCode + "){" + FirstOrderCode + "}" + "else {" + SecondorderCode + "} }";
                finalCShrapCode = "if (" + PreConditionCsharpCode + ") {" + "if " + ConditionCode + "{" + FirstOrderCode + "}" + "else {" + SecondorderCode + "} }";
            }
        }
    }
    var Resualt = CheckValidateInGeneratingCode(PreConditionCsharpCode, ConditionCode, FirstOrderCode, SecondorderCode);
    if (Resualt === 0) {
        return 0;
    }
    else {
        $("#testlbl2").text(finalCShrapCode);
        return finalCShrapCode;
    }
}
// ایجاد آرایه ای از آبجکت ها از تمام فیلدهای حافظه
function CreateJsonobjectOfHiddenFields() {

    AllHiddenFieldValues = [];
    var DaysAndorhf = $("#hfDaysAndOr").val();
    var a = { key: 1, Value: DaysAndorhf };
    var DaysAndorJsonConceptObj = JSON.stringify(a);
    AllHiddenFieldValues.push(DaysAndorJsonConceptObj);

    for (var i = 1; i <= num; i++) {
        var HiddenFieldValue = $("#Conditionhf_" + i).val();
        var c = { Key: i, Value: HiddenFieldValue };
        var JsonConceptObj = JSON.stringify(c);
        AllHiddenFieldValues.push(JsonConceptObj);

    }
    for (var i = 1; i <= ThenOrderCounter; i++) {
        var HiddenFieldValue = $("#FirstOrderhf_" + i).val();
        var c = { Key: i, Value: HiddenFieldValue };
        var JsonConceptObj = JSON.stringify(c);
        AllHiddenFieldValues.push(JsonConceptObj);
    }
    for (var i = 1; i <= ElseOrderCounter; i++) {
        var HiddenFieldValue = $("#SecondOrderhf_" + i).val();
        var c = { Key: i, Value: HiddenFieldValue };
        var JsonConceptObj = JSON.stringify(c);
        AllHiddenFieldValues.push(JsonConceptObj);
    }
    //  return AllHiddenFieldValues;
}
// ویرایش قانون
function GetRulePropertyInEditMode() {
    var DialogRuleGeneratorValue = parent.DialogRuleGenerator.get_value();
    var EdittingRuleStateObject = DialogRuleGeneratorValue.RuleStateJsonObject;
    EditRule(EdittingRuleStateObject);
}
// ویرایش قانون
function EditRule(EdittingRuleStateObject) {
    var RuleStateJsonObject = [];
    var RuleStateValueJsonObject = [];
    try
    {
        var JsonRuleStateObject = JSON.parse(EdittingRuleStateObject);
        var JsonRuleStateObjectLenght = JsonRuleStateObject.length;
    }
    catch (ex)
    {
        var ErrorMessage = $("#hfOperationError").val();
        $("#tlbRuleName_TlbRuleGenerator").text(ErrorMessage);
        $("#tlbRuleName_TlbRuleGenerator").css('color', 'red');
        $("#AddCon").css('visibility', 'hidden');
        $("#AddOrders").css('visibility', 'hidden');
        $("#AddElseOrder").css('visibility', 'hidden');
    }
    for (var i = 0; i < JsonRuleStateObjectLenght ; i++) {
        RuleStateJsonObject[i] = $.parseJSON(JsonRuleStateObject[i]);
        var StringRuleStateObject = RuleStateJsonObject[i].Value;
        RuleStateValueJsonObject[i] = $.parseJSON(StringRuleStateObject);
        ArrengeRules(RuleStateValueJsonObject[i], StringRuleStateObject, RuleStateValueJsonObject[i - 1]);
    }

}
//ویرایش قانون
function ArrengeRules(RuleStateValueJsonObject, StringRuleStateObject, PreviousRuleStateValueJsonObject) {
    try
    {
        var RuleStateValueJsonObjectLenght = RuleStateValueJsonObject.length;
        var RowCount = RuleStateValueJsonObject[0].ID;
        for (var i = 1; i <= RuleStateValueJsonObjectLenght; i++) {
            var RuleStateObjectType = RuleStateValueJsonObject[0].Type;
            var ConceptHFValue = $("#hfConcept_Concept").val();
            var JsonConceptObj = JSON.parse(ConceptHFValue);
            operations = [{ ID: 1, ConceptCode: 1, Value: ">" }, { ID: 2, ConceptCode: 2, Value: "<" }, { ID: 3, ConceptCode: 3, Value: ">=" }, { ID: 4, ConceptCode: 4, Value: "<=" }, { ID: 5, ConceptCode: 5, Value: "==" }, { ID: 6, ConceptCode: 6, Value: "<>" }];
            var operationstringify = JSON.stringify(operations);
            var JsonObj = JSON.parse(operationstringify);
            switch (RuleStateObjectType) {
                case "Dayscmb":
                    var FirstObjValues = RuleStateValueJsonObject[0].Value;
                    var FirstCheckedItemsCount = FirstObjValues.length;
                    if (FirstObjValues != "") {
                        //$("#WorkDaysFirstCombo_RuleGenerator").text("");
                        for (var j = 0; j < FirstCheckedItemsCount; j++) {
                            $("#Firstcmb")[0].children[RuleStateValueJsonObject[0].Value[j]].children[0].children[0].setAttribute('checked', 'checked');
                            //if (FirstCheckedItemsCount <= 2) {
                            //    $("#WorkDaysFirstCombo_RuleGenerator").append(RuleStateValueJsonObject[0].Value[j]);
                            //}
                        }

                   
                    
                        if (FirstCheckedItemsCount > 0) {
                            $("#WorkDaysFirstCombo_RuleGenerator").text(FirstCheckedItemsCount + " " + "آیتم انتخاب شدند");
                        }
                    }
                    var firstandor = RuleStateValueJsonObject[1].Value;
                    var secondandor = RuleStateValueJsonObject[3].Value;
                    $("#FirstAndOR_RuleGenerator").val(firstandor);
                    $("#SecondAndOr_RuleGenerator").val(secondandor);
                    switch (firstandor) {
                        case "0":
                            $("#FirstAndOR_RuleGenerator").text('و');
                            break;
                        case "1":
                            $("#FirstAndOR_RuleGenerator").text('یا');
                            break;
                        case "2":
                            $("#FirstAndOR_RuleGenerator").text('قبل');
                            break;
                    }
                    switch (secondandor) {
                        case "0":
                            $("#SecondAndOr_RuleGenerator").text('و');
                            break;
                        case "1":
                            $("#SecondAndOr_RuleGenerator").text('یا');
                            break;
                        case "2":
                            $("#SecondAndOr_RuleGenerator").text('قبل');
                            break;
                    }
                    var SecondObjValues = RuleStateValueJsonObject[2].Value;
                    var SecondCheckedItemsCount = SecondObjValues.length;
                    if (SecondObjValues != "")
                    {
                        //$("#WorkDaysSecondCombo_RuleGenerator").text("");
                        for (var k = 0; k < SecondCheckedItemsCount; k++) {
                            $("#Secondcmb")[0].children[RuleStateValueJsonObject[2].Value[k]].children[0].children[0].setAttribute('checked', 'checked');
                            //if (FirstCheckedItemsCount <= 2) 
                            //   {
                            //       $("#WorkDaysSecondCombo_RuleGenerator").text(SecondObjValues);
                            //   }
                        }
                        if (SecondCheckedItemsCount > 0) {
                            $("#WorkDaysSecondCombo_RuleGenerator").text(SecondCheckedItemsCount + " " + "آیتم انتخاب شدند");
                        }
                    }
               
               
                    var ThirdObjValues = RuleStateValueJsonObject[4].Value;
                    var ThirdCheckedItemsCount = ThirdObjValues.length;
                    if (ThirdObjValues != "")
                    {
                        //$("#WorkDaysThirdCombo_RuleGenerator").text("");
                        for (var c = 0; c < length; c++) {
                            $("#Thirdcmb")[0].children[RuleStateValueJsonObject[4].Value[c]].children[0].children[0].setAttribute('checked', 'checked');
                            //if (FirstCheckedItemsCount <= 2) {
                            //       $("#WorkDaysThirdCombo_RuleGenerator").text(SecondObjValues);
                            //   }
                        }
                        if (ThirdCheckedItemsCount > 0) {
                            $("#WorkDaysThirdCombo_RuleGenerator").text(ThirdCheckedItemsCount + " " + "آیتم انتخاب شدند");
                        }
                    }
               
               
                    $("#hfDaysAndOr").val(StringRuleStateObject);
                    break;
                case "Condition_1":
                case "Condition_2":
                    var first = RuleStateValueJsonObject[5].Value;
                    var second = RuleStateValueJsonObject[7].Value;
                    if (RuleStateObjectType === "Condition_2") {
                        //var second = RuleStateValueJsonObject[6].Value;
                        second = RuleStateValueJsonObject[8].Value;
                    }
                    var Third = RuleStateValueJsonObject[9].Value;
                    if (RuleStateObjectType === "Condition_2") {
                        //var Third = RuleStateValueJsonObject[8].Value;
                        Third = RuleStateValueJsonObject[10].Value;
                    }

                    var AndOr = RuleStateValueJsonObject[1].Value;
                    var FirstParenthesis = RuleStateValueJsonObject[3];
                    if (RuleStateObjectType === "Condition_2") {
                        var SecondParenthesis = RuleStateValueJsonObject[13];
                    }
                    else {
                        var SecondParenthesis = RuleStateValueJsonObject[12];
                    }
                    var firstRuleStateValueJsonObjectConceptName = first;
                    var secondRuleStateValueJsonObjectConceptName = second;
                    var thirdRuleStateValueJsonObjectConceptName = Third;
                    if (AndOr == "") {
                        AndOr = "و";
                    }
                    //Third = jQuery.trim(Third);
                    //Third = parseInt(Third);
                    for (var j = 0; j < JsonConceptObj.length; j++) {
                        if (firstRuleStateValueJsonObjectConceptName.constructor === String) {
                            firstRuleStateValueJsonObjectConceptName = firstRuleStateValueJsonObjectConceptName;
                        }
                        else {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(first)) {
                                firstRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;
                            }
                        }
                        //secondRuleStateValueJsonObjectConceptName =second;
                        if (thirdRuleStateValueJsonObjectConceptName.constructor !== String) {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(Third)) {
                                thirdRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;
                            }
                        }
                    }
                    for (var k = 0; k < JsonObj.length; k++) {
                        if (JsonObj[k].ConceptCode === second) {
                            secondRuleStateValueJsonObjectConceptName = JsonObj[k].Value;
                            break;
                        }
                    }
                    var EdittingPrimaryContr = "";
                    if (RowCount === 1) {
                        EdittingPrimaryContr = $("<tr id= 'Conditiontr_" + RowCount + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + RowCount + "'><tr id='Conditiontr_1_table_" + RowCount + "'><td id='td_2_tr_" + RowCount + "'>اگر</td> <td id='td_3_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + FirstParenthesis.Value + "' id='ParenthesisCmb_td_3_tr_" + num + "' > " + FirstParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td> <td id='td_4_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + RowCount + "' value='" + first + "'>" + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' style='width:100%' id='ConceptULCondition_td_4_tr_" + RowCount + "'><li><input id='inputBox_td_4_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_5_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + RowCount + "' value='" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + RowCount + "'></ul></div></div></td> <td id='td_6_tr_" + RowCount + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + RowCount + "' value='" + third + "'> " + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul style='width:100%' class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + RowCount + "'><li><input id='inputBox_td_6_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + SecondParenthesis.Value + "' id='ParenthesisCmb_td_7_tr_" + num + "' > " + SecondParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td> <td id='td_8_tr_" + RowCount + "'><input type=button id='ButtonAddCondition_td_8_tr_" + RowCount + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td style='width: 20%' id='td_9_tr_" + RowCount + "'><input style='width: 100%' type=button id='RemoveConbtn_td_9_tr_" + RowCount + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + RowCount + "'><input type='hidden' class='HFClass' id=Conditionhf_" + RowCount + " value='" + StringRuleStateObject + "' /></td> </tr></table></td></tr>")
                    }
                    else {
                        EdittingPrimaryContr = $("<tr id= 'Conditiontr_" + RowCount + "'class='BoxStyle'><td ><table id='Conditiontable_1_tr_" + RowCount + "'><tr id='Conditiontr_1_table_" + RowCount + "'><td id='td_1_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_1_tr_" + RowCount + "' value='" + AndOr + "'> " + AndOr + " <span class='caret'></span></button><ul class='dropdown-menu test' id='ConOrAnd_td_1_tr_" + RowCount + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td> <td id='td_2_tr_" + RowCount + "'>اگر</td><td id='td_3_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + FirstParenthesis.Value + "' id='ParenthesisCmb_td_3_tr_" + num + "' > " + FirstParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_3_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td> <td id='td_4_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_4_tr_" + RowCount + "' value='" + first + "'>" + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu test scrollable-menu' role='menu' style='width:100%' id='ConceptULCondition_td_4_tr_" + RowCount + "'><li><input id='inputBox_td_4_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_5_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:80px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_5_tr_" + RowCount + "' value='" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu test' role='menu' id='ConOpr_td_5_tr_" + RowCount + "'></ul></div></div></td> <td id='td_6_tr_" + RowCount + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddCondition_td_6_tr_" + RowCount + "' value= '" + third + "'  > " + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul style='width:100%' class='dropdown-menu test scrollable-menu' role='menu' id='ConceptULCondition_td_6_tr_" + RowCount + "'><li><input id='inputBox_td_6_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div> </td> <td id='td_7_tr_" + num + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + SecondParenthesis.Value + "' id='ParenthesisCmb_td_7_tr_" + num + "' > " + SecondParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='ParenthesisUL_td_7_tr_" + num + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td> <td id='td_8_tr_" + RowCount + "'><input type=button id='ButtonAddCondition_td_8_tr_" + RowCount + "' value=اضافه class='AddbtnClass btn btn-info'> </td><td style='width: 20%' id='td_9_tr_" + RowCount + "'><input style='width: 100%' type=button id='RemoveConbtn_td_9_tr_" + RowCount + "' value=" + "حذف" + " onclick='Removetr(1,this)' class='btn btn-danger'/></td><td id='td_10_tr_" + RowCount + "'><input type='hidden' class='HFClass' id=Conditionhf_" + RowCount + " value='" + StringRuleStateObject + "' /></td> </tr></table></td></tr>")
                    }

                    $(".data-Add-Condition").append(EdittingPrimaryContr);
                    FillOperationCombo("#ConOpr_td_5_tr_" + RowCount);
                    FillCombo('Conditionhf_' + RowCount, "#ConceptULCondition_td_4_tr_" + RowCount);
                    FillCombo('Conditionhf_' + RowCount, "#ConceptULCondition_td_6_tr_" + RowCount);
                    num = RowCount;
                    return;
                case "FirstlvlOrder_1":
                case "FirstlvlOrder_2":


                    var first = RuleStateValueJsonObject[1].Value;
                    var second = RuleStateValueJsonObject[4].Value;
                    var firstRuleStateValueJsonObjectConceptName = first;
                    var secondRuleStateValueJsonObjectConceptName = second;
                    Third = jQuery.trim(Third);
                    Third = parseInt(Third);
                    for (var j = 0; j < JsonConceptObj.length; j++) {
                        if (firstRuleStateValueJsonObjectConceptName.constructor === String) {
                            firstRuleStateValueJsonObjectConceptName = firstRuleStateValueJsonObjectConceptName;
                        }
                        else {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(first)) {
                                firstRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;

                            }
                        }
                        if (secondRuleStateValueJsonObjectConceptName.constructor !== String) {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(second)) {
                                secondRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;

                            }
                        }
                    }
                    var EdittingPrimaryOrdertr = $("<tr id='OrderThentr_" + RowCount + "'  class='BoxStyle'><td><table id='OrderThentable_1_tr_" + RowCount + "'><tr id='OrderThentr_1_table_" + RowCount + "'><td id='td_1_tr_" + RowCount + "'>آنگاه</td> <td id='td_2_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddOrders_td_2_tr_" + RowCount + "' value='" + first + "'>" + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul style='width:100%' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrders_td_2_tr_" + RowCount + "'><li><input id='inputBoxSecondlvlByPrimarybtn_td_2_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_3_tr_" + RowCount + "'>=</td> <td id='td_4_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddOrders_td_4_tr_" + RowCount + "' value='" + second + "'>" + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' style='width:100%' role='menu' id='ConceptULOrders_td_4_tr_" + RowCount + "'><li><input id='inputBoxSecondlvlByPrimarybtn_td_4_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td> <td id='td_5_tr_" + RowCount + "'><input type=button id='AddSubConditionbtn_td_5_tr_" + RowCount + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'></td> <td id='td_6_tr_" + RowCount + "'><input type=button id='AddOrdersbtn_td_6_tr_" + RowCount + "' value=" + " دستورات" + " class='AddOrderbtnClass btn btn-info'></td><td style='width: 20%' id='td_7_tr_" + RowCount + "'><input style='width: 100%' type=button id='RemoveFirOrdersbtn_td_7_tr_" + RowCount + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + RowCount + "'><input type='hidden' class='HFClass' id=FirstOrderhf_" + RowCount + "  value='" + StringRuleStateObject + "' /></td></tr></table></td></tr>")

                    $(".data-Add-Order").append(EdittingPrimaryOrdertr);
                    FillCombo('FirstOrderhf_' + RowCount, "#ConceptULOrders_td_2_tr_" + RowCount);
                    FillCombo('FirstOrderhf_' + RowCount, "#ConceptULOrders_td_4_tr_" + RowCount);
                    FillCombo('FirstOrderhf_' + RowCount, "#ConceptULOrdersByOrderButton_td_4_tr_" + RowCount);
                    FillCombo('FirstOrderhf_' + RowCount, "#ConceptULOrdersByOrderButton_td_2_tr_" + RowCount);


                    ThenOrderCounter = RowCount;
                    return;
                case "FirstlvlOrder_3":
                    var first = RuleStateValueJsonObject[5].Value;
                    var second = RuleStateValueJsonObject[8].Value;
                    var third = RuleStateValueJsonObject[10].Value;
                    var AndOr = RuleStateValueJsonObject[1].Value;
                    var FirstParenthesis = "";
                    var SecondParenthesis = "";
                    var firstRuleStateValueJsonObjectConceptName = first;
                    var thirdRuleStateValueJsonObjectConceptName = third;
                    var secondRuleStateValueJsonObjectConceptName = second;
                    var PreviousRow = PreviousRuleStateValueJsonObject[0].Type;
                    for (var j = 0; j < JsonConceptObj.length; j++) {
                        if (firstRuleStateValueJsonObjectConceptName.constructor === String) {
                            firstRuleStateValueJsonObjectConceptName = firstRuleStateValueJsonObjectConceptName;
                        }
                        else {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(first)) {
                                firstRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;
                            }
                        }
                        //secondRuleStateValueJsonObjectConceptName =second;
                        if (thirdRuleStateValueJsonObjectConceptName.constructor !== String) {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(third)) {
                                thirdRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;

                            }
                        }
                    }
                    for (var k = 0; k < JsonObj.length; k++) {
                        if (JsonObj[k].ConceptCode === second) {
                            secondRuleStateValueJsonObjectConceptName = JsonObj[k].Value;
                            break;
                        }
                    }

                    if (PreviousRow === "FirstlvlOrder_3") {
                        FirstParenthesis = RuleStateValueJsonObject[3];
                        SecondParenthesis = RuleStateValueJsonObject[13];
                        EdittingContr = $("<tr id='OrderThentr_" + ThenOrderCounter + "' class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "' ><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='AndOrcmb_td_1_tr_" + ThenOrderCounter + "' value='" + AndOr + "'> " + AndOr + " <span class='caret'></span></button><ul class='dropdown-menu test' id='ConOrAnd_td_1_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td><td id='td_2_tr_" + ThenOrderCounter + "'>اگر</td><td id='td_3_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + FirstParenthesis.Value + "' id='FirOrderParenthesisCmb_td_3_tr_" + ThenOrderCounter + "' > " + FirstParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "' value='" + first + "'> " + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirstOrder_td_6_tr_" + ThenOrderCounter + "' value='" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprFirstOrder_td_6_tr_" + ThenOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_6_tr_" + ThenOrderCounter + "' value='" + third + "'>" + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_6_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + SecondParenthesis.Value + "' id='FirOrderParenthesisCmb_td_7_tr_" + ThenOrderCounter + "' > " + SecondParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_7_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_8_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'/></td><td id='td_9_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_9_tr_" + ThenOrderCounter + "' value=" + "دستورات" + " class='AddOrderbtnClass btn btn-info'/></td><td id='td_10_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_10_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><input type='hidden' class='HFClass' id=FirstOrderhf_" + RowCount + "  value='" + StringRuleStateObject + "' /></td></tr></table></td></tr>")

                    }
                    else {
                        FirstParenthesis = RuleStateValueJsonObject[3];
                        SecondParenthesis = RuleStateValueJsonObject[13];
                        // اضافه کردن شرط با پرانتز
                        EdittingContr = $("<tr id='OrderThentr_" + ThenOrderCounter + "' class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "' ><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_2_tr_" + ThenOrderCounter + "'>اگر</td><td id='td_3_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + FirstParenthesis.Value + "' id='FirOrderParenthesisCmb_td_3_tr_" + ThenOrderCounter + "' > " + FirstParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "' value='" + first + "'> " + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirstOrder_td_6_tr_" + ThenOrderCounter + "' value='" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprFirstOrder_td_6_tr_" + ThenOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_6_tr_" + ThenOrderCounter + "' value='" + third + "'>" + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_6_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + SecondParenthesis.Value + "' id='FirOrderParenthesisCmb_td_7_tr_" + ThenOrderCounter + "' > " + SecondParenthesis.Value + " <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_7_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_8_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'/></td><td id='td_9_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_9_tr_" + ThenOrderCounter + "' value=" + "دستورات" + " class='AddOrderbtnClass btn btn-info'/></td><td id='td_10_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_10_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><input type='hidden' class='HFClass' id=FirstOrderhf_" + RowCount + "  value='" + StringRuleStateObject + "' /></td></tr></table></td></tr>")
                        //  EdittingContr = $("<tr id='OrderThentr_" + ThenOrderCounter + "' class='BoxStyle'><td><table id='OrderThentable_1_tr_" + ThenOrderCounter + "' ><tr id='OrderThentr_1_table_" + ThenOrderCounter + "'><td id='td_2_tr_" + ThenOrderCounter + "'>اگر</td><td id='td_3_tr_" + ThenOrderCounter + "'> ( </td></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_4_tr_" + ThenOrderCounter + "' value='" + first + "'> " + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_4_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_4_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_4_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirstOrder_td_6_tr_" + ThenOrderCounter + "' value='" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprFirstOrder_td_6_tr_" + ThenOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonOrder_td_6_tr_" + ThenOrderCounter + "' value='" + third + "'>" + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULOrdersByConditionButton_td_6_tr_" + ThenOrderCounter + "'><li><input style='width:100%' id='inputBoxByConditionbtn_td_6_tr_" + ThenOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> ) </td><td id='td_8_tr_" + ThenOrderCounter + "'><input type=button id='AddSubConditionbtn_td_8_tr_" + ThenOrderCounter + "' value=" + " شرط" + " class='AddSubConbtnClass btn btn-info'/></td><td id='td_9_tr_" + ThenOrderCounter + "'><input type=button id='AddOrdersbtn_td_9_tr_" + ThenOrderCounter + "' value=" + "دستورات" + " class='AddOrderbtnClass btn btn-info'/></td><td id='td_10_tr_" + ThenOrderCounter + "'><input type=button id='RemoveFirOrdersbtn_td_10_tr_" + ThenOrderCounter + "' value=" + "حذف" + " onclick='Removetr(2,this)' class='btn btn-danger'/></td><input type='hidden' class='HFClass' id=FirstOrderhf_" + ThenOrderCounter + " /></td></tr></table></td></tr>")
                    }
                    $(".data-Add-Order").append(EdittingContr);
                    FillCombo('FirstOrderhf_' + RowCount, "#ConceptULOrdersByConditionButton_td_4_tr_" + RowCount);
                    FillCombo('FirstOrderhf_' + RowCount, "#ConceptULOrdersByConditionButton_td_6_tr_" + RowCount);
                    ThenOrderCounter = RowCount;
                    return;
                case "SecondlvlOrder_1":
                case "SecondlvlOrder_2":
                    var first = RuleStateValueJsonObject[1].Value;
                    var second = RuleStateValueJsonObject[4].Value;
                    var firstRuleStateValueJsonObjectConceptName = first;
                    var secondRuleStateValueJsonObjectConceptName = second;

                    Third = jQuery.trim(Third); 4
                    Third = parseInt(Third);
                    for (var j = 0; j < JsonConceptObj.length; j++) {
                        if (firstRuleStateValueJsonObjectConceptName.constructor === String) {
                            firstRuleStateValueJsonObjectConceptName = firstRuleStateValueJsonObjectConceptName;
                        }
                        else {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(first)) {
                                firstRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;

                            }
                        }
                        if (secondRuleStateValueJsonObjectConceptName.constructor !== String) {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(second)) {
                                secondRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;

                            }
                        }
                    }
                    var EdittingtrElse = $("<tr id='OrderElsetr_" + RowCount + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + RowCount + "' ><tr id='OrderElsetr_1_table_" + RowCount + "'><td id='td_2_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:410px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_2_tr_" + RowCount + "' value='" + first + "'>" + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul style='width:100%' class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevel_td_2_tr_" + RowCount + "'><li><input id='inputBox_td_2_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_3_tr_" + RowCount + "'>=</td><td id='td_4_tr_" + RowCount + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:410px' class='btn btn-info dropdown-toggle ' type='button' data-toggle='dropdown' id='DropDownAddElseOrders_td_3_tr_" + RowCount + "' value='" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' style='width:100%' id='ConceptULSecondLevel_td_3_tr_" + RowCount + "'><li><input id='inputBox_td_3_tr_" + RowCount + "' type='text' onclick='searchdropdownitem(this)'  style='width:100%'></li></ul></div></div></td><td id='td_5_tr_" + RowCount + "'><input type=button id='ButtonAddElseCondition_td_5_tr_" + RowCount + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_6_tr_" + RowCount + "'><input type=button id='ButtonAddElseOrder_td_6_tr_" + RowCount + "' value=" + " دستورات" + " class='AddOrderElsebtnClass btn btn-info'></td><td style='width: 20%' id='td_7_tr_" + RowCount + "'><input style='width: 100%' type=button id='RemoveSecOrdersbtn_td_7_tr_" + RowCount + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td><td id='td_8_tr_" + RowCount + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + RowCount + "  value='" + StringRuleStateObject + "'/></td></tr></table></td></tr>")
                    $(".data-Add-OrderElse").append(EdittingtrElse);
                    FillCombo('SecondOrderhf_' + RowCount, "#ConceptULSecondLevel_td_2_tr_" + RowCount);
                    FillCombo('SecondOrderhf_' + RowCount, "#ConceptULSecondLevel_td_3_tr_" + RowCount);
                    FillCombo('SecondOrderhf_' + RowCount, "#DropDownAddTempBySubButtonOrderElse_td_4_tr_" + RowCount);
                    FillCombo('SecondOrderhf_' + RowCount, "#DropDownAddTempBySubButtonOrderElse_td_2_tr_" + RowCount);


                    ElseOrderCounter = RowCount;
                    return;
                case "SecondlvlOrder_3":
                    var first = RuleStateValueJsonObject[5].Value;
                    var second = RuleStateValueJsonObject[8].Value;
                    var third = RuleStateValueJsonObject[10].Value;
                    var AndOr = RuleStateValueJsonObject[1].Value;
                    var FirstParenthesis = "";
                    var SecondParenthesis = "";
                    var firstRuleStateValueJsonObjectConceptName = first;
                    var thirdRuleStateValueJsonObjectConceptName = third;
                    var secondRuleStateValueJsonObjectConceptName = second;
                    var PreviousRow = PreviousRuleStateValueJsonObject[0].Type;
                    for (var j = 0; j < JsonConceptObj.length; j++) {
                        if (firstRuleStateValueJsonObjectConceptName.constructor === String) {
                            firstRuleStateValueJsonObjectConceptName = firstRuleStateValueJsonObjectConceptName;
                        }
                        else {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(first)) {
                                firstRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;
                            }
                        }
                        //secondRuleStateValueJsonObjectConceptName =second;
                        if (thirdRuleStateValueJsonObjectConceptName.constructor !== String) {
                            if (jQuery.trim(JsonConceptObj[j].DataValueObj.ConceptCode) === jQuery.trim(third)) {
                                thirdRuleStateValueJsonObjectConceptName = JsonConceptObj[j].ConceptName;
                            }
                        }
                    }
                    for (var k = 0; k < JsonObj.length; k++) {
                        if (JsonObj[k].ConceptCode === second) {
                            secondRuleStateValueJsonObjectConceptName = JsonObj[k].Value;
                            break;
                        }
                    }
                    var ElseContr = "";
                    if (PreviousRow === "SecondlvlOrder_3") {
                        FirstParenthesis = RuleStateValueJsonObject[3].Value;
                        SecondParenthesis = RuleStateValueJsonObject[13].Value;
                        ElseContr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_1_tr_" + ThenOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:50px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='و' id='AndOrcmbSecOrder_td_1_tr_" + ElseOrderCounter + "' > و <span class='caret'></span></button><ul class='dropdown-menu test' id='AndOrcmbSecOrderUL_td_1_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>و</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>یا</a></li></ul></div></div></td><td id='td_2_tr_" + ElseOrderCounter + "'> اگر</td><td id='td_3_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + FirstParenthesis + "' id='FirOrderParenthesisCmb_td_3_tr_" + ElseOrderCounter + "' > " + FirstParenthesis + " <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_4_tr_" + ElseOrderCounter + "' value='" + first + "'> " + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirSecstOrder_td_5_tr_" + ElseOrderCounter + "' value = '" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprSecstOrder_td_5_tr_" + ElseOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_6_tr_" + ElseOrderCounter + "' value = '" + third + "'> " + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_6_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + SecondParenthesis + "' id='SecOrderParenthesisCmb_td_7_tr_" + ThenOrderCounter + "' > " + SecondParenthesis + " <span class='caret'></span></button><ul class='dropdown-menu test' id='SecOrderParenthesisUL_td_7_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_8_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_9_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_9_tr_" + ElseOrderCounter + "' value=" + "دستور" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_10_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_10_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_11_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + RowCount + "  value='" + StringRuleStateObject + "'/></td></tr></table></td></tr>")
                    }
                    else {
                        FirstParenthesis = RuleStateValueJsonObject[3].Value;
                        SecondParenthesis = RuleStateValueJsonObject[13].Value;
                        ElseContr = $("<tr id='OrderElsetr_" + ElseOrderCounter + "' class='BoxStyle'><td><table id='OrderElsetable_1_tr_" + ElseOrderCounter + "' ><tr id='OrderElsetr_1_table_" + ElseOrderCounter + "'><td id='td_2_tr_" + ElseOrderCounter + "'> اگر</td><td id='td_3_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + FirstParenthesis + "' id='FirOrderParenthesisCmb_td_3_tr_" + ElseOrderCounter + "' > " + FirstParenthesis + " <span class='caret'></span></button><ul class='dropdown-menu test' id='FirOrderParenthesisUL_td_3_tr_" + ElseOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>((((</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>(((((</a></li></ul></div></div></td><td id='td_4_tr_" + ElseOrderCounter + "'><div class='btn-group'> <div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_4_tr_" + ElseOrderCounter + "' value = '" + first + "'> " + firstRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_4_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_4_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)' ></li></ul></div></div></td><td id='td_5_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:70px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='oprcmbFirSecstOrder_td_5_tr_" + ElseOrderCounter + "' value = '" + second + "'> " + secondRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu' role='menu' id='ConOprSecstOrder_td_5_tr_" + ElseOrderCounter + "'></ul></div></div></td><td id='td_6_tr_" + ElseOrderCounter + "'><div class='btn-group'><div class='dropdown pull-right dropup'><button style='width:370px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' id='DropDownAddTempBySubButtonConditionElse_td_6_tr_" + ElseOrderCounter + "' value='" + third + "'> " + thirdRuleStateValueJsonObjectConceptName + " <span class='caret'></span></button><ul class='dropdown-menu scrollable-menu' role='menu' id='ConceptULSecondLevelByConditionButton_td_6_tr_" + ElseOrderCounter + "'><li><input style='width:100%' id='inputBoxBySecondlvlConditionbtn_td_6_tr_" + ElseOrderCounter + "' type='text' onclick='searchdropdownitem(this)'></li></ul></div></div></td><td id='td_7_tr_" + ThenOrderCounter + "'> <div class='btn-group'><div class='dropdown pull-right'><button style='width:60px' class='btn btn-info dropdown-toggle' type='button' data-toggle='dropdown' value='" + SecondParenthesis + "' id='SecOrderParenthesisCmb_td_7_tr_" + ThenOrderCounter + "' > " + SecondParenthesis + " <span class='caret'></span></button><ul class='dropdown-menu test' id='SecOrderParenthesisUL_td_7_tr_" + ThenOrderCounter + "'><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>))))</a></li><li onclick='ShowDropDownContentInDropDowns(this)'><a href='#'>)))))</a></li></ul></div></div></td><td id='td_8_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseCondition_td_8_tr_" + ElseOrderCounter + "' value=" + " شرط" + " class='AddSubElseConbtnClass btn btn-info'></td><td id='td_9_tr_" + ElseOrderCounter + "'><input type=button id='ButtonAddElseOrder_td_9_tr_" + ElseOrderCounter + "' value=" + "دستور" + " class='AddOrderElsebtnClass btn btn-info'></td><td id='td_10_tr_" + ElseOrderCounter + "'><input type=button id='RemoveSecOrdersbtn_td_10_tr_" + ElseOrderCounter + "' value=" + "حذف" + " onclick='Removetr(3,this)' class='btn btn-danger'/></td> <td id='td_11_tr_" + ElseOrderCounter + "'><input type='hidden' class='HFClass' id=SecondOrderhf_" + RowCount + "  value='" + StringRuleStateObject + "'/></td></tr></table></td></tr>")
                    }
                    $(".data-Add-OrderElse").append(ElseContr);

                    FillCombo('SecondOrderhf_' + RowCount, "#ConceptULSecondLevelByConditionButton_td_4_tr_" + RowCount);
                    FillCombo('SecondOrderhf_' + RowCount, "#ConceptULSecondLevelByConditionButton_td_6_tr_" + RowCount);
                    ThenOrderCounter = RowCount;
                    return;
            }

        }
    }
    catch (ex)
    {
        var ErrorMessage = $("#hfOperationError").val();
        $("#tlbRuleName_TlbRuleGenerator").text(ErrorMessage);
        $("#tlbRuleName_TlbRuleGenerator").css('color', 'red');
        $("#AddCon").css('visibility', 'hidden');
        $("#AddOrders").css('visibility', 'hidden');
        $("#AddElseOrder").css('visibility', 'hidden');
    }
}
function tlbItemSave_TlbRuleGenerator_onClick() {

    ItemSave_RuleGenerator();
 //   RefreshGridInParentForm();

}
function tlbItemExit_TlbRuleGenerator_onClick(sender, e) {
    ShowDialogConfirm();
}
function ShowDialogConfirm() {

    document.getElementById('lblConfirm').innerHTML = document.getElementById('hfCloseMessage_Rules').value;

    DialogConfirm.Show();
}
function tlbItemCancel_TlbCancelConfirm_onClick() {
    DialogConfirm.Close();
}

function tlbItemOk_TlbOkConfirm_onClick() {

    DialogRuleGenerator();
    DialogConfirm.Close();
}
function DialogRuleGenerator() {
    parent.document.getElementById('DialogRuleGenerator_IFrame').src = 'WhitePage.aspx';
    parent.DialogRuleGenerator.Close();
    RefreshGridInParentForm();

}
function RefreshGridInParentForm()
{
    parent.document.getElementById('DialogRulesManagement_IFrame').contentWindow.Apply_Object_RuleStateObjectArrey_FromRuleGenerator(AllHiddenFieldValues);
  
}
function tlbItemHelp_TlbRuleGenerator_onClick() {
    LoadHelpPage('tlbItemHelp_TlbRuleGenerator');
}