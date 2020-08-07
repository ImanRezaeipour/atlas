define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/overtime/list',
        'spa.engine.core.dataTable',
        'spa.engine.infrastructure.moment.jalali',
        'spa.engine.infrastructure.datepicker',
        'spa.engine.core.modal',
        'spa.engine.widget.message',
        'spa.engine.core.select'
],
function ($, ko, template, dataTable, moment, datepicker, modal, message, select) {
    function viewModel(params) {
        var self = this;

        //عدم نمایش لودر
        params.loading(false);

        self.selectedId = ko.observable();

        var gDate = new Date();
        var persian = jd_to_persian(gregorian_to_jd(gDate.getFullYear(), gDate.getMonth() + 1, gDate.getDate()))
        var monthNames = ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];

        //placeHolder, tags, multiple, maximumSelectionLength, minimumInputLenght, url, pageSize, templateResult, selectedItems,localdata, enableValidation
        self.years = new select('سال', false, false, null, null, '/api/yearmonth/GetYears', 12, null, [{ id: persian[0], text: persian[0].toString() }], null, true);
        self.months = new select('ماه', false, false, null, null, '/api/yearmonth/GetMonths', 12, null, [{ id: persian[1], text: monthNames[persian[1] - 1].toString() }], null, true);
        self.departments = new select('ارگان/سازمان', false, false, null, null, '/api/Department/GetOrganizations', 10, null, null, null, true);

        var currentAction = '';

        //Initialize and Display Loader
        self.loading = ko.observable(true);

        //define DataTable
        self.dtURL = '/api/overtime/list';
        self.dtColumns = [
            {
                targets: 0,
                searchable: false,
                orderable: false,
                data: "ID",
                width: 15,
                className: 'text-center',
                render: function (data) { return '<input type="checkbox" class="select" value="' + data + '" />'; }
            },
            { targets: 1, data: "ID", visible: false, title: "ID" },
            { targets: 2, data: "DepartmentName", title: "معاونت", searchable: true, orderable: false, width: 200, className: 'select' },
            { targets: 3, data: "MaxOverTime", title: "سرانه اضافه کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 4, data: "OverTimePersonCount", title: "پرسنل اضافه کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 5, data: "TotalOverTime", title: "جمع سرانه اضافه کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 6, data: "MaxHoliday", title: "سرانه تعطیل کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 7, data: "HolidayPersonCount", title: "پرسنل تعطیل کاری", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 8, data: "TotalHoliday", title: "جمع سرانه تعطیل کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 9, data: "MaxNightly", title: "سرانه شب کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 10, data: "NightlyPersonCount", title: "پرسنل شب کاری", searchable: true, orderable: false, className: 'sum text-center' },
            { targets: 11, data: "TotalNightly", title: "جمع سرانه شب کاری تشویقی", searchable: true, orderable: false, className: 'sum text-center' }
        ];

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

        self.overtimeDataTable = new dataTable(self.dtColumns, self.dtPageSize(), self.dtFilter, self.dtDraw, self.dtPage, self.dtSelect, self.dtURL);

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

        self.displayFilter = ko.observable(false);
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
            }
        });

        self.add = function () {
            currentAction = 'add';
            self.modal.title('درج سرانه جدید');
            self.modal.body({ name: 'spa-app-overtimeCreate', params: {} });
            self.modal.visible(true);
        };
        self.edit = function () {
            currentAction = 'edit';
            self.modal.title('ویرایش سرانه');
            self.modal.body({ name: 'spa-app-overtimeEdit', params: { id: self.selectedId() } });
            self.modal.visible(true);
        };
        self.refresh = function () {
            self.loading(true);
            self.overtimeDataTable.current.draw();
        }
    }

    return {
        viewModel: viewModel,
        template: template
    };
});