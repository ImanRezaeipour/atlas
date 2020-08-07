define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/approvalschedule/list',
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

        var currentAction = '';

        //Initialize and Display Loader
        self.loading = ko.observable(true);

        //define DataTable
        self.dtURL = '/api/approvalschedule/list';
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
            { targets: 2, data: "ApprovalType", title: "نوع", searchable: true, orderable: false, className: 'select' },
            {
                targets: 3, data: "DateFrom", title: "از تاریخ", searchable: true, orderable: false, className: 'text-center', render: function (data) {
                    return moment(data, 'YYYY/MM/DD').format('jYYYY/jM/jD');
                }
            },
            {
                targets: 4, data: "DateTo", title: "تا تاریخ", searchable: true, orderable: false, className: 'text-center',
                render: function (data) {
                    return moment(data, 'YYYY/MM/DD').format('jYYYY/jM/jD');
                }
            }
        ];

        self.dtFilter = function (filter) {
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
            self.approvalScheduleDataTable.chagePageLength(value[0]);
        });

        self.dtPageSizes = [20, 50, 100];

        self.approvalScheduleDataTable = new dataTable(self.dtColumns, self.dtPageSize(), self.dtFilter, self.dtDraw, self.dtPage, self.dtSelect, self.dtURL);


        //config modal
        self.modal = new modal(function (button, data) {
            switch (currentAction) {
                case 'create': 
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
            }
        });
          
        self.edit = function () {
            currentAction = 'edit';
            self.modal.title('ویرایش زمان بندی');
            self.modal.body({ name: 'spa-app-approvalScheduleEdit', params: { id: self.selectedId() } });
            self.modal.visible(true);
        };

        self.refresh = function () {
            self.loading(true);
            self.approvalScheduleDataTable.current.draw();
        }
    }

    return {
        viewModel: viewModel,
        template: template
    };
});