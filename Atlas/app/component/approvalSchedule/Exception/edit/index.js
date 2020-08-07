define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/approvalschedule/Exception/edit/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.infrastructure.guid',
        './validator',
        'spa.engine.core.data',
        'spa.engine.core.select',
        'spa.engine.handler.datepicker',
        'spa.engine.infrastructure.validator.custom.jDate'],
function (ko, template, guid, validator, dataService, select) {
    function viewModel(params) {
        var self = this;

        self.id = params.id;
        self.approvalScheduleID = null;
        self.DateFrom = ko.observable();
        self.DateTo = ko.observable();

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        self.Persons = new select({
            placeholder: 'پرسنل',
            type: 'GET',
            url: '/DesktopModules/Atlas/api/Person/QuickSearch',
            size: 20,
            enableValidation: true,
            minimumInputLength: 3
        });

        function getData() {
            dataService.get('/DesktopModules/Atlas/api/approvalschedule/GetExceptionItem/', self.id).done(function (data) {

                self.approvalScheduleID = data.ApprovalAttendanceScheduleID;
                self.DateFrom(new Date(data.DateFrom));
                self.DateTo(new Date(data.DateTo));
                self.Persons.selectItems({ id: data.PersonID, text: data.PersonFullName });
                //Hide Parent Loader
                params.loading(false);
            }, function (error) {

            });
        }

        function toJS() {
            debugger;
            var model = {
                ID: self.id,
                ApprovalAttendanceScheduleID: self.approvalScheduleID,
                DateFrom: (new Date(self.DateFrom())).toDateString(),
                DateTo: (new Date(self.DateTo())).toDateString(),
                PersonID: self.Persons.selectedItems()

            };
            return model;
        }

        //Register Modal Buttons
        function registerButtons() {
            var buttons = [
                {
                    text: 'ذخیره', css: 'btn btn-primary', icon: self.spinner, click: function () {
                        self.validator.validate(guid.newGuid());
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
                dataService.put('/DesktopModules/Atlas/api/approvalschedule/EditException/', self.id, toJS())
                    .done(function (data) {
                        params.context.visible(false);
                        params.context.callback('saveException', data);

                    }, function (error) {
                        //error
                        params.context.visible(false);
                        params.context.callback('cancel', data);
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