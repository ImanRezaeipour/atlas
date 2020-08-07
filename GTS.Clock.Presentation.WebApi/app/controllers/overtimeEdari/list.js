define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/overtimeEdari/list',
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

        //عدم نمایش لودر
        params.loading(false);

        self.selectedYear = 0;
        self.selectedMonth = 0;
        self.records = ko.observable([]);
        self.disabled = ko.observable(true);
        self.selectedId = ko.observable();
        self.selectedDepartmentId = ko.observable();
        self.departments = [];
        var currentAction = '';
        self.displayFinalApprove = ko.observable(true);
        self.spinner = ko.observable('glyphicon glyphicon-floppy-save');

        var gDate = new Date();
        var persian = jd_to_persian(gregorian_to_jd(gDate.getFullYear(), gDate.getMonth() + 1, gDate.getDate()))
        var monthNames = ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];

        //placeHolder, tags, multiple, maximumSelectionLength, minimumInputLenght, url, pageSize, templateResult, selectedItems,localdata, enableValidation
        self.years = new select('سال', false, false, null, null, '/api/yearmonth/GetYears', 12, null, [{ id: persian[0], text: persian[0].toString() }], null, true);
        self.months = new select('ماه', false, false, null, null, '/api/yearmonth/GetMonths', 12, null, [{ id: persian[1], text: monthNames[persian[1] - 1].toString() }], null, true);

        //Initialize and Display Loader
        self.loading = ko.observable(true);

        self.departments = dataService.get('/api/Department/GetAllManagerDepartmentTree').done(function (data) {
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

            self.loading(false);
        }, function () {
            //خطا در بازیابی اطلاعات
        });

        //define DataTable
        self.dtURL = '/api/overtimeedari/list';
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

            { targets: 1, data: "ID", title: "ID", visible: false },
            { targets: 2, data: "PersonId", title: "PersonId", visible: false },
            { targets: 3, data: "PersonFullName", title: "نام و نام خانوادگی", searchable: true, orderable: false, className: 'text-center' },
            { targets: 4, data: "PersonCode", title: "کد پرسنلی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 5, data: "HasPeyment", title: "مجوز اضافه کاری", visible: false },
            { targets: 6, data: "HasHolidayWork", title: "مجوز تعطیل کاری", visible: false },
            { targets: 7, data: "HasNightWork", title: "مجوز شب کاری", visible: false },

            { targets: 8, data: "MaxOverTime", title: "اضافه کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 9, data: "MaxHoliday", title: "تعطیل کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },
            { targets: 10, data: "MaxNightly", title: "شب کاری پیشنهادی", searchable: true, orderable: false, width: 80, className: 'text-center' },

            { targets: 11, data: "DailyPureOperation", title: "کارکرد خالص روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 12, data: "DailyAbsence", title: "غیبت روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 13, data: "DailyMeritoriouslyLeave", title: "مرخصی استحقاقی روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 14, data: "DailySickLeave", title: "مرخصی استعلاجی روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 15, data: "DailyWithoutPayLeave", title: "مرخصی بی حقوق روزانه", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 16, data: "AllowableOverTime", title: "اضافه کار مجاز", searchable: true, orderable: false, width: 70, className: 'text-center' },
            { targets: 17, data: "UnallowableOverTime", title: "اضافه کار غیر مجاز", searchable: true, orderable: false, width: 70, className: 'text-center' }
        ];

        self.dtTotalColumns = [
            { targets: 0, data: "Total", title: "تعداد پرسنل", searchable: false, orderable: false, className: 'text-center' },

            { targets: 1, data: "RealMaxOverTime", title: "مجموع سهمیه اضافه کاری", searchable: true, orderable: false, width: 100, className: 'text-center' },
            { targets: 2, data: "MaxOverTime", title: "سهمیه اضافه کاری پیشنهادی", searchable: true, orderable: false, width: 100, className: 'text-center' },
            { targets: 3, data: "RemainingOverTime", title: "باقیمانده اضافه کاری", searchable: true, orderable: false, width: 100, className: 'text-center' },

            { targets: 4, data: "RealMaxHoliday", title: "مجموع سهمیه تعطیل کاری", searchable: true, orderable: false, width: 100, className: 'text-center' },
            { targets: 5, data: "MaxHoliday", title: "سهیمه تعطیل کاری پیشنهادی", searchable: true, orderable: false, width: 100, className: 'text-center' },
            { targets: 6, data: "RemainingHoliday", title: "باقیمانده تعطیل کاری", searchable: true, orderable: false, width: 100, className: 'text-center' },

            { targets: 7, data: "RealMaxNightly", title: "مجموع سهمیه شب کاری", searchable: true, orderable: false, width: 100, className: 'text-center' },
            { targets: 8, data: "MaxNightly", title: "سهمیه شب کاری پیشنهادی", searchable: true, orderable: false, width: 100, className: 'text-center' },
            { targets: 9, data: "RemainingNightly", title: "باقیمانده شب کاری", searchable: true, orderable: false, width: 100, className: 'text-center' }
        ];

        self.dtFilter = function (filter) {

            if (self.years.selectedItems().length)
                self.year(self.years.selectedItems());
            if (self.months.selectedItems().length)
                self.month(self.months.selectedItems());

            filter.departmentId = self.selectedDepartmentId();
            filter.year = self.year();
            filter.month = self.month();
            //-----------------------------
            self.selectedYear = self.year();
            self.selectedMonth = self.month();
        }

        //هر بار بارگذاری مجدد داده
        self.dtDraw = function () {
            self.checkApproved();
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

        self.overtimePersonDataTable = new dataTable(self.dtColumns, self.dtPageSize(), self.dtFilter, self.dtDraw, self.dtPage, self.dtSelect, self.dtURL);

        self.overtimeTotalPersonDataTable = new dataTable(self.dtTotalColumns, 1, self.dtFilter, self.dtTotalDraw, self.dtTotalPage, self.dtTotalSelect, '/api/overtimeedari/listTotal');
         
        //Define Filters  
        self.selectedDepartmentId = ko.observable(1);
        self.year = ko.observable(1395);
        self.month = ko.observable(1);

        //Other Feilds

        self.search = function () {
            self.loading(true);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
        }

        self.checkApproved = function () {
            var model = {
                year: self.selectedYear,
                month: self.selectedMonth
            };
            dataService.post('/api/overtime/IsAproved/', model)
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
            self.selectedDepartmentId = ko.observable(1);
            self.year(1395);
            self.month(1);
            self.displayFilter(false);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
        }

        self.displayFilter = ko.observable(false);
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
                dataService.post('/api/overtime/approve/', model)
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
        self.add = function () {
            //currentAction = 'add';
            //self.modal.title('درج سرانه جدید');
            //self.modal.body({ name: 'spa-app-overtimeCreate', params: {} });
            //self.modal.visible(true);
        };
        self.edit = function () {

            currentAction = 'edit';
            self.modal.title('ویرایش سرانه');
            self.modal.body({ name: 'spa-app-overtimeEdariEdit', params: { id: self.selectedId() } });
            self.modal.visible(true);
        };
        self.refresh = function () {
            self.loading(true);
            self.overtimePersonDataTable.current.draw();
            self.overtimeTotalPersonDataTable.current.draw();
        }

        $("#dropDownButton").jqxDropDownButton({ rtl: true, width: '100%', height: 34, theme: "bootstrap" });
        $('#jqxTree').on('select', function (event) {
            var args = event.args;
            var item = $('#jqxTree').jqxTree('getItem', args.element);
            self.selectedDepartmentId(item.id);
            var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 5px;">' + item.label + '</div>';
            $("#dropDownButton").jqxDropDownButton('setContent', dropDownContent);
        });
    }

    return {
        viewModel: viewModel,
        template: template
    };
});