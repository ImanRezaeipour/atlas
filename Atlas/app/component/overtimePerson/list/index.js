define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/DesktopModules/Atlas/app/component/overtimePerson/list/index.html' + '?ver=' + (new Date()).getTime(),
        'spa.engine.core.dataTable',
        'spa.engine.infrastructure.moment.jalali',
        'spa.engine.infrastructure.datepicker',
        'spa.engine.core.data',
        'spa.engine.widget.message',
        'spa.engine.core.select',
        'spa.engine.core.modal',
        'jqx-all', 'jqxcheckbox'],
function ($, ko, template, dataTable, moment, datepicker, dataService, message, select, modal) {
    function viewModel(params) {
        var self = this;
        debugger;
        //عدم نمایش لودر
        params.loading(false);

        self.username = params.root.user;
        self.records = ko.observable([]);
        self.disabled = ko.observable(true);
        self.selectedId = ko.observable();
        self.selectedDepartmentId = ko.observable();
        self.departments = [];
        self.currentManagerId = 0;

        //Define Filters  
        self.selectedDepartmentId = ko.observable(0);
        self.year = ko.observable(1395);
        self.month = ko.observable(1);
        self.person = ko.observable('');
        self.directManager = ko.observable("1");
        //Other Feilds
         
        self.displaySubstitute = ko.computed(function () {
            if (self.directManager() == 1)
                return false;
            else
                return true;
        });

        var currentAction = '';
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

        self.years = new select({ placeholder: 'سال', type: 'GET', url: '/DesktopModules/Atlas/api/yearmonth/GetYears', size: 12, enableValidation: true, selectItem: GetCurrentYear() });
        self.months = new select({ placeholder: 'ماه', type: 'GET', url: '/DesktopModules/Atlas/api/yearmonth/GetMonths', size: 12, enableValidation: true, selectItem: GetCurrentMonth() });
        self.years.selectedItems(GetCurrentYear()[0].id.toString());
        self.months.selectedItems(GetCurrentMonth()[0].id.toString());

        self.substituteManagers = new select({ placeholder: 'مدیر', type: 'GET', url: '/DesktopModules/Atlas/api/overtimeperson/GetSubstituteManager', size: 50, enableValidation: true, minimumResultsForSearch: 3 });
         
        //Initialize and Display Loader
        self.loading = ko.observable(true);

        function GetTreeParam() {
            var id = 0;
            if (self.substituteManagers.selectedItems().length && self.directManager() == 0) {
                id = self.substituteManagers.selectedItems();
            }
            return id;
        }

        function FillDepartment() {
            self.departments = dataService.get('/DesktopModules/Atlas/api/Department/GetAllManagerDepartmentTree/', GetTreeParam())
                .done(function (data) {
                    self.departments = data;
                    var source =
                      {
                          datatype: "observablearray",
                          datafields: [{ name: 'id' }, { name: 'parentid' }, { name: 'text' }, { name: 'value' }],
                          id: 'id',
                          localdata: self.departments
                      };
                    var dataAdapter = new $.jqx.dataAdapter(source);
                    dataAdapter.dataBind();
                    self.records = dataAdapter.getRecordsHierarchy('id', 'parentid', 'items', [{ name: 'text', map: 'label' }]);

                    $("#jqxTree").jqxTree({ source: self.records, rtl: true, width: '100%', height: 300, theme: "bootstrap" });

                    //self.loading(false);
                }, function () {
                    //خطا در بازیابی اطلاعات
                });
        }

        FillDepartment();

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
                    self.modal.body({ name: 'spa-app-overtimePerson-Edit', params: { id: data.ID, currentManagerId: self.currentManagerId } });
                    self.modal.visible(true);
                },
                icon: "glyphicon glyphicon-pencil",
            }]
        };
        self.dtColumns = [
            { targets: 1, data: "ID", title: "ID", visible: false },
            { targets: 2, data: "PersonId", title: "PersonId", visible: false },
            { targets: 3, data: "PersonFullName", title: "نام و نام خانوادگی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 4, data: "PersonCode", title: "کد پرسنلی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 5, data: "HasOverTimeWork", title: "مجوز اضافه کاری", visible: false },
            { targets: 6, data: "HasHolidayWork", title: "مجوز تعطیل کاری", visible: false },
            { targets: 7, data: "HasNightWork", title: "مجوز شب کاری", visible: false },

            { targets: 8, data: "MaxOverTime", title: "اضافه کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 9, data: "MaxHoliday", title: "تعطیل کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 10, data: "MaxNightly", title: "شب کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 11, data: "DailyPureOperation", title: "کارکرد خالص روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 12, data: "DailyAbsence", title: "غیبت روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 13, data: "DailyMeritoriouslyLeave", title: "مرخصی استحقاقی روزانه", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 14, data: "DailySickLeave", title: "مرخصی استعلاجی روزانه", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 15, data: "DailyWithoutPayLeave", title: "مرخصی بی حقوق روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 16, data: "AllowableOverTime", title: "اضافه کار مجاز", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 17, data: "UnallowableOverTime", title: "اضافه کار بی تاثیر", searchable: true, orderable: false, width: 70, className: 'text-center' }
        ];

        self.dtTotalColumns = [
            { targets: 0, data: "Total", title: "تعداد پرسنل", searchable: false, orderable: false, className: 'text-center', width: 70 },

            { targets: 1, data: "RealMaxOverTime", title: "سهمیه اضافه کاری", searchable: true, orderable: false, className: 'text-center' },
            { targets: 2, data: "MaxOverTime", title: "مجموع اضافه کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 3, data: "RemainingOverTime", title: "باقیمانده اضافه کاری", searchable: true, orderable: false, className: 'text-center' },

            { targets: 4, data: "RealMaxHoliday", title: "سهمیه ایام تعطیل", searchable: true, orderable: false, className: 'text-center' },
            { targets: 5, data: "MaxHoliday", title: "مجموع ایام تعطیل پیشنهادی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 6, data: "RemainingHoliday", title: "باقیمانده تعطیل کاری", searchable: true, orderable: false, className: 'text-center' },

            { targets: 7, data: "RealMaxNightly", title: "مجموع سهمیه شب کاری", searchable: true, orderable: false, className: 'text-center' },
            { targets: 8, data: "MaxNightly", title: "مجموع شب کاری پیشنهادی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 9, data: "RemainingNightly", title: "باقیمانده شب کاری", searchable: true, orderable: false, className: 'text-center' }
        ];

        self.dtFilter = function (filter) {

            if (self.years.selectedItems().length) { self.year(self.years.selectedItems()); }
            if (self.months.selectedItems().length) { self.month(self.months.selectedItems()); }

            filter.departmentId = self.selectedDepartmentId();
            filter.year = self.year();
            filter.month = self.month();
            filter.person = self.person();
            filter.directManager = self.directManager();
            filter.substituteManager = "";
            self.currentManagerId = 0;
            if (self.substituteManagers.selectedItems().length && self.directManager() == 0) {
                filter.substituteManager = self.substituteManagers.selectedItems();
                self.currentManagerId = self.substituteManagers.selectedItems();
            }

            //Other Field ...
        }

        //هر بار بارگذاری مجدد داده
        self.dtDraw = function () {
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
        self.dtSelect = function (id) {
            self.selectedId(id);
        }
        self.dtTotalSelect = function (id) { }

        self.dtPageSize = ko.observable(20);

        self.dtPageSize.subscribe(function (value) {
            self.loading(true);
            self.overtimePersonDataTable.chagePageLength(value[0]);
            self.overtimeTotalPersonDataTable.chagePageLength(value[0]);
        });

        self.dtPageSizes = [20, 50, 100];

        self.overtimePersonDataTable = new dataTable({
            columnDefs: self.dtColumns,
            pageLength: self.dtPageSize(),
            filter: self.dtFilter,
            draw: self.dtDraw,
            page: self.dtPage,
            select: self.dtSelect,
            url: '/DesktopModules/Atlas/api/overtimeperson/list',
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
            info: false,
            paging: false,
            url: '/DesktopModules/Atlas/api/overtimeperson/listTotal',
            type: 'GET'
        });


        self.search = function () {
            self.loading(true);
            FillDepartment();
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
        }

        self.clear = function () {
            self.loading(true);
            self.selectedDepartmentId = ko.observable(0);
            self.year(GetCurrentYear()[0].id);
            self.month(GetCurrentMonth()[0].id);
            self.directManager("1");
            self.person('');
            self.displayFilter(false);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
            $("#jqxTree").jqxTree('selectItem', null);
            $("#dropDownButton").jqxDropDownButton('setContent', null);
        }

        self.displayFilter = ko.observable(true);
        self.displaySearch = function () {
            self.displayFilter(!self.displayFilter());
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
            //currentAction = 'add';
            //self.modal.title('درج سرانه جدید');
            //self.modal.body({ name: 'spa-app-overtimeCreate', params: {} });
            //self.modal.visible(true);
        };

        self.refresh = function () {
            self.loading(true);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
        }

        self.close = function () {
            window.top.close();
        }

        $("#dropDownButton").jqxDropDownButton({ rtl: true, width: '100%', height: 34, theme: "bootstrap" });
        $('#jqxTree').on('select', function (event) {
            var args = event.args;
            var item = $('#jqxTree').jqxTree('getItem', args.element);
            self.selectedDepartmentId(item.id);
            var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 5px;">' + item.label + '</div>';
            $("#dropDownButton").jqxDropDownButton('setContent', dropDownContent);
        });


        $('#dropDownButtonContentdropDownButton').on('click', function () {
            $('#panelContentpaneljqxTree').css('left', '0');
        });
    }

    return {
        viewModel: viewModel,
        template: template
    };
});