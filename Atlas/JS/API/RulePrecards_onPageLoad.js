$(document).ready
        (
            function () {

                document.body.dir = document.RulePrecardsForm.dir;
                SetWrapper_Alert_Box(document.RulePrecardsForm.id);
                GetBoxesHeaders_RulePrecards();
                Fill_GridRulePrecards_RulePrecards('Normal');
            }
        );