define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/sendFunctions/list/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.core.dataTable',
        'spa.engine.infrastructure.moment.jalali',
        'spa.engine.infrastructure.datepicker',
        'spa.engine.core.data',
        'spa.engine.widget.message',
        'spa.engine.core.select',
        'spa.engine.core.modal'],
function ($, ko, template, dataTable, moment, datepicker, dataService, message, select, modal) {
    function viewModel(params) {
        var self = this;

        //عدم نمایش لودر
        params.loading(false);
        self.username = params.root.user;
        self.access = params.root.access;
        self.displaySend = ko.observable(false);

        if ($.grep(self.access, function (e) { return e.ID == 30025; }).length == 1) {
            self.displaySend(true);
        }
        self.selectedYear = 0;
        self.selectedMonth = 0;
        self.records = ko.observable([]);
        self.disabled = ko.observable(true);
        self.selectedId = ko.observable();
        self.selectedDepartmentId = ko.observable();
        var currentAction = '';
        self.displayFinalApprove = ko.observable(true);
        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        var gDate = new Date();
        //برای آنکه ماه قبل به صورت پیش فرض نمایش داده شود +1 را انجام نمی دهیم 
        //var persian = jd_to_persian(gregorian_to_jd(gDate.getFullYear(), gDate.getMonth() + 1, gDate.getDate()))
        var persian = jd_to_persian(gregorian_to_jd(gDate.getFullYear(), gDate.getMonth(), gDate.getDate()))
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

        self.years = new select({ placeholder: 'سال', type: 'GET', url: '/DesktopModules/Atlas/api/yearmonth/GetYears', size: 12, enableValidation: false, selectItem: GetCurrentYear() });
        self.months = new select({ placeholder: 'ماه', type: 'GET', url: '/DesktopModules/Atlas/api/yearmonth/GetMonths', size: 12, enableValidation: false, selectItem: GetCurrentMonth() });
        self.years.selectedItems(GetCurrentYear()[0].id.toString());
        self.months.selectedItems(GetCurrentMonth()[0].id.toString());

        self.Org = ko.observable('0');
        self.organizations = new select({ placeholder: 'سازمان', type: 'GET', url: '/DesktopModules/Atlas/api/department/GetOrganizations', size: 10, enableValidation: false, minimumResultsForSearch: 3 });
        self.costCenters = new select({ placeholder: 'مرکز هزینه', type: 'GET', url: "/DesktopModules/Atlas/api/CostCenter/list", size: 10, pagination: true, enableValidation: true, minimumResultsForSearch: 3 });
        self.employmentTypes = new select({ placeholder: 'نوع استخدام', type: 'GET', url: "/DesktopModules/Atlas/api/EmploymentType/list", size: 10, pagination: true, enableValidation: true, minimumResultsForSearch: 3 });


        //Initialize and Display Loader
        self.loading = ko.observable(true);

        //define DataTable

        self.dtColumns = [
            { targets: 0, data: "PersonnelId", title: "PersonnelId", visible: false },
            { targets: 1, data: "PersonnelId", title: "ردیف", width: 30, orderable: false, render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { targets: 2, data: "PersonnelFullName", title: "نام و نام خانوادگی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 3, data: "PersonnelCode", title: "کد پرسنلی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 4, data: "FunctionDay", title: "روزهای کاری ماه", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 5, data: "PersonnelFunctionDay", title: "روزهای کارکرد", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 6, data: "PersonnelHourPresent", title: "حضور ساعتی", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 7, data: "PersonnelWorkingHolidaysDay", title: "روز تعطیل کاری", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 8, data: "PersonnelWorkingHolidays", title: "ساعات تعطیل کاری", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 9, data: "PersonnelNightWorkDay", title: "روز شب کاری", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 10, data: "PersonnelNightWorkHours", title: "ساعت شب کاری", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 11, data: "DeservedFunctionOutHoliday", title: "مرخصی خارج از تعطیلی", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 12, data: "DeservedFunctionInHoliday", title: "مرخصی بین تعطیلی", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 13, data: "PaylessDay", title: "روز بدون حقوق", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 14, data: "HolidayFunctionDay", title: "نهار تعطیل", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 15, data: "RealFunctionDay", title: "نهار", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 16, data: "PersonnelOverTimeHours", title: "ساعت اضافه کاری", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 17, data: "PersonnelMissionHours", title: "ماموریت ساعتی", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 18, data: "PersonnelNoEnter", title: " روزهای غیبت", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 19, data: "PersonnelIllnessDay", title: "استعلاجی", searchable: true, orderable: false, width: 40, className: 'text-center' },
            { targets: 20, data: "PersonnelAbsence", title: "غیبت ساعتی", searchable: true, orderable: false, width: 40, className: 'text-center' },
            {
                targets: 21, data: "HRM_NationalNo", title: "", searchable: true, orderable: false, width: 20, className: 'text-center',
                render: function (data) {
                    if (data == null)
                        return '<i class="glyphicon glyphicon-remove text-danger" ></i>';
                    else
                        return '<i class="glyphicon glyphicon-ok text-success"  ></i>';
                },
            }
        ];

        self.createdRow = function (element, row, data, index) {
            debugger;
            if (date.HRM_NationalNo == null)
                $(row).addClass("warning");
        };

        self.dtFilter = function (filter) {

            if (self.employmentTypes.selectedItems() != null) { if (self.employmentTypes.selectedItems().length) { self.employmentType(self.employmentTypes.selectedItems()); } }
            if (self.costCenters.selectedItems() != null) { if (self.costCenters.selectedItems().length) { self.costCenter(self.costCenters.selectedItems()); } }
            if (self.organizations.selectedItems() != null) { if (self.organizations.selectedItems().length) { self.organization(self.organizations.selectedItems()); } }
            if (self.years.selectedItems().length) { self.year(self.years.selectedItems()); }
            if (self.months.selectedItems().length) { self.month(self.months.selectedItems()); }

            filter.employmentTypeId = self.employmentType();
            filter.costCenterId = self.costCenter();
            filter.organizationId = self.organization();
            filter.year = self.year();
            filter.month = self.month();
            filter.person = self.person();
            //-----------------------------
            self.selectedYear = self.year();
            self.selectedMonth = self.month();
        }

        //هر بار بارگذاری مجدد داده
        self.dtDraw = function () {
            self.checkApproved();
            self.loading(false);
        }

        self.dtPage = function () {
            self.loading(true);
        }

        self.dtSelect = function (id) {
            self.selectedId(id);
        }

        self.dtPageSize = ko.observable(99999999);

        self.dtPageSize.subscribe(function (value) {
            self.loading(true);
            self.overtimePersonDataTable.chagePageLength(value[0]);
        });

        self.dtPageSizes = [20, 50, 100];

        self.overtimePersonDataTable = new dataTable({
            columnDefs: self.dtColumns,
            pageLength: self.dtPageSize(),
            filter: self.dtFilter,
            draw: self.dtDraw,
            page: self.dtPage,
            select: self.dtSelect,
            url: '/DesktopModules/Atlas/api/OverTimeEdari/functionlist',
            type: 'GET',
            selected: true,
            keybord: true,
            createdRow: self.createdRow
        });

        //Define Filters  
        self.costCenter = ko.observable(0);
        self.employmentType = ko.observable(0);
        self.organization = ko.observable(-1);
        self.year = ko.observable(1395);
        self.month = ko.observable(1);
        self.person = ko.observable('');

        //Other Feilds

        self.search = function () {
            self.loading(true);
            self.overtimePersonDataTable.current.draw();
        }


        self.checkApproved = function () {
            var model = {
                year: self.selectedYear,
                month: self.selectedMonth
            };
            dataService.post('/DesktopModules/Atlas/api/overtime/IsAproved/', model)
                        .done(function (data) {
                            if (data == true)
                                self.displayFinalApprove(false);
                            else
                                self.displayFinalApprove(true);

                        }, function (error) {
                            message.error("خطا در ارسال اطلاعات");
                        });
        };

        self.clear = function () {

            self.loading(true);
            self.Org('');
            self.organizations.current.empty();
            self.organizations.current.select2(self.organizations.options);
            self.organizations.selectedItems(null);
            self.costCenters.current.empty();
            self.costCenters.current.select2(self.costCenters.options);
            self.costCenters.selectedItems(null);
            self.employmentTypes.current.empty();
            self.employmentTypes.current.select2(self.employmentTypes.options);
            self.employmentTypes.selectedItems(null);

            self.costCenter(0);
            self.employmentType(0);
            self.organization(-1);
            self.year(GetCurrentYear()[0].id);
            self.month(GetCurrentMonth()[0].id);
            self.person('');
            self.displayFilter(true);
            self.overtimePersonDataTable.current.draw();
        }

        self.displayFilter = ko.observable(true);
        self.displaySearch = function () {
            self.displayFilter(!self.displayFilter());
        }

        self.goHome = function () {
            document.location.href = "/";
        }

        //config modal

        self.send = function () {
            var r = confirm("آیا از تایید نهایی کارتابل اطمینان دارید!");
            if (r == true) {
                params.loading(true);

                if (self.costCenters.selectedItems() != null) { if (self.costCenters.selectedItems().length) { self.costCenter(self.costCenters.selectedItems()); } }
                if (self.employmentTypes.selectedItems() != null) { if (self.employmentTypes.selectedItems().length) { self.employmentType(self.employmentTypes.selectedItems()); } }
                if (self.organizations.selectedItems() != null) { if (self.organizations.selectedItems().length) { self.organization(self.organizations.selectedItems()); } }
                if (self.years.selectedItems().length) { self.year(self.years.selectedItems()); }
                if (self.months.selectedItems().length) { self.month(self.months.selectedItems()); }

                var model = {
                    year: self.year(),
                    month: self.month(),
                    organizationId: self.organization(),
                    asistantId: 0,
                    departmentId: self.organization(),
                    costCenterId: self.costCenter(),
                    employmentTypeId: self.employmentType(),
                    person: self.person()
                };
                dataService.post('/DesktopModules/Atlas/api/OverTimeEdari/sendItems/', model)
                    .done(function (data) {
                        //Hide Parent Loader
                        params.loading(false);
                        message.success("اطلاعات با موفقیت ارسال گردید");
                    }, function (error) {
                        params.loading(false);
                        message.error("خطا در ارسال اطلاعات");
                    });
            }
        }
        self.refresh = function () {
            self.loading(true);
            self.overtimePersonDataTable.current.draw();
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