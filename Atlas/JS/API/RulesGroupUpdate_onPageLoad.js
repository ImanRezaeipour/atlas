$(document).ready(function () {

    document.body.dir = document.RulesGroupUpdateForm.dir;
    SetWrapper_Alert_Box(document.RulesGroupUpdateForm.id);
    SetActionMode_RulesGroupUpdate();
    GetBoxesHeaders_RulesGroupUpdate();
    CacheTreeViewsSize_RulesGroupUpdate();
    Fill_trvRulesTemplates_RulesGroupUpdate();
    Fill_trvRules_RulesGroupUpdate();

    $('iframe').on('load', function () {
        if ($(this).contents().find("form[action='/Login']").length > 0) { window.location.href = '/'; }
    });

});