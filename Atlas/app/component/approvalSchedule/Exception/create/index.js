define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/approvalschedule/Exception/create/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.infrastructure.guid',
        './validator',
        'spa.engine.core.data',
        'spa.engine.core.select',
        'spa.engine.handler.datepicker',
        'spa.engine.infrastructure.validator.custom.jDate'],
function (ko, template, guid, validator, dataService, select) {
    function viewModel(params) {
        var self = this;

        //Hide Parent Loader
        params.loading(false);

        self.approvalScheduleID = params.approvalScheduleID;
        self.DateFrom = ko.observable();
        self.DateTo = ko.observable();

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        function formatRepo(repo) {
            if (repo.loading) return repo.text;

            return repo.text;
        }

        self.Persons = new select({
            placeholder: 'پرسنل',
            type: 'GET',
            url: '/DesktopModules/Atlas/api/Person/QuickSearch',
            size: 20,
            enableValidation: true,
            minimumInputLength: 3
        });

        function getModel() {
            var model = {
                ID: 0,
                ApprovalType: null,
                DateFrom: (new Date(self.DateFrom())).toDateString(),
                DateTo: (new Date(self.DateTo())).toDateString(),
                PersonID: self.Persons.selectedItems(),
                PersonFullName: null,
                PersonCode: null,
                ApprovalAttendanceScheduleID: self.approvalScheduleID
            };
            console.log(model);
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
                dataService.post('/DesktopModules/Atlas/api/approvalschedule/createException/', getModel()).done(function (data) {
                    params.context.visible(false);
                    params.context.callback('addException', data);

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

    }

    return {
        viewModel: viewModel,
        template: template
    };
});