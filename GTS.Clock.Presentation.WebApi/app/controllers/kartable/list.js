define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/kartable/list',
        'spa.engine.core.dataTable',
        'spa.engine.infrastructure.moment.jalali'],
function ($, ko, template, dataTable, moment) {
    function viewModel(params) {
        var self = this;

        //عدم نمایش لودر
        params.loading(false);

        self.loading = ko.observable(false);

        self.selectedId = ko.observable();
        //define DataTable
        self.dtURL = '/api/kartable';
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
              { targets: 2, data: "Barcode", title: "کد پرسنلی", searchable: false, sortable: false, orderable: false },
              { targets: 3, data: "Applicant", title: "درخواست کننده", searchable: false, orderable: false },
              { targets: 4, data: "RequestTitle", title: "عنوان درخواست", searchable: false, orderable: false },
              { targets: 5, data: "TheFromDate", title: "از تاریخ", searchable: false, orderable: false },
              { targets: 6, data: "TheToDate", title: "تا تاریخ", searchable: false, orderable: false },
              { targets: 7, data: "TheFromTime", title: "از ساعت", searchable: false, orderable: false },
              { targets: 8, data: "TheToTime", title: "تا ساعت", searchable: false, orderable: false },
              { targets: 9, data: "TheDuration", title: "مقدار", searchable: false, orderable: false },
              { targets: 10, data: "RegistrationDate", title: "تاریخ صدور", searchable: false, orderable: false },
              { targets: 11, data: "OperatorUser", title: "صادر کننده", searchable: false, orderable: false },
              { targets: 12, data: "Description", title: "شرح درخواست", searchable: false, orderable: false }
        ];
        self.dtFilter = function (filter) {
            filter.firstName = self.firstName();
            filter.lastName = self.lastName();
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

        self.dtPageSize = ko.observable(5);
        self.dtPageSize.subscribe(function (value) {
            self.loading(true);
            self.kartableDataTable.chagePageLength(value[0]);
        });

        self.dtPageSizes = [2, 4, 6, 8];
        self.kartableDataTable = new dataTable(self.dtColumns, self.dtPageSize(), self.dtFilter,
            self.dtDraw, self.dtPage, self.dtSelect, self.dtURL);

        //Define Filters
        self.firstName = ko.observable();
        self.lastName = ko.observable();
        //Other Feilds

        self.search = function () {
            self.loading(true);
            self.kartableDataTable.current.draw();
        }

        self.clear = function () {
            self.loading(true);
            self.firstName('');
            self.lastName('');
            self.displayFilter(false);
            self.kartableDataTable.current.draw();
        }

        self.displayFilter = ko.observable(false);
        self.displaySearch = function () {
            self.displayFilter(true);
        }
    }

    return {
        viewModel: viewModel,
        template: template
    };
});


