

function DialogRuleGenerator_onShow(sender, e)
{
    CurrentLangID = parent.CurrentLangID;
    var ObjDialogRuleGenerator = parent.DialogRuleGenerator.get_value();
    var RequestCaller = ObjDialogRuleGenerator.RequestCaller;
    var Applicant = ObjDialogRuleGenerator.Applicant;
    var KeyApplicant = ObjDialogRuleGenerator.KeyApplicant;
    var LastRequestDate = ObjDialogRuleGenerator.LastRequestDate;
    var contentUrl_DialogRuleGenerator = "RuleGenerator.aspx?RequestCaller=" + CharToKeyCode(RequestCaller) + "&Applicant=" + CharToKeyCode(Applicant) + "&KeyApplicant=" + CharToKeyCode(KeyApplicant) + "&LastRequestDate=" + CharToKeyCode(LastRequestDate);
    DialogRuleGenerator.set_contentUrl(contentUrl_DialogRuleGenerator);
    document.getElementById('DialogRuleGenerator_IFrame').style.display = '';
    document.getElementById('DialogRuleGenerator_IFrame').style.visibility = 'visible';
    if (CurrentLangID == 'fa-IR') {
        document.getElementById('DialogRuleGenerator_topLeftImage').src = 'Images/Dialog/top_right.gif';
        document.getElementById('DialogRuleGenerator_topRightImage').src = 'Images/Dialog/top_left.gif';
        document.getElementById('DialogRuleGenerator_downLeftImage').src = 'Images/Dialog/down_right.gif';
        document.getElementById('DialogRuleGenerator_downRightImage').src = 'Images/Dialog/down_left.gif';
        document.getElementById('CloseButton_DialogRuleGenerator').align = 'left';
    }
    if (CurrentLangID == 'en-US')
        document.getElementById('DialogRuleGenerator').align = 'right';

    var direction = null;
    switch (CurrentLangID) {
        case 'fa-IR':
            direction = 'rtl';
            break;
        case 'en-US':
            direction = 'ltr';
            break;
    }
    document.getElementById('tbl_DialogRuleGeneratorheader').dir = document.getElementById('tbl_DialogRuleGeneratorfooter').dir = direction;
    ChangeStyle_DialogRuleGenerator();

}

function CharToKeyCode(str) {
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

function DialogRuleGenerator_onClose(sender, e)
{
    document.getElementById('DialogRuleGenerator_IFrame').style.display = 'none';
    document.getElementById('DialogRuleGenerator_IFrame').style.visibility = 'hidden';
    DialogRuleGenerator.set_contentUrl("about:blank");
}
function ChangeStyle_DialogRuleGenerator() {
    document.getElementById('DialogRuleGenerator_IFrame').style.width = (screen.width - 10).toString() + 'px';
    document.getElementById('DialogRuleGenerator_IFrame').style.height = (0.75 * screen.height).toString() + 'px';
    document.getElementById('DialogRuleGenerator_IFrame').style.left = '0px';
    document.getElementById('tbl_DialogRuleGeneratorheader').style.width = document.getElementById('tbl_DialogRuleGeneratorfooter').style.width = (screen.width - 7).toString() + 'px';
    document.getElementById('tbl_DialogRuleGeneratorheader').style.left = document.getElementById('tbl_DialogRuleGeneratorfooter').style.left = '0px';
}
function RefreshGridInParentForm() {
    var AllHiddenFieldValues = "";
    parent.document.getElementById('DialogRulesManagement_IFrame').contentWindow.Apply_Object_RuleStateObjectArrey_FromRuleGenerator(AllHiddenFieldValues);

}