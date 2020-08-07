define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/overtime/list/index.html' + '?ver=' + (new Date()).getTime(),
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

        params.loading(false);
        self.username = params.root.user;
        self.selectedId = ko.observable();
          
        var gDate = new Date();
        var persian = jd_to_persian(gregorian_to_jd(gDate.getFullYear(), gDate.getMonth() + 1, gDate.getDate()))
        var monthNames = ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];

        function GetCurrentYear() {
            var model = [{
                id: persian[0],
                text: persian[0].toString()
            }];
            return model;
        }

        function GetCurrentMonth() {
            var model = [{
                id: persian[1],
                text: monthNames[persian[1] - 1].toString()
            }];
            return model;
        }
         
        self.years = new select({ placeholder: 'سال', type: 'GET', url: '/DesktopModules/Atlas/api/yearmonth/GetYears', size: 12, enableValidation: true, selectItem: GetCurrentYear() });
        self.months = new select({ placeholder: 'ماه', type: 'GET', url: '/DesktopModules/Atlas/api/yearmonth/GetMonths', size: 12, enableValidation: true, selectItem: GetCurrentMonth() });
        self.years.selectedItems(GetCurrentYear()[0].id.toString());
        self.months.selectedItems(GetCurrentMonth()[0].id.toString());
        self.departments = new select({ placeholder: 'ارگان/سازمان', type: 'GET', url: '/DesktopModules/Atlas/api/Department/GetOrganizations', size: 10, enableValidation: true, minimumResultsForSearch: 3 });

        var currentAction = '';

        //Initialize and Display Loader
        self.loading = ko.observable(true);

 
        self.dtColumns = [
            { targets: 1, data: "ID", visible: false, title: "ID" },
            { targets: 2, data: "DepartmentName", title: "معاونت", searchable: true, orderable: false, width: 200, className: 'select' },
            { targets: 3, data: "MaxOverTime", title: "سرانه اضافه کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 4, data: "OverTimePersonCount", title: "تعداد پرسنل اضافه کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 5, data: "TotalOverTime", title: "جمع سرانه اضافه کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 6, data: "MaxHoliday", title: "درصد سرانه تعطیل کاری تشویقی", searchable: true, orderable: false, className: 'text-center', render: function (data) { return data + ' %'; } },
            { targets: 7, data: "MonthHolidayCount", title: "تعداد روزهای تعطیل", searchable: true, orderable: false, className: 'text-center' },
            { targets: 8, data: "HolidayPersonCount", title: "تعداد پرسنل تعطیل کاری", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 9, data: "TotalHoliday", title: "جمع سرانه تعطیل کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 10, data: "MaxNightly", title: "سرانه شب کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 11, data: "NightlyPersonCount", title: "تعداد پرسنل شب کاری", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 12, data: "TotalNightly", title: "جمع سرانه شب کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' }
        ];

        self.actionButtons = {
            targets: 0,
            title: "عملیات",
            data: "ID",
            width: "15px",
            searchable: false, orderable: false,
            buttons: [{
                name: "",
                attr: { type: "button", "class": "btn btn-success", "style": "margin:2px" },
                action: function (val, data) {
                    currentAction = 'edit';
                    self.modal.title('ویرایش سرانه');
                    self.modal.body({ name: 'spa-app-overtime-Edit', params: { id: data.ID } });
                    self.modal.visible(true);
                },
                icon: "glyphicon glyphicon-pencil",
            }]
        };

        self.dtFilter = function (filter) {
            if (self.departments.selectedItems().length)
                self.department(self.departments.selectedItems());
            if (self.years.selectedItems().length)
                self.year(self.years.selectedItems());
            if (self.months.selectedItems().length)
                self.month(self.months.selectedItems());

            filter.department = self.department();
            filter.departmentName = self.departmentName();
            filter.year = self.year();
            filter.month = self.month();

            //Other Field ...
        }
        //هر بار بارگذاری مجدد داده
        self.dtDraw = function () {
            self.loading(false);
        }
        self.dtPage = function () {
            self.loading(true);
        }
        self.dtSelect = function (id) {
            self.selectedId(id);
        }

        self.dtPageSize = ko.observable(20);

        self.dtPageSize.subscribe(function (value) {
            self.loading(true);
            self.overtimeDataTable.chagePageLength(value[0]);
        });

        self.dtPageSizes = [20, 50, 100];

        self.overtimeDataTable = new dataTable({
            columnDefs: self.dtColumns,
            pageLength: self.dtPageSize(),
            filter: self.dtFilter,
            draw: self.dtDraw,
            page: self.dtPage,
            select: self.dtSelect,
            url: '/DesktopModules/Atlas/api/overtime/list',
            type: 'GET',
            actionButtons: self.actionButtons,
            selected: true,
            keybord: true
        });

        //Define Filters 
        self.department = ko.observable();
        self.year = ko.observable(1395);
        self.month = ko.observable(1);
        self.departmentName = ko.observable();
        //Other Feilds

        self.search = function () {
            self.loading(true);
            self.overtimeDataTable.current.draw();
        }

        self.clear = function () {
            self.loading(true);
            self.department('');
            self.departmentName('');
            self.year(1395);
            self.month(1);
            self.displayFilter(false);
            self.overtimeDataTable.current.draw();
        }

        self.displayFilter = ko.observable(true);
        self.displaySearch = function () {
            self.displayFilter(!self.displayFilter());
        }

        //config modal
        self.modal = new modal(function (button, data) {
            switch (currentAction) {
                case 'create':
                    if (button == 'save') {
                        self.loading(true);
                        self.overtimeDataTable.current.draw();

                        message.info("ثبت با موفقیت انجام شد.");
                    }
                    break;
                case 'edit':
                    if (button == "save") {
                        self.loading(true);
                        self.overtimeDataTable.current.draw();
                        message.info("ویرایش با موفقیت انجام شد.");
                    }
                    break;
                case 'remove':
                    if (button == "delete") {
                        self.loading(true);
                        self.overtimeDataTable.current.draw();
                        message.info("حذف با موفقیت انجام شد.");
                    }
                    break;
                case 'cancel':
                    break;
            }
        });

        self.add = function () {
            currentAction = 'add';
            self.modal.title('درج سرانه جدید');
            self.modal.body({ name: 'spa-app-overtimeCreate', params: {} });
            self.modal.visible(true);
        };
        
        self.refresh = function () {
            self.loading(true);
            self.overtimeDataTable.current.draw();
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