define(['ko',
        'spa.engine.infrastructure.htmlLoader!/approvalschedule/edit',
        './validator',
        'spa.engine.core.data',
        'spa.engine.handler.datepicker',
        'spa.engine.infrastructure.validator.custom.jDate'],
function (ko, template, validator, dataService) {
    function viewModel(params) {
        var self = this;

        self.id = params.id;
        self.DateFrom = ko.observable();
        self.DateTo = ko.observable();
        self.ApprovalType = ko.observable();

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        function getData() {
            dataService.get('/api/approvalschedule/getitem/', self.id).done(function (data) {
                self.DateFrom(new Date(data.DateFrom));
                self.DateTo(new Date(data.DateTo));
                self.ApprovalType(data.ApprovalType);

                //Hide Parent Loader
                params.loading(false);
            }, function (error) {

            });
        }

        function toJS() {
            var model = {
                ID: self.id,
                ApprovalType: self.ApprovalType(),
                DateFrom: (new Date(self.DateFrom())).toDateString(),
                DateTo: (new Date(self.DateTo())).toDateString()
            };
            return model;
        }

        //Register Modal Buttons
        function registerButtons() {
            var buttons = [
                {
                    text: 'ذخیره', css: 'btn btn-primary', icon: self.spinner, click: function () {
                        self.validator.validate();
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

        //Register Form Validator
        function registerValidator() {
            self.success = function () {
                self.spinner('spa-spiner');
                dataService.put('/api/approvalschedule/edit/', self.id, toJS())
                    .done(function (data) {
                        params.context.visible(false);
                        params.context.callback('save', data);

                    }, function (error) {
                        //error
                    })
            }
            self.error = function () {
                //
            }
            self.validator = new validator(self.success, self.error);
        };

        registerButtons();
        registerValidator();
        getData();
    }

    return {
        viewModel: viewModel,
        template: template
    };
});