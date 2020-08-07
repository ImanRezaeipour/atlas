define(['jquery',
        'ko',
        'spa.engine.infrastructure.htmlLoader!/hrms/index',
        'spa.engine.widget.message',
        'spa.engine.core.data'],
function ($, ko, template, message, dataService) {
    function viewModel(params) {
        var self = this;

        params.loading(false);

        self.notify = function () {
            //alert('Ok');
            message.info("This is a information");
            message.success("This is a success")
            message.warning("This is a warning")
            message.error("This is a error")
        }

        self.loading = ko.observable(false);

        self.showLoading = function () {
            //Call deffer Processing
            self.loading(true);

            setTimeout(function () {
                self.loading(false);
            }, 2000);
        }

        self.items = ko.observableArray([]);

        self.loadData = function () {
            self.loading(true);
            //Restfull API
            //GET, POST, PUT, DELETE
            //get, post, put, delete
            dataService.get('/api/hrms').done(function (data) {
                self.loading(false);
                self.items(data);
                message.info("تعداد " + data.length + " رکورد بازیابی شد.");
            }, function (error) {
            });
        }
    }

    return {
        viewModel: viewModel,
        template: template
    };
});