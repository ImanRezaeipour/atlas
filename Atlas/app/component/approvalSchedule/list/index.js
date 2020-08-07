define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/approvalschedule/list/index.html' + '?ver=' + (new Date()).getTime(),
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
        debugger;
        //عدم نمایش لودر
        params.loading(false);

        var currentAction = '';
        self.username = params.root.user;
        self.access = params.root.access;
        self.selectedId = ko.observable();
        self.selectedExceptionId = ko.observable();

        self.costCenters = new select({
            placeholder: 'مرکز هزینه',
            type: 'GET',
            url: '/DesktopModules/Atlas/api/CostCenter/list',
            size: 20,
            pagination: true,
            enableValidation: true,
            minimumResultsForSearch: 3
        });

        //Initialize and Display Loader
        self.loading = ko.observable(true);

        self.actionButtons = {
            targets: 0,
            title: "عملیات",
            data: "ID",
            width: "90px",
            searchable: false, orderable: false,
            buttons: [{
                name: " ",
                attr: { type: "button", "class": "btn btn-info", "style": "margin:2px" },
                icon: "glyphicon glyphicon-info-sign",
                action: function (val, data) {
                    self.selectedId(data.ID);
                    self.loading(true);
                    self.approvalScheduleExceptionDataTable.current.draw();
                },
            }]
        };

        if ($.grep(self.access, function (e) { return e.ID == 30011; }).length == 1) {
            self.actionButtons.buttons.add({
                name: " ",
                attr: { type: "button", "class": "btn btn-success", "style": "margin:2px" },
                icon: "glyphicon glyphicon-pencil",
                action: function (val, data) {
                    currentAction = 'edit';
                    self.modal.title('ویرایش زمان بندی');
                    self.modal.body({ name: 'spa-app-approvalSchedule-Edit', params: { id: data.ID } });
                    self.modal.visible(true);
                },
            });
        }

        self.dtColumns = [
            { targets: 1, data: "ID", visible: false, title: "ID" },
            { targets: 2, data: "ApprovalType", title: "نوع", searchable: true, orderable: false, className: 'select' },
            { targets: 3, data: "DateFrom", title: "از تاریخ", searchable: true, orderable: false, className: 'text-center', render: function (data) { return moment(data, 'YYYY/MM/DD').format('jYYYY/jM/jD'); } },
            { targets: 4, data: "DateTo", title: "تا تاریخ", searchable: true, orderable: false, className: 'text-center', render: function (data) { return moment(data, 'YYYY/MM/DD').format('jYYYY/jM/jD'); } },
            { targets: 5, data: "DateRangeOrder", title: "کد دوره", searchable: true, orderable: false, className: 'text-center' },
            { targets: 6, data: "CostCenterName", title: "مرکز هزینه", searchable: true, orderable: false, className: 'text-center' }
        ];

        self.actionExceptionButtons = { targets: 0, title: "عملیات", data: "ID", width: "90px", searchable: false, orderable: false, buttons: [] };
        if ($.grep(self.access, function (e) { return e.ID == 30023; }).length == 1) {
            self.actionExceptionButtons.buttons.add({
                name: "",
                attr: { type: "button", "class": "btn btn-danger", "style": "margin:2px" },
                icon: "glyphicon glyphicon-remove",
                action: function (val, data) {
                    currentAction = 'removeException';
                    self.modal.title('حذف استثناء زمان بندی');
                    self.modal.body({
                        name: 'spa-app-approvalSchedule-Exception-Delete', params: {
                            id: data.ID,
                            DateTo: data.DateTo,
                            DateFrom: data.DateFrom,
                            Person: data.PersonFullName,
                        }
                    });
                    self.modal.visible(true);
                }
            });
        }
        if ($.grep(self.access, function (e) { return e.ID == 30022; }).length == 1) {
            self.actionExceptionButtons.buttons.add({
                name: "",
                attr: { type: "button", "class": "btn btn-success", "style": "margin:2px" },
                icon: "glyphicon glyphicon-pencil",
                action: function (val, data) {
                    currentAction = 'editException';
                    self.modal.title('ویرایش استثناء زمان بندی');
                    self.modal.body({ name: 'spa-app-approvalSchedule-Exception-Edit', params: { id: data.ID } });
                    self.modal.visible(true);
                }
            });
        }
        self.dtExceptionColumns = [
            { targets: 1, data: "ID", visible: false, title: "ID" },
            { targets: 2, data: "PersonFullName", title: "پرسنل", searchable: true, orderable: false, className: 'select' },
            { targets: 3, data: "PersonCode", title: "کد پرسنلی", searchable: true, orderable: false, className: 'select' },
            { targets: 4, data: "DateFrom", title: "از تاریخ", searchable: true, orderable: false, className: 'text-center', render: function (data) { return moment(data, 'YYYY/MM/DD').format('jYYYY/jM/jD'); } },
            { targets: 5, data: "DateTo", title: "تا تاریخ", searchable: true, orderable: false, className: 'text-center', render: function (data) { return moment(data, 'YYYY/MM/DD').format('jYYYY/jM/jD'); } }
        ];

        self.dtFilter = function (filter) {
            if (self.costCenters.selectedItems().length)
                self.costCenter(self.costCenters.selectedItems());

            filter.costCenter = self.costCenter();
        }

        self.dtExceptionFilter = function (filter) {
            filter.approvalAttendanceSchedule = self.selectedId();
        }

        self.dtPageSizes = [20, 50, 100];

        //هر بار بارگذاری مجدد داده
        self.dtDraw = function () { self.loading(false); }
        self.dtPage = function () { self.loading(true); }
        self.dtSelect = function (id) {
            //self.selectedId(id);
        }
        self.dtSelectData = function (data) {
            self.selectedId(data.ID);
            self.loading(true);
            self.approvalScheduleExceptionDataTable.current.draw();
        }
        self.dtPageSize = ko.observable(20);
        self.dtPageSize.subscribe(function (value) {
            self.loading(true);
            self.approvalScheduleDataTable.chagePageLength(value[0]);
        });

        self.dtExceptionDraw = function () { self.loading(false); }
        self.dtExceptionPage = function () { self.loading(true); }
        self.dtExceptionSelect = function (id) { self.selectedExceptionId(id); }
        self.dtExceptionPageSize = ko.observable(20);
        self.dtExceptionPageSize.subscribe(function (value) {
            self.loading(true);
            self.approvalScheduleExceptionDataTable.chagePageLength(value[0]);
        });

        self.approvalScheduleDataTable = new dataTable({
            columnDefs: self.dtColumns,
            pageLength: self.dtPageSize(),
            filter: self.dtFilter,
            draw: self.dtDraw,
            page: self.dtPage,
            select: self.dtSelect,
            selectData: self.dtSelectData,
            url: '/DesktopModules/Atlas/api/approvalschedule/list',
            type: 'GET',
            actionButtons: self.actionButtons,
            selected: true
        });
        self.approvalScheduleExceptionDataTable = new dataTable({
            columnDefs: self.dtExceptionColumns,
            pageLength: self.dtExceptionPageSize(),
            filter: self.dtExceptionFilter,
            draw: self.dtExceptionDraw,
            page: self.dtExceptionPage,
            select: self.dtExceptionSelect,
            url: '/DesktopModules/Atlas/api/approvalschedule/listException',
            type: 'GET',
            actionButtons: self.actionExceptionButtons,
            selected: true
        });

        //Define Filters 
        self.costCenter = ko.observable();
        //Other Feilds

        self.search = function () {
            self.loading(true);
            self.selectedId(0);
            self.approvalScheduleDataTable.current.draw();
            self.approvalScheduleExceptionDataTable.current.draw();
        }

        self.clear = function () {
            self.loading(true);
            self.displayFilter(false);
            self.approvalScheduleDataTable.current.draw();
            self.approvalScheduleExceptionDataTable.current.draw();
        }

        self.displayFilter = ko.observable(true);
        self.displaySearch = function () {
            self.displayFilter(!self.displayFilter());
        }

        //config modal
        self.modal = new modal(function (button, data) {
            switch (currentAction) {
                case 'addException':
                    if (button == "addException") {
                        self.loading(true);
                        self.approvalScheduleExceptionDataTable.current.draw();
                    }
                    break;
                case 'editException':
                    if (button == "saveException") {
                        self.loading(true);
                        self.approvalScheduleExceptionDataTable.current.draw();
                        message.info("ویرایش با موفقیت انجام شد.");
                    }
                    break;
                case 'removeException':
                    if (button == "removeException") {
                        self.loading(true);
                        self.approvalScheduleExceptionDataTable.current.draw();
                        message.info("حذف با موفقیت انجام شد.");
                    }
                    break;
                case 'add':
                    if (button == "add") {
                        self.loading(true);
                        self.approvalScheduleDataTable.current.draw();
                    }
                    break;
                case 'edit':
                    if (button == "save") {
                        self.loading(true);
                        self.approvalScheduleDataTable.current.draw();
                        message.info("ویرایش با موفقیت انجام شد.");
                    }
                    break;
                case 'remove':
                    break;
                case 'cancel':
                    break;
            }
        });

        self.add = function () {
            currentAction = 'addException';
            self.modal.title('ایجاد استثناء');
            self.modal.body({ name: 'spa-app-approvalSchedule-Exception-Create', params: { approvalScheduleID: self.selectedId() } });
            self.modal.visible(true);
        }

        self.refresh = function () {
            self.loading(true);
            self.approvalScheduleDataTable.current.draw();
            self.approvalScheduleExceptionDataTable.current.draw();
        }
        self.close = function () {
            window.top.close();
        }
    }

    return {
        viewModel: viewModel,
        template: template
    };
});