define(['ko',
        'spa.engine.infrastructure.htmlLoader!/overtimePerson/edit',
        './validator',
        'spa.engine.core.data'],
function (ko, template, validator, dataService) {
    function viewModel(params) {
        var self = this;

        self.id = params.id;
        self.MaxOverTime = ko.observable();
        self.MaxNightWork = ko.observable();
        self.MaxHolidayWork = ko.observable();
        self.HasOverTime = ko.observable();
        self.HasNightWork = ko.observable();
        self.HasHolidayWork = ko.observable();
        self.Person = ko.observable();
        self.OverTime = ko.observable();

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        function getData() {
            dataService.get('/api/overtimeperson/getitem/', self.id).done(function (data) {
                self.Person(data.PersonFullName);
                self.OverTime = data.Date;
                self.MaxOverTime(data.MaxOverTime);
                self.MaxHolidayWork(data.MaxHolidayWork);
                self.MaxNightWork(data.MaxNightWork);

                self.HasOverTime(!data.HasOverTime);
                self.HasNightWork(!data.HasNightWork);
                self.HasHolidayWork(!data.HasHolidayWork);

                //Hide Parent Loader
                params.loading(false);
            }, function (error) {

            });
        }

        function toJS() {
            var model = {
                MaxOverTime: self.MaxOverTime(),
                MaxNightly: self.MaxNightWork(),
                MaxHoliday: self.MaxHolidayWork()
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
                dataService.put('/api/overtimeperson/edit/', self.id, toJS())
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