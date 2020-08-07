define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/overtimeEdari/list/index.html' + '?ver=' + (new Date()).getTime(),
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
         
        if ($.grep(self.access, function (e) { return e.ID == 30024; }).length == 1) {
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
        self.assistances = new select({ placeholder: 'معاونت', type: 'GET', url: function (value) { return "/DesktopModules/Atlas/api/department/GetAssistances/" + value; }, size: 20, enableValidation: false, minimumResultsForSearch: 3, cascade: self.organizations.selectedItems });
        self.managements = new select({ placeholder: 'اداره', type: 'GET', url: function (value) { return "/DesktopModules/Atlas/api/department/GetManagements/" + value; }, size: 20, enableValidation: false, minimumResultsForSearch: 3, cascade: self.assistances.selectedItems });
        self.costCenters = new select({
            placeholder: 'مرکز هزینه',
            type: 'GET',
            url: function (value) { return "/DesktopModules/Atlas/api/CostCenter/list"; },
            size: 10,
            pagination: true,
            enableValidation: true,
            minimumResultsForSearch: 3,
            cascade: self.assistances.selectedItems
        });
        self.employmentTypes = new select({
            placeholder: 'نوع استخدام',
            type: 'GET',
            url: function (value) { return "/DesktopModules/Atlas/api/EmploymentType/list"; },
            size: 10,
            pagination: true,
            enableValidation: true,
            minimumResultsForSearch: 3,
            cascade: self.assistances.selectedItems
        });


        //Initialize and Display Loader
        self.loading = ko.observable(true);

        //define DataTable

        self.dtColumns = [
            { targets: 1, data: "ID", title: "ID", visible: false },
            { targets: 2, data: "ID", title: "ردیف", width: 30, orderable: false, render: function (data, type, row, meta) { return meta.row + meta.settings._iDisplayStart + 1; } },
            { targets: 3, data: "PersonId", title: "PersonId", visible: false },
            { targets: 4, data: "PersonFullName", title: "نام و نام خانوادگی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 5, data: "PersonCode", title: "کد پرسنلی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 6, data: "HasOverTimeWork", title: "مجوز اضافه کاری", visible: false },
            { targets: 7, data: "HasHolidayWork", title: "مجوز تعطیل کاری", visible: false },
            { targets: 8, data: "HasNightWork", title: "مجوز شب کاری", visible: false },

            { targets: 9, data: "MaxOverTime", title: "اضافه کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 10, data: "MaxHoliday", title: "تعطیل کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 11, data: "MaxNightly", title: "شب کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 12, data: "DailyPureOperation", title: "کارکرد خالص روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 13, data: "DailyAbsence", title: "غیبت روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 14, data: "DailyMeritoriouslyLeave", title: "مرخصی استحقاقی روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 15, data: "DailySickLeave", title: "مرخصی استعلاجی روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 16, data: "DailyWithoutPayLeave", title: "مرخصی بی حقوق روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 17, data: "AllowableOverTime", title: "اضافه کار مجاز", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 18, data: "UnallowableOverTime", title: "اضافه کار بی تاثیر", searchable: true, orderable: false, width: 70, className: 'text-center' }
        ];


        self.dtTotalColumns = [
           { targets: 0, data: "Total", title: "تعداد پرسنل", searchable: false, orderable: false, className: 'text-center', width: 70 },

           { targets: 1, data: "RealMaxOverTime", title: "سهمیه اضافه کاری", searchable: true, orderable: false, className: 'text-center' },
           { targets: 2, data: "MaxOverTime", title: "مجموع اضافه کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' },
           { targets: 3, data: "RemainingOverTime", title: "باقیمانده اضافه کاری", searchable: true, orderable: false, className: 'text-center' },

           { targets: 4, data: "RealMaxHoliday", title: "سهمیه ایام تعطیل", searchable: true, orderable: false, className: 'text-center' },
           { targets: 5, data: "MaxHoliday", title: "مجموع ایام تعطیل پیشنهادی", searchable: true, orderable: false, className: 'text-center' },
           { targets: 6, data: "RemainingHoliday", title: "باقیمانده تعطیل کاری", searchable: true, orderable: false, className: 'text-center' },

           { targets: 7, data: "RealMaxNightly", title: "سهمیه شب کاری", searchable: true, orderable: false, className: 'text-center' },
           { targets: 8, data: "MaxNightly", title: "مجموع شب کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' },
           { targets: 9, data: "RemainingNightly", title: "باقیمانده شب کاری", searchable: true, orderable: false, className: 'text-center' }
        ];

        self.dtOrganizationColumns = [

          { targets: 0, data: "OverTimeCount", title: "تعداد پرسنل مجاز", searchable: false, orderable: false, className: 'text-center' },
          { targets: 1, data: "RealMaxOverTime", title: "سهمیه اضافه کاری کل سازمان", searchable: true, orderable: false, className: 'text-center' },
          { targets: 2, data: "MaxOverTime", title: "مجموع اضافه کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' },

          { targets: 3, data: "HolidayCount", title: "تعداد پرسنل مجاز تعطیل کار", searchable: false, orderable: false, className: 'text-center' },
          { targets: 4, data: "RealMaxHoliday", title: "سهمیه تعطیل کاری کل سازمان", searchable: true, orderable: false, className: 'text-center' },
          { targets: 5, data: "MaxHoliday", title: "مجموع تعطیل کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' },

          { targets: 6, data: "NightlyCount", title: "تعداد پرسنل مجاز شب کار", searchable: false, orderable: false, className: 'text-center' },
          { targets: 7, data: "RealMaxNightly", title: "سهمیه شب کاری کل سازمان", searchable: true, orderable: false, className: 'text-center' },
          { targets: 8, data: "MaxNightly", title: "مجموع شب کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' }

        ];

        self.dtFilter = function (filter) {

            if (self.employmentTypes.selectedItems() != null) { if (self.employmentTypes.selectedItems().length) { self.employmentType(self.employmentTypes.selectedItems()); } }
            if (self.costCenters.selectedItems() != null) { if (self.costCenters.selectedItems().length) { self.costCenter(self.costCenters.selectedItems()); } }
            if (self.managements.selectedItems() != null) { if (self.managements.selectedItems().length) { self.department(self.managements.selectedItems()); } }
            if (self.assistances.selectedItems() != null) { if (self.assistances.selectedItems().length) { self.asistant(self.assistances.selectedItems()); } }
            if (self.organizations.selectedItems() != null) { if (self.organizations.selectedItems().length) { self.organization(self.organizations.selectedItems()); } }
            if (self.years.selectedItems().length) { self.year(self.years.selectedItems()); }
            if (self.months.selectedItems().length) { self.month(self.months.selectedItems()); }
             
            filter.departmentId = self.department();
            filter.employmentTypeId = self.employmentType();
            filter.costCenterId = self.costCenter();
            filter.asistantId = self.asistant();
            filter.organizationId = self.organization();
            filter.year = self.year();
            filter.month = self.month();
            filter.person = self.person();
            filter.cardnum = self.cardnum();
            filter.nationalcode = self.nationalcode();
            //-----------------------------
            self.selectedYear = self.year();
            self.selectedMonth = self.month();
        }


        //هر بار بارگذاری مجدد داده
        self.dtDraw = function () {
            self.checkApproved();
            self.loading(false);
        }
        self.dtOrgDraw = function () {
            self.loading(false);
        }
        self.dtTotalDraw = function () {
            self.loading(false);
        }
        self.dtPage = function () {
            self.loading(true);
        }
        self.dtTotalPage = function () {
            self.loading(true);
        }
        self.dtOrgPage = function () {
            self.loading(true);
        }
        self.dtSelect = function (id) {
            self.selectedId(id);
        }
        self.dtTotalSelect = function (id) { }
        self.dtOrgSelect = function (id) { }

        self.dtPageSize = ko.observable(99999999);

        self.dtPageSize.subscribe(function (value) {
            self.loading(true);
            self.overtimePersonDataTable.chagePageLength(value[0]);
            self.overtimeTotalPersonDataTable.chagePageLength(value[0]);
            self.overtimeOrganizationDataTable.chagePageLength(value[0]);
        });

        self.dtPageSizes = [20, 50, 100];

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
                    self.modal.cssClass('modal-dialog modal-lg');
                    self.modal.body({ name: 'spa-app-overtimeEdari-Edit', params: { id: data.ID } });
                    self.modal.visible(true);
                },
                icon: "glyphicon glyphicon-pencil",
            }]
        };
        self.overtimePersonDataTable = new dataTable({
            columnDefs: self.dtColumns,
            pageLength: self.dtPageSize(),
            filter: self.dtFilter,
            draw: self.dtDraw,
            page: self.dtPage,
            select: self.dtSelect,
            url: '/DesktopModules/Atlas/api/overtimeedari/list',
            type: 'GET',
            actionButtons: self.actionButtons,
            selected: true,
            keybord: true
        });

        self.overtimeTotalPersonDataTable = new dataTable({
            columnDefs: self.dtTotalColumns,
            pageLength: 1,
            filter: self.dtFilter,
            draw: self.dtTotalDraw,
            page: self.dtTotalPage,
            select: self.dtTotalSelect,
            info: 0,
            paging: 0,
            url: '/DesktopModules/Atlas/api/overtimeedari/listTotal',
            type: 'GET'
        });

        self.overtimeOrganizationDataTable = new dataTable({
            columnDefs: self.dtOrganizationColumns,
            pageLength: 1,
            filter: self.dtFilter,
            draw: self.dtOrgDraw,
            page: self.dtOrgPage,
            select: self.dtOrgSelect,
            info: 0,
            paging: 0,
            url: '/DesktopModules/Atlas/api/overtimeedari/OrganizationList',
            type: 'GET'
        });


        //Define Filters  
        self.department = ko.observable();
        self.costCenter = ko.observable();
        self.employmentType = ko.observable();
        self.asistant = ko.observable();
        self.organization = ko.observable(-1);
        self.year = ko.observable(1395);
        self.month = ko.observable(1);
        self.person = ko.observable('');
        self.cardnum = ko.observable('');
        self.nationalcode = ko.observable('');

        //Other Feilds

        self.search = function () {
            self.loading(true);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
            self.overtimeOrganizationDataTable.current.draw();
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
            self.assistances.current.empty();
            self.assistances.current.select2(self.assistances.options);
            self.assistances.selectedItems(null);
            self.managements.current.empty();
            self.managements.current.select2(self.managements.options);
            self.managements.selectedItems(null);
            self.costCenters.current.empty();
            self.costCenters.current.select2(self.costCenters.options);
            self.costCenters.selectedItems(null);
            self.employmentTypes.current.empty();
            self.employmentTypes.current.select2(self.employmentTypes.options);
            self.employmentTypes.selectedItems(null);

            self.department(0);
            self.costCenter(0);
            self.employmentType(0);
            self.asistant(0);
            self.organization(-1);
            self.year(GetCurrentYear()[0].id);
            self.month(GetCurrentMonth()[0].id);
            self.person('');
            self.displayFilter(true);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
            self.overtimeOrganizationDataTable.current.draw();
        }

        self.displayFilter = ko.observable(true);
        self.displaySearch = function () {
            self.displayFilter(!self.displayFilter());
        }

        self.goHome = function () {
            document.location.href = "/";
        }

        //config modal
        self.modal = new modal(function (button, data) {
            switch (currentAction) {
                case 'create':
                    break;
                case 'edit':
                    if (button == "save") {
                        self.loading(true);
                        self.overtimePersonDataTable.current.draw();
                        self.overtimeTotalPersonDataTable.current.draw();
                        self.overtimeOrganizationDataTable.current.draw();
                        message.info("ویرایش با موفقیت انجام شد.");
                    }
                    break;
                case 'remove':
                    break;
                case 'cancel':
                    break;
            }
        });

        self.finalApprove = function () {
            var r = confirm("آیا از تایید نهایی کارتابل اطمینان دارید!");
            if (r == true) {
                self.loading(true);
                var model = {
                    year: self.selectedYear,
                    month: self.selectedMonth
                };
                dataService.post('/DesktopModules/Atlas/api/overtime/approve/', model)
                        .done(function (data) {
                            self.loading(false);
                            message.success("اطلاعات با موفقیت تایید گردید");
                            self.displayFinalApprove(false);

                        }, function (error) {
                            self.loading(false);
                            message.error("خطا در ارسال اطلاعات");
                        });
            }

        };

        self.send = function () {
            var r = confirm("آیا از تایید نهایی کارتابل اطمینان دارید!");
            if (r == true) {
                params.loading(true);
                 
                if (self.managements.selectedItems() != null) { if (self.managements.selectedItems().length) { self.department(self.managements.selectedItems()); } }
                if (self.costCenters.selectedItems() != null) { if (self.costCenters.selectedItems().length) { self.costCenter(self.costCenters.selectedItems()); } }
                if (self.employmentTypes.selectedItems() != null) { if (self.employmentTypes.selectedItems().length) { self.employmentType(self.employmentTypes.selectedItems()); } }
                if (self.assistances.selectedItems() != null) { if (self.assistances.selectedItems().length) { self.asistant(self.assistances.selectedItems()); } }
                if (self.organizations.selectedItems() != null) { if (self.organizations.selectedItems().length) { self.organization(self.organizations.selectedItems()); } }
                if (self.years.selectedItems().length) { self.year(self.years.selectedItems()); }
                if (self.months.selectedItems().length) { self.month(self.months.selectedItems()); }

                var model = {
                    year: self.year(),
                    month: self.month(),
                    organizationId: self.organization(),
                    asistantId: self.asistant(),
                    departmentId: self.department(),
                    costCenterId: self.costCenter(),
                    employmentTypeId: self.employmentType(),
                    person: self.person()
                };
                dataService.post('/DesktopModules/Atlas/api/OverTimeEdari/sendItems/', model)
                    .done(function (data) {
                         
                        params.loading(false);
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
            self.overtimeTotalPersonDataTable.current.draw();
            self.overtimeOrganizationDataTable.current.draw();
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