define(['ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/approvalschedule/edit/index.html' + '?ver=' + (new Date()).getTime(),
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
        self.DateFrom = ko.observable();
        self.DateTo = ko.observable();
        self.ApprovalType = ko.observable();
        self.DateRangeOrder = ko.observable();
        self.CostCenterID = ko.observable();

        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        self.DateRangeOrders = new select({
            placeholder: 'دوره',
            type: 'GET',
            url: '/DesktopModules/Atlas/api/yearmonth/GetDateRangeOrders',
            size: 12,
            enableValidation: true
        });

        function getData() {
            dataService.get('/DesktopModules/Atlas/api/approvalschedule/getitem/', self.id).done(function (data) {

                self.DateFrom(new Date(data.DateFrom));
                self.DateTo(new Date(data.DateTo));
                self.ApprovalType(data.ApprovalType);
                self.DateRangeOrder(data.DateRangeOrder);
                self.CostCenterID(data.CostCenterID);
                self.DateRangeOrders.selectItems({ id: data.DateRangeOrder, text: data.DateRangeOrder });
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
                DateTo: (new Date(self.DateTo())).toDateString(),
                DateRangeOrder: self.DateRangeOrders.selectedItems(),
                CostCenterID: self.CostCenterID()

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
                dataService.put('/DesktopModules/Atlas/api/approvalschedule/edit/', self.id, toJS())
                    .done(function (data) {
                        params.context.visible(false);
                        params.context.callback('save', data);

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