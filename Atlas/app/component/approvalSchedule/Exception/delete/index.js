define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/approvalschedule/Exception/delete/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.core.data',
        'spa.engine.infrastructure.moment.jalali'],
function (ko, template, dataService, moment) {
    function viewModel(params) {

        var self = this;

        self.id = params.id;
        self.Person = ko.observable(params.Person);
        self.DateFrom = ko.observable(moment(params.DateFrom, 'YYYY/MM/DD').format('jYYYY/jM/jD'));
        self.DateTo = ko.observable(moment(params.DateTo, 'YYYY/MM/DD').format('jYYYY/jM/jD'));

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        params.loading(false);

        //Register Modal Buttons
        function registerButtons() {
            var buttons = [
                {
                    text: 'حذف', css: 'btn btn-warning', icon: self.spinner, click: function () {
                        self.spinner('spa-spiner');
                        dataService.put('/DesktopModules/Atlas/api/approvalschedule/deleteException/', self.id, null)
                            .done(function (data) {
                                params.context.visible(false);
                                params.context.callback('removeException', data);
                            }, function () {
                                alert('اشکال در ثبت اطلاعات');
                            });
                    }
                },
                {
                    text: 'لغو', css: 'btn btn-danger', icon: '', click: function () {
                        params.context.visible(false);
                        params.context.callback('discard', '');
                    }
                }
            ];

            params.context.registerButtons(buttons);
        };

        registerButtons();
    }

    return {
        viewModel: viewModel,
        template: template
    };
});