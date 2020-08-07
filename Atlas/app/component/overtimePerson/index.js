define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/overtimePerson/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.core.dataTable',
        'spa.engine.infrastructure.moment.jalali',
        'spa.engine.infrastructure.datepicker',
        'spa.engine.core.modal',
        'spa.engine.widget.message',
        'spa.engine.core.select',
        'spa.engine.core.data'
],
function ($, ko, template, dataTable, moment, datepicker, modal, message, select, dataService) {
    function viewModel(params) {
        var self = this;
        
        self.params=params;

        //عدم نمایش لودر
        params.loading(false);

       
    }

    return {
        viewModel: viewModel,
        template: template
    };
});