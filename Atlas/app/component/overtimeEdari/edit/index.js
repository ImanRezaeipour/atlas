define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/overtimeEdari/edit/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.infrastructure.guid',
        './validator',
        'spa.engine.core.data'],
function (ko, template, guid, validator, dataService) {
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

        self.P1 = ko.observable();
        self.P2 = ko.observable();
        self.P3 = ko.observable();
        self.P4 = ko.observable();
        self.P5 = ko.observable();
        self.P6 = ko.observable();
        self.P7 = ko.observable();
        self.P8 = ko.observable();
        self.P9 = ko.observable();
        self.P10 = ko.observable();
        self.P11 = ko.observable();
        self.P12 = ko.observable();

        self.P1Old = null; self.P2Old = null; self.P3Old = null; self.P4Old = null; self.P5Old = null; self.P6Old = null; self.P7Old = null; self.P8Old = null; self.P9Old = null; self.P10Old = null; self.P11Old = null; self.P12Old = null;
        self.IsArchiveEnable = ko.observable();

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        function getData() {
            dataService.get('/DesktopModules/Atlas/api/OverTimeEdari/getItem/', self.id).done(function (data) {
                self.Person(data.PersonFullName);
                self.OverTime = data.Date;
                self.MaxOverTime(data.MaxOverTime);
                self.MaxHolidayWork(data.MaxHolidayWork);
                self.MaxNightWork(data.MaxNightWork);

                self.HasOverTime(!data.HasOverTime);
                self.HasNightWork(!data.HasNightWork);
                self.HasHolidayWork(!data.HasHolidayWork);

                self.P1(data.P1); self.P2(data.P2); self.P3(data.P3); self.P4(data.P4); self.P5(data.P5); self.P6(data.P6); self.P7(data.P7); self.P8(data.P8); self.P9(data.P9); self.P10(data.P10); self.P11(data.P11); self.P12(data.P12);
                self.P1Old = data.P1; self.P2Old = data.P2; self.P3Old = data.P3; self.P4Old = data.P4; self.P5Old = data.P5; self.P6Old = data.P6; self.P7Old = data.P7; self.P8Old = data.P8; self.P9Old = data.P9; self.P10Old = data.P10; self.P11Old = data.P11; self.P12Old = data.P12;

                self.IsArchiveEnable(data.IsArchiveEnable);
                //Hide Parent Loader
                params.loading(false);
            }, function (error) {
            });
        }

        function toJS() {
            var model = {
                MaxOverTime: self.MaxOverTime(),
                MaxNightWork: self.MaxNightWork(),
                MaxHolidayWork: self.MaxHolidayWork(),

                P1: self.P1(), P2: self.P2(), P3: self.P3(), P4: self.P4(), P5: self.P5(), P6: self.P6(), P7: self.P7(), P8: self.P8(), P9: self.P9(), P10: self.P10(), P11: self.P11(), P12: self.P12(),
                P1Old: self.P1Old, P2Old: self.P2Old, P3Old: self.P3Old, P4Old: self.P4Old, P5Old: self.P5Old, P6Old: self.P6Old, P7Old: self.P7Old, P8Old: self.P8Old, P9Old: self.P9Old, P10Old: self.P10Old, P11Old: self.P11Old, P12Old: self.P12Old,
                IsArchiveEnable: self.IsArchiveEnable()
            };
            return model;
        }

        //Register OldModal Buttons
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
                dataService.put('/DesktopModules/Atlas/api/overtimeedari/edit/', self.id, toJS())
                    .done(function (data) {
                        params.context.visible(false);
                        params.context.callback('save', data);

                    }, function (error) {
                        self.spinner('');
                        //error
                        //params.context.visible(false);
                        //params.context.callback('cancel', data);
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